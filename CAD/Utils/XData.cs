using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using DNA;

/*Copyright © CHINA 2008

AutoCAD version:AutoCAD 2006
Description:  

To use XDataCore.dll:

1. Start AutoCAD and open a new drawing.
2. Type netload and select XDataCore.dll.
3. Execute the Test command.*/


namespace CAD
{
    public class XData
    {
        public ObjectId Id
        {
            get
            {
                return this.id;
            }
        }

        public XData(ObjectId id)
        {
            this.id = id;
            this.Initial();
        }

        public bool HasXData()//判断实体是否有扩展数据
        {
            //Entity ent = Tools.GetEntity(this.id);
            //ResultBuffer buffer = ent.XData;

            //if (buffer == null)
            //{
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}

            if (appName2values == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //扩展数据是否更新完毕
        public bool IsXDataUpdate
        {
            get
            {
                return !isChanged;
            }
        }

        //这里存在一个小问题
        //如果appname已经存在，且有相同的param数据，该如何处理
        //这里只是在原有数据的基础上添加，而不考虑重复问题
        //如果将来出现问题，可以考虑将该appname对应的扩展数据先清空，然后再添加数据
        public void CreateXData(string appName, string param, string value)//根据appname创建一个新的xdata，和HasXData一起配合使用
        {
            //clearXData(appName);//清空appname对应的扩展数据--待定
            //-----------------------------------------------
            //ResultBuffer buffer = new ResultBuffer();

            //buffer.Add(new TypedValue((int)DxfCode.ExtendedDataRegAppName, appName));
            //buffer.Add(new TypedValue((int)DxfCode.ExtendedDataAsciiString, param));
            //buffer.Add(new TypedValue((int)DxfCode.ExtendedDataAsciiString, value));
            //id.SetXData(appName, buffer);

            //Initial();
            //-----------------------------------------
            //if (appName2values.ContainsKey(param))
            //{

            //}
            //else
            //{
            //    Dictionary<string, string> dic = new Dictionary<string, string>();
            //    dic.Add(param, value);

            //    appName2values.Add(param, dic);
            //    isChanged = true;
            //}

            //----------------------------
            if (appName == null || param == null)
            {
                return;
            }

            if (appName2values == null)
            {
                appName2values = new Dictionary<string, Dictionary<string, string>>();
            }

            if (appName2values.ContainsKey(appName))
            {
                if (!appName2values[appName].ContainsKey(param))
                {
                    appName2values[appName].Add(param, value);
                    isChanged = true;
                }
            }
            else
            {
                appName2values.Add(appName, new Dictionary<string, string>());
                appName2values[appName].Add(param, value);
                isChanged = true;
            }
        }

        public void CreateXData(string appName, Dictionary<string, string> dic)//根据字典添加扩展数据值对，如果有参数名重复的，则不添加到原有的数据中 
        {
            if (appName2values == null)
            {
                appName2values = new Dictionary<string, Dictionary<string, string>>();
            }

            if (dic == null || dic.Count == 0 || appName == null)
            {
                return;
            }

            string[] keys = new string[appName2values.Keys.Count];
            appName2values.Keys.CopyTo(keys, 0);

            foreach (string str in keys)
            {
                if (dic.ContainsKey(str))
                {
                    dic.Remove(str);
                }
            }

            if (!appName2values.ContainsKey(appName))
            {
                appName2values.Add(appName, new Dictionary<string, string>());
            }

            foreach (KeyValuePair<string, string> kp in dic)
            {
                appName2values[appName].Add(kp.Key, kp.Value);
            }

            isChanged = true;
        }

        public void UpdateXData()//更新扩展数据
        {
            if (isChanged)
            {
                //下面2行代码锁住文档
                DocumentLock docLock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument();
                Document doc = Application.DocumentManager.MdiActiveDocument;
                Transaction tr = doc.TransactionManager.StartTransaction();
                using (tr)
                {
                    DBObject obj = tr.GetObject(this.id, OpenMode.ForWrite);
                    ResultBuffer rb = new ResultBuffer();
                    foreach (KeyValuePair<string, Dictionary<string, string>> kp in this.appName2values)
                    {
                        rb.Add(new TypedValue((int)DxfCode.ExtendedDataRegAppName, kp.Key));
                        foreach (KeyValuePair<string, string> pair in kp.Value)
                        {
                            rb.Add(new TypedValue((int)DxfCode.ExtendedDataAsciiString, pair.Key));
                            rb.Add(new TypedValue((int)DxfCode.ExtendedDataAsciiString, pair.Value));
                        }

                        obj.XData = rb;//替换对应appname的扩展数据
                        rb = new ResultBuffer();
                    }

                    rb.Dispose();
                    tr.Commit();
                }
                isChanged = false;

                docLock.Dispose();
            }
        }

        public void ClearXData(string appName)//根据appname删除扩展数据
        {
            if (appName2values!=null)
            {
                if (IsAppExist(appName))
                {
                    if (this.appName2values.ContainsKey(appName))
                    {
                        //Document doc = Application.DocumentManager.MdiActiveDocument;

                        //Transaction tr = doc.TransactionManager.StartTransaction();
                        //using (tr)
                        //{
                        //    DBObject obj = tr.GetObject(this.id, OpenMode.ForWrite);
                        //    ResultBuffer rb = new ResultBuffer();
                        //    rb.Add(new TypedValue((int)DxfCode.ExtendedDataRegAppName, appName));

                        //    obj.XData = rb;

                        //    rb.Dispose();
                        //    tr.Commit();
                        //}

                        ////ResultBuffer rb = new ResultBuffer();
                        ////rb.Add(new TypedValue((int)DxfCode.ExtendedDataRegAppName, appName));
                        ////ReplaceXData(rb);

                        ////isChanged = false;//防止调用update更新

                        //另外的一种方法
                        appName2values[appName].Clear();
                        //appName2values.Remove(appName);
                        isChanged = true;

                        UpdateXData();
                        appName2values.Remove(appName);
                    }
                }
            }
        }

        public void ClearAllXData()//删除所有的扩展数据
        {
            //Document doc = Application.DocumentManager.MdiActiveDocument;

            //Transaction tr = doc.TransactionManager.StartTransaction();
            //using (tr)
            //{
            //    DBObject obj = tr.GetObject(this.id, OpenMode.ForWrite);
            //    ResultBuffer rb = new ResultBuffer();
            //    foreach (KeyValuePair<string, Dictionary<string, string>> kp in this.appName2values)
            //    {
            //        rb.Add(new TypedValue((int)DxfCode.ExtendedDataRegAppName, kp.Key));
            //        obj.XData = rb;
            //        rb = new ResultBuffer();
            //    }

            //    isChanged = false;//防止update更新

            //    rb.Dispose();
            //    tr.Commit();
            //}            
            if (appName2values.Count != 0)
            {
                string[] keys = new string[appName2values.Keys.Count];
                appName2values.Keys.CopyTo(keys, 0);

                foreach (string str in keys)
                {
                    appName2values[str].Clear();
                    //appName2values.Remove(str);
                }

                isChanged = true;
            }
        }

        public bool AddXData(string param, string value)//添加参数--值对
        {
            bool isSuccesful = true;
            if (appName2values[currentAppName].ContainsKey(param))
            {
                isSuccesful = false;
                Tools.WriteMessage("\n该参数已经存在\n");
            }
            else
            {
                isSuccesful = true;
                appName2values[currentAppName].Add(param, value);
                isChanged = true;
            }

            return isSuccesful;

        }

        public bool DeleteXData(string param)//删除扩展数据
        {
            bool isSuccesful = true;

            if (appName2values[currentAppName].ContainsKey(param))
            {
                isSuccesful = true;
                appName2values[currentAppName].Remove(param);
                isChanged = true;
            }
            else
            {
                isSuccesful = false;
            }

            return isSuccesful;
        }

        public bool InsertXData(string beforParam, string param, string value)
        {
            bool isSuccesful = true;
            if (beforParam == param)
            {
                isSuccesful = false;
            }
            else if (appName2values[currentAppName].ContainsKey(param))
            {
                isSuccesful = false;
            }
            else
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                foreach (KeyValuePair<string, string> kp in appName2values[currentAppName])
                {
                    dic.Add(kp.Key, kp.Value);
                    if (kp.Key == beforParam)
                    {
                        dic.Add(param, value);
                    }
                }

                //ResultBuffer rb = BuildResultBuffer(currentAppName, dic);//构造buffer
                //ReplaceXData(rb);//直接更新

                //在这里存在更新问题，
                //是在appname2values更新后再update
                //还是直接更新？
                appName2values[currentAppName] = dic;
                isSuccesful = true;
                isChanged = true;
            }

            return isSuccesful;
        }

        public bool ModifyXData(string param, string value)//修改参数--值对
        {
            bool isSuccesful = true;
            if (appName2values[currentAppName].ContainsKey(param))
            {
                appName2values[currentAppName][param] = value;
                isSuccesful = true;
                isChanged = true;
            }
            else
            {
                isSuccesful = false;
                Tools.WriteMessage("\n该参数不存在\n");
            }

            return isSuccesful;
        }

        public bool ModifyParam(string originParam, string resultParam)//修改参数名称
        {
            bool isSuccesful = true;
            bool isFind = false;

            //另外的一种在foreach中修改集合
            Dictionary<string, string> dic = this.appName2values[this.currentAppName];
            //string[] keys = new string[dic.Keys.Count];
            //dic.Keys.CopyTo(keys, 0);

            Dictionary<string, string> dicTmp = new Dictionary<string, string>();
            //foreach (string key in keys)
            //{
            //}

            foreach (KeyValuePair<string, string> kp in this.appName2values[currentAppName])
            {
                if (originParam == kp.Key || originParam == null)
                {
                    isFind = true;
                    dicTmp.Add(resultParam, kp.Value);
                }
                else
                {
                    isFind = false;
                    dicTmp.Add(kp.Key, kp.Value);
                }
            }

            if (!isFind)
            {
                isSuccesful = false;
                dicTmp.Clear();
            }
            else
            {
                this.appName2values[currentAppName] = dicTmp;
                isSuccesful = true;
                isChanged = true;
                dic.Clear();
            }

            return isSuccesful;
        }

        public string GetXData(string param)
        {
            string value = null;
            if (appName2values[currentAppName].ContainsKey(param))
            {
                value = appName2values[currentAppName][param];
            }
            else
            {
                Tools.WriteMessage("\n该参数不存在\n");
            }

            return value;
        }


        public void PrintXDataList()//本方法只考虑扩展数据配对的情况，即在除掉appname之后，扩展数据应为偶数个
        {

            if (appName2values == null)
            {
                Tools.WriteMessage("\n该实体不存在扩展数据\n");
            }
            else
            {
                Tools.WriteMessage("\n*-----------*--------------^-------------*-----------*\n");
                foreach (KeyValuePair<string, Dictionary<string, string>> kp in this.appName2values)
                {
                    Tools.WriteMessage(string.Format("\n****扩展数据注册应用程序名称：{0}\n", kp.Key));
                    foreach (KeyValuePair<string, string> pair in kp.Value)
                    {
                        Tools.WriteMessage(string.Format("\n参数={0}, 值={1}\n", pair.Key, pair.Value));
                    }
                }
            }
        }

        public void PrintXData(string appName)
        {
            if (IsAppExist(appName))
            {
                Tools.WriteMessage(string.Format("\n****扩展数据注册应用程序名称：{0}\n", appName));
                foreach (KeyValuePair<string, string> pair in appName2values[appName])
                {
                    Tools.WriteMessage(string.Format("\n参数={0}, 值={1}\n", pair.Key, pair.Value));
                }
            }
            else
            {
                Tools.WriteMessage("\n不存在该注册应用程序名称\n");
            }
        }

        public void setCurrentAppName(string appName)
        {
            this.currentAppName = appName;//前提假设该appname已经存在，因此必须配合registerappp 或者 isappexist一起使用
        }

        public string getCurrentAppName()
        {
            return this.currentAppName;
        }

        public static void RegisterApp(string appName)//注册应用程序名称
        {
            DocumentLock docLock = Autodesk.AutoCAD.ApplicationServices.Application.

              DocumentManager.MdiActiveDocument.LockDocument();

            using (Transaction trans = Application.DocumentManager.MdiActiveDocument.TransactionManager.StartTransaction())
            {

                Database workingdatabase = HostApplicationServices.WorkingDatabase;

                RegAppTable appTbl = trans.GetObject(workingdatabase.RegAppTableId, OpenMode.ForWrite) as RegAppTable;

                if (!appTbl.Has(appName))
                {
                    RegAppTableRecord appTblRcd = new RegAppTableRecord();
                    appTblRcd.Name = appName;
                    appTbl.Add(appTblRcd);

                    trans.AddNewlyCreatedDBObject(appTblRcd, true);
                }

                trans.Commit();
            }

            docLock.Dispose();

        }

        //判断应用程序名称是否存在
        public static bool IsAppExist(string appName)
        {
            bool isExist = true;
            using (Transaction trans = Application.DocumentManager.MdiActiveDocument.TransactionManager.StartTransaction())
            {

                Database workingdatabase = HostApplicationServices.WorkingDatabase;
                SymbolTable table = (SymbolTable)trans.GetObject(workingdatabase.RegAppTableId,
                    Autodesk.AutoCAD.DatabaseServices.OpenMode.ForRead, false);
                if (!table.Has(appName))
                {
                    isExist = false;
                }
                else
                {
                    isExist = true;
                }
                trans.Commit();
            }

            return isExist;
        }


        //打包注册应用程序及其XData
        private ResultBuffer BuildResultBuffer(string appName, Dictionary<string, string> dic)
        {
            if (dic.Count == 0)
            {
                return null;
            }
            else
            {
                ResultBuffer rb = new ResultBuffer();
                rb.Add(new TypedValue((int)DxfCode.ExtendedDataRegAppName, appName));

                foreach (KeyValuePair<string, string> kp in dic)
                {
                    rb.Add(new TypedValue((int)DxfCode.ExtendedDataAsciiString, kp.Key));
                    rb.Add(new TypedValue((int)DxfCode.ExtendedDataAsciiString, kp.Value));
                }

                return rb;
            }
        }

        //更新XData
        private void ReplaceXData(ResultBuffer rb)
        {
            if (rb != null)
            {
                Document doc = Application.DocumentManager.MdiActiveDocument;
                Transaction tr = doc.TransactionManager.StartTransaction();
                using (tr)
                {
                    DBObject obj = tr.GetObject(this.id, OpenMode.ForWrite);
                    string appName = rb.AsArray()[0].Value.ToString();
                    if (appName2values.ContainsKey(appName))
                    {
                        obj.XData = rb;
                    }
                    tr.Commit();
                }
            }
        }


        //初始化
        private void Initial()
        {
            Entity ent = Tools.GetEntity(this.id);
            ResultBuffer buffer = ent.XData;
            if (buffer != null)
            {
                this.currentAppName = buffer.AsArray()[0].Value.ToString();//将第一个appname设置为当前
                TypedValue[] bufferArray = buffer.AsArray();
                ArrayList pos = new ArrayList();
                ScanBufferArray(bufferArray, ref pos); //扫描扩展数据，得到appname的位置集合              
                string lastAppName = null;
                appName2values = new Dictionary<string, Dictionary<string, string>>();
                Dictionary<string, string> ht = new Dictionary<string, string>();
                int start = 0, end = 0;

                int currentAppPos = 0;
                while (currentAppPos <= pos.Count - 1)
                {
                    lastAppName = (string)bufferArray[(int)pos[currentAppPos]].Value;

                    if (currentAppPos == pos.Count - 1)
                    {
                        start = (int)pos[currentAppPos];
                        end = bufferArray.Length;
                    }
                    else
                    {
                        start = (int)pos[currentAppPos];
                        end = (int)pos[currentAppPos + 1];
                    }

                    ht = BuildPairs(bufferArray, start, end);
                    appName2values.Add(lastAppName, ht);
                    ht = new Dictionary<string, string>();
                    currentAppPos++;
                }

            }
            else
            {
                this.appName2values = null;
                this.currentAppName = null;
                this.isChanged = false;
            }

        }

        //扫描返回注册的应用程序链表
        private void ScanBufferArray(TypedValue[] bufferArray, ref ArrayList pos)
        {
            pos = new ArrayList();

            for (int i = 0; i < bufferArray.Length; i++)
            {
                if (bufferArray[i].TypeCode == (int)DxfCode.ExtendedDataRegAppName)
                {
                    pos.Add(i);
                }
            }
        }


        //用字符串数组打包XData字典
        private Dictionary<string, string> BuildPairs(TypedValue[] bufferArray, int start, int end)
        {
            Dictionary<string, string> ht = new Dictionary<string, string>();
            int pairCount = end - start - 1;
            if (pairCount == 1)
            {
                ht.Add(bufferArray[start + 1].Value.ToString(), "null");//将null改变为"null"，不知道是否合适?
            }
            else if (pairCount % 2 == 0)
            {
                for (int i = start + 1; i < end; i += 2)
                {
                    ht.Add(bufferArray[i].Value.ToString(), bufferArray[i + 1].Value.ToString());
                }

            }
            else if (pairCount % 2 != 0)
            {
                int i = 0;//初始化
                for (i = start + 1; i < end - 1; i += 2)
                {
                    ht.Add(bufferArray[i].Value.ToString(), bufferArray[i + 1].Value.ToString());
                }

                ht.Add(bufferArray[i].Value.ToString(), "null");
            }

            return ht;
        }

        //返回所有注册的应用程序名
        public ICollection GetAppNames()
        {
            return appName2values.Keys;
        }


        //根据注册应用程序名查找相应的XData字典
        public Dictionary<string, string> GetParamsWithAppName(string appName)
        {
            if (appName2values.ContainsKey(appName))
            {
                return (Dictionary<string, string>)appName2values[appName];
            }
            else
            {
                return null;
            }
        }

        private ObjectId id;                                                           //实体ID
        private string currentAppName = null;                                          //当前注册应用程序名
        private Dictionary<string, Dictionary<string, string>> appName2values = null;  //XData数据字典
        private bool isChanged = false;                                                //是否改变
    }

}
