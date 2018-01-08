using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using System.Text;
using AppLibrary;
using System.Data;

namespace AccountingPlusEA.SKY
{
    public partial class SelectGroup : System.Web.UI.Page
    {
        public static string theme = string.Empty;
        public string AssignedGroups = string.Empty;
        static string userSysID = string.Empty;
        static string userID = string.Empty;
        static bool isPageRedirected = true;
        static string deviceModel = string.Empty;
        static string userSource = string.Empty;
        static int costCenterCount = 0;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            AccountingPlusDevice.AppCode.ApplicationHelper.ClearSqlPools();
            Response.AppendHeader("Content-type", "text/xml");
            string costCenterID = "";
            if (Request.Params["CC"] != null)
            {
                costCenterID = Request.Params["CC"] as string;
                RedirectPage(costCenterID);
                if (!isPageRedirected)
                {
                    return;
                }
            }
            userSysID = Session["UserSystemID"] as string;
            userSource = Session["UserSource"] as string;
            userID = Session["UserID"] as string;
            //Request.Params["uid"];
            if (!IsPostBack)
            {
                deviceModel = Session["OSAModel"] as string;
                BindGroups();
                ApplyThemes();
            }
        }

        /// <summary>
        /// Applies the themes.
        /// </summary>
        private void ApplyThemes()
        {
            string currentTheme = Session["MFPTheme"] as string;

            if (string.IsNullOrEmpty(currentTheme))
            {
                string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
                currentTheme = DataManagerDevice.ProviderDevice.Device.ProvideTheme("FORM", deviceIpAddress);

                if (string.IsNullOrEmpty(currentTheme))
                {
                    currentTheme = Constants.DEFAULT_THEME;
                }
                else
                {
                    Session["MFPTheme"] = currentTheme;
                }
            }
            theme = currentTheme;
        }

        /// <summary>
        /// Binds the groups.
        /// </summary>
        private void BindGroups()
        {
            // Get All the Groups;
            //DataSet dsUserGroups = DataManagerDevice.ProviderDevice.Users.ProvideGroups(userID, userSource);
            int groupsCount = DataManagerDevice.ProviderDevice.Users.ProvideGroupsCount(userID, userSource);
            DataSet dsGroups = DataManagerDevice.ProviderDevice.Users.ProvideAccessRights(userSysID, userSource, Request.Params["REMOTE_ADDR"].ToString());

            StringBuilder sbMenuControls = new StringBuilder();
            int groupCount = dsGroups.Tables[0].Rows.Count;
            bool isMyAccountSet = false;

            if (groupsCount > 0)
            {
                for (int group = 0; group < groupCount; group++)
                {
                    string costCenterId = dsGroups.Tables[0].Rows[group]["COSTCENTERID"].ToString();
                    string costCenterName = dsGroups.Tables[0].Rows[group]["COSTCENTERNAME"].ToString();
                    if (costCenterId == userSysID)
                    {
                        sbMenuControls.Append(string.Format("<input id='menu{0}' type='submit' value='{1}' onclick='SelectGroup.aspx?CC={2}'/>", groupCount, "My Account", userSysID));
                        costCenterCount++;
                        isMyAccountSet = true;
                    }
                    else
                    {
                        sbMenuControls.Append(string.Format("<input id='menu{0}' type='submit' value='{1}' onclick='SelectGroup.aspx?CC={2}'/>", groupCount, costCenterName, costCenterId));
                    }
                }
            }
            else
            {
                RedirectPage(userSysID);
            }
            AssignedGroups = sbMenuControls.ToString();
        }

        /// <summary>
        /// Redirects the page.
        /// </summary>
        /// <param name="costCenterID">The cost center ID.</param>
        private void RedirectPage(string costCenterID)
        {
            string limitsOn = "User";
            string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
            bool isUserLoginAllowed = true;
            if (costCenterID != userSysID)
            {
                limitsOn = "Cost Center";
            }

            isUserLoginAllowed = DataManagerDevice.ProviderDevice.Users.ProvideIsUserLoginAllowed(userSysID, costCenterID, deviceIpAddress, limitsOn);

            if (!isUserLoginAllowed)
            {
                isPageRedirected = false;
                if (costCenterCount <= 1)
                {
                    Response.Redirect("MessageForm.aspx?FROM=ManualLogOn.aspx&MESS=NoPermissionToDevice");
                }
                else
                {
                    Response.Redirect("MessageForm.aspx?FROM=SelectGroup.aspx&MESS=NoPermissionToDevice");
                }
                return;
            }
            else
            {
                string isRedirectToDeviceMode = Convert.ToString(Session["IsRedirectToDeviceMode"]);
                if (!string.IsNullOrEmpty(isRedirectToDeviceMode))
                {
                    if (bool.Parse(isRedirectToDeviceMode))
                    {
                        Response.Redirect("../Mfp/DeviceScreen.aspx", true);
                    }
                    else
                    {
                        Response.Redirect("PrintJobs.aspx", true);
                    }
                }
                else
                {
                    Response.Redirect("PrintJobs.aspx", true);
                }
            }
        }
    }
}