using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using ApplicationBase;
using System.Globalization;
using ApplicationAuditor;
using AppLibrary;
using AccountingPlusWeb.MasterPages;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;


public partial class AdministrationCustomTheme : ApplicationBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(Session["UserRole"] as string))
            {
                Response.Redirect("../Web/LogOn.aspx", true);
                return;
            }
            else if (Session["UserRole"] as string != "admin")
            {
                Response.Redirect("~/Administration/UnAuthorisedAccess.aspx");
            }
            if (!IsPostBack)
            {

                BindCustomImages();
                BindCustomImageValues();
               
                //BindAppThemes("WEB");
            }
            LocalizeThisPage();
            ImageButtonDelete.Attributes.Add("onclick", "return DeleteUsers()");
            ImageButtonEdit.Attributes.Add("onclick", "return UpdateUserDetails()");

            LinkButton manageSettings = (LinkButton)Master.FindControl("LinkButtonAppThemes");
            if (manageSettings != null)
            {
                manageSettings.CssClass = "linkButtonSelect_Selected";
            }
        }
        catch
        {

        }
    }

    private void BindAppThemes(string mfpModel)
    {
        try
        {

            if (string.IsNullOrEmpty(mfpModel))
            {
                mfpModel = "WEB";
            }

            string appThemeFolderPath = string.Empty;
            if (mfpModel == "WEB")
            {
                appThemeFolderPath = Server.MapPath("~/App_Themes");
            }
            else
            {
                appThemeFolderPath = ConfigurationManager.AppSettings["EAMThemesFolder"] + "\\App_Themes";
            }


            string[] filePaths = Directory.GetDirectories(appThemeFolderPath);
            DropDownListApplicationTheme.Items.Clear();
            DropDownListApplicationTheme.Items.Add(new ListItem("[Select]", "-1"));
            for (int folderName = 0; folderName < filePaths.Length; folderName++)
            {
                string appThemeFolderName = Path.GetFileName(filePaths[folderName]).ToString();
                if (appThemeFolderName.ToLower() != "cvs")
                {
                    DropDownListApplicationTheme.Items.Add(new ListItem(appThemeFolderName, appThemeFolderName));
                }
            }
        }
        catch
        {

        }
    }

    private void LocalizeThisPage()
    {
        string labelResourceIDs = "CUSTOM_THEME,SELECT_IMAGE,CLOSE,UPLOAD_CUSTOMIMAGE,NOTE,UPLOAD,RESET,APP_THEME,APP_FONT_COLOR,APP_BACKGROUND_COLOR,APP_TITLEBACKGROUND_COLOR,CUSTOM_WALLPAPER,REQUIRED_FIELDS,CLICKHERE_TO_ADD_CUSTOMWALLPAPER,CLICKHERE_TO_EDIT_CUSTOMWALLPAPER,CLICKHERE_TO_RESET_CUSTOMWALLPAPER,THEME_DETAILS,CUSTOM_THEME_DETAILS,APPLICATION_MFP_MODEL,THEME_NAME,HEIGHT,WIDTH,CUSTOM_WALLPAPER_NAME,CUSTOM_WALLPAPER_HEIGHT,CUSTOM_WALLPAPER_WIDTH";
        string clientMessagesResourceIDs = "PLEASE_SELECT_APPLICATION_MFP_MODEL,SELECT_ONLY_ONE_WALLPAPER";
        string serverMessageResourceIDs = "PLEASE_SELECT_APPLICATION_MFP_MODEL";
        Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

        LabelHeadingGeneralPage.Text = localizedResources["L_CUSTOM_THEME"].ToString();
        LabelSelectImage.Text = localizedResources["L_SELECT_IMAGE"].ToString();
        LabelUploadImage.Text = localizedResources["L_UPLOAD_CUSTOMIMAGE"].ToString();
        LabelThemes.Text = localizedResources["L_CUSTOM_WALLPAPER"].ToString();
        LabelRequiredField.Text = localizedResources["L_REQUIRED_FIELDS"].ToString();
        LabelAppTheme.Text = localizedResources["L_APP_THEME"].ToString();
        LabelAppBckColor.Text = localizedResources["L_APP_BACKGROUND_COLOR"].ToString();
        LabelAppTitleBar.Text = localizedResources["L_APP_TITLEBACKGROUND_COLOR"].ToString();
        LabelAppFontColor.Text = localizedResources["L_APP_FONT_COLOR"].ToString();
        LabelNote.Text = localizedResources["L_NOTE"].ToString();

        ButtonUpload.Text = localizedResources["L_UPLOAD"].ToString();
        ButtonReset.Text = localizedResources["L_RESET"].ToString();
        ButtonCancel.Text = localizedResources["L_CLOSE"].ToString();
                
        ImageButtonAdd.ToolTip = localizedResources["L_CLICKHERE_TO_ADD_CUSTOMWALLPAPER"].ToString();
        ImageButtonEdit.ToolTip = localizedResources["L_CLICKHERE_TO_EDIT_CUSTOMWALLPAPER"].ToString();
        ImageButtonDelete.ToolTip = localizedResources["L_CLICKHERE_TO_RESET_CUSTOMWALLPAPER"].ToString();
        TableHeaderCellThemeDetails.Text = localizedResources["L_THEME_DETAILS"].ToString();
        TableHeaderCellCustomThemeDetails.Text = localizedResources["L_CUSTOM_THEME_DETAILS"].ToString();
        TableHeaderCellApplicationName.Text = localizedResources["L_APPLICATION_MFP_MODEL"].ToString();
        TableHeaderCellThemeName.Text = localizedResources["L_THEME_NAME"].ToString();
        TableHeaderCellHeight.Text = localizedResources["L_HEIGHT"].ToString();
        TableHeaderCellWidth.Text = localizedResources["L_WIDTH"].ToString();
        TableHeaderCellCustomWallPaper.Text = localizedResources["L_CUSTOM_WALLPAPER_NAME"].ToString();
        TableHeaderCellCustomHeight.Text = localizedResources["L_CUSTOM_WALLPAPER_HEIGHT"].ToString(); 
        TableHeaderCellCustomWidth.Text = localizedResources["L_CUSTOM_WALLPAPER_WIDTH"].ToString();
    }
    private void BindCustomImageValues()
    {
        try
        {
            int row = 0;
            DbDataReader drCustomThemes = DataManager.Provider.Users.ProvideThemeImageValues();
            while (drCustomThemes.Read())
            {
                TableRow WallPaperDetailRow = new TableRow();
                AppController.StyleTheme.SetGridRowStyle(WallPaperDetailRow);
                WallPaperDetailRow.ID = drCustomThemes["BG_APP_NAME"].ToString();

                row = row + 1;

                TableCell tdCheckBox = new TableCell();
                tdCheckBox.HorizontalAlign = HorizontalAlign.Left;
                tdCheckBox.Text = "<input type='checkbox' id=\"" + drCustomThemes["BG_APP_NAME"].ToString() + "\" name='__CUSTOMID' value=\"" + drCustomThemes["BG_APP_NAME"].ToString() + "\" onclick='javascript:ValidateSelectedCount()' />";
                tdCheckBox.Width = 30;

                TableCell tdSlNo = new TableCell();
                tdSlNo.HorizontalAlign = HorizontalAlign.Left;
                tdSlNo.Text = Convert.ToString(row, CultureInfo.CurrentCulture);
                tdSlNo.Width = 30;

                TableCell tdImageType = new TableCell();
                tdImageType.Text = Server.HtmlEncode(drCustomThemes["BG_IMAGE_TYPE"].ToString());
                tdImageType.CssClass = "GridLeftAlign";
                tdImageType.Attributes.Add("onclick", "togall(" + row + ")");
                TableCell tdImageHeight = new TableCell();
                tdImageHeight.Text = Server.HtmlEncode(drCustomThemes["BG_ACT_HEIGHT"].ToString());
                tdImageHeight.HorizontalAlign = HorizontalAlign.Left;
                tdImageHeight.Attributes.Add("onclick", "togall(" + row + ")");

                TableCell tdImageWidth = new TableCell();
                tdImageWidth.Text = Server.HtmlEncode(drCustomThemes["BG_ACT_WIDTH"].ToString());
                tdImageWidth.HorizontalAlign = HorizontalAlign.Left;
                tdImageWidth.Attributes.Add("onclick", "togall(" + row + ")");

                TableCell tdImageTheme = new TableCell();
                tdImageTheme.Text = Server.HtmlEncode(drCustomThemes["APP_THEME"].ToString());
                tdImageTheme.HorizontalAlign = HorizontalAlign.Left;
                tdImageTheme.Attributes.Add("onclick", "togall(" + row + ")");

                TableCell tdCustomImageType = new TableCell();
                if (string.IsNullOrEmpty(Convert.ToString(drCustomThemes["BG_UPDATED_IMAGEPATH"])))
                {
                    tdCustomImageType.Text = "";
                }
                else
                {
                    tdCustomImageType.Text = Server.HtmlEncode(drCustomThemes["BG_UPDATED_IMAGEPATH"].ToString());
                }
                tdCustomImageType.CssClass = "GridLeftAlign";
                tdCustomImageType.Attributes.Add("onclick", "togall(" + row + ")");
                //tdCustomImageType.HorizontalAlign = HorizontalAlign.Left;

                TableCell tdCustomImageHeight = new TableCell();
                if (Convert.ToString(drCustomThemes["BG_REC_HEIGHT"]) == "0" || Convert.ToString(drCustomThemes["BG_REC_HEIGHT"]) == "")
                {
                    tdCustomImageHeight.Text = "";
                }
                else
                {

                    tdCustomImageHeight.Text = Server.HtmlEncode(drCustomThemes["BG_REC_HEIGHT"].ToString());
                }

                tdCustomImageHeight.HorizontalAlign = HorizontalAlign.Left;
                tdCustomImageHeight.Attributes.Add("onclick", "togall(" + row + ")");

                TableCell tdCustomImageWidth = new TableCell();
                if (Convert.ToString(drCustomThemes["BG_REC_WIDTH"]) == "0" || Convert.ToString(drCustomThemes["BG_REC_WIDTH"]) == "")
                {
                    tdCustomImageWidth.Text = "";
                }
                else
                {

                    tdCustomImageWidth.Text = Server.HtmlEncode(drCustomThemes["BG_REC_WIDTH"].ToString());
                }
                tdCustomImageWidth.Attributes.Add("onclick", "togall(" + row + ")");
                tdCustomImageWidth.HorizontalAlign = HorizontalAlign.Left;

                WallPaperDetailRow.Cells.Add(tdCheckBox);
                WallPaperDetailRow.Cells.Add(tdSlNo);
                WallPaperDetailRow.Cells.Add(tdImageType);
                WallPaperDetailRow.Cells.Add(tdImageTheme);
                WallPaperDetailRow.Cells.Add(tdImageHeight);
                WallPaperDetailRow.Cells.Add(tdImageWidth);
                WallPaperDetailRow.Cells.Add(tdCustomImageType);
                WallPaperDetailRow.Cells.Add(tdCustomImageHeight);
                WallPaperDetailRow.Cells.Add(tdCustomImageWidth);

                TableCustomThems.Rows.Add(WallPaperDetailRow);
                HiddenUsersCount.Value = Convert.ToString(row);
            }

        }
        catch
        {

        }
    }
    private InnerPage GetMasterPage()
    {
        MasterPage masterPage = this.Page.Master;
        InnerPage headerPage = (InnerPage)masterPage;
        return headerPage;
    }
    private void BindCustomImages()
    {
        try
        {

            DropDownListImageType.Items.Clear();
            DropDownListImageType.Items.Add(new ListItem("[Select]", "-1"));

            DbDataReader drImageType = DataManager.Provider.Users.ProvideThemeImages();

            if (drImageType.HasRows)
            {
                while (drImageType.Read())
                {
                    DropDownListImageType.Items.Add(new ListItem(drImageType["BG_IMAGE_TYPE"].ToString(), Convert.ToString(drImageType["BG_APP_NAME"])));
                }
            }
            if (drImageType != null && drImageType.IsClosed == false)
            {
                drImageType.Close();
            }
        }
        catch
        {

        }

    }
    protected void ButtonUpload_Click(object sender, EventArgs e)
    {
        UploadWallPaper();
        //GenerateWallPaper();
    }
    private void GenerateWallPaper(string wallPaperFile)
    {
        string selectedApplication = DropDownListImageType.SelectedItem.Value;
        string selectedThemeName = DropDownListApplicationTheme.SelectedItem.Value;
        if (selectedApplication != "WEB")
        {
            if (File.Exists(wallPaperFile))
            {
                string finalWallPapperPath = string.Empty;
                //string currentWebAppRootFolder = Server.MapPath("~");
                //string installationPath = currentWebAppRootFolder.Replace(@"\AccountingPlusAdmin", "");
                string mfpEAMAppPath = ConfigurationManager.AppSettings["EAMThemesFolder"];
                string mfpACMAppPath = ConfigurationManager.AppSettings["ACMThemesFolder"];
                string finalImageName = "CustomAppBG.jpg";



                switch (selectedApplication)
                {

                    case "Wide-XGA":
                        GenerateWallPapers(wallPaperFile, selectedApplication, mfpEAMAppPath, finalImageName, 60, 65, 240, 295, 1280, 725, selectedThemeName);
                        GenerateWallPapers(wallPaperFile, selectedApplication, mfpACMAppPath, finalImageName, 60, 65, 240, 295, 1280, 725, selectedThemeName);
                        break;
                    case "Wide-SVGA":
                        GenerateWallPapers(wallPaperFile, selectedApplication, mfpEAMAppPath, finalImageName, 60, 65, 240, 220, 1030, 590, selectedThemeName);
                        GenerateWallPapers(wallPaperFile, selectedApplication, mfpACMAppPath, finalImageName, 60, 65, 240, 220, 1030, 590, selectedThemeName);
                        break;
                    case "Wide-VGA":
                        GenerateWallPapers(wallPaperFile, selectedApplication, mfpEAMAppPath, finalImageName, 75, 55, 140, 175, 800, 392, selectedThemeName);
                        GenerateWallPapers(wallPaperFile, selectedApplication, mfpACMAppPath, finalImageName, 75, 55, 140, 175, 800, 392, selectedThemeName);
                        break;
                    case "480X272":
                        GenerateWallPapers(wallPaperFile, selectedApplication, mfpEAMAppPath, finalImageName, 20, 27, 140, 175, 480, 272, selectedThemeName);
                        GenerateWallPapers(wallPaperFile, selectedApplication, mfpACMAppPath, finalImageName, 20, 27, 140, 175, 480, 272, selectedThemeName);
                        break;

                    default:
                        break;

                }
            }
        }


    }
    private static void GenerateWallPapers(string wallPaperFile, string selectedApplication, string appRootPath, string finalImageName, int loginUserIconPositionX, int loginUserIconPositionY, int loginIconPositionX, int loginIconPositionY, int wallPaperWidth, int wallPaperHeight, string selectedThemeName)
    {

        //string[] themeNames = { "Green", "Blue", "Black" };


        //for (int themeValue = 0; themeValue < themeNames.Length; themeValue++)
        //{
        string finalWallPaperPath = string.Empty;

        DataTable dtImages = new DataTable();

        dtImages.Columns.Add("FILE_PATH", typeof(string));
        dtImages.Columns.Add("POSITION_X", typeof(int));
        dtImages.Columns.Add("POSITION_Y", typeof(int));

        // Login
        dtImages.Rows.Clear();
        dtImages.Rows.Add(wallPaperFile, 0, 0);
        dtImages.Rows.Add(appRootPath + "/App_Themes/" + selectedThemeName + "/" + selectedApplication + "/Images/Overlay_Login.png", 0, 0);
        dtImages.Rows.Add(appRootPath + "/App_Themes/" + selectedThemeName + "/" + selectedApplication + "/Images/LoginIcon.png", loginUserIconPositionX, loginUserIconPositionY);
        dtImages.Rows.Add(appRootPath + "/App_Themes/" + selectedThemeName + "/" + selectedApplication + "/Images/UserGroup_Login.png", loginIconPositionX, loginIconPositionY);
        finalWallPaperPath = appRootPath + "/App_UserData/WallPapers/" + selectedThemeName + "/" + selectedApplication + "/" + finalImageName;
        string overlayResult = DataManager.OverlayImages.CreateOverlayImages(dtImages, finalWallPaperPath, wallPaperWidth, wallPaperHeight);

        // Inside Page
        dtImages.Rows.Clear();
        dtImages.Rows.Add(wallPaperFile, 0, 0);
        dtImages.Rows.Add(appRootPath + "/App_Themes/" + selectedThemeName + "/" + selectedApplication + "/Images/Overlay_Inside.png", 0, 0);
        finalWallPaperPath = appRootPath + "/App_UserData/WallPapers/" + selectedThemeName + "/" + selectedApplication + "/Inside_" + finalImageName;
        overlayResult = DataManager.OverlayImages.CreateOverlayImages(dtImages, finalWallPaperPath, wallPaperWidth, wallPaperHeight);

        // Settings Page
        dtImages.Rows.Clear();
        dtImages.Rows.Add(wallPaperFile, 0, 0);
        dtImages.Rows.Add(appRootPath + "/App_Themes/" + selectedThemeName + "/" + selectedApplication + "/Images/Settings_Overlay.png", 0, 0);
        finalWallPaperPath = appRootPath + "/App_UserData/WallPapers/" + selectedThemeName + "/" + selectedApplication + "/JobList_" + finalImageName;
        overlayResult = DataManager.OverlayImages.CreateOverlayImages(dtImages, finalWallPaperPath, wallPaperWidth, wallPaperHeight);

        // Card Login
        dtImages.Rows.Clear();
        dtImages.Rows.Add(wallPaperFile, 0, 0);
        dtImages.Rows.Add(appRootPath + "/App_Themes/" + selectedThemeName + "/" + selectedApplication + "/Images/Overlay_Login.png", 0, 0);
        dtImages.Rows.Add(appRootPath + "/App_Themes/" + selectedThemeName + "/" + selectedApplication + "/Images/LoginIcon.png", loginUserIconPositionX, loginUserIconPositionY);
        dtImages.Rows.Add(appRootPath + "/App_Themes/" + selectedThemeName + "/" + selectedApplication + "/Images/Card_Login.png", loginIconPositionX, loginIconPositionY);

        finalWallPaperPath = appRootPath + "/App_UserData/WallPapers/" + selectedThemeName + "/" + selectedApplication + "/Card_" + finalImageName;
        overlayResult = DataManager.OverlayImages.CreateOverlayImages(dtImages, finalWallPaperPath, wallPaperWidth, wallPaperHeight);

        // MFP Job Status
        dtImages.Rows.Clear();
        dtImages.Rows.Add(wallPaperFile, 0, 0);
        dtImages.Rows.Add(appRootPath + "/App_Themes/" + selectedThemeName + "/" + selectedApplication + "/Images/Overlay_Inside.png", 0, 0);
        dtImages.Rows.Add(appRootPath + "/App_Themes/" + selectedThemeName + "/" + selectedApplication + "/Images/MFPTransparent.png", (wallPaperWidth / 2) - 75, 100);

        finalWallPaperPath = appRootPath + "/App_UserData/WallPapers/" + selectedThemeName + "/" + selectedApplication + "/JobStatus_" + finalImageName;
        overlayResult = DataManager.OverlayImages.CreateOverlayImages(dtImages, finalWallPaperPath, wallPaperWidth, wallPaperHeight);
        //}
    }
    private void UploadWallPaper()
    {
        try
        {
            string ThemeFileExtension = string.Empty;
            string ThemeSelectedFileName = string.Empty;
            string ThemeCombainPath = string.Empty;
            string bgAppName = DropDownListImageType.SelectedItem.Value;
            string auditorSource = HostIP.GetHostIP();

            string appBackgroundColor = TextBoxApplicationBackgroundColor.Text.Trim();
            string appTittlebarColor = TextBoxTittlebarBackground.Text.Trim();
            string appFontColor = TextBoxApplicationFontColor.Text.Trim();

            string themesFolderEAM = ConfigurationManager.AppSettings["EAMThemesFolder"];
            string themesFolderACM = ConfigurationManager.AppSettings["ACMThemesFolder"];
            string height = "0";
            string width = "0";
            string maximumAllowedSize = "0";
            if (!Directory.Exists(Server.MapPath("~/App_UserData/WallPapers")))
            {
                Directory.CreateDirectory(Server.MapPath("~/App_UserData/WallPapers"));
            }


            if (bgAppName == "-1")
            {
                //string serverMessage = "Please select Application/MFP Model"; //Localization.GetServerMessage("", Session["selectedCulture"] as string, "SELECT_BG_IMAGE");

                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PLEASE_SELECT_APPLICATION_MFP_MODEL");
              
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage, null);
                BindCustomImageValues();
                return;
            }
            string selectedTheme = DropDownListApplicationTheme.SelectedItem.Value;
            if (FileUploadCustomTheme.HasFile)
            {
                ThemeFileExtension = Path.GetExtension(FileUploadCustomTheme.PostedFile.FileName).ToLower();
                if (ThemeFileExtension.ToLower() == ".png" || ThemeFileExtension.ToLower() == ".jpg" || ThemeFileExtension.ToLower() == ".gif")
                {
                    //Uploaded file extension is valid
                }
                else
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "UPLOAD_EXTENSIONS_LIMIT");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage, null);
                    BindCustomImageValues();
                    return;
                }


            }
            if (selectedTheme == "-1")
            {
                string serverMessage = "Please select theme"; //Localization.GetServerMessage("", Session["selectedCulture"] as string, "SELECT_BG_IMAGE");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage, null);
                BindCustomImageValues();
                return;
            }

            if (selectedTheme != "-1" && bgAppName != "-1")
            {

                if (FileUploadCustomTheme.HasFile)
                {
                    ThemeFileExtension = Path.GetExtension(FileUploadCustomTheme.PostedFile.FileName).ToLower();
                    int uploadedFilesizeBytes = FileUploadCustomTheme.PostedFile.ContentLength;
                    int kiloBytes = uploadedFilesizeBytes / 1024;
                    DbDataReader wallPaperDefaultValues = DataManager.Provider.Users.ProvideMaximumWallPaperSize(bgAppName);
                    maximumAllowedSize = "0";
                    while (wallPaperDefaultValues.Read())
                    {

                        maximumAllowedSize = wallPaperDefaultValues["BG_ALLOWED_SIZE_KB"].ToString();
                    }
                    wallPaperDefaultValues.Close();
                    if (kiloBytes >= Convert.ToInt32(maximumAllowedSize))
                    {
                        string auditMessage = "Exceeded maximun allowed size";
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                        string serverMessage = Localization.GetLabelText("", Session["selectedCulture"] as string, "MAXIMUMSIZE_EXCEEDED");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage, null);
                        BindCustomImageValues();
                        return;
                    }
                    ThemeSelectedFileName = Path.GetFileName(FileUploadCustomTheme.PostedFile.FileName);
                    ThemeCombainPath = Path.Combine(Server.MapPath("~/App_UserData/WallPapers"), bgAppName + "_" + ThemeSelectedFileName);
                    FileUploadCustomTheme.PostedFile.SaveAs(ThemeCombainPath);
                    height = "0";
                    width = "0";
                    System.Drawing.Image image = System.Drawing.Image.FromFile(ThemeCombainPath);
                    height = image.Height.ToString();
                    width = image.Width.ToString();
                    image.Dispose();
                }
                else
                {
                    ThemeSelectedFileName = string.Empty;

                }

                string updateSqlResponse = DataManager.Controller.Users.InsertCustomTheme(ThemeSelectedFileName, bgAppName, height, width, selectedTheme, appBackgroundColor, appTittlebarColor, appFontColor);
                if (string.IsNullOrEmpty(updateSqlResponse))
                {
                    if (bgAppName != "WEB")
                    {
                        string auditMessage = "Updated default theme to selected theme successfully";
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "UPDATE_TO_SELECT");
                        if (FileUploadCustomTheme.HasFile)
                        {
                            //Generate OverlayImages
                            GenerateWallPaper(ThemeCombainPath);
                        }
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                        BindCustomImageValues();
                        return;
                    }
                    else
                    {
                        Response.Redirect("~/Administration/CustomTheme.aspx");
                    }
                }
                else
                {
                    string auditMessage = "Failed to updated default theme to selected theme ";
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, auditMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FAILED_UPDATE_SELECT");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                    BindCustomImageValues();
                    return;
                }

            }
            else
            {

            }
        }
        catch
        {

        }
    }
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        BindCustomImageValues();
        divBrowseCustomWallPaper.Visible = false;
        ImageButtonEdit.Enabled = true;
        ImageButtonAdd.Enabled = true;
    }
    protected void DropDownListImageType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListImageType.SelectedItem.Value != "-1")
        {
            BindAppThemes(DropDownListImageType.SelectedItem.Value);
        }
        BindRecommandedValues();
    }
    private void BindRecommandedValues()
    {
        try
        {
            LabelFilesize.Text = LabelHeightWidth.Text = LabelNotes.Text = LabelFileSizeValue.Text = LabelValues.Text = string.Empty;
            string bgAppName = DropDownListImageType.SelectedItem.Value;
            if (bgAppName != "-1")
            {
                DbDataReader wallPaperDefaultValues = DataManager.Provider.Users.ProvideMaximumWallPaperSize(bgAppName);
                while (wallPaperDefaultValues.Read())
                {  

                    LabelFilesize.Text = "1. " + Localization.GetLabelText("", Session["selectedCulture"] as string, "MAXIMUM_FILESIZE");
                    LabelFileSizeValue.Text = wallPaperDefaultValues["BG_ALLOWED_SIZE_KB"].ToString() + " KB.";

                    LabelHeightWidth.Text = "2.  " + Localization.GetLabelText("", Session["selectedCulture"] as string, "WALLPAPER_HEIGHT_AND_WIDTH_SHOULD_BE");                                       
                    LabelValues.Text = wallPaperDefaultValues["BG_ACT_WIDTH"].ToString() + " " + "X" + " " + wallPaperDefaultValues["BG_ACT_HEIGHT"].ToString();

                    LabelNotes.Text = "3. " + Localization.GetLabelText("", Session["selectedCulture"] as string,"USE_RECOMMENDED_HEIGHT_AND_WIDTH");
                    LabelNotes.Text = wallPaperDefaultValues["USE_RECOMMENDED_HEIGHT_AND_WIDTH"].ToString();
                }
                wallPaperDefaultValues.Close();

            }
            BindCustomImageValues();
        }
        catch
        {

        }
    }
    protected void ImageButtonAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BindCustomImageValues();
            divBrowseCustomWallPaper.Visible = true;
            DropDownListImageType.SelectedValue = "-1";
            DropDownListApplicationTheme.SelectedValue = "-1";
            ImageButtonEdit.Enabled = false;
            
        }
        catch
        {

        }
    }
    protected void ImageButtonDelete_Click(object sender, ImageClickEventArgs e)
    {
        string selectedWallPapers = Request.Form["__CUSTOMID"];
        DeleteCustomWallPaper(selectedWallPapers);

    }
    protected void ImageButtonEdit_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            string appName = Request.Form["__CUSTOMID"];
            BindCustomThemeValues(appName);
            divBrowseCustomWallPaper.Visible = true;
            ImageButtonAdd.Enabled = false;
        }
        catch
        {

        }
    }
    private void BindCustomThemeValues(string appName)
    {
        try
        {

            DbDataReader drCustomThemValues = DataManager.Provider.Users.ProvideThemeImageValuesByAppName(appName);
            while (drCustomThemValues.Read())
            {

                BindCustomImages();
                BindAppThemes(drCustomThemValues["BG_APP_NAME"].ToString());
                DropDownListImageType.SelectedValue = drCustomThemValues["BG_APP_NAME"].ToString();
                DropDownListApplicationTheme.SelectedValue = drCustomThemValues["APP_THEME"].ToString();
                BindRecommandedValues();
            }
            drCustomThemValues.Close();
        }
        catch
        {

        }
    }
    private void DeleteCustomWallPaper(string themeName)
    {
        try
        {
            string auditorSource = HostIP.GetHostIP();
            if (!string.IsNullOrEmpty(themeName))
            {

                //Deleting uploaded wall paper in ACM and EAM
                DeleteSelectedWallPaperOverlayImages(themeName);
                string updateSqlResponse = DataManager.Controller.Users.UpdateCustomThemeDetails(themeName);
                if (string.IsNullOrEmpty(updateSqlResponse))
                {
                    Response.Redirect("~/Administration/CustomTheme.aspx");
                    //string auditMessage = "Updated custom Them  to default theme successfully";
                    //LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                    //string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "UPDATE_TO_DEFAULT");
                    //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                    //BindCustomImageValues();
                    //return;

                }
                else
                {
                    string auditMessage = "Failed to update custom them to default theme";
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, auditMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FAILED_UPDATE_DEFAULT");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                    BindCustomImageValues();
                    return;

                }
            }
        }
        catch
        {

        }
    }
    private void DeleteSelectedWallPaperOverlayImages(string themeName)
    {


        string[] customFileName = { "Card_CustomAppBG.jpg", "CustomAppBG.jpg", "Inside_CustomAppBG.jpg", "JobStatus_CustomAppBG.jpg", "JobList_CustomAppBG.jpg" };

        string[] selectedWallPapers = themeName.Split(',');
        for (int i = 0; i < selectedWallPapers.Length; i++)
        {
            for (int customFile = 0; customFile < customFileName.Length; customFile++)
            {
                deleteCustomFiles(selectedWallPapers[i].ToString(), customFileName[customFile].ToString());
            }

        }
    }
    private void deleteCustomFiles(string MFPModel, string selectedFile)
    {
        string[] themeNames = { "Green", "Blue", "Black" };

        for (int themeValue = 0; themeValue < themeNames.Length; themeValue++)
        {

            string themesFolderEAM = ConfigurationManager.AppSettings["EAMThemesFolder"];
            string themesFolderACM = ConfigurationManager.AppSettings["ACMThemesFolder"];
            string EAMCustomWallPaperFullPath = themesFolderEAM + "\\App_UserData\\WallPapers\\" + themeNames[themeValue].ToString() + "\\" + MFPModel + "\\" + selectedFile;
            string ACMCustomWallPaperFullPath = themesFolderACM + "\\App_UserData\\WallPapers\\" + themeNames[themeValue].ToString() + "\\" + MFPModel + "\\" + selectedFile;
            if (File.Exists(EAMCustomWallPaperFullPath))
            {
                File.Delete(EAMCustomWallPaperFullPath);
            }
            if (File.Exists(ACMCustomWallPaperFullPath))
            {
                File.Delete(ACMCustomWallPaperFullPath);
            }
        }
    }
}
