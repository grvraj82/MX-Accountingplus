REM GenerateLocalization Script

"E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\AppLocalizer.exe" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\LABELS.xls" "LABELS"  "RESX_LABELS" "" "GENERATESQL" "GENERATESQLFORMASTERTABLE" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\SQLScripts\INSTALL_04_LocalizedData.sql" ""

"E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\AppLocalizer.exe" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\CLIENT_MESSAGES.xls"  "CLIENT_MESSAGES" "RESX_CLIENT_MESSAGES" "" "GENERATESQL" "" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\SQLScripts\INSTALL_04_LocalizedData.sql" ""

"E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\AppLocalizer.exe" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\SERVER_MESSAGES.xls" "SERVER_MESSAGES" "RESX_SERVER_MESSAGES" "" "GENERATESQL" "" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\SQLScripts\INSTALL_04_LocalizedData.sql" ""

REM upgrade Strings

"E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\AppLocalizer.exe" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\LABELS.xls" "LABELS"  "RESX_LABELS" "" "GENERATESQL" "" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\SQLScripts\UPGRADE_11_04_LocalizedData.sql" "1.1"

"E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\AppLocalizer.exe" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\CLIENT_MESSAGES.xls"  "CLIENT_MESSAGES" "RESX_CLIENT_MESSAGES" "" "GENERATESQL" "" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\SQLScripts\UPGRADE_11_04_LocalizedData.sql" "1.1"

"E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\AppLocalizer.exe" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\Localization Resources\SERVER_MESSAGES.xls" "SERVER_MESSAGES" "RESX_SERVER_MESSAGES" "" "GENERATESQL" "" "E:\Projects\MXAccountingPlusCode\MX-AccountingPlus\SQLScripts\UPGRADE_11_04_LocalizedData.sql" "1.1"


pause