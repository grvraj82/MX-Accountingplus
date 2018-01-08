echo off

rem E:
rem cd E:\Projects\MXAccountingPlusCode\MX-AccountingPlus
rem ATTRIB +H "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\BuildHelper\AutoBuild"
rem FOR /D %%i IN ("E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\*") DO RD /S /Q "%%i" DEL /Q "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\*.*"
rem ATTRIB -H "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\BuildHelper\AutoBuild"

rem del "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\*.version"

rem del "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\*.sln"

E:
 cd E:\Projects\MXAccountingPlusCode\MX-AccountingPlus
 set CVSROOT=:sspi::drajshekhar@172.29.240.66:2401/solutions
 cvs update -P -d -C






REM C:
REM cd C:\Program Files\Microsoft SQL Server\90\Tools\Publishing\1.2
REM sqlpubwiz script -C "data source=LIVESOLUTIONS\SQLEXPRESS; initial catalog=PrintRover; uid=sa; pwd=ssdiblr; " C:\Source\MFP\PrintRelease\SQL\PrintRelease.SQL
REM sqlpubwiz script -C "data source=LIVESOLUTIONS\SQLEXPRESS; initial catalog=PrintReleaseTest; uid=sa; pwd=ssdiblr; " C:\Source\MFP\PrintRelease\SQL\PrintRelease.SQL



REM --Update Complited

C:
cd C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319

MSBuild.exe E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\BuildHelper\VersionManager\VersionManager.csproj   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\BuildHelper\VersionManager\VersionManager.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild

MSBuild.exe E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\BuildHelper\GuidManager\GuidManager.csproj   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\BuildHelper\GuidManager\GuidManager.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild


e:

cd "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\BuildHelper\GuidManager\bin\Release" 
echo "updating ISM......"
GuidManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Packaging\MXAccountingPlus.ism"


cd "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\BuildHelper\VersionManager\bin\Release" 

echo "updating version..."
VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\DatabaseBridge\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:true" "-updatereadme:true" "-updatesqlfile:true" "-updateismfile:true" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\AppLibrary\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" 
"-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\MFPDiscovery\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" "-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\DataManager\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" "-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\DataManagerMfp\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" "-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\RegistrationAdapter\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" "-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\AppLocalizer\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" "-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\OsaDirectEAManager\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" "-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\OsaDirectManager\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" "-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\ApplicationAuditor\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" "-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\ldap dll\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" "-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\MXAccountingWeb\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" "-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\PrintJobProvider\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" "-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\MXAccountingMfp\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" "-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\AccountingPlusPrimaryJobListner\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" "-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\AccountingPlusSecondaryJobListner\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" "-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\AccountingPlusTeritiaryJobListner\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" "-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\AccountingPlusPrimaryJobReleaser\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" "-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\AccountingPlusSecondaryJobReleaser\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" "-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\AccountingPlusTeritiaryJobReleaser\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" "-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\AccountingPlusWatcher\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" "-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\ScreenCastClient\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" "-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\JobParser\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" "-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\JobTransmitter\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" "-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\JobDispatcher\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" "-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\EventSimulator\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" "-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\AccountingPlusEmailExtractor\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" "-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\D.Net.EmailClient\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" "-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\D.Net.EmailInterfaces\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" "-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

VersionManager.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Net\Properties\AssemblyInfo.cs" "-set:", "-versionfile:E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version" "-incrementversion:false" "-updatereadme:false" "-updatesqlfile:false" "-updateismfile:false" "-buildtype:unregistered"

cho "version updated..."

 cd E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\
 set CVSROOT=:sspi::grvaradharaj@172.29.240.66:2401:/solutions

 cvs commit -m "Build version updated" Build.version
 cvs commit -m "Build version updated" "Packaging\MXAccountingPlus.ism"
 cvs commit -m "Build version updated" AppLibrary/Properties/AssemblyInfo.cs
 cvs commit -m "Build version updated" DatabaseBridge/Properties/AssemblyInfo.cs
 cvs commit -m "Build version updated" MFPDiscovery/Properties/AssemblyInfo.cs
 cvs commit -m "Build version updated" DataManager/Properties/AssemblyInfo.cs
 cvs commit -m "Build version updated" RegistrationAdapter/Properties/AssemblyInfo.cs
 cvs commit -m "Build version updated" DataManagerMfp/Properties/AssemblyInfo.cs
 cvs commit -m "Build version updated" AppLocalizer/Properties/AssemblyInfo.cs
 cvs commit -m "Build version updated" OsaDirectEAManager/Properties/AssemblyInfo.cs
 cvs commit -m "Build version updated" OsaDirectManager/Properties/AssemblyInfo.cs
 cvs commit -m "Build version updated" ApplicationAuditor/Properties/AssemblyInfo.cs
 cvs commit -m "Build version updated" ldap dll/Properties/AssemblyInfo.cs
 cvs commit -m "Build version updated" MXAccountingWeb/Properties/AssemblyInfo.cs

 cvs commit -m "Build version updated" PrintJobProvider/Properties/AssemblyInfo.cs
 cvs commit -m "Build version updated" MXAccountingEA/Properties/AssemblyInfo.cs
 cvs commit -m "Build version updated" MXAccountingMfp/Properties/AssemblyInfo.cs
 cvs commit -m "Build version updated" AccountingPlusPrimaryJobListner/Properties/AssemblyInfo.cs
 cvs commit -m "Build version updated" AccountingPlusSecondaryJobListner/Properties/AssemblyInfo.cs
 cvs commit -m "Build version updated" AccountingPlusTeritiaryJobListner/Properties/AssemblyInfo.cs
 cvs commit -m "Build version updated" AccountingPlusPrimaryJobReleaser/Properties/AssemblyInfo.cs
 cvs commit -m "Build version updated" AccountingPlusSecondaryJobReleaser/Properties/AssemblyInfo.cs
 cvs commit -m "Build version updated" AccountingPlusTeritiaryJobReleaser/Properties/AssemblyInfo.cs
 cvs commit -m "Build version updated" AccountingPlusWatcher\Properties\AssemblyInfo.cs
 cvs commit -m "Build version updated" JobParser/Properties/AssemblyInfo.cs
 cvs commit -m "Build version updated" JobTransmitter/Properties/AssemblyInfo.cs
 cvs commit -m "Build version updated" ScreenCastClient/Properties/AssemblyInfo.cs
 cvs commit -m "Build version updated" JobDispatcher/Properties/AssemblyInfo.cs
 cvs commit -m "Build version updated" EventSimulator/Properties/AssemblyInfo.cs
 cvs commit -m "Build version updated" AccountingPlusEmailExtractor/Properties/AssemblyInfo.cs
 cvs commit -m "Build version updated" D.Net.EmailClient/Properties/AssemblyInfo.cs
 cvs commit -m "Build version updated" D.Net.EmailInterfaces/Properties/AssemblyInfo.cs
 cvs commit -m "Build version updated" Net/Properties/AssemblyInfo.cs
 
c:
cd C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319

MSBuild.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\BuildHelper\AutoBuild\AutoBuild.csproj"   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\AutoBuild.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild

MSBuild.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\ldap dll\LdapStoreManager.csproj"   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\LdapStoreManager.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild

MSBuild.exe E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\DatabaseBridge\DatabaseBridge.csproj   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\DatabaseBridge.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild

MSBuild.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\AppLocalizer\AppLocalizer.csproj"   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\AppLocalizer\AppLocalizer.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild

MSBuild.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\AppLibrary\AppLibrary.csproj"   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\AppLocalizer\AppLocalizer.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild

MSBuild.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\MFPDiscovery\MFPDiscovery.csproj"   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\MFPDiscovery\MFPDiscovery.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild

MSBuild.exe E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\DataManager\DataManager.csproj   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\DataManager.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild

MSBuild.exe E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\DataManagerMfp\DataManagerMfp.csproj   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\DataManagerMfp.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild

MSBuild.exe E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\RegistrationAdapter\RegistrationAdapter.csproj   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\RegistrationAdapter.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild

MSBuild.exe E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\OsaDirectManager\OsaDirectManager.csproj   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\OsaDirectManager.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild

MSBuild.exe E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\OsaDirectEAManager\OsaDirectEAManager.csproj   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\OsaDirectManager.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild

MSBuild.exe "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\ApplicationAuditor.csproj"   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\ApplicationAuditor.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild

MSBuild.exe E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\MXAccountingWeb\MXAccountingWeb.csproj /p:Configuration=Release   /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\MXAccountingWeb.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild


MSBuild.exe E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\PrintJobProvider\PrintJobProvider.csproj /p:Configuration=Release /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\PrintJobProvider.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild

MSBuild.exe E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\MXAccountingMfp\MXAccountingMfp.csproj /p:Configuration=Release /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\MXAccountingMfp.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild

MSBuild.exe E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\MXAccountingEA\MXAccountingEA.csproj /p:Configuration=Release /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\MXAccountingMfp.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild

MSBuild.exe E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\AccountingPlusPrimaryJobListner\AccountingPlusPrimaryJobListner.csproj   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\AccountingPlusPrimaryJobListner.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild


MSBuild.exe E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\AccountingPlusSecondaryJobListner\AccountingPlusSecondaryJobListner.csproj   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\AccountingPlusSecondaryJobListner.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild

MSBuild.exe E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\AccountingPlusTeritiaryJobListner\AccountingPlusTeritiaryJobListner.csproj   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\AccountingPlusTeritiaryJobListner.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild

MSBuild.exe E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\AccountingPlusPrimaryJobReleaser\AccountingPlusPrimaryJobReleaser.csproj   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\AccountingPlusPrimaryJobReleaser.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild


MSBuild.exe E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\AccountingPlusSecondaryJobReleaser\AccountingPlusSecondaryJobReleaser.csproj   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\AccountingPlusSecondaryJobReleaser.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild

MSBuild.exe E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\AccountingPlusTeritiaryJobReleaser\AccountingPlusTeritiaryJobReleaser.csproj   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\AccountingPlusTeritiaryJobReleaser.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild

MSBuild.exe E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\AccountingPlusWatcher\AccountingPlusWatcher.csproj   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\AccountingPlusWatcher.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild

MSBuild.exe E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\ScreenCastClient\ScreenCastClient.csproj   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\ScreenCastClient.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild

MSBuild.exe E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\JobParser\JobParser.csproj   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\JobParser.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild

MSBuild.exe E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\JobTransmitter\JobTransmitter.csproj   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\JobTransmitter.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild

MSBuild.exe E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\JobDispatcher\JobDispatcher.csproj   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\JobDispatcher.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild

MSBuild.exe E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\EventSimulator\EventSimulator.csproj   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\EventSimulator.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild
 
MSBuild.exe E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\AccountingPlusEmailExtractor\AccountingPlusEmailExtractor.csproj   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\AccountingPlusEmailExtractor.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild
MSBuild.exe E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\D.Net.EmailClient\D.Net.EmailClient.csproj   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\D.Net.EmailClient.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild
MSBuild.exe E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\D.Net.EmailInterfaces\D.Net.EmailInterfaces.csproj   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\D.Net.EmailInterfaces.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild
MSBuild.exe E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Net\Net.csproj   /p:Configuration=Release  /fl  /flp2:errorsonly;logfile=E:\Projects\MXAccountingPlusCode\MXAccountingPlusLog\Net.err;Verbosity=n;Encoding=UTF-8 /t:Rebuild

REM GenerateLocalization Script

 "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\AppLocalizer.exe" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\LABELS.xls" "LABELS" "RESX_LABELS" "" "GENERATESQL" "GENERATESQLFORMASTERTABLE" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\SQLScripts\INSTALL_04_LocalizedData.sql" ""

 "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\AppLocalizer.exe" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\CLIENT_MESSAGES.xls" "CLIENT_MESSAGES" "RESX_CLIENT_MESSAGES" "" "GENERATESQL" "" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\SQLScripts\INSTALL_04_LocalizedData.sql" ""

 "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\AppLocalizer.exe" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\SERVER_MESSAGES.xls" "SERVER_MESSAGES" "RESX_SERVER_MESSAGES" "" "GENERATESQL" "" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\SQLScripts\INSTALL_04_LocalizedData.sql" ""


REM upgrade Strings

 "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\AppLocalizer.exe" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\LABELS.xls" "LABELS" "RESX_LABELS" "" "GENERATESQL" "" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\SQLScripts\UPGRADE_04_LocalizedData.sql" ""

 "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\AppLocalizer.exe" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\CLIENT_MESSAGES.xls" "CLIENT_MESSAGES" "RESX_CLIENT_MESSAGES" "" "GENERATESQL" "" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\SQLScripts\UPGRADE_04_LocalizedData.sql" ""
 "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\AppLocalizer.exe" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\SERVER_MESSAGES.xls" "SERVER_MESSAGES" "RESX_SERVER_MESSAGES" "" "GENERATESQL" "" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\SQLScripts\UPGRADE_04_LocalizedData.sql" ""

RMDIR /Q /S "D:\Projects\MX-AccountingPlus\MXAccountingWeb\obj"
RMDIR /Q /S "D:\Projects\MX-AccountingPlus\MXAccountingEA\obj"
RMDIR /Q /S "D:\Projects\MX-AccountingPlus\MXAccountingMfp\obj"

REM copy "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\SQLScripts\PrintReleaseBuildDatabase.sql" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\SQLScripts\LatestPRScript.sql"

e:
cd E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\
set CVSROOT=:sspi::psreedhar@172.29.240.66:2401:/solutions

cvs commit -m "Latest SQL Script" SQLScripts/LatestPRScript.sql

C:
rem cd C:\Program Files\Macrovision\IS 12 StandaloneBuild
cd C:\Program Files\Macrovision\2010 StandaloneBuild\System
IsCmdBld.exe -p "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Packaging\MXAccountingPlus.ism" 

REM --Created Setup files

@REM Seamonkey's quick date batch (MMDDYYYY format)
@REM Setups %date variable
@REM First parses month, day, and year into mm , dd, yyyy formats and then combines to be MMDDYYYY

REM FOR /F "TOKENS=1* DELIMS= " %%A IN ('DATE/T') DO SET CDATE=%%B
REM FOR /F "TOKENS=1,2 eol=/ DELIMS=/ " %%A IN ('DATE/T') DO SET mm=%%B
REM FOR /F "TOKENS=1,2 DELIMS=/ eol=/" %%A IN ('echo %CDATE%') DO SET dd=%%B
REM FOR /F "TOKENS=2,3 DELIMS=/ " %%A IN ('echo %CDATE%') DO SET yyyy=%%B
REM SET date=%mm%%dd%%yyyy%
REM mkdir E:\PrintRelease_Builds\"PrintRelease_%date%"
REM mkdir E:\PrintRelease_Builds\"PrintReleaseServer_%date%"

REM C:
REM xcopy /S "C:\Projects\PrintRover\PrintRelease\PrintRelease\Product Configuration 1\Release 1\DiskImages\DISK1"  E:\PrintRelease_Builds\"PrintRelease_%date%"
REM xcopy /S "C:\Projects\PrintRover\Print Release Server\PrintServer\Product Configuration 1\Release 1\DiskImages\DISK1"  E:\PrintRelease_Builds\"PrintReleaseServer_%date%"

E:
cd "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\BuildHelper\AutoBuild\bin\Release"
AutoBuild.exe   "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Build.version"  "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Packaging\Product Configuration\Release\DiskImages\DISK1" "E:\Builds\AccountingPlusStandard" ""


