namespace CAD
{
    partial class ToolsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.saveFileButton = new Telerik.WinControls.UI.RadMenuItem();
            this.checkButton = new Telerik.WinControls.UI.RadMenuItem();
            this.dwgListButton = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenu1 = new Telerik.WinControls.UI.RadMenu();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // saveFileButton
            // 
            this.saveFileButton.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
            this.saveFileButton.Image = global::CAD.Properties.Resources.save2;
            this.saveFileButton.Name = "saveFileButton";
            this.saveFileButton.Text = "radMenuItem3";
            this.saveFileButton.ToolTipText = "保存图纸";
            this.saveFileButton.Click += new System.EventHandler(this.saveFileButton_Click);
            // 
            // checkButton
            // 
            this.checkButton.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
            this.checkButton.Image = global::CAD.Properties.Resources.msoffice_menu_documentworkspace;
            this.checkButton.Name = "checkButton";
            this.checkButton.Text = "radMenuItem1";
            this.checkButton.ToolTipText = "校审批注";
            // 
            // dwgListButton
            // 
            this.dwgListButton.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
            this.dwgListButton.Image = global::CAD.Properties.Resources.msoffice_Footer;
            this.dwgListButton.Name = "dwgListButton";
            this.dwgListButton.Text = "radMenuItem2";
            this.dwgListButton.ToolTipText = "图纸目录";
            // 
            // ToolsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(192, 42);
            // 
            // radMenu1
            // 
            this.radMenu1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.saveFileButton,
            this.checkButton,
            this.dwgListButton});
            this.radMenu1.Location = new System.Drawing.Point(0, 0);
            this.radMenu1.Name = "radMenu1";
            this.radMenu1.Size = new System.Drawing.Size(192, 40);
            this.radMenu1.TabIndex = 0;
            this.radMenu1.Text = "radMenu1";
            this.Controls.Add(this.radMenu1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(200, 80);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(200, 80);
            this.Name = "ToolsForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MaxSize = new System.Drawing.Size(200, 80);
            this.RootElement.MinSize = new System.Drawing.Size(200, 80);
            this.Text = "协同设计";
            this.ThemeName = "ControlDefault";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ToolsForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.radMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadMenuItem saveFileButton;
        private Telerik.WinControls.UI.RadMenu radMenu1;
        private Telerik.WinControls.UI.RadMenuItem checkButton;
        private Telerik.WinControls.UI.RadMenuItem dwgListButton;
    }
}