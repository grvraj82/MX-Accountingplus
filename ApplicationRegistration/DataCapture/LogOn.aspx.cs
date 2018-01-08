#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: LogOn.aspx.cs
  Description: Log On Screen
  Date Created : June 12, 07

 */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  Sept 09, 07         Rajshekhar D
*/
#endregion

using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Text;


namespace ApplicationRegistration.DataCapture
{
    public partial class LogOn : System.Web.UI.Page
    {

        /// <summary>
        /// The Method that get called on Page Load
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>

        protected void Page_Load(object sender, EventArgs eventArgument)
        {
            TextBoxLogOnID.Focus();
        }

        /// <summary>
        /// The Method that get called on LogOn Button click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ButtonLogOn_Click(object sender, EventArgs eventArgument)
        {
            // Clear Action Message
            LabelActionMessage.Text = Resources.Labels.BlankText;

            // Display failure message, if user provides invalid User Id or Password
            if (string.IsNullOrEmpty(TextBoxLogOnID.Text))
            {
                LabelActionMessage.Text = Resources.FailureMessages.InvalidIdPassword;
                return;
            }


            #region Validate user credentials

            // Get user details for signed in user
            SqlDataReader drUser = DataProvider.GetUsers(TextBoxLogOnID.Text);
            string preferredProduct = null;
            if (drUser.HasRows)
            {
                #region update Last Access DateTime
                DataController.UpdateLastAccessTime(TextBoxLogOnID.Text);
                #endregion

                drUser.Read();

                #region Check wether Access for the user allowed or not.
                if (!(bool)drUser["REC_ACTIVE"])
                {
                    LabelActionMessage.Text = Resources.FailureMessages.AceessDenied;
                    drUser.Close();
                    return;
                }
                #endregion

                #region Validate pasword
                if (drUser["USR_PASSWORD"].ToString() != DataProvider.GetHashedPassword(TextBoxLogOnID.Text, TextBoxLogOnPassword.Text))
                {
                    LabelActionMessage.Text = Resources.FailureMessages.InvalidIdPassword;
                    drUser.Close();
                    return;
                }
                #endregion

                #region Store user information in Session
                Session.Clear();


                Session.Add("UserSystemID", drUser["USR_SYSID"].ToString());
                Session.Add("UserID", drUser["USR_ID"].ToString());
                Session.Add("UserName", drUser["USR_NAME"].ToString());
                Session.Add("UserEmail", drUser["USR_EMAIL"].ToString());

                // Get the role of the signedIn user
                DataSet dsSignedInUserRoles = DataProvider.GetSignedInUserRoles(drUser["USR_SYSID"].ToString());
                Session.Add("UserRoles", dsSignedInUserRoles);
                Session.Add("RoleID", 0);

                // Get the redistributor of the signedIn user
                DataSet dsSignedInUserRedistributor = DataProvider.GetSignedInUserRedistributor(drUser["USR_SYSID"].ToString());
                if (dsSignedInUserRedistributor.Tables[0].Rows.Count > 0)
                {
                    // Get the redistributor Products of the signedIn user
                    string redistributor = dsSignedInUserRedistributor.Tables[0].Rows[0]["REDIST_SYSID"].ToString();
                    DataSet dsSignedInUserRedistributorProduct = DataProvider.GetSignedInUserRedistributorProduct(redistributor);

                    // Get the redistributor product limits of the signedIn user
                    string redistributorProdut = dsSignedInUserRedistributorProduct.Tables[0].Rows[0]["PRDCT_ID"].ToString();
                    string redistributorProdutCode = dsSignedInUserRedistributorProduct.Tables[0].Rows[0]["PRDCT_CODE"].ToString();
                    string redistributorProdutVersion = dsSignedInUserRedistributorProduct.Tables[0].Rows[0]["PRDCT_VERSION"].ToString();
                    Session.Add("Redistributor", redistributor);
                    Session.Add("UserPrdctId", redistributorProdut);
                    Session.Add("UserPrdctCode", redistributorProdutCode);
                    Session.Add("UserPrdctVersion", redistributorProdutVersion);
                    DataSet dsSignedInRedistributorProductLimit = DataProvider.GetSignedInUserProductLimit(redistributorProdut);


                    string limit = dsSignedInRedistributorProductLimit.Tables[0].Rows[0]["LIMITS"].ToString();
                    // Protal Admin / Product Admin Roles
                    Session.Add("isPortalAdmin", false);
                    Session.Add("isProductAdmin", false);
                    Session.Add("isProductCSR", false);


                    preferredProduct = drUser["USR_PREFERRED_PRODUCT"].ToString();
                    if (string.IsNullOrEmpty(preferredProduct) == true || preferredProduct == "0")
                    {
                        // Get the first product in the product List
                        SqlDataReader drProducts = DataProvider.GetProductList(Session["UserSystemId"].ToString());
                        if (drProducts != null && drProducts.HasRows)
                        {
                            drProducts.Read();
                            preferredProduct = drProducts["PRDCT_ID"].ToString();
                        }

                        drProducts.Close();
                    }
                    Session["SelectedProduct"] = preferredProduct;
                #endregion

                    #region Redirect to the respective pages, depending on user role
                    if (dsSignedInUserRoles.Tables[0].Rows.Count > 0)
                    {

                        Session["RoleID"] = int.Parse(dsSignedInUserRoles.Tables[0].Rows[0]["ROLE_ID"].ToString(), CultureInfo.InvariantCulture);

                        // Get the role id for the preferred Product
                        if (!string.IsNullOrEmpty(preferredProduct) && preferredProduct != "0")
                        {
                            DataRow[] drRoleArray = dsSignedInUserRoles.Tables[0].Select("PRDCT_ID='" + preferredProduct + "'");
                            if (drRoleArray != null && drRoleArray.Length > 0)
                            {
                                int roleID = int.Parse(drRoleArray[0]["ROLE_ID"].ToString(), CultureInfo.InvariantCulture);
                                Session["RoleID"] = roleID;
                            }
                        }

                        DataRow[] drPortalAdmin = dsSignedInUserRoles.Tables[0].Select("ROLE_CODE = 'PORTALADMIN'");
                        if (drPortalAdmin.Length > 0)
                        {
                            Session["isPortalAdmin"] = true;
                            Response.Redirect("../DataCapture/Products.aspx");
                            //Response.Redirect("../DataCapture/SelectProduct.aspx");
                        }
                        else
                        {
                            // Check wether signed in user is having Product Admin Role
                            DataRow[] drProductAdmin = dsSignedInUserRoles.Tables[0].Select("ROLE_CODE = 'PRODUCTADMIN' and PRDCT_ID='" + preferredProduct + "'");
                            if (drProductAdmin.Length > 0)
                            {
                                Session["isProductAdmin"] = true;
                            }

                            DataRow[] drProductCsr = dsSignedInUserRoles.Tables[0].Select("ROLE_CODE = 'PRODUCTCR' and PRDCT_ID='" + preferredProduct + "'");
                            if (drProductCsr.Length > 0)
                            {
                                Session["isProductCSR"] = true;
                            }
                            Response.Redirect("../Views/RegistrationDetails.aspx");
                        }
                    }
                    else
                    {
                        LabelActionMessage.Text = Resources.FailureMessages.NoRolesAssiged;
                    }
                }
                else
                {
                    LabelActionMessage.Text = "No Redistributors assgined";
                }
                      
                #endregion
            }
                     
            else
            {
                LabelActionMessage.Text = Resources.FailureMessages.InvalidIdPassword;
            }
            drUser.Close();
            #endregion

        }


    }
}
