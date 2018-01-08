#region Copyright SHARP Corporation 2008
//
//	Sharp OSA V3.0 Utility Classes
//
//	Copyright 2008, Sharp Corporation.  ALL RIGHTS RESERVED.
//
//	This software is protected under the Copyright Laws of the United States,
//	Title 17 USC, by the Berne Convention, and the copyright laws of other
//	countries.
//
//	THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDER ``AS IS'' AND ANY EXPRESS 
//	OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
//	OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//
#endregion

using System;
using System.Xml;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using OsaDirectManager.Osa.MfpWebService;

namespace Osa.Util
{
    public class MFPCoreWSEx : MFPCoreWS
	{
		public const string MFP_CORE_WS_URL = "/MfpWebServices/MFPCoreWS.asmx";
		public const string WSDL_BUILD_NUMBER = "1.0.0.23";

		public string wsdlGeneric;
		public string uiSessionId;

		//-------------------------------------------------------------
		// constructors

		public MFPCoreWSEx(string mfpUrl, string vkey, CREDENTIALS_TYPE cred, string uisid)
		{
			base.Url = mfpUrl + MFP_CORE_WS_URL;
			base.Security = new SECURITY_SOAPHEADER_TYPE();
			base.Security.licensekey = vkey;
			base.Security.Credentials = cred;

			wsdlGeneric = WSDL_BUILD_NUMBER;
			uiSessionId = (uisid == null) ? string.Empty : uisid;
		}

		//-------------------------------------------------------------
		// web service wrapper methods
		
		public XML_DOC_DSC_TYPE GetJobSettableElements(ARG_SETTABLE_TYPE arg)
		{
			return base.GetJobSettableElements(arg, ref wsdlGeneric);
		}

		public OSA_JOB_ID_TYPE CreateJob(E_MFP_JOB_TYPE jobType)
		{
			return base.CreateJob(jobType, uiSessionId, ref wsdlGeneric);
		}
		
		public void ShowScreen(SHOWSCREEN_TYPE scr)
		{

            try
            {
                base.ShowScreen(uiSessionId, scr, ref wsdlGeneric);
            }
            catch (Exception)
            {

            }
		}

		public void ShowScreen(String appPage)
		{
			SHOWSCREEN_TYPE scr = new SHOWSCREEN_TYPE();
			scr.Item = appPage;
			ShowScreen(scr);
		}

		public void ShowScreen(E_MFP_SHOWSCREEN_TYPE mfpPage)
		{
			SHOWSCREEN_TYPE scr = new SHOWSCREEN_TYPE();
			scr.Item = mfpPage;
			ShowScreen(scr);
		}

		public void Subscribe(OSA_JOB_ID_TYPE jid, E_EVENT_ID_TYPE eType, ACCESS_POINT_TYPE sinkURL, bool action)
		{
            try
            {
                base.Subscribe(jid, eType, sinkURL, action, ref wsdlGeneric);
            }
            catch (Exception)
            {
                throw;
            }
		}
		
		public void SetJobElements(OSA_JOB_ID_TYPE jid, XML_DOC_SET_TYPE set)
		{
			base.SetJobElements(jid, set, ref wsdlGeneric);
		}

		public void ExecuteJob(OSA_JOB_ID_TYPE jid)
		{
			base.ExecuteJob(jid, ref wsdlGeneric);
		}

		public JOB_STATUS_TYPE GetJobStatus(OSA_JOB_ID_TYPE jid)
		{
			return base.GetJobStatus(jid, ref wsdlGeneric);
		}

		public void CancelJob(OSA_JOB_ID_TYPE jid)
		{
			base.CancelJob(jid, ref wsdlGeneric);
		}

		public void CloseJob(OSA_JOB_ID_TYPE jid)
		{
			base.CloseJob(jid, ref wsdlGeneric);
		}

        //public void EnableDevice(ACL_DOC_TYPE acl, LIMITS_TYPE[] limits)
        //{
        //    base.EnableDevice(acl, limits, ref wsdlGeneric);
        //}

        public void EnableDevice(ACL_DOC_TYPE acl, LIMITS_DOC_TYPE limits)
        {
            base.EnableDevice(acl, limits, ref wsdlGeneric);
        }
		//---------------------------------------------------------
		// Generic Utilities

		public static string Fault_GetOsaSubCode(SoapException ex)
		{
			XmlNamespaceManager nsm = new XmlNamespaceManager(new NameTable());
			nsm.AddNamespace("osa", "urn:schemas-sc-jp:mfp:osa-1-1");
			return ex.Detail.SelectSingleNode(".//osa:sub-code", nsm).InnerText;
		}

		public static ERROR_TYPE[] Status_GetErrors(JOB_STATUS_TYPE stat)
		{
			if (stat.details is JOB_STATUS_DETAILS_STARTED_TYPE)
			{
				return ((JOB_STATUS_DETAILS_STARTED_TYPE)stat.details).errorList;
			}
			else if (stat.details is JOB_STATUS_DETAILS_FINISHED_TYPE)
			{
				return ((JOB_STATUS_DETAILS_FINISHED_TYPE)stat.details).errorList;
			}
			else if (stat.details is JOB_STATUS_DETAILS_SUSPENDED_TYPE)
			{
				return ((JOB_STATUS_DETAILS_SUSPENDED_TYPE)stat.details).errorList;
			}
			else if (stat.details is JOB_STATUS_DETAILS_ERROR_TYPE)
			{
				return ((JOB_STATUS_DETAILS_ERROR_TYPE)stat.details).errorList;
			}
			else return null;
		}

		//---------------------------------------------------------
		// Job Setting

		public static void SET_SetPropertyValue(ref XML_DOC_SET_TYPE doc, string path, string val)
		{
			LinkedList<string> nameList = new LinkedList<string>();
			foreach (string name in path.Split('/'))
			{
				nameList.AddLast(name);
			}

			SET_SetPropertyValue(ref doc.complex[0], nameList, val);
		}

		private static void SET_SetPropertyValue(ref COMPLEX_SET_TYPE complex, LinkedList<string> nameList, string val)
		{
			string name = nameList.First.Value;
			nameList.RemoveFirst();

			PROPERTY_SET_TYPE[] props = complex.property;
			if (props != null)
			{
				for (int i = 0; i < props.Length; i++)
				{
					if (props[i].sysname == name)
					{
						SET_SetPropertyValue(ref props[i], val);
					}
				}
			}

			COMPLEX_SET_TYPE[] comps = complex.complex;
			if (comps != null)
			{
				for (int i = 0; i < comps.Length; i++)
				{
					if (comps[i].sysname == name)
					{
						SET_SetPropertyValue(ref comps[i], nameList, val);
					}
				}
			}
		}

		private static void SET_SetPropertyValue(ref PROPERTY_SET_TYPE property, string val)
		{
			property.Value = val;
		}

		//---------------------------------------------------------
		// Job Setting Test

		public static bool DSC_HasProperty(XML_DOC_DSC_TYPE xmlDocDsc, string path)
		{
			return DSC_IsAllowableValue(xmlDocDsc, path, null);
		}

		public static bool DSC_IsAllowableValue(XML_DOC_DSC_TYPE xmlDocDsc, string path, string val)
		{
			LinkedList<string> nameList = new LinkedList<string>();
			foreach (string name in path.Split('/'))
			{
				nameList.AddLast(name);
			}

			return DSC_IsAllowableValue(xmlDocDsc.complex[0], nameList, val);
		}

		private static bool DSC_IsAllowableValue(COMPLEX_DSC_TYPE complex, LinkedList<string> nameList, string val)
		{
			string name = nameList.First.Value;
			nameList.RemoveFirst();

			PROPERTY_DSC_TYPE[] props = complex.property;
			if (props != null)
			{
				foreach (PROPERTY_DSC_TYPE prop in props)
				{
					if (prop == null) continue;
					if (prop.sysname == name)
					{
						return DSC_IsAllowableValue(prop, val);
					}
				}
			}

			COMPLEX_DSC_TYPE[] comps = complex.complex;
			if (comps != null)
			{
				foreach (COMPLEX_DSC_TYPE comp in comps)
				{
					if (comp == null) continue;
					if (comp.sysname == name)
					{
						return DSC_IsAllowableValue(comp, nameList, val);
					}
				}
			}

			return false;
		}

		private static bool DSC_IsAllowableValue(PROPERTY_DSC_TYPE property, string value)
		{
			// if check property existence only
			if (value == null) return true;

			// four cases; bool, integer, string, or list are examined here.
			ELEMENT_VALUE_TYPE_TYPE valueType = property.isType;

			if (valueType == ELEMENT_VALUE_TYPE_TYPE.@bool)
			{
				// for bool, allowable value is "true" and "false"
				return (value == "true" || value == "false");
			}
			else if (valueType == ELEMENT_VALUE_TYPE_TYPE.integer)
			{
				// for integer, allowable value is the value within
				// specified range
				int intValue = int.Parse(value);

				ELEMENT_CONSTRAINT_TYPE[] constTypes = property.appInfo;
				foreach (ELEMENT_CONSTRAINT_TYPE constType in constTypes)
				{
					if (constType.name == ELEMENT_CONSTRAINT_ATTR_TYPE.minValue &&
						intValue < int.Parse(constType.Value))
					{
						return false;
					}
					if (constType.name == ELEMENT_CONSTRAINT_ATTR_TYPE.maxValue &&
						intValue > int.Parse(constType.Value))
					{
						return false;
					}
				}
				return true;
			}
			else if (valueType == ELEMENT_VALUE_TYPE_TYPE.@string)
			{
				// for string, allowable value is the one within a range
				// of specified number of characters
				int length = value.Length;

				ELEMENT_CONSTRAINT_TYPE[] constTypes = property.appInfo;
				foreach (ELEMENT_CONSTRAINT_TYPE constType in constTypes)
				{
					if (constType.name == ELEMENT_CONSTRAINT_ATTR_TYPE.minLength &&
						length < int.Parse(constType.Value))
					{
						return false;
					}
					if (constType.name == ELEMENT_CONSTRAINT_ATTR_TYPE.maxLength &&
						length > int.Parse(constType.Value))
					{
						return false;
					}
				}
				return true;
			}
			else if (valueType == ELEMENT_VALUE_TYPE_TYPE.list)
			{
				// for list, allowable value is the one within an allowable value list
				string[] validValues = property.allowedValueList;
				foreach (string validValue in validValues)
				{
					if (value == validValue)
					{
						return true;
					}
				}
				return false;
			}
			else // NO SUPPORT
			{
				return true;
			}
		}

		//---------------------------------------------------------
		// Job Setting Enumeration

		public static List<string> DSC_EnumerateSettableValues(XML_DOC_DSC_TYPE xmlDocDsc, string path)
		{
			LinkedList<string> nameList = new LinkedList<string>();
			foreach (string name in path.Split('/'))
			{
				nameList.AddLast(name);
			}

			List<string> list = new List<string>();
			foreach (string e in DSC_EnumerateSettableValues(xmlDocDsc.complex[0], nameList))
			{
				list.Add(e);
			}

			return list;
		}

		private static string[] DSC_EnumerateSettableValues(COMPLEX_DSC_TYPE complex, LinkedList<string> nameList)
		{
			string name = nameList.First.Value;
			nameList.RemoveFirst();

			PROPERTY_DSC_TYPE[] props = complex.property;
			if (props != null)
			{
				foreach (PROPERTY_DSC_TYPE prop in props)
				{
					if (prop == null) continue;
					if (prop.sysname == name)
					{
						return DSC_EnumerateSettableValues(prop);
					}
				}
			}

			COMPLEX_DSC_TYPE[] comps = complex.complex;
			if (comps != null)
			{
				foreach (COMPLEX_DSC_TYPE comp in comps)
				{
					if (comp == null) continue;
					if (comp.sysname == name)
					{
						return DSC_EnumerateSettableValues(comp, nameList);
					}
				}
			} 
			
			// can't find the property
			return null;
		}

		private static string[] DSC_EnumerateSettableValues(PROPERTY_DSC_TYPE property)
		{
			// four cases; bool, integer, string, or list are examined here.
			ELEMENT_VALUE_TYPE_TYPE valueType = property.isType;

			// only list is enumerable
			if (valueType == ELEMENT_VALUE_TYPE_TYPE.list)
			{
				// return property list
				return property.allowedValueList;
			}

			return null;
		}

		public static XML_DOC_SET_TYPE DSC_ConvertToSettable(XML_DOC_DSC_TYPE xmlDocDsc)
		{
			XML_DOC_SET_TYPE xmlDocSet = new XML_DOC_SET_TYPE();
			xmlDocSet.property = null;

			COMPLEX_DSC_TYPE[] subComps = xmlDocDsc.complex;
			COMPLEX_SET_TYPE[] tmpComps = new COMPLEX_SET_TYPE[1];
			tmpComps[0] = DSC_ConvertToSettable(subComps[0]);

			xmlDocSet.complex = tmpComps;

			return xmlDocSet;
		}

		private static COMPLEX_SET_TYPE DSC_ConvertToSettable(COMPLEX_DSC_TYPE complexD)
		{
			COMPLEX_SET_TYPE complexS = new COMPLEX_SET_TYPE();
			complexS.sysname = complexD.sysname;

			// inner Property
			PROPERTY_DSC_TYPE[] subprops = complexD.property;
			if (subprops == null)
			{
				complexS.property = null;
			}
			else
			{
				PROPERTY_SET_TYPE[] tmpProps = new PROPERTY_SET_TYPE[subprops.Length];
				for (int i = 0; i < subprops.Length; i++)
				{
					tmpProps[i] = DSC_ConvertToSettable(subprops[i]);
				}
				complexS.property = tmpProps;
			}

			// inner Complex
			COMPLEX_DSC_TYPE[] subcomps = complexD.complex;
			if (subcomps == null)
			{
				complexS.complex = null;
			}
			else
			{
				COMPLEX_SET_TYPE[] tmpComps = new COMPLEX_SET_TYPE[subcomps.Length];
				for (int i = 0; i < subcomps.Length; i++)
				{
					tmpComps[i] = DSC_ConvertToSettable(subcomps[i]);
				}
				complexS.complex = tmpComps;
			}

			return complexS;
		}

		private static PROPERTY_SET_TYPE DSC_ConvertToSettable(PROPERTY_DSC_TYPE propertyD)
		{
			PROPERTY_SET_TYPE propertyS = new PROPERTY_SET_TYPE();
			propertyS.sysname = propertyD.sysname;
			propertyS.Value = propertyD.value;

			return propertyS;
		}
	}
}