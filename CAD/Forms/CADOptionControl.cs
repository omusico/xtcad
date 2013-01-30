using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CAD
{
    public partial class CADOptionControl : UserControl
    {
        private IniFile optionFile;
        public CADOptionControl()
        {
            InitializeComponent();
            optionFile = IniFile.getIniFile(@"options.ini");
            this.serverPath.Text = optionFile.IniReadValue("服务器","地址");
        }

        public void SaveOptions()
        {
            optionFile.IniWriteValue("服务器","地址",this.serverPath.Text);
        }
    }
}
