using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.Interop;
using System.Runtime.InteropServices;
using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace CAD
{
    public class TitleIcon
    {
        #region 宏定义
        public const int IMAGW_ICON = 1;
        public const int LR_LOADFROMFILE = 0x10;

        public const int WM_SETICON = 0x80;

        #endregion

        #region WinAPI定义

        [DllImport("user32", EntryPoint = "LoadImage")]
        public static extern int LoadImageA(int hInst, string lpsz, int un1, int n1, int n2, int un2);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(
            int hWnd, // handle to destination window 
            int Msg, // message 
            int wParam, // first message parameter 
            int lParam // second message parameter 
        );

        [DllImport("user32", EntryPoint = "SetWindowText")]
        public static extern int SetWindowTextA(int hwnd, string lpString);

        #endregion

        #region 更改AutoCAD窗口的标题和图标

        /// <summary>
        /// 更改AutoCAD窗口的图标
        /// </summary>
        public static void SetIcon()
        {
            AcadApplication acadApp = AcadApp.AcadApplication as AcadApplication;
            int AcadHwnd = acadApp.HWND;//获取AutoCAD应用程序的窗口句柄
            string path = acadApp.Path;
            //从文件载入图标(16*16大小)
            string FileName = @"D:\WorkSpace\LCSCAD\CAD\Resources\cabinet.ico";

            if (System.IO.File.Exists(FileName))
            {
                int hIcon = LoadImageA(0, FileName, IMAGW_ICON, 16, 16, LR_LOADFROMFILE);

                if (hIcon != 0)
                {
                    SendMessage(AcadHwnd, WM_SETICON, 0, hIcon);
                }
            }
        }

        /// <summary>
        /// 更改AutoCAD窗口的标题名称
        /// </summary>
        public static void SetTitle(string title)
        {
            AcadApplication acadApp = AcadApp.AcadApplication as AcadApplication;
            int AcadHwnd = acadApp.HWND;//获取AutoCAD应用程序的窗口句柄
            SetWindowTextA(AcadHwnd, title);
        }

        #endregion

    }
}
