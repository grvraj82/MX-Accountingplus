using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace EventSimulator
{
    public static class SoapTransmitter
    {
        
        public static string SendRequest(string webMethod, string webUri, string soapSourcePath)
        {
            string soapResponse = string.Empty;
            try
            {
                DateTime wsRequest = DateTime.Now;

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(string.Format("\nWEB REQUEST {0}  START {1} ", webMethod, DateTime.Now));

                
                StreamReader reader = new StreamReader(soapSourcePath);
                string createJobQuery = reader.ReadToEnd();


                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(webUri);

                WebProxy prxy = new WebProxy("172.29.240.69", 80);
                prxy.Credentials = new NetworkCredential("internal", "internal", "ssdi.sharp.co.in");
                webRequest.Proxy = prxy;
                string soapHeaderValue = string.Format("\"urn:schemas-sc-jp:mfp:osa-1-1/{0}\"", webMethod);

                webRequest.Headers.Add("SOAPAction", soapHeaderValue);
                webRequest.ContentType = "text/xml;charset=\"utf-8\"";
                webRequest.Accept = "text/xml";
                webRequest.Method = "POST";

                using (Stream stream = webRequest.GetRequestStream())
                {
                    using (StreamWriter sw = new StreamWriter(stream))
                    {
                        sw.Write(createJobQuery);
                    }
                }

                WebResponse response = webRequest.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader responseReader = new StreamReader(responseStream);
                soapResponse = responseReader.ReadToEnd();
                responseReader.Close();
                responseReader.Dispose();

                DateTime wsResponse = DateTime.Now;

                TimeSpan tsWebReponseTime = wsResponse - wsRequest;
                                
               
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\nRespose : " + soapResponse + "\n"); 
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(string.Format("\nWEB REQUEST {0}  END {1} , Total Time = {2} Seconds", webMethod, DateTime.Now, tsWebReponseTime.TotalSeconds));

                
                

            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n Failed to process Web request placed ! \n\n Exception :" + ex.Message );
              
            }
            Console.ResetColor();
            return soapResponse;


        }
    }
}
