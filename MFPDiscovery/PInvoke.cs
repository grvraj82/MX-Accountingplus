#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Bhaskar Pujari, Rajshekhar D
  File Name: Enums.cs
  Description: Database Connection 
  Date Created : Sept 2010
  */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.          
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace MFPDiscovery
{
    public partial class MFPDiscoverer
    {
        private const string NATIVE_DLL_PATH = @"sfvecomm.dll";

        [DllImport(NATIVE_DLL_PATH)]
        private static extern IntPtr MFPComm_Initialize();

        [DllImport(NATIVE_DLL_PATH)]
        private static extern MFPCOMM_ERROR MFPComm_Uninitialize(IntPtr handle);

        [DllImport(NATIVE_DLL_PATH)]
        private static extern MFPCOMM_ERROR MFPComm_SetDiscoveryType(IntPtr handle, DiscoveryType discoveryType);

        [DllImport(NATIVE_DLL_PATH)]
        private static extern MFPCOMM_ERROR MFPComm_DiscoverMFPs(IntPtr handle);

        [DllImport(NATIVE_DLL_PATH)]
        private static extern MFPCOMM_ERROR MFPComm_DiscoverMFPRange(IntPtr handle, string startMfpIP, string endMfpIP);

        [DllImport(NATIVE_DLL_PATH)]
        private static extern MFPCOMM_ERROR MFPComm_DiscoverMFP(IntPtr handle, string mfpIP);

        //[DllImport(NATIVE_DLL_PATH)]
        //private static extern MFPCOMM_ERROR MFPComm_DiscoverMFPByName(IntPtr handle, string mfpName);
        
        [DllImport(NATIVE_DLL_PATH)]
        private static extern MFPCOMM_ERROR MFPComm_GetMFP(IntPtr handle, ref IntPtr mfp_t);

        [DllImport(NATIVE_DLL_PATH)]
        private static extern int MFPComm_GetDiscoverMFPCount(IntPtr handle);

        [DllImport(NATIVE_DLL_PATH)]
        private static extern MFPCOMM_ERROR MFPComm_CancelDiscovery(IntPtr handle);
    }
}
