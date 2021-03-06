﻿using System;
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
using System.IO;

namespace CAD
{

    public static class Commands
    {
        public static PaletteSet ps;
        public static DesignePalette dp;

        //将窗口显示
        [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
        private static extern int ShowWindow(IntPtr hWnd, int nCmdShow);
        //切换窗体显示
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("acad.exe", EntryPoint = "?acedGetUserFavoritesDir@@YAHPA_W@Z", CharSet = CharSet.Auto)]
        public static extern bool acedGetUserFavoritesDir([MarshalAs(UnmanagedType.LPWStr)] StringBuilder sDir);

        private static CADService service = HessianHelper.getServiceInstance();

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

        [CommandMethod("hessian")]
        public static void hessian()
        {
            //string userName = Tools.Editor.GetString("用户名").StringResult;
            //List<Node> nodes = CADServiceImpl.getSubProjectNodes(AppInitialization.loginUser.id.ToString());
            ////User user = CADServiceImpl.findUser(userName);
            //foreach (Node node in nodes)
            //{
            //    Tools.WriteMessage(node.label + ":" + node.type);
            //    foreach (Node nc in node.children)
            //    {
            //        Tools.WriteMessage(nc.label + ":" + nc.type);
            //    }
            //}
            //如果面板还没有被创建
            if (ps == null)
            {
                //新建一个面板对象，标题为"工具面板"
                ps = new PaletteSet("CIADMS面板");
                //设置面板的最小尺寸为控件的尺寸
                ps.MinimumSize = new System.Drawing.Size(150, 240);
                if (dp == null)
                {
                    dp = new DesignePalette();
                }
                //添加命令工具面板项
                ps.Add("CIADMS设计面板", dp);
            }
            //获取命令行编辑器对象，主要是为了坐标转换用
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //在设置停靠属性之前，必须让面板可见，否则总是停靠在窗口的左侧
            ps.Visible = true;
            //设置面板不停靠在窗口的任一边
            ps.Dock = DockSides.Left;
            
        }

        [CommandMethod("PlotFile")]
        public static void PlotFile()
        {
            string fileName = Tools.Editor.GetString("文件路径").StringResult;
            if (File.Exists(fileName))
            {
                DocumentCollection acDocMgr = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager;
                Document doc = acDocMgr.Open(fileName, false);
                Database db = doc.Database;
                List<FrameInfo> frames = PlotUtil.GetDrawingFrames(db);
                foreach (FrameInfo frameInfo in frames)
                {
                    PlotUtil.Plot(doc, frameInfo.extents2d, "pdfFactory Pro", 3, frameInfo.scale);
                }
            }
        }

        [CommandMethod("RunCmd")]
        public static void RunCmd()
        {
            //string cmd = @"D:\360安全浏览器下载\pdfspme_win\pdfspme_cmd.exe  -mer -i D:\ftpHome\pdfcreator\1.pdf -i D:\ftpHome\pdfcreator\2.pdf -o D:\ftpHome\pdfcreator\4.pdf";
            //Tools.WriteMessage(CommonTools.RunCmd(cmd,0));
            string tempFile = Environment.GetEnvironmentVariable("TEMP");
            Tools.WriteMessage(tempFile);
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
            com.ccepc.entities.FileInfo fileInfo = (com.ccepc.entities.FileInfo)Tools.Document.UserData["文件信息"];
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
            com.ccepc.entities.FileInfo fileInfo = CADServiceImpl.getFileInfo(fileId);
            AppInitialization.fileInfoPanel.Text = "文件信息：" + fileInfo.fileName;
            if (!Tools.Document.UserData.Contains("文件信息"))
            {
                Tools.Document.UserData.Add("文件信息", fileInfo);
            }
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
