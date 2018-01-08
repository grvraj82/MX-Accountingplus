using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingPlusWeb.Administration
{
    public partial class ServerScreen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindControls();
            }
        }

        private void BindControls()
        {

            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;

            int hour = DateTime.Now.Hour;
            int minute = DateTime.Now.Minute;
            int second = DateTime.Now.Second;

            // Year
            for (int itemIndex = year - 1; itemIndex < year; itemIndex++)
            {
                drpYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            }

            drpYear.SelectedIndex = drpYear.Items.IndexOf(drpYear.Items.FindByValue(year.ToString()));

            BuildOptions(1, 12, month, drpMonth);

            BuildOptions(1, 31, day, drpDay);

            BuildOptions(1, 23, hour, drpHour);

            BuildOptions(0, 59, minute, drpMinute);

            BuildOptions(0, 59, second, drpSecond);

            //drpSpeed.Items.Add(new ListItem("0.5 Second", "0.5"));
            drpSpeed.Items.Add(new ListItem("1 Second", "1" , true));

            var targetScreenShot = string.Format("../ScreenCapture/{0}/{1}/{2}/{3}/{4}/{5}.png", year, month, day, hour, minute, second);
            ImageServerScreen.ImageUrl = targetScreenShot;

        }

        private void BuildOptions(int startNumber, int endNumber, int currentNumber, DropDownList targetControl)
        {
            for (int itemIndex = startNumber; itemIndex <= endNumber; itemIndex++)
            {
                if (itemIndex < 10)
                {
                    targetControl.Items.Add(new ListItem("0" + itemIndex.ToString(), "0" + itemIndex.ToString()));
                }
                else
                {
                    targetControl.Items.Add(new ListItem(itemIndex.ToString(), itemIndex.ToString()));
                }
            }

            if (currentNumber < 10)
            {
                targetControl.SelectedIndex = targetControl.Items.IndexOf(targetControl.Items.FindByValue("0" + currentNumber.ToString()));
            }
            else
            {
                targetControl.SelectedIndex = targetControl.Items.IndexOf(targetControl.Items.FindByValue(currentNumber.ToString()));
            }
        }
    }
}