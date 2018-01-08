using System;
using System.IO;
using System.Text;

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

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			for (int i = 0; i < args.Length; i++) {
				if (args[i].StartsWith("-inc:")) {
					string s = args[i].Substring("-inc:".Length);
					incParamNum = int.Parse(s);
				}
				else if (args[i].StartsWith("-set:")) {
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
				else
					fileName = args[i];
			}

			if (Path.GetExtension(fileName).ToLower() == ".vb")
				isVB = true;

			if (fileName == "") {
				System.Console.WriteLine("Usage: AssemblyInfoUtil <path to AssemblyInfo.cs or AssemblyInfo.vb file> [options]");
				System.Console.WriteLine("Options: ");
				System.Console.WriteLine("  -set:<new version number> - set new version number (in NN.NN.NN.NN format)");
                System.Console.WriteLine("  -versionfile:<Version File Path> - Version File Path");
                System.Console.WriteLine("  -incrementversion:<true/false> - true/false");
                System.Console.WriteLine("  -inc:<parameter index>  - increases the parameter with specified index (can be from 1 to 4)");
				return;
                // VersionManager.exe "E:\Projects\PrintRoverCode\PrintRover\PrintRover\DataManager\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\PrintRoverCode\PrintRover\PrintRover\Build.version" "-incrementversion:true" 
			}

			if (!File.Exists(fileName)) {
				System.Console.WriteLine("Error: Can not find file \"" + fileName + "\"");
				return;
			}
            if (File.Exists(versionFile))
            {
                versionStr = GetNextVersion(versionFile);
            }
			System.Console.Write("Processing \"" + fileName + "\"...");
			StreamReader reader = new StreamReader(fileName);
            StreamWriter writer = new StreamWriter(fileName + ".out");
			String line;

			while ((line = reader.ReadLine()) != null) {
				line = ProcessLine(line);
                
				writer.WriteLine(line);
			}
			reader.Close();
			writer.Close();

			File.Delete(fileName);
			File.Move(fileName + ".out", fileName);
			System.Console.WriteLine("Done!");
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


		private static string ProcessLine(string line) {
			if (isVB) {
				line = ProcessLinePart(line, "<Assembly: AssemblyVersion(\"");
				line = ProcessLinePart(line, "<Assembly: AssemblyFileVersion(\"");
			} 
			else {
				line = ProcessLinePart(line, "[assembly: AssemblyVersion(\"");
				line = ProcessLinePart(line, "[assembly: AssemblyFileVersion(\"");
                line = ProcessLinePart(line, "[assembly: Guid(\"");

                //updateGuid(line);
			}
			return line;
		}

        private static void updateGuid(string line)
        {
            
        }

		private static string ProcessLinePart(string line, string part) {
			int spos = line.IndexOf(part);
			if (spos >= 0) {
				spos += part.Length;
				int epos = line.IndexOf('"', spos);
				string oldVersion = line.Substring(spos, epos - spos);
				string newVersion = "";
				bool performChange = false;

				if (incParamNum > 0) {
					string[] nums = oldVersion.Split('.');
					if (nums.Length >= incParamNum && nums[incParamNum - 1] != "*") {
						Int64 val = Int64.Parse(nums[incParamNum - 1]);
						val++;
						nums[incParamNum - 1] = val.ToString();
						newVersion = nums[0]; 
						for (int i = 1; i < nums.Length; i++) {
							newVersion += "." + nums[i];
						}
						performChange = true;
					}

				}
				else if (versionStr != null) {
					newVersion = versionStr;
					performChange = true;
				}

				if (performChange) {
					StringBuilder str = new StringBuilder(line);
					str.Remove(spos, epos - spos);
					str.Insert(spos, newVersion);
					line = str.ToString();
				}

                if(part == "[assembly: Guid(\"")
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
