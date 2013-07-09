using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using System.Linq;
using System.IO;
using com.ccepc.utils;
using com.ccepc.entities;
using DNA;
using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;
using Telerik.WinControls.Enumerations;
using System.Collections;
using Model.com.ccepc.utils;

namespace CAD
{
    public partial class SaveFileForm : Telerik.WinControls.UI.RadForm
    {
        private string fileName;
        private string fName;
        private string pdfPath = @"D:\ftpHome\pdfcreator";

        private CADService service = HessianHelper.getServiceInstance();

        public SaveFileForm()
        {
            InitializeComponent();
            OkButton.Enabled = false;
            string result = service.getDesignerConfigsByUser(AppInitialization.loginUser.id.ToString());
            List<DesignerConfig> designerConfigs = JsonHelper.JsonDeserialize<List<DesignerConfig>>(result.ToString());
            foreach (var desigerConfig in designerConfigs)
            {
                DisciplineConfig disciplineConfig = desigerConfig.disciplineConfig;
                SubProject subProject = disciplineConfig.subProject;
                Project project = subProject.project;
                string projectName = subProject.subProjectName + "  "
                                    + disciplineConfig.discipline.disciplineName
                                    + desigerConfig.configDesc;
                itemComboBox.ComboBoxElement.Items.Add(new RadComboBoxItem(projectName, desigerConfig));
            }
            fileName = Tools.Document.Name;
            fName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
            Tools.RunCommand(true, "qsave");
            dwgPic.BackColor = Color.White;
            dwgPic.DwgFileName = fileName;
        }

        private void clearPlotFiles(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (System.IO.FileInfo f in dir.GetFiles("*.pdf"))
            {
                f.Delete();
            }
        }

        private ArrayList getPlotFiles(string path)
        {
            ArrayList plotFiles = new ArrayList();
            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (System.IO.FileInfo f in dir.GetFiles("*.pdf")) 
            {
                plotFiles.Add(f.FullName);
            }
            return plotFiles;
        }

        private string getCommands(ArrayList plotFiles,string outputFile)
        {
            string cmd = @"D:\360°²È«ä¯ÀÀÆ÷ÏÂÔØ\pdfspme_win\pdfspme_cmd.exe  -mer";
            foreach(string plotFile in plotFiles)
            {
                cmd = cmd + " -i " + plotFile;
            }
            cmd = cmd + " -o " + outputFile;
            return cmd;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DesignerConfig designerConfig = itemComboBox.SelectedValue as DesignerConfig;
            string result = service.getDesigneFolder(designerConfig.id.ToString());
            FolderInfo folderInfo = JsonHelper.JsonDeserialize<FolderInfo>(result.ToString());
            string tempFile = Environment.GetEnvironmentVariable("TEMP") + "\\" + fName;
            string uid = Guid.NewGuid().ToString("D");
            string fileNewName = uid + ".dwg";
            FtpUtil.UploadFile(tempFile, folderInfo.folderPath, 
                "127.0.0.1", "admin", "admin",fileNewName);
            this.Hide();
            ArrayList plotFiles = getPlotFiles(pdfPath);
            string outputFile = pdfPath + "\\" + fName.Substring(0,fName.LastIndexOf(".")) + ".pdf";
            string cmd = getCommands(plotFiles, outputFile);
            CommonTools.RunCmd(cmd, 0);
            FtpUtil.UploadFile(outputFile, folderInfo.folderPath,
                "127.0.0.1", "admin", "admin", uid + ".pdf");
            result = service.uploadFile(designerConfig.id.ToString(), fileNewName, fName, AppInitialization.loginUser.id.ToString());
            System.IO.FileInfo f = new System.IO.FileInfo(tempFile);
            f.Delete();
            RadMessageBox.Show(result.ToString());
            this.Close();
        }

        private void itemComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DesignerConfig designerConfig = itemComboBox.SelectedValue as DesignerConfig;
            string result = service.getFileInfoByDesignerConfigAndFileName(designerConfig.id.ToString(), fName);
            com.ccepc.entities.FileInfo fileInfo = JsonHelper.JsonDeserialize<com.ccepc.entities.FileInfo>(result.ToString());
            if (fileInfo == null)
            {
                overrideVersionButton.Enabled = false;
                newVersionButton.ToggleState = ToggleState.On;
            }
            else
            {
                overrideVersionButton.Enabled = true;
                overrideVersionButton.ToggleState = ToggleState.On;
            }
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            clearPlotFiles(pdfPath);
            string tempFile = Environment.GetEnvironmentVariable("TEMP") + "\\" + fName;
            File.Copy(fileName, tempFile, true);
            CommonTools.OpenDoc(tempFile);
            Document doc = G.Doc;
            Database db = G.Db;
            List<FrameInfo> frames = PlotUtil.GetDrawingFrames(db);
            foreach (FrameInfo frameInfo in frames)
            {
                PlotUtil.Plot(doc, frameInfo.extents2d, "PDFCreator", 3, frameInfo.scale);
            }
            doc.CloseAndDiscard();
            OkButton.Enabled = true;
            this.Show();
        }
    }
}
