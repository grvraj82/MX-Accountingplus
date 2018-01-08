
#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: ProductActivation.asmx.cs
  Description: Webservices exposed to Registration Client
  Date Created : June 15, 07

 */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  Sept 03, 07         Rajshekhar D
*/  
#endregion

#region Namespaces
    // Microsoft
    using System;
    using System.Data;
    using System.Web;
    using System.Collections;
    using System.Web.Services;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml;
    using System.Data.SqlClient;
    using System.Configuration;
    using Sharp.DataManager;
    using System.Net.Mail;
    using System.Globalization;
    using System.Text;
    using System.Net.Sockets;
    
    //Sharp
    //using SDHashLib;
#endregion

namespace ApplicationRegistration.WebServices
{
    /// <summary>
    /// Webservice: Product Registration Webservices
    /// </summary>
    [WebService(Namespace = "http://www.sharp.com/ApplicationRegistration")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    
    public class ProductActivation : System.Web.Services.WebService
    {
        /// <summary>
        /// Adds Registration Details
        /// </summary>
        /// <param name="productAccessId">Product Access Id</param>
        /// <param name="productAccessPassword">Product Access Password</param>
        /// <param name="registrationXmlData">Registration Details in XML Format</param>
        [WebMethod]
        public string Register(string productAccessId, string productAccessPassword, string registrationXmlData)
        {
            #region Validate Function Parameters
                if(string.IsNullOrEmpty(productAccessId))
                {
                    throw new ArgumentNullException("productAccessId"); 
                }
                
                if (string.IsNullOrEmpty(productAccessPassword))
                {
                    throw new ArgumentNullException("productAccessPassword");
                }
                
                if(string.IsNullOrEmpty(registrationXmlData))
                {
                    throw new ArgumentNullException("registrationXmlData");
                }
            #endregion

            // Authenticate the client Credentials [UserID and Password]
            string productID = DataProvider.AuthenticateClient(productAccessId, productAccessPassword);
            
            // Create XML Response Node
            XmlDocument xmlRegistrationResponse = new XmlDocument();
            XmlDeclaration xmlDeclaration = xmlRegistrationResponse.CreateXmlDeclaration("1.0", "utf-8", null);

            // Create the root element
            XmlElement rootNode = xmlRegistrationResponse.CreateElement("registration");
          
            rootNode.SetAttribute("productID", productID);
            xmlRegistrationResponse.InsertBefore(xmlDeclaration, xmlRegistrationResponse.DocumentElement);
            xmlRegistrationResponse.AppendChild(rootNode);

            XmlElement xmlActivationStartTime = xmlRegistrationResponse.CreateElement("activationStartTime");
            xmlActivationStartTime.InnerText = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            xmlRegistrationResponse.DocumentElement.AppendChild(xmlActivationStartTime);

            XmlElement xmlActivationCode = xmlRegistrationResponse.CreateElement("activationCode");
            xmlActivationCode.InnerText = "";
            xmlRegistrationResponse.DocumentElement.AppendChild(xmlActivationCode);

            XmlElement xmlActivationDate = xmlRegistrationResponse.CreateElement("activationDate");
            xmlActivationDate.InnerText = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            xmlRegistrationResponse.DocumentElement.AppendChild(xmlActivationDate);

            XmlElement xmlErrors = xmlRegistrationResponse.CreateElement("errors");
            xmlErrors.InnerText = "";

            XmlElement xmlWarnings = xmlRegistrationResponse.CreateElement("warnings");
            xmlWarnings.InnerText = "";

            if (productID != null)
            {
                if (DataProvider.IsRegistrationAllowed(productID))
                {
                    ProcessRequest(productID, xmlRegistrationResponse, xmlErrors, xmlWarnings, registrationXmlData);                   
                }
                else
                {

                    SetError(xmlRegistrationResponse, xmlErrors, "RegistrationDisabled", Resources.WebServiceMessages.RegistrationDisabled);
                }
            }
            else
            {
                SetError(xmlRegistrationResponse, xmlErrors, "AccessDenied", Resources.WebServiceMessages.RegistrationDisabled);
            }
            
            xmlRegistrationResponse.DocumentElement.AppendChild(xmlWarnings);
            xmlRegistrationResponse.DocumentElement.AppendChild(xmlErrors);
            
            XmlElement xmlActivationEndTime = xmlRegistrationResponse.CreateElement("activationEndTime");
            xmlActivationEndTime.InnerText = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            xmlRegistrationResponse.DocumentElement.AppendChild(xmlActivationEndTime);

            return xmlRegistrationResponse.InnerXml;
        }

        /// <summary>
        /// Process the Registration and Activation Client Request
        /// </summary>
        /// <param name="productID">Product Id</param>
        /// <param name="xmlRegistrationResponse">XML Response</param>
        /// <param name="xmlErrors">XML Error Element</param>
        /// <param name="xmlWarnings">XML Warning Element</param>
        /// <param name="registationXmlData">Registration XML Data</param>
        private static void ProcessRequest(string productID, XmlDocument xmlRegistrationResponse, XmlElement xmlErrors, XmlElement xmlWarnings, string registationXmlData)
        {

            #region Validate Function Parameters
                if (string.IsNullOrEmpty(productID))
                {
                    throw new ArgumentNullException("productID");
                }
              
                if (xmlRegistrationResponse == null)
                {
                    throw new ArgumentNullException("xmlRegistrationResponse");
                }

                if (xmlErrors == null)
                {
                    throw new ArgumentNullException("xmlErrors");
                }

                if (string.IsNullOrEmpty(registationXmlData))
                {
                    throw new ArgumentNullException("registrationXmlData");
                }
            #endregion
           
            XmlDocument xdocRequest = new XmlDocument();
            string sqlQuery = "";
            try
            {
                xdocRequest.LoadXml(registationXmlData);
                
                // Get System Field values
                XmlElement xrootRequest = xdocRequest.DocumentElement;
                string clientCode = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='1']"), 50);
                string serialKey = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='3']"), 50);

              
                string productVerion = DataProvider.GetProductVersion(productID);
                // Check Wether Registration allowed for given Product Id and Serial Number 
                if (!DataProvider.IsRegistrationAllowed(productID, serialKey))
                {
                    SetError(xmlRegistrationResponse, xmlErrors, "SerialKeyDisabled", Resources.WebServiceMessages.SerialKeyDisabled);
                    return;
                }
                
                // Validate Serial Number
                if (!DataValidator.IsValidSerialKey(productID, serialKey, productVerion))
                {
                    SetError(xmlRegistrationResponse, xmlErrors, "InvalidSerialKey", Resources.WebServiceMessages.InvalidSerialKey);
                    return ;
                }

                // Check whether Activation data Exists for the current Request
                if (DataProvider.IsRegistrationDetailsExists(productID, serialKey, clientCode))
                {
                    SetError(xmlRegistrationResponse, xmlErrors, "ProductActivatedAlready", Resources.WebServiceMessages.ProductActivatedAlready);
                    return ;
                }

                string firstName = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='5']"), 50);
                string lastName = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='6']"), 50);
                string address1 = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='7']"), 250);
                string address2 = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='8']"), 250);
                string city = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='9']"), 50);
                string state = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='10']"), 4);
                string stateOthers = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='11']"), 50);
                string country = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='12']"), 4);
                string zipCode = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='13']"), 15);
                string phone = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='14']"), 15);
                string extension = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='15']"), 8);
                string fax = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='16']"), 15);
                string email = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='17']"), 50);
                string company = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='18']"), 100);
                string department = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='19']"), 4);
                string jobFunction = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='20']"), 4);

                string industryType = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='21']"), 4);
                string organisationType = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='22']"), 4);
                string dealerName = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='23']"), 50);
                string dealerAddress = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='24']"), 250);
                string sendNotifications = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='25']"), 50);
                string model = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='26']"), 50);
                
                string macAddress = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='27']"), 50);
                string IPAddress = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='28']"), 50);
                string hardDiskID = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='29']"), 50);
                string processorType = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='30']"), 30);
                string processorCount = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='31']"), 2);
                string operatingSystem = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='32']"), 50);
                string computerName = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='34']"), 50);
                string registrationType = "Online";
                if (xrootRequest.SelectSingleNode("systemFields/field[@id='35']") != null)
                {
                    registrationType = GetValidData(xrootRequest.SelectSingleNode("systemFields/field[@id='35']"), 20);
                }


                int maxLicenceCount = DataProvider.GetNumberOfLicenses(productID, serialKey);

                string activationCode = DataProvider.GetProductActivationCode(maxLicenceCount,productID, serialKey, clientCode,productVerion);
                // Set the Activation code in the Response
                xmlRegistrationResponse.GetElementsByTagName("activationCode")[0].InnerText = activationCode;
                
                sqlQuery = "insert into T_REGISTRATION(";
                sqlQuery += "PRDCT_ID, REG_CLIENT_CODE, REG_ACTIVATION_CODE, REG_SERIAL_KEY, REG_FIRST_NAME, REG_LAST_NAME, REG_ADDRESS1, REG_ADDRESS2,";
                sqlQuery += "REG_CITY, REG_STATE, REG_STATE_OTHER, REG_COUNTRY, REG_ZIPCODE, REG_PHONE,";
                sqlQuery += "REG_PHONE_EXTN, REG_FAX, REG_EMAIL, REG_COMPANY, REG_INDUSTRY_TYPE, REG_ORG_TYPE,REG_JOB_FUNCTION,";
                sqlQuery += "REG_DEALER_NAME, REG_SEND_NOTIFICATIONS, REG_MFP_MODEL," ;
                sqlQuery += "REG_MAC_ADDRESS, REG_COMPUTER_NAME, REG_IP_ADDRESS, REG_HARDDISK_ID, REG_PROCESSOR_TYPE, REG_PROCESSOR_COUNT,";
                sqlQuery += "REG_DEPT, REG_OS, REG_TYPE, REC_DATE) values(";

                sqlQuery += "N'" + productID + "', N'" + clientCode + "', N'" + activationCode + "', N'" + serialKey + "', N'" + firstName + "', N'" + lastName + "', N'" + address1 + "', N'" + address2 + "', ";
                sqlQuery += "N'" + city + "', N'" + state + "', N'" + stateOthers + "', N'" + country + "', N'" + zipCode + "', N'" + phone + "', ";
                sqlQuery += "N'" + extension + "', N'" + fax + "', N'" + email + "', N'" + company + "', N'" + industryType + "', N'" + organisationType + "', N'" + jobFunction + "',";
                sqlQuery += "N'" + dealerName + "', N'" + sendNotifications + "', N'" + model + "', ";
                sqlQuery += "N'" + macAddress + "', N'" + computerName + "', N'" + IPAddress + "', N'" + hardDiskID + "', N'" + processorType + "', N'" + processorCount + "', ";
                sqlQuery += "N'" + department + "', N'" + operatingSystem + "', N'" + registrationType + "', N'" + DateTime.Now.ToString(CultureInfo.InvariantCulture) + "')";

                // Custom Fields

                XmlNodeList xnlCustomFields = xrootRequest.SelectNodes("customFields/field");
                string idValuePair = "";
                char stringSeperator = '^';
                foreach (XmlNode xNode in xnlCustomFields)
                {
                    idValuePair += stringSeperator + xNode.Attributes["id"].Value + stringSeperator + GetValidData(xNode, 500);
                }
                if (!string.IsNullOrEmpty(idValuePair))
                {
                    idValuePair = idValuePair.Substring(1);
                }
               
                // Check whether Licenses available for the new registration
                if (!DataProvider.IsLicensesAvailable(productID, serialKey))
                {
                    SetError(xmlRegistrationResponse, xmlErrors, "NoLicensesAvailable", Resources.WebServiceMessages.NoLicensesAvailable);
                    return ;
                }
                else
                {
                    // Add the data to SQL Server
                    int registrationID = DataController.AddRegistrationDetails(productID, serialKey, maxLicenceCount, sqlQuery, idValuePair);
                    if (registrationID > 0)
                    {
                        // Add Reference ID Element to XML Response
                        XmlElement xeRegID = xmlRegistrationResponse.CreateElement("registrationID");
                        xeRegID.InnerText = registrationID.ToString(CultureInfo.InvariantCulture);
                        xmlRegistrationResponse.DocumentElement.InsertBefore(xeRegID, xmlRegistrationResponse.DocumentElement.FirstChild);
                    }
                    else
                    {
                        return;
                    }
                    // Compose Email
                    if (!ComposeEmail(registrationID, productID, firstName, email, serialKey, activationCode))
                    {
                        SetWarning(xmlRegistrationResponse, xmlWarnings, "FailedToSendEmail", Resources.WebServiceMessages.FailedToSendEmail);
                    }

                }
            }
            catch(XmlException xmlEx)
            {
                SetError(xmlRegistrationResponse, xmlErrors, "-1", xmlEx.Message );
                return ;
            }
            catch (DataException xmlEx)
            {
                SetError(xmlRegistrationResponse, xmlErrors, "-1", xmlEx.Message);
                return ;
            }
            catch (Exception xmlEx)
            {
                SetError(xmlRegistrationResponse, xmlErrors, "-1", xmlEx.Message);
                return ;
            }

        }

        /// <summary>
        /// Gets the processed data [Removes  ' and ^]
        /// </summary>
        /// <param name="xmlNodeData">XMLNode Data</param>
        /// <param name="maxDataLength">Maximum Length</param>
        private static string GetValidData(XmlNode xmlNodeData, int maxDataLength)
        {
            string requiredData = "";
            if (xmlNodeData != null)
            {
                requiredData = xmlNodeData.InnerText.Trim().Replace("'", "''");
                requiredData = requiredData.Replace("^", "");
                if (requiredData.Length > maxDataLength)
                {
                    requiredData = requiredData.Substring(0, maxDataLength - 1);
                }
            }

            return requiredData;
        }

        /// <summary>
        /// Adds the Error Node to the errors Element
        /// </summary>
        /// <param name="xmlRegistrationResponse">Registration Response XML Document</param>
        /// <param name="xmlErrors">'errors' XML Element</param>
        /// <param name="errorID">Error Id</param>
        /// <param name="errorText">Error Text</param>
        private static void SetError(XmlDocument xmlRegistrationResponse, XmlElement xmlErrors, string errorID, string errorText)
        {
            #region Validate Function Parameters
                if (xmlRegistrationResponse == null)
                {
                    throw new ArgumentNullException("xmlRegistrationResponse");
                }
                if (xmlErrors == null)
                {
                    throw new ArgumentNullException("xmlErrors");
                }
                if (string.IsNullOrEmpty(errorID))
                {
                    throw new ArgumentNullException("errorID");
                }
                if (string.IsNullOrEmpty(errorText))
                {
                    throw new ArgumentNullException("errorText");
                }
            #endregion
            
            XmlElement xmlError = xmlRegistrationResponse.CreateElement("error");
            xmlError.SetAttribute("id", errorID);
            xmlError.InnerText = errorText;
            xmlErrors.AppendChild(xmlError);
        }

        /// <summary>
        /// Adds the Warning Node to the warnings Element
        /// </summary>
        /// <param name="xmlRegistrationResponse">Registration Response XML Document</param>
        /// <param name="xmlWarnings">Adds the warning Node to the warning Element</param>
        /// <param name="warningID">Warning Id</param>
        /// <param name="warningText">Warning Text</param>
        private static void SetWarning(XmlDocument xmlRegistrationResponse, XmlElement xmlWarnings, string warningID, string warningText)
        {
            #region Validate Function Parameters
                if (xmlRegistrationResponse == null)
                {
                    throw new ArgumentNullException("xmlRegistrationResponse");
                }
                if (xmlWarnings == null)
                {
                    throw new ArgumentNullException("xmlWarnings");
                }
                if (string.IsNullOrEmpty(warningID))
                {
                    throw new ArgumentNullException("warningID");
                }
                if (string.IsNullOrEmpty(warningText))
                {
                    throw new ArgumentNullException("warningText");
                }
            #endregion

            XmlElement xmlWarning = xmlRegistrationResponse.CreateElement("warning");
            xmlWarning.SetAttribute("id", warningID);
            xmlWarning.InnerText = warningText;
            xmlWarnings.AppendChild(xmlWarning);
        }

        /// <summary>
        /// Composes Registration Email
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <param name="recepientName">Recepient Name</param>
        /// <param name="recepientEmailAddress">Recepient To Address</param>
        /// <param name="serialNumber">Serial Number</param>
        /// <param name="activationCode">Activation Code</param>
        private static bool ComposeEmail(int registrationID, string productId, string recepientName, string recepientEmailAddress, string serialNumber, string activationCode)
        {
            #region Validate Function Parameters

            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentNullException("productId");
            }

            if (string.IsNullOrEmpty(recepientName))
            {
                throw new ArgumentNullException("recepientName");
            }

            if (string.IsNullOrEmpty(recepientEmailAddress))
            {
                throw new ArgumentNullException("recepientEmailAddress");
            }

            if (string.IsNullOrEmpty(serialNumber))
            {
                throw new ArgumentNullException("serialNumber");
            }

            if (string.IsNullOrEmpty(activationCode))
            {
                throw new ArgumentNullException("activationCode");
            }
            
            #endregion

            try
            {
                

                string fromAddress = "";
                string ccAddress = "";
                string bccAddress = "";
                string emailMessage = "";
                string subject = "";
                MailMessage activationEmail = new MailMessage();
                activationEmail.To.Add(recepientEmailAddress);
                string imageFolderPhysicalPath = HttpContext.Current.Server.MapPath("../AppImages");

                /*
                Attachment attachmentProductActivationImage = new Attachment(imageFolderPhysicalPath+ "/ProductRegActLogo.gif");
                attachmentProductActivationImage.Name = "Registration.gif";
                activationEmail.Attachments.Add(attachmentProductActivationImage);


                Attachment attachmentComapanyImage = new Attachment(imageFolderPhysicalPath + "/CompanyLogo.gif");
                attachmentComapanyImage.Name = "CompanyLogo.gif";
                activationEmail.Attachments.Add(attachmentComapanyImage);
                */

                // Get email content 
                SqlDataReader drEmailContent = DataProvider.GetEmailContent(productId);
                if (drEmailContent != null && drEmailContent.HasRows)
                {
                    drEmailContent.Read();
                    emailMessage = drEmailContent["Content"].ToString().Trim();
                    fromAddress = drEmailContent["From"].ToString();
                    ccAddress = drEmailContent["CC"].ToString();
                    bccAddress = drEmailContent["BCC"].ToString();
                    subject = drEmailContent["subject"].ToString(); 
                }
                drEmailContent.Close();

                
                if (string.IsNullOrEmpty(emailMessage))
                {
                    drEmailContent = DataProvider.GetEmailContent("-1"); // -1 = All Products
                    if (drEmailContent != null && drEmailContent.HasRows)
                    {
                        drEmailContent.Read();
                        emailMessage = drEmailContent["Content"].ToString().Trim();
                        fromAddress = drEmailContent["From"].ToString();
                        ccAddress = drEmailContent["CC"].ToString();
                        bccAddress = drEmailContent["BCC"].ToString();
                        subject = drEmailContent["subject"].ToString(); 
                    }
                    drEmailContent.Close();
                }

                //Get Registration Details
                string filterCiteria = "REC_ID = '" + registrationID.ToString() + "'";
                DataSet drRegistrationDetails = DataProvider.GetRegistrationDetails(productId, 1, 1, "", filterCiteria);
                string productName = "";
                if (drRegistrationDetails != null)
                {
                    DataTable dtRegistrationDetails = drRegistrationDetails.Tables[0];
                    DataTable dtRegistrationFields = drRegistrationDetails.Tables[1];

                    string searchCriteria = "";
                    string replaceWith = "";
                    for (int field = 0; field < dtRegistrationFields.Rows.Count; field++)
                    {
                        //searchCriteria = "<" + dtRegistrationFields.Rows[field]["FLD_NAME"].ToString() + ">[Field: " + dtRegistrationFields.Rows[field]["FLD_ENGLISH_NAME"].ToString() + "]";
                        
                        searchCriteria = "[Field: " + dtRegistrationFields.Rows[field]["FLD_ENGLISH_NAME"].ToString() + "]";

                        replaceWith = dtRegistrationDetails.Rows[0][dtRegistrationFields.Rows[field]["FLD_NAME"].ToString()].ToString();
                        emailMessage = emailMessage.Replace(searchCriteria, replaceWith);
                        if (dtRegistrationFields.Rows[field]["FLD_ENGLISH_NAME"].ToString() == "PRODUCT_NAME")
                        {
                            productName = replaceWith;
                        }
                    }
                }
                /*
                // Get Product Logo
                string productName = "";
                string productLogofile = "";
                SqlDataReader drProductLogo = DataProvider.GetProductLogo(productId);
                if (drProductLogo != null && drProductLogo.HasRows)
                {
                    drProductLogo.Read();
                    productName = drProductLogo["ProductName"].ToString();
                    productLogofile = "ProductLogo.gif";
                    byte[] bytPic = (byte[])drProductLogo["ProductLogo"];
                    System.IO.MemoryStream x = new System.IO.MemoryStream(bytPic);
                    Attachment productLogoAttachment = new Attachment(x, productLogofile);
                    activationEmail.Attachments.Add(productLogoAttachment);
                }
                drProductLogo.Close();
                */
                activationEmail.From = new MailAddress(fromAddress);
                if(!string.IsNullOrEmpty(ccAddress))
                {
                    activationEmail.CC.Add(ccAddress);
                }
                if(!string.IsNullOrEmpty(bccAddress))
                {
                    activationEmail.Bcc.Add(bccAddress);
                }

                if (string.IsNullOrEmpty(subject))
                {
                    subject = productName + " activation details";
                }
                activationEmail.IsBodyHtml = true;
                string messageBody = "<html><title>" + productName + "&copy; Registration</title><body bgcolor='white'>";
                messageBody += "<table width='600' cellspacing=0 cellpadding=0 border=0>";
                //messageBody += "<tr><td align='left'><img src='Registration.gif' alt='Product Registration'></td><td align='right'><img src='CompanyLogo.gif'></td></tr>";
                //messageBody += "<tr><td colspan=2><br /><br /><font color='black' face='Arial' size='2'>Dear " + recepientName + "</td></tr>";
                //messageBody += "<tr><td colspan=2><font color='black' face='Arial' size='2'><br />Thank you for registering " + productName + "</font></td></tr>";
                //messageBody += "<tr><td colspan=2><br/><table><tr><td style='White-space:nowrap'><font color='black' face='Arial' size='2'>Product</font></td><td><img src='" + productLogofile + "' alt='" + productName + "'></td></tr>"; ;
                //messageBody += "<tr><td style='White-space:nowrap'><font color='black' face='Arial' size='2'>Serial Number</font></td><td><b><font color='green' face='Arial' size='2'>" + serialNumber + "</font></b></td></tr>";
                //messageBody += "<tr><td style='White-space:nowrap'><font color='black' face='Arial' size='2'>Activation Code</font></td><td><b><font color='green' face='Arial' size='2'>" + activationCode + "</font></b></td></tr></table></td>";
                messageBody += "<tr><td colspan=2><br /><server name='" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"].ToString() + "'>";
                messageBody += emailMessage;
                messageBody += "</td></tr></table></body></html>";
                // Create the smtp client
                activationEmail.Body = messageBody;
                activationEmail.Subject = subject ;
                SmtpClient SMTPClient = new SmtpClient();
                SMTPClient.Host = DataProvider.GetDBConfigValue("SMTP_SERVER");

                SMTPClient.Send(activationEmail);
                return true;
            }
            catch (SmtpException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// Cancels the Registration
        /// </summary>
        /// <param name="productAccessId">Product Access Id</param>
        /// <param name="productAccessPassword">Product Access Password</param>
        /// <param name="serialKey">Serial Number</param>
        /// <param name="clientCode">Client Code</param>
        [WebMethod]
        public bool UnRegister(string productAccessId, string productAccessPassword, string serialKey, string clientCode)
        {
            #region Validate Function Parameters
                if (string.IsNullOrEmpty(productAccessId))
                {
                    throw new ArgumentNullException("productAccessId");
                }

                if (string.IsNullOrEmpty(productAccessPassword))
                {
                    throw new ArgumentNullException("productAccessPassword");
                }

                if (string.IsNullOrEmpty(serialKey))
                {
                    throw new ArgumentNullException("serialKey");
                }

                if (string.IsNullOrEmpty(clientCode))
                {
                    throw new ArgumentNullException("clientCode");
                }
            #endregion
            
            // Authenticate the client Credentials [UserID and Password]
            string productId = DataProvider.AuthenticateClient(productAccessId, productAccessPassword);
            if (!string.IsNullOrEmpty(productId))
            {
                // Get Reference Id
                int referenceId = DataProvider.GetApplicationRegistrationReferenceId(productId, serialKey, clientCode);
                if (referenceId > 0)
                {
                    return DataController.DeleteRegistration(referenceId);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Imports License
        /// </summary>
        /// <param name="productAccessId">Product Aceess Id</param>
        /// <param name="productAccessPassword">Product Aceess Password</param>
        /// <param name="serialKey">Serial Number</param>
        /// <param name="oldClientCode">Old Client Code</param>
        /// <param name="oldActivationCode">Old Activation Code</param>
        /// <param name="newClientCode">New Client Code</param>
        /// <param name="newActivationCode">New Activation Code</param>
        /// <param name="xmlRegistrationData">Registration Data in XML format</param>
        /// <returns></returns>
        [WebMethod]
        public string ImportLicense(string productAccessId, string productAccessPassword, string serialKey, string oldClientCode, string oldActivationCode, string newClientCode, string newActivationCode, string xmlRegistrationData)
        {

            #region Validate Function Parameters
                if (string.IsNullOrEmpty(productAccessId))
                {
                    throw new ArgumentNullException("productAccessId");
                }

                if (string.IsNullOrEmpty(productAccessPassword))
                {
                    throw new ArgumentNullException("productAccessPassword");
                }

                if (string.IsNullOrEmpty(serialKey))
                {
                    throw new ArgumentNullException("serialKey");
                }

                if (string.IsNullOrEmpty(oldClientCode))
                {
                    throw new ArgumentNullException("oldClientCode");
                }

                if (string.IsNullOrEmpty(oldActivationCode))
                {
                    throw new ArgumentNullException("oldActivationCode");
                }

                if (string.IsNullOrEmpty(newClientCode))
                {
                    throw new ArgumentNullException("newClientCode");
                }

                if (string.IsNullOrEmpty(newActivationCode))
                {
                    throw new ArgumentNullException("newActivationCode");
                }

                if (string.IsNullOrEmpty(xmlRegistrationData))
                {
                    throw new ArgumentNullException("xmlRegistrationData");
                }

            #endregion

            // Authenticate the client Credentials [UserID and Password]
            string productID = DataProvider.AuthenticateClient(productAccessId, productAccessPassword);

            // Create XML Response Node
            XmlDocument xmlExportResponse = new XmlDocument();
            XmlDeclaration xmlDeclaration = xmlExportResponse.CreateXmlDeclaration("1.0", "utf-8", null);

            // Create the root element
            XmlElement rootNode = xmlExportResponse.CreateElement("registration");

            rootNode.SetAttribute("productID", productID);
            xmlExportResponse.InsertBefore(xmlDeclaration, xmlExportResponse.DocumentElement);
            xmlExportResponse.AppendChild(rootNode);

            XmlElement xmlImportStartTime = xmlExportResponse.CreateElement("importStartTime");
            xmlImportStartTime.InnerText = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            xmlExportResponse.DocumentElement.AppendChild(xmlImportStartTime);

            XmlElement xmlErrors = xmlExportResponse.CreateElement("errors");
            xmlErrors.InnerText = "";

            XmlElement xmlWarnings = xmlExportResponse.CreateElement("warnings");
            xmlWarnings.InnerText = "";

            if (productID != null)
            {
                try
                {
                    // Check whether Licenses available for the new registration
                    if (DataProvider.IsLicensesAvailable(productID, serialKey))
                    {
                        string processedFieldData = GetProcessedFieldData(xmlRegistrationData);
                        int oldReferenceId = 0;
                        int newReferenceId = 0;
                        bool isLicenseImported = DataController.ImportLicense(productID, serialKey, oldClientCode, oldActivationCode, newClientCode, newActivationCode, processedFieldData, out oldReferenceId, out newReferenceId);
                        if (!isLicenseImported)
                        {
                            if (oldReferenceId == 0)
                            {
                                SetError(xmlExportResponse, xmlErrors, "5", "Failed to import lincense, There are no previously Exported Licenses");
                            }
                            if (newReferenceId == 0)
                            {
                                SetError(xmlExportResponse, xmlErrors, "6", "Failed to import lincense, No records found for the given lincense details");
                            }

                            SetError(xmlExportResponse, xmlErrors, "2", "Failed to import lincense");
                        }
                    }
                    else
                    {
                        SetError(xmlExportResponse, xmlErrors, "4", "All the licenses are used for the serial key:" + serialKey);
                    }
                    
                }
                catch (Exception ex)
                {
                    SetError(xmlExportResponse, xmlErrors, "3", ex.Message);
                }
            }
            else
            {
                SetError(xmlExportResponse, xmlErrors, "1", "Access Denied");
            }

            xmlExportResponse.DocumentElement.AppendChild(xmlWarnings);
            xmlExportResponse.DocumentElement.AppendChild(xmlErrors);

            XmlElement xmlImportEndTime = xmlExportResponse.CreateElement("importEndTime");
            xmlImportEndTime.InnerText = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            xmlExportResponse.DocumentElement.AppendChild(xmlImportEndTime);

            return xmlExportResponse.InnerXml;
        }

        /// <summary>
        /// Export License 
        /// </summary>
        /// <param name="productAccessId">Product Access Id</param>
        /// <param name="productAccessPassword">Product Access Password</param>
        /// <param name="serialKey">Serial Number</param>
        /// <param name="clientCode">Client Code</param>
        /// <param name="activationCode">Activation Code</param>
        /// <returns>Result as XML (string) Which contains errors, warnings, exportStartTime and exportEndTime </returns>
        [WebMethod]
        public string ExportLicense(string productAccessId, string productAccessPassword, string serialKey, string clientCode, string activationCode)
        {
             #region Validate Function Parameters
                if (string.IsNullOrEmpty(productAccessId))
                {
                    throw new ArgumentNullException("productAccessId");
                }

                if (string.IsNullOrEmpty(productAccessPassword))
                {
                    throw new ArgumentNullException("productAccessPassword");
                }

                if (string.IsNullOrEmpty(serialKey))
                {
                    throw new ArgumentNullException("serialKey");
                }

                if (string.IsNullOrEmpty(clientCode))
                {
                    throw new ArgumentNullException("clientCode");
                }

                if (string.IsNullOrEmpty(activationCode))
                {
                    throw new ArgumentNullException("activationCode");
                }
            #endregion

            // Authenticate the client Credentials [UserID and Password]
            string productID = DataProvider.AuthenticateClient(productAccessId, productAccessPassword);

            // Create XML Response Node
            XmlDocument xmlExportResponse = new XmlDocument();
            XmlDeclaration xmlDeclaration = xmlExportResponse.CreateXmlDeclaration("1.0", "utf-8", null);

            // Create the root element
            XmlElement rootNode = xmlExportResponse.CreateElement("registration");

            rootNode.SetAttribute("productID", productID);
            xmlExportResponse.InsertBefore(xmlDeclaration, xmlExportResponse.DocumentElement);
            xmlExportResponse.AppendChild(rootNode);

            XmlElement xmlExportStartTime = xmlExportResponse.CreateElement("exportStartTime");
            xmlExportStartTime.InnerText = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            xmlExportResponse.DocumentElement.AppendChild(xmlExportStartTime);

            XmlElement xmlErrors = xmlExportResponse.CreateElement("errors");
            xmlErrors.InnerText = "";

            XmlElement xmlWarnings = xmlExportResponse.CreateElement("warnings");
            xmlWarnings.InnerText = "";

            if (productID != null)
            {
                try
                {
                    int referenceId = 0;
                    bool isLicenseExported = DataController.ExportLicense(productID, serialKey, clientCode, activationCode, out referenceId);
                    if(!isLicenseExported)
                    {
                        if(referenceId == 0)
                        {
                            SetError(xmlExportResponse, xmlErrors, "3", "Failed to transfer Lincense, No registration record found for the given lincense details");
                        }
                        else
                        {
                            SetError(xmlExportResponse, xmlErrors, "4", "Failed to transfer Lincense");
                        }

                    }
                }
                catch (Exception ex)
                {
                    SetError(xmlExportResponse, xmlErrors, "2", ex.Message);
                }
            }
            else
            {
                SetError(xmlExportResponse, xmlErrors, "1", "Access Denied");
            }

            xmlExportResponse.DocumentElement.AppendChild(xmlWarnings);
            xmlExportResponse.DocumentElement.AppendChild(xmlErrors);

            XmlElement xmlExportEndTime = xmlExportResponse.CreateElement("exportEndTime");
            xmlExportEndTime.InnerText = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            xmlExportResponse.DocumentElement.AppendChild(xmlExportEndTime);

            return xmlExportResponse.InnerXml;

        }
         
        /// <summary>
        /// Updates custom fields
        /// </summary>
        /// <param name="productAccessId"></param>
        /// <param name="productAccessPassword"></param>
        /// <param name="serialKey"></param>
        /// <param name="clientCode"></param>
        /// <param name="customFieldXmlData"></param>
        /// <returns></returns>
        [WebMethod]
        public bool UpdateCustomFields(string productAccessId, string productAccessPassword, string serialKey, string clientCode, string customFieldXmlData)
        {
            #region Validate Function Parameters
                if (string.IsNullOrEmpty(productAccessId))
                {
                    throw new ArgumentNullException("productAccessId");
                }

                if (string.IsNullOrEmpty(productAccessPassword))
                {
                    throw new ArgumentNullException("productAccessPassword");
                }

                if (string.IsNullOrEmpty(serialKey))
                {
                    throw new ArgumentNullException("serialKey");
                }

                if (string.IsNullOrEmpty(clientCode))
                {
                    throw new ArgumentNullException("clientCode");
                }

                if (string.IsNullOrEmpty(customFieldXmlData))
                {
                    throw new ArgumentNullException("customFieldXmlData");
                }
            #endregion

            // Authenticate the client Credentials [UserID and Password]
            string productId = DataProvider.AuthenticateClient(productAccessId, productAccessPassword);
            if (string.IsNullOrEmpty(productId))
            {
                return false;
            }
            try
             {
                 XmlDocument xdocRequest = new XmlDocument();
                 xdocRequest.LoadXml(customFieldXmlData);
                 XmlElement xrootRequest = xdocRequest.DocumentElement;
                 XmlNodeList xnlCustomFields = xrootRequest.SelectNodes("customFields/field");
                 char stringSeperator = '^';
                 StringBuilder sbIdValuePair = new StringBuilder();
                 foreach (XmlNode xNode in xnlCustomFields)
                 {
                     sbIdValuePair.Append(stringSeperator);
                     sbIdValuePair.Append(xNode.Attributes["id"].Value);
                     sbIdValuePair.Append(stringSeperator);
                     sbIdValuePair.Append(GetValidData(xNode, 500));
                 }
                 if (!string.IsNullOrEmpty(sbIdValuePair.ToString()))
                 {
                     DataController.UpdateCustomFields(-1, int.Parse(productId, CultureInfo.InvariantCulture), serialKey, clientCode, sbIdValuePair.ToString().Substring(1));
                    return true;
                 }
                 else
                 {
                    return false;
                 }
             }
             catch (SqlException)
             {
                 return false;
             }
             catch (NullReferenceException)
             {
                 return false;
             }
        }

        /// <summary>
        /// Gets the List of MFPs
        /// </summary>
        /// <param name="productAccessId">Product Access Id</param>
        /// <param name="productAccessPassword">Product Access Password</param>
        [WebMethod]
        public string GetMfpList(string productAccessId, string productAccessPassword)
        {
            #region Validate Function Parameters
                if (string.IsNullOrEmpty(productAccessId))
                {
                    throw new ArgumentNullException("productAccessId");
                }

                if (string.IsNullOrEmpty(productAccessPassword))
                {
                    throw new ArgumentNullException("productAccessPassword");
                }
            #endregion

            // Authenticate the client Credentials [UserID and Password]
            string productID = DataProvider.AuthenticateClient(productAccessId, productAccessPassword);
            if (!string.IsNullOrEmpty(productID))
            {
                StringBuilder sbXmlMfpList = new StringBuilder("<mfpModels>");
                SqlDataReader drModels = DataProvider.GetMFPModels();
                while (drModels.Read())
                {
                    sbXmlMfpList.Append("<model ID='" + drModels["MFPMODEL_ID"].ToString() + "'>" + drModels["MFPMODEL_NAME"].ToString().Trim() + "</model>");
                }
                sbXmlMfpList.Append("</mfpModels>");
                drModels.Close();
                return sbXmlMfpList.ToString();
            }
            else
            {
                return null;
            }

        }
        
        /// <summary>
        /// Get the data from Registration XML Request Data
        /// </summary>
        /// <param name="fieldXmlData">XMLNode Data</param>
        private string GetProcessedFieldData(string fieldXmlData)
        {
            if (string.IsNullOrEmpty(fieldXmlData))
            {
                return null;
            }
            try
            {
                XmlDocument xdocRequest = new XmlDocument();
                xdocRequest.LoadXml(fieldXmlData);
                XmlElement xrootRequest = xdocRequest.DocumentElement;
                XmlNodeList xnlSystemFields = xrootRequest.SelectNodes("systemFields/field");
                XmlNodeList xnlCustomFields = xrootRequest.SelectNodes("customFields/field");
                char stringSeperator = '^';
                StringBuilder sbIdValuePair = new StringBuilder();
                // System Fields
                foreach (XmlNode xNode in xnlSystemFields)
                {
                    sbIdValuePair.Append(stringSeperator);
                    sbIdValuePair.Append(xNode.Attributes["id"].Value.Replace('^', ' '));
                    sbIdValuePair.Append(stringSeperator);
                    sbIdValuePair.Append(GetValidData(xNode, 500));
                }

                // Custom Fields
                foreach (XmlNode xNode in xnlCustomFields)
                {
                    sbIdValuePair.Append(stringSeperator);
                    sbIdValuePair.Append(xNode.Attributes["id"].Value.Replace('^', ' '));
                    sbIdValuePair.Append(stringSeperator);
                    sbIdValuePair.Append(GetValidData(xNode, 500));
                }

                
                return sbIdValuePair.ToString().Substring(1);
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

    }
}
