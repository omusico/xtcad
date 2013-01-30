using System;
using System.Collections.Generic;
using System.Text;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.ApplicationServices;
using System.Windows.Forms;
using Autodesk.AutoCAD.PlottingServices;
using System.Collections.Specialized;

namespace CAD
{
    public class PlotUtil
    {

        /// <summary>
        ///首尾相连的线段连接成多段线
        /// V1.0 by WeltionChen @2011.02.17
        /// 实现原理：
        /// 1.选择图面上所有直线段
        /// 2.选取选集第一条直线作为起始线段，向线段的两个方向搜索与之相连的直线段
        /// 3.搜索方式采用Editor的SelectCrossingWindow方法通过线段的端点创建选集
        /// 正常情况下会选到1到2个线段（本程序暂不处理3个线段相交的情况），剔除本身，得到与之相连的直线段
        /// 4.处理过的直线段将不再作为起始线段，由集合中剔除
        /// 4.通过递归循环依次搜索，直到末端。
        /// 5.删除原线段，根据创建多段线
        /// 6.循环处理所有的线段
        /// </summary>
        [CommandMethod("tt5")]
        public void JionLinesToPline()
        {
            //选择图面上所有直线段
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SelectionFilter sf = new SelectionFilter(new TypedValue[] { new TypedValue(0, "Line") });
            PromptSelectionResult selectLinesResult = ed.SelectAll(sf);
            if (selectLinesResult.Status != PromptStatus.OK)
                return;
            //需要处理的直线段集合
            List<ObjectId> lineObjectIds = new List<ObjectId>(selectLinesResult.Value.GetObjectIds());
            using (Transaction tr = HostApplicationServices.WorkingDatabase.TransactionManager.StartTransaction())
            {
                Database db = HostApplicationServices.WorkingDatabase;
                BlockTableRecord currentSpace = tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;
                while (true)
                {
                    //选取选集第一条直线作为起始线段
                    ObjectId currentLineId = lineObjectIds[0];
                    //处理过的直线段将不再作为起始线段，由集合中剔除
                    lineObjectIds.RemoveAt(0);
                    Line currentLine = tr.GetObject(currentLineId, OpenMode.ForWrite) as Line;
                    //多段线的顶点集合，由各段相连的直线段的端点组成，初始值为起始线段的端点
                    List<Point3d> plinePoints = new List<Point3d> { currentLine.StartPoint, currentLine.EndPoint };
                    //每个直线段有两个方向，由起点向终点方向搜索
                    JionLinesToPline(ref lineObjectIds, tr, ref plinePoints, currentLineId, currentLineId);
                    //翻转点集
                    plinePoints.Reverse();
                    //由终点向起点方向搜索
                    JionLinesToPline(ref lineObjectIds, tr, ref plinePoints, currentLineId, currentLineId);
                    //本程序为将相连的直线段转成多段线，所以对孤立的直线段不做处理
                    if (plinePoints.Count > 2)
                    {
                        //创建多段线
                        Autodesk.AutoCAD.DatabaseServices.Polyline resultPline = new Autodesk.AutoCAD.DatabaseServices.Polyline();
                        for (int i = 0; i < plinePoints.Count - 1; i++)
                        {
                            resultPline.AddVertexAt(i, new Point2d(plinePoints[i].X, plinePoints[i].Y), 0, 0, 0);
                        }
                        if (plinePoints[0] == plinePoints[plinePoints.Count - 1])
                        {
                            resultPline.Closed = true;
                        }
                        else
                        {
                            resultPline.AddVertexAt(plinePoints.Count - 1, new Point2d(plinePoints[plinePoints.Count - 1].X, plinePoints[plinePoints.Count - 1].Y), 0, 0, 0);
                        }
                        resultPline.Layer = currentLine.Layer;
                        resultPline.Linetype = currentLine.Linetype;
                        resultPline.LinetypeScale = currentLine.LinetypeScale;
                        currentSpace.AppendEntity(resultPline);
                        tr.AddNewlyCreatedDBObject(resultPline, true);
                        //删除起始直线段
                        currentLine.Erase();
                    }
                    //处理完毕，跳出循环
                    if (lineObjectIds.Count == 0)
                        break;
                }
                tr.Commit();
            }
        }
        /// <summary>
        /// 线段连接成多段线递归循环部分
        /// V1.0 by WeltionChen @2011.02.17
        /// </summary>
        /// <param name="lineObjectIds">线段的objectid集合</param>
        /// <param name="tr">transaction</param>
        /// <param name="plinePoints">多段线顶点坐标，也是各线段的端点坐标集合</param>
        /// <param name="currentLineId">当前线段的objectid</param>
        void JionLinesToPline(ref List<ObjectId> lineObjectIds, Transaction tr, ref List<Point3d> plinePoints, ObjectId currentLineId, ObjectId startLineId)
        {
            //提取端点
            Point3d lastPoint = plinePoints[plinePoints.Count - 1];
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SelectionFilter sf = new SelectionFilter(new TypedValue[] { new TypedValue(0, "Line") });
            //通过点创建选集
            PromptSelectionResult selectLinesResult = ed.SelectCrossingWindow(lastPoint, lastPoint, sf);
            if (selectLinesResult.Status == PromptStatus.OK)
            {
                List<ObjectId> selectedLinesId = new List<ObjectId>(selectLinesResult.Value.GetObjectIds());
                //剔除本身
                selectedLinesId.Remove(currentLineId);
                //处理相连的直线段
                if (selectedLinesId.Count == 1)
                {
                    ObjectId selectedLineId = selectedLinesId[0];
                    //处理过的直线段将不再作为起始线段，由集合中剔除
                    if (selectedLineId != startLineId)
                    {
                        lineObjectIds.Remove(selectedLineId);
                        Line selectedLine = tr.GetObject(selectedLineId, OpenMode.ForWrite) as Line;
                        //添加顶点
                        if (selectedLine.StartPoint == lastPoint)
                        {
                            plinePoints.Add(selectedLine.EndPoint);
                        }
                        else
                        {
                            plinePoints.Add(selectedLine.StartPoint);
                        }
                        //递归继续搜索
                        JionLinesToPline(ref lineObjectIds, tr, ref plinePoints, selectedLineId, startLineId);
                        //删除中间线段
                        selectedLine.Erase();
                    }
                }
            }
        }

        /// <summary>
        /// 搜索图框
        /// V1.0 by WeltionChen@2011.02.24
        /// </summary>
        public static List<ObjectId> GetDrawingFrames(Database db)
        {
            List<ObjectId> result = null;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                Point3d minPoint = db.Extmin;
                Point3d maxPoint = db.Extmax;
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ViewTableRecord currentVP = ed.GetCurrentView() as ViewTableRecord;
                ViewTableRecord zoomVP = new ViewTableRecord();
                zoomVP.CopyFrom(currentVP);
                zoomVP.Width = maxPoint.X - minPoint.X;
                zoomVP.Height = maxPoint.Y - minPoint.Y;
                zoomVP.CenterPoint = new Point2d((minPoint.X + maxPoint.X) / 2, (minPoint.Y + maxPoint.Y) / 2);
                ed.SetCurrentView(zoomVP);
                List<Point3d[]> drawingFramePoints = new List<Point3d[]> { };
                bool isSearchByFrameLine = true; //测试用
                if (isSearchByFrameLine)
                {
                    SelectionFilter frameFilter = new SelectionFilter(new TypedValue[] { new TypedValue(0, "LWPOLYLINE"), new TypedValue(90, 4), new TypedValue(70, 1) });
                    PromptSelectionResult selectedFrameResult = ed.SelectAll(frameFilter);
                    if (selectedFrameResult.Status == PromptStatus.OK)
                    {
                        List<ObjectId> selectedObjectIds = new List<ObjectId>(selectedFrameResult.Value.GetObjectIds());
                        List<ObjectId> resultObjectIds = new List<ObjectId>(selectedFrameResult.Value.GetObjectIds());
                        RemoveInnerPLine(tr, ref selectedObjectIds, ref resultObjectIds);
                        foreach (ObjectId frameId in resultObjectIds)
                        {
                            Autodesk.AutoCAD.DatabaseServices.Polyline framePline = tr.GetObject(frameId, OpenMode.ForRead) as Autodesk.AutoCAD.DatabaseServices.Polyline;
                            framePline.Highlight();
                        }
                        result = resultObjectIds;
                    }
                }
            }
            return result;
        }
        private static void RemoveInnerPLine(Transaction tr, ref List<ObjectId> selectedObjectIds, ref List<ObjectId> resultObjectIds)
        {
            ObjectId outerPlineId = selectedObjectIds[0];
            selectedObjectIds.RemoveAt(0);
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Autodesk.AutoCAD.DatabaseServices.Polyline outerPline = tr.GetObject(outerPlineId, OpenMode.ForRead) as Autodesk.AutoCAD.DatabaseServices.Polyline;
            SelectionFilter frameFilter = new SelectionFilter(new TypedValue[] { new TypedValue(0, "LWPOLYLINE"), new TypedValue(90, 4), new TypedValue(70, 1) });
            PromptSelectionResult getInnerPlineResult = ed.SelectWindow(outerPline.GetPoint3dAt(0), outerPline.GetPoint3dAt(2), frameFilter);
            if (getInnerPlineResult.Status == PromptStatus.OK)
            {
                List<ObjectId> innerPlineObjectIds = new List<ObjectId>(getInnerPlineResult.Value.GetObjectIds());
                innerPlineObjectIds.Remove(outerPlineId);
                foreach (ObjectId innerPlineObjectId in innerPlineObjectIds)
                {
                    selectedObjectIds.Remove(innerPlineObjectId);
                    resultObjectIds.Remove(innerPlineObjectId);
                }
                if (selectedObjectIds.Count > 0)
                {
                    RemoveInnerPLine(tr, ref selectedObjectIds, ref resultObjectIds);
                }
            }
        }

        public static FrameInfo GetFrameSizeScale(Database db, ObjectId frameId)
        {
            FrameInfo frameInfo = null;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                Polyline frameLine = tr.GetObject(frameId, OpenMode.ForRead) as Polyline;
                Point2d pt1 = frameLine.GetPoint2dAt(0);
                Point2d pt2 = frameLine.GetPoint2dAt(2);
                GetFrameSizeScale(pt1, pt2, out frameInfo);
                Extents2d extents2d = new Extents2d(pt1, pt2);
                frameInfo.extents2d = extents2d;
            }
            return frameInfo;
        }

        private static bool GetFrameSizeScale(Point2d pt1, Point2d pt2, out FrameInfo frameInfo)
        {
            frameInfo = new FrameInfo();
            List<int> pageWidths = new List<int> { 841, 594, 419, 293, 210 };

            double frameXSize = Math.Abs(pt1.X - pt2.X);

            double frameYSize = Math.Abs(pt1.Y - pt2.Y);

            double frameWidth = Math.Min(frameXSize, frameYSize);

            for (int i = 0; i < pageWidths.Count; i++)
            {

                int pageWidth = pageWidths[i];

                if ((int)frameWidth % pageWidth == 0)
                {
                    double scale = pageWidth / frameWidth;
                    frameInfo.scale = scale;
                    frameInfo.width = frameXSize * scale;
                    frameInfo.heigth = frameYSize * scale;
                    return true;
                }

            }

            return false;
        }

        private static PlotInfo SetPlotInfo(Document doc, Extents2d exArea, string strPrinter, int iSize, double scale)
        {
            Database db = doc.Database;
            Editor ed = doc.Editor;
            using (Transaction tr = db.TransactionManager.StartTransaction())           //开启事务
            {
                //设置布局管理器为当前布局管理器
                LayoutManager layoutMan = LayoutManager.Current;
                //获取当前布局，用GetObject的方式
                Layout currentLayout = tr.GetObject(layoutMan.GetLayoutId(layoutMan.CurrentLayout), OpenMode.ForRead) as Layout;
                //创建一个PlotInfo类，从布局获取信息
                PlotInfo pi = new PlotInfo();
                pi.Layout = currentLayout.ObjectId;
                //从布局获取一个PlotSettings对象的附本
                PlotSettings ps = new PlotSettings(currentLayout.ModelType);
                ps.CopyFrom(currentLayout);
                //创建PlotSettingsValidator对象，通过它来改变PlotSettings.
                PlotSettingsValidator psv = PlotSettingsValidator.Current;
                //设置打印窗口范围
                psv.SetPlotWindowArea(ps, exArea);
                psv.SetPlotType(ps, Autodesk.AutoCAD.DatabaseServices.PlotType.Window);
                //设置打印比例为布满图纸
                //psv.SetUseStandardScale(ps, true);
                psv.SetCustomPrintScale(ps, new CustomScale(1.0, 1.0 / scale));
                psv.SetStdScaleType(ps, StdScaleType.ScaleToFit);
                //设置居中打印
                psv.SetPlotCentered(ps, true);
                //设置横向打印还是纵向打印
                if ((exArea.MaxPoint.X - exArea.MinPoint.X) > (exArea.MaxPoint.Y - exArea.MinPoint.Y))
                {
                    psv.SetPlotRotation(ps, PlotRotation.Degrees090);                   //横向
                }
                else
                {
                    psv.SetPlotRotation(ps, PlotRotation.Degrees000);                   //纵向  
                }
                //设置打印设备

                if (iSize == 3)
                {
                    psv.SetPlotConfigurationName(ps, strPrinter, null);                 //打印A3
                    StringCollection sMediasA3 = psv.GetCanonicalMediaNameList(ps);
                    bool isA3 = false;
                    int i = 0;
                    for (i = 0; i < sMediasA3.Count; i++)
                    {
                        if (sMediasA3[i].ToLower().Contains("a3") == true)
                        {
                            isA3 = true;
                            break;
                        }
                    }
                    if (isA3 == true)
                    {
                        psv.SetPlotConfigurationName(ps, strPrinter, sMediasA3[i]);
                    }
                    else
                    {
                        MessageBox.Show("您选择的打印机不支持A3！请重新选择A3打印机");
                        return null;
                    }
                }
                else
                {
                    psv.SetPlotConfigurationName(ps, strPrinter, null);                 //打印A4
                    StringCollection sMediasA4 = psv.GetCanonicalMediaNameList(ps);
                    bool isA4 = false;
                    int j = 0;
                    for (j = 0; j < sMediasA4.Count; j++)
                    {
                        if (sMediasA4[j].ToLower().Contains("a4") == true)
                        {
                            isA4 = true;
                            break;
                        }
                    }
                    if (isA4 == true)
                    {
                        psv.SetPlotConfigurationName(ps, strPrinter, sMediasA4[j]);
                    }
                    else
                    {
                        MessageBox.Show("您选择的A4打印机不支持A4,请重新选择A4打印机!");
                    }
                }
                //重写打印信息
                pi.OverrideSettings = ps;
                //确认打印信息
                PlotInfoValidator piv = new PlotInfoValidator();
                piv.MediaMatchingPolicy = MatchingPolicy.MatchEnabled;
                piv.Validate(pi);
                return pi;
            }
        }
        public static int Plot(Document doc, Extents2d exArea, string strPrinter, int iSize, double scale)  //打印单张图纸
        {
            PlotInfo pi = SetPlotInfo(doc, exArea, strPrinter, iSize, scale);
            if (pi == null)
            {
                return 1;
            }
            //设置系统环境变量
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("BACKGROUNDPLOT", 0);
            try
            {
                //检查打印状态
                if (PlotFactory.ProcessPlotState == ProcessPlotState.NotPlotting)
                {
                    //开户引擎
                    using (PlotEngine pe = PlotFactory.CreatePublishEngine())
                    {
                        PlotProgressDialog ppDialog = new PlotProgressDialog(false, 1, true);
                        using (ppDialog)
                        {
                            //显示进程对话框
                            ppDialog.OnBeginPlot();
                            ppDialog.IsVisible = true;
                            //开始打印布局
                            pe.BeginPlot(ppDialog, null);
                            //sw.WriteLine(DateTime.Now + "       pe.BeginPlot()开始执行.");
                            //显示当前打印相关信息
                            pe.BeginDocument(pi, doc.Name, null, 1, false, null);
                            //打印第一个视口
                            PlotPageInfo ppInfo = new PlotPageInfo();
                            //sw.WriteLine(DateTime.Now + "       创建一个PlotPageInfo类");
                            pe.BeginPage(ppInfo, pi, true, null);    //此处第三个参数至为关键，如果设为false则会不能执行.
                            pe.BeginGenerateGraphics(null);
                            pe.EndGenerateGraphics(null);
                            //完成打印视口
                            pe.EndPage(null);
                            ppDialog.OnEndSheet();
                            //完成文档打印
                            pe.EndDocument(null);
                            //完成打印操作
                            ppDialog.OnEndPlot();
                            pe.EndPlot(null);

                        }
                    }
                }
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("BACKGROUNDPLOT", 2);
            //sw.WriteLine(DateTime.Now + "       设置环境变量BACKGROUNDPLOT = 2.\n\n\n");
            //sw.Close();
            return 0;
        }
    }
}
