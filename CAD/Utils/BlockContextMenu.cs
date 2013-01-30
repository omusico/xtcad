using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.Windows;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using System.Windows.Forms;
using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;
using Telerik.WinControls;
using DNA;

namespace CAD
{
    public class BlockContextMenu
    {
        private static ContextMenuExtension cme;

        public static void AttachMenu() {
            cme = new ContextMenuExtension();
            cme.Title = "图块设置";
            cme.Popup += new EventHandler(cme_Popup);
            Autodesk.AutoCAD.Windows.MenuItem blockmenu = new Autodesk.AutoCAD.Windows.MenuItem("图块设置");
            Autodesk.AutoCAD.Windows.MenuItem pidblock = new Autodesk.AutoCAD.Windows.MenuItem("PID块设置");
            pidblock.Click += new EventHandler(pidblock_Click);
            blockmenu.MenuItems.Add(pidblock);
            cme.MenuItems.Add(blockmenu);
            RXClass rxc = BlockReference.GetClass(typeof(BlockReference));
            Autodesk.AutoCAD.ApplicationServices.Application.AddObjectContextMenuExtension(rxc,cme);
        }

        private static void cme_Popup(object sender, EventArgs e)
        {
            ContextMenuExtension cme = sender as ContextMenuExtension;
            if (cme!=null)
            {
                Document doc = AcadApp.DocumentManager.MdiActiveDocument;
                //if (!doc.Name.Contains(CADOptions.pidpath))
                //{
                //    cme.MenuItems[0].MenuItems[0].Enabled = false;
                //}
            }
        }

        public static void DetachMenu() {
            RXClass rxc = BlockReference.GetClass(typeof(BlockReference));
            Autodesk.AutoCAD.ApplicationServices.Application.RemoveObjectContextMenuExtension(rxc,cme);
        }

        private static void pidblock_Click(object sender, EventArgs e)
        {
            Document doc = AcadApp.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            SelectionSet sset = Tools.Editor.SelectImplied().Value;
            if (sset!=null)
            {
                if (sset.Count <= 1)
                {
                    ObjectId id = sset[0].ObjectId;
                    if (id != ObjectId.Null)
                    {
                        using (Transaction trans = db.TransactionManager.StartTransaction())
                        {
                            BlockReference br = (BlockReference)trans.GetObject(id, OpenMode.ForRead);
                            if (br != null)
                            {
                                BlockTableRecord btr = (BlockTableRecord)trans.GetObject(br.BlockTableRecord, OpenMode.ForRead);
                                if (btr != null)
                                {
                                    //EditBlockForm editpidblock = new EditBlockForm(btr);
                                    //editpidblock.Show();
                                }
                            }

                        }

                    }
                }
                else {
                    RadMessageBox.Show("选择的对象超过一个，请重新选择！");
                }
            }
            
        }
    }
}
