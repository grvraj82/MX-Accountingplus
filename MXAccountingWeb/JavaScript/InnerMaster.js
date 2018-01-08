function AdjustHeight() {
    var availheight = screen.availHeight;
    var availwidth = screen.availWidth;
    var imageObject = document.getElementById("HeightAdjustImage");

    imageObject.width = availwidth - 25;
    imageObject.style.paddingLeft = "30px";
}

function ConnectTo(pageUrl) {
    location.href = pageUrl;
}

function SelectTab(object) {
    object.className = "TabOnmouseOver";
}

function ResetTab(object) {
    object.className = "TabItem";
}
function AboutUs() {
    document.getElementById('Aboutbox').style.display = "inline";
}
function AboutUsClose() {
    document.getElementById('Aboutbox').style.display = "none";
}

function ToggleMenu() {
    try {

        var menuState = document.getElementById("righttab").style.display;
        //alert(menuState);
        if (menuState == "none") {
            document.getElementById("righttab").style.display = "block";
            document.getElementById("arrow").innerHTML = "<a href=\"javascript:return ToggleMenu();\"><img src=../App_Themes/Green/Images/Hidden_Menu.png border=0></a>";

            try {
                var divID = document.getElementById("JobLogDisplay_Overflow");
                divID.style.width = parseInt(screen.availWidth - 300) + 'px';
            }
            catch (Error) {
            }

        }
        else {
            document.getElementById("righttab").style.display = "none";
            document.getElementById("arrow").innerHTML = "<a href=\"javascript:return ToggleMenu();\"><img src=../App_Themes/Green/Images/Show_Menu.png border=0></a>";

            try {
                var divID = document.getElementById("JobLogDisplay_Overflow");
                divID.style.width = parseInt(screen.availWidth - 100) + 'px';
            }
            catch (Error) {
            }
        }


    }
    catch (Error) { }
}


function fnShowCellUsers() {
    try {
        document.getElementById("TdMFPSubMenu").style.display = "none"
    }
    catch (Error) { }
    try {
        document.getElementById("TdReportsMenu").style.display = "none";
    }
    catch (Error) { }
    try {
        document.getElementById("TrCostCenterSubMenu").style.display = "none"
    }
    catch (Error) { }
    try {
        document.getElementById("TrPriceSubMenu").style.display = "none"
    }
    catch (Error) { }
    try {
        document.getElementById("TrSettingsMenu").style.display = "none";
    }
    catch (Error) { }

    try {

        if (document.getElementById("TdUserSubMenu").style.display == "none") {
            try {
                document.getElementById("TdUserSubMenu").style.display = "block";                
            }
            catch (Error) { }
            try {
                document.getElementById("CostCenters").className = "Menu_bg";
                document.getElementById("TrCostCenterSubMenu").style.display = "Menu_bg";
            }
            catch (Error) {
            }
            try {
                document.getElementById("Device").className = "Menu_bg";
                document.getElementById("TdMFPSubMenu").style.display = "Menu_bg";
            }
            catch (Error) { }
            try {
                document.getElementById("Pricing").className = "Menu_bg";
                document.getElementById("TrPriceSubMenu").style.display = "Menu_bg";
            }
            catch (Error) { }
            try {
                document.getElementById("Reports").className = "Menu_bg";
                document.getElementById("TdReportsMenu").style.display = "Menu_bg";
            }
            catch (Error) { }
            try {
                document.getElementById("Settings").className = "Menu_bg";
                document.getElementById("TrSettingsMenu").style.display = "Menu_bg";
            }
            catch (Error) { }            
        }
        else {
            document.getElementById("TdUserSubMenu").style.display = "none";
        }
    }
    catch (Error) { }
    DisplayTabs();
}

function fnShowCellMFPs() {


    try {
        document.getElementById("TdUserSubMenu").style.display = "none";
    }
    catch (Error) {
    }
    try {
        document.getElementById("TrPriceSubMenu").style.display = "none"
    }
    catch (Error) {
    }
    try {
        document.getElementById("TrCostCenterSubMenu").style.display = "none"
    }
    catch (Error) { }
    try {
        document.getElementById("TdReportsMenu").style.display = "none";
    }
    catch (Error) {
    }
    try {
        document.getElementById("TrSettingsMenu").style.display = "none";
    }
    catch (Error) {
    }

    try {

        if (document.getElementById("TdMFPSubMenu").style.display == "none") {

            try {
                document.getElementById("UserID").className = "Menu_bg";
                document.getElementById("TdUserSubMenu").style.display = "Menu_bg";
            }
            catch (Error) {
            }
            try {
                document.getElementById("CostCenters").className = "Menu_bg";
                document.getElementById("TrCostCenterSubMenu").style.display = "Menu_bg";
            }
            catch (Error) {
            }
            try {
                document.getElementById("TdMFPSubMenu").style.display = "block";
            }
            catch (Error) {
            }
            try {
                document.getElementById("Pricing").className = "Menu_bg";
                document.getElementById("TrPriceSubMenu").style.display = "Menu_bg"
            }
            catch (Error) {
            }

            try {
                document.getElementById("Reports").className = "Menu_bg";
                document.getElementById("TdReportsMenu").style.display = "Menu_bg";
            }
            catch (Error) {
            }
            try {
                document.getElementById("Settings").className = "Menu_bg";
                document.getElementById("TrSettingsMenu").style.display = "Menu_bg";
            }
            catch (Error) {
            }

        }
        else {
            document.getElementById("TdMFPSubMenu").style.display = "none";
        }
    }
    catch (Error) {
    }
    DisplayTabs();
}

function fnShowCostCenters() {
    try {
        document.getElementById("TdMFPSubMenu").style.display = "none"
    }
    catch (Error) { }
    try {
        document.getElementById("TdReportsMenu").style.display = "none";
    }
    catch (Error) { }
    try {
        document.getElementById("TrPriceSubMenu").style.display = "none"
    }
    catch (Error) { }
    try {
        document.getElementById("TrSettingsMenu").style.display = "none";
    }
    catch (Error) { }
    if (document.getElementById("TrCostCenterSubMenu").style.display == "none") {
        try {
            document.getElementById("UserID").className = "Menu_bg";
            document.getElementById("TdUserSubMenu").style.display = "Menu_bg";
        }
        catch (Error) { }
        try {
            document.getElementById("TrCostCenterSubMenu").style.display = "block";
        }
        catch (Error) {
        }
        try {
            document.getElementById("Device").className = "Menu_bg";
            document.getElementById("TdMFPSubMenu").style.display = "Menu_bg";
        }
        catch (Error) { }
        try {
            document.getElementById("Pricing").className = "Menu_bg";
            document.getElementById("TrPriceSubMenu").style.display = "Menu_bg"
        }
        catch (Error) { }
        try {
            document.getElementById("Reports").className = "Menu_bg";
            document.getElementById("TdReportsMenu").style.display = "Menu_bg";
        }
        catch (Error) { }
        try {
            document.getElementById("Settings").className = "Menu_bg";
            document.getElementById("TrSettingsMenu").style.display = "Menu_bg";
        }
        catch (Error) { }
    }
    else {
        document.getElementById("TrCostCenterSubMenu").style.display = "none";
    }
    DisplayTabs();
}

function fnShowPrice() {

    try {
        document.getElementById("TdUserSubMenu").style.display = "none";
    }
    catch (Error) {
    }
    try {
        document.getElementById("TdMFPSubMenu").style.display = "none"
    }
    catch (Error) {
    }
    try {
        document.getElementById("TdReportsMenu").style.display = "none";
    }
    catch (Error) {
    }
    try {
        document.getElementById("TrCostCenterSubMenu").style.display = "none"
    }
    catch (Error) { }
    try {
        document.getElementById("TrSettingsMenu").style.display = "none";
    }
    catch (Error) {
    }

    if (document.getElementById("TrPriceSubMenu").style.display == "none") {

        try {
            document.getElementById("UserID").className = "Menu_bg";
            document.getElementById("TdUserSubMenu").style.display = "Menu_bg";
        }
        catch (Error) {
        }
        try {
            document.getElementById("CostCenters").className = "Menu_bg";
            document.getElementById("TrCostCenterSubMenu").style.display = "Menu_bg";
        }
        catch (Error) {
        }
        try {
            document.getElementById("Device").className = "Menu_bg";
            document.getElementById("TdMFPSubMenu").style.display = "Menu_bg";
        }
        catch (Error) {
        }
        try {
            document.getElementById("Pricing").className = "Menu_bg";
            document.getElementById("TrPriceSubMenu").style.display = "block";
        }
        catch (Error) {
        }
        try {
            document.getElementById("Reports").className = "Menu_bg";
            document.getElementById("TdReportsMenu").style.display = "Menu_bg";
        }
        catch (Error) {
        }
        try {
            document.getElementById("Settings").className = "Menu_bg";
            document.getElementById("TrSettingsMenu").style.display = "Menu_bg";
        }
        catch (Error) {
        }
    }
    else {
        document.getElementById("TrPriceSubMenu").style.display = "none";
    }
    DisplayTabs();
}

//AuditLog

function fnShowAuditLog() {

    try {
        document.getElementById("TdUserSubMenu").style.display = "none";
    }
    catch (Error) {
    }
    try {
        document.getElementById("TdMFPSubMenu").style.display = "none";
    }
    catch (Error) {
    }
    try {
        document.getElementById("TrPriceSubMenu").style.display = "none";
    }
    catch (Error) {
    }
    try {
        document.getElementById("TrCostCenterSubMenu").style.display = "none"
    }
    catch (Error) { }
    try {

        document.getElementById("TrSettingsMenu").style.display = "none";
    }
    catch (Error) {
    }

    try
     {
        if (document.getElementById("TrAuditLogSubMenu").style.display == "none")
         {
            document.getElementById("TrAuditLogSubMenu").style.display = "block";
        }
        else {
            document.getElementById("TrAuditLogSubMenu").style.display = "none";
        }
    }
    catch (Error) {
    }    
    DisplayTabs();
}

//Reports
function fnShowCellReports() {   
    try {
        document.getElementById("TdUserSubMenu").style.display = "none";
    }
    catch (Error) {
    }
    try {
        document.getElementById("TdMFPSubMenu").style.display = "none";
    }
    catch (Error) {
    }
    try {
        document.getElementById("TrPriceSubMenu").style.display = "none";
    }
    catch (Error) {
    }
    try {
        document.getElementById("TrCostCenterSubMenu").style.display = "none"
    }
    catch (Error) { }
    try {

        document.getElementById("TrSettingsMenu").style.display = "none";
    }
    catch (Error) {
    }

    if (document.getElementById("TdReportsMenu").style.display == "none") {
        try {
            document.getElementById("TdReportsMenu").style.display = "block";
        }
        catch (Error) {
        }
        try {
            document.getElementById("UserID").className = "Menu_bg";
            document.getElementById("TdUserSubMenu").style.display = "Menu_bg";
        }
        catch (Error) {
        }
        try {
            document.getElementById("CostCenters").className = "Menu_bg";
            document.getElementById("TrCostCenterSubMenu").style.display = "Menu_bg";
        }
        catch (Error) {
        }
        try {
            document.getElementById("Device").className = "Menu_bg";
            document.getElementById("TdMFPSubMenu").style.display = "Menu_bg";
        }
        catch (Error) {
        }
        try {
            document.getElementById("Pricing").className = "Menu_bg";
            document.getElementById("TrPriceSubMenu").style.display = "Menu_bg";
        }
        catch (Error) {
        }
        try {
            document.getElementById("Settings").className = "Menu_bg";
            document.getElementById("TrSettingsMenu").style.display = "Menu_bg";
        }
        catch (Error) {
        }
    }
    else {

        document.getElementById("TdReportsMenu").style.display = "none";
    }
    DisplayTabs();
}
//Settings
function fnShowCellSettings() {
    try {
        document.getElementById("TdUserSubMenu").style.display = "none";
    }
    catch (Error) {
    }
    try {
        document.getElementById("TdMFPSubMenu").style.display = "none";
    }
    catch (Error) {
    }
    try {
        document.getElementById("TrPriceSubMenu").style.display = "none";
    }
    catch (Error) {
    }
    try {
        document.getElementById("TrCostCenterSubMenu").style.display = "none"
    }
    catch (Error) { }
    try {
        document.getElementById("TdReportsMenu").style.display = "none";
    }
    catch (Error) {
    }

    if (document.getElementById("TrSettingsMenu").style.display == "none") {
        try {
            document.getElementById("UserID").className = "Menu_bg";
            document.getElementById("TdUserSubMenu").style.display = "Menu_bg";
        }
        catch (Error) {
        }
        try {
            document.getElementById("CostCenters").className = "Menu_bg";
            document.getElementById("TrCostCenterSubMenu").style.display = "Menu_bg";
        }
        catch (Error) {
        }
        try {
            document.getElementById("Device").className = "Menu_bg";
            document.getElementById("TdMFPSubMenu").style.display = "Menu_bg";
        }
        catch (Error) {
        }
        try {
            document.getElementById("Pricing").className = "Menu_bg";
            document.getElementById("TrPriceSubMenu").style.display = "Menu_bg";
        }
        catch (Error) {
        }
        try {
            document.getElementById("Reports").className = "Menu_bg";
            document.getElementById("TdReportsMenu").style.display = "Menu_bg";
        }
        catch (Error) {
        }
        try {
            document.getElementById("TrSettingsMenu").style.display = "block";
        }
        catch (Error) {
        }
    }
    else {
        try {
            document.getElementById("TrSettingsMenu").style.display = "none";
        }
        catch (Error) {
        }
    }
    DisplayTabs();
}

function DisplayTabs() {    
    var role = document.getElementById('ctl00_HiddenFieldUserRole').value;
    if (role.toLowerCase() == 'admin') {
        // document.getElementById('MyProfile').style.display = "none";
        document.getElementById('MyPermissionsandLimits').style.display = "none";
    }
    else {

        document.getElementById('MyPermissionsandLimits').style.display = "inline";

        document.getElementById('UserID').style.display = "none";
        
        document.getElementById('ManageUsers').style.display = "none";
        document.getElementById('PermissionsLimits').style.display = "none";
        document.getElementById('AccessRights').style.display = "none";

        document.getElementById('menuSplitDevices').style.display = "none";
        document.getElementById('Device').style.display = "none";

        document.getElementById('menuSplitAuditLog').style.display = "none";
        document.getElementById('AuditLog').style.display = "none";

        document.getElementById('menuSplitPricing').style.display = "none";
        document.getElementById('Pricing').style.display = "none";

        document.getElementById('menuSplitPricing').style.display = "none";
        document.getElementById('Pricing').style.display = "none";

        document.getElementById('AdvancedReport').style.display = "none";
        document.getElementById('Invoice').style.display = "none";

        document.getElementById('GeneralSettings').style.display = "none";
        document.getElementById('AutoRefill').style.display = "none";
        document.getElementById('CardConfiguration').style.display = "none";
        document.getElementById('ADandDMSettings').style.display = "none";
        document.getElementById('ADSyncSettings').style.display = "none";
        document.getElementById('JobConfiguration').style.display = "none";
        document.getElementById('LogConfiguration').style.display = "none";
        document.getElementById('Departments').style.display = "none";
        document.getElementById('Subscription').style.display = "none";
        //document.getElementById('PaperSizes').style.display = "none";
        document.getElementById('ManageLanguages').style.display = "none";
        document.getElementById('CustomMessages').style.display = "none";
        document.getElementById('CostCenters').style.display = "none";
        document.getElementById('Backup').style.display = "none";
      
        document.getElementById('GraphicalReport').style.display = "none";

        document.getElementById('ApplicationRegistration').style.display = "none";
        document.getElementById('RegistrationDetails').style.display = "none";

        document.getElementById('AppThems').style.display = "none";
        document.getElementById('SMTPSettings').style.display = "none";
        document.getElementById('ProxySettings').style.display = "none";
        document.getElementById('ServerDetails').style.display = "none";
       

    }
}

function Meuselected(val) {
    document.getElementById(val).className = "Selected_MenuBG";
    if (val != "UserID") {
        document.getElementById("TdUserSubMenu").style.display = "none";
    }
    else {
        document.getElementById("TdUserSubMenu").style.display = "block";
    }
}
