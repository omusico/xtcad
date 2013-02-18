using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using com.ccepc.utils;
using com.ccepc.entities;
using DNA;


namespace CAD
{
    public partial class LoginForm : Telerik.WinControls.UI.RadForm
    {
        private string loginname;
        private string loginpwd;
        private bool isEmpty = false;

        public LoginForm()
        {
            InitializeComponent();
            this.UserName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.UserName.Items.Clear();
            string[] args = new string[1];
            args[0] = "测试程序";
            object result = WebServiceHelper.InvokeWebService("HelloWebservice", "getName", args);
            List<User> users = JsonHelper.JsonDeserialize<List<User>>(result.ToString());
            foreach (var user in users)
            {
                this.UserName.ComboBoxElement.Items.Add(new RadComboBoxItem(user.userName, user));
            }
            LoadHistroy();
        }

        private void Logon()
        {
            isEmpty = UserInputCheck();

            if (isEmpty == true)
            {
                string[] args = new string[2];
                args[0] = this.UserName.Text;
                args[1] = this.Password.Text;
                object result = WebServiceHelper.InvokeWebService("UserWebservice", "login", args);
                if (result != null)
                {
                    User u = JsonHelper.JsonDeserialize<User>(result.ToString());
                    if (u != null)
                    {
                        AppInitialization.loginUser = u;
                        AppInitialization.userInfoPanel.Text = "登陆用户:" + u.realName;
                        SaveHistroy();
                        Tools.WriteMessageWithReturn(u.realName + "登陆成功！");
                        this.Close();
                    }
                    else
                    {
                        RadMessageBox.Show(this, "登陆失败：用户名或密码错误,请重新输入!", "登陆提示", MessageBoxButtons.OK, RadMessageIcon.Error);
                        this.UserName.Text = "";
                        this.Password.Text = "";
                        this.UserName.Focus();
                    }
                }
                else
                {
                    RadMessageBox.Show(this, "登陆失败：用户名或密码错误,请重新输入!", "登陆提示", MessageBoxButtons.OK, RadMessageIcon.Error);
                    this.UserName.Text = "";
                    this.Password.Text = "";
                    this.UserName.Focus();
                }
            }

        }


        /// <summary>
        /// 用户输入验证
        /// </summary>
        /// <returns></returns>
        private bool UserInputCheck()
        {
            // 保存登录名称
            loginname = this.UserName.Text.Trim();
            // 保存用户密码
            loginpwd = this.Password.Text.Trim();

            // 开始验证
            if (string.IsNullOrEmpty(loginname))
            {
                this.toolTip.ToolTipIcon = ToolTipIcon.Info;
                this.toolTip.ToolTipTitle = "登录提示";
                Point showLocation = new Point(
                    this.UserName.Location.X + this.UserName.Width,
                    this.UserName.Location.Y);
                this.toolTip.Show("请您输入登录账户名！", this, showLocation, 5000);
                this.UserName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(loginpwd))
            {

                this.toolTip.ToolTipIcon = ToolTipIcon.Info;
                this.toolTip.ToolTipTitle = "登录提示";
                Point showLocation = new Point(
                    this.Password.Location.X + this.Password.Width,
                    this.Password.Location.Y);
                this.toolTip.Show("请您输入登陆账户密码！", this, showLocation, 5000);
                this.Password.Focus();
                return false;
            }
            //else if (loginpwd.Length < 6)
            //{
            //    this.toolTip.ToolTipIcon = ToolTipIcon.Warning;
            //    this.toolTip.ToolTipTitle = "登录警告";
            //    Point showLocation = new Point(
            //        this.Password.Location.X + this.Password.Width,
            //        this.Password.Location.Y);
            //    this.toolTip.Show("用户密码长度不能小于六位！", this, showLocation, 5000);
            //    this.Password.Focus();
            //    return false;
            //}

            // 如果已通过以上所有验证则返回真
            return true;
        }


        private void LoginButtonClick(object sender, EventArgs e)
        {
            Logon();
        }

        private void ExitButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UserName_KeyDown(object sender, KeyEventArgs e)
        {
            //判断用户是否按了回车
            if (e.KeyValue == 13)
            {
                Logon();
            }
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            //判断用户是否按了回车
            if (e.KeyValue == 13)
            {
                Logon();
            }
        }

        // 写登陆成功的用户名
        private void SaveHistroy()
        {
            string fileName = Path.Combine(Application.StartupPath, @"History.txt");
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs, Encoding.Default);
            writer.WriteLine(loginname);
            writer.Flush();
            writer.Close();
            //List<string> userlist = this.UserName.Items.Select(o => o.Text).ToList();
            //if (!userlist.Contains(loginname))
            //{
            //    string fileName = Path.Combine(Application.StartupPath, @"History.txt");

            //    StreamWriter writer = new StreamWriter(fileName, false, Encoding.Default);
            //    foreach (var it in UserName.Items)
            //    {
            //        writer.WriteLine(it.Text);

            //    }
            //    writer.WriteLine(loginname); 
            //    writer.Flush();
            //    writer.Close();
            //}
        }

        // 读登陆成功的用户名
        private void LoadHistroy()
        {
            string fileName = Path.Combine(Application.StartupPath, @"History.txt");
            StreamReader reader = new StreamReader(fileName, Encoding.Default);
            string name = reader.ReadLine();
            this.UserName.SelectedText = name;
            reader.Close();
            //if (File.Exists(fileName))
            //{
            //    StreamReader reader = new StreamReader(fileName, Encoding.Default);
            //    string name = reader.ReadLine();
            //    while (name!=null)
            //    {
            //        if (!string.IsNullOrEmpty(name))
            //        {
            //            RadListDataItem item = new RadListDataItem(name);
            //            this.UserName.Items.Add(item);
            //        }
            //        name = reader.ReadLine();
            //    }
            //    reader.Close();
            //}

        }
    }
}
