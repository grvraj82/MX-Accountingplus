

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
    public class MfpDiscoveredEventArgs : EventArgs
    {
        private MFPDiscoverer.MFP_T _mfp_t;

        public MFPDiscoverer.MFP_T MFP_T
        {
            get
            {
                return _mfp_t;
            }
        }

        public MfpDiscoveredEventArgs(MFPDiscoverer.MFP_T mfp_t)
        {
            _mfp_t = mfp_t;
        }
    }
}
