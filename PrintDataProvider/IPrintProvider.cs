#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s):  Hirdesh
  File Name: FileServerPrintProvider.cs
  Description: Provides Print Data to Application
  Date Created : July 2010
  */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  27.7.2010          Rajshekhar
*/
#endregion

using System.Collections.Generic;
using System.Data;
using System.Collections;

namespace PrintDataProviderService
{
    /// <summary>
    /// Interface to provide loos coupling between Service and Provider classes
    /// </summary>
    interface IPrintProvider
    {
        byte[] ProvidePrintedFile(string userId, string userSource, string jobId);

        Dictionary<string, string> ProvidePrintJobSettings(string userId, string userSource, string jobId);

        DataTable ProvidePrintJobs(string userId, string userSource);
        DataTable ProvideAllPrintJobs(string userSource);

        byte[] ProvidePrintReadyFileWithEditableSettings(Dictionary<string, string> printSettings, string userId, string userSource, string jobId, string duplexDirection, string driverType, bool isCollate, string pageCount);

        Dictionary<string, string> ProvidePrintSettings(Dictionary<string, string> dcSettings, string userId, string userSource, string jobId);

        void DeletePrintJobs(string userId, string userSource, ArrayList dcJobs);

        void DeletePrintJobs(string userId, string userSource);

        void DeletePrintJobs();

        void DeleteAllPrintJobs(string userSource);

        DataTable ProvidePrintedUsers(string userSource);

        string ProvideDuplexDirection(string userId, string userSource, string jobId, string driverType);
    }
}
