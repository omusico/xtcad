using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using System.Reflection;
using System.IO;
using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace CAD
{
    public static class G
    {

        public static bool AlmostZero(double d)
        {
            return Equal(d, 0);
        }

        public static double Degree2Radian(double degree)
        {
            return degree * Math.PI / 180;
        }

        public static bool Equal(double a, double b)
        {
            return (Math.Abs(a - b) < 1E-6);
        }

        public static void InvalidDocument()
        {
            sDoc = null;
            sEd = null;
            sDb = null;
        }

        public static string LoadStringFromDwg(string key, Transaction tr)
        {
            return TransHelper.TransResult<string, string>(tr, pTransLoadStringFromDwg, key);
        }

        public static void Log(string msg)
        {
            Ed.WriteMessage(msg);
        }

        public static int Rand(int i)
        {

            if (sRand == null)
                sRand = new Random();
            return sRand.Next() % i;
        }

        public static bool SaveStringToDwg(string key, string value, Transaction tr)
        {
            return TransHelper.TransResult<string, string, bool>(tr, pTransSaveStringToDwg, key, value);
        }

        public static Database Db
        {
            get
            {

                if (sDb == null && Doc != null)
                    sDb = Doc.Database;
                return sDb;
            }
        }

        public static string DllPath
        {
            get
            {

                if (string.IsNullOrEmpty(sDllPath))
                {
                    sDllPath = Assembly.GetAssembly(typeof(G)).Location;
                    sDllPath = Path.GetDirectoryName(sDllPath);
                }
                return sDllPath;
            }
        }

        public static Document Doc
        {
            get
            {

                if (sDoc == null)
                    sDoc = AcadApp.DocumentManager.MdiActiveDocument;
                return sDoc;
            }
        }

        public static Editor Ed
        {
            get
            {

                if (sEd == null && Doc != null)
                    sEd = Doc.Editor;
                return sEd;
            }
        }

        public static string Connectionstring
        {
            get
            {
                return connectionstring;
            }
        }
        private static string sAppKey = "CSCAD";
        private static Database sDb;
        private static string sDllPath;

        private static Document sDoc;
        private static Editor sEd;
        private static Random sRand;

        private static string connectionstring = "server=localhost;user id=root;password=hlzhu;database=cscad";

        private static ObjectId pGetDict(bool createIfNotExisting, Transaction tr)
        {
            return TransHelper.TransResult<bool, ObjectId>(tr, pTransGetDict, createIfNotExisting);
        }

        private static ObjectId pTransGetDict(Transaction tr, bool createIfNotExisting)
        {
            ObjectId dictId;
            DBDictionary namedObjects = (DBDictionary)tr.GetObject(Db.NamedObjectsDictionaryId,
                OpenMode.ForRead);

            if (!namedObjects.Contains(AppKey))
            {
                if (!createIfNotExisting)
                    return ObjectId.Null;
                DBDictionary dict = new DBDictionary();
                namedObjects.UpgradeOpen();
                dictId = namedObjects.SetAt(AppKey, dict);
                tr.AddNewlyCreatedDBObject(dict, true);
            }
            else
            {
                dictId = namedObjects.GetAt(AppKey);
            }
            return dictId;
        }

        private static string pTransLoadStringFromDwg(Transaction tr, string key)
        {
            ObjectId dictId = pGetDict(false, tr);

            if (dictId.IsNull)
                return null;

            DBDictionary dict = tr.GetObject(dictId, OpenMode.ForRead) as DBDictionary;

            if (dict == null)
                return null;

            if (dict.Contains(key))
            {
                DBObject obj = tr.GetObject(dict.GetAt(key), OpenMode.ForRead);
                Xrecord xrec = obj as Xrecord;

                if (xrec == null)
                    return null;
                ResultBuffer rb = xrec.Data;

                foreach (TypedValue tv in rb)
                {
                    return tv.Value as string;
                }
            }
            return null;
        }

        private static bool pTransSaveStringToDwg(Transaction tr, string key, string value)
        {
            DBDictionary dict = tr.GetObject(pGetDict(true, tr), OpenMode.ForWrite) as DBDictionary;

            if (dict == null)
                return false;
            Xrecord xrec;
            bool newXrec = false;

            if (dict.Contains(key))
            {
                DBObject obj = tr.GetObject(dict.GetAt(key), OpenMode.ForWrite);
                xrec = obj as Xrecord;

                if (xrec == null)
                {
                    obj.Erase();
                    xrec = new Xrecord();
                    newXrec = true;
                }
            }

            else
            {
                xrec = new Xrecord();
                newXrec = true;
            }
            xrec.XlateReferences = true;
            xrec.Data = new ResultBuffer(new TypedValue((int)DxfCode.Text, value));

            if (newXrec)
            {
                dict.SetAt(key, xrec);
                tr.AddNewlyCreatedDBObject(xrec, true);
            }
            return true;
        }
        private static string AppKey { get { return sAppKey; } }
    }
}
