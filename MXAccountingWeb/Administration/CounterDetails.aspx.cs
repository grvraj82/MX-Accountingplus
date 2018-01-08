using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationBase;
using System.Windows.Forms;
using System.Net;
using MFPDiscovery;
using System.Xml;
using System.Data;
using ApplicationAuditor;
using AppLibrary;
using System.Data.Common;
using System.Threading.Tasks;
using System.Globalization;
using System.Data.SqlClient;
using AccountingPlusWeb.MasterPages;
using System.Configuration;
using System.IO;

namespace AccountingPlusWeb.Administration
{
    public partial class CounterDetails : ApplicationBasePage
    {
        public static string mfpIPAddress = string.Empty;
        XmlDocument doc = new XmlDocument();
        internal static bool Historybutton = false;
        internal static bool Csvbutton = false;
        internal static bool Updatebutton = false;
        //private const string UPDATE_COUNTERDETAILS = "UpdateProcess";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["UserRole"] as string))
            {
                Response.Redirect("../Web/LogOn.aspx", true);
                return;
            }

            if (!IsPostBack)
            {
                //BindMFPs();
                //GetDeviceDetails();
                GetDetails();
            }

            LinkButton counterdetails = (LinkButton)Master.FindControl("LinkButtonCounterDetails");
            if (counterdetails != null)
            {
                counterdetails.CssClass = "linkButtonSelect_Selected";
            }
        }

        protected void ImageButtonRefresh_Click(object sender, ImageClickEventArgs e)
        {
            //Updatebutton = true;
            //Response.Redirect("CounterStatus.aspx?updatedetails=CounterDetails.aspx&ProcessSessionName=" + UPDATE_COUNTERDETAILS);
            //DivUpdateProgress.Visible = true;
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "ShowMessage(0);", true);
            GetDeviceDetails();
            GetDetails();
        }

        protected void ImageButtonExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            Csvbutton = true;
            GetDeviceDetails();
        }

        //private void ExportToExcel(DataTable datatableClick)
        //{           
        //    DataTable dtClick = (DataTable)Session["dtdatatableClick"];
        //    DataTable toExcel = dtClick;
        //    if (dtClick.Rows.Count == 0)
        //    {
        //        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "NO_REC_TO_EXPORT");
        //        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage, null);
        //        return;
        //    }
        //    string filename = "Counter_ClickDetails";
        //    HttpContext context = HttpContext.Current;
        //    context.Response.Clear();

        //    foreach (DataColumn column in toExcel.Columns)
        //    {
        //        context.Response.Write(column.ColumnName + ",");
        //    }

        //    context.Response.Write(Environment.NewLine);
        //    foreach (DataRow row in toExcel.Rows)
        //    {
        //        for (int i = 0; i < toExcel.Columns.Count; i++)
        //        {
        //            context.Response.Write(row[i].ToString().Replace(",", string.Empty) + ",");
        //        }
        //        context.Response.Write(Environment.NewLine);
        //    }

        //    context.Response.ContentType = "text/csv";
        //    context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + ".csv");
        //    context.Response.End();
        //}

        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }

        protected void ImageButtonHistory_Click(object sender, ImageClickEventArgs e)
        {
            Historybutton = true;
            LabelMFPIP.Visible = true;
            DropDownListMFPs.Visible = true;
            ImageButtonExportToExcel.Visible = false;
            ImageButtonUpdate.Visible = false;
            ImageButtonPublish.Visible = false;
            BindMFPs();
            GetDetails();
        }

        protected void ImageButtonPublish_Click(object sender, ImageClickEventArgs e)
        {
            string returnValue = string.Empty;
            byte[] counterDetails = DataManager.Provider.Device.CounterInformation();

            SmartAccountant.CounterAccountant sAccount = new SmartAccountant.CounterAccountant();
            try
            {
                //Reading proxy settings from database file
                DbDataReader drProxySettingsSettings = DataManager.Provider.Users.ProvideProxySettings();
                string useProxy = string.Empty;
                if (drProxySettingsSettings.HasRows)
                {
                    while (drProxySettingsSettings.Read())
                    {
                        string isProxyEnabled = drProxySettingsSettings["PROXY_ENABLED"] as string;
                        if (isProxyEnabled == "Yes")
                        {
                            string proxyUrl = drProxySettingsSettings["SERVER_URL"] as string;
                            string proxyUserName = drProxySettingsSettings["USER_NAME"] as string;
                            string proxyPass = drProxySettingsSettings["USER_PASSWORD"] as string;
                            string proxyDomain = drProxySettingsSettings["DOMAIN_NAME"] as string;

                            WebProxy proxyObject = new WebProxy(proxyUrl, true);
                            NetworkCredential networkCredential = new NetworkCredential(proxyUserName, proxyPass, proxyDomain);
                            proxyObject.Credentials = networkCredential;
                            sAccount.Proxy = proxyObject;
                        }
                    }
                }

                if (drProxySettingsSettings != null && drProxySettingsSettings.IsClosed == false)
                {
                    drProxySettingsSettings.Close();
                }
            }
            catch 
            {

            }

            string cAccountantUrl = ConfigurationManager.AppSettings["Key9"];


            sAccount.Url = cAccountantUrl;

            string wsResponse = string.Empty;
            string accessId = string.Empty;
            string tokenId = string.Empty;

            string customerId = DataManager.Provider.Settings.ProvideRedistID(out accessId, out tokenId);
            
            try
            {
                //var timeout = new SmartAccountant.CounterAccountant();
                //timeout.Timeout = 150000000;
                sAccount.ProcessCounterRequest(customerId, counterDetails, accessId, tokenId);
            }
            catch (Exception ex)
            {
                //connection failed to webservice
            }
            GetDetails();
        }

        private void BindMFPs()
        {
            DropDownListMFPs.Items.Clear();

            DataSet dsMFPs = DataManager.Provider.Device.ProvideMFPsDetails();
            if (dsMFPs.Tables[0].Rows.Count > 0)
            {
                DropDownListMFPs.DataSource = dsMFPs;
                DropDownListMFPs.DataTextField = "MFP_IP";
                DropDownListMFPs.DataValueField = "MFP_IP";
                DropDownListMFPs.DataBind();
            }
        }  

        private void GetDeviceDetails()
        {
            string macaddress = "";
            string serialNumber = "";
            string modelName = string.Empty;
            DbDataReader drDevices = DataManager.Provider.Device.ProvideDeviceIPAddress();

            DataTable datatableClick = new DataTable("MFP_CLICK");
            datatableClick.Locale = CultureInfo.InvariantCulture;
            datatableClick.Columns.Add("REC_ID", typeof(int));
            datatableClick.Columns.Add("MAC_ADDRESS", typeof(string));
            datatableClick.Columns.Add("MODEL_NAME", typeof(string));
            datatableClick.Columns.Add("SERIAL_NUMBER", typeof(string));
            datatableClick.Columns.Add("PRINT_TOTAL", typeof(string));
            datatableClick.Columns.Add("PRINT_COLOR", typeof(string));
            datatableClick.Columns.Add("PRINT_BW", typeof(string));
            datatableClick.Columns.Add("DUPLEX", typeof(string));
            datatableClick.Columns.Add("COPIES", typeof(string));
            datatableClick.Columns.Add("COPY_BW", typeof(string));
            datatableClick.Columns.Add("COPY_COLOR", typeof(string));
            datatableClick.Columns.Add("TWO_COLOR_COPY_COUNT", typeof(string));
            datatableClick.Columns.Add("SINGLE_COLOR_COPY_COUNT", typeof(string));
            datatableClick.Columns.Add("BW_TOTAL_COUNT", typeof(string));
            datatableClick.Columns.Add("FULL_COLOR_TOTAL_COUNT", typeof(string));
            datatableClick.Columns.Add("TWO_COLOR_TOTAL_COUNT", typeof(string));
            datatableClick.Columns.Add("SINGLE_COLOR_TOTAL_COUNT", typeof(string));
            datatableClick.Columns.Add("BW_OTHER_COUNT", typeof(string));
            datatableClick.Columns.Add("FULL_COLOR_OTHER_COUNT", typeof(string));
            datatableClick.Columns.Add("SCAN_TO_HDD", typeof(string));
            datatableClick.Columns.Add("BW_SCAN_TO_HDD", typeof(string));
            datatableClick.Columns.Add("COLOR_SCAN_TO_HDD", typeof(string));
            datatableClick.Columns.Add("TWO_COLOR_SCAN_HDD", typeof(string));
            datatableClick.Columns.Add("TOTAL_DOC_FILING_PRINT", typeof(string));
            datatableClick.Columns.Add("BW_DOC_FILING_PRINT", typeof(string));
            datatableClick.Columns.Add("COLOR_DOC_FILING_PRINT", typeof(string));
            datatableClick.Columns.Add("TWO_COLOR_DOC_FILING_PRINT", typeof(string));
            datatableClick.Columns.Add("DOCUMENT_FEEDER", typeof(string));
            datatableClick.Columns.Add("TOTAL_SCAN_TO_EMAIL_FTP", typeof(string));
            datatableClick.Columns.Add("BW_SCAN", typeof(string));
            datatableClick.Columns.Add("COLOR_SCAN", typeof(string));
            datatableClick.Columns.Add("REC_CDATE", typeof(string));
            datatableClick.Columns.Add("REC_MDATE", typeof(string));
            datatableClick.Columns.Add("FAX_SEND", typeof(string));
            datatableClick.Columns.Add("FAX_RECEIVE", typeof(string));
            datatableClick.Columns.Add("IFAX_SEND_COUNT", typeof(string));

            DataTable datatablePage = new DataTable("MFP_PAPER");
            datatablePage.Locale = CultureInfo.InvariantCulture;
            datatablePage.Columns.Add("REC_ID", typeof(int));
            datatablePage.Columns.Add("MAC_ADDRESS", typeof(string));
            datatablePage.Columns.Add("MODEL_NAME", typeof(string));
            datatablePage.Columns.Add("SERIAL_NUMBER", typeof(string));
            //  DatatablePage.Columns.Add("BYPASS_TRAY",typeof(string));
            datatablePage.Columns.Add("TRAY1", typeof(string));
            datatablePage.Columns.Add("TRAY2", typeof(string));
            datatablePage.Columns.Add("TRAY3", typeof(string));
            datatablePage.Columns.Add("TRAY4", typeof(string));
            datatablePage.Columns.Add("TRAY5", typeof(string));
            datatablePage.Columns.Add("TRAY6", typeof(string));
            datatablePage.Columns.Add("TRAY7", typeof(string));
            datatablePage.Columns.Add("TRAY8", typeof(string));
            //datatablePage.Columns.Add("AUTO_SELECT",typeof(string));
            datatablePage.Columns.Add("REC_CDATE", typeof(string));
            datatablePage.Columns.Add("REC_MDATE", typeof(string));

            DataTable datatableToner = new DataTable("MFP_TONER");
            datatableToner.Locale = CultureInfo.InvariantCulture;
            datatableToner.Columns.Add("REC_ID", typeof(int));
            datatableToner.Columns.Add("MODEL_NAME", typeof(string));
            datatableToner.Columns.Add("MAC_ADDRESS", typeof(string));
            datatableToner.Columns.Add("SERIAL_NUMBER", typeof(string));
            datatableToner.Columns.Add("CYAN", typeof(string));
            datatableToner.Columns.Add("YELLOW", typeof(string));
            datatableToner.Columns.Add("MAGENTA", typeof(string));
            datatableToner.Columns.Add("BLACK", typeof(string));
            datatableToner.Columns.Add("REC_CDATE", typeof(string));
            datatableToner.Columns.Add("REC_MDATE", typeof(string));

            if (drDevices.HasRows)
            {
                while (drDevices.Read())
                {
                    modelName = drDevices["MFP_MODEL"].ToString();
                    macaddress = drDevices["MFP_MAC_ADDRESS"].ToString();
                    mfpIPAddress = drDevices["MFP_IP"].ToString();
                    serialNumber = drDevices["MFP_SERIALNUMBER"].ToString();
                    //Task taskcounterdetails = Task.Factory.StartNew(() => DisplayCounterDetails(modelName, macaddress, mfpIPAddress, serialNumber, datatableClick, datatablePage, datatableToner));

                    DisplayCounterDetails(modelName, macaddress, mfpIPAddress, serialNumber, datatableClick, datatablePage, datatableToner);
                }
            }

            if (drDevices != null && drDevices.IsClosed == false)
            {
                drDevices.Close();
            }
            
            DataSet dsCounterDetails = new DataSet();

            dsCounterDetails.Tables.Add(datatableClick);
            dsCounterDetails.Tables.Add(datatableToner);
            dsCounterDetails.Tables.Add(datatablePage);

            //Session["dtdatatableClick"] = datatableClick;
            //Session["dtdatatablePage"] = datatablePage;
            //Session["dtdatatableToner"] = datatableToner;
            //Session["dsCounterDetails"] = dsCounterDetails;
            if (Csvbutton == true)
            {
                ExportToExcel(dsCounterDetails);
            }

            if (dsCounterDetails.Tables.Count > 0)
            {
                string insertIntoTable = DataManager.Controller.Device.InsertCounterDetails(dsCounterDetails);
            }
        }

        private void ExportToExcel(DataSet dsCounterDetails)
        {
            DataSet toExcel = dsCounterDetails;

            string filename = "Counter_ClickDetails";
            HttpContext context = HttpContext.Current;
            context.Response.Clear();

            for (int i = 0; i < dsCounterDetails.Tables.Count; i++)
            {
                if (dsCounterDetails.Tables[i].Rows.Count == 0)
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "NO_REC_TO_EXPORT");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage, null);
                    continue;
                }

                foreach (DataColumn column in toExcel.Tables[i].Columns)
                {
                    context.Response.Write(column.ColumnName + ",");
                }
                context.Response.Write(Environment.NewLine);

                foreach (DataRow row in toExcel.Tables[i].Rows)
                {
                    for (int index = 0; index < toExcel.Tables[i].Columns.Count; index++)
                    {
                        context.Response.Write(row[index].ToString().Replace(",", string.Empty) + ",");
                    }
                    context.Response.Write(Environment.NewLine);
                }
                context.Response.Write(Environment.NewLine);
            }
            context.Response.ContentType = "text/csv";
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + ".csv");
            context.Response.End();

            Csvbutton = false;
        }

        private object DisplayCounterDetails(string modelName, string macaddress, string mfpIPAddress, string serialNumber, DataTable datatableClick, DataTable datatablePage, DataTable datatableToner)
        {
            SNMPFunc SNMPGet = new SNMPFunc();
            string ipAddress = mfpIPAddress;
            IPAddress ip;

            bool parsed = IPAddress.TryParse(ipAddress, out ip);
            if (!parsed)
            {
                //"Invalid IP Address"
            }
            else
            {
                try
                {
                    doc = SNMPGet.Get(ip);

                    #region COUNTER
                    XmlNode xmlDeviceDetails = doc.SelectSingleNode("Source/Result/CounterDetails");
                    DataRow drCounter = datatableClick.NewRow();
                    foreach (XmlNode xmlCounter in xmlDeviceDetails.ChildNodes)
                    {
                        string counterName = xmlCounter.FirstChild.Value;
                        string counterValue = xmlCounter.SelectSingleNode("CounterValue").InnerText;

                        switch (counterName)
                        {
                            case "Prints":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["PRINT_TOTAL"] = counterValue;//Convert.ToInt32(counterValue, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Black & White Print Count":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["PRINT_BW"] = counterValue;//Convert.ToInt32(counterValue, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Full Color Print Count":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["PRINT_COLOR"] = counterValue; //Convert.ToInt32(counterValue, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Duplex":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["DUPLEX"] = counterValue; //Convert.ToInt32(counterValue, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Copies":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["COPIES"] = counterValue; //Convert.ToInt32(counterValue, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Black & White Copy Count":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["COPY_BW"] = counterValue; //Convert.ToInt32(counterValue, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Full Color Copy Count":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["COPY_COLOR"] = counterValue; // Convert.ToInt32(counterValue, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Two Color Copy Count":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["TWO_COLOR_COPY_COUNT"] = counterValue; // Convert.ToInt32(counterValue, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Single Color Copy Count":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["SINGLE_COLOR_COPY_COUNT"] = counterValue; // Convert.ToInt32(counterValue, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Black & White Total Count":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["BW_TOTAL_COUNT"] = counterValue; // Convert.ToInt32(counterValue, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Full Color Total Count":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["FULL_COLOR_TOTAL_COUNT"] = counterValue; // Convert.ToInt32(counterValue, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Two Color Total Count":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["TWO_COLOR_TOTAL_COUNT"] = counterValue; // Convert.ToInt32(counterValue, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Single Color Total Count":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["SINGLE_COLOR_TOTAL_COUNT"] = counterValue; // Convert.ToInt32(counterValue, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Black & White Other Count":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["BW_OTHER_COUNT"] = counterValue; // Convert.ToInt32(counterValue, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Full Color Other Count":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["FULL_COLOR_OTHER_COUNT"] = counterValue; // Convert.ToInt32(counterValue, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Scan to HDD":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["SCAN_TO_HDD"] = counterValue; // Convert.ToInt32(counterValue, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Black & White Scan to HDD Count":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["BW_SCAN_TO_HDD"] = counterValue; // Convert.ToInt32(counterValue, CultureInfo.CurrentCulture);
                                }
                                break;

                            case "Full Color Scan to HDD Count":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["COLOR_SCAN_TO_HDD"] = counterValue; // Convert.ToInt32(counterValue, CultureInfo.CurrentCulture);
                                }
                                break;

                            case "Prints(Document Filing)":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["TOTAL_DOC_FILING_PRINT"] = counterValue; // Convert.ToInt32(counterValue, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Black & White Document Filing Print Count":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["BW_DOC_FILING_PRINT"] = counterValue; // Convert.ToInt32(counterValue, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Full Color Document Filing Print Count":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["COLOR_DOC_FILING_PRINT"] = counterValue; // Convert.ToInt32(counterValue, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Two Color Document Filing Print Count":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["TWO_COLOR_DOC_FILING_PRINT"] = counterValue; // Convert.ToInt32(counterValue, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Document Feeder":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["DOCUMENT_FEEDER"] = counterValue; // Convert.ToInt32(counterValue, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Scan to E-mail/FTP":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["TOTAL_SCAN_TO_EMAIL_FTP"] = counterValue; // Convert.ToInt32(counterValue, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Black & White Scanner Count":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["BW_SCAN"] = counterValue; // Convert.ToInt32(counterValue, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Full Color Scanner Count":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["COLOR_SCAN"] = counterValue; // Convert.ToInt32(counterValue, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Two Color Scan to HDD Count":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["TWO_COLOR_SCAN_HDD"] = counterValue; // Convert.ToInt32(counterValue, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Fax Send":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["FAX_SEND"] = counterValue;
                                }
                                break;
                            case "Fax Receive":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["FAX_RECEIVE"] = counterValue;
                                }
                                break;
                            case "I-Fax Send Count":
                                if (!string.IsNullOrEmpty(counterValue))
                                {
                                    drCounter["IFAX_SEND_COUNT"] = counterValue;
                                }
                                break;
                        }
                    }

                    drCounter["MODEL_NAME"] = modelName;
                    drCounter["MAC_ADDRESS"] = macaddress;
                    drCounter["SERIAL_NUMBER"] = serialNumber;
                    drCounter["REC_CDATE"] = DateTime.Now.ToString();
                    drCounter["REC_MDATE"] = DateTime.Now.ToString();
                    datatableClick.Rows.Add(drCounter);
                    #endregion

                    #region PAPER
                    xmlDeviceDetails = doc.SelectSingleNode("Source/Result/TrayDetails");
                    DataRow drPage = datatablePage.NewRow();

                    foreach (XmlNode xmlTray in xmlDeviceDetails.ChildNodes)
                    {
                        string trayName = xmlTray.FirstChild.Value;
                        string trayPaperSize = xmlTray.SelectSingleNode("TrayPaperSize").InnerText;
                        string trayCurrent = xmlTray.SelectSingleNode("TrayCurrent").InnerText;
                        string trayCapacity = xmlTray.SelectSingleNode("TrayCapacity").InnerText;
                        string trayUnits = xmlTray.SelectSingleNode("TrayUnits").InnerText;

                        switch (trayName)
                        {
                            //case "BypassTray":
                            //    if (!string.IsNullOrEmpty(trayName))
                            //    {
                            //        drPage["BYPASS_TRAY"] = Convert.ToInt32(trayCurrent, CultureInfo.CurrentCulture);
                            //    }
                            case "Tray 1":
                                if (!string.IsNullOrEmpty(trayCurrent))
                                {

                                    drPage["TRAY1"] = trayCurrent;// Convert.ToInt32(trayCurrent, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Tray 2":
                                if (!string.IsNullOrEmpty(trayCurrent))
                                {
                                    drPage["TRAY2"] = trayCurrent;// Convert.ToInt32(trayCurrent, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Tray 3":
                                if (!string.IsNullOrEmpty(trayCurrent))
                                {
                                    drPage["TRAY3"] = trayCurrent; // Convert.ToInt32(trayCurrent, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Tray 4":
                                if (!string.IsNullOrEmpty(trayCurrent))
                                {
                                    drPage["TRAY4"] = trayCurrent;// Convert.ToInt32(trayCurrent, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Tray 5":
                                if (!string.IsNullOrEmpty(trayCurrent))
                                {
                                    drPage["TRAY5"] = "0";// Convert.ToInt32(trayCurrent, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Tray 6":
                                if (!string.IsNullOrEmpty(trayCurrent))
                                {
                                    drPage["TRAY6"] = "0";// Convert.ToInt32(trayCurrent, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Tray 7":
                                if (!string.IsNullOrEmpty(trayCurrent))
                                {
                                    drPage["TRAY7"] = "0";// Convert.ToInt32(trayCurrent, CultureInfo.CurrentCulture);
                                }
                                break;
                            case "Tray 8":
                                if (!string.IsNullOrEmpty(trayCurrent))
                                {
                                    drPage["TRAY8"] = "0";// Convert.ToInt32(trayCurrent, CultureInfo.CurrentCulture);
                                }
                                break;
                            //case "Auto Select":
                            //    if (!string.IsNullOrEmpty(trayCurrent))
                            //    {
                            //        drPage["AUTO_SELECT"] = Convert.ToInt32(trayCurrent, CultureInfo.CurrentCulture);
                            //    }
                            //    break;
                        }
                    }

                    drPage["MODEL_NAME"] = modelName;
                    drPage["MAC_ADDRESS"] = macaddress;
                    drPage["SERIAL_NUMBER"] = serialNumber;
                    drPage["REC_CDATE"] = DateTime.Now.ToString();
                    drPage["REC_MDATE"] = DateTime.Now.ToString();
                    datatablePage.Rows.Add(drPage);
                    #endregion

                    #region TONER
                    xmlDeviceDetails = doc.SelectSingleNode("Source/Result/TonerDetails");
                    DataRow drToner = datatableToner.NewRow();
                    foreach (XmlNode xmlToner in xmlDeviceDetails.ChildNodes)
                    {
                        string tonerName = xmlToner.FirstChild.Value;
                        string tonertrayCurrent = xmlToner.SelectSingleNode("TrayCurrent").InnerText;
                        string tonertrayCapacity = xmlToner.SelectSingleNode("TrayCapacity").InnerText;

                        switch (tonerName.ToLower())
                        {
                            case "cyan":
                                if (!string.IsNullOrEmpty(tonertrayCurrent))
                                {
                                    drToner["CYAN"] = tonertrayCurrent; //Convert.ToInt32(tonertrayCurrent, CultureInfo.CurrentCulture);
                                }
                                break;

                            case "magenta":
                                if (!string.IsNullOrEmpty(tonertrayCurrent))
                                {
                                    drToner["MAGENTA"] = tonertrayCurrent; //Convert.ToInt32(tonertrayCurrent, CultureInfo.CurrentCulture);
                                }
                                break;

                            case "yellow":
                                if (!string.IsNullOrEmpty(tonertrayCurrent))
                                {
                                    drToner["YELLOW"] = tonertrayCurrent; //Convert.ToInt32(tonertrayCurrent, CultureInfo.CurrentCulture);
                                }
                                break;

                            case "black":
                                if (!string.IsNullOrEmpty(tonertrayCurrent))
                                {
                                    drToner["BLACK"] = tonertrayCurrent;// Convert.ToInt32(tonertrayCurrent, CultureInfo.CurrentCulture);
                                }
                                break;
                        }
                    }

                    drToner["MODEL_NAME"] = modelName;
                    drToner["MAC_ADDRESS"] = macaddress;
                    drToner["SERIAL_NUMBER"] = serialNumber;
                    drToner["REC_CDATE"] = DateTime.Now.ToString();
                    drToner["REC_MDATE"] = DateTime.Now.ToString();
                    datatableToner.Rows.Add(drToner);
                    #endregion

                }
                catch (Exception ex)
                {

                }
            }
            return ip;
        }

        private void GetDetails()
        {
            TableRow trHeaderFirstRow = TableCounterDetails.Rows[0];
            TableRow trHeaderSecondRow = TableCounterDetails.Rows[1];
            TableCounterDetails.Rows.Clear();
            TableCounterDetails.Rows.AddAt(0, trHeaderFirstRow);
            TableCounterDetails.Rows.AddAt(1, trHeaderSecondRow);
            
            DataSet dsDevices = null;

            if (Historybutton == true)
            {
                ImageButtonHistory.Visible = false;
                mfpIPAddress = DropDownListMFPs.SelectedValue;
                dsDevices = DataManager.Provider.Device.ProvideMFPCounterHistory(mfpIPAddress);
            }
            else
            {
                dsDevices = DataManager.Provider.Device.ProvideCounterDetailsDS();
            }

            for (int rowIndex = 1; rowIndex < dsDevices.Tables[0].Rows.Count; rowIndex++)
            {
                TableRow trCount = new TableRow();
                AppController.StyleTheme.SetGridRowStyle(trCount);

                TableCell tdSlNo = new TableCell();
                tdSlNo.HorizontalAlign = HorizontalAlign.Left;
                tdSlNo.Text = rowIndex.ToString();

                TableCell tdDate = new TableCell();
                tdDate.Text = dsDevices.Tables[0].Rows[rowIndex]["REC_CDATE"].ToString();
                tdDate.Wrap = false;
                tdDate.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdCountMfpMacAddress = new TableCell();
                tdCountMfpMacAddress.Text = dsDevices.Tables[0].Rows[rowIndex]["MAC_ADDRESS"].ToString();
                tdCountMfpMacAddress.Wrap = false;
                //tdMacAddress.CssClass = "GridLeftAlign";

                TableCell tdModelname = new TableCell();
                tdModelname.Text = dsDevices.Tables[0].Rows[rowIndex]["MODEL_NAME"].ToString();
                tdModelname.Wrap = false;
                tdModelname.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdCountSerialNum = new TableCell();
                tdCountSerialNum.Text = dsDevices.Tables[0].Rows[rowIndex]["SERIAL_NUMBER"].ToString();
                tdCountSerialNum.Text = "<a href='CounterInformation.aspx?sno=" + dsDevices.Tables[0].Rows[rowIndex]["SERIAL_NUMBER"].ToString() + "'  target='_blank'>" + dsDevices.Tables[0].Rows[rowIndex]["SERIAL_NUMBER"].ToString() + "</a>";
                tdCountSerialNum.Wrap = false;
                tdCountSerialNum.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdPrinttotal = new TableCell();
                tdPrinttotal.Text = dsDevices.Tables[0].Rows[rowIndex]["PRINT_TOTAL"].ToString();
                tdPrinttotal.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdPrintbw = new TableCell();
                tdPrintbw.Text = dsDevices.Tables[0].Rows[rowIndex]["PRINT_BW"].ToString();
                tdPrintbw.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdPrintcolor = new TableCell();
                tdPrintcolor.Text = dsDevices.Tables[0].Rows[rowIndex]["PRINT_COLOR"].ToString();
                tdPrintcolor.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdduplex = new TableCell();
                tdduplex.Text = dsDevices.Tables[0].Rows[rowIndex]["DUPLEX"].ToString();
                tdduplex.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdcopies = new TableCell();
                tdcopies.Text = dsDevices.Tables[0].Rows[rowIndex]["COPIES"].ToString();
                tdcopies.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdcopybw = new TableCell();
                tdcopybw.Text = dsDevices.Tables[0].Rows[rowIndex]["COPY_BW"].ToString();
                tdcopybw.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdCopycolor = new TableCell();
                tdCopycolor.Text = dsDevices.Tables[0].Rows[rowIndex]["COPY_COLOR"].ToString();
                tdCopycolor.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdTwoColorcopycount = new TableCell();
                tdTwoColorcopycount.Text = dsDevices.Tables[0].Rows[rowIndex]["TWO_COLOR_COPY_COUNT"].ToString();
                tdTwoColorcopycount.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdSingleColorcopycount = new TableCell();
                tdSingleColorcopycount.Text = dsDevices.Tables[0].Rows[rowIndex]["SINGLE_COLOR_COPY_COUNT"].ToString();
                tdSingleColorcopycount.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdBWTotalcount = new TableCell();
                tdBWTotalcount.Text = dsDevices.Tables[0].Rows[rowIndex]["BW_TOTAL_COUNT"].ToString();
                tdBWTotalcount.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdFullcolortotalcount = new TableCell();
                tdFullcolortotalcount.Text = dsDevices.Tables[0].Rows[rowIndex]["FULL_COLOR_TOTAL_COUNT"].ToString();
                tdFullcolortotalcount.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdTwocolorTotalcount = new TableCell();
                tdTwocolorTotalcount.Text = dsDevices.Tables[0].Rows[rowIndex]["TWO_COLOR_TOTAL_COUNT"].ToString();
                tdTwocolorTotalcount.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdSingleColortotalcount = new TableCell();
                tdSingleColortotalcount.Text = dsDevices.Tables[0].Rows[rowIndex]["SINGLE_COLOR_TOTAL_COUNT"].ToString();
                tdSingleColortotalcount.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdBWothercount = new TableCell();
                tdBWothercount.Text = dsDevices.Tables[0].Rows[rowIndex]["BW_OTHER_COUNT"].ToString();
                tdBWothercount.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdFullColorOthercount = new TableCell();
                tdFullColorOthercount.Text = dsDevices.Tables[0].Rows[rowIndex]["FULL_COLOR_OTHER_COUNT"].ToString();
                tdFullColorOthercount.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdScanHDD = new TableCell();
                tdScanHDD.Text = dsDevices.Tables[0].Rows[rowIndex]["SCAN_TO_HDD"].ToString();
                tdScanHDD.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdBWScanHDD = new TableCell();
                tdBWScanHDD.Text = dsDevices.Tables[0].Rows[rowIndex]["BW_SCAN_TO_HDD"].ToString();
                tdBWScanHDD.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdColorScanHDD = new TableCell();
                tdColorScanHDD.Text = dsDevices.Tables[0].Rows[rowIndex]["COLOR_SCAN_TO_HDD"].ToString();
                tdColorScanHDD.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdTwoColorScanHDD = new TableCell();
                tdTwoColorScanHDD.Text = dsDevices.Tables[0].Rows[rowIndex]["TWO_COLOR_SCAN_HDD"].ToString();
                tdTwoColorScanHDD.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdTotalDocFilingPrint = new TableCell();
                tdTotalDocFilingPrint.Text = dsDevices.Tables[0].Rows[rowIndex]["TOTAL_DOC_FILING_PRINT"].ToString();
                tdTotalDocFilingPrint.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdBWDocFilingPrint = new TableCell();
                tdBWDocFilingPrint.Text = dsDevices.Tables[0].Rows[rowIndex]["BW_DOC_FILING_PRINT"].ToString();
                tdBWDocFilingPrint.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdColorDocfilingPrint = new TableCell();
                tdColorDocfilingPrint.Text = dsDevices.Tables[0].Rows[rowIndex]["COLOR_DOC_FILING_PRINT"].ToString();
                tdColorDocfilingPrint.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdTwoColorDocfilingPrint = new TableCell();
                tdTwoColorDocfilingPrint.Text = dsDevices.Tables[0].Rows[rowIndex]["TWO_COLOR_DOC_FILING_PRINT"].ToString();
                tdTwoColorDocfilingPrint.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdDocumentFeeder = new TableCell();
                tdDocumentFeeder.Text = dsDevices.Tables[0].Rows[rowIndex]["DOCUMENT_FEEDER"].ToString();
                tdDocumentFeeder.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdFaxsend = new TableCell();
                tdFaxsend.Text = dsDevices.Tables[0].Rows[rowIndex]["FAX_SEND"].ToString();
                tdFaxsend.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdFaxreceive = new TableCell();
                tdFaxreceive.Text = dsDevices.Tables[0].Rows[rowIndex]["FAX_RECEIVE"].ToString();
                tdFaxreceive.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdIFaxsendcount = new TableCell();
                tdIFaxsendcount.Text = dsDevices.Tables[0].Rows[rowIndex]["IFAX_SEND_COUNT"].ToString();
                tdIFaxsendcount.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdTotalScanEmailFtp = new TableCell();
                tdTotalScanEmailFtp.Text = dsDevices.Tables[0].Rows[rowIndex]["TOTAL_SCAN_TO_EMAIL_FTP"].ToString();
                tdTotalScanEmailFtp.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdBWScan = new TableCell();
                tdBWScan.Text = dsDevices.Tables[0].Rows[rowIndex]["BW_SCAN"].ToString();
                tdBWScan.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdColorScan = new TableCell();
                tdColorScan.Text = dsDevices.Tables[0].Rows[rowIndex]["COLOR_SCAN"].ToString();
                tdColorScan.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdCyan = new TableCell();
                tdCyan.Text = dsDevices.Tables[0].Rows[rowIndex]["CYAN"].ToString();
                tdCyan.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdYellow = new TableCell();
                tdYellow.Text = dsDevices.Tables[0].Rows[rowIndex]["YELLOW"].ToString();
                tdYellow.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdMagenta = new TableCell();
                tdMagenta.Text = dsDevices.Tables[0].Rows[rowIndex]["MAGENTA"].ToString();
                tdMagenta.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdBlack = new TableCell();
                tdBlack.Text = dsDevices.Tables[0].Rows[rowIndex]["BLACK"].ToString();
                tdBlack.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdTray1 = new TableCell();
                tdTray1.Text = dsDevices.Tables[0].Rows[rowIndex]["TRAY1"].ToString();
                tdTray1.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdTray2 = new TableCell();
                tdTray2.Text = dsDevices.Tables[0].Rows[rowIndex]["TRAY2"].ToString();
                tdTray2.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdTray3 = new TableCell();
                tdTray3.Text = dsDevices.Tables[0].Rows[rowIndex]["TRAY3"].ToString();
                tdTray3.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdTray4 = new TableCell();
                tdTray4.Text = dsDevices.Tables[0].Rows[rowIndex]["TRAY4"].ToString();
                tdTray4.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdTray5 = new TableCell();
                tdTray5.Text = dsDevices.Tables[0].Rows[rowIndex]["TRAY5"].ToString();
                tdTray5.HorizontalAlign = HorizontalAlign.Center;

                TableCell tdTray6 = new TableCell();
                tdTray6.Text = dsDevices.Tables[0].Rows[rowIndex]["TRAY6"].ToString();
                tdTray6.HorizontalAlign = HorizontalAlign.Center;

                trCount.Cells.Add(tdSlNo);
                trCount.Cells.Add(tdDate);
                trCount.Cells.Add(tdCountMfpMacAddress);
                trCount.Cells.Add(tdModelname);
                trCount.Cells.Add(tdCountSerialNum);
                trCount.Cells.Add(tdPrinttotal);
                trCount.Cells.Add(tdPrintbw);
                trCount.Cells.Add(tdPrintcolor);
                trCount.Cells.Add(tdduplex);
                trCount.Cells.Add(tdcopies);
                trCount.Cells.Add(tdcopybw);
                trCount.Cells.Add(tdCopycolor);
                trCount.Cells.Add(tdTwoColorcopycount);
                trCount.Cells.Add(tdSingleColorcopycount);
                trCount.Cells.Add(tdBWTotalcount);
                trCount.Cells.Add(tdFullcolortotalcount);
                trCount.Cells.Add(tdTwocolorTotalcount);
                trCount.Cells.Add(tdSingleColortotalcount);
                trCount.Cells.Add(tdBWothercount);
                trCount.Cells.Add(tdFullColorOthercount);
                trCount.Cells.Add(tdScanHDD);
                trCount.Cells.Add(tdBWScanHDD);
                trCount.Cells.Add(tdColorScanHDD);
                trCount.Cells.Add(tdTwoColorScanHDD);
                trCount.Cells.Add(tdTotalDocFilingPrint);
                trCount.Cells.Add(tdBWDocFilingPrint);
                trCount.Cells.Add(tdColorDocfilingPrint);
                trCount.Cells.Add(tdTwoColorDocfilingPrint);
                trCount.Cells.Add(tdDocumentFeeder);
                trCount.Cells.Add(tdFaxsend);
                trCount.Cells.Add(tdFaxreceive);
                trCount.Cells.Add(tdIFaxsendcount);
                trCount.Cells.Add(tdTotalScanEmailFtp);
                trCount.Cells.Add(tdBWScan);
                trCount.Cells.Add(tdColorScan);

                trCount.Cells.Add(tdCyan);
                trCount.Cells.Add(tdYellow);
                trCount.Cells.Add(tdMagenta);
                trCount.Cells.Add(tdBlack);

                trCount.Cells.Add(tdTray1);
                trCount.Cells.Add(tdTray2);
                trCount.Cells.Add(tdTray3);
                trCount.Cells.Add(tdTray4);
                trCount.Cells.Add(tdTray5);
                trCount.Cells.Add(tdTray6);

                TableCounterDetails.Rows.Add(trCount);

                //ImportDataToDataset(tdCountMfpMacAddress.Text, tdModelname.Text, tdCountSerialNum.Text, tdPrinttotal.Text, tdPrintcolor.Text, tdPrintbw.Text, tdduplex.Text,
                //    tdcopies.Text, tdcopybw.Text, tdCopycolor.Text, tdTwoColorcopycount.Text, tdSingleColorcopycount.Text, tdBWTotalcount.Text, tdFullcolortotalcount.Text,
                //    tdTwocolorTotalcount.Text, tdSingleColortotalcount.Text, tdBWothercount.Text, tdFullColorOthercount.Text, tdScanHDD.Text, tdBWScanHDD.Text, tdColorScanHDD.Text,
                //    tdTwoColorScanHDD.Text, tdTotalDocFilingPrint.Text, tdBWDocFilingPrint.Text, tdColorDocfilingPrint.Text, tdTwoColorDocfilingPrint.Text, tdDocumentFeeder.Text,
                //    tdTotalScanEmailFtp.Text, tdBWScan.Text, tdColorScan.Text, tdCyan.Text, tdYellow.Text, tdMagenta.Text, tdBlack.Text, tdTray1.Text, tdTray2.Text, tdTray3.Text, tdTray4.Text,
                //    tdTray5.Text, tdTray6.Text);
            }
            
            //Updatebutton = false;
            Historybutton = false;
        }

        private void ImportDataToDataset(string tdCountMfpMacAddress, string tdModelname, string tdCountSerialNum, string tdPrinttotal, string tdPrintcolor, string tdPrintbw, string tdduplex, string tdcopies, string tdcopybw, string tdCopycolor, string tdTwoColorcopycount, string tdSingleColorcopycount, string tdBWTotalcount, string tdFullcolortotalcount, string tdTwocolorTotalcount, string tdSingleColortotalcount, string tdBWothercount, string tdFullColorOthercount, string tdScanHDD, string tdBWScanHDD, string tdColorScanHDD, string tdTwoColorScanHDD, string tdTotalDocFilingPrint, string tdBWDocFilingPrint, string tdColorDocfilingPrint, string tdTwoColorDocfilingPrint, string tdDocumentFeeder, string tdTotalScanEmailFtp, string tdBWScan, string tdColorScan, string tdCyan, string tdYellow, string tdMagenta, string tdBlack, string tdTray1, string tdTray2, string tdTray3, string tdTray4, string tdTray5, string tdTray6)
        {
            //throw new NotImplementedException();
            //datatableCounterDetails.Rows.Add(tdCountMfpMacAddress, tdModelname, tdCountSerialNum, tdPrinttotal, tdPrintcolor, tdPrintbw, tdduplex, tdcopies, tdcopybw, tdCopycolor, tdTwoColorcopycount, tdSingleColorcopycount, tdBWTotalcount, tdFullcolortotalcount, tdTwocolorTotalcount, tdSingleColortotalcount, tdBWothercount, tdFullColorOthercount, tdScanHDD, tdBWScanHDD, tdColorScanHDD, tdTwoColorScanHDD, tdTotalDocFilingPrint, tdBWDocFilingPrint, tdColorDocfilingPrint, tdTwoColorDocfilingPrint, tdDocumentFeeder, tdTotalScanEmailFtp, tdBWScan, tdColorScan, tdCyan, tdYellow, tdMagenta, tdBlack, tdTray1, tdTray2, tdTray3, tdTray4, tdTray5, tdTray6);
            //datatableCounterDetails.Rows.Add(tdCountMfpMacAddress, tdModelname, tdCountSerialNum, tdPrinttotal, tdPrintcolor, tdPrintbw, tdduplex, tdcopies, tdcopybw, tdCopycolor, tdTwoColorcopycount, tdSingleColorcopycount, tdBWTotalcount, tdFullcolortotalcount, tdTwocolorTotalcount, tdSingleColortotalcount, tdBWothercount, tdFullColorOthercount, tdScanHDD, tdBWScanHDD, tdColorScanHDD, tdTwoColorScanHDD, tdTotalDocFilingPrint, tdBWDocFilingPrint, tdColorDocfilingPrint, tdTwoColorDocfilingPrint, tdDocumentFeeder, tdTotalScanEmailFtp, tdBWScan, tdColorScan, tdCyan, tdYellow, tdMagenta, tdBlack, tdTray1, tdTray2, tdTray3, tdTray4, tdTray5, tdTray6);
        }

        protected void DropDownListMFPs_SelectedIndexChanged(object sender, EventArgs e)
        {
            Historybutton = true;
            GetDetails();
        }

        public DataTable datatableCounterDetails { get; set; }
    }
}