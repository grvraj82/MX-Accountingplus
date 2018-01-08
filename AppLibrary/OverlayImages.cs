using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System;
using System.Data;
using System.IO;

namespace DataManager
{
    
    public class OverlayImages
    {
        /// 
        /// Generate Overlay Images 
        /// 
        /// "workingDir">The working dir.
        /// "imagePath">Base image
        /// "transparentImagePath">Overlay image
        /// 
        /// path to generated Overlay image
        /// 
        public static string CreateOverlayImages(string sourceImagePath, string transparentOverlayImagePath, string destinationImagePath, int positionX, int positionY, int resizeWidth, int resizeHeight)
        {
            string returnValue = string.Empty;

            try
            {
                string finalImagePath;
                string fullPath = string.Empty;

                //create a image object containing the photograph to watermark
                fullPath = sourceImagePath;
                finalImagePath = destinationImagePath;

                // create a bitmap with new PixelFormal and bitmap of actual image to draw from to resolve issue
                // where "A Graphics object cannot be created from an image that has an indexed pixel format."
                Bitmap bmTemp = null;

                using (Bitmap image = new Bitmap(fullPath))
                {
                    bmTemp = (Bitmap)image.Clone();
                    
                    image.Dispose();
                    //bmTemp = image;
                } 

                Bitmap bmPhoto = new Bitmap(resizeWidth, resizeHeight, PixelFormat.Format16bppRgb555);
                if (resizeWidth > 0 && resizeHeight > 0)
                {
                    Size imageSize = new Size(resizeWidth, resizeHeight);
                    bmTemp = ResizeBitmap(bmTemp, imageSize, false);

                    //bmTemp = (Bitmap)ResizeImage(bmTemp, imageSize, true);
                }
                //load the Bitmap into a Graphics object 
                Graphics grPhoto = Graphics.FromImage(bmPhoto);

                //Set the rendering quality for this Graphics object
                grPhoto.SmoothingMode = SmoothingMode.AntiAlias;

                //Draws the photo Image object at original size to the graphics object.
                grPhoto.DrawImage(
                    bmTemp,							   // Photo Image object
                    new Rectangle(0, 0, bmTemp.Width, bmTemp.Height), // Rectangle structure
                    0,									  // x-coordinate of the portion of the source image to draw. 
                    0,									  // y-coordinate of the portion of the source image to draw. 
                    bmTemp.Width,								// Width of the portion of the source image to draw. 
                    bmTemp.Height,							   // Height of the portion of the source image to draw. 
                    GraphicsUnit.Pixel);					// Units of measure 

                //create a Bitmap the Size of the original photograph
                //fullPath = transparentOverlayImagePath;
                Bitmap bmOver = new Bitmap(transparentOverlayImagePath);

                using (Bitmap image = new Bitmap(transparentOverlayImagePath))
                {
                    bmOver = (Bitmap)image.Clone();
                    image.Dispose();
                } 

                //Draws the photo Image object at original size to the graphics object.
                grPhoto.DrawImage(
                    bmOver,							   // Photo Image object
                    new Rectangle(positionX, positionY, bmOver.Width, bmOver.Height), // Rectangle structure
                    0,									  // x-coordinate of the portion of the source image to draw. 
                    0,									  // y-coordinate of the portion of the source image to draw. 
                    bmOver.Width,								// Width of the portion of the source image to draw. 
                    bmOver.Height,							   // Height of the portion of the source image to draw. 
                    GraphicsUnit.Pixel);					// Units of measure 

                grPhoto.Dispose();
                
                bmPhoto.Save(finalImagePath.ToString(), ImageFormat.Jpeg);
                
                //using (MemoryStream ms = new MemoryStream())
                //{
                //    using (FileStream fs = new FileStream(finalImagePath, FileMode.OpenOrCreate))
                //    {
                //        bmPhoto.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                //        ms.WriteTo(fs);
                //        fs.Close();
                //        fs.Dispose();
                //    }
                //}

                bmTemp.Dispose();
                bmPhoto.Dispose();
                bmOver.Dispose();

                returnValue = finalImagePath;
            }
            catch (Exception ex)
            {
                returnValue = ex.Message;
            }
            return returnValue;
        }
     
        public static string CreateOverlayImages(DataTable dtImageSources, string finalImagePath, int resizeWidth, int resizeHeight)
        {
            string overLayedImagePath = null;
            if (dtImageSources != null && dtImageSources.Rows.Count >= 2)
            {
                string sourceImagePath = dtImageSources.Rows[0]["FILE_PATH"] as string;
                string transparentOverlayImagePath = dtImageSources.Rows[1]["FILE_PATH"] as string;
                int positionX = int.Parse(dtImageSources.Rows[1]["POSITION_X"].ToString());
                int positionY= int.Parse(dtImageSources.Rows[1]["POSITION_Y"].ToString());
                
                string overlayedImage = CreateOverlayImages(sourceImagePath, transparentOverlayImagePath, finalImagePath, 0, 0, resizeWidth, resizeHeight);
               
                for (int imageIndex = 2; imageIndex < dtImageSources.Rows.Count; imageIndex++)
                {
                    positionX = int.Parse(dtImageSources.Rows[imageIndex]["POSITION_X"].ToString());
                    positionY = int.Parse(dtImageSources.Rows[imageIndex]["POSITION_Y"].ToString());

                    transparentOverlayImagePath = dtImageSources.Rows[imageIndex]["FILE_PATH"] as string;

                    //finalImagePath = finalImagePath.Replace(".png", imageIndex.ToString() + "_.png");
                    overlayedImage = CreateOverlayImages(overlayedImage, transparentOverlayImagePath, finalImagePath, positionX, positionY, resizeWidth, resizeHeight);
                }
                overLayedImagePath = overlayedImage;
            }
            return overLayedImagePath;
        }


        public static Bitmap ResizeBitmap(Bitmap sourceBMP, Size size, bool preserveAspectRatio = true)
        {
            int newWidth;
            int newHeight;
            if (preserveAspectRatio)
            {
                int originalWidth = sourceBMP.Width;
                int originalHeight = sourceBMP.Height;
                float percentWidth = (float)size.Width / (float)originalWidth;
                float percentHeight = (float)size.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            }
            else
            {
                newWidth = size.Width;
                newHeight = size.Height;
            }

            Bitmap result = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.DrawImage(sourceBMP, 0, 0, newWidth, newHeight);
                g.Dispose();
            }
            sourceBMP.Dispose();
            return result;
        }


        public void ResizeValues(int width, int height)
        {

        }

        public static Image ResizeImage(Image image, Size size,  bool preserveAspectRatio = true)
        {
            int newWidth;
            int newHeight;
            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = (float)size.Width / (float)originalWidth;
                float percentHeight = (float)size.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            }
            else
            {
                newWidth = size.Width;
                newHeight = size.Height;
            }
            Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }
    }
}
