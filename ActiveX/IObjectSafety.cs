﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ActiveX
{
    //[ComImport, GuidAttribute("CB5BDC81-93C1-11CF-8F20-00805F2CD064")]
    //[InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    // public interface IObjectSafety
    // {
    //    [PreserveSig]
    //    int GetInterfaceSafetyOptions(ref Guid riid,[MarshalAs(UnmanagedType.U4)] 
    //        ref int pdwSupportedOptions,[MarshalAs(UnmanagedType.U4)]
    //        ref int pdwEnabledOptions);
    //    [PreserveSig()]
    //    int SetInterfaceSafetyOptions(ref Guid riid,[MarshalAs(UnmanagedType.U4)]
    //        int dwOptionSetMask,[MarshalAs(UnmanagedType.U4)]
    //        int dwEnabledOptions);
    // }

    [
        Serializable,
        ComVisible(true)
    ]
    public enum ObjectSafetyOptions
    {
        INTERFACESAFE_FOR_UNTRUSTED_CALLER = 0x00000001,
        INTERFACESAFE_FOR_UNTRUSTED_DATA = 0x00000002,
        INTERFACE_USES_DISPEX = 0x00000004,
        INTERFACE_USES_SECURITY_MANAGER = 0x00000008
    };

    //
    // MS IObjectSafety Interface definition
    //
    [
        ComImport(),
        Guid("CB5BDC81-93C1-11CF-8F20-00805F2CD064"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)
    ]
    public interface IObjectSafety
    {
        [PreserveSig]
        long GetInterfaceSafetyOptions(ref Guid iid, out int pdwSupportedOptions, out int pdwEnabledOptions);

        [PreserveSig]
        long SetInterfaceSafetyOptions(ref Guid iid, int dwOptionSetMask, int dwEnabledOptions);
    };
}
