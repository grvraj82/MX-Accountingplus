using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Globalization;
using System.Timers;
using System.Configuration;

namespace ScreenCastClient
{

    class Program
    {
        private Timer screenCaptureTimer = null;
        private string screenCaptureDestination = ConfigurationManager.AppSettings["ScreenCaptureDestination"];
        public static void Main(string[] args)
        {
            Console.WriteLine("Launching Screen Capute...");
            Program program = new Program();
            program.InitializeTimer();
            Console.ReadLine();
        }

        private void InitializeTimer()
        {
            try
            {
                int screenCaptureInterval = int.Parse(ConfigurationManager.AppSettings["ScreenCaptureInterval"]);
                screenCaptureTimer = new Timer();
                screenCaptureTimer.Enabled = true;
                screenCaptureTimer.AutoReset = true;
                screenCaptureTimer.Interval = screenCaptureInterval;
                screenCaptureTimer.Elapsed += new ElapsedEventHandler(CaptureUserScreen);

                
            }
            catch (Exception ex)
            {
                
            }
        }

        private Bitmap ResizeBitmap(Bitmap sourceBMP, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                //g.InterpolationMode = InterpolationMode.Low;
                g.DrawImage(sourceBMP, 0, 0, width, height);
            }
            return result;
        }

        private void CaptureUserScreen(object sender, ElapsedEventArgs e)
        {
            try
            {
                //Console.WriteLine("Screen Capture Started at " + DateTime.Now.ToString("U"));
                //picScreen.Image = CaptureScreen.GetDesktopImage();
                Bitmap bitmap = ResizeBitmap(CaptureScreenLibrary.CaptureScreen.GetDesktopImage(), 200 * 4, 200 * 3);
               
                //Bitmap bitmap = CaptureScreenLibrary.CaptureScreen.GetDesktopImage();
                
                DateTime currentDateTime = DateTime.Now;

                string year = currentDateTime.Year.ToString();
                string month = currentDateTime.Month.ToString();
                string day = currentDateTime.Day.ToString();

                string hour = currentDateTime.Hour.ToString();
                string minute = currentDateTime.Minute.ToString();
                string second = currentDateTime.Second.ToString();

                if (month.Length == 1)
                {
                    month = "0" + month;
                }

                if (day.Length == 1)
                {
                    day = "0" + day;
                }

                if (hour.Length == 1)
                {
                    hour = "0" + hour;
                }

                if (minute.Length == 1)
                {
                    minute = "0" + minute;
                }
                               
                if (second.Length == 1)
                {
                    second = "0" + second;
                }
                string destinationDirectory = string.Format("{0}/{1}/{2}/{3}/{4}", year, month, day, hour, minute);

                destinationDirectory = Path.Combine(screenCaptureDestination, destinationDirectory);
                if (!Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                }

                string screenCaptureDestinationPath = Path.Combine(destinationDirectory, second + ".png");
                bitmap.Save(screenCaptureDestinationPath, System.Drawing.Imaging.ImageFormat.Png);
                Console.WriteLine("Captured Screen at " + DateTime.Now.ToString("U"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
