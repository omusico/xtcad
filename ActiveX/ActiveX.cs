using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Autodesk.AutoCAD.Interop;
using System.Diagnostics;

namespace ActiveX
{
    [Guid("3A398EC1-EBAD-474f-9046-19628225EF73")]
    public partial class ActiveX : UserControl,IObjectSafety
    {
        //将窗口显示
        [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
        private static extern int ShowWindow(IntPtr hWnd, int nCmdShow);
        //切换窗体显示
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        
        public ActiveX()
        {
            InitializeComponent();
        }

        public AcadApplication openCAD()
        {
            const string progID = "AutoCAD.Application";
            AcadApplication acApp = null;
            try
            {
                acApp = (AcadApplication)Marshal.GetActiveObject(progID);
            }
            catch
            {
                try
                {
                    Type acType = Type.GetTypeFromProgID(progID);
                    acApp = (AcadApplication)Activator.CreateInstance(acType, true);
                }
                catch
                {
                    MessageBox.Show("Cannot create object of type \"" + progID + "\"");
                }
            }
            return acApp;
        }


        public void downLoadAndOpenFile(string url,string fileName,string fileId)
        {
            AcadApplication acApp = openCAD();
            if (acApp != null)
            {
                acApp.Visible = true;
                String filePath = "c:\\xtcad\\" + fileName;
                FileUtil.SaveFileFromUrl(filePath, url);
                acApp.Documents.Open(filePath, false, null);
                IntPtr appHwd = new IntPtr(acApp.HWND);
                ShowWindow(appHwd, 3);
                SetForegroundWindow(appHwd);
                try
                {
                    acApp.ActiveDocument.SendCommand("AttachFileInfo " + fileId + " ");
                    //acApp.ActiveDocument.SendCommand("test1 11 12 ");
                    //Tools.WriteMessageWithReturn("调用命令之前");
                    //Tools.RunCommand(true, "purge");
                    //Tools.WriteMessageWithReturn("调用命令之后");
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        #region [IObjectSafety implementation]
        private ObjectSafetyOptions m_options =
            ObjectSafetyOptions.INTERFACESAFE_FOR_UNTRUSTED_CALLER |
            ObjectSafetyOptions.INTERFACESAFE_FOR_UNTRUSTED_DATA;

        public long GetInterfaceSafetyOptions(ref Guid iid, out int pdwSupportedOptions, out int pdwEnabledOptions)
        {
            pdwSupportedOptions = (int)m_options;
            pdwEnabledOptions = (int)m_options;
            return 0;
        }

        public long SetInterfaceSafetyOptions(ref Guid iid, int dwOptionSetMask, int dwEnabledOptions)
        {
            return 0;
        }
        #endregion

    //    #region IObjectSafety成员
    //    private const string _IID_IDispatch= "{00020400-0000-0000-C000-000000000046}";
    //    private const string _IID_IDispatchEx= "{a6ef9860-c720-11d0-9337-00a0c90dcaa9}";
    //    private const string _IID_IPersistStorage= "{0000010A-0000-0000-C000-000000000046}";
    //    private const string _IID_IPersistStream= "{00000109-0000-0000-C000-000000000046}";
    //    private const string _IID_IPersistPropertyBag= "{37D84F60-42CB-11CE-8135-00AA004BB851}";
    //    private const int INTERFACESAFE_FOR_UNTRUSTED_CALLER=0x00000001;
    //    private const int INTERFACESAFE_FOR_UNTRUSTED_DATA=0x00000002;
    //    private const int S_OK = 0;
    //    private const int E_FAIL= unchecked((int)0x80004005);
    //    private const int E_NOINTERFACE= unchecked((int)0x80004002);
    //    private bool _fSafeForScripting= true;
    //    private bool _fSafeForInitializing = true;
    //    public int GetInterfaceSafetyOptions(ref Guid riid,ref int pdwSupportedOptions,ref int pdwEnabledOptions)
    //    {
    //     int Rslt= E_FAIL;
    //     string strGUID= riid.ToString("B");
    //     pdwSupportedOptions= INTERFACESAFE_FOR_UNTRUSTED_CALLER|INTERFACESAFE_FOR_UNTRUSTED_DATA;
    //        switch(strGUID)
    //        {
    //            case _IID_IDispatch:
    //            case _IID_IDispatchEx:
    //                Rslt = S_OK;
    //                pdwSupportedOptions= 0;
    //                if(_fSafeForScripting==true)
    //                pdwEnabledOptions=INTERFACESAFE_FOR_UNTRUSTED_CALLER;
    //                break;
    //            case _IID_IPersistStorage:
    //            case _IID_IPersistStream:
    //            case _IID_IPersistPropertyBag:
    //                Rslt = S_OK;
    //                pdwEnabledOptions= 0;
    //                if(_fSafeForInitializing==true)
    //                pdwEnabledOptions=INTERFACESAFE_FOR_UNTRUSTED_DATA;
    //                break;
    //            default:
    //                Rslt = E_NOINTERFACE;
    //                break;
    //        }
    //        return Rslt;
    //    }
    //    public int SetInterfaceSafetyOptions(ref Guid riid,int
    //    dwOptionSetMask,int dwEnabledOptions)
    //    {
    //        int Rslt= E_FAIL;
    //        string strGUID= riid.ToString("B");
    //        switch(strGUID)
    //        {
    //            case _IID_IDispatch:
    //            case _IID_IDispatchEx:
    //                if(((dwEnabledOptions& dwOptionSetMask)==
    //                INTERFACESAFE_FOR_UNTRUSTED_CALLER)&&(_fSafeForScripting==true))
    //                Rslt= S_OK;
    //                break;
    //            case _IID_IPersistStorage:
    //            case _IID_IPersistStream:
    //            case _IID_IPersistPropertyBag:
    //                if(((dwEnabledOptions& dwOptionSetMask)==
    //                INTERFACESAFE_FOR_UNTRUSTED_DATA)&&(_fSafeForInitializing ==true))
    //                Rslt= S_OK;
    //                break;
    //            default:
    //                Rslt = E_NOINTERFACE;
    //                break;
    //        }
    //        return Rslt;
    //    }
    //#endregion




    }
}
