using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationBase;
using System.IO;
using System.Text;
using System.Data;

namespace AccountingPlusWeb.Administration
{
    public partial class CurrencySettings : ApplicationBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SelectCurrenySymbol();
            LinkButton jobConfiguration = (LinkButton)Master.FindControl("LinkButtonCurrency");
            if (jobConfiguration != null)
            {
                jobConfiguration.CssClass = "linkButtonSelect_Selected";
            }
            if (!IsPostBack)
            {
                GetCurrencyDetails();
            }
        }

        private void GetCurrencyDetails()
        {
            string selectedType = string.Empty;;
            DataSet dsCurrency = new DataSet();
            dsCurrency = DataManager.Provider.Settings.ProvideCurrency();

            string currImg = string.Empty;
            string currTxt = string.Empty;
            string curAppend = string.Empty;
            if (dsCurrency.Tables[0].Rows.Count > 0)
            {
                currTxt = dsCurrency.Tables[0].Rows[0]["CUR_SYM_TXT"].ToString();
                currImg = dsCurrency.Tables[0].Rows[0]["CUR_SYM_IMG"].ToString();
                curAppend = dsCurrency.Tables[0].Rows[0]["CUR_APPEND"].ToString();
            }
            if (!string.IsNullOrEmpty(curAppend))
            {
                DropDownListAppend.SelectedValue = curAppend;
            }
            if (!string.IsNullOrEmpty(currTxt) && string.IsNullOrEmpty(currImg))
            {
                DropDownListCurrencySymbol.SelectedValue = "text";
                selectedType = DropDownListCurrencySymbol.SelectedValue;
               
                SelectCurrenySymbol();
            }
            if (!string.IsNullOrEmpty(currImg) && string.IsNullOrEmpty(currTxt))
            {
                DropDownListCurrencySymbol.SelectedValue = "image";
                selectedType = DropDownListCurrencySymbol.SelectedValue;
                SelectCurrenySymbol();
            }
            if (selectedType.ToLower() == "text")
            {
                if (dsCurrency.Tables[0].Rows.Count > 0)
                {
                    TextboxSumbolText.Text = dsCurrency.Tables[0].Rows[0]["CUR_SYM_TXT"].ToString();
                }
            }
            else
            {
                if (dsCurrency.Tables[0].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(currImg))
                    {
                        string path = (Server.MapPath("~/") + "App_UserData\\Currency\\");

                        if (Directory.Exists(path))
                        {
                            DirectoryInfo downloadedInfo = new DirectoryInfo(path);
                            foreach (FileInfo file in downloadedInfo.GetFiles())
                            {
                                ImageCurrency.Visible = true;
                                ImageCurrency.ImageUrl = "../App_UserData/Currency/" + file.Name;
                                break;
                            }

                        }
                    }
                }
            }
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            string selectedSymbolType = DropDownListCurrencySymbol.SelectedValue;
            string symbolText = TextboxSumbolText.Text;
            string curappend = DropDownListAppend.SelectedValue;
            //byte[] bytes = Encoding.Default.GetBytes(symbolText);
            //symbolText = Encoding.UTF8.GetString(bytes);

            string path = string.Empty;
            if (FileUpload1.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(FileUpload1.FileName);
                    path = (Server.MapPath("~/") + "App_UserData\\Currency\\");

                    if (Directory.Exists(path))
                    {
                        DirectoryInfo downloadedInfo = new DirectoryInfo(path);
                        foreach (FileInfo file in downloadedInfo.GetFiles())
                        {
                            file.Delete();
                        }
                        foreach (DirectoryInfo dir in downloadedInfo.GetDirectories())
                        {
                            dir.Delete(true);
                        }
                        path = path + filename;
                        FileUpload1.SaveAs(path);
                        symbolText = string.Empty;
                    }
                    else
                    {
                        Directory.CreateDirectory(path);
                        FileUpload1.SaveAs(path + filename);
                        symbolText = string.Empty;
                    }

                }
                catch (Exception ex)
                {

                }
            }
            string message = DataManager.Controller.Settings.AddCurrencySettings(selectedSymbolType, symbolText, path, curappend);

            if (string.IsNullOrEmpty(message))
            {
                string serverMessage = "Currency settings updated successfully! ";
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
            }
            else
            {
                string serverMessage = "Failed to update Currency settings";
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
            }
            GetCurrencyDetails();
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {

        }



        protected void DropDownListCurrencySymbol__SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectCurrenySymbol();
        }

        private void SelectCurrenySymbol()
        {
            string selectedValue = DropDownListCurrencySymbol.SelectedValue;

            if (selectedValue.ToString() == "text")
            {
                trSymbolText.Visible = true;
                trSymbolImage.Visible = false;
            }
            else
            {
                trSymbolText.Visible = false;
                trSymbolImage.Visible = true;
            }
        }
    }
}