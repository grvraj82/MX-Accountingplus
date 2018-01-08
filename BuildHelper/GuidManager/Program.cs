using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace GuidManager
{
    class Program
    {
        private static bool isUpdateVersion = false;

        static void Main(string[] args)
        {

            string versionFile = args[0];
            string ismFile = args[1];

            if (File.Exists(versionFile))
            {
                string versionStr = GetNextVersion(versionFile);

                //string ISMFile = @"E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Packaging\MXAccountingPlus.ism";
                // Update ISM File
                UpdateISM(versionStr, ismFile);

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
                        int nextVersionNumber = int.Parse(nextVersionArray[2]) + 1;

                        nextVersion = nextVersionArray[0] + "." + nextVersionArray[1] + "." + nextVersionNumber.ToString() + "." + nextVersionArray[3];

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
   
    }
}
