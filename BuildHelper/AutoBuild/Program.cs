using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Net.Mail;
using System.Configuration;

namespace AutoBuild
{
    class Program
    {
        static string VersionFile = string.Empty;
        static string StartVersion = ConfigurationManager.AppSettings["StartVersion"];
        static string SourceBuildPath = string.Empty;
        static string DestinationBuildPath = string.Empty;
        static string BuildType = string.Empty;
        static void Main(string[] args)
        {
            Console.WriteLine("Copying Build........");
            VersionFile = args[0];
            SourceBuildPath = args[1];
            DestinationBuildPath = args[2];
            BuildType = args[3];

            ArrayList Files = new ArrayList();
            Files.Add("E:\\Projects\\MXAccountingPlusCode\\MXAccountingPlusLog\\MXAccountingWeb.err");
            Files.Add("E:\\Projects\\MXAccountingPlusCode\\MXAccountingPlusLog\\PrintDataProvider.err");
            Files.Add("E:\\Projects\\MXAccountingPlusCode\\MXAccountingPlusLog\\MXAccountingMfp.err");
            Files.Add("E:\\Projects\\MXAccountingPlusCode\\MXAccountingPlusLog\\AccountingPlusPrimaryJobListner.err");
            Files.Add("E:\\Projects\\MXAccountingPlusCode\\MXAccountingPlusLog\\AccountingPlusSecondaryJobListner.err");
            Files.Add("E:\\Projects\\MXAccountingPlusCode\\MXAccountingPlusLog\\AccountingPlusTeritiaryJobListner.err");
            Files.Add("E:\\Projects\\MXAccountingPlusCode\\MXAccountingPlusLog\\DatabaseBridge.err");
            Files.Add("E:\\Projects\\MXAccountingPlusCode\\MXAccountingPlusLog\\DataManager.err");
            Files.Add("E:\\Projects\\MXAccountingPlusCode\\MXAccountingPlusLog\\DataManagerMfp.err");
            Files.Add("E:\\Projects\\MXAccountingPlusCode\\MXAccountingPlusLog\\RegistrationAdapter.err");
            Files.Add("E:\\Projects\\MXAccountingPlusCode\\MXAccountingPlusLog\\LDAP.err");
            Files.Add("E:\\Projects\\MXAccountingPlusCode\\MXAccountingPlusLog\\ScreenCastClient.err");
            Files.Add("E:\\Projects\\MXAccountingPlusCode\\MXAccountingPlusLog\\JobParser.err");
            Files.Add("E:\\Projects\\MXAccountingPlusCode\\MXAccountingPlusLog\\JobTransmitter.err");
            Files.Add("E:\\Projects\\MXAccountingPlusCode\\MXAccountingPlusLog\\JobDispatcher.err");
            Files.Add("E:\\Projects\\MXAccountingPlusCode\\MXAccountingPlusLog\\EventSimulator.err");
            Files.Add("E:\\Projects\\MXAccountingPlusCode\\MXAccountingPlusLog\\AccountingPlusPrimaryJobReleaser.err");
            Files.Add("E:\\Projects\\MXAccountingPlusCode\\MXAccountingPlusLog\\AccountingPlusSecondaryJobReleaser.err");
            Files.Add("E:\\Projects\\MXAccountingPlusCode\\MXAccountingPlusLog\\AccountingPlusTeritiaryJobReleaser.err");

            long ErrorCount = GetErrorCount(Files);
            bool buildFailed = false;
            if (ErrorCount > 0)
                buildFailed = true;

            if (!File.Exists(VersionFile))
            {
                writeFile(VersionFile, StartVersion);
                string NewVersion = StartVersion;
                if (buildFailed)
                {
                    NewVersion = NewVersion + "_F";
                }

                if (BuildType == "NFR")
                {
                    Console.WriteLine("Creating NFR Build....");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.ResetColor();

                    NewVersion += "_NFR";
                }

                if (!Directory.Exists(Path.Combine(DestinationBuildPath, NewVersion)))
                {
                    Directory.CreateDirectory(Path.Combine(DestinationBuildPath, NewVersion));
                }

                string LogPath = NewVersion + "\\BuildLog";

                if (buildFailed)
                {
                    CopyDirectory(SourceBuildPath, Path.Combine(DestinationBuildPath, NewVersion));
                    if (!Directory.Exists(Path.Combine(DestinationBuildPath, LogPath)))
                    {
                        Directory.CreateDirectory(Path.Combine(DestinationBuildPath, LogPath));
                    }
                    CopyDirectory("E:\\Projects\\MXAccountingPlusCode\\MXAccountingPlusLog", Path.Combine(DestinationBuildPath, LogPath));
                }
                SendBuildMail(buildFailed, NewVersion);
                string destinationDocuments = Path.Combine(DestinationBuildPath, NewVersion);
                CopyDocuments(destinationDocuments);
            }
            else
            {
                string OldVersion = ReadFile(VersionFile);
                string[] VNo = OldVersion.Split(".".ToCharArray());
                int lastNo = Convert.ToInt32(VNo[2].ToString());
                //lastNo = lastNo + 1;
                string NewVersion = VNo[0].ToString() + "." + VNo[1].ToString() + "." + lastNo.ToString() + "." + VNo[3].ToString();
                //writeFile(VersionFile, NewVersion);
                if (buildFailed)
                    NewVersion = NewVersion + "_F";
                if (BuildType == "NFR")
                {
                    Console.WriteLine("Creating NFR Build....");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.ResetColor();

                    NewVersion += "_NFR";
                }
                if (!Directory.Exists(Path.Combine(DestinationBuildPath, NewVersion)))
                    Directory.CreateDirectory(Path.Combine(DestinationBuildPath, NewVersion));

                CopyDirectory(SourceBuildPath, Path.Combine(DestinationBuildPath, NewVersion));
                if (buildFailed)
                {
                    string LogPath = NewVersion + "\\BuildLog";
                    if (!Directory.Exists(Path.Combine(DestinationBuildPath, LogPath)))
                        Directory.CreateDirectory(Path.Combine(DestinationBuildPath, LogPath));

                    CopyDirectory("E:\\Projects\\MXAccountingPlusCode\\MXAccountingPlusLog", Path.Combine(DestinationBuildPath, LogPath));
                }

                SendBuildMail(buildFailed, NewVersion);
                string destinationDocuments = Path.Combine(DestinationBuildPath, NewVersion);
                CopyDocuments(destinationDocuments);
            }
            Console.WriteLine("Copy Build filnished");
        }

        private static void CopyDocuments(string destinationDocuments)
        {
            string Current = Directory.GetCurrentDirectory();
            string[] splitRoot = Current.Split('\\');
            string projectDirectoryPath = string.Empty;
            for (int count = 0; count < splitRoot.Length; count++)
            {
                projectDirectoryPath += splitRoot[count] + "\\";
                if (splitRoot[count] == "MX-AccountingPlus")
                {
                    break;
                }
            }
            projectDirectoryPath += "Redist Documents";

            CopyFolder(projectDirectoryPath, destinationDocuments + "/Documents");
        }

        static public void CopyFolder(string sourceFolder, string destFolder)
        {
            if (destFolder[destFolder.Length - 1] != Path.DirectorySeparatorChar)
            {
                destFolder = destFolder + Path.DirectorySeparatorChar;
            }
            if (!Directory.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);
            }
            foreach (string str in Directory.GetFileSystemEntries(sourceFolder))
            {
                string pathFolder = Path.GetFileName(str);
                if (pathFolder != "CVS")
                {
                    if (Directory.Exists(sourceFolder))
                    {
                        string dst = destFolder + pathFolder;
                        CopyGuides(str, dst);
                    }
                    else
                    {
                        string extension = Path.GetExtension(str);
                        if (extension.ToLower() == ".pdf")
                        {
                            File.Copy(str, destFolder + pathFolder, true);
                        }
                    }
                }
            }
        }

        public static void CopyGuides(string Src, string Dst)
        {
            if (Dst[Dst.Length - 1] != Path.DirectorySeparatorChar)
            {
                Dst = Dst + Path.DirectorySeparatorChar;
            }
            if (!Directory.Exists(Dst))
            {
                Directory.CreateDirectory(Dst);
            }
            foreach (string str in Directory.GetFileSystemEntries(Src))
            {
                string pathFolder = Path.GetFileName(str);
                if (pathFolder != "CVS")
                {
                    if (Directory.Exists(str))
                    {
                        CopyGuides(str, Dst + pathFolder);
                    }
                    else
                    {
                        string extension = Path.GetExtension(str);
                        if (extension.ToLower() == ".pdf")
                        {
                            File.Copy(str, Dst + pathFolder, true);
                        }
                    }
                }
            }
        }


        private static long GetErrorCount(ArrayList Files)
        {
            long count = 0;
            foreach (string f in Files)
            {
                using (StreamReader r = new StreamReader(f))
                {
                    string line;
                    while ((line = r.ReadLine()) != null)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private static string ReadFile(string FilePath)
        {
            string value = string.Empty;
            TextReader tr = new StreamReader(FilePath);
            value = tr.ReadLine();
            tr.Close();
            return value;
        }
        private static void writeFile(string FilePath, string Value)
        {
            if (File.Exists(FilePath))
                File.Delete(FilePath);
            TextWriter tw = new StreamWriter(FilePath);
            tw.WriteLine(Value);
            tw.Close();
            Console.WriteLine(Value);
        }

        public static void CopyDirectory(string Src, string Dst)
        {
            if (Dst[Dst.Length - 1] != Path.DirectorySeparatorChar)
            {
                Dst = Dst + Path.DirectorySeparatorChar;
            }
            if (!Directory.Exists(Dst))
            {
                Directory.CreateDirectory(Dst);
            }
            foreach (string str in Directory.GetFileSystemEntries(Src))
            {
                if (Directory.Exists(str))
                {
                    CopyDirectory(str, Dst + Path.GetFileName(str));
                }
                else
                {
                    File.Copy(str, Dst + Path.GetFileName(str), true);
                }
            }
        }

        private static void SendBuildMail(bool isBuildFailed, string bildFolder)
        {
            string strMailTo = ConfigurationManager.AppSettings["mailTo"];
            string strMailFrom = ConfigurationManager.AppSettings["mailFrom"];
            string strMailCC = ConfigurationManager.AppSettings["MailCC"];

            MailMessage mail = new MailMessage();
            mail.To.Add(strMailTo);
            mail.CC.Add(strMailCC);
            mail.From = new MailAddress(strMailFrom);
            mail.Subject = "[AccountingPlus] Build# " + bildFolder + " " + DateTime.Now.ToString();
            if (isBuildFailed)
                mail.Body = "Hi All <br> AccountingPlus Build is failed.<br> Please find AccountingPlus build at the location : \\\\solutionsBuild\\AccountingPlusStandard\\" + bildFolder;
            else
                mail.Body = "Hi All <br> AccountingPlus Build is Sucessful.<br> Please find AccountingPlus build at the location : \\\\solutionsBuild\\AccountingPlusStandard\\" + bildFolder;
            try
            {
                mail.IsBodyHtml = true;
                SmtpClient Email = new SmtpClient();
                Email.Host = "172.29.240.82";
                Email.Port = 25;
                Email.Send(mail);

            }
            catch (Exception)
            {
            }
        }

    }
}
