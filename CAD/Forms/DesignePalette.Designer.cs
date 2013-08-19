namespace CAD
{
    partial class DesignePalette
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
            this.components = new System.ComponentModel.Container();
            this.radtabstrip = new Telerik.WinControls.UI.RadTabStrip();
            this.tabItem1 = new Telerik.WinControls.UI.TabItem();
            this.InstrumentDeviceTree = new Telerik.WinControls.UI.RadTreeView();
            this.tabItem2 = new Telerik.WinControls.UI.TabItem();
            this.ElectricalDeviceTree = new Telerik.WinControls.UI.RadTreeView();
            this.tabItem3 = new Telerik.WinControls.UI.TabItem();
            this.tabItem4 = new Telerik.WinControls.UI.TabItem();
            ((System.ComponentModel.ISupportInitialize)(this.radtabstrip)).BeginInit();
            this.radtabstrip.SuspendLayout();
            this.tabItem1.ContentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InstrumentDeviceTree)).BeginInit();
            this.tabItem2.ContentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ElectricalDeviceTree)).BeginInit();
            this.SuspendLayout();
            // 
            // radtabstrip
            // 
            this.radtabstrip.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.radtabstrip.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.radtabstrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radtabstrip.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.tabItem1,
            this.tabItem2,
            this.tabItem3,
            this.tabItem4});
            this.radtabstrip.Location = new System.Drawing.Point(0, 0);
            this.radtabstrip.Name = "radtabstrip";
            this.radtabstrip.ScrollOffsetStep = 5;
            this.radtabstrip.Size = new System.Drawing.Size(270, 341);
            this.radtabstrip.TabIndex = 0;
            this.radtabstrip.TabScrollButtonsPosition = Telerik.WinControls.UI.TabScrollButtonsPosition.RightBottom;
            // 
            // tabItem1
            // 
            this.tabItem1.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // tabItem1.ContentPanel
            // 
            this.tabItem1.ContentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.tabItem1.ContentPanel.CausesValidation = true;
            this.tabItem1.ContentPanel.Controls.Add(this.InstrumentDeviceTree);
            this.tabItem1.ContentPanel.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tabItem1.ContentPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.tabItem1.ContentPanel.Location = new System.Drawing.Point(1, 23);
            this.tabItem1.ContentPanel.Size = new System.Drawing.Size(268, 317);
            this.tabItem1.IsSelected = true;
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.StretchHorizontally = false;
            this.tabItem1.Text = "仪表设备";
            // 
            // InstrumentDeviceTree
            // 
            this.InstrumentDeviceTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InstrumentDeviceTree.Location = new System.Drawing.Point(0, 0);
            this.InstrumentDeviceTree.Name = "InstrumentDeviceTree";
            this.InstrumentDeviceTree.Size = new System.Drawing.Size(268, 317);
            this.InstrumentDeviceTree.TabIndex = 0;
            this.InstrumentDeviceTree.Text = "radTreeView1";
            // 
            // tabItem2
            // 
            this.tabItem2.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // tabItem2.ContentPanel
            // 
            this.tabItem2.ContentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.tabItem2.ContentPanel.CausesValidation = true;
            this.tabItem2.ContentPanel.Controls.Add(this.ElectricalDeviceTree);
            this.tabItem2.ContentPanel.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tabItem2.ContentPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.tabItem2.ContentPanel.Location = new System.Drawing.Point(1, 23);
            this.tabItem2.ContentPanel.Size = new System.Drawing.Size(268, 317);
            this.tabItem2.Name = "tabItem2";
            this.tabItem2.StretchHorizontally = false;
            this.tabItem2.Text = "电气设备";
            // 
            // ElectricalDeviceTree
            // 
            this.ElectricalDeviceTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ElectricalDeviceTree.Location = new System.Drawing.Point(0, 0);
            this.ElectricalDeviceTree.Name = "ElectricalDeviceTree";
            this.ElectricalDeviceTree.Size = new System.Drawing.Size(268, 317);
            this.ElectricalDeviceTree.TabIndex = 1;
            this.ElectricalDeviceTree.Text = "radTreeView1";
            // 
            // tabItem3
            // 
            this.tabItem3.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // tabItem3.ContentPanel
            // 
            this.tabItem3.ContentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.tabItem3.ContentPanel.CausesValidation = true;
            this.tabItem3.ContentPanel.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tabItem3.ContentPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.tabItem3.ContentPanel.Location = new System.Drawing.Point(1, 23);
            this.tabItem3.ContentPanel.Size = new System.Drawing.Size(268, 317);
            this.tabItem3.Name = "tabItem3";
            this.tabItem3.StretchHorizontally = false;
            this.tabItem3.Text = "机柜";
            // 
            // tabItem4
            // 
            this.tabItem4.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // tabItem4.ContentPanel
            // 
            this.tabItem4.ContentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.tabItem4.ContentPanel.CausesValidation = true;
            this.tabItem4.ContentPanel.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tabItem4.ContentPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.tabItem4.ContentPanel.Location = new System.Drawing.Point(1, 23);
            this.tabItem4.ContentPanel.Size = new System.Drawing.Size(268, 317);
            this.tabItem4.Name = "tabItem4";
            this.tabItem4.StretchHorizontally = false;
            this.tabItem4.Text = "控制信号";
            // 
            // DesignePalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radtabstrip);
            this.Name = "DesignePalette";
            this.Size = new System.Drawing.Size(270, 341);
            ((System.ComponentModel.ISupportInitialize)(this.radtabstrip)).EndInit();
            this.radtabstrip.ResumeLayout(false);
            this.tabItem1.ContentPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.InstrumentDeviceTree)).EndInit();
            this.tabItem2.ContentPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ElectricalDeviceTree)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadTabStrip radtabstrip;
        private Telerik.WinControls.UI.TabItem tabItem1;
        private Telerik.WinControls.UI.TabItem tabItem2;
        private Telerik.WinControls.UI.TabItem tabItem3;
        private Telerik.WinControls.UI.TabItem tabItem4;
        private Telerik.WinControls.UI.RadTreeView InstrumentDeviceTree;
        private Telerik.WinControls.UI.RadTreeView ElectricalDeviceTree;


    }
}
