using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using DataManager;
using System.Configuration;

namespace VersionManager
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    class AssemblyInfoUtil
    {
        private static int incParamNum = 0;
        private static string fileName = "";
        private static string versionStr = null;
        private static string versionFile = null;
        private static bool isVB = false;
        private static bool isUpdateVersion = false;
        private static bool isUpdateReadme = false;
        private static bool isUpdateSQLFile = false;
        private static bool isUpdateISMFile = false;
        private static string buildType = string.Empty;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].StartsWith("-inc:"))
                {
                    string s = args[i].Substring("-inc:".Length);
                    incParamNum = int.Parse(s);
                }
                else if (args[i].StartsWith("-set:"))
                {
                    versionStr = args[i].Substring("-set:".Length);
                }
                else if (args[i].StartsWith("-versionfile:"))
                {
                    versionFile = args[i].Substring("-versionfile:".Length);

                }
                else if (args[i].StartsWith("-incrementversion:"))
                {
                    isUpdateVersion = bool.Parse(args[i].Substring("-incrementversion:".Length));
                }
                else if (args[i].StartsWith("-updatereadme:"))
                {
                    isUpdateReadme = bool.Parse(args[i].Substring("-updatereadme:".Length));
                }
                else if (args[i].StartsWith("-updatesqlfile:"))
                {
                    isUpdateSQLFile = bool.Parse(args[i].Substring("-updatesqlfile:".Length));
                }
                else if (args[i].StartsWith("-updateismfile:"))
                {
                    isUpdateISMFile = bool.Parse(args[i].Substring("-updateismfile:".Length));
                }
                else if (args[i].StartsWith("-buildtype:"))
                {
                    buildType = args[i].Substring("-buildtype:".Length);
                }
                else
                    fileName = args[i];
            }

            if (Path.GetExtension(fileName).ToLower() == ".vb")
                isVB = true;

            if (fileName == "")
            {
                System.Console.WriteLine("Usage: AssemblyInfoUtil <path to AssemblyInfo.cs or AssemblyInfo.vb file> [options]");
                System.Console.WriteLine("Options: ");
                System.Console.WriteLine("  -set:<new version number> - set new version number (in NN.NN.NN.NN format)");
                System.Console.WriteLine("  -versionfile:<Version File Path> - Version File Path");
                System.Console.WriteLine("  -incrementversion:<true/false> - true/false");
                System.Console.WriteLine("  -inc:<parameter index>  - increases the parameter with specified index (can be from 1 to 4)");
                return;
                // VersionManager.exe "E:\Projects\PrintRoverCode\PrintRover\PrintRover\DataManager\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\PrintRoverCode\PrintRover\PrintRover\Build.version" "-incrementversion:true" 
            }

            if (!File.Exists(fileName))
            {
                System.Console.WriteLine("Error: Can not find file \"" + fileName + "\"");
                return;
            }
            if (File.Exists(versionFile))
            {
                versionStr = GetNextVersion(versionFile);

                if (isUpdateReadme)
                {
                    try
                    {
                        string readMe = @"E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\ReadMe\Readme.txt";
                        MassFindAndReplace replace = new MassFindAndReplace();
                        replace.FindAndReplace(readMe, "{0}", versionStr);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Successfully Updated ReadMe");
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Failed to Update ReadMe");
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                }

                if (isUpdateISMFile)
                {
                    try
                    {
                        string ISMFile = @"E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Packaging\MXAccountingPlus.ism";
                        // Update ISM File
                        UpdateISM(versionStr, ISMFile);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Successfully ISM File");
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Failed to Update ISM File");
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                }



                // Update SQL File with Version
                if (isUpdateSQLFile)
                {
                    try
                    {
                        string sqlInstallFile = @"E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\SQLscripts\INSTALL_05_UpdateVersion.sql";
                        string sqlUpgradeFile = @"E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\SQLscripts\UPGRADE_05_UpdateVersion.sql";

                        MassFindAndReplace replace = new MassFindAndReplace();

                        replace.FindAndReplace(sqlInstallFile, "{0}", versionStr);
                        replace.FindAndReplace(sqlUpgradeFile, "{0}", versionStr);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Successfully Updated SQL Files");
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Failed to Update SQL Files");
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                }


                // update BuildType  Trial/NFR
                
                if(!string.IsNullOrEmpty(buildType))
                {
                    //buildType = "NFR"; // Just uncomment while releasing NFR build
                    string buildText = "UNREGISTERED";
                    if(buildType == "NFR")
                    {
                        buildText = "NOT FOR RESALE";
                    }

                    try
                    {
                        string sqlInstallFile = @"E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\SQLscripts\INSTALL_06_BuildType.sql";
                        string sqlUpgradeFile = @"E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\SQLscripts\UPGRADE_06_BuildType.sql";

                        MassFindAndReplace replace = new MassFindAndReplace();

                        replace.FindAndReplace(sqlInstallFile, "{0}", buildText);
                        replace.FindAndReplace(sqlUpgradeFile, "{0}", buildText);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Successfully Updated SQL Files (Build Type)");
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Failed to Update SQL Files (Build Type)");
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }

                }
            }
            System.Console.Write("Processing \"" + fileName + "\"...");
            StreamReader reader = new StreamReader(fileName);
            StreamWriter writer = new StreamWriter(fileName + ".out");
            String line;

            while ((line = reader.ReadLine()) != null)
            {
                line = ProcessLine(line);
                writer.WriteLine(line);
            }
            reader.Close();
            writer.Close();

            File.Delete(fileName);
            File.Move(fileName + ".out", fileName);
            System.Console.WriteLine("Done!");
        }

        public static void UpdateISM(string versionNumber, string ISMFilePath)
        {

            // For Installer version and product code change.
            try
            {
                Version buildVer = new Version(versionNumber);

                Guid guid = Guid.NewGuid();
                string NewGuid = "{";
                NewGuid = NewGuid + guid.ToString() + "}";
                NewGuid = NewGuid.ToUpper();

                string Path = ISMFilePath;

                // For Changing ProductCode
                StreamReader PdCodeReader = new StreamReader(Path);
                string PdCodeContents = PdCodeReader.ReadToEnd();
                PdCodeReader.Close();

                string ProductCode = string.Format("<row><td>ProductCode</td><td>{0}</td><td/></row>", NewGuid);
                string ProductCodeChange = Regex.Replace(PdCodeContents, @"<row><td>ProductCode</td><td>.*</td><td/></row>", ProductCode);

                StreamWriter PdCodeWriter = new StreamWriter(Path, false);
                PdCodeWriter.Write(ProductCodeChange);
                PdCodeWriter.Close();
                // For Changing ProductVersion
                StreamReader PdVersionReader = new StreamReader(Path);
                string PdVersionContents = PdVersionReader.ReadToEnd();
                PdVersionReader.Close();

                string ProductVersion = string.Format("<row><td>ProductVersion</td><td>{0}.{1}.{2}.{3}</td><td/></row>", buildVer.Major, buildVer.Minor, buildVer.Build, 0);
                string ProductVersionChange = Regex.Replace(PdVersionContents, @"<row><td>ProductVersion</td><td>.*</td><td/></row>", ProductVersion);

                StreamWriter PdVersionWriter = new StreamWriter(Path, false);
                PdVersionWriter.Write(ProductVersionChange);
                PdVersionWriter.Close();

                // For Changing Registry Version
                StreamReader RegistryVersionReader = new StreamReader(Path);
                string RegContents = RegistryVersionReader.ReadToEnd();
                RegistryVersionReader.Close();

                string RegistryVersion = string.Format(@"<td>Version</td><td>{0}.{1}.{2}.{3}</td><td>ISRegistryComponent</td><td>0</td></row>", buildVer.Major, buildVer.Minor, buildVer.Build, 0);
                string RegistryVersionChange = Regex.Replace(RegContents, "<td>Version</td><td>.*</td><td>ISRegistryComponent</td><td>0</td></row>", RegistryVersion);

                StreamWriter RegistryVersionWriter = new StreamWriter(Path, false);
                RegistryVersionWriter.Write(RegistryVersionChange);
                RegistryVersionWriter.Close();

            }
            catch (System.UnauthorizedAccessException unAuthEx)
            {

            }
        }

        private static string GetNextVersion(string versionFile)
        {
            string nextVersion = string.Empty;
            if (File.Exists(versionFile))
            {
                TextReader textReader = new StreamReader(versionFile);
                nextVersion = textReader.ReadLine();
                textReader.Close();

                if (!string.IsNullOrEmpty(nextVersion))
                {
                    if (isUpdateVersion)
                    {
                        string[] nextVersionArray = nextVersion.Split(".".ToCharArray());
                        string buildVersion = ConfigurationManager.AppSettings["BuildVersion"];
                       
                        switch (buildVersion.ToUpper())
                        {
                            case "MAJOR":
                                nextVersion = string.Format("{0}.{1}.{2}.{3}", int.Parse(nextVersionArray[0]) + 1, nextVersionArray[1], nextVersionArray[2], nextVersionArray[3]);
                                break;
                            case "MINOR":
                                nextVersion = string.Format("{0}.{1}.{2}.{3}", nextVersionArray[0], int.Parse(nextVersionArray[1]) + 1, nextVersionArray[2], nextVersionArray[3]);
                                break;
                            case "BUILD":
                                nextVersion = string.Format("{0}.{1}.{2}.{3}", nextVersionArray[0], nextVersionArray[1], int.Parse(nextVersionArray[2]) + 1, nextVersionArray[3]);
                                break;
                            case "PATCH":
                                nextVersion = string.Format("{0}.{1}.{2}.{3}", nextVersionArray[0], nextVersionArray[1], nextVersionArray[2], int.Parse(nextVersionArray[3]) + 1);
                                break;

                            default:
                                nextVersion = string.Format("{0}.{1}.{2}.{3}", nextVersionArray[0], nextVersionArray[1], int.Parse(nextVersionArray[2]) + 1, nextVersionArray[3]);
                                break;
                        }
                                                

                        if (File.Exists(versionFile))
                        {
                            File.Delete(versionFile);
                            TextWriter textWriter = new StreamWriter(versionFile);
                            textWriter.WriteLine(nextVersion);
                            textWriter.Close();
                            Console.WriteLine(nextVersion);
                        }

                    }
                }
            }
            
            return nextVersion;
        }

        private static string ProcessLine(string line)
        {
            if (isVB)
            {
                line = ProcessLinePart(line, "<Assembly: AssemblyVersion(\"");
                line = ProcessLinePart(line, "<Assembly: AssemblyFileVersion(\"");
            }
            else
            {
                line = ProcessLinePart(line, "[assembly: AssemblyVersion(\"");
                line = ProcessLinePart(line, "[assembly: AssemblyFileVersion(\"");
                line = ProcessLinePart(line, "[assembly: Guid(\"");
            }
            return line;
        }

        private static string ProcessLinePart(string line, string part)
        {
            int spos = line.IndexOf(part);
            if (spos >= 0)
            {
                spos += part.Length;
                int epos = line.IndexOf('"', spos);
                string oldVersion = line.Substring(spos, epos - spos);
                string newVersion = "";
                bool performChange = false;

                if (incParamNum > 0)
                {
                    string[] nums = oldVersion.Split('.');
                    if (nums.Length >= incParamNum && nums[incParamNum - 1] != "*")
                    {
                        Int64 val = Int64.Parse(nums[incParamNum - 1]);
                        val++;
                        nums[incParamNum - 1] = val.ToString();
                        newVersion = nums[0];
                        for (int i = 1; i < nums.Length; i++)
                        {
                            newVersion += "." + nums[i];
                        }
                        performChange = true;
                    }

                }
                else if (versionStr != null)
                {
                    newVersion = versionStr;
                    performChange = true;
                }

                if (performChange)
                {
                    StringBuilder str = new StringBuilder(line);
                    str.Remove(spos, epos - spos);
                    str.Insert(spos, newVersion);
                    line = str.ToString();
                }

                if (part == "[assembly: Guid(\"")
                {
                    StringBuilder strGuid = new StringBuilder(line);
                    string guID = Guid.NewGuid().ToString();
                    spos = line.IndexOf(part);
                    spos += part.Length;
                    epos = line.IndexOf('"', spos);

                    strGuid.Remove(spos, epos - spos);
                    strGuid.Insert(spos, guID);
                    line = strGuid.ToString();
                }
            }
            return line;
        }

    }
}
