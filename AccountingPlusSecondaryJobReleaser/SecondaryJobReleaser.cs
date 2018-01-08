using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using System.Globalization;
using System.Data.SqlClient;

namespace AccountingPlusSecondaryJobReleaser
{
    public partial class SecondaryJobReleaser : ServiceBase
    {
        private const string SERVICE_NAME = "AccountingPlusSecondaryJobReleaser";

        private System.Timers.Timer jobMoniterTimer;

        private static string serviceWatchTime = ConfigurationManager.AppSettings["ServiceWatchTime"];

        public SecondaryJobReleaser()
        {
            InitializeComponent();
        }

        protected void InitializeTimer()
        {
            try
            {
                SqlConnection.ClearAllPools();
            }
            catch
            {
            }
            jobMoniterTimer = new System.Timers.Timer();
            jobMoniterTimer.AutoReset = true;
            jobMoniterTimer.Interval = Convert.ToDouble(ConfigurationManager.AppSettings["JobMoniteringInterval"], CultureInfo.CurrentCulture);
            jobMoniterTimer.Elapsed += new System.Timers.ElapsedEventHandler(MoniterJobReleaseRequests);
        }

        protected override void OnStart(string[] args)
        {
            PrintJobProvider.JobReleaserServiceHelper.RecordServiceTimings(SERVICE_NAME, "start");
            InitializeTimer();
            jobMoniterTimer.Enabled = true;
        }

        protected override void OnStop()
        {
            PrintJobProvider.JobReleaserServiceHelper.RecordServiceTimings(SERVICE_NAME, "stop");
        }

        private void MoniterJobReleaseRequests(object source, System.Timers.ElapsedEventArgs e)
        {
            PrintJobProvider.JobReleaserServiceHelper.ReleaseJobsFromDatabaseQueue(SERVICE_NAME);
        }
    }
}
