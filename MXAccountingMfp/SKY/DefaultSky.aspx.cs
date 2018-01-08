using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccountingPlusDevice;
using AppLibrary;

namespace AccountingPlusEA.SKY
{
    public partial class DefaultSky : System.Web.UI.Page
    {
        public static string theme = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Content-type", "text/xml");
            ApplyThemes();
        }

        /// <summary>
        /// Applies the themes.
        /// </summary>
        private void ApplyThemes()
        {
            AccountingPlusDevice.AppCode.ApplicationHelper.ClearSqlPools();
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
    }
}