using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Security;
using Lextm.SharpSnmpLib.Messaging;
using Mono.Options;

namespace MFPDiscovery
{
    public class SNMPFunc
    {
        string community = "public";
        VersionCode version = VersionCode.V2;
        int timeout = 1000;
        int retry = 0;
        ISnmpMessage response;
        IPEndPoint receiver;
        String strError;
        int maxRepetitions = 100;
        int nonRepeaters = 0;
        XmlDocument XmlResultDcmt = new XmlDocument();


        public XmlDocument Get(IPAddress ip)
        {

            bool showHelp = false;
            bool showVersion = false;


            Levels level = Levels.Reportable;
            string user = string.Empty;
            string authentication = string.Empty;
            string authPhrase = string.Empty;
            string privacy = string.Empty;
            string privPhrase = string.Empty;
            bool dump = false;




            OptionSet p = new OptionSet()
                .Add("c:", "Community name, (default is public)", delegate(string v) { if (v != null) community = v; })
                .Add("l:", "Security level, (default is noAuthNoPriv)", delegate(string v)
                                                                                   {
                                                                                       if (v.ToUpperInvariant() == "NOAUTHNOPRIV")
                                                                                       {
                                                                                           level = Levels.Reportable;
                                                                                       }
                                                                                       else if (v.ToUpperInvariant() == "AUTHNOPRIV")
                                                                                       {
                                                                                           level = Levels.Authentication | Levels.Reportable;
                                                                                       }
                                                                                       else if (v.ToUpperInvariant() == "AUTHPRIV")
                                                                                       {
                                                                                           level = Levels.Authentication | Levels.Privacy | Levels.Reportable;
                                                                                       }
                                                                                       else
                                                                                       {
                                                                                           throw new ArgumentException("no such security mode: " + v);
                                                                                       }
                                                                                   })
                .Add("a:", "Authentication method (MD5 or SHA)", delegate(string v) { authentication = v; })
                .Add("A:", "Authentication passphrase", delegate(string v) { authPhrase = v; })
                .Add("x:", "Privacy method", delegate(string v) { privacy = v; })
                .Add("X:", "Privacy passphrase", delegate(string v) { privPhrase = v; })
                .Add("u:", "Security name", delegate(string v) { user = v; })
                .Add("h|?|help", "Print this help information.", delegate(string v) { showHelp = v != null; })
                .Add("V", "Display version number of this application.", delegate(string v) { showVersion = v != null; })
                .Add("d", "Display message dump", delegate(string v) { dump = true; })
                .Add("t:", "Timeout value (unit is second).", delegate(string v) { timeout = int.Parse(v) * 1000; })
                .Add("r:", "Retry count (default is 0)", delegate(string v) { retry = int.Parse(v); })
                .Add("v|version:", "SNMP version (1, 2, and 3 are currently supported)", delegate(string v)
                                                                                               {
                                                                                                   switch (int.Parse(v))
                                                                                                   {
                                                                                                       case 1:
                                                                                                           version = VersionCode.V1;
                                                                                                           break;
                                                                                                       case 2:
                                                                                                           version = VersionCode.V2;
                                                                                                           break;
                                                                                                       case 3:
                                                                                                           version = VersionCode.V3;
                                                                                                           break;
                                                                                                       default:
                                                                                                           throw new ArgumentException("no such version: " + v);
                                                                                                   }
                                                                                               });

            //  XmlNode IpNode;
            //IpNode = new XmlNode()
            // XmlNode docNode = XmlResultDcmt.CreateXmlDeclaration("1.0", "UTF-8", null);
            //   XmlResultDcmt.AppendChild(docNode);
            XmlNode SourceNode = XmlResultDcmt.CreateElement("Source");
            XmlResultDcmt.AppendChild(SourceNode);
            XmlNode IPAddressNode = XmlResultDcmt.CreateElement("IPAddress");
            SourceNode.AppendChild(IPAddressNode);
            IPAddressNode.AppendChild(XmlResultDcmt.CreateTextNode(ip.ToString()));

            //Test for Ping
            Ping p1 = new Ping();
            PingReply PR = p1.Send(ip.ToString());
            // check when the ping is not success
            if (!PR.Status.ToString().Equals("Success"))
            {
                //Console.WriteLine(PR.Status.ToString());
                XmlNode ErrorNode = XmlResultDcmt.CreateElement("Error");
                SourceNode.AppendChild(ErrorNode);
                ErrorNode.AppendChild(XmlResultDcmt.CreateTextNode(PR.Status.ToString()));
                return XmlResultDcmt;
            }

            receiver = new IPEndPoint(ip, 161);
            List<Variable> vList = new List<Variable>();

            //Check whether it is MFP or Not




            Variable test = new Variable(new ObjectIdentifier("1.3.6.1.2.1.43.5.1.1.2.1"));
            vList.Add(test);



            Variable Result = Get(vList);

            if (Result == null)
            {
                XmlNode ErrorNode = XmlResultDcmt.CreateElement("Error");
                SourceNode.AppendChild(ErrorNode);
                ErrorNode.AppendChild(XmlResultDcmt.CreateTextNode("Node Not a SNMP Device"));

                return XmlResultDcmt;
            }
            else if (Result.Data.ToString() != "1")
            {
                XmlNode ErrorNode = XmlResultDcmt.CreateElement("Error");
                SourceNode.AppendChild(ErrorNode);
                ErrorNode.AppendChild(XmlResultDcmt.CreateTextNode("Node is a SNMP supported Device But Not a MFP"));
                return XmlResultDcmt;
            }

            XmlNode ResultNode = XmlResultDcmt.CreateElement("Result");
            SourceNode.AppendChild(ResultNode);

            /*
            //name
                vList.Add(new Variable( new ObjectIdentifier("1.3.6.1.2.1.1.5.0")));
            //Location
                vList.Add(new Variable(new ObjectIdentifier("1.3.6.1.2.1.1.6.0")));
           //Desc
                vList.Add(new Variable(new ObjectIdentifier("1.3.6.1.2.1.1.1.0")));
            //Model 
                vList.Add(new Variable(new ObjectIdentifier("1.3.6.1.2.1.25.3.2.1.3.1")));
            //Mac Address
                vList.Add(new Variable(new ObjectIdentifier("1.3.6.1.2.1.2.2.1.6.1")));
            //Cover details
                vList.Add(new Variable(new ObjectIdentifier("1.3.6.1.2.1.43.6")));*/
            vList.Clear();
            List<Variable> RsltList = new List<Variable>();
            //General
            vList.Add(new Variable(new ObjectIdentifier("1.3.6.1.2.1.1")));
            RsltList = GetBulk(vList);
            if (RsltList == null || RsltList.Count == 0)
            {
                XmlNode ErrorNode = XmlResultDcmt.CreateElement("Error");
                SourceNode.AppendChild(ErrorNode);
                ErrorNode.AppendChild(XmlResultDcmt.CreateTextNode("Error Retrieving Property, Please Check MFP supports SNMPv2"));
                return XmlResultDcmt;
            }
            ProcessSystem(RsltList, ref ResultNode);
            vList.Clear();
            RsltList.Clear();
            //Tray Details
            vList.Add(new Variable(new ObjectIdentifier("1.3.6.1.2.1.43.8")));
            RsltList = GetBulk(vList);
            ProcessTray(RsltList, ref ResultNode);
            vList.Clear();
            RsltList.Clear();
            //Toner details
            vList.Add(new Variable(new ObjectIdentifier("1.3.6.1.2.1.43.11.1.1.7.1.9")));
            RsltList = GetBulk(vList);
            ProcessToner(RsltList, ref ResultNode);
            vList.Clear();
            RsltList.Clear();
            //Count Details
            vList.Add(new Variable(new ObjectIdentifier("1.3.6.1.4.1.2385.1.1.19.2.1")));
            RsltList = GetBulk(vList);
            ProcessCounts(RsltList, ref ResultNode);




            //   vList.Add(new ObjectIdentifier(""));
            /* foreach (
                     Variable variable in
                         Messenger.Get(version, receiver, new OctetString(community), vList, timeout)) { 
             }

             GetNextRequestMessage message = new GetNextRequestMessage(0,
                               VersionCode.V1,
                               new OctetString("public"),
                               vList);
                
             try
             {
                 response = message.GetResponse(timeout, receiver);
             }
             catch(Exception e)
             {
                    
              }
             if (response.Pdu().ErrorStatus.ToInt32() != 0)
             {
                 XmlNode ErrorNode = XmlResultDcmt.CreateElement("Error");
                 SourceNode.AppendChild(ErrorNode);
                 ErrorNode.AppendChild(XmlResultDcmt.CreateTextNode("Node Not a MFP"));
                 return XmlResultDcmt;
             }
             else
             {
                 var result = response.Pdu().Variables;
                                      
             }*/


            //      ISnmpMessage response = request.GetResponse(timeout, receiver);
            /*  if (dump)
              {
                  Console.WriteLine(ByteTool.Convert(request.ToBytes()));
              }*/

            /*  if (response.Pdu().ErrorStatus.ToInt32() != 0) // != ErrorCode.NoError
              {
                 // throw ErrorException.Create(
                //      "error in response",
                //      receiver.Address,
               //       response);
                  XmlNode ErrorNode = XmlResultDcmt.CreateElement("Error");
                  SourceNode.AppendChild(ErrorNode);
                  ErrorNode.AppendChild(XmlResultDcmt.CreateTextNode("Node not a MFP"));
                  return XmlResultDcmt;
              }*/

            /*  foreach (Variable v in response.Pdu().Variables)
              {
                  Console.WriteLine(v);
              }*/


            return XmlResultDcmt;
            // check after the ping is n success
            /* while (PR.Status.ToString().Equals("Success"))
             {
                 Console.WriteLine(PR.Status.ToString());
                 PR = p1.Send("192.168.2.18");
             }*/
            //productNode.AppendChild(nameNode);
            //XmlNode IpNode = doc.CreateNode("element", "IpAddress", ip.ToString());
            // IpNode.InnerText = ip.ToString();
            //   XmlResultDcmt.AppendChild(IpNode);

            /*  bool parsed = IPAddress.TryParse(extra[0], out ip);
              if (!parsed)
              {
                  foreach (IPAddress address in
                      Dns.GetHostAddresses(extra[0]).Where(address => address.AddressFamily == AddressFamily.InterNetwork))
                  {
                      ip = address;
                      break;
                  }

                  if (ip == null)
                  {


                      XmlNode ErrorNode = doc.CreateNode("element", "IpAddress", "invalid host or wrong IP address found ");
                    //  IpNode.InnerText = ip.ToString();
                      IpNode.AppendChild(ErrorNode);
                    //  Console.WriteLine("invalid host or wrong IP address found: " + extra[0]);
                      return;
                  }
              }*/

            /*  try
              {
                  List<Variable> vList = new List<Variable>();
                  for (int i = 1; i < extra.Count; i++)
                  {
                      Variable test = new Variable(new ObjectIdentifier(extra[i]));
                      vList.Add(test);
                  }

                  IPEndPoint receiver = new IPEndPoint(ip, 161);
                  if (version != VersionCode.V3)
                  {
                      foreach (
                          Variable variable in
                              Messenger.Get(version, receiver, new OctetString(community), vList, timeout))
                      {
                          Console.WriteLine(variable);
                      }

                      return;
                  }
                
                  if (string.IsNullOrEmpty(user))
                  {
                      Console.WriteLine("User name need to be specified for v3.");
                      return;
                  }

                  IAuthenticationProvider auth = (level & Levels.Authentication) == Levels.Authentication
                                                     ? GetAuthenticationProviderByName(authentication, authPhrase)
                                                     : DefaultAuthenticationProvider.Instance;

                  IPrivacyProvider priv;
                  if ((level & Levels.Privacy) == Levels.Privacy)
                  {
                      priv = new DESPrivacyProvider(new OctetString(privPhrase), auth);
                  }
                  else
                  {
                      priv = new DefaultPrivacyProvider(auth);
                  }

                  Discovery discovery = Messenger.NextDiscovery;
                  ReportMessage report = discovery.GetResponse(timeout, receiver);

                  GetRequestMessage request = new GetRequestMessage(VersionCode.V3, Messenger.NextMessageId, Messenger.NextRequestId, new OctetString(user), vList, priv, Messenger.MaxMessageSize, report);

                  ISnmpMessage response = request.GetResponse(timeout, receiver);
                  if (dump)
                  {
                      Console.WriteLine(ByteTool.Convert(request.ToBytes()));
                  }

                  if (response.Pdu().ErrorStatus.ToInt32() != 0) // != ErrorCode.NoError
                  {
                      throw ErrorException.Create(
                          "error in response",
                          receiver.Address,
                          response);
                  }

                  foreach (Variable v in response.Pdu().Variables)
                  {
                      Console.WriteLine(v);
                  }
              }
              catch (SnmpException ex)
              {
                  Console.WriteLine(ex);
              }
              catch (SocketException ex)
              {
                  Console.WriteLine(ex);
              }*/
        }

        /*  private static void ShowHelp(OptionSet optionSet)
          {
              Console.WriteLine("#SNMP is available at http://sharpsnmplib.codeplex.com");
              Console.WriteLine("snmpget [Options] IP-address|host-name OID [OID] ...");
              Console.WriteLine("Options:");
              optionSet.WriteOptionDescriptions(Console.Out);
          }

          private static IAuthenticationProvider GetAuthenticationProviderByName(string authentication, string phrase)
          {
              if (authentication.ToUpperInvariant() == "MD5")
              {
                  return new MD5AuthenticationProvider(new OctetString(phrase));
              }
            
              if (authentication.ToUpperInvariant() == "SHA")
              {
                  return new SHA1AuthenticationProvider(new OctetString(phrase));
              }

              throw new ArgumentException("unknown name", "authentication");
          }*/

        private Variable Get(List<Variable> vList)
        {

            strError = null;
            try
            {
                foreach (
                          Variable variable in
                              Messenger.Get(version, receiver, new OctetString(community), vList, timeout))
                {
                    return variable;
                }
            }
            catch (Exception e)
            {
                strError = "Error Retrieving Property";
                return null;
            }

            return null;

        }
        private Variable GetNext(List<Variable> vList)
        {

            strError = null;

            GetNextRequestMessage message = new GetNextRequestMessage(0,
                                  VersionCode.V1,
                                  new OctetString("public"),
                                  vList);
            try
            {
                response = message.GetResponse(timeout, receiver);
            }
            catch (Exception e)
            {
                strError = "Error Retrieving Property";
                return null;
            }
            if (response.Pdu().ErrorStatus.ToInt32() != 0)
            {
                strError = "Error Retrieving Property";
                return null;
            }
            else
            {
                return response.Pdu().Variables[0];

            }


            return null;

        }



        private bool Split(Variable varMap, string oid, string val)
        {
            string strResult;

            strResult = varMap.ToString();



            return true;
        }

        private List<Variable> GetBulk(List<Variable> vList)
        {
            GetBulkRequestMessage message = new GetBulkRequestMessage(0,
                                                                              version,
                                                                              new OctetString(community),
                                                                              nonRepeaters,
                                                                              maxRepetitions, vList);
            List<Variable> RsltList = new List<Variable>(); ;

            try
            {
                response = message.GetResponse(5000, receiver);
            }

            catch (Exception e)
            {
                strError = "Error Retrieving Property";
                return null;
            }
            if (response.Pdu().ErrorStatus.ToInt32() != 0)
            {
                strError = "Error Retrieving Property";
                return null;
            }
            else
            {
                foreach (Variable variable in response.Pdu().Variables)
                {
                    RsltList.Add(variable);
                }
                //  RsltList = List<Variable>(response.Pdu().Variables);

            }



            return RsltList;
        }
        public void ProcessSystem(List<Variable> RsltList, ref XmlNode ResultNode)
        {

            string strOid = "";
            string strValue = "";

            XmlNode GeneralNode = XmlResultDcmt.CreateElement("General");
            ResultNode.AppendChild(GeneralNode);

            foreach (Variable variable in RsltList)
            {

                split(variable, ref strOid, ref strValue);
                //Name
                if (strOid.StartsWith(".1.3.6.1.2.1.1.5.0"))
                {
                    createTextElmt(ref GeneralNode, "Name", strValue);
                }
                if (strOid.StartsWith(".1.3.6.1.2.1.1.1.0"))
                {
                    createTextElmt(ref GeneralNode, "Model", strValue);
                }
                //Location
                if (strOid.StartsWith(".1.3.6.1.2.1.1.6.0"))
                {
                    createTextElmt(ref GeneralNode, "Location", strValue);
                }
                //Desc

                //Mac Address
                /* if (strOid.StartsWith(".1.3.6.1.2.1.2.2.1.6.1"))
                 {
                     createTextElmt(ref GeneralNode, "MacAddress", strValue);
                 }*/
            }
        }
        public void ProcessTray(List<Variable> RsltList, ref XmlNode ResultNode)
        {

            string strOid = "";
            string strValue = "";
            string strTrayName = "";
            string strTraySizeOid = "";
            string strTraySize = "";
            string strTrayCur = "";
            string strTrayMax = "";
            string strTrayUnit = "";

            XmlNode TrayDetNode = XmlResultDcmt.CreateElement("TrayDetails");
            ResultNode.AppendChild(TrayDetNode);

            foreach (Variable variable in RsltList)
            {
                split(variable, ref strOid, ref strValue);

                if (strOid.StartsWith(".1.3.6.1.2.1.43.8.2.1.13.1"))
                {
                    strTrayName = strValue;
                    //strTraySizeOid = strOid.Replace('13','12') ;

                    GetModOid(RsltList, strOid, ref strTraySize, "13", "12");

                    GetModOid(RsltList, strOid, ref strTrayCur, "13", "10");

                    GetModOid(RsltList, strOid, ref strTrayMax, "13", "9");

                    GetModOid(RsltList, strOid, ref strTrayUnit, "13", "8");
                    if (strTrayUnit == "8")
                    {
                        strTrayUnit = "Sheets";
                    }

                    //  XmlNode TrayNode = XmlResultDcmt.CreateElement(strTrayName);
                    // TrayDetNode.AppendChild(TrayNode);
                    XmlNode TrayNode = XmlResultDcmt.CreateElement("TrayName");
                    TrayDetNode.AppendChild(TrayNode);
                    TrayNode.AppendChild(XmlResultDcmt.CreateTextNode(strTrayName));

                    createTextElmt(ref TrayNode, "TrayPaperSize", strTraySize);

                    createTextElmt(ref TrayNode, "TrayCurrent", strTrayCur);

                    createTextElmt(ref TrayNode, "TrayCapacity", strTrayMax);

                    createTextElmt(ref TrayNode, "TrayUnits", strTrayUnit);


                    // RsltList.Find(y >= y.PartId.contains("")    
                    //  strTrayCur = ;
                    //  strTrayMax = ;
                    //  strTraySize = ;
                    //  strTrayunit = ;


                }


            }
            return;
        }
        public void ProcessToner(List<Variable> RsltList, ref XmlNode ResultNode)
        {

            string strOid = "";
            string strValue = "";
            string strTonerName = "";
            string strTonerCur = "";
            string strTonerMax = "";


            XmlNode TonerDetNode = XmlResultDcmt.CreateElement("TonerDetails");
            ResultNode.AppendChild(TonerDetNode);

            foreach (Variable variable in RsltList)
            {
                split(variable, ref strOid, ref strValue);

                if (strOid.StartsWith(".1.3.6.1.2.1.43.12.1.1.4.1"))
                {
                    strTonerName = strValue;
                    //strTraySizeOid = strOid.Replace('13','12') ;

                    GetModOid(RsltList, strOid, ref strTonerCur, ".12.1.1.4.1", ".11.1.1.9.1");

                    GetModOid(RsltList, strOid, ref strTonerMax, ".12.1.1.4.1", ".11.1.1.8.1");




                    XmlNode TonerNode = XmlResultDcmt.CreateElement("TonerName");
                    TonerDetNode.AppendChild(TonerNode);
                    TonerNode.AppendChild(XmlResultDcmt.CreateTextNode(strTonerName));

                    createTextElmt(ref TonerNode, "TrayCurrent", strTonerCur);

                    createTextElmt(ref TonerNode, "TrayCapacity", strTonerMax);


                    // RsltList.Find(y >= y.PartId.contains("")    
                    //  strTrayCur = ;
                    //  strTrayMax = ;
                    //  strTraySize = ;
                    //  strTrayunit = ;


                }


            }
            return;
        }

        public void ProcessCounts(List<Variable> RsltList, ref XmlNode ResultNode)
        {

            string strOid = "";
            string strValue = "";
            string strCounterName = "";
            string strCounterValue = "";



            XmlNode CounterDetNode = XmlResultDcmt.CreateElement("CounterDetails");
            ResultNode.AppendChild(CounterDetNode);

            foreach (Variable variable in RsltList)
            {
                split(variable, ref strOid, ref strValue);

                if (strOid.StartsWith(".1.3.6.1.4.1.2385.1.1.19.2.1.4"))
                {
                    strCounterName = strValue;
                    //strTraySizeOid = strOid.Replace('13','12') ;

                    GetModOid(RsltList, strOid, ref strCounterValue, ".2.1.4", ".2.1.3");



                    XmlNode CounterNode = XmlResultDcmt.CreateElement("CounterName");
                    CounterDetNode.AppendChild(CounterNode);
                    CounterNode.AppendChild(XmlResultDcmt.CreateTextNode(strCounterName));

                    createTextElmt(ref CounterNode, "CounterValue", strCounterValue);


                    // RsltList.Find(y >= y.PartId.contains("")    
                    //  strTrayCur = ;
                    //  strTrayMax = ;
                    //  strTraySize = ;
                    //  strTrayunit = ;


                }


            }
            return;
        }

        public void createTextElmt(ref XmlNode SrcNode, string NodeName, string NodeValue)
        {
            XmlNode ErrorNode = XmlResultDcmt.CreateElement(NodeName);
            SrcNode.AppendChild(ErrorNode);
            ErrorNode.AppendChild(XmlResultDcmt.CreateTextNode(NodeValue));
        }
        public void split(Variable variable, ref string strOid, ref string strValue)
        {
            string strVariable;
            string[] arrVariable;
            // char[] stringSeparators = new char[] { ';' };


            strValue = variable.Data.ToString();
            strOid = variable.Id.ToString();

            // arrVariable = strVariable.Split(stringSeparators);

            //  strOid = arrVariable[0];
            //  strValue = arrVariable[1];

        }
        public void GetModOid(List<Variable> RsltList, string strOid, ref string strNewVal, string oldChar, string newChar)
        {
            string strNewOid = "";
            Variable newVar;

            strNewOid = strOid.Replace(oldChar, newChar);


            newVar = RsltList.Find(y => y.Id.ToString() == strNewOid);
            // newVar = RsltList.Find(new Variable(new ObjectIdentifier(strNewOid)));

            strNewVal = newVar.Data.ToString();

            return;

        }
    }
}
