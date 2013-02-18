using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.Runtime;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using System.Runtime.InteropServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Windows;
using Autodesk.AutoCAD.Geometry;
using System.Collections;
using Telerik.WinControls;
using Autodesk.AutoCAD.ApplicationServices;
using com.ccepc.utils;
using com.ccepc.entities;
using DNA;
using Autodesk.AutoCAD.Interop;
using System.Configuration;
using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace CAD
{

    public static class Commands
    {
        //将窗口显示
        [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
        private static extern int ShowWindow(IntPtr hWnd, int nCmdShow);
        //切换窗体显示
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("acad.exe", EntryPoint = "?acedGetUserFavoritesDir@@YAHPA_W@Z", CharSet = CharSet.Auto)]
        public static extern bool acedGetUserFavoritesDir([MarshalAs(UnmanagedType.LPWStr)] StringBuilder sDir);

        public static void AddMenu()
        {
            //COM方式获取AutoCAD应用程序对象
            AcadApplication acadApp = (AcadApplication)AcadApp.AcadApplication;
            //为AutoCAD添加一个新的菜单，并设置标题为"我的菜单"
            AcadPopupMenu pm = acadApp.MenuGroups.Item(0).Menus.Add("协同设计");
            //将定义的菜单显示在AutoCAD菜单栏的最后
            pm.InsertInMenuBar(acadApp.MenuBar.Count + 1);

            pm.AddMenuItem(pm.Count + 1, "登陆", "_Login ");
            pm.AddMenuItem(pm.Count + 1, "保存图纸", "_SaveFile ");
            pm.AddMenuItem(pm.Count + 1, "校审批注", "_CheckDwg ");
            pm.AddMenuItem(pm.Count + 1, "图纸目录", "_DwgList ");
            pm.AddMenuItem(pm.Count + 1, "批量打印", "_BatchPlot ");
            pm.AddMenuItem(pm.Count + 1, "注销", "_Logout ");
        }

        [CommandMethod("PathTest")]
        public static void PathTest()
        {
            //获取模块的完整路径
            string s1 = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            Tools.WriteMessageWithReturn("获取模块的完整路径:" + s1);
            //获取和设置当前目录（该进程从中启动的目录）的完全限定目录
            s1 = System.Environment.CurrentDirectory;
            Tools.WriteMessageWithReturn("获取和设置当前目录（该进程从中启动的目录）的完全限定目录" + s1);
            //获取程序的基目录
            s1 = System.AppDomain.CurrentDomain.BaseDirectory;
            Tools.WriteMessageWithReturn("获取程序的基目录" + s1);
            //获取和设置包括该应用程序目录的名称
            s1 = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            Tools.WriteMessageWithReturn("获取和设置包括该应用程序目录的名称" + s1);
            //获取当前运行的程序集
            System.Reflection.Module myModule = System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0];
            //获取当前运行的程序集的完整路径（包含文件名）
            s1 = myModule.FullyQualifiedName;
            Tools.WriteMessageWithReturn("获取当前运行的程序集的完整路径（包含文件名）" + s1);
        }

        [CommandMethod("BatchPlot")]
        public static void BatchPlot()
        {
            BatchPlot batchPlotForm = new BatchPlot();
            batchPlotForm.Show();
        }



        [CommandMethod("Login")]
        public static void Login()
        {
            if (AppInitialization.loginUser == null)
            {
                LoginForm loginForm = new LoginForm();
                loginForm.ShowDialog();
            }
            else
            {
                RadMessageBox.Show(AppInitialization.loginUser.realName + "已登陆,要更换登陆用户请先注销当前用户！");
            }
        }

        [CommandMethod("UserDataInfo")]
        public static void UserDataInfo()
        {
            FileInfo fileInfo = (FileInfo)Tools.Document.UserData["文件信息"];
            CheckStage checkStage = (CheckStage)Tools.Document.UserData["操作信息"];
            if(fileInfo != null)
            {
                Tools.WriteMessageWithReturn("文件信息:" + fileInfo.fileName);
            }
            if (checkStage != null)
            {
                Tools.WriteMessageWithReturn("操作信息:" + checkStage.stageName);
            }
            
        }

        [CommandMethod("AttachFileInfo")]
        public static void AttachFileInfo()
        {
            AcadApp.SetSystemVariable("cmdecho", 0);
            string fileId = Tools.Editor.GetString("文件id").StringResult;
            AcadApp.SetSystemVariable("cmdecho", 1);
            string[] args = new string[1];
            args[0] = fileId;
            object result = WebServiceHelper.InvokeWebService("UserWebservice", "getFileInfo", args);
            FileInfo fileInfo = JsonHelper.JsonDeserialize<FileInfo>(result.ToString());
            AppInitialization.fileInfoPanel.Text = "文件信息：" + fileInfo.fileName;
            Tools.Document.UserData.Add("文件信息", fileInfo);
            if(fileInfo.currentOperator.id == AppInitialization.loginUser.id)
            {
                Tools.Document.UserData.Add("操作信息",fileInfo.currentCheck);
            }
            
        }

        [CommandMethod("Logout")]
        public static void Logout()
        {
            AppInitialization.loginUser = null;
            AppInitialization.userInfoPanel.Text = "用户未登陆!";
        }

        [CommandMethod("OpenServerFile")]
        public static void OpenServerFile()
        {
            string fileName = Tools.Editor.GetString("文件名").StringResult;
            string url = Tools.Editor.GetString("文件来源").StringResult;
            string filePath = "c:\\xtcad\\" + fileName;
            FileUtil.SaveFileFromUrl(filePath, url);
            AcadApplication acadApp = (AcadApplication)AcadApp.AcadApplication;
            acadApp.Documents.Open(filePath, false, null);
            //string filePath = "c:\\xtcad\\" + fileName;
            //FileUtil.SaveFileFromUrl(filePath, url);
            //CommonTools.OpenDoc(filePath);
            //AcadApplication acadApp = (AcadApplication)AcadApp.AcadApplication;
            //IntPtr appHwd = new IntPtr(acadApp.HWND);
            //ShowWindow(appHwd, 3);
            //SetForegroundWindow(appHwd);
            //acadApp.Documents.Open(filePath, false, null);
        }

        [CommandMethod("test")]
        public static void test()
        {
            string[] args = new string[1];
            args[0] = "测试程序";
            object result = WebServiceHelper.InvokeWebService("HelloWebservice", "getName", args);
            List<User> fileTypes = JsonHelper.JsonDeserialize<List<User>>(result.ToString());
            //FileType ft = parse<FileType>(result.ToString());
            //ed.WriteMessage(fileType.fileTypeName);
            Tools.WriteMessageWithReturn("共有" + fileTypes.Count.ToString() + "种文件类型");
            Tools.RunCommand(true, "test1", "12", "111");
        }

        [CommandMethod("test1")]
        public static void test1()
        {
            int firstParam = Tools.Editor.GetInteger("参数1").Value;
            int secondParam = Tools.Editor.GetInteger("参数2").Value;

            ((AcadDocument)Tools.Document.AcadDocument).SendCommand("line");

            Tools.WriteMessageWithReturn("参数1：" + firstParam);
            Tools.WriteMessageWithReturn("参数2：" + secondParam);
        }


        [CommandMethod("ArxTest")]
        public static void ArxTest()
        {
            StringBuilder sDir = new StringBuilder(256);
            bool bRet = acedGetUserFavoritesDir(sDir);

            if (bRet && sDir.Length > 0)
            {
                Tools.WriteMessageWithReturn("收藏夹目录为: " + sDir.ToString());
            }
        }

        [CommandMethod("InfoTips")]
        public static void InfoTips()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.PointMonitor += new PointMonitorEventHandler(ed_PointMonitor);
        }

        [CommandMethod("SaveFile")]
        public static void SaveFile()
        {
            if (AppInitialization.loginUser == null)
            {
                Login();
            }
            else
            {
                SaveFileForm saveFileForm = new SaveFileForm();
                AcadApp.ShowModelessDialog(saveFileForm);
                //saveFileForm.ShowDialog();
            }
        }

        private static void ed_PointMonitor(object sender, PointMonitorEventArgs e)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            FullSubentityPath[] paths = e.Context.GetPickedEntities();

            if (paths.Length > 0)
            {

                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    ObjectId entId = paths[0].GetObjectIds()[0];
                    BlockReference bref = (Entity)trans.GetObject(entId, OpenMode.ForRead) as BlockReference;

                    if (bref != null)
                    {
                        XData data = new XData(bref.ObjectId);

                        if (data.HasXData())
                        {
                            ArrayList regappname = new ArrayList();
                            IEnumerator appname = data.GetAppNames().GetEnumerator();

                            while (appname.MoveNext())
                            {
                                regappname.Add(appname.Current);
                            }

                            if (!regappname.Contains("CSCAD"))
                            {
                                return;
                            }
                            Dictionary<string, string> blockinfodic = data.GetParamsWithAppName("CSCAD");

                            if (blockinfodic == null)
                            {
                                return;
                            }

                            switch (blockinfodic["块类型"].ToString())
                            {

                                case "PID设备":
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    trans.Commit();
                }

            }
        }
    }
}
