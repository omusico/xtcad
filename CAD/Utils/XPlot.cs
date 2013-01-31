using System;
using System.Collections.Generic;
using System.Text;
using AcadAppser = Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.PlottingServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using System.IO;
using Autodesk.AutoCAD.Geometry;
using System.Windows.Forms;
using System.Drawing.Printing;



namespace CAD
{
    public enum EmObjRel
    {
        err = 0,
        off = 1,
        near = 2,
        same = 3,
        inter = 4,
        AcontainB = 5,
        BcontainA = 6
    }

    public class XPlot
    {
        /// <summary>
        /// 得到打印驱动器名称列表
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static string[] GetPlotDeviceNames(AcadAppser.Document doc)
        {
            /*
            AcadApplication AcadApp = (AcadApplication)AcadAppser.Application.AcadApplication;
            AcadDocument Acaddoc = AcadApp.ActiveDocument;
             Layout Layout;
            Layout = Acaddoc.ModelSpace.Layout;
            Layout.RefreshPlotDeviceInfo();//刷新打印驱动信息

            string[] plotDevices = (string[])Layout.GetPlotDeviceNames();//获取打印驱动名称
          */
            string[] re = null;
            using (Transaction tr = doc.Database.TransactionManager.StartTransaction())
            {
                // 将打印当前布局
                BlockTableRecord btr = (BlockTableRecord)tr.GetObject(doc.Database.CurrentSpaceId, OpenMode.ForRead);
                Layout lo = (Layout)tr.GetObject(btr.LayoutId, OpenMode.ForRead);

            }
            return re;
        }

        /// <summary>
        /// 得到某打印设备的图纸名称列表
        /// </summary>
        /// <param name="strPrinterName"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetMediaNames(string strPrinterName)
        {
            Dictionary<string, string> re = new Dictionary<string, string>();
            try
            {

                //得到CAD图纸名称，作为值
                List<string> cadName = new List<string>();
                Autodesk.AutoCAD.PlottingServices.PlotConfigManager.SetCurrentConfig(strPrinterName);
                Autodesk.AutoCAD.PlottingServices.PlotConfig pcf = Autodesk.AutoCAD.PlottingServices.PlotConfigManager.CurrentConfig;
                //  DeviceType dt = (DeviceType)pcf.DeviceType;
                foreach (string s in pcf.CanonicalMediaNames)
                {
                    cadName.Add(s);
                }

                //得到系统图纸名称,作为键
                List<string> sysName = new List<string>();
                PageSetupDialog psd = new PageSetupDialog();
                psd.PageSettings = new System.Drawing.Printing.PageSettings();
                PrinterSettings ps = new PrinterSettings();
                ps.PrinterName = strPrinterName;
                psd.PrinterSettings = ps;
                foreach (PaperSize p in ps.PaperSizes)
                {
                    sysName.Add(p.PaperName);
                }
                sysName.Reverse();


                int n = sysName.Count;
                if (n == cadName.Count)
                {
                    for (int i = 0; i < n; i++)
                    {
                        if (!re.ContainsKey(sysName[i]))
                        {
                            re.Add(sysName[i], cadName[i]);
                        }
                    }
                }
                else
                {
                    foreach (string s in cadName)
                    {
                        if (!re.ContainsKey(s))
                        {
                            re.Add(s, s);
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                throw new System.Exception("->GetMediaNames:" + ex.Message);
            }
            return re;
        }

        /// <summary>
        /// 生成PLOT
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="pltFullFile"></param>
        /// <param name="printDevice"></param>
        /// <param name="printSize"></param>
        public static void getPLTByCommand(AcadAppser.Document doc, string pltFullFile, string printDevice, string printSize)
        {
            try
            {

                string t = pltFullFile.Replace("\\", "\\\\");
                string tt = "(command \"zoom\" \"e\") \b";
                doc.SendStringToExecute(tt, true, false, true);
                string ttt = string.Format("{0}{1}{2}{3}{4}{5}{6}", "(command \"plot\" \"y\" \"模型\" \"", printDevice, "\" \"", printSize, "\" \"m\" \"l\" \"n\" \"e\" \"f\" \"c\" \"y\" \".\" \"y\" \"a\" \"", t, "\" \"n\" \"y\") \b");
                // string.Format("{0}{1}{2}{3}{4}{5}{6}", "(command \"plot\" \"y\" \"模型\" \"", printDevice, "\" \"", printSize, "\" \"m\" \"l\" \"n\" \"e\" \"f\" \"c\" \"y\" \".\" \"y\" \"a\" \"y\" \"", t, "\" \"n\" \"y\") \b");
                doc.SendStringToExecute(ttt, true, false, true);
                string r = "(command \"close\" \"n\" ) \b";
                //  doc.SendStringToExecute(r, true, false, true);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("->getPLTByCommand:" + ex.Message);
            }
        }


        public static string[] getStdScaleTypeStr()
        {
            List<string> re = new List<string>();
            re.Add("布满图纸");
            re.Add("1000:1");
            re.Add("100:1");
            re.Add("10:1");
            re.Add("8:1");
            re.Add("4:1");
            re.Add("2:1");
            re.Add("1:1");
            re.Add("1:2");
            re.Add("1:4");
            re.Add("1:8");
            re.Add("1:10");
            re.Add("1:16");
            re.Add("1:20");
            re.Add("1:30");
            re.Add("1:40");
            re.Add("1:50");
            re.Add("1:100");
            return re.ToArray();
        }

        /// <summary>
        /// 根据打印比例名称字符串，得到打印比例类型枚举
        /// </summary>
        /// <param name="StdScaleTypeStr"></param>
        /// <returns></returns>
        public static StdScaleType getStdScaleTypeByStr(string StdScaleTypeStr)
        {
            StdScaleType re = StdScaleType.ScaleToFit;
            switch (StdScaleTypeStr)
            {
                case "布满图纸": re = StdScaleType.ScaleToFit; break;
                case "1000:1": re = StdScaleType.StdScale1000To1; break;
                case "100:1": re = StdScaleType.StdScale100To1; break;
                case "10:1": re = StdScaleType.StdScale10To1; break;
                case "8:1": re = StdScaleType.StdScale8To1; break;
                case "4:1": re = StdScaleType.StdScale4To1; break;
                case "2:1": re = StdScaleType.StdScale2To1; break;
                case "1:1": re = StdScaleType.StdScale1To1; break;
                case "1:2": re = StdScaleType.StdScale1To2; break;
                case "1:4": re = StdScaleType.StdScale1To4; break;
                case "1:8": re = StdScaleType.StdScale1To8; break;
                case "1:10": re = StdScaleType.StdScale1To10; break;
                case "1:16": re = StdScaleType.StdScale1To16; break;
                case "1:20": re = StdScaleType.StdScale1To20; break;
                case "1:30": re = StdScaleType.StdScale1To30; break;
                case "1:40": re = StdScaleType.StdScale1To40; break;
                case "1:50": re = StdScaleType.StdScale1To50; break;
                case "1:100": re = StdScaleType.StdScale1To100; break;
                default: break;
            }
            return re;
        }

        /// <summary>
        /// 将当前空间生成PLOT,居中打印
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="pltFullFile"></param>
        /// <param name="printDeviceName"></param>
        /// <param name="mediaName"></param>
        /// <param name="scaleType"></param>
        public static void getPLTByCode(AcadAppser.Document doc, string pltFullFile, string printDeviceName, string mediaName, StdScaleType scaleType)
        {
            object backgroundplot = AcadAppser.Application.GetSystemVariable("BACKGROUNDPLOT");
            AcadAppser.Application.SetSystemVariable("BACKGROUNDPLOT", 0);
            try
            {

                using (AcadAppser.DocumentLock docLock = doc.LockDocument()) //在非模态下打开模型空间前要解锁  
                {
                    Editor ed = doc.Editor;
                    Database db = doc.Database;
                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        // 将打印当前布局
                        BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForRead);
                        //Point3d  max = btr.Database.Extmax;
                        //Point3d min = btr.Database.Extmin;
                        //double diffX = max.X - min.X;
                        //double diffY = max.Y - min.Y;                
                        Layout lo = (Layout)tr.GetObject(btr.LayoutId, OpenMode.ForRead);

                        ////获取当前布局管理器变量
                        //LayoutManager layoutMgr = LayoutManager.Current;

                        ////获取当前布局变量
                        //Layout layout = trans.GetObject(layoutMgr.GetLayoutId(layoutMgr.CurrentLayout), OpenMode.ForRead) as Layout;

                        ////获取当前布局的打印信息
                        //PlotInfo plInfo = new PlotInfo();
                        //plInfo.Layout = layout.ObjectId;


                        // 需要一个与布局有关的PlotInfo对象
                        PlotInfo pi = new PlotInfo();
                        pi.Layout = btr.LayoutId;
                        // 需要一个基于布局设置的PlotSettings对象，这样我们就可以进行自定义设置
                        PlotSettings ps = new PlotSettings(lo.ModelType);
                        ps.CopyFrom(lo);

                        /*
                        //着色打印选项，设置按线框进行打印---
                        ps.ShadePlot = PlotSettingsShadePlotType.Wireframe;

                        //设置打印比例--
                        if (rb_FitToPlot.Checked)
                        {
                            psVdr.SetUseStandardScale(plSet, true);
                            psVdr.SetStdScaleType(plSet, StdScaleType.ScaleToFit);
                        }
                        else if (rb_CustomScale.Checked)
                        {
                            psVdr.SetUseStandardScale(plSet, false);
                            CustomScale cScale = new CustomScale(System.Convert.ToDouble(txt_ScaleX.Text.Trim()), System.Convert.ToDouble(txt_ScaleY.Text.Trim()));
                            psVdr.SetCustomPrintScale(plSet, cScale);
                        }

                        //设置是否居中打印--
                        if (ckb_CenterPlot.Checked)
                        {
                            psVdr.SetPlotCentered(plSet, ckb_CenterPlot.Checked);
                        }

                        //设置打印样式表--
                        psVdr.SetCurrentStyleSheet(plSet, cbx_PlotStyle.SelectedItem.ToString());

                        //设置打印设备和纸张--
                        psVdr.SetPlotConfigurationName(plSet, cbx_Ploter.SelectedItem.ToString(), cbx_Media.SelectedItem.ToString());

                        //设置打印区域--
                        psVdr.SetPlotWindowArea(plSet, tukuangToPlot);
                        psVdr.SetPlotType(plSet, Autodesk.AutoCAD.DatabaseServices.PlotType.Window);
                        //设置竖向打印--
                        psVdr.SetPlotRotation(plSet, PlotRotation.Degrees000);//横向打印PlotRotation.Degrees090
                        //重载和保存打印信息--
                        plInfo.OverrideSettings = plSet;
                        */

                        // PlotSettingsValidator对象帮助我们创建一个有效的PlotSettings对象    
                        PlotSettingsValidator psv = PlotSettingsValidator.Current;

                        //进行范围打印、中心打印和按比例打印         
                        psv.SetPlotType(ps, Autodesk.AutoCAD.DatabaseServices.PlotType.Extents);
                        psv.SetUseStandardScale(ps, true);
                        psv.SetStdScaleType(ps, scaleType);
                        psv.SetPlotCentered(ps, true);
                        psv.SetCurrentStyleSheet(ps, "");

                        //使用标准的DWF PC3，这里我们只是打印到文件        

                        psv.SetPlotConfigurationName(ps, printDeviceName, mediaName);// "DWF6 ePlot.pc3" "ANSI_A_(8.50_x_11.00_Inches)" /ISO_A0_(841.00_x_1189.00_MM)

                        //  psv.SetCurrentStyleSheet(ps, "acad.ctb");
                        // 我们需要把PlotInfo链接到PlotSettings并验证它的有效性
                        pi.OverrideSettings = ps;

                        PlotInfoValidator piv = new PlotInfoValidator();

                        piv.MediaMatchingPolicy = MatchingPolicy.MatchEnabled;

                        piv.Validate(pi);

                        // PlotEngine对象执行真正的打印工作         

                        // (也可以创建一个用来预览)        

                        if (PlotFactory.ProcessPlotState == ProcessPlotState.NotPlotting)
                        {

                            PlotEngine pe = PlotFactory.CreatePublishEngine();
                            //   PlotFactory.CreatePreviewEngine(PreviewEngineFlags.Plot);
                            using (pe)
                            {

                                //创建一个打印对话框，用于提供打印信息和允许用户取消打印            

                                PlotProgressDialog ppd = new PlotProgressDialog(false, 1, true);
                                //PlotProgressDialog plProgDlg = new PlotProgressDialog(ispreview, 1, true);

                                using (ppd)
                                {

                                    ppd.set_PlotMsgString(PlotMessageIndex.DialogTitle, "预览打印进度:" + doc.Name);
                                    ppd.set_PlotMsgString(PlotMessageIndex.CancelJobButtonMessage, "取消任务");
                                    ppd.set_PlotMsgString(PlotMessageIndex.CancelSheetButtonMessage, "取消本页任务");
                                    ppd.set_PlotMsgString(PlotMessageIndex.SheetSetProgressCaption, "所有页面任务进度");
                                    ppd.set_PlotMsgString(PlotMessageIndex.SheetProgressCaption, "本页任务进度");
                                    //设置打印进度范围
                                    ppd.LowerPlotProgressRange = 0;
                                    ppd.UpperPlotProgressRange = 100;
                                    ppd.PlotProgressPos = 0;
                                    //显示进度对话框            
                                    ppd.OnBeginPlot();
                                    ppd.IsVisible = true;
                                    //开始打印布局
                                    pe.BeginPlot(ppd, null);
                                    //定义打印输出            
                                    pe.BeginDocument(pi, doc.Name, null, 1, true, pltFullFile);//"c:\\test-output"
                                    // plEng.BeginDocument(plInfo, doc.Name, null, System.Convert.ToInt16(nUD_Copies.Value), plotToFile, savePath + Path.GetFileNameWithoutExtension(doc.Name) + "-" + System.Convert.ToString(i + 1));


                                    ppd.OnBeginSheet();
                                    //设置图纸集进度范围 
                                    ppd.LowerSheetProgressRange = 0;
                                    ppd.UpperSheetProgressRange = 100;
                                    ppd.SheetProgressPos = 0;
                                    //打印图纸或布局
                                    PlotPageInfo ppi = new PlotPageInfo();
                                    pe.BeginPage(ppi, pi, true, null);
                                    //开始打印
                                    pe.BeginGenerateGraphics(null);
                                    pe.EndGenerateGraphics(null);
                                    //结束打印图纸集或布局            
                                    pe.EndPage(null);
                                    ppd.SheetProgressPos = 100;
                                    ppd.OnEndSheet();
                                    //结束打印文档            
                                    pe.EndDocument(null);
                                    // 结束打印               
                                    ppd.PlotProgressPos = 100;
                                    ppd.OnEndPlot();
                                    pe.EndPlot(null);
                                }
                            }
                        }
                        else
                        {
                            ed.WriteMessage("\nAnother plot is in progress.");
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("->getPLTByCode:" + ex.Message);
            }
            finally
            {
                AcadAppser.Application.SetSystemVariable("BACKGROUNDPLOT", backgroundplot);
                //销毁打印引擎
                //plEng.Destroy();
                //plEng.Dispose();
            }
        }

        //        打印样式 
        //打印样式通过确定打印特性（例如线宽、颜色和填充样式）来控制对象或布局的打印方式。 打印样式表中收集了多组打印样式。 打印样式管理器是一个窗口，其中显示了所有可用的打印样式表。 

        //打印样式有两种类型：颜色相关和命名。 一个图形只能使用一种类型的打印样式表。 用户可以在两种打印样式表之间转换。 也可以在设置了图形的打印样式表类型之后，修改所设置的类型。 

        //对于颜色相关打印样式表，对象的颜色确定如何对其进行打印。 这些打印样式表文件的扩展名为 .ctb。 不能直接为对象指定颜色相关打印样式。 相反，要控制对象的打印颜色，必须修改对象的颜色。 例如，图形中所有被指定为红色的对象均以相同的方式打印。 

        //命名打印样式表使用直接指定给对象和图层的打印样式。 这些打印样式表文件的扩展名为 .stb。 使用这些打印样式表可以使图形中的每个对象以不同颜色打印，与对象本身的颜色无关。


        public static bool getPLTByCode(PlotEngine plEng, bool IsPreview, AcadAppser.Document doc, string printDeviceName, string mediaName, bool ScaleToFit, CustomScale cScale, bool PlotCentered, Point2d OffsetPt2d, List<Extents2d> PlotLimit, bool plotToFile, string savePath, short plotCount)
        {

            Database db = doc.Database;

            //PreviewEndPlotStatus ret = PreviewEndPlotStatus.Normal;
            bool stop = false;
            object backgroundplot = AcadAppser.Application.GetSystemVariable("BACKGROUNDPLOT");

            try
            {
                AcadAppser.Application.SetSystemVariable("BACKGROUNDPLOT", 0);
                using (AcadAppser.DocumentLock docLock = doc.LockDocument()) //在非模态下打开模型空间前要解锁  
                {


                    using (Transaction trans = db.TransactionManager.StartTransaction())
                    {
                        // 将打印当前布局
                        BlockTableRecord btr = (BlockTableRecord)trans.GetObject(db.CurrentSpaceId, OpenMode.ForRead);
                        //获取当前布局变量
                        // Layout layout = trans.GetObject(layoutMgr.GetLayoutId(layoutMgr.CurrentLayout), OpenMode.ForRead) as Layout;
                        Layout layout = trans.GetObject(btr.LayoutId, OpenMode.ForRead) as Layout;


                        //获取当前布局的打印信息
                        PlotInfo plInfo = new PlotInfo();
                        plInfo.Layout = layout.ObjectId;

                        //复制当前打印配置
                        PlotSettings plSet = new PlotSettings(layout.ModelType);
                        plSet.CopyFrom(layout);

                        //着色打印选项，设置按线框进行打印--
                        plSet.ShadePlot = PlotSettingsShadePlotType.Wireframe;

                        PlotSettingsValidator psVdr = PlotSettingsValidator.Current;
                        //设置打印比例
                        if (ScaleToFit)
                        {
                            psVdr.SetUseStandardScale(plSet, true);
                            psVdr.SetStdScaleType(plSet, StdScaleType.ScaleToFit);
                        }
                        else
                        {
                            psVdr.SetUseStandardScale(plSet, false);
                            psVdr.SetCustomPrintScale(plSet, cScale);
                        }

                        //设置是否居中打印
                        if (PlotCentered)
                        {
                            psVdr.SetPlotCentered(plSet, true);
                        }
                        else
                        {
                            psVdr.SetPlotCentered(plSet, false);
                            psVdr.SetPlotOrigin(plSet, OffsetPt2d);
                        }

                        //设置打印样式表
                        //   psVdr.SetCurrentStyleSheet(plSet, strPlotStyleFile);

                        //如果没有制定打印区域，则获取图形范围，自动设置
                        if (PlotLimit == null || PlotLimit.Count == 0)
                        {
                            psVdr.SetPlotType(plSet, Autodesk.AutoCAD.DatabaseServices.PlotType.Extents);
                            Extents2d tempex2d = new Extents2d(btr.Database.Extmin.X, btr.Database.Extmin.Y, btr.Database.Extmax.X, btr.Database.Extmax.Y);
                            btr.Dispose();

                            PlotLimit = new List<Extents2d>();
                            PlotLimit.Add(tempex2d);
                        }
                        int i = 0;
                        string filename = "";
                        foreach (Extents2d ex2d in PlotLimit)
                        {
                            i++;
                            psVdr.SetPlotWindowArea(plSet, ex2d);
                            psVdr.SetPlotType(plSet, Autodesk.AutoCAD.DatabaseServices.PlotType.Window);

                            //设置打印方向
                            if (ex2d.MaxPoint.X - ex2d.MinPoint.X >= ex2d.MaxPoint.Y - ex2d.MinPoint.Y)
                            {
                                psVdr.SetPlotRotation(plSet, PlotRotation.Degrees090); //设置横向
                            }
                            else
                            {
                                psVdr.SetPlotRotation(plSet, PlotRotation.Degrees000); //设置竖向
                            }
                            psVdr.SetPlotConfigurationName(plSet, printDeviceName, mediaName);


                            //重载和保存打印信息
                            plInfo.OverrideSettings = plSet;

                            //验证打印信息设置，看是否有误
                            PlotInfoValidator plInfoVdr = new PlotInfoValidator();
                            plInfoVdr.MediaMatchingPolicy = MatchingPolicy.MatchEnabled;
                            plInfoVdr.Validate(plInfo);

                            PlotProgressDialog plProgDlg = new PlotProgressDialog(IsPreview, 1, true);

                            using (plProgDlg)
                            {
                                plProgDlg.set_PlotMsgString(PlotMessageIndex.DialogTitle, "打印进度:" + doc.Name);
                                plProgDlg.set_PlotMsgString(PlotMessageIndex.CancelJobButtonMessage, "取消任务");
                                plProgDlg.set_PlotMsgString(PlotMessageIndex.CancelSheetButtonMessage, "取消本页任务");
                                plProgDlg.set_PlotMsgString(PlotMessageIndex.SheetSetProgressCaption, "所有页面任务进度");
                                plProgDlg.set_PlotMsgString(PlotMessageIndex.SheetProgressCaption, "本页任务进度");

                                //设置打印进度范围
                                plProgDlg.LowerPlotProgressRange = 0;
                                plProgDlg.UpperPlotProgressRange = 100;
                                plProgDlg.PlotProgressPos = 0;

                                //显示进度对话框
                                plProgDlg.OnBeginPlot();
                                plProgDlg.IsVisible = true;

                                //开始打印布局
                                plEng.BeginPlot(plProgDlg, null);

                                //定义打印输出
                                filename = Path.Combine(savePath, Path.GetFileNameWithoutExtension(doc.Name) + "-" + System.Convert.ToString(i) + ".plt");
                                plEng.BeginDocument(plInfo, doc.Name, null, plotCount, plotToFile, filename);

                                plProgDlg.OnBeginSheet();

                                //设置图纸集进度范围 
                                plProgDlg.LowerSheetProgressRange = 0;
                                plProgDlg.UpperSheetProgressRange = 100;
                                plProgDlg.SheetProgressPos = 0;

                                //打印图纸或布局
                                PlotPageInfo plPageInfo = new PlotPageInfo();
                                plEng.BeginPage(plPageInfo, plInfo, true, null);

                                //开始打印
                                plEng.BeginGenerateGraphics(null);
                                plEng.EndGenerateGraphics(null);

                                //结束打印图纸集或布局
                                plEng.EndPage(null);

                                plProgDlg.SheetProgressPos = 100;
                                plProgDlg.OnEndSheet();

                                //结束打印文档
                                plEng.EndDocument(null);

                                //结束打印
                                plProgDlg.PlotProgressPos = 100;
                                plProgDlg.OnEndPlot();
                                plEng.EndPlot(null);
                            }

                        }

                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("getPLTByCode->" + ex.Message);
            }
            finally
            {
                AcadAppser.Application.SetSystemVariable("BACKGROUNDPLOT", backgroundplot);

                //销毁打印引擎
                plEng.Destroy();
                plEng.Dispose();
            }

            return stop;
        }

        //根据范围得到打印尺寸--未用
        private static string GetMediaByExtend(string printDeviceName, Extents2d ex2d)
        {
            string re = "";
            //得到CAD图纸名称，作为值
            List<string> cadName = new List<string>();
            Autodesk.AutoCAD.PlottingServices.PlotConfigManager.SetCurrentConfig(printDeviceName);
            Autodesk.AutoCAD.PlottingServices.PlotConfig pcf = Autodesk.AutoCAD.PlottingServices.PlotConfigManager.CurrentConfig;

            foreach (string s in pcf.CanonicalMediaNames)
            {
                cadName.Add(s);
            }
            return re;
        }

        //得到图框范围,必须是正向的矩形,自动找范围
        public static List<Extents2d> getPrintLimit(AcadAppser.Document doc, string strTKLayerName, double douValue)
        {
            List<Extents2d> re = null;

            TypedValue value1 = new TypedValue((int)DxfCode.LayerName, strTKLayerName);
            TypedValue value2 = new TypedValue(0, "*PolyLine");
            SelectionFilter sfilter = new SelectionFilter(new TypedValue[] { value1, value2 });

            using (AcadAppser.DocumentLock m_doclock = doc.LockDocument()) //在非模态下打开模型空间前要解锁  
            {
                using (Transaction trans = doc.Database.TransactionManager.StartTransaction())
                {
                    PromptSelectionResult res = doc.Editor.SelectAll();//sfilter
                    if (res != null && res.Status == PromptStatus.OK && res.Value != null && res.Value.Count > 0)
                    {
                        ObjectId[] Oid = res.Value.GetObjectIds();
                        double GeomExtentsArea;
                        Extents2d tempEx2d;
                        Polyline tempPL;
                        EmObjRel tempEmObjRel;
                        List<Extents2d> needDelExt2ds = new List<Extents2d>();
                        List<Extents2d> needAddExt2ds = new List<Extents2d>();
                        foreach (ObjectId id in Oid)
                        {
                            tempPL = null;
                            tempPL = trans.GetObject(id, OpenMode.ForRead) as Polyline;
                            if (tempPL != null)
                            {
                                if (tempPL.Closed && tempPL.NumberOfVertices == 4)
                                {
                                    GeomExtentsArea = (tempPL.GeometricExtents.MaxPoint.X - tempPL.GeometricExtents.MinPoint.X) * (tempPL.GeometricExtents.MaxPoint.Y - tempPL.GeometricExtents.MinPoint.Y);
                                    if (Math.Abs(tempPL.Area - GeomExtentsArea) < douValue) //如果闭合矩形，且面积=几何范围的面积，则满足条件
                                    {
                                        tempEx2d = new Extents2d(tempPL.GeometricExtents.MinPoint.X, tempPL.GeometricExtents.MinPoint.Y, tempPL.GeometricExtents.MaxPoint.X, tempPL.GeometricExtents.MaxPoint.Y);
                                        if (re == null)
                                        {
                                            re = new List<Extents2d>();
                                            re.Add(tempEx2d);
                                        }
                                        else
                                        {
                                            needDelExt2ds.Clear();
                                            needAddExt2ds.Clear();
                                            //与已经取得的所有范围比较
                                            foreach (Extents2d ex2d in re)
                                            {
                                                tempEmObjRel = judgeTwoExt2dTopology(tempEx2d, ex2d);
                                                if (tempEmObjRel == EmObjRel.AcontainB)
                                                {
                                                    needDelExt2ds.Add(ex2d);
                                                    //  re.Remove(ex2d);
                                                    if (!re.Contains(tempEx2d))
                                                    {
                                                        needAddExt2ds.Add(tempEx2d);
                                                        //re.Add(tempEx2d);
                                                    }
                                                }
                                                else if (tempEmObjRel == EmObjRel.near || tempEmObjRel == EmObjRel.off)
                                                {
                                                    if (!re.Contains(tempEx2d))
                                                    {
                                                        needAddExt2ds.Add(tempEx2d);
                                                        // re.Add(tempEx2d);
                                                    }
                                                }
                                                else if (tempEmObjRel == EmObjRel.inter)
                                                {
                                                    needDelExt2ds.Add(ex2d);
                                                    // re.Remove(ex2d);
                                                }
                                            }

                                            foreach (Extents2d ext2d in needAddExt2ds)
                                            {
                                                re.Add(ext2d);
                                            }
                                            foreach (Extents2d ext2d in needDelExt2ds)
                                            {
                                                re.Remove(ext2d);
                                            }

                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
            return re;
        }

        //计算两个几何范围的关系ok
        private static EmObjRel judgeTwoExt2dTopology(Extents2d Ext2dA, Extents2d Ext2dB)
        {
            EmObjRel re = EmObjRel.err;
            if (Ext2dA.MinPoint == Ext2dB.MinPoint && Ext2dA.MaxPoint == Ext2dB.MaxPoint) //相等
            {
                re = EmObjRel.same;
            }
            else if (Ext2dA.MinPoint.X >= Ext2dB.MinPoint.X && Ext2dA.MinPoint.Y >= Ext2dB.MinPoint.Y && Ext2dA.MaxPoint.X <= Ext2dB.MaxPoint.X && Ext2dA.MaxPoint.X <= Ext2dB.MaxPoint.X) //B包含A
            {
                re = EmObjRel.BcontainA;
            }
            else if (Ext2dA.MinPoint.X <= Ext2dB.MinPoint.X && Ext2dA.MinPoint.Y <= Ext2dB.MinPoint.Y && Ext2dA.MaxPoint.X >= Ext2dB.MaxPoint.X && Ext2dA.MaxPoint.X >= Ext2dB.MaxPoint.X)//A包含B
            {
                re = EmObjRel.AcontainB;
            }
            else
            {
                Point2d ptAlefttop = new Point2d(Ext2dA.MinPoint.X, Ext2dA.MaxPoint.Y);
                Point2d ptAbuttomright = new Point2d(Ext2dA.MaxPoint.X, Ext2dA.MinPoint.Y);
                Polyline plA = new Polyline(4);
                // plA.Normal = normal;
                plA.AddVertexAt(0, Ext2dA.MinPoint, 0, 0, 0);
                plA.AddVertexAt(0, ptAlefttop, 0, 0, 0);
                plA.AddVertexAt(0, Ext2dA.MaxPoint, 0, 0, 0);
                plA.AddVertexAt(0, ptAbuttomright, 0, 0, 0);
                plA.Closed = true;


                Point2d ptBlefttop = new Point2d(Ext2dB.MinPoint.X, Ext2dB.MaxPoint.Y);
                Point2d ptBbuttomright = new Point2d(Ext2dB.MaxPoint.X, Ext2dB.MinPoint.Y);
                Polyline plB = new Polyline(4);
                //  plB.Normal = normal;
                plB.AddVertexAt(0, Ext2dB.MinPoint, 0, 0, 0);
                plB.AddVertexAt(0, ptBlefttop, 0, 0, 0);
                plB.AddVertexAt(0, Ext2dB.MaxPoint, 0, 0, 0);
                plB.AddVertexAt(0, ptBbuttomright, 0, 0, 0);
                plB.Closed = true;

                Point3dCollection pt3ds = new Point3dCollection();
                plA.IntersectWith(plB, Intersect.OnBothOperands, pt3ds, 0, 0);
                if (pt3ds.Count == 0)
                {
                    re = EmObjRel.off;
                }
                else if (pt3ds.Count == 1)
                {
                    re = EmObjRel.near;
                }
                else if (pt3ds.Count == 2)//相接或相交
                {
                    List<Point2d> listA = new List<Point2d>();
                    listA.Add(Ext2dA.MinPoint);
                    listA.Add(ptAlefttop);
                    listA.Add(Ext2dA.MaxPoint);
                    listA.Add(ptAbuttomright);

                    List<Point2d> listB = new List<Point2d>();
                    listB.Add(Ext2dB.MinPoint);
                    listB.Add(ptBlefttop);
                    listB.Add(Ext2dB.MaxPoint);
                    listB.Add(ptBbuttomright);

                    Point2d pt1 = new Point2d(pt3ds[0].X, pt3ds[0].Y);
                    Point2d pt2 = new Point2d(pt3ds[1].X, pt3ds[1].Y);
                    if (listA.Contains(pt1) && listA.Contains(pt2))
                    {
                        re = EmObjRel.near;
                    }
                    else if (listB.Contains(pt1) && listB.Contains(pt2))
                    {
                        re = EmObjRel.near;
                    }
                    else
                    {
                        re = EmObjRel.inter;
                    }
                }
                else
                {
                    re = EmObjRel.inter;
                }
            }

            return re;
        }

        /// <summary>
        /// 得到plt打印的预览
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="pltFullFile"></param>
        /// <param name="printDeviceName"></param>
        /// <param name="mediaName"></param>
        /// <param name="scaleType"></param>
        public static void getPLTPreview(AcadAppser.Document doc, string pltFullFile, string printDeviceName, string mediaName, StdScaleType scaleType)
        {
            try
            {
                Editor ed = doc.Editor;
                Database db = doc.Database;
                // PlotEngines do the previewing and plotting
                if (PlotFactory.ProcessPlotState == ProcessPlotState.NotPlotting)
                {

                    // First we preview...
                    PreviewEndPlotStatus stat;
                    PlotEngine pre = PlotFactory.CreatePreviewEngine((int)PreviewEngineFlags.Plot);
                    using (pre)
                    {
                        stat = PlotOrPreview(doc, pre, true, db.CurrentSpaceId, "", printDeviceName, mediaName, scaleType);
                    }

                    if (stat == PreviewEndPlotStatus.Plot)
                    {
                        // And if the user asks, we plot...
                        PlotEngine ple = PlotFactory.CreatePublishEngine();
                        if (File.Exists(pltFullFile)) File.Delete(pltFullFile);
                        stat = PlotOrPreview(doc, ple, false, db.CurrentSpaceId, pltFullFile, printDeviceName, mediaName, scaleType);
                    }
                }

                else
                {
                    ed.WriteMessage("\nAnother plot is in progress.");

                }

            }
            catch (System.Exception ex)
            {
                throw new System.Exception("->getPLTPreview:" + ex.Message);
            }
        }



        static PreviewEndPlotStatus PlotOrPreview(AcadAppser.Document doc, PlotEngine pe, bool isPreview, ObjectId spaceId, string filename, string printDeviceName, string mediaName, StdScaleType scaleType)
        {
            try
            {

                PreviewEndPlotStatus ret = PreviewEndPlotStatus.Cancel;
                using (AcadAppser.DocumentLock docLock = doc.LockDocument()) //在非模态下打开模型空间前要解锁  
                {
                    Editor ed = doc.Editor;
                    Database db = doc.Database;
                    Transaction tr = db.TransactionManager.StartTransaction();
                    using (tr)
                    {

                        // 将打印当前布局
                        BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForRead);
                        Layout lo = (Layout)tr.GetObject(btr.LayoutId, OpenMode.ForRead);
                        // 需要一个与布局有关的PlotInfo对象
                        PlotInfo pi = new PlotInfo();
                        pi.Layout = btr.LayoutId;
                        // 需要一个基于布局设置的PlotSettings对象，这样我们就可以进行自定义设置
                        PlotSettings ps = new PlotSettings(lo.ModelType);
                        ps.CopyFrom(lo);

                        // PlotSettingsValidator对象帮助我们创建一个有效的PlotSettings对象
                        PlotSettingsValidator psv = PlotSettingsValidator.Current;

                        //进行范围打印、中心打印和按比例打印         
                        psv.SetPlotType(ps, Autodesk.AutoCAD.DatabaseServices.PlotType.Extents);
                        psv.SetUseStandardScale(ps, true);
                        psv.SetStdScaleType(ps, scaleType);
                        psv.SetPlotCentered(ps, true);

                        //使用标准的DWF PC3，这里我们只是打印到文件
                        psv.SetPlotConfigurationName(ps, printDeviceName, mediaName);// "DWF6 ePlot.pc3" "ANSI_A_(8.50_x_11.00_Inches)" /ISO_A0_(841.00_x_1189.00_MM)
                        // 我们需要把PlotInfo链接到PlotSettings并验证它的有效性        

                        pi.OverrideSettings = ps;
                        PlotInfoValidator piv = new PlotInfoValidator();
                        piv.MediaMatchingPolicy = MatchingPolicy.MatchEnabled;
                        piv.Validate(pi);

                        //创建一个打印对话框，用于提供打印信息和允许用户取消打印
                        PlotProgressDialog ppd = new PlotProgressDialog(isPreview, 1, true);

                        using (ppd)
                        {
                            string s = doc.Name.Substring(doc.Name.LastIndexOf("\\") + 1);
                            ppd.set_PlotMsgString(PlotMessageIndex.DialogTitle, "Custom Plot Progress");
                            ppd.set_PlotMsgString(PlotMessageIndex.SheetName, s);
                            ppd.set_PlotMsgString(PlotMessageIndex.CancelJobButtonMessage, "Cancel Job");
                            ppd.set_PlotMsgString(PlotMessageIndex.CancelSheetButtonMessage, "Cancel Sheet");
                            ppd.set_PlotMsgString(PlotMessageIndex.SheetSetProgressCaption, "Sheet Set Progress");
                            ppd.set_PlotMsgString(PlotMessageIndex.SheetProgressCaption, "Sheet Progress");

                            ppd.LowerPlotProgressRange = 0;
                            ppd.UpperPlotProgressRange = 100;
                            ppd.PlotProgressPos = 0;

                            // Let's start the plot/preview, at last

                            ppd.OnBeginPlot();
                            ppd.IsVisible = true;
                            pe.BeginPlot(ppd, null);

                            // We'll be plotting/previewing
                            // a single document

                            pe.BeginDocument(pi, doc.Name, null, 1, !isPreview, filename);

                            // Which contains a single sheet

                            ppd.OnBeginSheet();
                            ppd.LowerSheetProgressRange = 0;
                            ppd.UpperSheetProgressRange = 100;
                            ppd.SheetProgressPos = 0;
                            PlotPageInfo ppi = new PlotPageInfo();
                            pe.BeginPage(ppi, pi, true, null);
                            pe.BeginGenerateGraphics(null);
                            ppd.SheetProgressPos = 50;
                            pe.EndGenerateGraphics(null);

                            // Finish the sheet
                            PreviewEndPlotInfo pepi = new PreviewEndPlotInfo();
                            pe.EndPage(pepi);
                            ret = pepi.Status;
                            ppd.SheetProgressPos = 100;
                            ppd.OnEndSheet();

                            // Finish the document
                            pe.EndDocument(null);

                            // And finish the plot
                            ppd.PlotProgressPos = 100;
                            ppd.OnEndPlot();
                            pe.EndPlot(null);

                        }

                        // Committing is cheaper than aborting
                        tr.Commit();

                    }
                }

                return ret;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("->getPLTPreview:" + ex.Message);
            }
        }
    }
}

