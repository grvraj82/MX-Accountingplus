using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace JobDispatcher
{
    #region CsvReader Class
    /// <summary>
    /// Upload DB Users
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///     <item>
    ///        <term>CsvReader</term>
    ///            <description>Read CSV File Data</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintRoverWeb.Administration.CsvReader.png" />
    /// </remarks>
    /// <remarks>
    public class CsvReader : IDisposable
    {
        #region Declaration
        //

        private StreamReader objReader;

        //add name space System.IO.Stream
        #endregion

        #region Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="CsvReader"/> class.
        /// </summary>
        /// <param name="filestream">The filestream.</param>
        public CsvReader(Stream fileStream) : this(fileStream, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvReader"/> class.
        /// </summary>
        /// <param name="filestream">The filestream.</param>
        /// <param name="enc">The enc.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.CsvReader.CsvReader.jpg"/>
        /// </remarks>
        public CsvReader(Stream fileStream, Encoding enc)
        {
            //check the Pass Stream whether it is readable or not
            if (!fileStream.CanRead)
            {
                return;
            }
            objReader = (enc != null) ? new StreamReader(fileStream, enc) : new StreamReader(fileStream);
        }

        //parse the Line

        /// <summary>
        /// Gets the CSV line.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.CsvReader.GetCsvLine.jpg"/>
        /// </remarks>
        public string[] GetCsvLine()
        {
            string data = objReader.ReadLine();
            if (data == null) return null;
            if (data.Split(",".ToCharArray()).Length < 2) return null;

            if (data.Length == 0) return new string[0];
            //System.Collection.Generic
            ArrayList result = new ArrayList();
            //parsing CSV Data
            ParseCSVData(result, data);
            return (string[])result.ToArray(typeof(string));
        }

        /// <summary>
        /// Parses the CSV data.
        /// </summary>
        /// <param name="result">Result.</param>
        /// <param name="data">Data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.CsvReader.ParseCsvData.jpg"/>
        /// </remarks>
        private void ParseCSVData(ArrayList result, string data)
        {
            int position = -1;
            while (position < data.Length)
                result.Add(ParseCSVField(ref data, ref position));
        }

        /// <summary>
        /// Parses the CSV field.
        /// </summary>
        /// <param name="data">Data.</param>
        /// <param name="StartSeperatorPos">Start seperator pos.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.CsvReader.ParseCsvField.jpg"/>
        /// </remarks>
        private string ParseCSVField(ref string data, ref int StartSeperatorPos)
        {
            if (StartSeperatorPos == data.Length - 1)
            {
                StartSeperatorPos++;
                return "";
            }
            int fromPos = StartSeperatorPos + 1;
            if (data[fromPos] == '"')
            {
                int nextSingleQuote = GetSingleQuote(data, fromPos + 1);
                int lines = 1;
                while (nextSingleQuote == -1)
                {
                    data = data + "\n" + objReader.ReadLine();
                    nextSingleQuote = GetSingleQuote(data, fromPos + 1);
                    lines++;
                    //Future Use
                    //if (lines > 20)
                    //throw new Exception("lines overflow: " + data);
                }
                StartSeperatorPos = nextSingleQuote + 1;
                string tempString = data.Substring(fromPos + 1, nextSingleQuote - fromPos - 1);
                tempString = tempString.Replace("'", "''");
                return tempString.Replace("\"\"", "\"");
            }
            int nextComma = data.IndexOf(',', fromPos);
            if (nextComma == -1)
            {
                StartSeperatorPos = data.Length;
                return data.Substring(fromPos);
            }
            else
            {
                StartSeperatorPos = nextComma;
                return data.Substring(fromPos, nextComma - fromPos);
            }
        }

        /// <summary>
        /// Gets the single quote.
        /// </summary>
        /// <param name="data">Data.</param>
        /// <param name="SFrom">S from.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.CsvReader.GetSingleQuote.jpg"/>
        /// </remarks>
        private static int GetSingleQuote(string data, int SFrom)
        {
            int i = SFrom - 1;
            while (++i < data.Length)
                if (data[i] == '"')
                {
                    if (i < data.Length - 1 && data[i + 1] == '"')
                    {
                        i++;
                        continue;
                    }
                    else
                        return i;
                }
            return -1;
        }
        #endregion

        #region IDisposable Members

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        private void Dispose(bool disposing)
        {
            // free managed resources
            if (disposing)
            {

            }

        }

        #endregion
    }
    #endregion
}
