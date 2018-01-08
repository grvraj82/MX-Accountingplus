using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PrintJobProvider;
using System.Data.Common;
using System.Data;
using OsaDirectEAManager;

namespace AccountingPlusEA.Mfp
{
    public partial class AccountBalance : ApplicationBasePage
    {
        public int numJobs = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            AppCode.ApplicationHelper.ClearSqlPools();
            if (!IsPostBack)
            {
                string userID = Session["UserSystemID"] as string;
                string userName = Session["UserID"] as string;
                string userSource = Session["UserSource"] as string;
                string domainName = Session["DomainName"] as string;
                if (!string.IsNullOrEmpty(userID))
                {
                    AccUser.Text = "Account Balance of " + userName;
                    accBalance.Text = "Account Balance: Rs. " + Helper.UserAccount.GetBalance(userID).ToString();
                }

                numJobs = FileServerPrintJobProvider.ProvidePrintJobs(Session["UserID"].ToString(), userSource, domainName).Rows.Count;
                if (numJobs <= 0)
                {
                    currentPage.Value = "CopyScan.aspx";
                }
                else
                {
                    currentPage.Value = "JobList.aspx";
                }
            }
        }
    }
}