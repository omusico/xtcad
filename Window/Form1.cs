using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using com.ccepc.utils;

namespace Window
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            DateTime t = DateTime.Now;
            string s = JsonHelper.JsonSerializer<DateTime>(t);
            MessageBox.Show(s);
        }
    }
}
