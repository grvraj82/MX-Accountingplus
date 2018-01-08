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

namespace MFPDiscovery
{
    internal enum DiscoverMfp
    {
        All = 0,
        InRange = 1,
        IP = 2,
        //Name = 3
    }

    internal class DiscoveryWorkerArgument
    {
        private DiscoverMfp _enumType;
        private string _mfpIPorName;
        private string _startMfpIP;
        private string _endMfpIP;

        public DiscoverMfp Type
        {
            get
            {
                return _enumType;
            }
        }

        public string MfpIPorName
        {
            get
            {
                return _mfpIPorName;
            }
        }

        public string StartMfpIP
        {
            get
            {
                return _startMfpIP;
            }
        }

        public string EndMfpIP
        {
            get
            {
                return _endMfpIP;
            }
        }

        private DiscoveryWorkerArgument()
        { }

        public DiscoveryWorkerArgument(DiscoverMfp enumType, string mfpIPorName)
        {
            _enumType = enumType;
            _mfpIPorName = mfpIPorName;
        }

        public DiscoveryWorkerArgument(DiscoverMfp enumType, string startMfpIP, string endMfpIP)
        {
            _enumType = enumType;
            _startMfpIP = startMfpIP;
            _endMfpIP = endMfpIP;
        }
    }
}
