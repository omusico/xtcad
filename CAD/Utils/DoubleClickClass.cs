

/////以下为双击代码
using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.Interop.Common;
using System.Collections;
using System.Windows;
using System.Linq;
namespace CAD
{

    /// <summary>
    /// 当前文件双击事件
    /// </summary>
    public class CADDoubleClick
    {
        static string AppName;//传入的扩展数据名列表

        static bool m_DbClick = false;
        static Document doc;
        static Editor ed;
        static AcadDocument acaddoc;
        static ObjectId objSelId;
        static double dist = 1;//选择点与选择实体的距离偏差
        static Dictionary<string, string> copyentity;
        static bool selectornewdevice = false;

        public static void DoubleClickStart(string AppNameIn)
        {
            try
            {
                AppName = AppNameIn;
                doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                ed = doc.Editor;
                acaddoc = (AcadDocument)doc.AcadDocument;
                acaddoc.BeginDoubleClick += new _DAcadDocumentEvents_BeginDoubleClickEventHandler(beginDoubleClick);
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.DocumentLockModeChanged += new DocumentLockModeChangedEventHandler(vetoCommand);
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.DocumentCreated += new DocumentCollectionEventHandler(documentCreated);
                ed.SelectionAdded += new SelectionAddedEventHandler(ed_SelectionAdded);
            }
            catch
            { }
        }

        //属性块的双击而增加此事件
        static void ed_SelectionAdded(object sender, SelectionAddedEventArgs e)
        {
            try
            {
                if (e.AddedObjects.Count == 1)
                {
                    SelectedObject objSel = e.AddedObjects[0];
                    objSelId = objSel.ObjectId;
                }
                else
                {
                    // objSelId = ObjectId.Null; //不能加上，否则就和不用此方法效果一样
                }
            }
            catch
            { }
        }


        static void documentCreated(object sender, DocumentCollectionEventArgs e)
        {
            try
            {
                doc = e.Document;
                acaddoc = (AcadDocument)doc.AcadDocument;
                acaddoc.BeginDoubleClick += new _DAcadDocumentEvents_BeginDoubleClickEventHandler(beginDoubleClick);
            }
            catch
            { }
        }

        static void beginDoubleClick(object PickPoint)
        {
            try
            {
                PromptSelectionResult res = ed.SelectImplied();
                if (res.Status == PromptStatus.Error)//属性块双击不能选中的处理
                {
                    if (objSelId != ObjectId.Null)
                    {
                        ShowDialog(objSelId, PickPoint);//true表示是上次选中的（在方法中判断是属性块时用）
                        objSelId = ObjectId.Null;
                        return;
                    }
                }
                else
                {
                    SelectionSet SS = res.Value;
                    if (SS.GetObjectIds().Length == 1)
                    {
                        ObjectId oId = SS.GetObjectIds()[0];
                        ShowDialog(oId, PickPoint);
                        return;
                    }
                }
                m_DbClick = false;
            }
            catch
            {
                m_DbClick = false; //异常时保证面板等原来CAD自带的可用
            }
        }


        /// <summary>
        ///  //实现自定义窗口的调用
        /// </summary>
        /// <param name="oId">选中的ObjectId</param>
        /// <param name="ptPick">点取的点坐标</param>
        /// <param name="isSeled">是否是上次选中的</param>
        static void ShowDialog(ObjectId oId, object ptPick)
        {
            try
            {
                using (Transaction tr = doc.TransactionManager.StartTransaction())
                {
                    DBObject dbObj = tr.GetObject(oId, OpenMode.ForRead);
                    BlockReference bref = dbObj as BlockReference;
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
                            if (!regappname.Contains("CIADMS"))
                            {
                                m_DbClick = true;
                                return;
                            }
                            Dictionary<string, string> blockinfodic = data.GetParamsWithAppName("CIADMS");
                            if (blockinfodic == null)
                            {
                                m_DbClick = true;
                                return;
                            }
                            switch (blockinfodic["块类型"].ToString())
                            {
                                case "PID设备":
                                    //Commands.ShowDesignPalette();
                                    //int deviceid=int.Parse(blockinfodic["ID"].ToString());
                                    //Device d = context.Devices.Where(o => o.DeviceID == deviceid).SingleOrDefault();
                                    //if (d!=null)
                                    //{
                                    //    DeviceForm editdevice = new DeviceForm(Commands.dp,d.Loopinfo,d);
                                    //    editdevice.Show();
                                    //}
                                    //m_DbClick = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else {
                            m_DbClick = true;
                        }
                    }
                    else {
                        m_DbClick = true;
                        return;
                    }
                    // tr.Commit();
                }
            }
            catch
            { }
        }

        //屏蔽CAD命令
        static void vetoCommand(object sender, DocumentLockModeChangedEventArgs e)
        {
            try
            {
                string com = e.GlobalCommandName.ToLower();
                if (m_DbClick)
                {
                    switch (com)
                    {
                        //case "properties":
                        //    m_DbClick = false;
                        //    e.Veto();
                        //    break;
                        case "bedit":
                            m_DbClick = false;
                            e.Veto();
                            break;
                        case "eattedit":
                            m_DbClick = false;
                            e.Veto();
                            break;
                        case "ddedit":
                            m_DbClick = false;
                            e.Veto();
                            break;
                    }
                }
                else {
                    switch (com)
                    {
                        case "copyclip":
                            if (ed.Document == null)
                            {
                                DoubleClickStart("CSCAD");
                                copyentity = null;
                            }
                            PromptSelectionResult res = ed.SelectImplied();
                            if (res.Status != PromptStatus.Error)
                            {
                                SelectionSet SS = res.Value;
                                if (SS.GetObjectIds().Length == 1)
                                {
                                    ObjectId oId = SS.GetObjectIds()[0];
                                    //copyentity = CommonTools.IsCSCADEntity(oId);
                                }
                            }
                            break;
                        case "pasteclip":
                            if (copyentity!=null)
                            {
                                switch (copyentity["块类型"])
                                {
                                    case "PID设备":
                                        int id = int.Parse(copyentity["ID"].ToString());
                                        //PasteDevice pastedevice = new PasteDevice(id);
                                        //pastedevice.Show();
                                        e.Veto();
                                        break;
                                    default:
                                        break;
                                }
                            }
                            break;
                    }
                }
            }
            catch
            { }
        }

    }
}