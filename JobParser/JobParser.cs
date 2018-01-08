#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Sreedhar p, Prasad Gopathi, GR Varadharaj
  File Name: JobParser.cs
  Description: Job parser application to convert Simplex to Duplex and vice versa
  Date Created : November 2010
  */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  07-Dec-2010         Sudheendra Panganamala   
*/
#endregion

#region :Namespace:
using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using AppLibrary;
using System.Text;
#endregion

namespace JobParser
{
    /// <summary>
    /// Converts print file from Simplex Mode to Duplex Mode and vice versa
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// 	<img src="ClassDiagrams/CD_JobParser.JobParser.png"/>
    /// </remarks>
    public class JobParser
    {
        #region : Declaration :
        static int chunk = 1000000;
        #endregion

        /// <summary>
        /// Provides the duplex direction.
        /// </summary>
        /// <param name="printJobsLocation">The print jobs location.</param>
        /// <param name="driverType">Type of the driver.</param>
        /// <returns></returns>
        /// <remarks>
        /// Class Diagram:<br/>
        /// 	<img src="ClassDiagrams/SD_JobParser.JobParser.ProvideDuplexDirection.jpg"/>
        /// </remarks>
        public static string ProvideDuplexDirection(string printJobsLocation, string driverType)
        {
            string duplexDirection = string.Empty;
            try
            {
                //If driver type is PCL
                if (driverType == Constants.PCLDRIVER)
                {
                    duplexDirection = ProvidePCLDuplexDirection(printJobsLocation);
                }
                //if driver type is PCL5c or PCL5e
                else if (driverType == Constants.PCL5CDRIVER)
                {
                    duplexDirection = ProvidePCL5CDuplexDirection(printJobsLocation);
                }
                else
                {
                    duplexDirection = ProvidePostScriptDuplexDirection(printJobsLocation);
                }
            }
            catch (Exception ex)
            { }
            return duplexDirection;
        }

        #region :PCL5c/PCL5e Driver:

        /// <summary>
        /// Provides the PCL5c and PCL5e driver duplex direction.
        /// </summary>
        /// <param name="printJobsLocation">The print jobs location.</param>
        /// <returns></returns>
        /// <remarks>
        /// Class Diagram:<br/>
        /// 	<img src="ClassDiagrams/SD_JobParser.JobParser.ProvidePCL5CDuplexDirection.jpg"/>
        /// </remarks>
        private static string ProvidePCL5CDuplexDirection(string printJobsLocation)
        {
            string returnValue = string.Empty;
            try
            {
                // Change the print job location file name from .prn to .config
                printJobsLocation = printJobsLocation.Replace(".prn", ".config");
                using (StreamReader streamReader = new StreamReader(printJobsLocation))
                {
                    //Read the whole text file to the end
                    string allRead = streamReader.ReadToEnd();
                    //Closes the text file after it is fully read.
                    streamReader.Close();
                    // Search for the "DUPLEX=ON" string in the all read string
                    if (Regex.IsMatch(allRead, Constants.PCL5DUPLEXDUPLEXON))
                    {
                        // If found then check for the "BINDING=LONGEDGE" string.
                        // Which is equivalent to Booklet, If found return string as BOOK.
                        if (Regex.IsMatch(allRead, Constants.PCL5BOOKLETSTRING))
                        {
                            returnValue = Constants.BOOKLET;
                        }
                        // else check for the "BINDING=SHORTEDGE" string.
                        // Which is equivalent to Tablet, If found return string as TABLET.
                        else if (Regex.IsMatch(allRead, Constants.PCL5TABLETSTRING))
                        {
                            returnValue = Constants.TABLET;
                        }
                        // else nothing is found, then return string as none.
                        else
                        {
                            returnValue = Constants.NONE;
                        }
                    }
                    // If "DUPLEX=ON" string is not found, then return it as SIMPLEX.
                    else
                    {
                        returnValue = Constants.SIMPLEX;
                    }
                }
            }
            catch (Exception ex)
            { }
            return returnValue;
        }

        #endregion

        #region :PCL6 Driver:

        /// <summary>
        /// Provides the PCL6 driver duplex direction.
        /// </summary>
        /// <param name="printJobsLocation">The print jobs location.</param>
        /// <returns></returns>
        /// <remarks>
        /// Class Diagram:<br/>
        /// 	<img src="ClassDiagrams/SD_JobParser.JobParser.ProvidePCLDuplexDirection.jpg"/>
        /// </remarks>
        private static string ProvidePCLDuplexDirection(string printJobsLocation)
        {
            string returnValue = string.Empty;
            try
            {
                // Check for Print Jobs Location is not null or empty
                if (!string.IsNullOrEmpty(printJobsLocation))
                {
                    // Open the Print job location file
                    using (FileStream stream = new FileStream(printJobsLocation, FileMode.Open, FileAccess.Read))
                    {
                        int byteValue = 0;
                        bool bookletFlag = false;
                        bool duplexcompleted = false;
                        string hexValue;
                        // Read the complete stream in to bytes.
                        for (int byteIndex = 0; byteIndex < stream.Length; byteIndex++)
                        {
                            byteValue = stream.ReadByte();
                            hexValue = byteValue.ToString("X", CultureInfo.CurrentCulture);

                            if (!duplexcompleted)
                            {
                                if (hexValue == Constants.MEDIASIZE)
                                {
                                    bookletFlag = true;
                                    stream.Position = byteValue + 1;
                                }
                                if (hexValue == Constants.PAGEPATTERN && bookletFlag == true)
                                {
                                    returnValue = Constants.BOOKLET;
                                    duplexcompleted = true;
                                }
                                else if (hexValue == Constants.TEMPPATTERN && bookletFlag == true)
                                {
                                    returnValue = Constants.TABLET;
                                    duplexcompleted = true;
                                }
                            }

                        }
                        stream.Close();
                    }
                }
            }
            catch (Exception ex)
            { }
            return returnValue;
        }

        /// <summary>
        /// Provides the PCL converted data file.
        /// </summary>
        /// <param name="duplexDirection">The duplex direction.</param>
        /// <param name="physicalFilePath">The physical file path.</param>
        /// <returns></returns>
        /// <remarks>
        /// Class Diagram:<br/>
        /// 	<img src="ClassDiagrams/SD_JobParser.JobParser.ProvidePCLConvertedDataFile.jpg"/>
        /// </remarks>
        public static bool ProvidePCLConvertedDataFile(string duplexDirection, string physicalFilePath, string finalSettingsFile)
        {
            bool isFinalFileCreated = true;

            try
            {
                switch (duplexDirection)
                {
                    case "SIMPLEXDUPLEXBOOK":   //Simplex to Duplex Tablet
                        isFinalFileCreated = ConvertSimplexToDuplexBooklet(physicalFilePath, finalSettingsFile);
                        break;
                    case "SIMPLEXDUPLEXTABLET": //simplex to Duplex Book
                        isFinalFileCreated = ConvertSimplexToDuplexTablet(physicalFilePath, finalSettingsFile);
                        break;
                    case "DUPLEXBOOKSIMPLEX":   //Duplex Book to Simplex
                        isFinalFileCreated = ConvertDuplexBookletToSimplex(physicalFilePath, finalSettingsFile);
                        break;
                    case "DUPLEXTABLETSIMPLEX": //Duplex Tablet to Simplex
                        isFinalFileCreated = ConvertDuplexTabletToSimplex(physicalFilePath, finalSettingsFile);
                        break;
                    case "DUPLEXDUPLEXTABLET":  //Duplex Booklet to Duplex Tablet
                        isFinalFileCreated = ConvertDuplexBookletToDuplexTablet(physicalFilePath, finalSettingsFile);
                        break;
                    case "DUPLEXDUPLEXBOOK":    //Duplex Tablet to Duplex Booklet
                        isFinalFileCreated = ConvertDuplexTabletToDuplexBooklet(physicalFilePath, finalSettingsFile);
                        break;
                    default:
                        using (FileStream stream = new FileStream(physicalFilePath, FileMode.Open, FileAccess.ReadWrite))
                        {
                            int byteFoundAt = 0;

                            int byteValue = 0;
                            string hexValue;
                            int numberofBytes = 0;
                            bool isInitialByteFound = false;

                            for (int byteIndex = 0; byteIndex < stream.Length; byteIndex++)
                            {
                                byteValue = stream.ReadByte();
                                hexValue = byteValue.ToString("X", CultureInfo.CurrentCulture);
                                numberofBytes++;
                                if (isInitialByteFound)
                                {
                                    if (hexValue == "58")
                                    {
                                        isInitialByteFound = false;
                                        break;
                                    }
                                    else
                                    {
                                        isInitialByteFound = false;
                                    }
                                }
                                if (hexValue == "D1")
                                {
                                    byteFoundAt = numberofBytes - 1;
                                    isInitialByteFound = true;
                                }
                            }

                            stream.Seek(byteFoundAt, SeekOrigin.Begin);

                            int length = 10240;
                            byte[] buffer = new byte[length];
                            int bytesRead = 0;
                            // write the required bytes
                            FileStream fs = new FileStream(finalSettingsFile, FileMode.Append);

                            do
                            {
                                bytesRead = stream.Read(buffer, 0, length);
                                fs.Write(buffer, 0, bytesRead);
                            }
                            while (bytesRead == length);

                            fs.Close();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                isFinalFileCreated = false;
            }
            return isFinalFileCreated;
        }

        /// <summary>
        /// Converts the simplex to duplex booklet.
        /// </summary>
        /// <param name="physicalFilePath">The physical file path.</param>
        /// <returns></returns>
        /// <remarks>
        /// Class Diagram:<br/>
        /// 	<img src="ClassDiagrams/SD_JobParser.JobParser.ConvertSimplexToDuplexBooklet.jpg"/>
        /// </remarks>
        private static bool ConvertSimplexToDuplexBooklet(string physicalFilePath, string finalSettingsFile)
        {
            bool isFinalFileCreated = true;

            //ArrayList arrayBooklet = new ArrayList();
            string[] duplexTabletBytes = new string[] { Constants.DUPLEXPAGEMODE, Constants.UBYTE, Constants.TABLETDIRECTION, Constants.ATTRIBUTECONSTANT, Constants.DUPLEXPAGESIDE };
            bool Duplexflag = false;
            bool Duplexloop = false;
            bool bookletFlag = false;
            bool duplexcompleted = false;
            int maxSize = 100000;

            try
            {
                long position = ProvideSimplexToBookPosition(physicalFilePath, duplexTabletBytes, finalSettingsFile);

                using (FileStream stream = new FileStream(physicalFilePath, FileMode.Open, FileAccess.ReadWrite))
                {
                    stream.Seek(position, SeekOrigin.Begin);

                    int length = 10240;
                    byte[] buffer = new byte[length];
                    int bytesRead = 0;
                    // write the required bytes
                    FileStream fs = new FileStream(finalSettingsFile, FileMode.Append);

                    do
                    {
                        bytesRead = stream.Read(buffer, 0, length);
                        fs.Write(buffer, 0, bytesRead);
                    }
                    while (bytesRead == length);

                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                isFinalFileCreated = false;
            }

            return isFinalFileCreated;
        }

        /// <summary>
        /// Converts the simplex to duplex tablet.
        /// </summary>
        /// <param name="physicalFilePath">The physical file path.</param>
        /// <returns></returns>
        /// <remarks>
        /// Class Diagram:<br/>
        /// 	<img src="ClassDiagrams/SD_JobParser.JobParser.ConvertSimplexToDuplexTablet.jpg"/>
        /// </remarks>
        private static bool ConvertSimplexToDuplexTablet(string physicalFilePath, string finalSettingsFile)
        {
            bool isFinalFileCreated = true;
            //ArrayList arrayTablet = new ArrayList();
            string[] duplexTabletBytes = new string[] { Constants.DUPLEXPAGEMODE, Constants.UBYTE, Constants.TABLETDIRECTION, Constants.ATTRIBUTECONSTANT, Constants.DUPLEXPAGESIDE };
            bool Duplexflag = false;
            bool Duplexloop = false;
            bool duplexcompleted = false;
            int maxSize = 100000;

            try
            {
                long position = ProvideSimplexToTabletPosition(physicalFilePath, duplexTabletBytes, finalSettingsFile);

                using (FileStream stream = new FileStream(physicalFilePath, FileMode.Open, FileAccess.ReadWrite))
                {
                    stream.Seek(position, SeekOrigin.Begin);

                    int length = 10240;
                    byte[] buffer = new byte[length];
                    int bytesRead = 0;
                    // write the required bytes
                    FileStream fs = new FileStream(finalSettingsFile, FileMode.Append);

                    do
                    {
                        bytesRead = stream.Read(buffer, 0, length);
                        fs.Write(buffer, 0, bytesRead);
                    }
                    while (bytesRead == length);

                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                isFinalFileCreated = false;
            }

            return isFinalFileCreated;
        }

        /// <summary>
        /// Converts the duplex booklet to simplex.
        /// </summary>
        /// <param name="physicalFilePath">The physical file path.</param>
        /// <returns></returns>
        /// <remarks>
        /// Class Diagram:<br/>
        /// 	<img src="ClassDiagrams/SD_JobParser.JobParser.ConvertDuplexBookletToSimplex.jpg"/>
        /// </remarks>
        private static bool ConvertDuplexBookletToSimplex(string physicalFilePath, string finalSettingsFile)
        {
            bool isFinalFileCreated = true;
            //ArrayList arraySimplex = new ArrayList();
            bool Duplexloop = false;
            bool bookletFlag = false;
            bool duplexcompleted = false;

            try
            {
                long position = ProvideBookToSimplexPosition(physicalFilePath, finalSettingsFile);

                using (FileStream stream = new FileStream(physicalFilePath, FileMode.Open, FileAccess.ReadWrite))
                {
                    stream.Seek(position, SeekOrigin.Begin);

                    int length = 10240;
                    byte[] buffer = new byte[length];
                    int bytesRead = 0;
                    // write the required bytes
                    FileStream fs = new FileStream(finalSettingsFile, FileMode.Append);

                    do
                    {
                        bytesRead = stream.Read(buffer, 0, length);
                        fs.Write(buffer, 0, bytesRead);
                    }
                    while (bytesRead == length);

                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                isFinalFileCreated = false;
            }

            return isFinalFileCreated;
        }

        /// <summary>
        /// Converts the duplex tablet to simplex.
        /// </summary>
        /// <param name="physicalFilePath">The physical file path.</param>
        /// <returns></returns>
        /// <remarks>
        /// Class Diagram:<br/>
        /// 	<img src="ClassDiagrams/SD_JobParser.JobParser.ConvertDuplexTabletToSimplex.jpg"/>
        /// </remarks>
        private static bool ConvertDuplexTabletToSimplex(string physicalFilePath, string finalSettingsFile)
        {
            bool isFinalFileCreated = true;
            //ArrayList arraySimplexDataList = new ArrayList();
            bool Duplexloop = false;
            bool duplexcompleted = false;
            try
            {
                long position = ProvideTabletToSimplexPosition(physicalFilePath, finalSettingsFile);

                using (FileStream stream = new FileStream(physicalFilePath, FileMode.Open, FileAccess.ReadWrite))
                {
                    stream.Seek(position, SeekOrigin.Begin);

                    int length = 10240;
                    byte[] buffer = new byte[length];
                    int bytesRead = 0;
                    // write the required bytes
                    FileStream fs = new FileStream(finalSettingsFile, FileMode.Append);

                    do
                    {
                        bytesRead = stream.Read(buffer, 0, length);
                        fs.Write(buffer, 0, bytesRead);
                    }
                    while (bytesRead == length);

                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                isFinalFileCreated = false;
            }

            return isFinalFileCreated;
        }

        /// <summary>
        /// Converts the duplex booklet to duplex tablet.
        /// </summary>
        /// <param name="physicalFilePath">The physical file path.</param>
        /// <returns></returns>
        /// <remarks>
        /// Class Diagram:<br/>
        /// 	<img src="ClassDiagrams/SD_JobParser.JobParser.ConvertDuplexBookletToDuplexTablet.jpg"/>
        /// </remarks>
        private static bool ConvertDuplexBookletToDuplexTablet(string physicalFilePath, string finalSettingsFile)
        {
            bool isFinalFileCreated = true;
            //ArrayList arrayBookToTablet = new ArrayList();
            bool Duplexloop = false;
            bool bookletFlag = false;
            bool duplexcompleted = false;

            try
            {
                long position = ProvideBookToTabletPosition(physicalFilePath, finalSettingsFile);
                using (FileStream stream = new FileStream(physicalFilePath, FileMode.Open, FileAccess.ReadWrite))
                {
                    stream.Seek(position, SeekOrigin.Begin);

                    int length = 10240;
                    byte[] buffer = new byte[length];
                    int bytesRead = 0;
                    // write the required bytes
                    FileStream fs = new FileStream(finalSettingsFile, FileMode.Append);

                    do
                    {
                        bytesRead = stream.Read(buffer, 0, length);
                        fs.Write(buffer, 0, bytesRead);
                    }
                    while (bytesRead == length);

                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                isFinalFileCreated = false;
            }

            return isFinalFileCreated;
        }

        /// <summary>
        /// Converts the duplex tablet to duplex booklet.
        /// </summary>
        /// <param name="physicalFilePath">The physical file path.</param>
        /// <returns></returns>
        /// <remarks>
        /// Class Diagram:<br/>
        /// 	<img src="ClassDiagrams/SD_JobParser.JobParser.ConvertDuplexTabletToDuplexBooklet.jpg"/>
        /// </remarks>
        private static bool ConvertDuplexTabletToDuplexBooklet(string physicalFilePath, string finalSettingsFile)
        {
            bool isFinalFileCreated = true;
            ArrayList arrayTabletToBooklet = new ArrayList();
            bool Duplexloop = false;
            bool bookletFlag = false;
            bool duplexcompleted = false;

            try
            {
                long position = ProvideTabletToBookPosition(physicalFilePath, finalSettingsFile);
                using (FileStream stream = new FileStream(physicalFilePath, FileMode.Open, FileAccess.ReadWrite))
                {
                    stream.Seek(position, SeekOrigin.Begin);

                    int length = 10240;
                    byte[] buffer = new byte[length];
                    int bytesRead = 0;
                    // write the required bytes
                    FileStream fs = new FileStream(finalSettingsFile, FileMode.Append);

                    do
                    {
                        bytesRead = stream.Read(buffer, 0, length);
                        fs.Write(buffer, 0, bytesRead);
                    }
                    while (bytesRead == length);

                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                isFinalFileCreated = false;
            }
            return isFinalFileCreated;
        }

        /// <summary>
        /// Provides the simplex to book position.
        /// </summary>
        /// <param name="physicalFilePath">The physical file path.</param>
        /// <param name="duplexTabletBytes">The duplex tablet bytes.</param>
        /// <param name="finalSettingsFile">The final settings file.</param>
        /// <returns></returns>
        private static long ProvideSimplexToBookPosition(string physicalFilePath, string[] duplexTabletBytes, string finalSettingsFile)
        {

            long position = 0;
            bool Duplexflag = false;
            bool Duplexloop = false;
            bool bookletFlag = false;
            bool duplexcompleted = false;

            using (FileStream stream = new FileStream(physicalFilePath, FileMode.Open, FileAccess.ReadWrite))
            {
                int byteFoundAt = 0;

                int byteValue = 0;
                string hexValue;
                int numberofBytes = 0;
                bool isInitialByteFound = false;
                try
                {
                    for (int byteIndex = 0; byteIndex < stream.Length; byteIndex++)
                    {
                        byteValue = stream.ReadByte();
                        hexValue = byteValue.ToString("X", CultureInfo.CurrentCulture);
                        numberofBytes++;
                        if (hexValue == "D1")
                        {
                            byteFoundAt = numberofBytes - 1;
                            isInitialByteFound = true;
                        }
                        if (hexValue == "58" && isInitialByteFound == true)
                        {
                            isInitialByteFound = false;
                            break;
                        }
                    }
                    //byteFoundAt = 0;
                    stream.Seek(byteFoundAt, SeekOrigin.Begin);


                    byteValue = 0;
                    hexValue = string.Empty;
                    FileStream fs = new FileStream(finalSettingsFile, FileMode.Append);
                    for (int byteIndex = 0; byteIndex < stream.Length; byteIndex++)
                    {
                        position++;
                        Duplexloop = false;
                        byteValue = stream.ReadByte();
                        hexValue = byteValue.ToString("X", CultureInfo.CurrentCulture);

                        if (!duplexcompleted)
                        {
                            if (hexValue == Constants.MEDIASIZE)
                            {
                                bookletFlag = true;
                            }
                            if (hexValue == Constants.TEMPPATTERN && bookletFlag == true)
                            {
                                hexValue = Constants.BOOKLETDIRECTION;
                                bookletFlag = false;
                            }
                            if (hexValue == Constants.SIMPLEXPAGEMODE)
                            {
                                if (Duplexflag == false)
                                {
                                    Duplexflag = true;
                                    for (int i = 0; i < duplexTabletBytes.Length; i++)
                                    {
                                        Duplexloop = true;
                                        byte returnByteData = Convert.ToByte(duplexTabletBytes[i], 16);
                                        fs.WriteByte(returnByteData);
                                    }
                                    duplexcompleted = true;
                                }
                                break;
                            }
                        }

                        if (!Duplexloop)
                        {
                            byte returnByteData = Convert.ToByte(hexValue, 16);
                            fs.WriteByte(returnByteData);
                        }
                    }

                    fs.Close();
                    stream.Close();
                }
                catch (Exception ex)
                { }
                return position + byteFoundAt;
            }

        }

        /// <summary>
        /// Provides the simplex to tablet position.
        /// </summary>
        /// <param name="physicalFilePath">The physical file path.</param>
        /// <param name="duplexTabletBytes">The duplex tablet bytes.</param>
        /// <param name="finalSettingsFile">The final settings file.</param>
        /// <returns></returns>
        private static long ProvideSimplexToTabletPosition(string physicalFilePath, string[] duplexTabletBytes, string finalSettingsFile)
        {

            long position = 0;
            bool Duplexflag = false;
            bool Duplexloop = false;
            bool bookletFlag = false;
            bool duplexcompleted = false;

            using (FileStream stream = new FileStream(physicalFilePath, FileMode.Open, FileAccess.ReadWrite))
            {
                int byteFoundAt = 0;

                int byteValue = 0;
                string hexValue;
                int numberofBytes = 0;
                bool isInitialByteFound = false;
                try
                {
                    for (int byteIndex = 0; byteIndex < stream.Length; byteIndex++)
                    {
                        byteValue = stream.ReadByte();
                        hexValue = byteValue.ToString("X", CultureInfo.CurrentCulture);
                        numberofBytes++;
                        if (hexValue == "D1")
                        {
                            byteFoundAt = numberofBytes - 1;
                            isInitialByteFound = true;
                        }
                        if (hexValue == "58" && isInitialByteFound == true)
                        {
                            isInitialByteFound = false;
                            break;
                        }
                    }
                    //byteFoundAt = 0;
                    stream.Seek(byteFoundAt, SeekOrigin.Begin);


                    byteValue = 0;
                    hexValue = string.Empty;
                    FileStream fs = new FileStream(finalSettingsFile, FileMode.Append);
                    for (int byteIndex = 0; byteIndex < stream.Length; byteIndex++)
                    {
                        position++;
                        Duplexloop = false;
                        byteValue = stream.ReadByte();
                        hexValue = byteValue.ToString("X", CultureInfo.CurrentCulture);
                        if (!duplexcompleted)
                        {
                            if (hexValue == Constants.SIMPLEXPAGEMODE)
                            {
                                if (Duplexflag == false)
                                {
                                    Duplexflag = true;
                                    for (int i = 0; i < duplexTabletBytes.Length; i++)
                                    {
                                        Duplexloop = true;
                                        byte returnByteData = Convert.ToByte(duplexTabletBytes[i], 16);
                                        fs.WriteByte(returnByteData);
                                    }
                                    duplexcompleted = true;
                                }
                                break;
                            }
                        }
                        if (!Duplexloop)
                        {
                            byte returnByteData = Convert.ToByte(hexValue, 16);
                            fs.WriteByte(returnByteData);
                        }
                    }

                    fs.Close();
                    stream.Close();
                }
                catch (Exception ex)
                { }
                return position + byteFoundAt;
            }

        }

        /// <summary>
        /// Provides the book to simplex position.
        /// </summary>
        /// <param name="physicalFilePath">The physical file path.</param>
        /// <param name="finalSettingsFile">The final settings file.</param>
        /// <returns></returns>
        private static long ProvideBookToSimplexPosition(string physicalFilePath, string finalSettingsFile)
        {

            long position = 0;
            bool Duplexloop = false;
            bool bookletFlag = false;
            bool duplexcompleted = false;
            bool isBreak = false;
            int byteFoundAt = 0;
            try
            {
                using (FileStream stream = new FileStream(physicalFilePath, FileMode.Open, FileAccess.ReadWrite))
                {

                    int byteValue = 0;
                    string hexValue;
                    int numberofBytes = 0;
                    bool isInitialByteFound = false;

                    for (int byteIndex = 0; byteIndex < stream.Length; byteIndex++)
                    {
                        byteValue = stream.ReadByte();
                        hexValue = byteValue.ToString("X", CultureInfo.CurrentCulture);
                        numberofBytes++;
                        if (hexValue == "D1")
                        {
                            byteFoundAt = numberofBytes - 1;
                            isInitialByteFound = true;
                        }
                        if (hexValue == "58" && isInitialByteFound == true)
                        {
                            isInitialByteFound = false;
                            break;
                        }
                    }
                    //byteFoundAt = 0;
                    stream.Seek(byteFoundAt, SeekOrigin.Begin);

                    byteValue = 0;
                    hexValue = string.Empty;
                    FileStream fs = new FileStream(finalSettingsFile, FileMode.Append);
                    for (int byteIndex = 0; byteIndex < stream.Length; byteIndex++)
                    {
                        position++;
                        Duplexloop = false;
                        byteValue = stream.ReadByte();
                        hexValue = byteValue.ToString("X", CultureInfo.CurrentCulture);

                        if (!duplexcompleted)
                        {
                            if (hexValue == Constants.MEDIASIZE)
                            {
                                bookletFlag = true;
                            }
                            if (hexValue == Constants.PAGEPATTERN && bookletFlag == true)
                            {
                                hexValue = Constants.TABLETDIRECTION;
                                bookletFlag = false;
                            }
                            if (hexValue == Constants.DUPLEXPAGEMODE)
                            {
                                hexValue = Constants.SIMPLEXPAGEMODE;
                                stream.Position = byteIndex + 5;
                                duplexcompleted = true;
                                //position = stream.Position;
                                isBreak = true;
                            }
                        }

                        if (!Duplexloop)
                        {
                            byte returnByteData = Convert.ToByte(hexValue, 16);
                            fs.WriteByte(returnByteData);
                        }
                        if (isBreak)
                        {
                            break;
                        }
                    }
                    fs.Close();
                    stream.Close();
                }
            }
            catch (Exception ex)
            { }
            return position + byteFoundAt;
        }

        /// <summary>
        /// Provides the tablet to simplex position.
        /// </summary>
        /// <param name="physicalFilePath">The physical file path.</param>
        /// <param name="finalSettingsFile">The final settings file.</param>
        /// <returns></returns>
        private static long ProvideTabletToSimplexPosition(string physicalFilePath, string finalSettingsFile)
        {
            long position = 0;
            bool Duplexloop = false;
            bool bookletFlag = false;
            bool duplexcompleted = false;
            bool isBreak = false;
            int byteFoundAt = 0;
            try
            {
                using (FileStream stream = new FileStream(physicalFilePath, FileMode.Open, FileAccess.ReadWrite))
                {
                    int byteValue = 0;
                    string hexValue;
                    int numberofBytes = 0;
                    bool isInitialByteFound = false;

                    for (int byteIndex = 0; byteIndex < stream.Length; byteIndex++)
                    {
                        byteValue = stream.ReadByte();
                        hexValue = byteValue.ToString("X", CultureInfo.CurrentCulture);
                        numberofBytes++;
                        if (hexValue == "D1")
                        {
                            byteFoundAt = numberofBytes - 1;
                            isInitialByteFound = true;
                        }
                        if (hexValue == "58" && isInitialByteFound == true)
                        {
                            isInitialByteFound = false;
                            break;
                        }
                    }
                    //byteFoundAt = 0;
                    stream.Seek(byteFoundAt, SeekOrigin.Begin);

                    byteValue = 0;
                    hexValue = string.Empty;
                    FileStream fs = new FileStream(finalSettingsFile, FileMode.Append);
                    for (int byteIndex = 0; byteIndex < stream.Length; byteIndex++)
                    {
                        position++;
                        Duplexloop = false;
                        byteValue = stream.ReadByte();
                        hexValue = byteValue.ToString("X", CultureInfo.CurrentCulture);
                        if (!duplexcompleted)
                        {
                            if (hexValue == Constants.DUPLEXPAGEMODE)
                            {
                                hexValue = Constants.SIMPLEXPAGEMODE;
                                stream.Position = byteIndex + 5;
                                duplexcompleted = true;
                                //position = stream.Position;
                                isBreak = true;
                            }
                        }
                        if (!Duplexloop)
                        {
                            byte returnByteData = Convert.ToByte(hexValue, 16);
                            fs.WriteByte(returnByteData);
                        }
                        if (isBreak)
                        {
                            break;
                        }
                    }
                    fs.Close();
                    stream.Close();
                }
            }
            catch (Exception ex)
            { }
            return position + byteFoundAt;
        }

        /// <summary>
        /// Provides the book to tablet position.
        /// </summary>
        /// <param name="physicalFilePath">The physical file path.</param>
        /// <param name="finalSettingsFile">The final settings file.</param>
        /// <returns></returns>
        private static long ProvideBookToTabletPosition(string physicalFilePath, string finalSettingsFile)
        {
            long position = 0;
            bool Duplexloop = false;
            bool bookletFlag = false;
            bool duplexcompleted = false;
            bool isBreak = false;
            int byteFoundAt = 0;
            try
            {
                using (FileStream stream = new FileStream(physicalFilePath, FileMode.Open, FileAccess.ReadWrite))
                {
                    int byteValue = 0;
                    string hexValue;
                    int numberofBytes = 0;
                    bool isInitialByteFound = false;

                    for (int byteIndex = 0; byteIndex < stream.Length; byteIndex++)
                    {
                        byteValue = stream.ReadByte();
                        hexValue = byteValue.ToString("X", CultureInfo.CurrentCulture);
                        numberofBytes++;
                        if (hexValue == "D1")
                        {
                            byteFoundAt = numberofBytes - 1;
                            isInitialByteFound = true;
                        }
                        if (hexValue == "58" && isInitialByteFound == true)
                        {
                            isInitialByteFound = false;
                            break;
                        }
                    }
                    //byteFoundAt = 0;
                    stream.Seek(byteFoundAt, SeekOrigin.Begin);

                    byteValue = 0;
                    hexValue = string.Empty;
                    FileStream fs = new FileStream(finalSettingsFile, FileMode.Append);
                    for (int byteIndex = 0; byteIndex < stream.Length; byteIndex++)
                    {
                        position++;
                        Duplexloop = false;
                        byteValue = stream.ReadByte();
                        hexValue = byteValue.ToString("X", CultureInfo.CurrentCulture);

                        if (!duplexcompleted)
                        {
                            if (hexValue == Constants.MEDIASIZE)
                            {
                                bookletFlag = true;
                            }
                            if (hexValue == Constants.PAGEPATTERN && bookletFlag == true)
                            {
                                hexValue = Constants.TABLETDIRECTION;
                                bookletFlag = false;
                                duplexcompleted = true;
                                //position = stream.Position;
                                isBreak = true;
                            }
                        }

                        if (!Duplexloop)
                        {
                            byte returnByteData = Convert.ToByte(hexValue, 16);
                            fs.WriteByte(returnByteData);
                        }
                        if (isBreak)
                        {
                            break;
                        }
                    }
                    fs.Close();
                    stream.Close();
                }
            }
            catch (Exception ex)
            { }
            return position + byteFoundAt;
        }

        /// <summary>
        /// Provides the tablet to book position.
        /// </summary>
        /// <param name="physicalFilePath">The physical file path.</param>
        /// <param name="finalSettingsFile">The final settings file.</param>
        /// <returns></returns>
        private static long ProvideTabletToBookPosition(string physicalFilePath, string finalSettingsFile)
        {
            long position = 0;
            bool Duplexloop = false;
            bool bookletFlag = false;
            bool duplexcompleted = false;
            bool isBreak = false;
            int byteFoundAt = 0;
            try
            {
                using (FileStream stream = new FileStream(physicalFilePath, FileMode.Open, FileAccess.ReadWrite))
                {
                    int byteValue = 0;
                    string hexValue;
                    int numberofBytes = 0;
                    bool isInitialByteFound = false;

                    for (int byteIndex = 0; byteIndex < stream.Length; byteIndex++)
                    {
                        byteValue = stream.ReadByte();
                        hexValue = byteValue.ToString("X", CultureInfo.CurrentCulture);
                        numberofBytes++;
                        if (hexValue == "D1")
                        {
                            byteFoundAt = numberofBytes - 1;
                            isInitialByteFound = true;
                        }
                        if (hexValue == "58" && isInitialByteFound == true)
                        {
                            isInitialByteFound = false;
                            break;
                        }
                    }
                    //byteFoundAt = 0;
                    stream.Seek(byteFoundAt, SeekOrigin.Begin);

                    byteValue = 0;
                    hexValue = string.Empty;
                    FileStream fs = new FileStream(finalSettingsFile, FileMode.Append);
                    for (int byteIndex = 0; byteIndex < stream.Length; byteIndex++)
                    {
                        position++;
                        Duplexloop = false;
                        byteValue = stream.ReadByte();
                        hexValue = byteValue.ToString("X", CultureInfo.CurrentCulture);

                        if (!duplexcompleted)
                        {
                            if (hexValue == Constants.MEDIASIZE)
                            {
                                bookletFlag = true;
                            }
                            if (hexValue == Constants.TEMPPATTERN && bookletFlag == true)
                            {
                                hexValue = Constants.BOOKLETDIRECTION;
                                bookletFlag = false;
                                duplexcompleted = true;
                                //position = stream.Position;
                                isBreak = true;
                            }
                        }

                        if (!Duplexloop)
                        {
                            byte returnByteData = Convert.ToByte(hexValue, 16);
                            fs.WriteByte(returnByteData);
                        }
                        if (isBreak)
                        {
                            break;
                        }
                    }

                    fs.Close();
                    stream.Close();
                }
            }
            catch (Exception ex)
            { }
            return position + byteFoundAt;

        }

        #endregion

        #region :POST SCRIPT Driver:

        /// <summary>
        /// Provides the post script sriver duplex direction.
        /// </summary>
        /// <param name="printJobsLocation">The print jobs location.</param>
        /// <returns></returns>
        /// <remarks>
        /// Class Diagram:<br/>
        /// 	<img src="ClassDiagrams/SD_JobParser.JobParser.ProvidePostScriptDuplexDirection.jpg"/>
        /// </remarks>
        private static string ProvidePostScriptDuplexDirection(string printJobsLocation)
        {
            string returnValue = string.Empty;
            string duplexOn = Constants.POSTSCRIPTDUPLEXTRUE;
            string duplexOff = Constants.POSTSCRIPTDUPLEXFALSE;
            try
            {
                using (StreamReader streamReader = new StreamReader(printJobsLocation))
                {
                    while (streamReader.Peek() >= 0)
                    {
                        string readline = streamReader.ReadLine();
                        if (Regex.IsMatch(readline, duplexOn))
                        {
                            // If found then check for the "BINDING=LONGEDGE" string.
                            // Which is equivalent to Booklet, If found return string as BOOK.
                            if (Regex.IsMatch(readline, Constants.POSTSCRIPTBOOKLETSTRING))
                            {
                                returnValue = Constants.BOOKLET;
                            }
                            // else check for the "BINDING=SHORTEDGE" string.
                            // Which is equivalent to Tablet, If found return string as TABLET.
                            else if (Regex.IsMatch(readline, Constants.POSTSCRIPTTABLETSTRING))
                            {
                                returnValue = Constants.TABLET;
                            }
                            // else nothing is found, then return string as none.
                            else
                            {
                                returnValue = Constants.SIMPLEX;
                            }
                        }
                        // If "DUPLEX=ON" string is not found, then return it as SIMPLEX. 
                        else if (Regex.IsMatch(readline, duplexOff))
                        {
                            returnValue = Constants.SIMPLEX;
                        }

                        if (!string.IsNullOrEmpty(returnValue))
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
            return returnValue;
        }

        /// <summary>
        /// Provides the post script converted data file.
        /// </summary>
        /// <param name="duplexDirection">The duplex direction.</param>
        /// <param name="physicalFilePath">The physical file path.</param>
        /// <returns></returns>
        /// <remarks>
        /// Class Diagram:<br/>
        /// 	<img src="ClassDiagrams/SD_JobParser.JobParser.ProvidePostscriptConvertedDataFile.jpg"/>
        /// </remarks>
        public static bool ProvidePostscriptConvertedDataFile(string duplexDirection, string physicalFilePath, string postScriptColorMode, string finalSettingsFile)
        {
            bool isFinalFileCreated = true;
            string searchString = string.Empty;
            string replaceString = string.Empty;
            string textToInsert = string.Empty;
            try
            {
                switch (duplexDirection)
                {
                    case "SIMPLEXDUPLEXBOOK":   //Simplex to Duplex Tablet
                        textToInsert = "[{<< /Duplex true /Tumble false >> setpagedevice} stopped cleartomark";
                        isFinalFileCreated = InsertPostScriptDuplex(physicalFilePath, textToInsert, postScriptColorMode, finalSettingsFile);
                        break;
                    case "SIMPLEXDUPLEXTABLET": //simplex to Duplex Book
                        textToInsert = "[{<< /Duplex true /Tumble true >> setpagedevice} stopped cleartomark";
                        isFinalFileCreated = InsertPostScriptDuplex(physicalFilePath, textToInsert, postScriptColorMode, finalSettingsFile);
                        break;
                    case "DUPLEXBOOKSIMPLEX":   //Duplex Book to Simplex
                        isFinalFileCreated = ConvertPostScriptDuplexBookletToSimplex(physicalFilePath, postScriptColorMode, finalSettingsFile);
                        break;
                    case "DUPLEXTABLETSIMPLEX": //Duplex Tablet to Simplex
                        isFinalFileCreated = ConvertPostScriptDuplexBookletToSimplex(physicalFilePath, postScriptColorMode, finalSettingsFile); // Both functions do same operation
                        break;
                    case "DUPLEXDUPLEXTABLET":  //Duplex Booklet to Duplex Tablet
                        searchString = "/Duplex true /Tumble false >> setpagedevice} stopped cleartomark";
                        replaceString = "/Duplex true /Tumble true >> setpagedevice} stopped cleartomark";
                        isFinalFileCreated = ConvertPostScriptDuplex(searchString, replaceString, physicalFilePath, postScriptColorMode, finalSettingsFile);
                        break;
                    case "DUPLEXDUPLEXBOOK":
                        searchString = "/Duplex true /Tumble true >> setpagedevice} stopped cleartomark";
                        replaceString = "/Duplex true /Tumble false >> setpagedevice} stopped cleartomark";
                        isFinalFileCreated = ConvertPostScriptDuplex(searchString, replaceString, physicalFilePath, postScriptColorMode, finalSettingsFile);
                        break;
                    default:
                        isFinalFileCreated = providePostScriptSimplex(physicalFilePath, postScriptColorMode, finalSettingsFile);
                        break;
                }
            }
            catch (Exception ex)
            {
                isFinalFileCreated = false;
            }
            return isFinalFileCreated;
        }

        /// <summary>
        /// Provides the post script simplex.
        /// </summary>
        /// <param name="physicalFilePath">The physical file path.</param>
        /// <param name="postScriptColorMode">The post script color mode.</param>
        /// <returns></returns>
        /// <remarks>
        /// Class Diagram:<br/>
        /// 	<img src="ClassDiagrams/SD_JobParser.JobParser.providePostScriptSimplex.jpg"/>
        /// </remarks>
        private static bool providePostScriptSimplex(string physicalFilePath, string postScriptColorMode, string finalSettingsFile)
        {
            bool isFinalFileCreated = true;
            try
            {
                if (!string.IsNullOrEmpty(postScriptColorMode) && postScriptColorMode != "AUTO")
                {
                    string postScriptSearchString = "";
                    string postScriptReplaceString = "";
                    if (postScriptColorMode == "BW")
                    {
                        postScriptSearchString = "<< /ProcessColorModel /DeviceCMYK >> setpagedevice} stopped cleartomark";
                        postScriptReplaceString = "<< /ProcessColorModel /DeviceGray >> setpagedevice} stopped cleartomark";
                    }
                    else
                    {
                        postScriptSearchString = "<< /ProcessColorModel /DeviceGray >> setpagedevice} stopped cleartomark";
                        postScriptReplaceString = "<< /ProcessColorModel /DeviceCMYK >> setpagedevice} stopped cleartomark";
                    }
                    isFinalFileCreated = ReplaceChunkByChunk(physicalFilePath, postScriptSearchString, postScriptReplaceString, "", "", chunk, finalSettingsFile);
                }
                else
                {
                    using (FileStream stream = new FileStream(physicalFilePath, FileMode.Open, FileAccess.ReadWrite))
                    {
                        int byteFoundAt = 0;

                        int byteValue = 0;
                        string hexValue;
                        int numberofBytes = 0;
                        bool isInitialByteFound = false;

                        for (int byteIndex = 0; byteIndex < stream.Length; byteIndex++)
                        {
                            byteValue = stream.ReadByte();
                            hexValue = byteValue.ToString("X", CultureInfo.CurrentCulture);
                            numberofBytes++;
                            if (isInitialByteFound)
                            {
                                if (hexValue == "21")
                                {
                                    isInitialByteFound = false;
                                    break;
                                }
                                else
                                {
                                    isInitialByteFound = false;
                                }
                            }
                            if (hexValue == "25")
                            {
                                byteFoundAt = numberofBytes - 1;
                                isInitialByteFound = true;
                            }
                        }

                        stream.Seek(byteFoundAt, SeekOrigin.Begin);

                        int length = 10240;
                        byte[] buffer = new byte[length];
                        int bytesRead = 0;
                        // write the required bytes
                        FileStream fs = new FileStream(finalSettingsFile, FileMode.Append);

                        do
                        {
                            bytesRead = stream.Read(buffer, 0, length);
                            fs.Write(buffer, 0, bytesRead);
                        }
                        while (bytesRead == length);

                        fs.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                isFinalFileCreated = false;
            }
            return isFinalFileCreated;
        }

        /// <summary>
        /// Replaces the chunk by chunk.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="searchString">The search string.</param>
        /// <param name="replaceString">The replace string.</param>
        /// <param name="searchString2">The search string2.</param>
        /// <param name="replaceString2">The replace string2.</param>
        /// <param name="chunkLength">Length of the chunk.</param>
        /// <param name="finalSettingsFile">The final settings file.</param>
        /// <returns></returns>
        public static bool ReplaceChunkByChunk(string filePath, string searchString, string replaceString, string searchString2, string replaceString2, int chunkLength, string finalSettingsFile)
        {
            bool isFinalFileCreated = true;
            try
            {
                int loadLength = chunkLength + searchString.Length;
                char[] buffer = new char[loadLength];
                int index = 0;
                int replaceLength = searchString.Length;
                string previousData = string.Empty;
                int loopCount = 0;

                string tempFile = filePath + "PSTemp";
                StreamReader streamReader = new StreamReader(filePath);
                StreamWriter streamWriter = new StreamWriter(tempFile);

                try
                {
                    //Load the data according to the chunk length specified
                    //also load the extra data according to the length of
                    //the replace text so that chunk end and start text break
                    //can be handled            
                    while (true)
                    {
                        //Seek to the position from where we want to start and
                        //load the data in to the memory
                        streamReader.DiscardBufferedData();
                        streamReader.BaseStream.Seek(index, SeekOrigin.Begin);
                        int count = streamReader.ReadBlock(buffer, 0, loadLength);
                        if (count == 0) break;

                        //Get the data and check whether the extra loaded data
                        //is replaceable if yes then set end replaced flag                
                        string data = ConvertToString(buffer, count);
                        bool isEndReplaced = false;
                        if (count >= chunkLength)
                        {
                            isEndReplaced = (data.LastIndexOf(replaceString, chunkLength) > 0);
                        }

                        //Replace the data with the specified data and save the
                        //new data in the new file
                        data = data.Replace(searchString, replaceString);
                        if (!string.IsNullOrEmpty(searchString2))
                        {
                            data = data.Replace(searchString2, replaceString2);
                        }
                        if (isEndReplaced)
                        {
                            streamWriter.Write(data);
                            index += count;
                        }
                        else
                        {
                            if (count >= chunkLength)
                            {
                                if (loopCount > 0)
                                {
                                    string firstTwoCharacters = previousData.Substring(0, 2);
                                    if (data.Substring(0, 2) == firstTwoCharacters)
                                    {
                                        data = data.Remove(0, 2);
                                    }
                                }
                                previousData = data.Remove(0, data.Length - replaceLength - 2);
                                streamWriter.Write(data.Substring(0, data.Length - replaceLength));
                                index += chunkLength;
                            }
                            else
                            {
                                streamWriter.Write(data);
                                index += chunkLength;
                            }
                        }
                        loopCount++;
                    }

                    streamReader.Close();
                    streamWriter.Close();
                }
                catch (Exception ex)
                {
                    streamReader.Close();
                    streamWriter.Close();
                    isFinalFileCreated = false;
                }

                int read = 0;
                byte[] bufferTemp = new byte[4096];

                using (FileStream writeStream = new FileStream(finalSettingsFile, FileMode.Append))
                {
                    using (FileStream readStream = new FileStream(tempFile, FileMode.Open, FileAccess.Read))
                    {
                        int byteFoundAt = 0;

                        int byteValue = 0;
                        string hexValue;
                        int numberofBytes = 0;
                        bool isInitialByteFound = false;

                        for (int byteIndex = 0; byteIndex < readStream.Length; byteIndex++)
                        {
                            byteValue = readStream.ReadByte();
                            hexValue = byteValue.ToString("X", CultureInfo.CurrentCulture);
                            numberofBytes++;
                            if (isInitialByteFound)
                            {
                                if (hexValue == "21")
                                {
                                    isInitialByteFound = false;
                                    break;
                                }
                                else
                                {
                                    isInitialByteFound = false;
                                }
                            }
                            if (hexValue == "25")
                            {
                                byteFoundAt = numberofBytes - 1;
                                isInitialByteFound = true;
                            }
                        }

                        readStream.Seek(byteFoundAt, SeekOrigin.Begin);

                        while ((read = readStream.Read(bufferTemp, 0, bufferTemp.Length)) > 0)
                        {
                            writeStream.Write(bufferTemp, 0, read);
                        }
                    }
                    if (File.Exists(tempFile))
                    {
                        File.Delete(tempFile);
                    }
                }
            }
            catch (Exception ex)
            { }
            return isFinalFileCreated;
        }

        /// <summary>
        /// Inserts the post script duplex.
        /// </summary>
        /// <param name="physicalFilePath">The physical file path.</param>
        /// <param name="textToInsert">The text to insert.</param>
        /// <returns></returns>
        /// <remarks>
        /// Class Diagram:<br/>
        /// 	<img src="ClassDiagrams/SD_JobParser.JobParser.InsertPostScriptDuplex.jpg"/>
        /// </remarks>
        private static bool InsertPostScriptDuplex(string physicalFilePath, string textToInsert, string postScriptColorMode, string finalSettingsFile)
        {
            bool isFinalFileCreated = true;
            try
            {
                isFinalFileCreated = GetPostScriptConvertedDataFile(physicalFilePath, textToInsert, postScriptColorMode, finalSettingsFile);
            }
            catch (Exception ex)
            {
                isFinalFileCreated = false;
            }
            return isFinalFileCreated;
        }

        /// <summary>
        /// Converts the post script duplex.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <param name="replaceString">The replace string.</param>
        /// <param name="physicalFilePath">The physical file path.</param>
        /// <returns></returns>
        /// <remarks>
        /// Class Diagram:<br/>
        /// 	<img src="ClassDiagrams/SD_JobParser.JobParser.ConvertPostScriptDuplex.jpg"/>
        /// </remarks>
        private static bool ConvertPostScriptDuplex(string searchString, string replaceString, string physicalFilePath, string postScriptColorMode, string finalSettingsFile)
        {
            bool isFinalFileCreated = true;
            try
            {
                if (!string.IsNullOrEmpty(postScriptColorMode) && postScriptColorMode != "AUTO")
                {
                    string postScriptSearchString = "";
                    string postScriptReplaceString = "";
                    if (postScriptColorMode == "BW")
                    {
                        postScriptSearchString = "<< /ProcessColorModel /DeviceCMYK";
                        postScriptReplaceString = "<< /ProcessColorModel /DeviceGray";
                    }
                    else
                    {
                        postScriptSearchString = "<< /ProcessColorModel /DeviceGray";
                        postScriptReplaceString = "<< /ProcessColorModel /DeviceCMYK";
                    }
                    isFinalFileCreated = ReplaceChunkByChunk(physicalFilePath, searchString, replaceString, postScriptSearchString, postScriptReplaceString, chunk, finalSettingsFile);
                }
                else
                {
                    isFinalFileCreated = ReplaceChunkByChunk(physicalFilePath, searchString, replaceString, "", "", chunk, finalSettingsFile);
                }
            }
            catch (Exception ex)
            {
                isFinalFileCreated = false;
            }
            return isFinalFileCreated;
        }

        /// <summary>
        /// Converts the post script duplex booklet to simplex.
        /// </summary>
        /// <param name="physicalFilePath">The physical file path.</param>
        /// <returns></returns>
        /// <remarks>
        /// Class Diagram:<br/>
        /// 	<img src="ClassDiagrams/SD_JobParser.JobParser.ConvertPostScriptDuplexBookletToSimplex.jpg"/>
        /// </remarks>
        private static bool ConvertPostScriptDuplexBookletToSimplex(string physicalFilePath, string postScriptColorMode, string finalSettingsFile)
        {
            bool isFinalFileCreated = true;
            string searchString = Constants.POSTSCRIPTDUPLEXTRUE;
            string replaceString = Constants.POSTSCRIPTDUPLEXFALSE;
            try
            {
                if (!string.IsNullOrEmpty(postScriptColorMode) && postScriptColorMode == "AUTO")
                {
                    string searchString1 = "";
                    string replaceString1 = "";
                    if (postScriptColorMode == "BW")
                    {
                        searchString1 = "<< /ProcessColorModel /DeviceCMYK";
                        replaceString1 = "<< /ProcessColorModel /DeviceGray";
                    }
                    else
                    {
                        searchString1 = "<< /ProcessColorModel /DeviceGray";
                        replaceString1 = "<< /ProcessColorModel /DeviceCMYK";
                    }
                    isFinalFileCreated = ReplaceChunkByChunk(physicalFilePath, searchString, replaceString, searchString1, replaceString1, chunk, finalSettingsFile);
                }
                else
                {
                    isFinalFileCreated = ReplaceChunkByChunk(physicalFilePath, searchString, replaceString, "", "", chunk, finalSettingsFile);
                }
            }
            catch (Exception ex)
            {
                isFinalFileCreated = false;
            }

            return isFinalFileCreated;
        }

        /// <summary>
        /// Gets the post script converted data file.
        /// </summary>
        /// <param name="physicalFilePath">The physical file path.</param>
        /// <param name="textToInsert">The text to insert.</param>
        /// <returns></returns>
        /// <remarks>
        /// Class Diagram:<br/>
        /// 	<img src="ClassDiagrams/SD_JobParser.JobParser.GetPostScriptConvertedDataFile.jpg"/>
        /// </remarks>
        private static bool GetPostScriptConvertedDataFile(string physicalFilePath, string textToInsert, string postScriptColorMode, string finalSettingsFile)
        {
            bool isFinalFileCreated = true;
            try
            {
                string fileLocation = GetFileLocation(physicalFilePath);
                int insertAtLineNumber = 0;
                int count = 0;
                try
                {
                    using (StreamReader sr = new StreamReader(physicalFilePath))
                    {
                        while (sr.Peek() >= 0)
                        {
                            string readline = sr.ReadLine();
                            insertAtLineNumber += 1;
                            if (readline == "%%BeginSetup")
                            {
                                if (count == 1)
                                {
                                    sr.Close();
                                    break;
                                }
                                count += 1;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    isFinalFileCreated = false;
                }
                insertAtLineNumber = insertAtLineNumber + 4;
                ArrayList lines = new ArrayList();
                StreamReader streamReader = new StreamReader(physicalFilePath);
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
                streamReader.Close();

                if (lines.Count > insertAtLineNumber)
                {
                    lines.Insert(insertAtLineNumber, textToInsert);
                }
                else
                {
                    lines.Add(textToInsert);
                }

                string timeStamp = DateTime.Now.Ticks.ToString(CultureInfo.CurrentCulture).ToString();

                string tempFile = fileLocation + "\\Temp" + timeStamp + ".data";
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }

                // write temp data file
                StreamWriter streamWriter = new StreamWriter(tempFile);
                foreach (string strNewLine in lines)
                {
                    streamWriter.WriteLine(strNewLine);
                }
                streamWriter.Close();

                bool isDataAvailable = true;
                if (!string.IsNullOrEmpty(postScriptColorMode))
                {
                    if (postScriptColorMode != "AUTO")
                    {
                        string searchString = "";
                        string replaceString = "";

                        if (postScriptColorMode == "BW")
                        {
                            searchString = "<< /ProcessColorModel /DeviceCMYK >> setpagedevice} stopped cleartomark";
                            replaceString = "<< /ProcessColorModel /DeviceGray >> setpagedevice} stopped cleartomark";
                        }
                        else
                        {
                            searchString = "<< /ProcessColorModel /DeviceGray >> setpagedevice} stopped cleartomark";
                            replaceString = "<< /ProcessColorModel /DeviceCMYK >> setpagedevice} stopped cleartomark";
                        }
                        isFinalFileCreated = ReplaceChunkByChunk(tempFile, searchString, replaceString, "", "", chunk, finalSettingsFile);
                    }
                    else
                    {
                        isDataAvailable = false;
                    }
                }
                else
                {
                    isDataAvailable = false;
                }

                if (!isDataAvailable || isFinalFileCreated == false)
                {
                    using (FileStream stream = new FileStream(tempFile, FileMode.Open, FileAccess.ReadWrite))
                    {
                        int byteFoundAt = 0;

                        int byteValue = 0;
                        string hexValue;
                        int numberofBytes = 0;
                        bool isInitialByteFound = false;

                        for (int byteIndex = 0; byteIndex < stream.Length; byteIndex++)
                        {
                            byteValue = stream.ReadByte();
                            hexValue = byteValue.ToString("X", CultureInfo.CurrentCulture);
                            numberofBytes++;
                            if (isInitialByteFound)
                            {
                                if (hexValue == "21")
                                {
                                    isInitialByteFound = false;
                                    break;
                                }
                                else
                                {
                                    isInitialByteFound = false;
                                }
                            }
                            if (hexValue == "25")
                            {
                                byteFoundAt = numberofBytes - 1;
                                isInitialByteFound = true;
                            }
                        }

                        stream.Seek(byteFoundAt, SeekOrigin.Begin);

                        int length = 10240;
                        byte[] buffer = new byte[length];
                        int bytesRead = 0;
                        // write the required bytes
                        FileStream fs = new FileStream(finalSettingsFile, FileMode.Append);

                        do
                        {
                            bytesRead = stream.Read(buffer, 0, length);
                            fs.Write(buffer, 0, bytesRead);
                        }
                        while (bytesRead == length);

                        fs.Close();
                    }
                }

                //Delete temporary file
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
            }
            catch (Exception ex)
            { }
            return isFinalFileCreated;
        }

        #endregion

        #region :: Mac Driver ::
        /// <summary>
        /// Provides the mac converted data file.
        /// </summary>
        /// <param name="duplexDirection">The duplex direction.</param>
        /// <param name="physicalFilePath">The physical file path.</param>
        /// <param name="colorMode">The color mode.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="isCollate">if set to <c>true</c> [is collate].</param>
        /// <param name="macDefaultCopies">The mac default copies.</param>
        /// <param name="newCount">The new count.</param>
        /// <returns></returns>
        /// <remarks>
        /// Class Diagram:<br/>
        /// 	<img src="ClassDiagrams/SD_JobParser.JobParser.ProvideMacConvertedDataFile.jpg"/>
        /// </remarks>
        public static bool ProvideMacConvertedDataFile(string duplexDirection, string physicalFilePath, string colorMode, string offset, bool isCollate, int macDefaultCopies, int newCount, string finalSettingsFile)
        {
            bool isFinalFileCreated = true;
            string searchString = string.Empty;
            string replaceString = string.Empty;

            try
            {
                switch (duplexDirection)
                {
                    case "SIMPLEXDUPLEXBOOK":   //Simplex to Duplex BOOK
                        searchString = "/Duplex false";
                        replaceString = "/Duplex true /Tumble false";
                        //dataFile = searchReplaceString(physicalFilePath, searchString, replaceString, colorMode, offset, isCollate, macDefaultCopies, newCount);
                        isFinalFileCreated = ReplaceByChunk(physicalFilePath, searchString, replaceString, chunk, colorMode, offset, isCollate, macDefaultCopies, newCount, finalSettingsFile);
                        break;
                    case "SIMPLEXDUPLEXTABLET": //simplex to Duplex TABLET
                        searchString = "/Duplex false";
                        replaceString = "/Duplex true /Tumble true";
                        //dataFile = searchReplaceString(physicalFilePath, searchString, replaceString, colorMode, offset, isCollate, macDefaultCopies, newCount);
                        isFinalFileCreated = ReplaceByChunk(physicalFilePath, searchString, replaceString, chunk, colorMode, offset, isCollate, macDefaultCopies, newCount, finalSettingsFile);
                        break;
                    case "DUPLEXBOOKSIMPLEX":   //Duplex Book to Simplex
                        searchString = "/Duplex true /Tumble false";
                        replaceString = "/Duplex false";
                        //dataFile = searchReplaceString(physicalFilePath, searchString, replaceString, colorMode, offset, isCollate, macDefaultCopies, newCount);
                        isFinalFileCreated = ReplaceByChunk(physicalFilePath, searchString, replaceString, chunk, colorMode, offset, isCollate, macDefaultCopies, newCount, finalSettingsFile);
                        break;
                    case "DUPLEXTABLETSIMPLEX": //Duplex Tablet to Simplex
                        searchString = "/Duplex true /Tumble true";
                        replaceString = "/Duplex false";
                        //dataFile = searchReplaceString(physicalFilePath, searchString, replaceString, colorMode, offset, isCollate, macDefaultCopies, newCount);
                        isFinalFileCreated = ReplaceByChunk(physicalFilePath, searchString, replaceString, chunk, colorMode, offset, isCollate, macDefaultCopies, newCount, finalSettingsFile);
                        break;
                    case "DUPLEXDUPLEXTABLET":  //Duplex Booklet to Duplex Tablet
                        searchString = "/Duplex true /Tumble false";
                        replaceString = "/Duplex true /Tumble true";
                        //dataFile = searchReplaceString(physicalFilePath, searchString, replaceString, colorMode, offset, isCollate, macDefaultCopies, newCount);
                        isFinalFileCreated = ReplaceByChunk(physicalFilePath, searchString, replaceString, chunk, colorMode, offset, isCollate, macDefaultCopies, newCount, finalSettingsFile);
                        break;
                    case "DUPLEXDUPLEXBOOK"://Duplex Tablet to Duplex Booklet
                        searchString = "/Duplex true /Tumble true";
                        replaceString = "/Duplex true /Tumble false";
                        //dataFile = searchReplaceString(physicalFilePath, searchString, replaceString, colorMode, offset, isCollate, macDefaultCopies, newCount);
                        isFinalFileCreated = ReplaceByChunk(physicalFilePath, searchString, replaceString, chunk, colorMode, offset, isCollate, macDefaultCopies, newCount, finalSettingsFile);
                        break;
                    default:
                        //dataFile = searchReplaceString(physicalFilePath, searchString, replaceString, colorMode, offset, isCollate, macDefaultCopies, newCount);
                        isFinalFileCreated = ReplaceByChunk(physicalFilePath, searchString, replaceString, chunk, colorMode, offset, isCollate, macDefaultCopies, newCount, finalSettingsFile);
                        break;
                }
            }
            catch (Exception ex)
            {
                isFinalFileCreated = false;
            }
            return isFinalFileCreated;
        }

        /// <summary>
        /// Replaces the by chunk.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="searchString">The search string.</param>
        /// <param name="replaceString">The replace string.</param>
        /// <param name="chunkLength">Length of the chunk.</param>
        /// <param name="colorMode">The color mode.</param>
        /// <param name="offSet">The off set.</param>
        /// <param name="isCollate">if set to <c>true</c> [is collate].</param>
        /// <param name="defaultCopies">The default copies.</param>
        /// <param name="newCount">The new count.</param>
        /// <returns></returns>
        public static bool ReplaceByChunk(string filePath, string searchString, string replaceString, int chunkLength, string colorMode, string offSet, bool isCollate, int defaultCopies, int newCount, string finalSettingsFile)
        {
            bool isFinalFileCreated = true;
            try
            {
                int loadLength = chunkLength + searchString.Length;
                char[] buffer = new char[loadLength];
                int index = 0;
                int replaceLength = searchString.Length;
                string originalString = string.Empty;
                bool reaplceLineFound = false;
                string replaceColorString = "";
                bool colorModeFound = false;
                string beginFeatureString = "%%BeginFeature";

                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (sr.Peek() >= 0)
                    {
                        string readline = sr.ReadLine();
                        string[] inputSplit = readline.Split(":".ToCharArray());
                        if (reaplceLineFound)
                        {
                            originalString = readline;
                            break;
                        }
                        if (colorModeFound)
                        {
                            string[] splitSecond = readline.Split(" ".ToCharArray());
                            if (splitSecond[splitSecond.Length - 1] == "setcolormode")
                            {
                                reaplceLineFound = true;
                            }
                        }
                        else if (inputSplit[0] == beginFeatureString)
                        {
                            string[] splitSecond = inputSplit[1].Split(" ".ToCharArray());
                            // Split and search for *ARCMode, 
                            // If found next line will contain the Color Mode number
                            if (splitSecond[1] == "*ARCMode")
                            {
                                colorModeFound = true;
                            }
                        }
                    }
                }

                string tempFile = filePath + "DummyTemp";
                StreamReader streamReader = new StreamReader(filePath);
                //FileStream fs = new FileStream(finalSettingsFile, FileMode.Append);
                StreamWriter streamWriter = new StreamWriter(tempFile);

                try
                {
                    //Load the data according to the chunk length specified
                    //also load the extra data according to the length of
                    //the replace text so that chunk end and start text break
                    //can be handled            
                    while (true)
                    {
                        //Seek to the position from where we want to start and
                        //load the data in to the memory
                        streamReader.DiscardBufferedData();
                        streamReader.BaseStream.Seek(index, SeekOrigin.Begin);
                        int count = streamReader.ReadBlock(buffer, 0, loadLength);
                        if (count == 0) break;

                        //Get the data and check whether the extra loaded data
                        //is replaceable if yes then set end replaced flag                
                        string data = ConvertToString(buffer, count);
                        bool isEndReplaced = false;
                        if (count >= chunkLength)
                        {
                            isEndReplaced = (data.LastIndexOf(replaceString, chunkLength) > 0);
                        }

                        //Replace the data with the specified data and save the
                        //new data in the new file
                        // Check For Duplex Change
                        #region :Duplex:
                        if (!string.IsNullOrEmpty(searchString))
                        {
                            data = data.Replace(searchString, replaceString);
                        }
                        #endregion

                        #region :Color Mode:
                        if (!string.IsNullOrEmpty(colorMode) && colorMode != "AUTO")
                        {
                            if (colorMode == "BW")
                            {
                                replaceColorString = "<</ProcessColorModel /DeviceGray>> setpagedevice";
                            }
                            else if (colorMode == "COLOR")
                            {
                                replaceColorString = "<</ProcessColorModel /DeviceCMYK>> setpagedevice";
                            }
                            data = data.Replace(originalString, replaceColorString);
                        }
                        #endregion

                        #region :Copies Change:
                        string copiesSearchString = "";
                        string copiesReplaceString = "";
                        bool isCopiesChanged = true;

                        if (defaultCopies <= 1 && newCount >= 2)
                        {
                            copiesSearchString = "% x y w h ESPrc - Clip to a rectangle.";
                            copiesReplaceString = newCount + "/languagelevel where{pop languagelevel 2 ge}{false}ifelse {1 dict begin/NumCopies exch def currentdict end setpagedevice} {userdict/#copies 3 -1 roll put}ifelse";
                        }
                        else if (defaultCopies >= 2 && newCount <= 1)
                        {
                            copiesSearchString = defaultCopies + "/languagelevel where{pop languagelevel 2 ge}{false}ifelse";
                            copiesReplaceString = newCount + "/languagelevel where{pop languagelevel 2 ge}{false}ifelse";
                        }
                        else if (newCount >= 2)
                        {
                            copiesSearchString = defaultCopies + "/languagelevel where{pop languagelevel 2 ge}{false}ifelse";
                            copiesReplaceString = newCount + "/languagelevel where{pop languagelevel 2 ge}{false}ifelse";
                        }
                        else
                        {
                            isCopiesChanged = false;
                        }

                        if (isCopiesChanged)
                        {
                            data = data.Replace(copiesSearchString, copiesReplaceString);
                        }
                        #endregion

                        #region :Offset:
                        if (!string.IsNullOrEmpty(offSet))
                        {
                            string offSetSearchString = "";
                            string offSetReplaceString = "";

                            if (offSet == "ON")
                            {
                                offSetSearchString = "<</JobOffset 0>> setpagedevice";
                                offSetReplaceString = "<</JobOffset 1>> setpagedevice";
                            }
                            else
                            {
                                offSetSearchString = "<</JobOffset 1>> setpagedevice";
                                offSetReplaceString = "<</JobOffset 0>> setpagedevice";
                            }
                            data = data.Replace(offSetSearchString, offSetReplaceString);
                        }
                        #endregion

                        #region :Collate:

                        string collateSearchString = "";
                        string collateReplaceString = "";

                        if (isCollate)
                        {
                            collateSearchString = "<</Collate false>> setpagedevice";
                            collateReplaceString = "<</Collate true>> setpagedevice";
                        }
                        else
                        {
                            collateSearchString = "<</Collate true>> setpagedevice";
                            collateReplaceString = "<</Collate false>> setpagedevice";
                        }
                        data = data.Replace(collateSearchString, collateReplaceString);

                        #endregion

                        if (isEndReplaced)
                        {
                            streamWriter.Write(data);
                            index += count;
                        }
                        else
                        {
                            if (count >= chunkLength)
                            {
                                streamWriter.Write(data.Substring(0, data.Length - replaceLength));
                                index += chunkLength;
                            }
                            else
                            {
                                streamWriter.Write(data);
                                index += chunkLength;
                            }
                        }
                    }


                }
                catch (Exception ex)
                {
                    isFinalFileCreated = false;
                }


                streamReader.Close();
                streamWriter.Close();

                int read = 0;
                byte[] bufferTemp = new byte[4096];

                using (FileStream writeStream = new FileStream(finalSettingsFile, FileMode.Append))
                {
                    using (FileStream readStream = new FileStream(tempFile, FileMode.Open, FileAccess.Read))
                    {
                        int byteFoundAt = 0;

                        int byteValue = 0;
                        string hexValue;
                        int numberofBytes = 0;
                        bool isInitialByteFound = false;

                        for (int byteIndex = 0; byteIndex < readStream.Length; byteIndex++)
                        {
                            byteValue = readStream.ReadByte();
                            hexValue = byteValue.ToString("X", CultureInfo.CurrentCulture);
                            numberofBytes++;
                            if (isInitialByteFound)
                            {
                                if (hexValue == "21")
                                {
                                    isInitialByteFound = false;
                                    break;
                                }
                                else
                                {
                                    isInitialByteFound = false;
                                }
                            }
                            if (hexValue == "25")
                            {
                                byteFoundAt = numberofBytes - 1;
                                isInitialByteFound = true;
                            }
                        }

                        readStream.Seek(byteFoundAt, SeekOrigin.Begin);


                        while ((read = readStream.Read(bufferTemp, 0, bufferTemp.Length)) > 0)
                        {
                            writeStream.Write(bufferTemp, 0, read);
                        }
                    }
                    if (File.Exists(tempFile))
                    {
                        File.Delete(tempFile);
                    }
                }
            }
            catch (Exception ex)
            { }
            return isFinalFileCreated;
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        private static string ConvertToString(char[] buffer, int count)
        {
            StringBuilder data = new StringBuilder(buffer.Length);
            try
            {
                for (int i = 0; i < count; i++)
                {
                    data.Append(buffer[i]);
                }
            }
            catch (Exception ex)
            { }
            return data.ToString();
        }
        #endregion

        /// <summary>
        /// Writes the binary file.
        /// </summary>
        /// <param name="arrayData">The array data.</param>
        /// <returns></returns>
        /// <remarks>
        /// Class Diagram:<br/>
        /// 	<img src="ClassDiagrams/SD_JobParser.JobParser.ConvertToByte.jpg"/>
        /// </remarks>
        private static byte[] ConvertToByte(ArrayList arrayData)
        {
            byte[] returnByteData = new byte[arrayData.Count];
            try
            {
                for (int i = 0; i < arrayData.Count; i++)
                {
                    try
                    {
                        string listByte = arrayData[i].ToString();
                        returnByteData[i] = Convert.ToByte(listByte, 16);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch (Exception ex)
            { }
            return returnByteData;
        }

        /// <summary>
        /// Gets the file location.
        /// </summary>
        /// <param name="physicalFilePath">The physical file path.</param>
        /// <returns></returns>
        /// <remarks>
        /// Class Diagram:<br/>
        /// 	<img src="ClassDiagrams/SD_JobParser.JobParser.GetFileLocation.jpg"/>
        /// </remarks>
        private static string GetFileLocation(string physicalFilePath)
        {
            string[] fileLocation = physicalFilePath.Split('\\');
            string location = string.Empty;
            try
            {
                for (int i = 0; i < fileLocation.Length - 1; i++)
                {
                    if (string.IsNullOrEmpty(location))
                    {
                        location = fileLocation[i].ToString();
                    }
                    else
                    {
                        location = location + "\\" + fileLocation[i].ToString();
                    }
                }
            }
            catch (Exception ex)
            { }
            return location;
        }
    }
}
