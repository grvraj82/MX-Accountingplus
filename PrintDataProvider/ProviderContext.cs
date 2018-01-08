using System.Collections.Generic;
using System.Data;
using System.Collections;

namespace PrintDataProviderService
{
    class ProviderContext
    {
        IPrintProvider PrintProvider;


        public void SetPrintProvider(IPrintProvider printProvider)
        {
            this.PrintProvider = printProvider;
        }

        public byte[] ProvidePrintedFile(string userId,string userSource, string jobId)
        {
            byte[] FullFile = null;
            FullFile = PrintProvider.ProvidePrintedFile(userId, userSource, jobId);
            return FullFile;
        }
        public Dictionary<string, string> ProvidePrintJobSettings(string userId, string userSource, string jobId)
        {
            Dictionary<string, string> printJobSettings = new Dictionary<string, string>();
            printJobSettings=PrintProvider.ProvidePrintJobSettings(userId, userSource, jobId);
            return printJobSettings;
        }

        public DataTable ProvidePrintJobs(string userId, string userSource)
        {
            DataTable dtPrintJobs = null;
            dtPrintJobs=PrintProvider.ProvidePrintJobs(userId, userSource);
            return dtPrintJobs;
        }

        public DataTable ProvidePrintJobs(string userSource)
        {
            return PrintProvider.ProvideAllPrintJobs(userSource); 
        }

        public byte[] ProvidePrintReadyFileWithEditableSettings(Dictionary<string, string> printSettings, string userId, string userSource, string jobId, string duplexDirection, string driverType, bool isCollate, string pageCount)
        {
            byte[] FullFile = null;
            FullFile = PrintProvider.ProvidePrintReadyFileWithEditableSettings(printSettings, userId, userSource, jobId, duplexDirection, driverType, isCollate, pageCount);
            return FullFile;
        }

        public string ProvideDuplexDirection(string userId, string userSource, string jobId, string driverType)
        {
            string duplexDirection = string.Empty;
            duplexDirection = PrintProvider.ProvideDuplexDirection(userId, userSource, jobId,driverType);
            return duplexDirection;
        }

        public Dictionary<string, string> ProvidePrintSettings(Dictionary<string, string> dcSettings, string userId, string userSource, string jobId)
        {
            Dictionary<string, string> printJobSettings = new Dictionary<string, string>();
            printJobSettings = PrintProvider.ProvidePrintSettings(dcSettings, userId, userSource, jobId);
            return printJobSettings;
        }

        public void DeletePrintJobs(string userId, string userSource, ArrayList dcJobs)
        {
            PrintProvider.DeletePrintJobs(userId,userSource, dcJobs);
        }

        public void DeleteAllPrintJobs(string userSource)
        {
            PrintProvider.DeleteAllPrintJobs(userSource);
        }

        public void DeletePrintJobs(string userId, string userSource)
        {
            PrintProvider.DeletePrintJobs(userId, userSource);
        }
        public void DeletePrintJobs()
        {
            PrintProvider.DeletePrintJobs();
        }

        public DataTable ProvidePrintedUsers(string userSource)
        {
            DataTable dtPrintedUsers = null;
            dtPrintedUsers = PrintProvider.ProvidePrintedUsers(userSource);
            return dtPrintedUsers;
        }
    }
}
