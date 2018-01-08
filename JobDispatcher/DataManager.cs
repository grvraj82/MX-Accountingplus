using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JobDispatcher
{
    public static class DataManager
    {

        public static void QueueForFTPPrinting(string userSource, string userID, string jobID,  string jobFile, long jobSize, bool isReleaseWithSettings, string orginalSettings, string newSettings, string ftpPath, string ftpUserName, string ftpUserPassword, bool isDeleteFile)
        {
            int megaByte = 1024 * 1024;
            string serviceName = "AccountingPlusPrimaryJobReleaser";
            
            if (jobSize <= 10 * megaByte)
            {
                serviceName = "AccountingPlusPrimaryJobReleaser";
            }
            else if (jobSize > 10 * megaByte && jobSize <= 200 * megaByte)
            {
                serviceName = "AccountingPlusSecondaryJobReleaser";
            }
            else
            {
                serviceName = "AccountingPlusTertiaryJobReleaser";
            }

            string deleteFile = "false";
            if (isDeleteFile)
            {
                deleteFile = "true";
            }

            string releaseWithSettings = "false";
            if (isReleaseWithSettings)
            {
                releaseWithSettings = "true";
            }

            string isSettingsChanged = "true";

            if (!string.IsNullOrEmpty(orginalSettings))
            {
                orginalSettings = orginalSettings.Replace("'", "''");
            }

            if (!string.IsNullOrEmpty(newSettings))
            {
                newSettings = newSettings.Replace("'", "''");
            }

            string sqlQuery = "insert into T_PRINT_JOBS(USER_SOURCE, USER_ID, JOB_ID, JOB_FILE,JOB_SIZE,JOB_RELEASER_ASSIGNED,JOB_CHANGED_SETTINGS,JOB_RELEASE_WITH_SETTINGS,JOB_SETTINGS_ORIGINAL,JOB_SETTINGS_REQUEST,";
            sqlQuery += "JOB_FTP_PATH,JOB_FTP_ID,JOB_FTP_PASSWORD,JOB_PRINT_RELEASED,REC_DATE,DELETE_AFTER_PRINT)";
            sqlQuery += " values(N'" + userSource + "', N'" + userID + "', N'" + jobID.Replace("'", "''") + "', N'" + jobFile.Replace("'", "''") + "', N'" + jobSize.ToString() + "',  N'" + serviceName + "', '" + isSettingsChanged + "', '" + releaseWithSettings + "',";
            sqlQuery += " N'" + orginalSettings + "', N'" + newSettings + "', ";
            sqlQuery += " N'" + ftpPath + "',N'" + ftpUserName.Replace("'", "''") + "',N'" + ftpUserPassword.Replace("'", "''") + "','false',getdate(),'" + deleteFile + "')";

            using (Database dbPrintJobs = new Database())
            {
                string returnValue = dbPrintJobs.ExecuteNonQuery(dbPrintJobs.GetSqlStringCommand(sqlQuery));
            }
        }
    }
}
