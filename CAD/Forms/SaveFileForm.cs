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

namespace CAD
{
    public partial class SaveFileForm : Telerik.WinControls.UI.RadForm
    {
        private string fileName;
        private string fName;

        public SaveFileForm()
        {
            InitializeComponent();
            string[] args = new string[1];
            args[0] = AppInitialization.loginUser.id.ToString();
            try{
                object result = WebServiceHelper.InvokeWebService("UserWebservice", "getDesignerConfigs", args);
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
            catch(Exception e) {
                RadMessageBox.Show(e.Message);
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DesignerConfig designerConfig = itemComboBox.SelectedValue as DesignerConfig;
            FolderInfo folderInfo = designerConfig.folderInfos.Where(o => o.folderSubType == "designefile" && o.folderType == "designeConfig").FirstOrDefault();
            G.Doc.CloseAndSave(fileName);
            string fileNewName = Guid.NewGuid().ToString("D") + ".dwg";
            FtpUtil.UploadFile(fileName, folderInfo.folderPath, 
                "127.0.0.1", "admin", "admin",fileNewName);
            CommonTools.OpenDoc(fileName);
            string[] args = new string[3];
            args[0] = designerConfig.id.ToString();
            args[1] = fileNewName;
            args[2] = fName;
            object result = WebServiceHelper.InvokeWebService("UserWebservice", "uploadFile", args);
            RadMessageBox.Show(result.ToString());
            this.Close();
        }

        private void itemComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DesignerConfig designerConfig = itemComboBox.SelectedValue as DesignerConfig;
            com.ccepc.entities.FileInfo fileInfo = designerConfig.fileInfos.Where(o => o.fileName == fName).FirstOrDefault();
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
    }
}
