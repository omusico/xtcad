namespace CAD
{
    partial class SaveFileForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveFileForm));
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.itemComboBox = new Telerik.WinControls.UI.RadComboBox();
            this.OkButton = new Telerik.WinControls.UI.RadButton();
            this.CancelButton = new Telerik.WinControls.UI.RadButton();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.axDwgThumbnail1 = new AxDWGTHUMBNAILLib.AxDwgThumbnail();
            this.dwgPic = new AxDWGTHUMBNAILLib.AxDwgThumbnail();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.newVersionButton = new Telerik.WinControls.UI.RadRadioButton();
            this.overrideVersionButton = new Telerik.WinControls.UI.RadRadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OkButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CancelButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axDwgThumbnail1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dwgPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.newVersionButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.overrideVersionButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(12, 12);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(52, 18);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "选择项目";
            // 
            // itemComboBox
            // 
            this.itemComboBox.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.itemComboBox.Location = new System.Drawing.Point(72, 11);
            this.itemComboBox.Name = "itemComboBox";
            // 
            // 
            // 
            this.itemComboBox.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.itemComboBox.Size = new System.Drawing.Size(208, 20);
            this.itemComboBox.TabIndex = 37;
            this.itemComboBox.TabStop = false;
            this.itemComboBox.SelectedIndexChanged += new System.EventHandler(this.itemComboBox_SelectedIndexChanged);
            // 
            // OkButton
            // 
            this.OkButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.OkButton.Location = new System.Drawing.Point(76, 204);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 38;
            this.OkButton.Text = "添  加";
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.CancelButton.Location = new System.Drawing.Point(163, 204);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 39;
            this.CancelButton.Text = "取   消";
            // 
            // radLabel4
            // 
            this.radLabel4.Location = new System.Drawing.Point(14, 36);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(52, 18);
            this.radLabel4.TabIndex = 40;
            this.radLabel4.Text = "图块预览";
            // 
            // axDwgThumbnail1
            // 
            this.axDwgThumbnail1.Location = new System.Drawing.Point(349, 81);
            this.axDwgThumbnail1.Name = "axDwgThumbnail1";
            this.axDwgThumbnail1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axDwgThumbnail1.OcxState")));
            this.axDwgThumbnail1.Size = new System.Drawing.Size(75, 23);
            this.axDwgThumbnail1.TabIndex = 42;
            // 
            // dwgPic
            // 
            this.dwgPic.Location = new System.Drawing.Point(72, 37);
            this.dwgPic.Name = "dwgPic";
            this.dwgPic.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("dwgPic.OcxState")));
            this.dwgPic.Size = new System.Drawing.Size(203, 134);
            this.dwgPic.TabIndex = 43;
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(14, 169);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(52, 18);
            this.radLabel2.TabIndex = 44;
            this.radLabel2.Text = "文件版本";
            // 
            // newVersionButton
            // 
            this.newVersionButton.Location = new System.Drawing.Point(76, 169);
            this.newVersionButton.Name = "newVersionButton";
            this.newVersionButton.Size = new System.Drawing.Size(75, 18);
            this.newVersionButton.TabIndex = 45;
            this.newVersionButton.Text = "产生新版本";
            // 
            // overrideVersionButton
            // 
            this.overrideVersionButton.Location = new System.Drawing.Point(163, 169);
            this.overrideVersionButton.Name = "overrideVersionButton";
            this.overrideVersionButton.Size = new System.Drawing.Size(95, 18);
            this.overrideVersionButton.TabIndex = 46;
            this.overrideVersionButton.Text = "覆盖上一版本";
            // 
            // SaveFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 231);
            this.Controls.Add(this.overrideVersionButton);
            this.Controls.Add(this.newVersionButton);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.dwgPic);
            this.Controls.Add(this.axDwgThumbnail1);
            this.Controls.Add(this.radLabel4);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.itemComboBox);
            this.Controls.Add(this.radLabel1);
            this.Name = "SaveFileForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "保存文件";
            this.ThemeName = "ControlDefault";
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OkButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CancelButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axDwgThumbnail1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dwgPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.newVersionButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.overrideVersionButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadComboBox itemComboBox;
        private Telerik.WinControls.UI.RadButton OkButton;
        private Telerik.WinControls.UI.RadButton CancelButton;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private AxDWGTHUMBNAILLib.AxDwgThumbnail axDwgThumbnail1;
        private AxDWGTHUMBNAILLib.AxDwgThumbnail dwgPic;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadRadioButton newVersionButton;
        private Telerik.WinControls.UI.RadRadioButton overrideVersionButton;
    }
}

