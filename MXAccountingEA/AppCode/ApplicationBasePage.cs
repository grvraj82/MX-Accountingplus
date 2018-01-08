#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.

  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise,
  is prohibited without the prior written consent of the copyright owner.

  Author(s):Sreedhar P
  File Name: ApplicationBasePage.cs
  Description: Application base page
  Date Created : Oct 2010
  */
#endregion Copyright

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
            1. 10/21/2010           Rajshekhar D
*/
#endregion


using System;
using System.Globalization;
using AppLibrary;

namespace AccountingPlusEA
{
    public class ApplicationBasePage : System.Web.UI.Page
    {
        protected string PageWidth = string.Empty;
        protected string PageHeight = string.Empty;

        /// <summary>
        /// Handles the PreInit event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_PreInit(object sender, EventArgs e)
        {

            if (Session["OSAModel"] == null)
            {
                bool browserNF = true;
                try
                {
                    string alternative = Request.Headers["X-BC-Alternative"];
                    if (alternative.Contains("netfront"))
                    {
                        browserNF = true;
                    }
                }
                catch (Exception ex)
                {
                    browserNF = false;
                }

                string OSAModel = Request.Headers["X-BC-Resolution"];

                string theme = OSAModel;
                if (string.IsNullOrEmpty(Convert.ToString(theme, CultureInfo.CurrentCulture)))
                {
                    Session["selectedTheme"] = theme;
                    //Page.Theme = Convert.ToString(Session["selectedTheme"], CultureInfo.CurrentCulture);
                    Session["OSAModel"] = "FormBrowser";
                }
                else if (!browserNF && string.IsNullOrEmpty(OSAModel))
                {
                    Session["selectedTheme"] = null;
                    Page.Theme = null;
                    Session["OSAModel"] = "FormBrowser";
                }
                else
                {
                    if (OSAModel == Constants.DEVICE_MODEL_PSP)
                    {
                        //Page.Theme = Convert.ToString(Constants.DEVICE_MODEL_PSP, CultureInfo.CurrentCulture);
                        PageWidth = Constants.PSP_MODEL_WIDTH;
                        PageHeight = Constants.PSP_MODEL_HEIGHT;
                    }
                    else if (OSAModel == Constants.DEVICE_MODEL_OSA)
                    {
                        //Page.Theme = Constants.DEFAULT_THEME;
                        //Convert.ToString(Constants.DEVICE_MODEL_OSA, CultureInfo.CurrentCulture);
                        PageWidth = Constants.OSA_MODEL_WIDTH;
                        PageHeight = Constants.OSA_MODEL_HEIGHT;
                    }
                    else if (OSAModel == Constants.DEVICE_MODEL_HALF_VGA)
                    {
                        Session["selectedTheme"] = null;
                        Page.Theme = null;
                        OSAModel = "FormBrowser";
                    }
                    else if (OSAModel == Constants.DEVICE_MODEL_WIDE_XGA)
                    {
                        PageWidth = Constants.OSA_XGA_WIDTH;
                        PageHeight = Constants.OSA_XGA_HEIGHT;
                    }
                    else
                    {
                        //Page.Theme = Convert.ToString(Constants.DEVICE_MODEL_DEFAULT, CultureInfo.CurrentCulture);
                        PageWidth = Constants.DEFAULT_MODEL_WIDTH;
                        PageHeight = Constants.DEFAULT_MODEL_HEIGHT;
                    }

                    Session["Width"] = PageWidth;
                    Session["Height"] = PageHeight;
                    Session["OSAModel"] = OSAModel;
                }
            }
            else
            {
                string OSAModel = Session["OSAModel"] as string;

                if (OSAModel == Constants.DEVICE_MODEL_PSP)
                {
                    //Page.Theme = Convert.ToString(Constants.DEVICE_MODEL_PSP, CultureInfo.CurrentCulture);
                    PageWidth = Constants.PSP_MODEL_WIDTH;
                    PageHeight = Constants.PSP_MODEL_HEIGHT;
                }
                else if (OSAModel == Constants.DEVICE_MODEL_OSA)
                {
                    //Page.Theme = Constants.DEFAULT_THEME;
                    //Convert.ToString(Constants.DEVICE_MODEL_OSA, CultureInfo.CurrentCulture);
                    PageWidth = Constants.OSA_MODEL_WIDTH;
                    PageHeight = Constants.OSA_MODEL_HEIGHT;
                }
                else if (OSAModel == Constants.DEVICE_MODEL_HALF_VGA)
                {
                    Session["selectedTheme"] = null;
                    Page.Theme = null;
                    OSAModel = "FormBrowser";
                }
                else if (OSAModel == Constants.DEVICE_MODEL_WIDE_XGA)
                {
                    PageWidth = Constants.OSA_XGA_WIDTH;
                    PageHeight = Constants.OSA_XGA_HEIGHT;
                }
                else
                {
                    //Page.Theme = Convert.ToString(Constants.DEVICE_MODEL_DEFAULT, CultureInfo.CurrentCulture);
                    PageWidth = Constants.DEFAULT_MODEL_WIDTH;
                    PageHeight = Constants.DEFAULT_MODEL_HEIGHT;
                }

                Session["Width"] = PageWidth;
                Session["Height"] = PageHeight;
                Session["OSAModel"] = OSAModel;
            }
        }
    }
}
