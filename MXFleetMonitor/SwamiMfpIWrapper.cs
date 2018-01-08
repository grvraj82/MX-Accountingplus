using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Xml;
using System.IO;
using System.Reflection;
using ApplicationAuditor;

namespace FleetManagement
{
    public class SwamiMfpIWrapper
    {
        [DllImport("DSMCMFPInterface.dll")]
        static extern int Initialize();

        [DllImport("DSMCMFPInterface.dll")]
        static extern int Get(MSXML2.IXMLDOMDocument statusUpdateXML, String IPAddress);
        [DllImport("DSMCMFPInterface.dll")]
        static extern int Uninitialize(int resultofInitialize);

        //[DllImport("DSMCMFPInterface.dll")]
        //static extern integer StartDiscovery(MSXML2.IXMLDOMDocument discoveryXML, 


        private int resultofInitialize = 0;

        public SwamiMfpIWrapper()
        {
            resultofInitialize = Initialize();
        }

        public int StatusUpdate(String IPAddress, ref System.Xml.XmlDocument RequestXML, ref System.Xml.XmlDocument data)
        {
            string strXML = RequestXML.InnerXml;

            MSXML2.IXMLDOMDocument statusUpdateXML = new MSXML2.DOMDocument();

            statusUpdateXML.loadXML(strXML.ToString());

            string iPAddresslocal = IPAddress;
            try
            {
                int StatusUpdateID = Get(statusUpdateXML, iPAddresslocal);
            }
            catch (Exception ex)
            {
            }
            data.LoadXml(statusUpdateXML.xml);

            //string destination="";
            //string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            //UriBuilder uri = new UriBuilder(codeBase);
            //string path = Uri.UnescapeDataString(uri.Path);
            //destination = Path.Combine(Path.GetDirectoryName(path), "ResponseXML.xml");
            //statusUpdateXML.save(destination);

            return 1;
        }

        ~SwamiMfpIWrapper()
        {
            //Uninitialize(resultofInitialize);
        }

    };
}
