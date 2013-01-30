using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CAD
{
    public partial class ToolsForm : Telerik.WinControls.UI.RadForm
    {
        public ToolsForm()
        {
            InitializeComponent();
        }

        private void ToolsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void saveFileButton_Click(object sender, EventArgs e)
        {
            Commands.SaveFile();
        }
    }
}
