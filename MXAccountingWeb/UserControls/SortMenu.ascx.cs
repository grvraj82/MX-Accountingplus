using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingPlusWeb.UserControls
{
    public partial class SortMenu : System.Web.UI.UserControl
    {
        private Delegate _delUserId;
        /// <summary>
        /// Sets the page method with param ref.
        /// </summary>
        /// <value>
        /// The page method with param ref.
        /// </value>
        public Delegate PageMethodWithParamRef
        {
            set { _delUserId = value; }
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the MenuItemClick event of the Menu1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.MenuEventArgs"/> instance containing the event data.</param>
        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            string userId = Menu1.SelectedValue;
            object[] obj = new object[1];
            obj[0] = userId as object;
            _delUserId.DynamicInvoke(obj);
        }
    }
}