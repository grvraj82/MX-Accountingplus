using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OsaDirectEAManager;
using System.Data;

namespace AccountingPlusEA.SKY
{
    public partial class MyBalance : System.Web.UI.Page
    {
        public string BalanceReport = "";
        public string UserIdentityID = "";
        public string UserID = "";
        public string GroupID = "";
        public string UserDetails = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            AppCode.ApplicationHelper.ClearSqlPools();
            Response.AppendHeader("Content-type", "text/xml");

            UserIdentityID = Request.Params["usysid"];
            UserID = Request.Params["uid"];
            GroupID = Request.Params["gid"];

            //Helper.UserAccount.LimitsOn = "user";
            Helper.UserAccount.Create(UserIdentityID, "", UserIdentityID, "User", "MFP");
            Helper.DeviceSession.Get(Session["DeviceID"] as string).LogUserIn(UserIdentityID, new Helper.MyAccountant(), true, false);
           
            //if (Request.Params["id_ok"] != null)
            //{

            //    return;
            //}
            //else
            //{
            //    //GetBalance();
            //}
        }

        //private void GetBalance()
        //{
        //    string selectedGroup = Request.Params["gid"];
        //    GroupID = selectedGroup;
        //    UserIdentityID = Request.Params["usysid"];
        //    UserID = Request.Params["uid"];

        //    string groupName = AccountingBSL.GetGroupName(GroupID);
        //    string userName = AccountingBSL.GetUserName(UserIdentityID);

        //    DataSet dsJobTypes = AccountingBSL.GetJobTypes();
        //    DataSet dsBalance = AccountingBSL.GetGroupBalance(selectedGroup);
        //    UserDetails = "<li title=\"User: " + userName + "      Group: " + groupName + "\" />";
        //    UserDetails += "<li title=\"-------------------------------------------------------------------------------------------\"/>";
        //    //UserDetails = "<p>User: " + userName + "      Group: " + groupName + "</p><p></p>";
        //    UserDetails += "<li title=\"" + FormatText("Job Type") + FormatText("Page Limit") + FormatText("Used") + FormatText("Balance") + "\"/>";
        //    UserDetails += "<li title=\"-------------------------------------------------------------------------------------------\"/>";
        //    for (int row = 0; row < dsJobTypes.Tables[0].Rows.Count; row++)
        //    {

        //        string jobType = dsJobTypes.Tables[0].Rows[row]["JOB_ID"].ToString();
        //        if (jobType.ToUpper() != "SETTINGS")
        //        {
        //            DataRow[] drUserJobLimit = dsBalance.Tables[0].Select("JOB_TYPE ='" + jobType + "'");
        //            DataRow[] drJobPrice = dsBalance.Tables[1].Select("JOB_TYPE ='" + jobType + "'");


        //            Int32 jobLimit = 0;
        //            Int32 jobUsed = 0;
        //            Int32 jobRemaining = 0;
        //            Int32 jobUnitPrice = 0;
        //            Int32 jobTotalPrice = 0;

        //            string jobLimitData = "";
        //            string jobRemainingData = "";
        //            if (drUserJobLimit.Length > 0)
        //            {
        //                jobLimit = Int32.Parse(drUserJobLimit[0]["JOB_LIMIT"].ToString());
        //                jobUsed = Int32.Parse(drUserJobLimit[0]["JOB_USED"].ToString());
        //                jobRemaining = jobLimit - jobUsed;
        //            }

        //            if (drJobPrice.Length > 0)
        //            {
        //                jobUnitPrice = Int32.Parse(drJobPrice[0]["PRICE_PERUNIT"].ToString());
        //                jobTotalPrice = jobUsed * jobUnitPrice;
        //            }


        //            if (jobLimit == Int32.MaxValue)
        //            {
        //                jobLimitData = jobRemainingData = "~";
        //            }
        //            else
        //            {
        //                jobLimitData = jobLimit.ToString();
        //                jobRemainingData = jobRemaining.ToString();
        //            }
        //            jobType = FormatText(jobType);
        //            BalanceReport += "<li title=\"" + jobType + FormatText(jobLimitData) + FormatText(jobUsed.ToString()) + FormatText(jobRemainingData.ToString()) + " \"/>";
        //            //BalanceReport += "<p>" + jobType + "    " + jobUsed.ToString() + "  " + jobRemaining.ToString() + "</p>";
        //        }
        //    }
        //}


        private string FormatText(string textInput)
        {
            int textInputLength = textInput.Length;

            for (int space = textInputLength; space < 18; space++)
            {
                textInput += " ";
            }
            return textInput;
        }
    }
}