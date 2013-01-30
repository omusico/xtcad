using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.DatabaseServices;

namespace CAD
{
    public static class TransHelper
    {

        public static void Trans<T>(Transaction tr, Action<Transaction, T> action, T t)
        {
            Transaction newTr = null;

            if (tr == null)
            {
                newTr = G.Db.TransactionManager.StartTransaction();
                tr = newTr;
            }

            using (newTr)
            {
                action(tr, t);

                if (newTr != null)
                    newTr.Commit();
            }
        }

        public static void Trans<T1, T2, T3>(Transaction tr, Action<Transaction, T1, T2, T3> action,
            T1 t1, T2 t2, T3 t3)
        {
            Transaction newTr = null;

            if (tr == null)
            {
                newTr = G.Db.TransactionManager.StartTransaction();
                tr = newTr;
            }

            using (newTr)
            {
                action(tr, t1, t2, t3);

                if (newTr != null)
                    newTr.Commit();
            }
        }

        public static void Trans<T1, T2>(Transaction tr, Action<Transaction, T1, T2> action,
            T1 t1, T2 t2)
        {
            Transaction newTr = null;

            if (tr == null)
            {
                newTr = G.Db.TransactionManager.StartTransaction();
                tr = newTr;
            }

            using (newTr)
            {
                action(tr, t1, t2);

                if (newTr != null)
                    newTr.Commit();
            }
        }

        public static void Trans(Transaction tr, Action<Transaction> action)
        {
            Transaction newTr = null;

            if (tr == null)
            {
                newTr = G.Db.TransactionManager.StartTransaction();
                tr = newTr;
            }

            using (newTr)
            {
                action(tr);

                if (newTr != null)
                    newTr.Commit();
            }
        }

        public static TResult TransResult<T, TResult>(Transaction tr, Func<Transaction, T, TResult>
            func, T t)
        {
            Transaction newTr = null;

            if (tr == null)
            {
                newTr = G.Db.TransactionManager.StartTransaction();
                tr = newTr;
            }

            using (newTr)
            {
                TResult res = func(tr, t);

                if (newTr != null)
                    newTr.Commit();
                return res;
            }
        }

        public static TResult TransResult<T1, T2, T3, TResult>(Transaction tr, Func<Transaction,
            T1, T2, T3, TResult> func, T1 t1, T2 t2, T3 t3)
        {
            Transaction newTr = null;

            if (tr == null)
            {
                newTr = G.Db.TransactionManager.StartTransaction();
                tr = newTr;
            }

            using (newTr)
            {
                TResult res = func(tr, t1, t2, t3);

                if (newTr != null)
                    newTr.Commit();
                return res;
            }
        }

        public static TResult TransResult<T1, T2, TResult>(Transaction tr, Func<Transaction, T1, T2,
            TResult> func, T1 t1, T2 t2)
        {
            Transaction newTr = null;

            if (tr == null)
            {
                newTr = G.Db.TransactionManager.StartTransaction();
                tr = newTr;
            }

            using (newTr)
            {
                TResult res = func(tr, t1, t2);

                if (newTr != null)
                    newTr.Commit();
                return res;
            }
        }

        public static TResult TransResult<TResult>(Transaction tr, Func<Transaction, TResult> func)
        {
            Transaction newTr = null;

            if (tr == null)
            {
                newTr = G.Db.TransactionManager.StartTransaction();
                tr = newTr;
            }

            using (newTr)
            {
                TResult res = func(tr);

                if (newTr != null)
                    newTr.Commit();
                return res;
            }
        }
    }
}
