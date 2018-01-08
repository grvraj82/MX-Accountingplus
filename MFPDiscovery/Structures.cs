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
using System.Threading;

namespace MFPDiscovery
{
    public partial class MFPDiscoverer
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct MFP_T
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string szMfpIpAddress;

            #region WSD Based
            [MarshalAs(UnmanagedType.LPStr)]
            public string szMfpXAddrs;

            [MarshalAs(UnmanagedType.LPStr)]
            public string szMfpEndPointRefAddr;

            [MarshalAs(UnmanagedType.LPStr)]
            public string szMfpFamilyName;

            [MarshalAs(UnmanagedType.LPStr)]
            public string szMfpManufacturer;

            [MarshalAs(UnmanagedType.LPStr)]
            public string szMfpManufacturerUrl;

            [MarshalAs(UnmanagedType.LPStr)]
            public string szMfpModelNumber;

            [MarshalAs(UnmanagedType.LPStr)]
            public string szMfpPresentationUrl;

            [MarshalAs(UnmanagedType.LPStr)]
            public string szMfpDeviceCategory;

            [MarshalAs(UnmanagedType.LPStr)]
            public string szMfpFriendlyName;

            [MarshalAs(UnmanagedType.LPStr)]
            public string szMfpFirmwareVersion;

            [MarshalAs(UnmanagedType.LPStr)]
            public string szMfpSerialNumber;

            [MarshalAs(UnmanagedType.LPStr)]
            public string szMfpServiceId;

            [MarshalAs(UnmanagedType.LPStr)]
            public string szMfpHardwareId;
            #endregion

            #region SNMP Based
            [MarshalAs(UnmanagedType.LPStr)]
            public string szMfpModelName;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string szMfpDnsName;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string szMfpSysName;

            [MarshalAs(UnmanagedType.LPStr)]
            public string szMfpSysDesc;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string szMfpSysLocation;

            [MarshalAs(UnmanagedType.LPStr)]
            public string szMfpFamilyID;

            [MarshalAs(UnmanagedType.LPStr)]
            public string szMfpMacAddress;

            [MarshalAs(UnmanagedType.LPStr)]
            public string szMfpIPPStatus;

            [MarshalAs(UnmanagedType.LPStr)]
            public string szMfpIPPUrl;

            [MarshalAs(UnmanagedType.LPStr)]
            public string szMfpIPPSSLUrl;

            [MarshalAs(UnmanagedType.LPStr)]
            public string szMfpUnknown1;

            [MarshalAs(UnmanagedType.LPStr)]
            public string szMfpUnknown2;
            #endregion

            public int iMfpMetadataVersion;
            public DiscoveryType discoveryType;
        };
    }
}
