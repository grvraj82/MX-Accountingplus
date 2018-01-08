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
using ApplicationAuditor;

namespace RegistrationInfo
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class RegistrationInf : ISerializable
    {
        /// <summary>
        /// Gets or sets the licence data table.
        /// </summary>
        /// <value>
        /// The licence data table.
        /// </value>
        public DataTable LicenceDataTable { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationInf"/> class.
        /// </summary>
        public RegistrationInf()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationInf"/> class.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="ctxt">The CTXT.</param>
        public RegistrationInf(SerializationInfo info, StreamingContext ctxt)
        {

            LicenceDataTable = (DataTable)info.GetValue("LicenceDataTable", typeof(DataTable));

        }

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext"/>) for this serialization.</param>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("LicenceDataTable", LicenceDataTable);
        }



        /// <summary>
        /// Updates the table.
        /// </summary>
        /// <param name="licPath2">The lic path2.</param>
        /// <param name="clientCode">The client code.</param>
        /// <param name="activationCode">The activation code.</param>
        /// <returns></returns>
        public static DataTable UpdateTable(string licPath2, string clientCode, string activationCode)
        {
            Stream stream1 = File.Open(licPath2, FileMode.Open);
            BinaryFormatter bformatter1 = new BinaryFormatter();
            RegistrationInf wqe = (RegistrationInf)bformatter1.Deserialize(stream1);
            DataTable dt = wqe.LicenceDataTable;


            DataRow dr = dt.NewRow();
            dr["CLIENT_CODE"] = clientCode.ToString();
            dr["ACTIVATION_CODE"] = activationCode.ToString();

            dt.Rows.Add(dr);

            wqe.LicenceDataTable = dt;
            stream1.Position = 0;
            bformatter1.Serialize(stream1, wqe);

            stream1.Close();
            stream1.Dispose();
            return dt;
        }

        /// <summary>
        /// Determines whether [is activation exist] [the specified lic path2].
        /// </summary>
        /// <param name="licPath2">The lic path2.</param>
        /// <param name="activativationCodeExist">The activativation code exist.</param>
        /// <returns>
        ///   <c>true</c> if [is activation exist] [the specified lic path2]; otherwise, <c>false</c>.
        /// </returns>
        public static bool isActivationExist(string licPath2, string activativationCodeExist)
        {
            bool activationCode = false;

            Stream stream1 = File.Open(licPath2, FileMode.Open);
            BinaryFormatter bformatter1 = new BinaryFormatter();
            RegistrationInf wqe = (RegistrationInf)bformatter1.Deserialize(stream1);
            DataTable dt = wqe.LicenceDataTable;

            stream1.Close();
            stream1.Dispose();
            int rows = dt.Rows.Count;

            ArrayList arrayList = new ArrayList();
            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {

                arrayList.Add(RegistrationInf.DecodeString(dt.Rows[rowIndex]["ACTIVATION_CODE"].ToString())); // Where ACTIVATION_CODE is the name of fields from  PR.dat
            }

            if (arrayList.Contains(activativationCodeExist))
            {
                activationCode = true;
            }

            return activationCode;
        }

        /// <summary>
        /// Determines whether [is client code exist] [the specified lic path2].
        /// </summary>
        /// <param name="licPath2">The lic path2.</param>
        /// <param name="deviceIp">The device ip.</param>
        /// <returns>
        ///   <c>true</c> if [is client code exist] [the specified lic path2]; otherwise, <c>false</c>.
        /// </returns>
        public static bool isClientCodeExist(string licPath2, string deviceIp)
        {
            bool clientCode = false;
            // deviceIp = "172.29.241.40";
            Stream stream1 = File.Open(licPath2, FileMode.Open);
            BinaryFormatter bformatter1 = new BinaryFormatter();
            RegistrationInf wqe = (RegistrationInf)bformatter1.Deserialize(stream1);
            DataTable dt = wqe.LicenceDataTable;
            int count = dt.Rows.Count;

            stream1.Close();
            stream1.Dispose();
            int rows = dt.Rows.Count;
            DbDataReader deviceDetails = DataManager.Provider.Device.ProvideDeviceDetails(deviceIp);

            string deviceClientCode = string.Empty;
            string deviceSerial = string.Empty;
            string deviceModel = string.Empty;
            string clientCodeOld = string.Empty;
            string serialold = string.Empty;
            string modelold = string.Empty;
            try
            {

                if (deviceDetails.HasRows)
                {

                    deviceDetails.Read();
                    clientCodeOld = deviceClientCode = Convert.ToString(deviceDetails["MFP_SERIALNUMBER"], CultureInfo.CurrentCulture) + Convert.ToString(deviceDetails["MFP_MODEL"], CultureInfo.CurrentCulture);
                    serialold = deviceSerial = Convert.ToString(deviceDetails["MFP_SERIALNUMBER"], CultureInfo.CurrentCulture);
                    modelold = deviceModel = Convert.ToString(deviceDetails["MFP_MODEL"], CultureInfo.CurrentCulture);

                    if (!string.IsNullOrEmpty(deviceClientCode))
                    {
                        deviceClientCode = SerialIdentity(deviceClientCode);
                    }

                    if (!string.IsNullOrEmpty(deviceSerial))
                    {
                        deviceSerial = SerialIdentity(deviceSerial);
                    }

                    if (!string.IsNullOrEmpty(deviceModel))
                    {
                        deviceModel = SerialIdentity(deviceModel);
                    }
                    if (!string.IsNullOrEmpty(clientCodeOld))
                    {
                        clientCodeOld = SerialIdentityOld(clientCodeOld);
                    }

                    if (!string.IsNullOrEmpty(serialold))
                    {
                        serialold = SerialIdentityOld(serialold);
                    }

                    if (!string.IsNullOrEmpty(modelold))
                    {
                        modelold = SerialIdentityOld(modelold);
                    }

                }
                deviceDetails.Close();
                ArrayList arrayList = new ArrayList();
                for (int rowIndex = 0; rowIndex < rows; rowIndex++)
                {

                    arrayList.Add(RegistrationInf.DecodeString(dt.Rows[rowIndex]["CLIENT_CODE"].ToString())); // Where CLIENT_CODE is the name of fields from  PR.dat
                }

                if (arrayList.Contains(deviceClientCode) || arrayList.Contains(deviceSerial) || arrayList.Contains(deviceModel) || arrayList.Contains(clientCodeOld) || arrayList.Contains(serialold) || arrayList.Contains(modelold))
                {

                    clientCode = true;
                }

                if (arrayList.Contains(clientCodeOld) || arrayList.Contains(serialold) || arrayList.Contains(modelold))
                {

                    clientCode = true;
                }

            }
            catch (Exception ex)
            {
                LogManager.RecordMessage("isClientCodeExist", "isClientCodeExist", LogManager.MessageType.Success, ex.Message, "", "", "");
            }

            return clientCode;
        }

        private static string SerialIdentity(string uniqueID)
        {
            uniqueID = "C" + uniqueID;
            return uniqueID;
        }

        private static string SerialIdentityOld(string uniqueID)
        {
            uniqueID = uniqueID.Remove(0, 1);
            uniqueID = "C" + uniqueID;
            return uniqueID;
        }

        /// <summary>
        /// Creates the datae table.
        /// </summary>
        /// <param name="licpath">The licpath.</param>
        public static void CreateDataeTable(string licpath)
        {
            DataTable dt = new DataTable();
            DataColumn col1 = new DataColumn("CLIENT_CODE");
            DataColumn col2 = new DataColumn("ACTIVATION_CODE");

            col1.DataType = System.Type.GetType("System.String");
            col2.DataType = System.Type.GetType("System.String");

            dt.Columns.Add(col1);
            dt.Columns.Add(col2);

            UpdateFile(dt, licpath);

        }

        public static void GetNumberOfServerRegiesterd(string licPath, out int serverCount, out int deviceCount)
        {

            Stream stream1 = File.Open(licPath, FileMode.Open, FileAccess.ReadWrite);
            BinaryFormatter bformatter1 = new BinaryFormatter();
            RegistrationInf wqe = (RegistrationInf)bformatter1.Deserialize(stream1);
            DataTable dt = wqe.LicenceDataTable;
            int count = dt.Rows.Count;
            stream1.Close();
            stream1.Dispose();
            int rows = dt.Rows.Count;
            //get count of server and device regiesterd.
            serverCount = 0;
            deviceCount = 0;
            string value = string.Empty;
            for (int i = 0; i < rows; i++)
            {
                value = RegistrationInf.DecodeString(dt.Rows[i]["CLIENT_CODE"].ToString());
                if (!string.IsNullOrEmpty(value))
                {
                    if (value.StartsWith("S"))
                    {
                        serverCount = serverCount + 1;
                    }
                    if (value.StartsWith("C"))
                    {
                        deviceCount = deviceCount + 1;
                    }
                }
            }

        }



        /// <summary>
        /// Updates the file.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="licpath">The licpath.</param>
        public static void UpdateFile(DataTable dt, string licpath)
        {

            if (!File.Exists(licpath))
            {
                DataTable dt1 = new DataTable();
                RegistrationInf reg = new RegistrationInf();
                reg.LicenceDataTable = dt;
                FileStream fs = new FileStream(licpath, FileMode.Create);
                BinaryFormatter bformatter = new BinaryFormatter();
                bformatter.Serialize(fs, reg);
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
    }
}