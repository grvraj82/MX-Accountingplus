using System;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using System.Text;
using System.Collections;
using System.Data.Common;
using System.Globalization;

namespace RegistrationAdaptor
{
    [Serializable()]
    public class EmailMessageCounter : ISerializable
    {

        public DataTable EmailMessageCountDataTable { get; set; }

        /// <summary>
        ///Author(s): Varadharaj.G.R
        ///Function Name : EmailMessageCounter
        ///Description : 
        ///Inputs and Outputs : nil / nil
        ///Return Value : 
        ///Revision History : 
        ///Last Modified by : 
        /// </summary>
        public EmailMessageCounter()
        {
           
        }

        /// <summary>
        ///Author(s): Varadharaj.G.R
        ///Function Name : EmailMessageCounter
        ///Description : Deserialization constructor.
        ///Inputs and Outputs : SerializationInfo,StreamingContext / nil
        ///Return Value : 
        ///Revision History : 
        ///Last Modified by : 
        /// </summary>
        //Deserialization constructor.
        public EmailMessageCounter(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            EmailMessageCountDataTable = (DataTable)info.GetValue("EmailMessageCountDataTable", typeof(DataTable));
        }

        /// <summary>
        ///Author(s): Varadharaj.G.R
        ///Function Name : GetObjectData
        ///Description : Serialization function
        ///Inputs and Outputs : SerializationInfo,StreamingContext / nil
        ///Return Value : void
        ///Revision History : 
        ///Last Modified by : 
        /// </summary>
        //Serialization function.
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("EmailMessageCountDataTable", EmailMessageCountDataTable);
        }

        public static void CreateDataeTable(string licpath)
        {
            DataTable dt = new DataTable();
            DataColumn col1 = new DataColumn("EMAIL_ID");
            DataColumn col2 = new DataColumn("MESSAGE_COUNT");

            col1.DataType = System.Type.GetType("System.String");
            col2.DataType = System.Type.GetType("System.String");

            dt.Columns.Add(col1);
            dt.Columns.Add(col2);

            UpdateFile(dt, licpath);

        }

        public static void UpdateFile(DataTable dt, string licpath)
        {

            if (!File.Exists(licpath))
            {
                DataTable dt1 = new DataTable();
                EmailMessageCounter emailMessage = new EmailMessageCounter();
                emailMessage.EmailMessageCountDataTable = dt;
                FileStream fs = new FileStream(licpath, FileMode.Create);
                BinaryFormatter bformatter = new BinaryFormatter();
                bformatter.Serialize(fs, emailMessage);
                fs.Close();
                fs.Dispose();
            }
        }

        /// <summary>
        /// Encodes the string.
        /// </summary>
        /// <param name="origText">The orig text.</param>
        /// <returns></returns>
        public static string EncodeString(string origText)
        {
            byte[] stringBytes = Encoding.Unicode.GetBytes(origText);
            return Convert.ToBase64String(stringBytes, 0, stringBytes.Length);
        }

        /// <summary>
        /// Decodes the string.
        /// </summary>
        /// <param name="encodedText">The encoded text.</param>
        /// <returns></returns>
        public static string DecodeString(string encodedText)
        {
            byte[] stringBytes = Convert.FromBase64String(encodedText);
            return Encoding.Unicode.GetString(stringBytes);
        }

        public static DataTable UpdateTableAddNewRow(string countPath, string emailId, string messageCount)
        {
            Stream stream1 = File.Open(countPath, FileMode.Open);
            BinaryFormatter bformatter1 = new BinaryFormatter();
            EmailMessageCounter emailMessageCount = (EmailMessageCounter)bformatter1.Deserialize(stream1);
            DataTable dtEmailMessageCount = emailMessageCount.EmailMessageCountDataTable;


            DataRow drEmailMessageCount = dtEmailMessageCount.NewRow();
            drEmailMessageCount["EMAIL_ID"] =EncodeString(emailId.ToString());
            drEmailMessageCount["MESSAGE_COUNT"] = EncodeString( messageCount.ToString());

            dtEmailMessageCount.Rows.Add(drEmailMessageCount);

            emailMessageCount.EmailMessageCountDataTable = dtEmailMessageCount;
            stream1.Position = 0;
            bformatter1.Serialize(stream1, emailMessageCount);

            stream1.Close();
            stream1.Dispose();
            return dtEmailMessageCount;
        }

        public static DataTable UpdateTableExistingRow(string countPath, string emailId, string messageCount)
        {
            Stream stream1 = File.Open(countPath, FileMode.Open);
            BinaryFormatter bformatter1 = new BinaryFormatter();
            EmailMessageCounter emailMessageCount = (EmailMessageCounter)bformatter1.Deserialize(stream1);
            DataTable dtEmailMessageCount = emailMessageCount.EmailMessageCountDataTable;


            foreach (DataRow drEmailMessageCount in dtEmailMessageCount.Rows)
            {
                String checkEmailID = DecodeString(drEmailMessageCount["EMAIL_ID"].ToString());
                if (emailId == checkEmailID)
                {
                    drEmailMessageCount["MESSAGE_COUNT"] = EncodeString(messageCount);
                    drEmailMessageCount.EndEdit();
                    dtEmailMessageCount.AcceptChanges();
                }

            }

            emailMessageCount.EmailMessageCountDataTable = dtEmailMessageCount;
            stream1.Position = 0;
            bformatter1.Serialize(stream1, emailMessageCount);

            stream1.Close();
            stream1.Dispose();
            return dtEmailMessageCount;
        }

        public static bool isEmailIdExists(string countPath, string emailID)
        {
            bool isEmailId = false;

            Stream stream = File.Open(countPath, FileMode.Open);
            BinaryFormatter binaryformatter = new BinaryFormatter();
            EmailMessageCounter emailMessageCount = (EmailMessageCounter)binaryformatter.Deserialize(stream);
            DataTable dtemailMessageCount = emailMessageCount.EmailMessageCountDataTable;

            stream.Close();
            stream.Dispose();
            int rows = dtemailMessageCount.Rows.Count;

            ArrayList arrayList = new ArrayList();
            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {

                arrayList.Add(EmailMessageCounter.DecodeString(dtemailMessageCount.Rows[rowIndex]["EMAIL_ID"].ToString())); 
            }

            if (arrayList.Contains(emailID))
            {
                isEmailId = true;
            }

            return isEmailId;
        }

        public static string provideMessageCount(string countPath, string emailID)
        {
            string returnValue = string.Empty;

            Stream stream = File.Open(countPath, FileMode.Open);
            BinaryFormatter binaryformatter = new BinaryFormatter();
            EmailMessageCounter emailMessageCount = (EmailMessageCounter)binaryformatter.Deserialize(stream);
            DataTable dtemailMessageCount = emailMessageCount.EmailMessageCountDataTable;

            stream.Close();
            stream.Dispose();

            DataRow[] result = dtemailMessageCount.Select("EMAIL_ID ='" + EncodeString(emailID) + "' ");
            
            if(result!=null)
            {
                returnValue = DecodeString(result[0]["MESSAGE_COUNT"].ToString());
            }
            return returnValue;
        }
    }
}
