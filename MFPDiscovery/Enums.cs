

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

namespace MFPDiscovery
{
    public partial class MFPDiscoverer
    {
        public enum MFPCOMM_ERROR
        {
            MFPCOMM_FAIL = 0,
            MFPCOMM_OK = 1,
            MFPCOMM_WAIT = 2,
            MFPCOMM_CONTINUE = 3,
            MFPCOMM_FINISHED = 4,
            MFPCOMM_DISCOVERY_IN_PROGRESS = 5
        };

        public enum DiscoveryType
        {
            WebService,
            SNMP,
            SNMPWSD
        };
    }
}
