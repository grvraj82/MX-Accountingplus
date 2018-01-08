#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.

  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise,
  is prohibited without the prior written consent of the copyright owner.

  Author(s):
  File Name: SoapExp.aspx
  Description: MFP Soap Exceptions.
  Date Created : July 2010
  */

#endregion Copyright

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
            1. 9/7/2010           Rajshekhar D
*/
#endregion

using System;
using System.Collections;
using System.Web;
using AppLibrary;

namespace AccountingPlusEA.Mfp
{
    public partial class SoapExp : System.Web.UI.Page
    {
        static string deviceCulture = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            AppCode.ApplicationHelper.ClearSqlPools();
            if (!IsPostBack)
            {
                string soapExp = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "SOAP_EXCEPTION_OCCURED");
                deviceCulture = HttpContext.Current.Request.UserLanguages[0];
                bool isSupportedlangauge = DataManagerDevice.ProviderDevice.ApplicationSettings.IsSupportedLanguage(deviceCulture);
                if (!isSupportedlangauge)
                {
                    deviceCulture = "en-US";
                }
                if (Session["UILanguage"] != null)
                {
                    deviceCulture = Session["UILanguage"] as string;
                }
            }
        }
    }
}
