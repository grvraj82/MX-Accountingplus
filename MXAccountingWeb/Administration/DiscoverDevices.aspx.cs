#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Varadharaj
  File Name: DiscoverDevices.aspx
  Description: Discover devices
  Date Created : July 2010
  */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.          
*/
#endregion

using System;
using System.Collections;
using System.Web.UI;
using ApplicationBase;
using System.Threading;
using MFPDiscovery;
using ApplicationAuditor;
using System.Net;
using AppLibrary;
using AccountingPlusWeb.MasterPages;
using System.Web.UI.WebControls;

namespace PrintRoverWeb.Administration
{
    /// <summary>
    /// Discover Devices
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>DiscoverDevices</term>
    ///            <description>Discover Devices</description>
    ///     </item>
    /// </summary>
    /// 
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintRoverWeb.Administration.DiscoverDevices.png" />
    /// </remarks>

    public partial class DiscoverDevices : ApplicationBasePage
    {
        private const string PROCESS_NAME = "Process";
        MFPDiscoverer mfpDiscoverer = null;
        internal static string AUDITORSOURCE = string.Empty;
       

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.DiscoverDevices.Page_Load.jpg"/>
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            AUDITORSOURCE = Session["UserID"] as string;
            if (string.IsNullOrEmpty(Session["UserRole"] as string))
            {
                Response.Redirect("../Web/LogOn.aspx", true);
                return;
            }
            else if (Session["UserRole"] as string != "admin")
            {
                Response.Redirect("~/Administration/UnAuthorisedAccess.aspx");
            }
            if (!IsPostBack)
            {
                TextBoxStartIP.Enabled = false;
                TextBoxEndIP.Enabled = false;
                TextBoxIPAddress.Enabled = false;
            }
            LinkButton manageDevices = (LinkButton)Master.FindControl("LinkButtonManageDevices");
            if (manageDevices != null)
            {
                manageDevices.CssClass = "linkButtonSelect_Selected";
            }
            LocalizeThisPage();

        }

        private void StartAutoDiscovery()
        {
            if (Application["APP_LAUNCH_COUNT"] != null)
            {
                if (int.Parse(Application["APP_LAUNCH_COUNT"].ToString()) == 0)
                {
                    Application.Lock();
                    Application["APP_LAUNCH_COUNT"] = int.Parse(Application["APP_LAUNCH_COUNT"].ToString()) + 1;
                    Application.UnLock();

                    StartDeviceDiscovery();
                }
            }
        }

        /// <summary>
        /// Localizes the this page.
        /// </summary>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "FIND_DEVICES,DEVICES_IN_SUBNET,BY_IP_ADDRESS,BY_IP_RANGE,START_IP,END_IP,DISCOVER_DEVICE,CANCEL,CLICK_BACK";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "INVALID_IP";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            LabelDeviceDiscovery.Text = localizedResources["L_FIND_DEVICES"].ToString();
            RadioButtonDevicesInSubnet.Text = localizedResources["L_DEVICES_IN_SUBNET"].ToString();
            RadioButtonDeviceByIP.Text = localizedResources["L_BY_IP_ADDRESS"].ToString() + "/HostName";
            RadioButtonByIPRange.Text = localizedResources["L_BY_IP_RANGE"].ToString();
            LabelStartIP.Text = localizedResources["L_START_IP"].ToString();
            LabelEndIP.Text = localizedResources["L_END_IP"].ToString();
            ButtonDiscoverDevices.Text = localizedResources["L_DISCOVER_DEVICE"].ToString();
            ButtonCancelDiscovery.Text = localizedResources["L_CANCEL"].ToString();
            ImageButtonBack.ToolTip = localizedResources["L_CLICK_BACK"].ToString();
            //RegularExpressionValidator1.ErrorMessage = localizedResources["S_INVALID_IP"].ToString();
            RegularExpressionValidator2.ErrorMessage = localizedResources["S_INVALID_IP"].ToString();
            RegularExpressionValidator3.ErrorMessage = localizedResources["S_INVALID_IP"].ToString();
        }

        /// <summary>
        /// Handles the Click event of the ButtonDiscoverDevices control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.DiscoverDevices.ButtonDiscoverDevices_Click.jpg"/>
        /// </remarks>
        protected void ButtonDiscoverDevices_Click(object sender, EventArgs e)
        {
            StartDeviceDiscovery();
        }

        private void StartDeviceDiscovery()
        {
            string hostName = string.Empty;
            if (!ValidateUserInput())
            {
                 hostName = GetIpFromHost(TextBoxIPAddress.Text);
                if (!string.IsNullOrEmpty(hostName))
                {
                    if (!ValidateUserInput(hostName))
                    {
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "ENTER_VALID_IP");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage, null);
                        return;
                    }
                }

            }
            if (RadioButtonByIPRange.Checked)
            {
                if (!CompareIP())
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "END_IP_GREATER");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage, null);
                    return;
                }
            }


            mfpDiscoverer = new MFPDiscoverer();
            if (mfpDiscoverer.Initialize())
            {
                mfpDiscoverer.MfpDiscovered += new EventHandler<MfpDiscoveredEventArgs>(OnMfpDiscovered);
                mfpDiscoverer.DiscoverMfpsAsyncCompleted += new EventHandler<EventArgs>(DiscoverMfpsAsyncCompleted);
            }

            //Create and initialize new thread with the address of the StartLongProcess function
            Thread thread = new Thread(new ParameterizedThreadStart(StartDiscovery));

            //Start thread
            thread.Start(mfpDiscoverer);

            //Pass redirect page and session var name to the process wait (interum) page
            Response.Redirect("DiscoveryStatus.aspx?redirectPage=ManageDevice.aspx&ProcessSessionName=" + PROCESS_NAME);
        }

        /// <summary>
        /// Compares the start and end IP.
        /// </summary>
        private bool CompareIP()
        {
            bool returnValue = true;
            string devicestartIP = TextBoxStartIP.Text;
            string devicestartEndIP = TextBoxEndIP.Text;
            double startIp = IPAddressToNumber(devicestartIP);
            double endIp = IPAddressToNumber(devicestartEndIP);
            //Compare start and  IP 
            if (startIp >= endIp)
            {
                returnValue = false;
            }
            else
            {
                returnValue = true;
            }
            return returnValue;
        }

        /// <summary>
        /// Validates the user input.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.DiscoverDevices.ValidateUserInput.jpg"/>
        /// </remarks>
        /// 

        private bool ValidateUserInput()
        {
            bool returnValue = true;

            if (RadioButtonDeviceByIP.Checked)
            {
                string deviceIP = TextBoxIPAddress.Text;
                // Validate IP
                if (!IsValidIPAddress(deviceIP))
                {
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }
            }
            else if (RadioButtonByIPRange.Checked)
            {
                string devicestartIP = TextBoxStartIP.Text;
                string devicestartEndIP = TextBoxEndIP.Text;
                // Validate IP Addresses
                bool isVlaidFromIPAddress = IsValidIPAddress(devicestartIP);

                bool isVlaidToIPAddress = IsValidIPAddress(devicestartEndIP);


                if (isVlaidFromIPAddress == false || isVlaidToIPAddress == false)
                {

                    returnValue = false;
                }
            }
            return returnValue;
        }

        private bool ValidateUserInput(string ipAddress)
        {
            bool returnValue = true;

            
                string deviceIP = ipAddress;
                // Validate IP
                if (!IsValidIPAddress(deviceIP))
                {
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }
            
           
            return returnValue;
        }

        /// <summary>
        /// Handles the Click event of the ButtonCancelDiscovery control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.DiscoverDevices.ButtonCancelDiscovery_Click.jpg"/>
        /// </remarks>
        protected void ButtonCancelDiscovery_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageDevice.aspx", true);
        }

        /// <summary>
        /// Starts the discovery.
        /// </summary>
        /// <param name="mfpDiscoverer">MFP discoverer.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.DiscoverDevices.StartDiscovery.jpg"/>
        /// </remarks>
        private void StartDiscovery(object mfpDiscoverer)
        {

            MFPDiscoverer mfpDiscovery = mfpDiscoverer as MFPDiscoverer;
            //Initialize Session Variable
            Session[PROCESS_NAME] = false;

            try
            {
                if (RadioButtonDevicesInSubnet.Checked)
                {
                    mfpDiscovery.DiscoverMfpsAsync();
                }
                else if (RadioButtonDeviceByIP.Checked)
                {
                    string deviceIP = TextBoxIPAddress.Text;
                    string hostName = string.Empty;
                    if (!ValidateUserInput())
                    {
                        hostName = GetIpFromHost(TextBoxIPAddress.Text);
                        if (!string.IsNullOrEmpty(hostName))
                        {
                            if (!ValidateUserInput(hostName))
                            {
                                mfpDiscovery.DiscoverMfpAsync(deviceIP);
                            }
                            else
                            {
                                mfpDiscovery.DiscoverMfpAsync(hostName);
                            }
                        }
                    }
                    else 
                    {
                        mfpDiscovery.DiscoverMfpAsync(deviceIP);
                    }
                    
                }
                else if (RadioButtonByIPRange.Checked)
                {
                    string devicestartIP = TextBoxStartIP.Text;
                    string devicestartEndIP = TextBoxEndIP.Text;
                    // Validate IP Addresses
                    bool isVlaidFromIPAddress = IsValidIPAddress(devicestartIP);

                    bool isVlaidToIPAddress = IsValidIPAddress(devicestartEndIP);

                    mfpDiscovery.DiscoveryMode = MFPDiscoverer.DiscoveryType.SNMP;
                    mfpDiscovery.DiscoverMfpsAsync(devicestartIP, devicestartEndIP);
                }
            }
            catch (Exception exceptionMessage)
            {
            }
        }

        /// <summary>
        /// Called when [MFP discovered].
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">The <see cref="MFPDiscovery.MfpDiscoveredEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.DiscoverDevices.OnMfpDiscovered.jpg"/>
        /// </remarks>
        private void OnMfpDiscovered(object sender, MfpDiscoveredEventArgs e)
        {

            string discoveredDevice = e.MFP_T.szMfpIpAddress;
            string dnsname = e.MFP_T.szMfpDnsName;
            string location = e.MFP_T.szMfpSysLocation;
            string deviceName = e.MFP_T.szMfpSysName;
            string serialNumber = e.MFP_T.szMfpSerialNumber;
            string modelName = e.MFP_T.szMfpModelName;
            string macAddress = e.MFP_T.szMfpMacAddress;
            string url = "";
            string ftpAddress = discoveredDevice;
            string ftpport = "21";
            string printReleaseProtocol = "FTP";
            string hostName = GetHostName(discoveredDevice);


            DataManager.Controller.Device.RecordDevice(location, serialNumber, deviceName, modelName, macAddress, discoveredDevice, url, ftpAddress, ftpport, printReleaseProtocol, hostName);
        }

        private static string GetHostName(string discoveredDevice)
        {
            string hostName = string.Empty;
            try
            {
                IPHostEntry IpToDomainName = Dns.GetHostEntry(discoveredDevice);
                hostName = IpToDomainName.HostName;
            }
            catch (Exception ex)
            {
                hostName = discoveredDevice;
            }
            return hostName;
        }

        private static string GetIpFromHost(string discoveredDevice)
        {
            string hostName = string.Empty;
            try
            {
                IPHostEntry ipEntry = Dns.GetHostEntry(discoveredDevice);
                IPAddress[] addr = ipEntry.AddressList;

                for (int i = 0; i < addr.Length; i++)
                {
                    hostName = addr[0].ToString();
                }
            }
            catch (Exception ex)
            {
                hostName = discoveredDevice;
            }
            return hostName;
        }

        /// <summary>
        /// Discovers the MFPS async completed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.DiscoverDevices.DiscoverMfpsAsyncCompleted.jpg"/>
        /// </remarks>
        private void DiscoverMfpsAsyncCompleted(object sender, EventArgs e)
        {
            //Set session variable equal to true when process completes
            Session[PROCESS_NAME] = true;
            string auditorSuccessMessage = "Devices discovery completed successfully";
            string auditorSource = HostIP.GetHostIP();

            if (mfpDiscoverer != null)
            {
                mfpDiscoverer.Dispose();
            }
            LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Success, auditorSuccessMessage);
        }

        /// <summary>
        /// Gets the master page.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.DiscoverDevices.GetMasterPage.jpg"/>
        /// </remarks>
        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }

        /// <summary>
        /// Determines whether [is valid IP address] [the specified device IP].
        /// </summary>
        /// <param name="deviceIP">Device IP.</param>
        /// <returns>
        /// 	<c>true</c> if [is valid IP address] [the specified device IP]; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.DiscoverDevices.IsValidIPAddress.jpg"/>
        /// </remarks>
        private bool IsValidIPAddress(string deviceIP)
        {
            bool returnValue = false;
            try
            {
                IPAddress validIPAddress = IPAddress.Parse(deviceIP);
                returnValue = true;
            }
            catch (ArgumentNullException)
            {
                returnValue = false;
            }
            catch (FormatException)
            {
                returnValue = false;
            }
            return returnValue;
        }

        /// <summary>
        /// IP address to number.
        /// </summary>
        /// <param name="IPaddress">Ip</param>
        /// <returns></returns>
        public double IPAddressToNumber(string IPaddress)
        {
            int i;
            string[] arrDec;
            double num = 0;
            if (IPaddress == "")
            {
                return 0;
            }
            else
            {
                arrDec = IPaddress.Split('.');
                for (i = arrDec.Length - 1; i >= 0; i = i - 1)//converts ip address to integer
                {
                    num += ((Int64.Parse(arrDec[i]) % 256) * Math.Pow(256, (3 - i)));
                }
                return num;
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the RadioButtonDeviceByIP control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void RadioButtonDeviceByIP_CheckedChanged(object sender, EventArgs e)
        {
            TextBoxStartIP.Enabled = false;
            TextBoxEndIP.Enabled = false;
            TextBoxStartIP.Text = "";
            TextBoxEndIP.Text = "";
            TextBoxIPAddress.Enabled = true;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the RadioButtonByIPRange control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void RadioButtonByIPRange_CheckedChanged(object sender, EventArgs e)
        {
            TextBoxIPAddress.Enabled = false;
            TextBoxIPAddress.Text = "";
            TextBoxStartIP.Enabled = true;
            TextBoxEndIP.Enabled = true;
        }

        protected void RadioButtonDevicesInSubnet_CheckedChanged(object sender, EventArgs e)
        {
            TextBoxStartIP.Enabled = false;
            TextBoxEndIP.Enabled = false;
            TextBoxIPAddress.Enabled = false;
            TextBoxStartIP.Text = "";
            TextBoxEndIP.Text = "";
            TextBoxIPAddress.Text = "";
        }

        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Administration/ManageDevice.aspx");
        }

    }
}
