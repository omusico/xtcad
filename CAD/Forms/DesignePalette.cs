using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using com.ccepc.entities;
using com.ccepc.utils;
using DNA;

namespace CAD
{
    public partial class DesignePalette : UserControl
    {
        public DesignePalette()
        {
            InitializeComponent();
            UpdateInstrumentDeviceTree();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            UpdateInstrumentDeviceTree();
        }

        public void UpdateInstrumentDeviceTree()
        {
            this.InstrumentDeviceTree.Nodes.Clear();
            List<Node> subProjects = CADServiceImpl.getSubProjectNodes(AppInitialization.loginUser.id.ToString());
            if(subProjects.Count() > 0)
            {
                foreach (var subProject in subProjects)
                {
                    RadTreeNode subProjectNode = new RadTreeNode();
                    subProjectNode.Text = subProject.label;
                    subProjectNode.Tag = subProject;
                    subProjectNode.ExpandAll();
                    this.InstrumentDeviceTree.Nodes.Add(subProjectNode);
                    if(subProject.children.Count() > 0)
                    {
                        foreach (var disciplineConfig in subProject.children)
                        {
                            RadTreeNode disciplineConfigNode = new RadTreeNode();
                            disciplineConfigNode.Text = disciplineConfig.label;
                            disciplineConfigNode.Tag = disciplineConfig;
                            disciplineConfigNode.ExpandAll();
                            subProjectNode.Nodes.Add(disciplineConfigNode);
                            if(disciplineConfig.children.Count() > 0)
                            {
                                foreach (var designerConfig in disciplineConfig.children)
                                {
                                    RadTreeNode designerConfigNode = new RadTreeNode();
                                    designerConfigNode.Text = designerConfig.label;
                                    designerConfigNode.Tag = designerConfig;
                                    designerConfigNode.ExpandAll();
                                    disciplineConfigNode.Nodes.Add(designerConfigNode);
                                    if(designerConfig.children.Count() > 0)
                                    {
                                        foreach (var checkManConfig in designerConfig.children)
                                        {
                                            RadTreeNode checkManConfigNode = new RadTreeNode();
                                            checkManConfigNode.Text = checkManConfig.label;
                                            checkManConfigNode.Tag = checkManConfig;
                                            checkManConfigNode.ExpandAll();
                                            designerConfigNode.Nodes.Add(checkManConfigNode);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
