#region Copyright SHARP Corporation
//    Copyright (c) 2005-2009 SHARP CORPORATION. All rights reserved.
//
//    SHARP OSA-NST
//
//    This software is protected under the Copyright Laws of the United States,
//    Title 17 USC, by the Berne Convention, and the copyright laws of other
//    countries.
//
//    THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDER ''AS IS'' AND ANY EXPRESS 
//    OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
//    OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//
//==============================================================================
#endregion

#region FileHeader
//===========================================================================
//  File          : OsaRequestInfo.cs
//
//  Module        : 
//                                                                             
//  Owner         : Sharp Software Development India
//
//  Author        :
//
//  Reference     :
//===========================================================================
#endregion

#region Review History
//===========================================================================
//  Reviews 
//
//  Sl#     Review Date     Reviewer(s)               Remarks
//===========================================================================
#endregion


using System;
using System.Web;

////using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace Osa.Util
{
	public class OsaRequestInfo
	{
		private string uisID;
        private string devID;
        private string userAgent;
		private string scheme;
		private string localHost;
		private string localPort;
		private string appPath;
		private string remoteHost;
		private string bcResolution;
		private string bcColor;
		private string bcAlternative;
		private string wsPort;

		public OsaRequestInfo(HttpRequest request)
		{
			uisID = request.Params["UISessionId"];
            if (string.IsNullOrEmpty(uisID))
            {
              uisID =request.Params["HTTP_X_UISessionId"];
            }
			devID = request.Params["DeviceId"];
            if (string.IsNullOrEmpty(devID))
            {
                devID = request.Params["HTTP_X_DeviceId"];
            }
			userAgent = request.UserAgent;
			scheme = request.Url.Scheme;
			localHost = request.Url.Host;
			localPort = request.Url.Port.ToString();
			appPath = request.ApplicationPath;
			remoteHost = request.UserHostAddress;
			bcResolution = request.Headers["X-BC-Resolution"];
			bcColor = request.Headers["X-BC-Color"];
			bcAlternative = request.Headers["X-BC-Alternative"];
			wsPort = request.Headers["X-WS-Port"];
		}

		public string GetMfpIPAddress()
		{
			return remoteHost;
		}

		public string GetMfpBaseUrl(string scheme, string wsPort)
		{
			return scheme + "://" + GetMfpIPAddress() + ":" + wsPort;
		}

		public string GetAppHostAddress()
		{
			return localHost;
		}

		public string GetAppBaseUrl()
		{
			return scheme + "://" + localHost + ":" + localPort + appPath;
		}

		public string GetAbsoluteUrl(string relUrl)
		{
			return GetAppBaseUrl() + relUrl;
		}

		public bool IsOpenSystems()
		{
			return userAgent.StartsWith("OpenSystems");
		}

		public string GetUISessionID()
		{
			return uisID;
		}

		public string GetDeviceID()
		{
			return devID;
		}

		private string GetUserAgentValue(string key)
		{
			string[] data = userAgent.Split(";".ToCharArray(), 256);
			foreach (string datum in data)
			{
				string[] pair = datum.Split("=".ToCharArray(), 2);

				if (pair.Length == 2 && pair[0].Trim() == key)
				{
					string val = pair[1].Trim();
					return val.Substring(1, val.Length - 2);
				}
			}

			return null;
		}

		public string GetProductFamily()
		{
			return GetUserAgentValue("product-family");
		}

		public string GetProductVersion()
		{
			return GetUserAgentValue("product-version");
		}

		public string GetBC_Resolution()
		{
            if (string.IsNullOrEmpty(bcResolution)) // use classic logic
			{
				string fami = GetProductFamily();
				if (fami == "37") return "VGA";
				if (fami == "45") return "VGA";
				if (fami == "57") return "VGA";
				return "Half-VGA";
			}
			return bcResolution;
		}

		public string GetBC_Color()
		{
            if (string.IsNullOrEmpty(bcColor))  // use classic logic
			{
				string fami = "Monochrome";
                switch (GetProductFamily())
                {
                    case "37":
                    case "45":
                    case "57":
                        fami = "Color";
                        break;
                    default:
                        break;
                }
                return fami;
			}
			return bcColor;
		}

		public string GetBC_Alternative()
		{
			return bcAlternative;
		}

		public string GetWS_Port()
		{
            if (string.IsNullOrEmpty(wsPort))   // use default
			{
				return "80;443";
			}
			return wsPort;
		}

		public string GetWS_HttpPort()
		{
			try
			{
				return GetWS_Port().Split(";".ToCharArray())[0];
			}
			catch (Exception ex)
			{
				return "80";    // Default HTTP Port
			}
		}

		public string GetWS_HttpsPort()
		{
			try
			{
				return GetWS_Port().Split(";".ToCharArray())[1];
			}
			catch (Exception ex)
			{
				return "443"; // Default HTTPS Port
			}
		}
	}
}
