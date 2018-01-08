using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationBase;
using AccountingPlusWeb.MasterPages;
using System.Data;

namespace AccountingPlusWeb.Administration
{
    public partial class GeneratePinnumber : ApplicationBasePage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            GetUserPinCountDetails();
        }

        private void GetUserPinCountDetails()
        {
            DataSet dsUserPinCountDetails = DataManager.Controller.Users.GetUserPinCountDetails();
            LabelTotalUser.Text = dsUserPinCountDetails.Tables[0].Rows[0]["count"].ToString();
            LabelTotalUserWithPin.Text = dsUserPinCountDetails.Tables[1].Rows[0]["count"].ToString();
            LabelTotalUserWithPinAD.Text = dsUserPinCountDetails.Tables[2].Rows[0]["count"].ToString();
            LabelTotalUserWithoutPin.Text = dsUserPinCountDetails.Tables[3].Rows[0]["count"].ToString();
            LabelTotalUserWithoutPinAD.Text = dsUserPinCountDetails.Tables[4].Rows[0]["count"].ToString();
            LabelTotalUserAD.Text = dsUserPinCountDetails.Tables[5].Rows[0]["count"].ToString();
        }

        /// <summary>
        /// Handles the Click event of the ButtonGenerate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ButtonGenerate_Click(object sender, EventArgs e)
        {
            GenerateUserPin();
            GetUserPinCountDetails();
        }

        /// <summary>
        /// Generates the user pin.
        /// </summary>
        private void GenerateUserPin()
        {
            bool isDBselected = CheckBoxDB.Checked;
            bool isADselected = CheckBoxAD.Checked;
            string selectedSource = string.Empty;
            if (isDBselected)
            {
                selectedSource = "DB"; 
            }
            if (isADselected)
            {
                selectedSource = "AD"; 
            }
            if (isADselected && isDBselected)
            {
                selectedSource = "AD,DB";
            }
            try
            {
                string sqlResponse = DataManager.Controller.Users.GenerateUserPin(selectedSource);
                if (String.IsNullOrEmpty(sqlResponse))
                {
                    string serverMessage = "Pin number(s) generated succesfully";
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                }
            }

            catch (Exception ex)
            {

            }

            try
            {
                string response = DataManager.Controller.Users.EncryptPin();
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

        /// <summary>
        /// Handles the Click event of the ImageButtonBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Administration/ManageUsers.aspx");
        }


        /// <summary>
        /// Handles the Click event of the ButtonCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/ManageUsers.aspx");
        }
    }
}