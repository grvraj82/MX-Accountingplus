using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization;

namespace EventSimulator
{
    public static class SOAPFormater
    {
        public static T SOAPToObject<T>(string SOAP)
        {
            if (string.IsNullOrEmpty(SOAP))
            {
                throw new ArgumentException("SOAP can not be null/empty");
            }
            using (MemoryStream Stream = new MemoryStream(UTF8Encoding.UTF8.GetBytes(SOAP)))
            {
                SoapFormatter Formatter = new SoapFormatter();
                return (T)Formatter.Deserialize(Stream);
            }
        }

        public static string ObjectToSOAP(object Object)
        {
            if (Object == null)
            {
                throw new ArgumentException("Object can not be null");
            }
            using (MemoryStream Stream = new MemoryStream())
            {
                SoapFormatter Serializer = new SoapFormatter();
                Serializer.Serialize(Stream, Object);
                Stream.Flush();
                return UTF8Encoding.UTF8.GetString(Stream.GetBuffer(), 0, (int)Stream.Position);
            }
        }

        public static string ToSoap(Object objToSoap, string filePath)
        {
            IFormatter formatter;
            FileStream fileStream = null;
            string strObject = "";
            try
            {
                fileStream = new FileStream(filePath,
                       FileMode.Create, FileAccess.Write);
                formatter = new SoapFormatter();
                formatter.Serialize(fileStream, objToSoap);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (fileStream != null) fileStream.Close();
            }
            return strObject;
        }

        public static object SoapToFromFile(string filePath)
        {
            IFormatter formatter;
            FileStream fileStream = null;
            Object objectFromSoap = null;
            try
            {
                fileStream = new FileStream(filePath,
                              FileMode.Open, FileAccess.Read);
                formatter = new SoapFormatter();
                objectFromSoap = formatter.Deserialize(fileStream);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (fileStream != null) fileStream.Close();
            }
            return objectFromSoap;
        }

        public static string ToSoap(Object objToSoap)
        {
            IFormatter formatter;
            MemoryStream memStream = null;
            string strObject = "";
            try
            {
                memStream = new MemoryStream();
                formatter = new SoapFormatter();
                formatter.Serialize(memStream, objToSoap);
                strObject =
                   Encoding.ASCII.GetString(memStream.GetBuffer());

                //Check for the null terminator character
                int index = strObject.IndexOf("\0");
                if (index > 0)
                {
                    strObject = strObject.Substring(0, index);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (memStream != null) memStream.Close();
            }
            return strObject;
        }

        public static object SoapTo(string soapString)
        {
            IFormatter formatter;
            MemoryStream memStream = null;
            Object objectFromSoap = null;
            try
            {
                byte[] bytes = new byte[soapString.Length];

                Encoding.ASCII.GetBytes(soapString, 0,
                             soapString.Length, bytes, 0);
                memStream = new MemoryStream(bytes);
                formatter = new SoapFormatter();
                objectFromSoap =
                     formatter.Deserialize(memStream);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (memStream != null) memStream.Close();
            }
            return objectFromSoap;
        }
    }
}
