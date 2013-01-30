using System;
using System.Windows.Forms;
namespace CAD
{
    partial class LoginForm
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelX1 = new Telerik.WinControls.UI.RadLabel();
            this.UserName = new Telerik.WinControls.UI.RadComboBox();
            this.labelX2 = new Telerik.WinControls.UI.RadLabel();
            this.Password = new Telerik.WinControls.UI.RadTextBox();
            this.ExitButton = new Telerik.WinControls.UI.RadButton();
            this.LoginButton = new Telerik.WinControls.UI.RadButton();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.radContextMenu1 = new Telerik.WinControls.UI.RadContextMenu(this.components);
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelX1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelX2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Password)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExitButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoginButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(312, 77);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // labelX1
            // 
            this.labelX1.Location = new System.Drawing.Point(23, 96);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(52, 18);
            this.labelX1.TabIndex = 9;
            this.labelX1.Text = "用户名：";
            // 
            // UserName
            // 
            this.UserName.Location = new System.Drawing.Point(77, 97);
            this.UserName.Name = "UserName";
            // 
            // 
            // 
            this.UserName.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.UserName.Size = new System.Drawing.Size(205, 20);
            this.UserName.TabIndex = 10;
            this.UserName.TabStop = false;
            this.UserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UserName_KeyDown);
            // 
            // labelX2
            // 
            this.labelX2.Location = new System.Drawing.Point(23, 127);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(47, 18);
            this.labelX2.TabIndex = 11;
            this.labelX2.Text = "密  码：";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(77, 128);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '●';
            this.Password.Size = new System.Drawing.Size(205, 20);
            this.Password.TabIndex = 12;
            this.Password.TabStop = false;
            this.Password.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Password_KeyDown);
            // 
            // ExitButton
            // 
            this.ExitButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ExitButton.Location = new System.Drawing.Point(167, 169);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(75, 23);
            this.ExitButton.TabIndex = 14;
            this.ExitButton.Text = "退   出";
            this.ExitButton.Click += new System.EventHandler(this.ExitButtonClick);
            // 
            // LoginButton
            // 
            this.LoginButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.LoginButton.Location = new System.Drawing.Point(69, 169);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(75, 23);
            this.LoginButton.TabIndex = 13;
            this.LoginButton.Text = "登   陆";
            this.LoginButton.Click += new System.EventHandler(this.LoginButtonClick);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::CAD.Properties.Resources.loginbackground;
            this.pictureBox2.Location = new System.Drawing.Point(0, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(312, 77);
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 206);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.UserName);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登陆";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelX1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelX2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Password)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExitButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoginButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        

        

        
        private System.Windows.Forms.ToolTip toolTip;
        private Telerik.WinControls.UI.RadComboBox UserName;
        private Telerik.WinControls.UI.RadButton ExitButton;
        private Telerik.WinControls.UI.RadButton LoginButton;
        private Telerik.WinControls.UI.RadTextBox Password;
        private Telerik.WinControls.UI.RadLabel labelX2;
        private Telerik.WinControls.UI.RadLabel labelX1;
        private System.Windows.Forms.PictureBox pictureBox1;

        #endregion
        private Telerik.WinControls.UI.RadContextMenu radContextMenu1;
        private PictureBox pictureBox2;
        
        
        
    }
}