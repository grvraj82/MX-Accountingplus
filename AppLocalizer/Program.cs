using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Globalization;
using System.Collections;
using System.Configuration;
using DatabaseBridge;
using System.Data.SqlClient;
using System.Data.Common;
using System.IO;
using ApplicationAuditor;

namespace AppLocalizer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Supported Languages
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Building Localized Strings");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            Hashtable hashTableSelectedlanguages = new Hashtable();
            hashTableSelectedlanguages.Add("en-US", "English");
            hashTableSelectedlanguages.Add("ar-KW", "Arabic");
            //hashTableSelectedlanguages.Add("de-DE", "German");
            //hashTableSelectedlanguages.Add("fr-FR", "French");
            //hashTableSelectedlanguages.Add("es-ES", "Spanish");
            //hashTableSelectedlanguages.Add("it-IT", "Italian");
            //hashTableSelectedlanguages.Add("pt-PT", "Portuguese");
            //hashTableSelectedlanguages.Add("nl-NL", "Dutch");
            //hashTableSelectedlanguages.Add("sv-SE", "Swedish");
            //hashTableSelectedlanguages.Add("da-DK", "Danish");
            //hashTableSelectedlanguages.Add("nb-NO", "Norwegian");
            //hashTableSelectedlanguages.Add("fi-FI", "Finland");
            //hashTableSelectedlanguages.Add("cs-CZ", "Czech");
            //hashTableSelectedlanguages.Add("hu-HU", "Hungarian");
            //hashTableSelectedlanguages.Add("zh-HK", "Chinese");


            int foundLanguages = 0;

            string resourceFile = args[0].ToString();
            string resourceSheet = args[1].ToString();
            string destinationTable = args[2].ToString();
            string exportToDatabase = args[3].ToString(); // EXPORTTODB Export To Database
            string generateSQLstatements =  args[4].ToString(); // GENERATESQL Generate SQL Statements
            string generateSQLstatements_APP_LANGUAGES = args[5].ToString(); // "GENERATESQLFORMASTERTABLE";

            string sqlScriptFile = args[6].ToString(); // "SQL File to Appended/Created";
            string version = args[7].ToString(); // "string version";

            if (System.IO.File.Exists(resourceFile))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Importing :" + resourceFile);
                Console.ForegroundColor = ConsoleColor.White;

                ArrayList languages = new ArrayList();
                string excel = resourceFile; //ConfigurationSettings.AppSettings["ResourceFileLocation"];
                OleDbConnection connnection = new OleDbConnection();
                OleDbCommand command = new OleDbCommand();
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
                DataTable dataTable = new DataTable();
                string query = null;
                string connectionString = "";
                string strFileType = System.IO.Path.GetExtension(excel).ToString().ToLower();

                //Check file type
                if (strFileType == ".xls" || strFileType == ".xlsx")
                {
                    //
                }
                else
                {
                    Console.WriteLine("Invalid File");
                }

                //Connection String to Excel Workbook
                if (strFileType.Trim() == ".xls")
                {
                    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + excel + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                }
                else if (strFileType.Trim() == ".xlsx")
                {
                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excel + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                }

                if (!string.IsNullOrEmpty(version))
                {
                    query = "SELECT * FROM [" + resourceSheet + "$] where Version = '" + version + "'";
                }
                else
                {
                    query = "SELECT * FROM [" + resourceSheet + "$]";
                }

                connnection = new OleDbConnection(connectionString);
                if (connnection.State == ConnectionState.Closed) connnection.Open();
                command = new OleDbCommand(query, connnection);
                dataAdapter = new OleDbDataAdapter(command);
                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                connnection.Close();
                connnection.Dispose();
                languages.Clear();
                Hashtable hashtablecultures = new Hashtable();
                int hashTableCount = 0;
                hashtablecultures.Add(-1, "truncate table APP_LANGUAGES");


                //string sqlScriptFile = ConfigurationSettings.AppSettings["SQLScriptFile"];
                if (generateSQLstatements == "GENERATESQL" && generateSQLstatements_APP_LANGUAGES == "GENERATESQLFORMASTERTABLE")
                {
                    using (StreamWriter swScriptFile = new StreamWriter(sqlScriptFile, true, Encoding.Unicode))
                    {
                        swScriptFile.WriteLine("-- Localization Script --");
                        //swScriptFile.WriteLine("truncate table APP_LANGUAGES");
                    }
                }

                StringBuilder sbAppLangauges = new StringBuilder();
                string appLangaugesScript = string.Empty;

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    string columnName = Convert.ToString(dataTable.Columns[i].ColumnName, CultureInfo.CurrentCulture); //dataTable.Tables[0].Columns[i].ColumnName.ToString();
                    CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
                    // LINQ 
                    var selectCulture = from culture in cultures where culture.Name == columnName select culture;

                    if (selectCulture.Count() == 1)
                    {
                        languages.Add(columnName);
                        string uiDirection = "LTR";
                        if (columnName.ToLower() == "ar-sa")
                        {
                            uiDirection = "RTL";
                        }
                        string englishName = CultureInfo.CreateSpecificCulture(columnName).EnglishName;
                        string neutralCulture = CultureInfo.CreateSpecificCulture(columnName).TwoLetterISOLanguageName;
                        bool recActive = false;
                        if (hashTableSelectedlanguages.ContainsKey(columnName))
                        {
                            foundLanguages++;
                            recActive = true;


                            appLangaugesScript = "insert into APP_LANGUAGES(APP_CULTURE,APP_NEUTRAL_CULTURE,APP_LANGUAGE,APP_CULTURE_DIR,REC_ACTIVE) values(N'" + columnName + "',N'" + neutralCulture + "', N'" + englishName.Replace("'", "''") + "', '" + uiDirection + "','" + recActive + "')";

                            hashtablecultures.Add(hashTableCount, appLangaugesScript);
                            hashTableCount++;

                            if (generateSQLstatements == "GENERATESQL")
                            {
                                sbAppLangauges.AppendLine(appLangaugesScript);
                            }
                        }
                    }
                }

                if (generateSQLstatements == "GENERATESQL" && generateSQLstatements_APP_LANGUAGES == "GENERATESQLFORMASTERTABLE")
                {
                    using (StreamWriter swScriptFile = new StreamWriter(sqlScriptFile, true, Encoding.Unicode))
                    {
                        swScriptFile.WriteLine("-- APP_LANGUAGES");
                        swScriptFile.WriteLine(sbAppLangauges.ToString());
                        swScriptFile.WriteLine();
                        //swScriptFile.WriteLine("GO");
                    }

                    
                }

                int language;
                int resource;
                DataTable dataTableNew = null;
                if (exportToDatabase == "EXPORTTODB")
                {
                    dataTableNew = new DataTable();
                    dataTableNew.Columns.Add("RESX_CULTURE_ID");
                    dataTableNew.Columns.Add("RESX_ID");
                    dataTableNew.Columns.Add("RESX_VALUE");
                    dataTableNew.Columns.Add("REC_CDATE");
                    //dataTableNew.Columns.Add("RESX_LOCATION");
                }

                if (generateSQLstatements == "GENERATESQL")
                {
                    using (StreamWriter swScriptFile = new StreamWriter(sqlScriptFile, true, Encoding.Unicode))
                    {
                        swScriptFile.WriteLine("-- Localized Strings ");
                        swScriptFile.WriteLine();
                        string sqlScript = string.Empty;


                        //swScriptFile.WriteLine("truncate table " + destinationTable);
                        //swScriptFile.WriteLine();
                        //swScriptFile.WriteLine("GO");
                        //swScriptFile.WriteLine();

                        for (language = 0; language < languages.Count; language++)
                        {
                            if (hashTableSelectedlanguages.ContainsKey(languages[language]))
                            {
                                swScriptFile.WriteLine("-- Language = " + languages[language]);
                                //swScriptFile.WriteLine("GO");

                                for (resource = 0; resource < dataTable.Rows.Count; resource++)
                                {
                                    StringBuilder sbReources = new StringBuilder();
                                    sbReources.Append("insert into " + destinationTable + "(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( ");
                                    sbReources.Append("N''");
                                    sbReources.Append(", N'" + languages[language] + "'");
                                    sbReources.Append(", N'" + Convert.ToString(dataTable.Rows[resource]["Resource ID"], CultureInfo.CurrentCulture) + "'");
                                    sbReources.Append(", N'" + GetFormatedStringForDatabase(Convert.ToString(dataTable.Rows[resource][languages[language].ToString()], CultureInfo.CurrentCulture)) + "'");
                                    sbReources.Append(", getdate(), getdate(), 'Build', 'Build')");

                                    swScriptFile.WriteLine(sbReources.ToString());
                                    //swScriptFile.WriteLine();
                                }

                                //swScriptFile.WriteLine("GO");

                                swScriptFile.WriteLine("------- end of language " + languages[language] + "------------------");
                                //swScriptFile.WriteLine();
                                //swScriptFile.WriteLine();
                            }
                        }
                    }
                }
                if (exportToDatabase == "EXPORTTODB")
                {
                    string sqlConnectionString = ConfigurationSettings.AppSettings["DBConnection"]; ;
                    // Update APP_LANGUAGES Table
                    using (Database dbLanguages = new Database())
                    {
                        string insertStatus = dbLanguages.ExecuteNonQuery(hashtablecultures);
                    }

                    for (language = 0; language < languages.Count; language++)
                    {
                        if (hashTableSelectedlanguages.ContainsKey(languages[language]))
                        {
                            for (resource = 0; resource < dataTable.Rows.Count; resource++)
                            {
                                if (exportToDatabase == "EXPORTTODB")
                                {
                                    dataTableNew.Rows.Add(languages[language], Convert.ToString(dataTable.Rows[resource]["Resource ID"], CultureInfo.CurrentCulture), Convert.ToString(dataTable.Rows[resource][languages[language].ToString()], CultureInfo.CurrentCulture), Convert.ToString(DateTime.Now, CultureInfo.CurrentCulture));
                                }

                            }
                        }
                    }

                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConnectionString))
                    {
                        sqlBulkCopy.DestinationTableName = destinationTable;
                        sqlBulkCopy.ColumnMappings.Add("RESX_CULTURE_ID", "RESX_CULTURE_ID");
                        sqlBulkCopy.ColumnMappings.Add("RESX_ID", "RESX_ID");
                        sqlBulkCopy.ColumnMappings.Add("RESX_VALUE", "RESX_VALUE");
                        sqlBulkCopy.ColumnMappings.Add("REC_CDATE", "REC_CDATE");
                        //sqlBulkCopy.ColumnMappings.Add("RESX_LOCATION", "RESX_LOCATION");

                        if (dataTableNew.Rows.Count > 0)
                        {
                            //using (Database dbLocalizer = new Database())
                            //{
                            //    string sqlQuery = "truncate table " + destinationTable + "";
                            //    DbCommand dbCommandLocalizer = dbLocalizer.GetSqlStringCommand(sqlQuery);
                            //    string returnValue = dbLocalizer.ExecuteNonQuery(dbCommandLocalizer);
                            //}
                            sqlBulkCopy.WriteToServer(dataTableNew);
                            int result = sqlBulkCopy.NotifyAfter;
                            if (result == 0)
                            {
                                Console.WriteLine("Resources updated succesfully.");
                                //Console.ReadLine();
                            }
                            else
                            {
                                Console.WriteLine("Failed to update Resources.");
                                //Console.ReadLine();
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("File cannot be found in specified directory.");
                Console.ReadLine();
            }
        }

        private static string GetFormatedStringForDatabase(string orginalString)
        {
            orginalString = orginalString.Replace("'", "''");
            return orginalString;
        }
    }
}
