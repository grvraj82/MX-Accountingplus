#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s):  Hirdesh
  File Name: IPrintDataProvider.cs
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

using System;
using System.ServiceModel;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace PrintDataProviderService
{
    [ServiceContract(Namespace = "http://DataService")]

    interface IPrintDataProvider
    {

        [OperationContract]
        DataTable ProvidePrintJobs(string userId, string userSource);

        [OperationContract]
        DataTable ProvideAllPrintJobs(string userSource);
        
        [OperationContract]
        byte[] ProvidePrintedFile(string userId,string userSource, string jobId);

        [OperationContract]
        byte[] ProvidePrintReadyFileWithEditableSettings(Dictionary<string, string> printSettings, string userId, string userSource, string jobId, string duplexDirection, string driverType, bool isCollate,string pageCount);

        [OperationContract]
        string ProvideDuplexDirection(string userId, string userSource, string jobId, string driverType);

        [OperationContract]
        Dictionary<string, string> ProvidePrintJobSettings(string userId, string userSource, string jobId);

        [OperationContract]
        Dictionary<string, string> ProvidePrintSettings(Dictionary<string, string> dcSettings, string userId, string userSource, string jobId);

        [OperationContract]
        void DeleteAllPrintJobs(string userSource);
                
        [OperationContract]
        DataTable ProvidePrintedUsers(string userSource);

        [OperationContract]
        void DeletePrintJobs(string userId, string userSource, ArrayList dcJobs);

        [OperationContract]
        bool IsServiceLive();
    }
}
