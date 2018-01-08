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
using System.Management;
using System.Management.Instrumentation;
using System.IO;
using System.Text;
using Microsoft.Win32;
using System.Globalization;

namespace AccountingPlusWeb.Administration
{
    public partial class ServerDetails : ApplicationBase.ApplicationBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataTable dtSystemDetails = new DataTable();

                dtSystemDetails.Columns.Add("KEY", typeof(string));
                dtSystemDetails.Columns.Add("VALUE", typeof(string));


                GetCPUData(dtSystemDetails);
                GetSystemInfo(dtSystemDetails);
                GetProductID(dtSystemDetails);

                BuildSystemDetails(dtSystemDetails);
            }
            LocalizeThisPage();

            LinkButton manageSettings = (LinkButton)Master.FindControl("LinkButtonServerDetails");
            if (manageSettings != null)
            {
                manageSettings.CssClass = "linkButtonSelect_Selected";
            }
        }

        private void LocalizeThisPage()
        {
            string labelResourceIDs = "SERVER_DETAILS,NAME,DETAILS";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            LabelHeadingServerDetails.Text = localizedResources["L_SERVER_DETAILS"].ToString();
            TableHeaderCellEnglish.Text = localizedResources["L_NAME"].ToString();
            TableHeaderCellSelctedMessage.Text = localizedResources["L_DETAILS"].ToString();

        }

        private void BuildSystemDetails(DataTable dtSystemDetails)
        {
            if (dtSystemDetails != null)
            {
                for (int row = 0; row < dtSystemDetails.Rows.Count; row++)
                {
                    TableRow trsettingvalue = new TableRow();
                    trsettingvalue.CssClass = "GridRow";


                    trsettingvalue.HorizontalAlign = HorizontalAlign.Left;

                    TableCell tdSlNo = new TableCell();
                    tdSlNo.HorizontalAlign = HorizontalAlign.Left;

                    tdSlNo.Text = "&nbsp;" + Convert.ToString((row + 1), CultureInfo.CurrentCulture);
                    string settingKey = dtSystemDetails.Rows[row]["KEY"].ToString();

                    TableCell tdsettingtype = new TableCell();
                    tdsettingtype.Text = "&nbsp;"+settingKey;

                    string settingvalue = dtSystemDetails.Rows[row]["VALUE"].ToString();



                    TableCell tdsettingvalue = new TableCell();

                    tdsettingvalue.Text = "&nbsp;" + settingvalue;
                    string htmlControlText = settingvalue;

                    tdsettingvalue.Text = htmlControlText;
                    trsettingvalue.Cells.Add(tdSlNo);
                    trsettingvalue.Cells.Add(tdsettingtype);
                    trsettingvalue.Cells.Add(tdsettingvalue);

                    TableServerDetails.Rows.Add(trsettingvalue);

                }
            }
        }
        private void GetProductID(DataTable dtSystemDetails)
        {
            string productID = "";
            string[] osversion = null;
            ManagementObjectSearcher query1 = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectCollection queryCollection1 = query1.Get();

            foreach (ManagementObject mo in queryCollection1)
            {
                productID = mo["SerialNumber"].ToString();
                osversion = mo["Name"].ToString().Split('|');

            }
            //Response.Write(productID);
            dtSystemDetails.Rows.Add("Serial Key", productID);
            // Response.Write(osversion[0].ToString());
            dtSystemDetails.Rows.Add("OS Version", osversion[0].ToString());
        }

        private void GetSystemInfo(DataTable dtSystemDetails)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DriveInfo di in DriveInfo.GetDrives())
            {
                if (di.IsReady)
                {
                    //sb.AppendFormat("Drive : " + di.Name + "---------" + "Total Size : " + (formatBytes(di.TotalSize)) + "-------" + "Total Free Space : " + formatBytes(di.TotalFreeSpace) + "<br/>");
                    dtSystemDetails.Rows.Add("Drive " + di.Name + "", "Total Size :" + (formatBytes(di.TotalSize)) + "    " + "Total Free Space : " + formatBytes(di.TotalFreeSpace) + "");
                }
            }
            //sb.Append(" Computer Name = " + Environment.MachineName + "<br/>");
            dtSystemDetails.Rows.Add("Computer Name", Environment.MachineName);
            //sb.Append(" userName =" + Environment.UserName + "<br/>");
            //dtSystemDetails.Rows.Add("User Name", Environment.UserName);
            // sb.Append(" OS-Version =" + Environment.OSVersion.ToString() + "<br/>");
            string model = "";
            Int64 memory = 0;
            ManagementObjectSearcher query1 = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
            ManagementObjectCollection queryCollection1 = query1.Get();

            foreach (ManagementObject mo in queryCollection1)
            {
                model = mo["model"].ToString();
                memory = Convert.ToInt64(mo["TotalPhysicalMemory"]);
            }
            //sb.Append(" Memory =" + formatBytes(memory) + " (Usable)" + "<br/>");
            dtSystemDetails.Rows.Add("Memory", formatBytes(memory) + " (Usable)");
            // sb.Append(" Model =" + model + "<br/>");
            dtSystemDetails.Rows.Add("Model", model);
            //sb.Append("Architecture  = " + Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE").ToString() + "<br/>");

            if (Environment.Is64BitOperatingSystem)
            {
                //sb.Append("System type = 64 bit Operating System" + "<br/>");
                dtSystemDetails.Rows.Add("System type", "64 bit Operating System");
            }
            else
            {
                //sb.Append("System type = 32 bit Operating System" + "<br/>");
                dtSystemDetails.Rows.Add("System type", "32 bit Operating System");
            }
            Response.Write(sb.ToString());

        }

        public void GetCPUData(DataTable dtSystemDetails)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();


            String cpuName = string.Empty;
            string cpuFullName = string.Empty;
            ManagementObjectSearcher searcher = new
                ManagementObjectSearcher("select * from Win32_Processor");
            foreach (ManagementObject o in searcher.Get())
            {
                sb.AppendFormat("<i>{0}</i><p/>", o.ToString());


                foreach (PropertyData prop in o.Properties)
                {
                    sb.AppendFormat("Name: {0} Value : {1}<br/>", prop.Name, prop.Value);
                    if (prop.Name == "SerialNumber")
                        cpuFullName = (String)prop.Value;
                    if (prop.Name == "Name")
                        //cpuFullName = "Processor : " + (String)prop.Value;

                        dtSystemDetails.Rows.Add("Processor", (String)prop.Value);
                }
            }


            //Response.Write(cpuFullName + "<br/>");
        }

        private string formatBytes(float bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = 0;
            for (i = 0; (int)(bytes / 1024) > 0; i++, bytes /= 1024)
                dblSByte = bytes / 1024.0;
            return String.Format("{0:0.00} {1}", dblSByte, Suffix[i]);
        }

    }
}