using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using System.Collections.Specialized;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.PlottingServices;
using Autodesk.AutoCAD.EditorInput;
using System.IO;
using Autodesk.AutoCAD.Geometry;

namespace CAD
{
    public partial class BatchPlot : Form
    {
        Layout currentLayout = null;
        PlotInfo pi = null;
        PlotSettingsValidator psv = null;

        public BatchPlot()
        {
            InitializeComponent();
            //创建PlotSettingsValidator对象，通过它来改变PlotSettings.
            psv = PlotSettingsValidator.Current;
            StringCollection plotDevices = psv.GetPlotDeviceList();
            StringCollection plotStyles = psv.GetPlotStyleSheetList();
            printerCombo.DataSource = plotDevices;
            styleCombo.DataSource = plotStyles;
        }

        private void printerCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlotSettings ps = new PlotSettings(true);
            psv.SetPlotConfigurationName(ps, printerCombo.SelectedText, null);
            psv.RefreshLists(ps);
            StringCollection paperSizes = psv.GetCanonicalMediaNameList(ps);
            this.paperSizeCombo.DataSource = paperSizes;
        }

        private PlotSettings initialPlotInfo()
        {
            PlotSettings ps = null;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;
            using (Transaction tr = db.TransactionManager.StartTransaction())           //开启事务
            {
                //设置布局管理器为当前布局管理器
                LayoutManager layoutMan = LayoutManager.Current;
                //获取当前布局，用GetObject的方式
                currentLayout = tr.GetObject(layoutMan.GetLayoutId(layoutMan.CurrentLayout), OpenMode.ForRead) as Layout;
                //创建一个PlotInfo类，从布局获取信息
                pi = new PlotInfo();
                pi.Layout = currentLayout.ObjectId;
                //从布局获取一个PlotSettings对象的附本
                ps = new PlotSettings(currentLayout.ModelType);
                ps.CopyFrom(currentLayout);
            }
            return ps;
        }

        private void addFileButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "cad图纸文件|*.dwg";
                dialog.Multiselect = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        String[] fileFullNames = dialog.FileNames;
                        foreach (String s in fileFullNames)
                        {
                            String fileName = Path.GetFileName(s);
                            String filePath = Path.GetDirectoryName(s);
                            fileListView.Rows.Add(new String[] { fileName, filePath });
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void removeFileButton_Click(object sender, EventArgs e)
        {
            fileListView.Rows.Remove(fileListView.CurrentRow);
        }

        private void plotButton_Click(object sender, EventArgs e)
        {
            if (fileListView.RowCount > 0)
            {
                this.Hide();
                for (int i = 0; i < fileListView.Rows.Count; i++)
                {
                    String fileName = fileListView.Rows[i].Cells[1].Value.ToString() + @"\" + fileListView.Rows[i].Cells[0].Value.ToString();
                    DocumentCollection acDocMgr = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager;
                    if (File.Exists(fileName))
                    {
                        Document doc = acDocMgr.Open(fileName, false);
                        Database db = doc.Database;
                        List<ObjectId> frames = PlotUtil.GetDrawingFrames(db);
                        foreach (ObjectId id in frames)
                        {
                            
                            FrameInfo frameInfo = PlotUtil.GetFrameSizeScale(db,id);
                            PlotUtil.Plot(doc, frameInfo.extents2d, printerCombo.Text, 3,frameInfo.scale);
                        }
                    }
                }
                this.Show();
            }
        }
    }
}
