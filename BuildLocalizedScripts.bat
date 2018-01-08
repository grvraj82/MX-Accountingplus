
REM echo off

E:
cd E:\Projects\MXAccountingPlusCode\MX-AccountingPlus
set CVSROOT=:sspi::drajshekhar@172.29.240.66:2401:/solutions
REM cvs update
REM cd E:\Projects\PrintRoverCode\PrintRover\SQLScripts
REM cvs update -P -d -C
REM GenerateLocalization Script
"E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\AppLocalizer.exe" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\LABELS.xls" "LABELS" "RESX_LABELS" "EXPORTTODB" "GENERATESQL" "GENERATESQLFORMASTERTABLE"
"E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\AppLocalizer.exe" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\CLIENT_MESSAGES.xls" "CLIENT_MESSAGES" "RESX_CLIENT_MESSAGES" "EXPORTTODB" "GENERATESQL" ""
"E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\AppLocalizer.exe" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\SERVER_MESSAGES.xls" "SERVER_MESSAGES" "RESX_SERVER_MESSAGES" "EXPORTTODB" "GENERATESQL" ""

pause
