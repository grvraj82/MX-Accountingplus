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
//  File          : OsaBridge.Direct.asx.cs
//
//  Module        : 
//                                                                             
//  Owner         : Sharp Software Development India
//
//  Author        : Rajshekhar
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
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Configuration;
using System.Web.Services.Protocols;
using System.Net;
using OsaDirectManager.Osa.MfpWebService;
using System.Web.Configuration;
using System.Reflection;
using System.Text;
using System.Globalization;
using System.Xml;

namespace OsaDirectManager
{

    public class Core
    {
        public const long TIMEOUT = 5000;
        public static string OSA_LICENSE_KEY = "VOdhRan7yFBQU8VOopBzcdKnTkFhqSZUqFX6TpamCWP9f9fi6o5AhYX+2/RkRjzxDFHHclC4nZNfjS2WgHiWCQ==";
        public static string g_WSDLGeneric = "1.0.0.23";

        // display top-level screen of the MFP
        public static void ShowTopLevelScreen(MFPCoreWS webService, string sessionID)
        {
            try
            {
                if (sessionID.Length > 0)
                {
                    SHOWSCREEN_TYPE screen_addr = new SHOWSCREEN_TYPE();
                    E_MFP_SHOWSCREEN_TYPE mfpTLS;
                    mfpTLS = E_MFP_SHOWSCREEN_TYPE.TOP_LEVEL_SCREEN;
                    screen_addr.Item = mfpTLS;

                    webService.ShowScreen(sessionID, screen_addr, ref g_WSDLGeneric);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void AssignOSALicenceKey(string osaLicenceKey)
        {
            if (!string.IsNullOrEmpty(osaLicenceKey))
            {
                OSA_LICENSE_KEY = osaLicenceKey;
            }
        }

        public static string GetMFPURL(string sIP)
        {
            string sURL = string.Empty;
            if (String.Compare(WebConfigurationManager.AppSettings.Get("UseSSL").ToString(), "true", true, CultureInfo.CurrentCulture) == 0)
            {
                sURL = @"https://" + sIP + @"/MfpWebServices/MFPCoreWS.asmx";
                //Set the SSL policy
                // surpress warning due to self-signed certificate
                //ServicePointManager.CertificatePolicy = new OSAScanOverrideSSLPolicy();

            }
            else
            {
                sURL = @"http://" + sIP + @"/MfpWebServices/MFPCoreWS.asmx";
            }
            return sURL;
        }

        //XML_DOC_SET_TYPE to XML_DOC_DSC_TYPE conversion 
        public static XML_DOC_SET_TYPE ConvertDSCToSETType(XML_DOC_DSC_TYPE DSCsettings)
        {
            if (DSCsettings == null || DSCsettings.complex.Length < 1)
                return null;

            XML_DOC_SET_TYPE xmlSetDoc = new XML_DOC_SET_TYPE();
            COMPLEX_SET_TYPE[] complexField = xmlSetDoc.complex;
            PopulateComplexSetType(DSCsettings.complex, ref complexField);
            xmlSetDoc.complex = complexField;
            return xmlSetDoc;
        }
        //sets the complex array recursively into XML_DOC_SET_TYPE complex
        private static void PopulateComplexSetType(COMPLEX_DSC_TYPE[] settings, ref COMPLEX_SET_TYPE[] complexSET)
        {
            if (settings != null && settings.Length > 0)
            {
                int count = 0;
                complexSET = new COMPLEX_SET_TYPE[settings.Length];
                foreach (COMPLEX_DSC_TYPE complex in settings)
                {
                    if (complex != null && (complex.sysname != null && complex.sysname.Length > 0))
                    {
                        complexSET[count] = new COMPLEX_SET_TYPE();
                        complexSET[count].sysname = complex.sysname;//scanner
                        PROPERTY_SET_TYPE[] complexField = complexSET[count].property;
                        COMPLEX_SET_TYPE[] complexField1 = complexSET[count].complex;
                        PopulatePropertySetType(complex.property, ref complexField);
                        complexSET[count].property = complexField;
                        if (complex.complex != null)
                        {
                            PopulateComplexSetType(complex.complex, ref complexField1);
                        }
                        complexSET[count].complex = complexField1;
                        count++;
                    }
                }
            }
        }
        //sets the prop array recursively into XML_DOC_SET_TYPE complex property
        private static void PopulatePropertySetType(PROPERTY_DSC_TYPE[] propertyDSCList, ref PROPERTY_SET_TYPE[] propertySET)
        {
            if (propertyDSCList != null && propertyDSCList.Length > 0)
            {
                propertySET = new PROPERTY_SET_TYPE[propertyDSCList.Length];
                for (int i = 0; i < propertyDSCList.Length; i++)
                {
                    propertySET[i] = new PROPERTY_SET_TYPE();
                    propertySET[i].sysname = propertyDSCList[i].sysname;
                    propertySET[i].Value = propertyDSCList[i].value;
                }
            }
        }

        public static string GetFileExtension(XML_DOC_SET_TYPE setXMLDOC)
        {
            const string fileformat = "file-format";//should match XML_DOC description
            string filetype = GetThePropValueFromXMLDOCSETType(setXMLDOC, fileformat).ToUpper();
            string extension = filetype;

            switch (filetype)
            {
                case "TIFF":
                    extension = "TIF";
                    break;
                case "JPEG":
                    extension = "JPG";
                    break;
                case "PDF":
                case "ENCRYPT_PDF":
                case "COMPACT_PDF":
                case "COMPACT_PDF_ULTRA_FINE":
                case "ENCRYPT_COMPACT_PDF":
                case "ENCRYPT_COMPACT_PDF_ULTRA_FINE":
                    extension = "PDF";
                    break;
                case "XPS":
                    extension = "XPS";
                    break;
                default:
                    break;
            }

            return extension;
        }

        //To retrieve the property value in the XML_DOC_SET_TYPE
        //gets the given property value in the XML_DOC_SET_TYPE
        public static string GetThePropValueFromXMLDOCSETType(XML_DOC_SET_TYPE xmlDocSET, string name)
        {
            string str_ret = string.Empty;
            if (xmlDocSET != null && name.Length > 0)
            {
                foreach (COMPLEX_SET_TYPE complex in xmlDocSET.complex)
                {
                    if (complex != null && (complex.sysname != null && complex.sysname.Length > 0))
                    {
                        str_ret = GetThePropValueRecursively(complex, name);
                    }
                }
            }
            return str_ret;
        }

        //Gets the prop array recursively from the complex
        private static string GetThePropValueRecursively(COMPLEX_SET_TYPE complexSET, string name)
        {
            string str_ret = string.Empty;
            if (complexSET != null)
            {
                if (complexSET.property != null && complexSET.property.Length > 0)
                {
                    foreach (PROPERTY_SET_TYPE propType in complexSET.property)
                    {
                        if (propType.sysname != null && propType.sysname.Length > 0)
                        {
                            if (propType.sysname == name)
                            {
                                str_ret = propType.Value;
                                break;
                            }
                        }
                    }

                }
                if (str_ret.Length == 0)
                {
                    if (complexSET.complex != null && complexSET.complex.Length > 0)
                    {
                        foreach (COMPLEX_SET_TYPE complexcomplex in complexSET.complex)
                        {
                            if (complexcomplex != null && (complexcomplex.sysname != null && complexcomplex.sysname.Length > 0))
                            {
                                str_ret = GetThePropValueRecursively(complexcomplex, name);
                                if (str_ret.Length != 0)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return str_ret;
        }
        //sets the specified property value in XML_DOC_SET_TYPE
        public static bool SetThePropValueInXMLDOCSETType(ref XML_DOC_SET_TYPE xmlDocSET, string complexName, string name, string new_value)
        {
            bool bRet = false;
            if (xmlDocSET != null && name.Length > 0)
            {
                for (int k = 0; k < xmlDocSET.complex.Length; k++)
                {
                    if (xmlDocSET.complex[k] != null && (xmlDocSET.complex[k].sysname != null && xmlDocSET.complex[k].sysname.Length > 0))
                    {
                        if (xmlDocSET.complex[k].sysname == complexName)
                        {
                            complexName = null;
                        }
                        bRet = SetThePropValueRecursively(ref xmlDocSET.complex[k], complexName, name, new_value);
                    }
                }
            }
            return bRet;
        }
        //sets the specified property value in XML_DOC_SET_TYPE, called by the SetThePropValueInXMLDOCSETType
        private static bool SetThePropValueRecursively(ref COMPLEX_SET_TYPE complexSET, string complexName, string name, string new_value)
        {
            bool bRet = false;
            if (complexSET != null)
            {
                if (null == complexName)
                {
                    if (complexSET.property != null && complexSET.property.Length > 0)
                    {
                        for (int i = 0; i < complexSET.property.Length; i++)
                        {
                            if (complexSET.property[i].sysname != null && complexSET.property[i].sysname.Length > 0)
                            {
                                if (complexSET.property[i].sysname == name)
                                {
                                    if (complexSET.property[i].Value != new_value)
                                    {
                                        ////OsaBridge.Direct.Log("The existing value for " + name + " = " + complexSET.property[i].Value.ToString());
                                        complexSET.property[i].Value = new_value;
                                        ////OsaBridge.Direct.Log("The New value = " + new_value);
                                        bRet = true;
                                    }
                                    break;
                                }
                            }
                        }

                    }
                }
                if (!bRet)
                {
                    if (complexSET.complex != null && complexSET.complex.Length > 0)
                    {
                        for (int j = 0; j < complexSET.complex.Length; j++)
                        {
                            if (complexSET.complex[j] != null && (complexSET.complex[j].sysname != null && complexSET.complex[j].sysname.Length > 0))
                            {
                                if (complexSET.complex[j].sysname == complexName)
                                {
                                    complexName = null;
                                }
                                bRet = SetThePropValueRecursively(ref complexSET.complex[j], complexName, name, new_value);
                                if (bRet)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return bRet;
        }

    }
}