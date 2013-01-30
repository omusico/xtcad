namespace CAD
{
    partial class BatchPlot
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
            this.grpgroupBox1 = new System.Windows.Forms.GroupBox();
            this.exitButton = new System.Windows.Forms.Button();
            this.plotButton = new System.Windows.Forms.Button();
            this.previewButton = new System.Windows.Forms.Button();
            this.centerPrintCheck = new System.Windows.Forms.CheckBox();
            this.copyCombo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.fillCustomRadio = new System.Windows.Forms.RadioButton();
            this.fillPaperRadio = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.styleCombo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.paperSizeCombo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.printerCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fileListGroup = new System.Windows.Forms.GroupBox();
            this.fileListView = new System.Windows.Forms.DataGridView();
            this.removeFileButton = new System.Windows.Forms.Button();
            this.addFileButton = new System.Windows.Forms.Button();
            this.fileNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filePathColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpgroupBox1.SuspendLayout();
            this.fileListGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileListView)).BeginInit();
            this.SuspendLayout();
            // 
            // grpgroupBox1
            // 
            this.grpgroupBox1.Controls.Add(this.exitButton);
            this.grpgroupBox1.Controls.Add(this.plotButton);
            this.grpgroupBox1.Controls.Add(this.previewButton);
            this.grpgroupBox1.Controls.Add(this.centerPrintCheck);
            this.grpgroupBox1.Controls.Add(this.copyCombo);
            this.grpgroupBox1.Controls.Add(this.label5);
            this.grpgroupBox1.Controls.Add(this.fillCustomRadio);
            this.grpgroupBox1.Controls.Add(this.fillPaperRadio);
            this.grpgroupBox1.Controls.Add(this.label4);
            this.grpgroupBox1.Controls.Add(this.styleCombo);
            this.grpgroupBox1.Controls.Add(this.label3);
            this.grpgroupBox1.Controls.Add(this.paperSizeCombo);
            this.grpgroupBox1.Controls.Add(this.label2);
            this.grpgroupBox1.Controls.Add(this.printerCombo);
            this.grpgroupBox1.Controls.Add(this.label1);
            this.grpgroupBox1.ForeColor = System.Drawing.Color.Blue;
            this.grpgroupBox1.Location = new System.Drawing.Point(12, 12);
            this.grpgroupBox1.Name = "grpgroupBox1";
            this.grpgroupBox1.Size = new System.Drawing.Size(409, 177);
            this.grpgroupBox1.TabIndex = 0;
            this.grpgroupBox1.TabStop = false;
            this.grpgroupBox1.Text = "打印选项";
            // 
            // exitButton
            // 
            this.exitButton.ForeColor = System.Drawing.SystemColors.WindowText;
            this.exitButton.Location = new System.Drawing.Point(319, 83);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 16;
            this.exitButton.Text = " 退  出";
            this.exitButton.UseVisualStyleBackColor = true;
            // 
            // plotButton
            // 
            this.plotButton.ForeColor = System.Drawing.SystemColors.WindowText;
            this.plotButton.Location = new System.Drawing.Point(319, 52);
            this.plotButton.Name = "plotButton";
            this.plotButton.Size = new System.Drawing.Size(75, 23);
            this.plotButton.TabIndex = 15;
            this.plotButton.Text = "打印文档";
            this.plotButton.UseVisualStyleBackColor = true;
            this.plotButton.Click += new System.EventHandler(this.plotButton_Click);
            // 
            // previewButton
            // 
            this.previewButton.ForeColor = System.Drawing.SystemColors.WindowText;
            this.previewButton.Location = new System.Drawing.Point(319, 23);
            this.previewButton.Name = "previewButton";
            this.previewButton.Size = new System.Drawing.Size(75, 23);
            this.previewButton.TabIndex = 14;
            this.previewButton.Text = "打印预览";
            this.previewButton.UseVisualStyleBackColor = true;
            // 
            // centerPrintCheck
            // 
            this.centerPrintCheck.AutoSize = true;
            this.centerPrintCheck.ForeColor = System.Drawing.SystemColors.WindowText;
            this.centerPrintCheck.Location = new System.Drawing.Point(219, 149);
            this.centerPrintCheck.Name = "centerPrintCheck";
            this.centerPrintCheck.Size = new System.Drawing.Size(72, 16);
            this.centerPrintCheck.TabIndex = 11;
            this.centerPrintCheck.Text = "居中打印";
            this.centerPrintCheck.UseVisualStyleBackColor = true;
            // 
            // copyCombo
            // 
            this.copyCombo.FormattingEnabled = true;
            this.copyCombo.Location = new System.Drawing.Point(80, 147);
            this.copyCombo.Name = "copyCombo";
            this.copyCombo.Size = new System.Drawing.Size(104, 20);
            this.copyCombo.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label5.Location = new System.Drawing.Point(20, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "打印份数：";
            // 
            // fillCustomRadio
            // 
            this.fillCustomRadio.AutoSize = true;
            this.fillCustomRadio.ForeColor = System.Drawing.SystemColors.WindowText;
            this.fillCustomRadio.Location = new System.Drawing.Point(157, 119);
            this.fillCustomRadio.Name = "fillCustomRadio";
            this.fillCustomRadio.Size = new System.Drawing.Size(83, 16);
            this.fillCustomRadio.TabIndex = 8;
            this.fillCustomRadio.TabStop = true;
            this.fillCustomRadio.Text = "自适应比例";
            this.fillCustomRadio.UseVisualStyleBackColor = true;
            // 
            // fillPaperRadio
            // 
            this.fillPaperRadio.AutoSize = true;
            this.fillPaperRadio.ForeColor = System.Drawing.SystemColors.WindowText;
            this.fillPaperRadio.Location = new System.Drawing.Point(80, 119);
            this.fillPaperRadio.Name = "fillPaperRadio";
            this.fillPaperRadio.Size = new System.Drawing.Size(71, 16);
            this.fillPaperRadio.TabIndex = 7;
            this.fillPaperRadio.TabStop = true;
            this.fillPaperRadio.Text = "布满图纸";
            this.fillPaperRadio.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label4.Location = new System.Drawing.Point(20, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "打印比例：";
            // 
            // styleCombo
            // 
            this.styleCombo.FormattingEnabled = true;
            this.styleCombo.Location = new System.Drawing.Point(80, 84);
            this.styleCombo.Name = "styleCombo";
            this.styleCombo.Size = new System.Drawing.Size(211, 20);
            this.styleCombo.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label3.Location = new System.Drawing.Point(20, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "打印样式：";
            // 
            // paperSizeCombo
            // 
            this.paperSizeCombo.FormattingEnabled = true;
            this.paperSizeCombo.Location = new System.Drawing.Point(80, 53);
            this.paperSizeCombo.Name = "paperSizeCombo";
            this.paperSizeCombo.Size = new System.Drawing.Size(211, 20);
            this.paperSizeCombo.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label2.Location = new System.Drawing.Point(20, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "纸张大小：";
            // 
            // printerCombo
            // 
            this.printerCombo.FormattingEnabled = true;
            this.printerCombo.Location = new System.Drawing.Point(80, 24);
            this.printerCombo.Name = "printerCombo";
            this.printerCombo.Size = new System.Drawing.Size(211, 20);
            this.printerCombo.TabIndex = 1;
            this.printerCombo.SelectedIndexChanged += new System.EventHandler(this.printerCombo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label1.Location = new System.Drawing.Point(20, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "打 印 机：";
            // 
            // fileListGroup
            // 
            this.fileListGroup.Controls.Add(this.fileListView);
            this.fileListGroup.Controls.Add(this.removeFileButton);
            this.fileListGroup.Controls.Add(this.addFileButton);
            this.fileListGroup.ForeColor = System.Drawing.Color.Blue;
            this.fileListGroup.Location = new System.Drawing.Point(12, 196);
            this.fileListGroup.Name = "fileListGroup";
            this.fileListGroup.Size = new System.Drawing.Size(409, 200);
            this.fileListGroup.TabIndex = 1;
            this.fileListGroup.TabStop = false;
            this.fileListGroup.Text = "文件列表";
            // 
            // fileListView
            // 
            this.fileListView.AllowUserToAddRows = false;
            this.fileListView.AllowUserToDeleteRows = false;
            this.fileListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fileListView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fileNameColumn,
            this.filePathColumn});
            this.fileListView.Location = new System.Drawing.Point(6, 49);
            this.fileListView.Name = "fileListView";
            this.fileListView.ReadOnly = true;
            this.fileListView.RowHeadersVisible = false;
            this.fileListView.RowTemplate.Height = 23;
            this.fileListView.Size = new System.Drawing.Size(388, 141);
            this.fileListView.TabIndex = 18;
            // 
            // removeFileButton
            // 
            this.removeFileButton.ForeColor = System.Drawing.SystemColors.WindowText;
            this.removeFileButton.Location = new System.Drawing.Point(109, 20);
            this.removeFileButton.Name = "removeFileButton";
            this.removeFileButton.Size = new System.Drawing.Size(75, 23);
            this.removeFileButton.TabIndex = 17;
            this.removeFileButton.Text = "删除文件";
            this.removeFileButton.UseVisualStyleBackColor = true;
            this.removeFileButton.Click += new System.EventHandler(this.removeFileButton_Click);
            // 
            // addFileButton
            // 
            this.addFileButton.ForeColor = System.Drawing.SystemColors.WindowText;
            this.addFileButton.Location = new System.Drawing.Point(22, 20);
            this.addFileButton.Name = "addFileButton";
            this.addFileButton.Size = new System.Drawing.Size(75, 23);
            this.addFileButton.TabIndex = 16;
            this.addFileButton.Text = "添加文件";
            this.addFileButton.UseVisualStyleBackColor = true;
            this.addFileButton.Click += new System.EventHandler(this.addFileButton_Click);
            // 
            // fileNameColumn
            // 
            this.fileNameColumn.HeaderText = "文件名";
            this.fileNameColumn.Name = "fileNameColumn";
            this.fileNameColumn.ReadOnly = true;
            // 
            // filePathColumn
            // 
            this.filePathColumn.HeaderText = "文件路径";
            this.filePathColumn.Name = "filePathColumn";
            this.filePathColumn.ReadOnly = true;
            this.filePathColumn.Width = 270;
            // 
            // BatchPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 408);
            this.Controls.Add(this.fileListGroup);
            this.Controls.Add(this.grpgroupBox1);
            this.MaximizeBox = false;
            this.Name = "BatchPlot";
            this.Text = "AutoCAD批量打印 - v1.0";
            this.grpgroupBox1.ResumeLayout(false);
            this.grpgroupBox1.PerformLayout();
            this.fileListGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fileListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpgroupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox printerCombo;
        private System.Windows.Forms.ComboBox paperSizeCombo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox styleCombo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton fillPaperRadio;
        private System.Windows.Forms.RadioButton fillCustomRadio;
        private System.Windows.Forms.ComboBox copyCombo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox centerPrintCheck;
        private System.Windows.Forms.Button previewButton;
        private System.Windows.Forms.Button plotButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.GroupBox fileListGroup;
        private System.Windows.Forms.Button removeFileButton;
        private System.Windows.Forms.Button addFileButton;
        private System.Windows.Forms.DataGridView fileListView;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn filePathColumn;
    }
}