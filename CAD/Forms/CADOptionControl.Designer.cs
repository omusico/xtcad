namespace CAD
{
    partial class CADOptionControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.serverPath = new Telerik.WinControls.UI.RadTextBox();
            this.serverLabel = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.serverPath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serverLabel)).BeginInit();
            this.SuspendLayout();
            // 
            // serverPath
            // 
            this.serverPath.Location = new System.Drawing.Point(83, 15);
            this.serverPath.Name = "serverPath";
            this.serverPath.Size = new System.Drawing.Size(224, 20);
            this.serverPath.TabIndex = 0;
            this.serverPath.TabStop = false;
            // 
            // serverLabel
            // 
            this.serverLabel.Location = new System.Drawing.Point(3, 18);
            this.serverLabel.Name = "serverLabel";
            this.serverLabel.Size = new System.Drawing.Size(74, 17);
            this.serverLabel.TabIndex = 1;
            this.serverLabel.Text = "服务器地址:";
            // 
            // CADOptionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.serverLabel);
            this.Controls.Add(this.serverPath);
            this.Name = "CADOptionControl";
            this.Size = new System.Drawing.Size(330, 246);
            ((System.ComponentModel.ISupportInitialize)(this.serverPath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serverLabel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadTextBox serverPath;
        private Telerik.WinControls.UI.RadLabel serverLabel;
    }
}
