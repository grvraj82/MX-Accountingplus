using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountingPlusDevice
{
    public partial class WallPaper : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AccountingPlusDevice.AppCode.ApplicationHelper.ClearSqlPools();
            GenerateWallPaper();
        }

        private void GenerateWallPaper()
        {
             
            DataTable dtImages = new DataTable();

            dtImages.Columns.Add("FILE_PATH", typeof(string));
            dtImages.Columns.Add("POSITION_X", typeof(int));
            dtImages.Columns.Add("POSITION_Y", typeof(int));

            string rootFolder = Server.MapPath("~");

            string finalOverlayedImage = rootFolder + "/App_Temp/Background.png";

            string backgroundImage = rootFolder + "/App_Temp/LondonBridge.jpg";
            string overlayImage = rootFolder + "/App_Temp/Overlay_Login.png";

            dtImages.Rows.Add(backgroundImage, 0, 0);
            dtImages.Rows.Add(overlayImage, 0, 0);
            dtImages.Rows.Add(rootFolder + "/App_Temp/Logo.png", 150, 100);
            dtImages.Rows.Add(rootFolder + "/App_Temp/MFP 173x155.png", 475, 200);

            string finalPath = DataManager.OverlayImages.CreateOverlayImages(dtImages, finalOverlayedImage, 1030, 590);

            WallPaperImage.ImageUrl = finalPath;

        }
    }
}