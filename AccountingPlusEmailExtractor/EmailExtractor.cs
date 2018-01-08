using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.IO;
using ApplicationAuditor;
using System.Globalization;
using D.Net.EmailClient;
using System.Net;
using System.Security.Cryptography;
using System.Data.SqlClient;
using D.Net.EmailInterfaces;



namespace AccountingPlusEmailExtractor
{

    public partial class EmailExtractor : ServiceBase
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);
        public EmailExtractor()
        {
            InitializeComponent();
        }

        internal static string AUDITORSOURCE = "Email Extractor";
        internal static string SERVICE_NAME = "AccountingPlusEmailExtractor";


        private System.Timers.Timer emailMoniterTimer;
        private static string serviceWatchTime = ConfigurationManager.AppSettings["ServiceWatchTime"];

        protected void InitializeTimer()
        {

            try
            {
                SqlConnection.ClearAllPools();
            }
            catch
            {
            }
            emailMoniterTimer = new System.Timers.Timer();
            emailMoniterTimer.AutoReset = true;
            emailMoniterTimer.Interval = Convert.ToDouble(ConfigurationManager.AppSettings["EmailMoniteringInterval"], CultureInfo.CurrentCulture);
            emailMoniterTimer.Elapsed += new System.Timers.ElapsedEventHandler(MoniterEmailExtractRequests);
        }
        protected override void OnStart(string[] args)
        {
            InitializeTimer();
            emailMoniterTimer.Enabled = true;
            //JobListner.JobProcessor.RecordServiceTimings("AccountingPlusEmailExtractor", "", "start");
            //Task tskPrintJobListner = Task.Factory.StartNew(StartExtractingEmail);
        }

        protected override void OnStop()
        {
            JobListner.JobProcessor.RecordServiceTimings("AccountingPlusEmailExtractor", "", "stop");
        }

        private void MoniterEmailExtractRequests(object source, System.Timers.ElapsedEventArgs e)
        {
          
           StartExtractingEmail();
        }

        private static void StartExtractingEmail()
        {
            bool status = false;
            //read config values from database 
            string emailId = string.Empty;
            string mfpId = string.Empty;
            string mfpIp = string.Empty;
            string emailHost = string.Empty;
            string emailPort = string.Empty;
            string emailUserName = string.Empty;
            string emailPassword = string.Empty;
            string isRequiredSSl = string.Empty;
            bool isDirectPrint = false;
            string messageCount = string.Empty;
            string ftpIPAddress = string.Empty;
            string ftpProtocol = string.Empty;
            string ftpPort = string.Empty;
            string ftpUserName = string.Empty;
            string ftpUserPassword = string.Empty;
            string FTPAddress = string.Empty;
            string userNameMail = string.Empty;
            StringBuilder sbFileNames = new StringBuilder();
            bool isConnectionSuccess = false;


            DataSet DTwithDuplicate = DataManager.Provider.Device.ProvideEmailSettings();
            for (int emailIndex = 0; emailIndex < DTwithDuplicate.Tables[1].Rows.Count; emailIndex++)
            {
                emailId = DTwithDuplicate.Tables[1].Rows[emailIndex]["EMAIL_ID"].ToString();
                if (!string.IsNullOrEmpty(emailId))
                {
                    var distinctRows = DTwithDuplicate.Tables[0].AsEnumerable().FirstOrDefault(r => r.Field<string>("EMAIL_ID") == emailId);
                    if (distinctRows != null)
                    {
                        mfpId = distinctRows["MFP_ID"].ToString();
                        mfpIp = distinctRows["MFP_IP"].ToString();
                        emailHost = distinctRows["EMAIL_HOST"].ToString();
                        emailPort = distinctRows["EMAIL_PORT"].ToString();
                        emailUserName = distinctRows["EMAIL_USERNAME"].ToString();
                        emailPassword = distinctRows["EMAIL_PASSWORD"].ToString();
                        if (!string.IsNullOrEmpty(emailPassword))
                        {
                            emailPassword = ProvideDecryptedPassword(emailPassword);
                        }
                        isRequiredSSl = Convert.ToString(distinctRows["EMAIL_REQUIRE_SSL"], CultureInfo.CurrentCulture).ToLower();
                        isDirectPrint = bool.Parse(Convert.ToString(distinctRows["EMAIL_DIRECT_PRINT"], CultureInfo.CurrentCulture));

                        ftpProtocol = distinctRows["FTP_PROTOCOL"].ToString().ToLower();
                        ftpIPAddress = distinctRows["FTP_ADDRESS"].ToString();
                        ftpPort = distinctRows["FTP_PORT"].ToString();
                        ftpUserName = distinctRows["FTP_USER_ID"].ToString();
                        ftpUserPassword = distinctRows["FTP_USER_PASSWORD"].ToString();
                        if (!string.IsNullOrEmpty(ftpUserPassword))
                        {
                            ftpUserPassword = ProvideDecryptedPassword(ftpUserPassword);
                        }
                        FTPAddress = string.Format("{0}://{1}:{2}", ftpProtocol, ftpIPAddress, ftpPort);


                        try
                        {
                            
                           POP3_Wrapper popWrapper = new POP3_Wrapper();
                           //IMAP_Wrapper popWrapper = new IMAP_Wrapper();
                            string directory = string.Empty;
                            string folderName = (string)System.Configuration.ConfigurationManager.AppSettings["folderName"];
                            string emailCountPath = (string)System.Configuration.ConfigurationManager.AppSettings["EmailCountPath"];
                            if (string.IsNullOrEmpty(emailHost) || string.IsNullOrEmpty(emailUserName) || string.IsNullOrEmpty(emailPassword) || string.IsNullOrEmpty(emailPort) || isRequiredSSl != null)
                            {
                                try
                                {
                                    popWrapper.Connect(emailHost, emailUserName, emailPassword, Convert.ToInt32(emailPort), (isRequiredSSl == "true" ? true : false));
                                    isConnectionSuccess = true;
                                }
                                catch (EMailException ex)
                                {
                                    LogManager.RecordMessage(AUDITORSOURCE, "StartExtractingEmail", LogManager.MessageType.Detailed, ex.InnerException.Message, null, ex.InnerException.Message, ex.StackTrace);
                                    isConnectionSuccess = false;
                                }
                                int emailCount = popWrapper.GetMessagesCount();

                                messageCount = DataManager.Provider.Device.GetMessageCount(mfpIp);
                                try
                                {
                                    if (!string.IsNullOrEmpty(messageCount) || messageCount != "0")
                                    {
                                        int messageCountInt = int.Parse(messageCount);
                                        messageCountInt = messageCountInt - 1;
                                        messageCount = messageCountInt.ToString();
                                    }
                                }
                                catch (Exception)
                                {

                                }

                                if (emailCount > 0)
                                {
                                    string result = DataManager.Controller.Device.UpdateMessageCount(emailId, emailCount.ToString());
                                }
                                if (!string.IsNullOrEmpty(messageCount) && isConnectionSuccess|| messageCount != "0" && isConnectionSuccess)
                                {
                                    popWrapper.LoadMessages(messageCount, emailCount.ToString());

                                    foreach (POP3_Message_Wrapper pMW in popWrapper.Messages)
                                    {
                                        foreach (POP3_Mail_Attachment pMA in pMW.Attachments)
                                        {

                                            if (folderName.ToLower() == "username")
                                            {
                                                string[] firstName = pMW.From[0].ToString().Split('@');
                                                userNameMail = firstName[0];
                                            }
                                            else
                                            {
                                                userNameMail = pMW.From[0].ToString();
                                            }

                                            directory = string.Empty;

                                            directory = (string)System.Configuration.ConfigurationManager.AppSettings["DestinationPath"];

                                            directory = directory + "\\" + "EMAIL" + "\\" + userNameMail;

                                            if (!Directory.Exists(directory))
                                            {
                                                Directory.CreateDirectory(directory);
                                            }
                                            string filepathExists = directory + "\\" + pMA.Text;
                                            bool fileexists = File.Exists(filepathExists);
                                            if (!fileexists)
                                            {
                                                File.WriteAllBytes(directory + "\\" + pMA.Text, pMA.Body);
                                                status = true;
                                                //Send  Email for new/Existing  user (credentials)

                                                string file = pMA.Text;
                                                sbFileNames.Append(file + ",");



                                                if (isDirectPrint)
                                                {
                                                    FileInfo fileinfo = new FileInfo(filepathExists);
                                                    string fileName = fileinfo.Name;
                                                    string fileExtension = fileinfo.Extension;
                                                    //if (fileName.Length >= 25)
                                                    //{
                                                    //    fileName = fileName.Substring(0, 25) + fileExtension;
                                                    //}                                                     
                                                    FTPAddress = FTPAddress + "//" + fileName;
                                                    UploadFile(filepathExists, FTPAddress, ftpUserName, ftpUserPassword);
                                                }

                                                string[] pathNamearray = directory.Split('\\');
                                                StringBuilder sbPath = new StringBuilder();
                                                for (int pathInedx = 0; pathInedx < pathNamearray.Length - 1; pathInedx++)
                                                {
                                                    sbPath.Append(pathNamearray[pathInedx]);
                                                    sbPath.Append("\\");
                                                    directory = sbPath.ToString();
                                                }
                                            }
                                            else
                                            {
                                                string[] pathNamearray = directory.Split('\\');
                                                StringBuilder sbPath = new StringBuilder();
                                                for (int pathInedx = 0; pathInedx < pathNamearray.Length - 1; pathInedx++)
                                                {
                                                    sbPath.Append(pathNamearray[pathInedx]);
                                                    if (pathInedx < pathNamearray.Length - 2)
                                                    {
                                                        sbPath.Append("\\");
                                                    }
                                                    directory = sbPath.ToString();
                                                }
                                            }

                                        }
                                        // more investigation reqiured
                                        SendUserCredentials(userNameMail, sbFileNames, mfpIp);
                                    }
                                }
                            }
                        }
                        catch (InvalidCastException invalidCastException)
                        {
                            //When no mail with attachments this exception is thrown                        
                        }
                        catch (Exception ex)
                        {
                            //LogManager.RecordMessage(AUDITORSOURCE, "StartExtractingEmail", LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
                        }
                        finally
                        {

                        }
                    }
                }
            }


        }

        private static void SendUserCredentials(string userNameMail, StringBuilder sbFileNames, string mfpIP)
        {
            //string adminEmail = DataManager.Provider.Email.GetAdminEmail(mfpIP);
            if (!string.IsNullOrEmpty(userNameMail))
            {
                bool isUserEmailExists = DataManager.Provider.Email.isEmailExists(userNameMail);

                if (sbFileNames.Length > 0)
                {
                    if (isUserEmailExists)
                    {
                        DataSet dsuser = DataManager.Provider.Email.ProvideUserDetail(userNameMail);
                        //send user detail via email
                        if (dsuser.Tables[0].Rows.Count > 0)
                        {
                            string userName = dsuser.Tables[0].Rows[0]["USR_NAME"].ToString();
                            string userSource = dsuser.Tables[0].Rows[0]["USR_SOURCE"].ToString();
                            string userEmail = dsuser.Tables[0].Rows[0]["USR_EMAIL"].ToString();
                            string userID = dsuser.Tables[0].Rows[0]["USR_ID"].ToString();
                            string userPassword = "**********";
                           //DataManager.Provider.Email.SendEmailUserCredential(userPassword, userName, userEmail, userSource, "", userID, sbFileNames);
                        }
                    }
                    else
                    {
                        string userName = string.Empty;
                        string password = string.Empty;
                        string pin = string.Empty;

                        string[] userNameArray = userNameMail.Split('@');
                        userName = userNameArray[0].ToString();
                        string randomFirst = RandomString(4);
                        string randomSecond = RandomString(4);
                        string docNum = randomFirst + randomSecond;
                        password = docNum.ToLower();
                        pin = RandomInteger().ToString();

                        string result = DataManager.Controller.Mail.AddNewUsers(userName, userNameMail, ProvideEncryptedPassword(password), ProvideEncryptedPin(pin));
                        //if (!string.IsNullOrEmpty(adminEmail))
                        //{
                        //    userNameMail = adminEmail;
                        //}

                        DataManager.Provider.Email.SendEmailUserCredential(password, userName, userNameMail, "Local", pin, userNameMail, sbFileNames);
                    }
                }
            }
        }

        public static void UploadFile(string _FileName, string _UploadPath, string _FTPUser, string _FTPPass)
        {
            System.IO.FileInfo _FileInfo = new System.IO.FileInfo(_FileName);

            // Create FtpWebRequest object from the Uri provided
            System.Net.FtpWebRequest _FtpWebRequest = (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(new Uri(_UploadPath));

            // Provide the WebPermission Credentials
            _FtpWebRequest.Credentials = new System.Net.NetworkCredential(_FTPUser, _FTPPass);
            _FtpWebRequest.Proxy = null;
            //_FtpWebRequest.ConnectionGroupName = _FileName;
            // By default KeepAlive is true, where the control connection is not closed
            // after a command is executed.
            _FtpWebRequest.KeepAlive = false;

            // set timeout for 20 seconds
            _FtpWebRequest.Timeout = 30000;

            // Specify the command to be executed.
            _FtpWebRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile;

            // Specify the data transfer type.
            _FtpWebRequest.UseBinary = true;

            // Notify the server about the size of the uploaded file
            _FtpWebRequest.ContentLength = _FileInfo.Length;

            // The buffer size is set to 2kb
            int buffLength = 1024 * 4;
            byte[] buff = new byte[buffLength];

            // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
            System.IO.FileStream _FileStream = _FileInfo.OpenRead();

            // Stream to which the file to be upload is written
            System.IO.Stream _Stream = _FtpWebRequest.GetRequestStream();

            try
            {
                // Read from the file stream 2kb at a time
                int contentLen = _FileStream.Read(buff, 0, buffLength);

                // Till Stream content ends
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload Stream
                    _Stream.Write(buff, 0, contentLen);
                    contentLen = _FileStream.Read(buff, 0, buffLength);
                }

                // Close the file stream and the Request Stream
                _Stream.Close();
                _Stream.Dispose();
                _FileStream.Close();
                _FileStream.Dispose();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (_Stream != null)
                {
                    _Stream.Close();
                    _Stream.Dispose();
                }
                if (_FileStream != null)
                {
                    _FileStream.Close();
                    _FileStream.Dispose();
                }
                _FtpWebRequest = null;
            }

        }

        private static string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        private static int RandomInteger()
        {
            int returnValue = 0;
            int seedValue = unchecked((int)(DateTime.Now.Ticks));
            Random random = new Random(seedValue);
            returnValue = random.Next() % 900000 + 100000;
            return returnValue;
        }

        public static string EncryptString(string Message, string Passphrase)
        {
            byte[] EncryResults;
            System.Text.UTF8Encoding UTF8Encoding = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider Md5HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = Md5HashProvider.ComputeHash(UTF8Encoding.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Converting the input string to a byte[]
            byte[] DataToEncrypt = UTF8Encoding.GetBytes(Message);

            // Step 5. Attempt to encrypt the string
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                EncryResults = Encryptor.TransformFinalBlock(DataToEncrypt, 0, Message.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                Md5HashProvider.Clear();
            }

            // Step 6. Return the encrypted string as a base64 encoded string
            return Convert.ToBase64String(EncryResults);
        }

        /// <summary>
        /// Decrypts the string.
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="Passphrase">The passphrase.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Protector.DecryptString.jpg"/>
        /// </remarks>
        private static string DecryptString(string Message, string Passphrase)
        {
            byte[] DercyResults;
            System.Text.UTF8Encoding UTF8Encoding = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider Md5HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = Md5HashProvider.ComputeHash(UTF8Encoding.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the decoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToDecrypt = Convert.FromBase64String(Message);

            // Step 5. Attempt to decrypt the string
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                DercyResults = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                Md5HashProvider.Clear();
            }

            // Step 6. Return the decrypted string in UTF8 format
            return UTF8Encoding.GetString(DercyResults);
        }

        public static string ProvideEncryptedPassword(string plainPassword)
        {
            return EncryptString(plainPassword, ProvidePasswordSaltString());
        }

        /// <summary>
        /// Provides the decrypted password.
        /// </summary>
        /// <param name="plainPassword">The plain password.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Protector.ProvideDecryptedPassword.jpg"/>
        /// </remarks>
        public static string ProvideDecryptedPassword(string plainPassword)
        {
            return DecryptString(plainPassword, ProvidePasswordSaltString());
        }

        private static string ProvidePasswordSaltString()
        {
            return "P5HARPC0RP0RAT10NWD";
        }

        public static string ProvideEncryptedPin(string pinNumber)
        {
            return EncryptString(pinNumber, ProvidePinSaltString());
        }

        private static string ProvidePinSaltString()
        {
            return "PIN5HARPC0RP0RAT10N";
        }

    }
}
