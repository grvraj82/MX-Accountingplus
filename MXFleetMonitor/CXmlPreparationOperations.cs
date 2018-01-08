using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace FleetManagement
{
    struct SNMPCredentials
    {
        public SNMPCredentials(_authVersion _authVersion, _privVersion _privVersion)
        {
            certificateName = "";
            userName = "";
            authPassword = "";
            privPassword = "";
            authVersion = _authVersion;
            privVersion = _privVersion;
        }
        public string certificateName;
        public string userName;
        public string authPassword;
        public string privPassword;

        public _authVersion authVersion;
        public _privVersion privVersion;
    }

    enum _authVersion : int
    {
        None = 0,
        MD5,
        SHA
    };


    enum _privVersion : int
    {
        None = 0,
        DES,
        AES
    };

    class CXmlPreparationOperations
    {
        public System.Xml.XmlDocument xmlDocument = new XmlDocument();

        private int _retry = 2;

        public int Retry
        {
            get { return _retry; }
            set { _retry = value; }
        }
        private int _timeOut = 2000;

        public int TimeOut
        {
            get { return _timeOut; }
            set { _timeOut = value; }
        }

        private int _snmpVersion = 1;

        public int SnmpVersion
        {
            get { return _snmpVersion; }
            set { _snmpVersion = value; }
        }

        private string _communityString = "public";

        public string CommunityString
        {
            get { return _communityString; }
            set { _communityString = value; }
        }

        private SNMPCredentials _snmpCredentials;

        public SNMPCredentials SnmpCredentials
        {
            get { return _snmpCredentials; }
            set { _snmpCredentials = value; }
        }

        public CXmlPreparationOperations(string strDocumentPath)
        {
            xmlDocument.Load(strDocumentPath);
        }

        public int SetSNMPCredentialsAndRetryTimeout()
        {
            if (1 == SnmpVersion)
            {
                // Set SNMP Version
                //

                System.Xml.XmlNode nodeSNMPVersion = xmlDocument.SelectSingleNode(@"system/manager/data[@ name='get/method/_is1/type']");
                nodeSNMPVersion.InnerText = "SNMP";

                // Set SNMP Community String
                //
                System.Xml.XmlNode nodeCommunityString = xmlDocument.SelectSingleNode(@"system/manager/data[@ name='snmp/communityStringForGetOperationList']");
                nodeCommunityString.InnerText = CommunityString;

                // Set SNMP Retry 
                //
                System.Xml.XmlNode nodeRetry = xmlDocument.SelectSingleNode(@"system/manager/data[@ name='snmp/retryNumber/_is1']");
                nodeRetry.InnerText = Retry.ToString();

                // Set SNMP Timeout
                //
                System.Xml.XmlNode nodeTimeOut = xmlDocument.SelectSingleNode(@"system/manager/data[@ name='snmp/timeout/_is1']");
                nodeTimeOut.InnerText = TimeOut.ToString();

            }
            else if (3 == SnmpVersion)
            {
                System.Xml.XmlNode nodeSNMPVersion = xmlDocument.SelectSingleNode(@"system/manager/data[@ name='get/method/_is1/type']");
                nodeSNMPVersion.InnerText = "SNMPv3";

                // Set UserName
                //
                System.Xml.XmlNode nodeSNMPUserName = xmlDocument.SelectSingleNode(@"system/manager/data[@ name='snmpv3/securityCredentialsList']/allowedValueList/security");
                nodeSNMPUserName.Attributes["name"].Value = SnmpCredentials.userName;


                System.Xml.XmlNodeList xmlChildNodeList = nodeSNMPUserName.ChildNodes;


                foreach (XmlElement xmlElementList in xmlChildNodeList)
                {
                    switch (xmlElementList.LocalName)
                    {

                        case "AuthorisationProtocol":
                            switch (SnmpCredentials.authVersion)
                            {
                                case _authVersion.None:
                                    xmlElementList.InnerText = "None";
                                    break;

                                case _authVersion.MD5:
                                    xmlElementList.InnerText = "MD5";
                                    break;

                                case _authVersion.SHA:
                                    xmlElementList.InnerText = "SHA";
                                    break;

                                default:
                                    xmlElementList.InnerText = "MD5";
                                    break;

                            };
                            break;

                        case "AuthorisationPassword":
                            xmlElementList.InnerText = SnmpCredentials.authPassword;
                            break;

                        //Set Privacy Protocol
                        //
                        case "PrivacyProtocol":
                            switch (SnmpCredentials.privVersion)
                            {
                                case _privVersion.None:
                                    xmlElementList.InnerText = "None";
                                    break;

                                case _privVersion.DES:
                                    xmlElementList.InnerText = "DES";
                                    break;

                                case _privVersion.AES:
                                    xmlElementList.InnerText = "AES";
                                    break;

                                default:
                                    xmlElementList.InnerText = "DES";
                                    break;

                            };
                            break;

                        case "PrivacyPassword":
                            xmlElementList.InnerText = SnmpCredentials.privPassword;
                            break;
                    } // Switch Nodes

                } // for Loop

                // Set SNMP Retry 
                //
                System.Xml.XmlNode nodeRetry = xmlDocument.SelectSingleNode(@"system/manager/data[@ name='snmpv3/retryNumber/_is1']");
                nodeRetry.InnerText = Retry.ToString();

                // Set SNMP Timeout
                //
                System.Xml.XmlNode nodeTimeOut = xmlDocument.SelectSingleNode(@"system/manager/data[@ name='snmpv3/timeout/_is1']");
                nodeTimeOut.InnerText = TimeOut.ToString();
            } // SNMPv3

            return 1;
        }


        public int ConfigureSNMP()
        {
            //Console.WriteLine("Enter SNMP Version \n '1'  for SNMPv1 and '3' for SNMPv3 ");
            String strSNMPVersion = "1";// Console.ReadLine();
            int nSNMPVersion = Convert.ToInt32(strSNMPVersion);

            //Console.WriteLine("Enter SNMP Retries");
            string strRetry = "1";//Console.ReadLine();
            Retry = Convert.ToInt32(strRetry);

            //Console.WriteLine("Enter SNMP Timeout");
            string strTimeout = "1000";// Console.ReadLine();
            TimeOut = Convert.ToInt32(strTimeout);


            if (1 == nSNMPVersion)
            {
                SnmpVersion = 1;

                //Console.WriteLine("Enter SNMPv1 Community String");
                CommunityString = "public";//Console.ReadLine();
            }
            else if (3 == nSNMPVersion)
            {
                SnmpVersion = 3;

                Console.WriteLine("Enter Certificate Name ");
                SNMPCredentials snmpCredentials = new SNMPCredentials();

                snmpCredentials.certificateName = Console.ReadLine();

                Console.WriteLine("Enter User Name ");
                snmpCredentials.userName = Console.ReadLine();

                Console.WriteLine("Enter Authentication Protocal '0' for None, '1' for MD5 and '2' for SHA ");
                string strAuthProtocal = Console.ReadLine();
                int iAuthProtocol = Convert.ToInt32(strAuthProtocal);

                switch (iAuthProtocol)
                {
                    case 0:
                        snmpCredentials.authVersion = _authVersion.None;
                        break;

                    case 1:
                        snmpCredentials.authVersion = _authVersion.MD5;
                        break;

                    case 2:
                        snmpCredentials.authVersion = _authVersion.SHA;
                        break;

                    default:
                        snmpCredentials.authVersion = _authVersion.MD5;
                        break;

                };

                Console.WriteLine("Enter Authentication Password ");
                snmpCredentials.authPassword = Console.ReadLine();

                Console.WriteLine("Enter Security Protocal '0' for None, '1' for DES and '2' for AES ");
                string strPrivProtocol = Console.ReadLine();
                int iPrivProtocol = Convert.ToInt32(strPrivProtocol);

                switch (iPrivProtocol)
                {
                    case 0:
                        snmpCredentials.privVersion = _privVersion.None;
                        break;

                    case 1:
                        snmpCredentials.privVersion = _privVersion.DES;
                        break;

                    case 2:
                        snmpCredentials.privVersion = _privVersion.AES;
                        break;

                    default:
                        snmpCredentials.privVersion = _privVersion.DES;
                        break;

                };


                Console.WriteLine("Enter Privacy Password ");
                snmpCredentials.privPassword = Console.ReadLine();

                SnmpCredentials = snmpCredentials;
            } // SNMPv3

            int iRet = SetSNMPCredentialsAndRetryTimeout();

            return 1;

        } // ConfigureSNMP


    } // Class CXmlPreparationOperations





}
