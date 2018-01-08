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
using System.Collections.Generic;
using OsaDirectEAManager.Osa.MfpWebService;

namespace Osa.Util
{
	public abstract class AccountantBase
	{
		//-------------------------------------------------------------
		// framework methods

		public abstract CREDENTIALS_BASE_TYPE ValidateCredential(CREDENTIALS_TYPE userinfo, string password);
        public abstract CREDENTIALS_BASE_TYPE ValidateCredential(CREDENTIALS_TYPE userinfo);
        public abstract CREDENTIALS_BASE_TYPE ValidateCredential(CREDENTIALS_TYPE userinfo, string cardID, string userGroup);

		public virtual ACL_DOC_TYPE BuildXmlDocAcl(string accid, ACL_DOC_TYPE xmlacl)
		{
			if (xmlacl.mfpfeature == null) // AR-Series hasn't ACL
			{
				return xmlacl;
			}

			xmlacl.userinfo.accountid = accid; // don't forget

			foreach (ACL_MAIN_FEATURE_TYPE mf in xmlacl.mfpfeature)
			{
				string name = mf.sysname;

				// set ACL to this feature.
				mf.allow = ObtainAcl(accid, name, "", "", "", "");

				if (mf.settingconstraints != null)
				{
					foreach (ACL_PROPERTY_TYPE prop in mf.settingconstraints)
					{
						if (prop.setting != null)
						{
							ACL_SETTING_TYPE[] settings = prop.setting;
							foreach (ACL_SETTING_TYPE setting in settings)
							{
								// Set ACL to this feature property.
								setting.Value = ObtainAcl(accid, name, prop.sysname, setting.sysname, "", "");
							}
						}
					}
				}

				if (mf.subfeature != null)
				{
					foreach (ACL_SUB_FEATURE_TYPE sf in mf.subfeature)
					{
						// Set ACL to this sub-feature.
						sf.allow = ObtainAcl(accid, name, "", "", sf.sysname, "");

						if (sf.details != null)
						{
							foreach (ACL_DETAIL_TYPE d in sf.details)
							{
								// Set ACL to this feature detail.
								d.Value = ObtainAcl(accid, name, "", "", sf.sysname, d.sysname);
							}
						}
					}
				}
			}

			return xmlacl;
		}

		protected abstract bool ObtainAcl(string accid, string featureName, 
			string propName, string propSetting, string subFeatureName, string detail);

		public virtual LIMITS_TYPE[] BuildXmlDocLimit(string accid, LIMITS_TYPE[] xmllimits)
		{
			foreach (LIMITS_TYPE lims in xmllimits)
			{
				foreach (PROPERTY_LIMIT_TYPE plim in lims.property)
				{
					foreach (LIMIT_TYPE lim in plim.limit)
					{
                       lim.Value = ObtainLimit(accid, lims.sysname, plim.sysname, lim.sysname).ToString();
					}
				}
			}

			return xmllimits;
		}

		protected abstract uint ObtainLimit(string accid, string limitsName, string propName, string limitName);
    
        public virtual void RecordClicks(EVENT_DATA_TYPE evt)
		{
            string accid = evt.userinfo.accountid;

			DETAILS_ON_JOB_COMPLETED_TYPE details = (DETAILS_ON_JOB_COMPLETED_TYPE)evt.details;
			JOB_RESULTS_BASE_TYPE jr = (JOB_RESULTS_BASE_TYPE)details.JobResults;

			RESOURCE_PAPER_TYPE[] paperout;
			if (jr is JOB_RESULTS_COPY_TYPE)
			{
				paperout = ((JOB_RESULTS_COPY_TYPE)jr).resources.paperout;
			}
			else if (jr is JOB_RESULTS_PRINT_TYPE)
			{
				paperout = ((JOB_RESULTS_PRINT_TYPE)jr).resources.paperout;
			}
			else if (jr is JOB_RESULTS_SCAN_TYPE)
			{
				paperout = ((JOB_RESULTS_SCAN_TYPE)jr).resources.paperout;
			}
			else if (jr is JOB_RESULTS_SCAN_TYPE)
			{
				paperout = ((JOB_RESULTS_DOCFILING_TYPE)jr).resources.paperout;
			}
			else return;

			RecordSheetCounts(accid, paperout, jr);
		}

		protected virtual void RecordSheetCounts(string accid, 
			RESOURCE_PAPER_TYPE[] pout, JOB_RESULTS_BASE_TYPE results)
		{
			foreach (RESOURCE_PAPER_TYPE pt in pout)
			{
				// See if this RESOURCE_PAPER_TYPE meets the criteria for counting its clicks.
				// In order for a RESOURCE_PAPER_TYPE to count, it must have a "color-mode"
				// property, and must NOT HAVE a "papersize" property.
				if (!IsForCountingClicks(pt)) break;

				uint sheetCount = Convert.ToUInt32(pt.sheetcount);

				foreach (PROPERTY_SET_TYPE property in pt.property)
				{
					RecordSheetCount(accid, sheetCount, property.sysname, property.Value, results);
				}
			}
		}

		protected abstract void RecordSheetCount(string accid, uint sheetCount, 
			string propName, string propValue, JOB_RESULTS_BASE_TYPE results);

		//-------------------------------------------------------------
		// utility methods

       	public static IEnumerable<string> GetAclMasterList()
		{
			yield return "COMMON,duplex-mode,SIMPLEX,,";
			yield return "COMMON,duplex-mode,DUPLEX,,";
			yield return "PRINT,color-mode,MONOCHROME,,";
			yield return "PRINT,color-mode,FULL-COLOR,,";
			yield return "PRINT,store-mode,QUICK-FILE,,";
			yield return "PRINT,store-mode,SHARING,,";
			yield return "PRINT,store-mode,CONFIDENTIAL,,";
			yield return "PRINT,,,FTP-PULL-PRINT,";
			yield return "PRINT,,,NOT-HOLD-JOB-PRINT,";
			yield return "PRINT,,,USB-DIRECT-PRINT,";
			yield return "COPY,color-mode,MONOCHROME,,";
			yield return "COPY,color-mode,SINGLE-COLOR,,";
			yield return "COPY,color-mode,DUAL-COLOR,,";
			yield return "COPY,color-mode,FULL-COLOR,,";
			yield return "COPY,store-mode,QUICK-FILE,,";
			yield return "COPY,store-mode,SHARING,,";
			yield return "COPY,store-mode,CONFIDENTIAL,,";
			yield return "COPY,,,SPECIAL-MODES,";
			yield return "IMAGE-SEND,color-mode,FULL-COLOR,,";
            yield return "IMAGE-SEND,color-mode,MONOCHROME,,";
			yield return "IMAGE-SEND,store-mode,QUICK-FILE,,";
			yield return "IMAGE-SEND,store-mode,SHARING,,";
			yield return "IMAGE-SEND,store-mode,CONFIDENTIAL,,";
			yield return "IMAGE-SEND,,,DIRECT-ADDRESS-ENTRY,EMAIL-SEND";
			yield return "IMAGE-SEND,,,DIRECT-ADDRESS-ENTRY,IFAX-SEND";
			yield return "IMAGE-SEND,,,DIRECT-ADDRESS-ENTRY,FAX-SEND";
			yield return "IMAGE-SEND,,,LOCAL-ADDRESS-BOOK,EMAIL-SEND";
			yield return "IMAGE-SEND,,,LOCAL-ADDRESS-BOOK,IFAX-SEND";
			yield return "IMAGE-SEND,,,LOCAL-ADDRESS-BOOK,FAX-SEND";
			yield return "IMAGE-SEND,,,GLOBAL-ADDRESS-BOOK,ADDRESS-BOOK-1";
			yield return "IMAGE-SEND,,,GLOBAL-ADDRESS-BOOK,ADDRESS-BOOK-2";
			yield return "IMAGE-SEND,,,GLOBAL-ADDRESS-BOOK,ADDRESS-BOOK-3";
			yield return "IMAGE-SEND,,,GLOBAL-ADDRESS-BOOK,ADDRESS-BOOK-4";
			yield return "IMAGE-SEND,,,GLOBAL-ADDRESS-BOOK,ADDRESS-BOOK-5";
			yield return "IMAGE-SEND,,,GLOBAL-ADDRESS-BOOK,ADDRESS-BOOK-6";
			yield return "IMAGE-SEND,,,GLOBAL-ADDRESS-BOOK,ADDRESS-BOOK-7";
			yield return "IMAGE-SEND,,,EMAIL-SEND,";
			yield return "IMAGE-SEND,,,FAX-SEND,";
			yield return "IMAGE-SEND,,,FAX2-SEND,";
			yield return "IMAGE-SEND,,,IFAX-SEND,";
			yield return "IMAGE-SEND,,,FTP-SEND,";
			yield return "IMAGE-SEND,,,NETWORK-FOLDER,";
			yield return "IMAGE-SEND,,,PC-FAX-SEND,";
			yield return "IMAGE-SEND,,,PC-IFAX-SEND,";
			yield return "IMAGE-SEND,,,REMOTE-PC-SCAN,";
			yield return "IMAGE-SEND,,,USB,";
			yield return "IMAGE-SEND,,,DESKTOP,";
			yield return "IMAGE-SEND,,,SPECIAL-MODES,";
			yield return "SCAN-TO-HDD,color-mode,MONOCHROME,,";
			yield return "SCAN-TO-HDD,color-mode,DUAL-COLOR,,";
			yield return "SCAN-TO-HDD,color-mode,FULL-COLOR,,";
			yield return "SCAN-TO-HDD,store-mode,SHARING,,";
			yield return "SCAN-TO-HDD,store-mode,CONFIDENTIAL,,";
			yield return "SCAN-TO-HDD,,,SPECIAL-MODES,";
			yield return "DOC-FILING-PRINT,color-mode,MONOCHROME,,";
			yield return "DOC-FILING-PRINT,color-mode,DUAL-COLOR,,";
			yield return "DOC-FILING-PRINT,color-mode,FULL-COLOR,,";
			yield return "DOC-FILING-PRINT,,,SPECIAL-MODES,";
			yield return "SHARP-OSA,,,STANDARD-APPLICATION,APPLICATION-1";
			yield return "SHARP-OSA,,,STANDARD-APPLICATION,APPLICATION-2";
			yield return "SHARP-OSA,,,STANDARD-APPLICATION,APPLICATION-3";
			yield return "SHARP-OSA,,,STANDARD-APPLICATION,APPLICATION-4";
			yield return "SHARP-OSA,,,STANDARD-APPLICATION,APPLICATION-5";
			yield return "SHARP-OSA,,,STANDARD-APPLICATION,APPLICATION-6";
			yield return "SHARP-OSA,,,STANDARD-APPLICATION,APPLICATION-7";
			yield return "SHARP-OSA,,,STANDARD-APPLICATION,APPLICATION-8";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,JOB-PROGRAM-STORE";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,TOTAL-COUNT";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,DISPLAY-CONTRAST";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,CLOCK";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,KEYBOARD-SELECT";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,LIST-PRINT-USER";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,BYPASS-TRAY-EXCLUDED";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,BYPASS-TRAY";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,ADDRESS-CONTROL";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,FAX-DATA-RECEIVE";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,PRINTER-DEFAULT-SETTINGS";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,PCL-SETTINGS";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,PS-SETTINGS";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,DOC-FILING-CONTROL";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,USB-DEVICE-CHECK";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,USER-CONTROL";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,ENERGY-SAVE";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,OPERATION-SETTINGS";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,DEVICE-SETTINGS";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,COPY-SETTINGS";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,NETWORK-SETTINGS";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,PRINTER-SETTINGS";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,IMAGESEND-OPERATION-SETTINGS";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,IMAGESEND-SCANNER-SETTINGS";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,IMAGESEND-IFAX-SETTINGS";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,IMAGESEND-FAX-SETTINGS";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,DOC-FILING-SETTINGS";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,LIST-PRINT-ADMIN";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,SECURITY-SETTINGS";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,PRODUCT-KEY";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,ESCP-SETTINGS";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,ENABLE-DISABLE-SETTINGS";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,FIELD-SUPPORT-SYSTEM";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,CHANGE-ADMIN-PASSWORD";
			yield return "SETTINGS,,,SYSTEM-SETTINGS,RETENTION-CALLING";
			yield return "SETTINGS,,,WEB-SETTINGS,DISPLAY-DEVICE";
			yield return "SETTINGS,,,WEB-SETTINGS,POWER-RESET";
			yield return "SETTINGS,,,WEB-SETTINGS,MACHINE-ID";
			yield return "SETTINGS,,,WEB-SETTINGS,APPLICATION-SETTINGS";
			yield return "SETTINGS,,,WEB-SETTINGS,REGISTER-PRE-SET-TEXT";
			yield return "SETTINGS,,,WEB-SETTINGS,EMAIL-ALERT";
			yield return "SETTINGS,,,WEB-SETTINGS,JOB-LOG";
			yield return "SETTINGS,,,WEB-SETTINGS,PORT-SETTINGS";
			yield return "SETTINGS,,,WEB-SETTINGS,STORAGE-BACKUP";
			yield return "SETTINGS,,,WEB-SETTINGS,CUSTOM-LINKS";
			yield return "SETTINGS,,,WEB-SETTINGS,OPERATION-MANUAL-DOWNLOAD";
			yield return "SETTINGS,,,WEB-SETTINGS,NETWORK-SETTINGS";
		}

		public static IEnumerable<string> GetLimitMasterList()
		{
			yield return "COPY,color-mode,MONOCHROME";
			yield return "COPY,color-mode,SINGLE-COLOR";
			yield return "COPY,color-mode,DUAL-COLOR";
			yield return "COPY,color-mode,FULL-COLOR";
			yield return "PRINT,color-mode,MONOCHROME";
			yield return "PRINT,color-mode,SINGLE-COLOR";
			yield return "PRINT,color-mode,DUAL-COLOR";
			yield return "PRINT,color-mode,TRIPLE-COLOR";
			yield return "PRINT,color-mode,FULL-COLOR";
			yield return "LIST-PRINT,color-mode,MONOCHROME";
			yield return "LIST-PRINT,color-mode,FULL-COLOR";
			yield return "FAX-SEND,color-mode,MONOCHROME";
			yield return "FAX2-SEND,color-mode,MONOCHROME";
			yield return "I-FAX-SEND,color-mode,MONOCHROME";
			yield return "SCANNER,color-mode,MONOCHROME";
			yield return "SCANNER,color-mode,FULL-COLOR";
			yield return "DOC-FILING-PRINT,color-mode,MONOCHROME";
			yield return "DOC-FILING-PRINT,color-mode,DUAL-COLOR";
			yield return "DOC-FILING-PRINT,color-mode,FULL-COLOR";
			yield return "SCAN-TO-HDD,color-mode,MONOCHROME";
			yield return "SCAN-TO-HDD,color-mode,DUAL-COLOR";
			yield return "SCAN-TO-HDD,color-mode,FULL-COLOR";
		}

		public static bool IsForCountingClicks(RESOURCE_PAPER_TYPE pt)
		{
			bool bHasPaperSize = false;
			bool bHasColorMode = false;
			bool bHasCopiesCount = false;

			//break if there is no valid sheetcount, since it is optional
			if (pt.sheetcount == null || pt.property == null)
			{
				return false;
			}

			if (pt.copiescount != null && pt.copiescount.Length > 0)
			{
				bHasCopiesCount = true;
			}

			foreach (PROPERTY_SET_TYPE property in pt.property)
			{
				if (property.sysname == "paper-size")
				{
					bHasPaperSize = true;
				}
				if (property.sysname == "color-mode")
				{
					bHasColorMode = true;
				}
			}

			if (bHasColorMode && bHasPaperSize && bHasCopiesCount)
			{
				return true;
			}

			if (bHasColorMode && !bHasPaperSize)
			{
				return true;
			}

			return false;
		}

		public static string MapJobModeToLimits(JOB_MODE_TYPE jobmode)
		{
			switch (jobmode.sysname)
			{
				//COPY
				case "JOB_MODE_COPIER":
					return "COPY";

				//PRINT
				case "JOB_MODE_PRINT":
				case "JOB_MODE_DIRECT_PRINT_EMAIL":
				case "JOB_MODE_DIRECT_PRINT_WEB":
				case "JOB_MODE_DIRECT_PRINT_FTP":
				case "JOB_MODE_DIRECT_PRINT_USB":
					return "PRINT";

				//SCAN
				case "JOB_MODE_SCAN_TO_DESKTOP":
				case "JOB_MODE_SCAN_TO_SMB":
				case "JOB_MODE_SCAN_TO_USB":
                case "JOB_MODE_SCAN_TO_HDD":
				case "JOB_MODE_FTP_SEND":
				case "JOB_MODE_E_MAIL_SEND":
				case "JOB_MODE_E_MAIL_FTP_SEND": //???
				case "JOB_MODE_META_E_MAIL":
				case "JOB_MODE_META_FTP":
				case "JOB_MODE_META_DESKTOP":
				case "JOB_MODE_META_SMB":
				case "JOB_MODE_TWAIN_PRE_SCAN":
				case "JOB_MODE_TWAIN":
				case "JOB_MODE_RELAY_E_MAIL":
					return "SCANNER";

				//DOC FILING
				case "JOB_MODE_REPRINT":
					return "DOC-FILING-PRINT";

				//LIST-PRINT
				case "JOB_MODE_LIST_PRINT":
					return "LIST-PRINT";

				//FAX-SEND
				case "JOB_MODE_FAX_SEND":
				case "JOB_MODE_FAX_SEND2":
				case "JOB_MODE_PC_FAX_SEND":
				case "JOB_MODE_PC_FAX_SEND2":
				case "JOB_MODE_POLLING_MODE":
				case "JOB_MODE_POLLING_MODE2":
				case "JOB_MODE_TRF_FAX":
				case "JOB_MODE_TRF_FAX2":
				case "JOB_MODE_RELAY_FAX":
				case "JOB_MODE_RELAY_FAX2":
				case "JOB_MODE_MULTI_POLLING":
				case "JOB_MODE_SCAN_TO_MEMBOX":
					return "FAX-SEND";

                //FAX-SEND
                case "JOB_MODE_FAX_PRINT":
                    return "FAX-PRINT";

				//IFAX-SEND
				case "JOB_MODE_INTERNET_FAX_SEND":
				case "JOB_MODE_PC_I_FAX_SEND":
				case "JOB_MODE_TRF_I_FAX":
					return "I-FAX-SEND";

				default:
					return null;
			}
		}

	}
}