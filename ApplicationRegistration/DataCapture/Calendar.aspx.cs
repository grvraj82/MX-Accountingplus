#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: Calendar.aspx.cs
  Description: Calender control to the date
  Date Created : June 20, 07

 */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  Sept 08, 07         Rajshekhar D
*/
#endregion

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Text;

namespace ApplicationRegistration.DataCapture
{
    public partial class Calendar : System.Web.UI.Page
    {
        /// <summary>
        /// Method that get called on Page Load
        /// </summary>
        /// <param name="sender">Event Source Control</param>
        /// <param name="eventArgument">Event Data</param>
        protected void Page_Load(object sender, EventArgs eventArgument)
        {
            if (!Page.IsPostBack)
            {
                CalDateSelection.SelectedDate = DateTime.Today;
                CalDateSelection.VisibleDate = CalDateSelection.TodaysDate;
                BuildMonths();
                BuildYears();
                DataController.SetAsSeletedValue(DropDownListMonth, CalDateSelection.SelectedDate.Month.ToString(CultureInfo.InvariantCulture), true);
                DataController.SetAsSeletedValue(DropDownListYear, CalDateSelection.SelectedDate.Year.ToString(CultureInfo.InvariantCulture), true);
            }
        }

        /// <summary>
        /// Generate Months for Month DropdownList
        /// </summary>
        private void BuildMonths()
        {
            
            //Months
            for (int month = 1; month <= 12; month++)
            {
                System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
                string monthName = dateInfo.GetMonthName(month);
                DropDownListMonth.Items.Add(new ListItem(monthName, month.ToString(CultureInfo.InvariantCulture)));
            }
        }

        /// <summary>
        /// Generate Years for Year DropdownList [Current Year +- 5]
        /// </summary>
        private void BuildYears()
        {
            //Years
            DropDownListYear.Items.Clear();
            for (int year = CalDateSelection.VisibleDate.Year - 5; year <= CalDateSelection.VisibleDate.Year + 5; year++)
            {
                DropDownListYear.Items.Add(new ListItem(year.ToString(CultureInfo.InvariantCulture), year.ToString(CultureInfo.InvariantCulture)));
            }
        }

        /// <summary>
        /// Method that get called, when Date selection changed
        /// </summary>
        /// <param name="sender">Event Source Control</param>
        /// <param name="eventArgument">Event Data</param>
        protected void CalDateSelection_SelectionChanged(object sender, EventArgs eventArgument)
        {
            string script = "";
            script = "<script language=\"javascript\">";
            script += "opener.document.";
            script += HttpContext.Current.Request.QueryString["CurDay"] + ".value='";

            string[] arrSeletedDate = CalDateSelection.SelectedDate.ToShortDateString().Split("/".ToCharArray());
            //arrSeletedDate[]
            if (arrSeletedDate[0].Length == 1)
            {
                arrSeletedDate[0] = "0" + arrSeletedDate[0]; 
            }
            if (arrSeletedDate[1].Length == 1)
            {
                arrSeletedDate[1] = "0" + arrSeletedDate[1];
            }
            string selectedDate = arrSeletedDate[0] + "/" + arrSeletedDate[1] + "/" + arrSeletedDate[2];
            script += selectedDate + "';window.close();";
            script += "</script>";
            StringBuilder sbScriptText = new StringBuilder(script);
            LabelScript.Text = sbScriptText.ToString();
        }

        private void SetDate()
        {
            int intYear = int.Parse(DropDownListYear.SelectedValue, CultureInfo.InvariantCulture);
            int intMonth = int.Parse(DropDownListMonth.SelectedValue, CultureInfo.InvariantCulture);
            int intDay = DateTime.Today.Day;
            CalDateSelection.TodaysDate = new DateTime(intYear, intMonth, intDay);
        }

        /// <summary>
        /// Method that get called , when Month is changed from DropDown List
        /// </summary>
        /// <param name="sender">Event Source Control</param>
        /// <param name="eventArgument">Event Data</param>
        protected void DropDownListMonth_SelectedIndexChanged(object sender, EventArgs eventArgument)
        {
            SetDate();
        }

        /// <summary>
        /// Method that get called, when Year is changed from DropDown List
        /// </summary>
        /// <param name="sender">Event Source Control</param>
        /// <param name="eventArgument">Event Data</param>
        protected void DropDownListYear_SelectedIndexChanged(object sender, EventArgs eventArgument)
        {
            SetDate();
        }

        /// <summary>
        /// Method that get called, when Visible Month is changed
        /// </summary>
        /// <param name="sender">Event Source Control</param>
        /// <param name="eventArgument">Event Data</param>
        protected void CalDateSelection_VisibleMonthChanged(object sender, MonthChangedEventArgs eventArgument)
        {
            CalDateSelection.TodaysDate = CalDateSelection.VisibleDate;
            DataController.SetAsSeletedValue(DropDownListMonth, CalDateSelection.VisibleDate.Month.ToString(CultureInfo.InvariantCulture), true);
            BuildYears();
            DataController.SetAsSeletedValue(DropDownListYear, CalDateSelection.VisibleDate.Year.ToString(CultureInfo.InvariantCulture), true);
        }
    }
}
