using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using PrintJobProvider;
using System.Text;
using System.Net.Sockets;
using System.IO;
using ApplicationAuditor;
using System.Configuration;
using System.Collections;

namespace AccountingPlusEA.NonOSA
{
    public partial class JobList : System.Web.UI.Page
    {
        static string userSource = string.Empty;
        static string domainName = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] != null)
            {
                LabelUser.Text = LabelUserHeader.Text = Convert.ToString(Session["Username"]);
            }
            BindJoblist();

            if (!IsPostBack)
            {
                BindMFPs();
            }
        }

        private void BindMFPs()
        {
            DataSet dsDevices = DataManagerDevice.ProviderDevice.Device.ProvideDevices();
            if (dsDevices.Tables[0].Rows.Count > 0)
            {
                DropDownListMFPs.DataSource = dsDevices;
                DropDownListMFPs.DataTextField = "MFP_IP";
                DropDownListMFPs.DataValueField = "MFP_IP";
                DropDownListMFPs.DataBind();
            }
            
        }
        protected void ImageButtonLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("../NonOSA/TabLogin.aspx");
        }
        private void BindJoblist()
        {
            DataTable dtPrintJobsOriginal = new DataTable();
            dtPrintJobsOriginal.Locale = CultureInfo.InvariantCulture;
            userSource = Session["UserSource"] as string;
            if (Session["UserSource"] == null)
            {
                string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
                Session["UserSource"] = DataManagerDevice.ProviderDevice.Device.ProvideDeviceAuthenticationSource(deviceIpAddress);
                userSource = Session["UserSource"] as string;
            }

            dtPrintJobsOriginal = FileServerPrintJobProvider.ProvidePrintJobs(Session["UserID"].ToString(), userSource, domainName);

            GridViewJobs.DataSource = dtPrintJobsOriginal;
            GridViewJobs.DataBind();
            GridViewJobs.Visible = true;
        }

        protected void ImageButtonPrint_Click(object sender, EventArgs e)
        {
            bool printandDelete = false;
            Print(printandDelete);
            BindJoblist();
        }

        protected void ImageButtonPrintDelete_Click(object sender, EventArgs e)
        {
            bool printandDelete = true;
            Print(printandDelete);
            BindJoblist();
        }

        protected void ImageButtonDelete_Click(object sender, EventArgs e)
        {
            string printJobsLocation = ConfigurationManager.AppSettings["PrintJobsLocation"];
            string jobName = string.Empty;
            string userId = Convert.ToString(Session["UserID"]);
            ArrayList sbJobs = new ArrayList();
            foreach (GridViewRow row in GridViewJobs.Rows)
            {
                // Access the CheckBox
                CheckBox cb = (CheckBox)row.FindControl("cbSelect");
                if (cb != null && cb.Checked)
                {
                    sbJobs.Add(GridViewJobs.Rows[row.RowIndex].Cells[3].Text + ",");
                }
            }
            foreach (string i in sbJobs)
            {
                jobName = i;

                jobName = jobName.Remove(jobName.Length - 1, 1);
                if (!string.IsNullOrEmpty(userSource))
                {
                    printJobsLocation = Path.Combine(printJobsLocation, userSource);
                }
                if (userSource == "AD")
                {
                    if (string.IsNullOrEmpty(domainName))
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(userId))
                            {
                                DataSet dsUserDetails = DataManagerDevice.ProviderDevice.Users.ProvideUserDetails(userId, userSource);
                                if (dsUserDetails != null && dsUserDetails.Tables[0].Rows.Count > 0)
                                {
                                    string domainNameUser = dsUserDetails.Tables[0].Rows[0]["USR_DOMAIN"].ToString();
                                    string printJobDomainName = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainName(domainName);
                                    domainName = printJobDomainName;
                                }
                            }
                        }
                        catch
                        {

                        }
                    }
                    if (!string.IsNullOrEmpty(domainName))
                    {
                        printJobsLocation = Path.Combine(printJobsLocation, domainName);
                    }
                    else
                    {

                    }

                }
                if (!string.IsNullOrEmpty(userId))
                {
                    printJobsLocation = Path.Combine(printJobsLocation, userId);
                }
                if (!string.IsNullOrEmpty(jobName))
                {
                    printJobsLocation = Path.Combine(printJobsLocation, jobName);
                }
                DeletePrintJobFile(printJobsLocation);
            }
            BindJoblist();
        }

        protected void ImageButtonFastPrint_Click(object sender, EventArgs e)
        {
            ToggleCheckState(true);
            bool printandDelete = true;
            Print(printandDelete);
        }


        private void Print(bool printandDelete)
        {
            string ipAddress = string.Empty;
            string port = string.Empty;
            string userId = Convert.ToString(Session["UserID"]);

            string printJobsLocation = ConfigurationManager.AppSettings["PrintJobsLocation"];
            string jobName = string.Empty;

            ipAddress = DropDownListMFPs.SelectedValue;

            if (!string.IsNullOrEmpty(ipAddress))
            {
                port = DataManagerDevice.ProviderDevice.Device.GetPort(ipAddress);
            }
            ArrayList sbJobs = new ArrayList();
            foreach (GridViewRow row in GridViewJobs.Rows)
            {
                // Access the CheckBox
                CheckBox cb = (CheckBox)row.FindControl("cbSelect");
                if (cb != null && cb.Checked)
                {
                    sbJobs.Add(GridViewJobs.Rows[row.RowIndex].Cells[3].Text + ",");
                }
            }

            foreach (string i in sbJobs)
            {
                jobName = i;

                jobName = jobName.Remove(jobName.Length - 1, 1);
                if (!string.IsNullOrEmpty(userSource))
                {
                    printJobsLocation = Path.Combine(printJobsLocation, userSource);
                }
                if (userSource == "AD")
                {
                    if (string.IsNullOrEmpty(domainName))
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(userId))
                            {
                                DataSet dsUserDetails = DataManagerDevice.ProviderDevice.Users.ProvideUserDetails(userId, userSource);
                                if (dsUserDetails != null && dsUserDetails.Tables[0].Rows.Count > 0)
                                {
                                    string domainNameUser = dsUserDetails.Tables[0].Rows[0]["USR_DOMAIN"].ToString();
                                    string printJobDomainName = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainName(domainName);
                                    domainName = printJobDomainName;
                                }
                            }
                        }
                        catch
                        {

                        }
                    }
                    if (!string.IsNullOrEmpty(domainName))
                    {
                        printJobsLocation = Path.Combine(printJobsLocation, domainName);
                    }
                    else
                    {

                    }

                }
                if (!string.IsNullOrEmpty(userId))
                {
                    printJobsLocation = Path.Combine(printJobsLocation, userId);
                }
                if (!string.IsNullOrEmpty(jobName))
                {
                    printJobsLocation = Path.Combine(printJobsLocation, jobName);
                }
                if (!string.IsNullOrEmpty(ipAddress) || !string.IsNullOrEmpty(port))
                {
                    UploadFileViaTCP(ipAddress, port, printJobsLocation, printandDelete);
                }
            }
        }

        private void ToggleCheckState(bool checkState)
        {
            // Iterate through the Products.Rows property
            foreach (GridViewRow row in GridViewJobs.Rows)
            {
                // Access the CheckBox
                CheckBox cb = (CheckBox)row.FindControl("cbSelect");
                if (cb != null)
                    cb.Checked = checkState;
            }
        }

        //protected void CheckAll_Click(object sender, EventArgs e)
        //{
        //    ToggleCheckState(true);
        //}
        //protected void UncheckAll_Click(object sender, EventArgs e)
        //{
        //    ToggleCheckState(false);
        //}

        public  void UploadFileViaTCP(string deviceIPAddress, string tcpPortString, string printJobsLocation, bool isDeleteFile)
        {
            byte[] SendingBuffer = null;
            TcpClient client = null;
            NetworkStream netstream = null;
            int BufferSize = 8 * 1024 * 1024;


            int tcpPort = 9100;

            try
            {
                tcpPort = int.Parse(tcpPortString);
            }
            catch
            { }

            try
            {

                client = new TcpClient(deviceIPAddress, tcpPort);

                netstream = client.GetStream();
                FileStream jobStream = new FileStream(printJobsLocation, FileMode.Open, FileAccess.Read);
                int NoOfPackets = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(jobStream.Length) / Convert.ToDouble(BufferSize)));

                int TotalLength = (int)jobStream.Length, CurrentPacketLength, counter = 0;

                for (int i = 0; i < NoOfPackets; i++)
                {
                    if (TotalLength > BufferSize)
                    {
                        CurrentPacketLength = BufferSize;
                        TotalLength = TotalLength - CurrentPacketLength;
                    }
                    else
                    {
                        CurrentPacketLength = TotalLength;
                    }
                    SendingBuffer = new byte[CurrentPacketLength];
                    jobStream.Read(SendingBuffer, 0, CurrentPacketLength);
                    netstream.Write(SendingBuffer, 0, (int)SendingBuffer.Length);
                }

                jobStream.Close();
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage(deviceIPAddress, "TCP File Upload", LogManager.MessageType.Exception, "TCP upload fail", "Check TCP Connection", ex.Message, ex.StackTrace);
            }

            finally
            {
                if (isDeleteFile)
                {
                    DeletePrintJobFile(printJobsLocation);
                }
            }
            //string serverMessage = "Print job submitted succesfully";
            //ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);

        }
        private static void DeletePrintJobFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
                fileName = fileName.Replace(".prn", ".config");
                File.Delete(fileName);
            }
        }

    }
}