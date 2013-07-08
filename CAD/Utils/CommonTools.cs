using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using System.Collections;
using DNA;
using Autodesk.AutoCAD.ApplicationServices;
using System.Diagnostics;

namespace CAD
{
    public class CommonTools
    {
        public static string RunCmd(string command, int milliseconds)
        {            
            
            Process p = new Process();
            string res = string.Empty;

            p.StartInfo.FileName = "cmd.exe";          
            p.StartInfo.Arguments = "/c " + command;    
            p.StartInfo.UseShellExecute = false;        
            p.StartInfo.RedirectStandardInput = true;   
            p.StartInfo.RedirectStandardOutput = true;  
            p.StartInfo.RedirectStandardError = true;   
            p.StartInfo.CreateNoWindow = true;          
            try
            {
                if (p.Start())       //开始进程
                {
                    if (milliseconds == 0)
                        p.WaitForExit();     //这里无限等待进程结束
                    else
                        p.WaitForExit(milliseconds);  //这里等待进程结束，等待时间为指定的毫秒     
                    res = p.StandardOutput.ReadToEnd();
                }
            }
            catch
            {
            }
            finally
            {
                if (p != null)
                    p.Close();
                //KillProcess(p);
            }
                        
            return res;      
        }



        public static Document OpenDoc(string path)
        {
            Document doc = null;
            //如果指定文件名的文件存在，则
            if (System.IO.File.Exists(path))
            {
                DocumentCollection docs = AcadApp.DocumentManager;
                //打开所选择的Dwg文件
                try
                {
                    doc = docs.Open(path, false);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            return doc;
        }

        /// <summary>
        /// 保存并关闭文档
        /// </summary>
        /// <param name="dwgDoc"></param>
        /// <param name="IsSave">是否保存</param> 
        public static void Close(Document dwgDoc, bool IsSave)
        {
            
            try
            {
                DocumentLock dockLock = dwgDoc.LockDocument();
                dockLock.Dispose();
                if (dwgDoc.CommandInProgress != "" && dwgDoc.CommandInProgress != "CD")
                {
                    dwgDoc.SendStringToExecute("\x03\x03", true, false, false);
                }

                if (dwgDoc.IsReadOnly)
                {
                    dwgDoc.CloseAndDiscard();
                }
                else
                {
                    // Activate the document, so we can check DBMOD
                    if (AcadApp.DocumentManager.MdiActiveDocument != dwgDoc)
                    {
                        AcadApp.DocumentManager.MdiActiveDocument = dwgDoc;
                    }

                    int isModified = System.Convert.ToInt32(AcadApp.GetSystemVariable("DBMOD"));

                    // No need to save if not modified
                    if (isModified != 0 && IsSave)
                    {
                        // This may create documents in strange places                       
                        dwgDoc.CloseAndSave(dwgDoc.Name);

                    }
                    else
                    {
                        dwgDoc.CloseAndDiscard();
                    }

                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("->Close:" + ex.Message);
            }
        } 


        public static IEnumerable<AttributeDefinition> GetAllAttributesInBlock(BlockTableRecord btr)
        {
            List<AttributeDefinition> list = new List<AttributeDefinition>();
            ObjectIdCollection ents = Tools.CollectBlockEnts(btr);
            Database db = HostApplicationServices.WorkingDatabase;
            DocumentLock doclock = AcadApp.DocumentManager.MdiActiveDocument.LockDocument();
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                foreach (ObjectId ent in ents)
                {
                    Entity entity = (Entity)trans.GetObject(ent, OpenMode.ForWrite);
                    if (entity is AttributeDefinition)
                    {
                        AttributeDefinition attribute = entity as AttributeDefinition;
                        list.Add(attribute);
                    }
                }
                trans.Commit();
            }
            doclock.Dispose();
            return list;
        }

        public static AttributeDefinition GetAttributeInBlock(BlockTableRecord btr, string attname)
        {
            AttributeDefinition att = null;
            ObjectIdCollection ents = Tools.CollectBlockEnts(btr);
            Database db = HostApplicationServices.WorkingDatabase;
            DocumentLock doclock = AcadApp.DocumentManager.MdiActiveDocument.LockDocument();
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                foreach (ObjectId ent in ents)
                {
                    Entity entity = (Entity)trans.GetObject(ent, OpenMode.ForWrite);
                    if (entity is AttributeDefinition)
                    {
                        AttributeDefinition attribute = entity as AttributeDefinition;
                        if (attribute.Tag == attname)
                        {
                            att = attribute;
                        }
                    }
                }
                trans.Commit();
            }
            doclock.Dispose();
            return att;
        }

        public static Dictionary<string, string> IsCSCADEntity(ObjectId id)
        {
            BlockReference bref = Tools.GetEntity(id) as BlockReference;
            if (bref != null)
            {
                XData data = new XData(bref.ObjectId);
                if (data.HasXData())
                {
                    ArrayList regappname = new ArrayList();
                    IEnumerator appname = data.GetAppNames().GetEnumerator();
                    while (appname.MoveNext())
                    {
                        regappname.Add(appname.Current);
                    }
                    if (!regappname.Contains("CSCAD"))
                    {
                        return null;
                    }
                    Dictionary<string, string> blockinfodic = data.GetParamsWithAppName("CSCAD");
                    return blockinfodic;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static bool IsOnCurrentDwg(string blocktype, int id)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                var blockrefs = db.GetAllEntitiesInModelSpace<BlockReference>(trans, OpenMode.ForRead).Where(o => CheckBlockReference(o,blocktype,id) == 0);
                if (blockrefs != null)
                {
                    if (blockrefs.Count() >= 1)
                    {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else { 
                    return false;
                }
            }
        }

        private static int CheckBlockReference(BlockReference bf, string blocktype, int id)
        {
            if (bf != null)
            {
                XData data = new XData(bf.ObjectId);
                if (data.HasXData())
                {
                    ArrayList regappname = new ArrayList();
                    IEnumerator appname = data.GetAppNames().GetEnumerator();
                    while (appname.MoveNext())
                    {
                        regappname.Add(appname.Current);
                    }
                    if (!regappname.Contains("CSCAD"))
                    {
                        return -1;
                    }
                    Dictionary<string, string> blockinfodic = data.GetParamsWithAppName("CSCAD");
                    if (blockinfodic == null)
                    {
                        return -1;
                    }
                    int ide = int.Parse(blockinfodic["ID"].ToString());
                    if (blockinfodic["块类型"]==blocktype && ide == id)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

    }
}
