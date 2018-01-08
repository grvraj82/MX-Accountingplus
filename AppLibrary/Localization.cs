
using System.Text;
using System.Collections;
using System.Data.Common;
using System.Data;

namespace AppLibrary
{
    public static class Localization
    {
        public static Hashtable Resources(string subSystem, string cultureID, string labelResourceIDs, string clientMessagesResourceIDs, string serverMessageResourceIDs)
        {
            cultureID = MappedCultureID(cultureID);
            Hashtable localizedResources = new Hashtable();

            string sqlQuery = string.Format("exec GetLocalizedStrings '{0}','{1}','{2}','{3}', '{4}'", subSystem, cultureID, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);
            DataSet dsLocalizedResources = null;
            using (Database database = new Database())
            {
                DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                dsLocalizedResources = database.ExecuteDataSet(dbCommand);
            }

            if (dsLocalizedResources != null && dsLocalizedResources.Tables.Count == 3)
            {
                string spliter = ",";
                string[] arrLabelResourceIDs = labelResourceIDs.Split(spliter.ToCharArray());
                string[] arrClientMessagesResourceIDs = clientMessagesResourceIDs.Split(spliter.ToCharArray());
                string[] arrServerMessageResourceIDs = serverMessageResourceIDs.Split(spliter.ToCharArray());

                if (arrLabelResourceIDs.Length > 0)
                {
                    foreach (string resourceID in arrLabelResourceIDs)
                    {
                        DataRow[] resourceRow = dsLocalizedResources.Tables[0].Select("RESX_ID = '" + resourceID + "'");
                        string resourceValue = cultureID + ":" + resourceID;
                        if (resourceRow != null && resourceRow.Length == 1)
                        {
                            resourceValue = resourceRow[0]["RESX_VALUE"].ToString();
                            resourceValue = resourceValue.Replace("'", "&#039;"); // &#039; == '

                            if (string.IsNullOrEmpty(resourceValue))
                            {
                                resourceValue = cultureID + ":?????:" + resourceID;
                            }
                        }
                        localizedResources.Add("L_" + resourceID, resourceValue);
                    }
                }

                if (arrClientMessagesResourceIDs.Length > 0)
                {
                    foreach (string resourceID in arrClientMessagesResourceIDs)
                    {
                        DataRow[] resourceRow = dsLocalizedResources.Tables[1].Select("RESX_ID = '" + resourceID + "'");
                        string resourceValue = cultureID + ":" + resourceID;
                        if (resourceRow != null && resourceRow.Length == 1)
                        {
                            resourceValue = resourceRow[0]["RESX_VALUE"].ToString();
                            resourceValue = resourceValue.Replace("'", "&#039;");
                            if (string.IsNullOrEmpty(resourceValue))
                            {
                                resourceValue = cultureID + ":?????:" + resourceID;
                            }
                        }
                        localizedResources.Add("C_" + resourceID, resourceValue);
                    }
                }

                if (arrServerMessageResourceIDs.Length > 0)
                {
                    foreach (string resourceID in arrServerMessageResourceIDs)
                    {
                        DataRow[] resourceRow = dsLocalizedResources.Tables[2].Select("RESX_ID = '" + resourceID + "'");
                        string resourceValue = cultureID + ":" + resourceID;
                        if (resourceRow != null && resourceRow.Length == 1)
                        {
                            resourceValue = resourceRow[0]["RESX_VALUE"].ToString();
                            resourceValue = resourceValue.Replace("'", "&#039;");
                            if (string.IsNullOrEmpty(resourceValue))
                            {
                                resourceValue = cultureID + ":?????:" + resourceID;
                            }
                        }
                        localizedResources.Add("S_" + resourceID, resourceValue);
                    }
                }
            }
            return localizedResources;
        }

        public static string GetServerMessage(string subSystem, string cultureID, string resourceID)
        {
            cultureID = MappedCultureID(cultureID);
            string resourceValue = cultureID + ":" + resourceID;

            string sqlQuery = string.Format("exec GetLocalizedServerMessage '{0}','{1}','{2}'", subSystem, cultureID, resourceID);
            DataSet dsLocalizedResources = null;
            using (Database database = new Database())
            {
                DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                dsLocalizedResources = database.ExecuteDataSet(dbCommand);
            }

            if (dsLocalizedResources != null && dsLocalizedResources.Tables.Count == 1)
            {
                DataRow[] resourceRow = dsLocalizedResources.Tables[0].Select("RESX_ID = '" + resourceID + "'");

                if (resourceRow != null && resourceRow.Length == 1)
                {
                    resourceValue = resourceRow[0]["RESX_VALUE"].ToString().Replace("\n", "");
                    resourceValue = resourceValue.Replace("'", "&#039;");
                    if (string.IsNullOrEmpty(resourceValue))
                    {
                        resourceValue = cultureID + ":?????:" + resourceID;
                    }
                }
            }
            return resourceValue;
        }

        public static string GetLabelText(string subSystem, string cultureID, string resourceID)
        {
            cultureID = MappedCultureID(cultureID);
            string resourceValue = cultureID + ":" + resourceID;

            string sqlQuery = string.Format("exec GetLocalizedLabel '{0}','{1}','{2}'", subSystem, cultureID, resourceID);
            DataSet dsLocalizedResources = null;
            using (Database database = new Database())
            {
                DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                dsLocalizedResources = database.ExecuteDataSet(dbCommand);
            }

            if (dsLocalizedResources != null && dsLocalizedResources.Tables.Count == 1)
            {
                DataRow[] resourceRow = dsLocalizedResources.Tables[0].Select("RESX_ID = '" + resourceID + "'");

                if (resourceRow != null && resourceRow.Length == 1)
                {
                    resourceValue = resourceRow[0]["RESX_VALUE"].ToString();
                    resourceValue = resourceValue.Replace("'", "&#039;");
                    if (string.IsNullOrEmpty(resourceValue))
                    {
                        resourceValue = cultureID + ":?????:" + resourceID;
                    }
                }
            }
            return resourceValue;
        }

        public static string BuildClientMessageVariables(string cultureID, Hashtable resourceIDs)
        {
            cultureID = MappedCultureID(cultureID);
            string returnClientmessagevariables = string.Empty;
            StringBuilder messageVaribales = new StringBuilder("\t<script language=\"javascript\">\n");

            foreach (DictionaryEntry scirptVariable in resourceIDs)
            {
                if (scirptVariable.Key.ToString().IndexOf("C_") > -1)
                {
                    string loalizedDataKey = scirptVariable.Key.ToString();
                    string loalizedDataValue = scirptVariable.Value.ToString();
                    //loalizedDataValue = loalizedDataValue.Replace("'", "");
                    if (!string.IsNullOrEmpty(loalizedDataKey))
                    {
                        if (loalizedDataValue != null)
                        {
                            messageVaribales.Append("\t\tvar ");
                            messageVaribales.Append(loalizedDataKey.Trim().Replace(" ", "_"));
                            messageVaribales.Append(" = ");
                            messageVaribales.Append("\"");
                            string loalizedData = loalizedDataValue.ToString().Trim();
                            loalizedData = loalizedData.Replace("\n", "\\n");
                            messageVaribales.Append(loalizedData);
                            messageVaribales.Append("\";\n");
                        }
                        else
                        {
                            messageVaribales.Append("\t\tvar ");
                            messageVaribales.Append(loalizedDataKey.Trim().Replace(" ", "_"));
                            messageVaribales.Append(" = ");
                            messageVaribales.Append("\"" + cultureID + ":" + loalizedDataKey.Trim());
                            messageVaribales.Append("\";\n");
                        }
                    }
                }
            }
            messageVaribales.Append("\t</script>\n");
            returnClientmessagevariables = messageVaribales.ToString();

            return returnClientmessagevariables;
        }

        private static string MappedCultureID(string cultureID)
        {
            string mappedCultureID = string.Empty;
            switch (cultureID)
            {
                case "en":
                case "en-US":
                case "en-GB":
                    mappedCultureID = "en-US";
                    break;
                case "de":
                case "de-DE":
                    mappedCultureID = "de-DE";
                    break;
                case "fr":
                case "fr-FR":
                    mappedCultureID = "fr-FR";
                    break;
                case "es":
                case "es-ES":
                    mappedCultureID = "es-ES";
                    break;
                case "it":
                case "it-IT":
                    mappedCultureID = "it-IT";
                    break;
                case "pt":
                case "pt-PT":
                    mappedCultureID = "pt-PT";
                    break;
                case "zh":
                case "zh-CHT":
                case "zh-CHS":
                case "zh-CN":
                case "zh-HK":
                    mappedCultureID = "zh-HK";
                    break;
                case "nl":
                case "nl-NL":
                    mappedCultureID = "nl-NL";
                    break;
                case "sv":
                case "sv-SE":
                    mappedCultureID = "sv-SE";
                    break;
                case "nb":
                case "no":
                case "nb-NO":
                    mappedCultureID = "nb-NO";
                    break;
                case "fi":
                case "fi-FI":
                    mappedCultureID = "fi-FI";
                    break;
                case "da":
                case "da-DK":
                    mappedCultureID = "da-DK";
                    break;
                case "hu":
                case "hu-HU":
                    mappedCultureID = "hu-HU";
                    break;
                case "cs":
                case "cs-CZ":
                    mappedCultureID = "cs-CZ";
                    break;
                default:
                    mappedCultureID = cultureID;
                    break;
            }
            return mappedCultureID;
        }
    }
}
