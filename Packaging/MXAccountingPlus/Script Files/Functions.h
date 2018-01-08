#define KEY_ALL_ACCESS 0xF003F
#define KEY_WOW64_64KEY 0x0100
#define IISREGPATH "SOFTWARE\\Microsoft\\InetStp"
#define IISVERSION "VersionString"    
#define DOTFRAMCHECK11 "SOFTWARE\\Microsoft\\.NETFramework\\policy\\v1.1"
#define DOTFRAMCHECK20 "SOFTWARE\\Microsoft\\.NETFramework\\policy\\v2.0"
#define FRAMEWORK11PATH  WindowsFolder ^ "Microsoft.NET\\Framework\\v1.1.4322"
#define FRAMEWORK20PATH  WindowsFolder ^ "Microsoft.NET\\Framework\\v2.0.50727"
#define FRAMEWORK20PATH64  WindowsFolder ^ "Microsoft.NET\\Framework64\\v2.0.50727"
#define MB_DEFBUTTON1               0x00000000
#define VIRNAME	"ExternalAccounting"     
#define CONUNINSTALLKEY "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\"  
#define DOTFRAMECHECK35 "SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v3.5"
#define DOTFRAMCHECK35 	"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\5.0\\User Agent\\Post Platform"
#define DOTFRAMECHECK40KEY86 "SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full"
#define DOTFRAMECHECK40KEY64 "SOFTWARE\\Wow6432Node\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full"
#define DOTFRAMEWORKVERSION 				3
#define DOTFRAMEWORKMINORVERSION            5
#define EVENTLOG_ERROR_TYPE 				1                                        
#define FRAMEWORK20PATH			WindowsFolder ^ "Microsoft.NET\\Framework\\v2.0.50727"        
#define FRAMEWORK40PATH			WindowsFolder ^ "Microsoft.NET\\Framework\\v4.0.30319"
#define FRAMEWORK20PATH64  		WindowsFolder ^ "Microsoft.NET\\Framework64\\v2.0.50727"
#define FRAMEWORK40PATH64  		WindowsFolder ^ "Microsoft.NET\\Framework64\\v4.0.30319"
#define FRAMEWORK35PATH			WindowsFolder ^ "Microsoft.NET\\Framework\\v3.5"       
#define AP_REGKEY_64BIT "SOFTWARE\\Wow6432Node\\Sharp\\AccountingPlus"
#define AP_REGKEY_32BIT "SOFTWARE\\Sharp\\AccountingPlus"    
#define REG_AP_BUILD_64 "SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall"
#define REG_AP_BUILD_32 "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall"   
#define DOTFRAMECHECK35 "SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v3.5"
#define OSQL_PATH_2012	"Microsoft SQL Server\\110\\tools\\binn\\sqlcmd.exe" 
#define SQL_INSTALLED_PATH_32 "SOFTWARE\\Microsoft\\Microsoft SQL Server"
#define SQL_INSTALLED_PATH_64 "SOFTWARE\\Wow6432Node\\Microsoft\\Microsoft SQL Server"
#define ID_REG_PATH_SQL2008R2 "SOFTWARE\\Microsoft\\Microsoft SQL Server\\100\\Bootstrap"
#define ID_REG_PATH64_SQL2008R2 "SOFTWARE\\Wow6432Node\\Microsoft\\Microsoft SQL Server\\100\\Bootstrap"
#define ID_REG_SQL_PATH_SQL2008R2 "SOFTWARE\\Microsoft\\Microsoft SQL Server"
#define ID_REG_SQL_PATH64_SQL2008R2 "SOFTWARE\\Wow6432Node\\Microsoft\\Microsoft SQL Server"
#define DBINSTANCE_REGKEY_32 "SOFTWARE\\Microsoft\\Microsoft SQL Server\\Instance Names\\SQL"
#define DBINSTANCE_REGKEY_64 "SOFTWARE\\Wow6432Node\\Microsoft\\Microsoft SQL Server\\Instance Names\\SQL"
#define SQL_DB_INSTANCE "ISPSERVER"






prototype BOOL IsASPNETVersion(BYVAL STRING,//File extension to search
								BYVAL STRING //Version to check
								);       
prototype void SetWebServiceExtension();  
prototype SetEnable32bitapponwin64();  
export prototype SetFilesNormal(	BYVAL STRING,//Directory path
									BYVAL STRING,//File type
									BYVAL NUMBER //Whether to include folders or not
									);   
  						   
prototype void CopyDocuments();	
prototype BOOL CheckNetFrameWork35();  
prototype BOOL CheckNetFrameWork40();  
prototype advapi32.RegOpenKeyExA( BYVAL LONG, BYVAL STRING, BYVAL LONG, BYVAL LONG, BYREF LONG);
prototype advapi32.RegCloseKeyA( BYVAL LONG );
prototype advapi32.RegQueryValueExA( BYVAL LONG, BYVAL STRING, BYVAL LONG, BYREF NUMBER, BYREF NUMBER, BYREF NUMBER);
prototype void CheckUpgrade();				 /// This function is used to check for the Build upgradation.
prototype void UpgradeProcess();             /// This Function is used to uninstall the existing lower build while upgrading. 
prototype string GetProductCode(BYVAL STRING);
prototype void WriteToLogFile(BYVAL STRING,BYVAL STRING);
prototype WriteEventLog(HWND, BYVAL STRING, BYVAL NUMBER);     
prototype void CreateRegistryEntries(HWND);
prototype writelinefile(STRING,NUMBER);
prototype string GenereteIssFile(BYVAL STRING);
prototype HWND advapi32.RegisterEventSourceA(BYVAL STRING, BYVAL STRING);
prototype BOOL advapi32.DeregisterEventSource(HWND);
prototype BOOL advapi32.ReportEventA(HWND, NUMBER, NUMBER, NUMBER, POINTER, NUMBER, NUMBER, POINTER, POINTER); 


prototype BOOL CheckOSVersion()  ;          
prototype BOOL CheckIIS(); 
prototype BOOL CheckSQLServerExpress();
prototype BOOL CheckAdminUser();
prototype BOOL CheckNetFrameWork20();   
prototype STRING GetProperty(HWND , BYVAL STRING);
prototype CheckPrerequisites();  
prototype CheckWebPrerequisites();
prototype BOOL VirDirectoryExists(STRING);  
prototype BOOL CheckNetFrameWork();								
prototype BOOL IIS6BackWardCompatability(); 
prototype BOOL User32.SetDlgItemText( HWND, INT, BYVAL STRING ); 
prototype INT Shell32.ShellExecuteA( HWND, BYREF STRING, BYREF STRING, BYVAL STRING, BYVAL STRING, INT );
// To replace the string in the file
prototype FindAndReplace(BYVAL STRING,//File path
								BYVAL STRING,//Search String
								BYVAL  STRING//Replace string
								);   
prototype OutputDebug(HWND,BYVAL STRING,BYVAL STRING);
prototype BOOL CheckDiskSpace(); 
prototype BOOL CheckProcesorSpeed();
prototype BOOL CheckSystemRam();
prototype STRING GetUIDFromSID(BYVAL STRING //Security Indentifier 
);
prototype SetNTFSAPermission(STRING,STRING); 
prototype ExecuteFile( BYREF STRING, BYREF STRING, BYVAL STRING, BYVAL STRING, INT );     
								
export prototype logMessage(BYVAL STRING,//Message type
							BYVAL STRING//Message Information
							);   
							
prototype kernel32.OutputDebugString (byval string);
prototype BOOL IsDoNotContainSpecialFolder(HWND,
								BYVAL STRING //Path
								,BYVAL STRING // Special Folders
								,BYVAL STRING //Delimeter		
								);
export prototype BrowseForFolder(	HWND,//Handle to the dialog
									BYVAL STRING,//Title
									BYREF STRING//Message
									);
typedef  BrowseInfo
  begin
	  long       hWndOwner;
	  long       pIDLRoot  ;
	  long       pszDisplayName ;
	  long       lpszTitle      ;
	  long       ulFlags        ;
	  long       lpfnCallback   ;
	  long       lParam         ;
	  long       iImage         ;
  end; 
export prototype LONG shell32.SHBrowseForFolder (	LONG //Pointer to a BROWSEINFO structure that 
														 //contains information used to display the dialog box.
													) ;
export prototype LONG shell32.SHGetPathFromIDList (	BYVAL LONG,//Pointer to an item identifier list 
															   //that specifies a file or directory location relative 
															   //to the root of the name space (the desktop).
													BYREF STRING//Pointer to a buffer that receives the file system path. 
																//The size of this buffer is assumed to be MAX_PATH bytes.
													) ;
export prototype BOOL IsValidDrive(	BYVAL STRING,//Directory path
									BYREF STRING,//Message
									BYVAL NUMBER//Drive type
									);  
									
prototype void ConfigStringReplacement();   

//prototype void UpdateConfigString();                                       							
                                  
prototype void InstallServices();

prototype void StartServices();

prototype void StopServices();

prototype void UninstallServices();

prototype void SetFilePermission();									
									
prototype void BackUpFiles();
									
prototype void CopyBackUpFiles();

prototype void GetDBSettingsValues();      

//prototype void SetDBSettingsValues();
 
prototype void InstallAspnet_regiis(); 

prototype void SetAppPoolSettings();                      

prototype StrPiece (BYREF STRING, STRING, STRING, NUMBER);   

prototype void UpgradeconfigFile();
     
prototype NUMBER SelectServerLocation();        

prototype BOOL IsSQLServer2008R2Installed();

prototype BOOL IsInstanceExists();

prototype void InstallSQLServer();

prototype void UninstallSQLServer();

prototype BOOL  CheckNetFrameWork45();

prototype void  InstallNetFrameWork40();

prototype void  InstallNetFrameWork45();

prototype NUMBER SelectServerType();  
                                     
prototype BOOL CheckDBUserPermission(STRING, STRING, STRING);
						
prototype BOOL CheckSQLConnection(STRING, STRING, STRING, STRING, STRING);

prototype string SubStrReplaceinStr(BYVAL STRING, BYVAL STRING, BYVAL STRING);                							
