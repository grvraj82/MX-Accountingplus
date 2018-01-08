using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Timers;
using OsaDirectManager.Osa.MfpWebService;

namespace AccountingPlusDevice
{
    public partial class Nop : System.Web.UI.Page
    {

        private int countdown = 20; 
        private System.Timers.Timer timer;
        private MFPCoreWS mfpWebService;

        protected void Page_Load(object sender, EventArgs e)
        {
            AccountingPlusDevice.AppCode.ApplicationHelper.ClearSqlPools();
            SetCounter();
        }

        private void SetCounter()
        {
            // Create the timer object.
            timer = new System.Timers.Timer(30000);
            // Make it repeat. Setting this to false will cause just one event.
            timer.AutoReset = true;
            // Assign the delegate method.
            timer.Elapsed += new ElapsedEventHandler(ProcessTimerEvent);
            // Start the timer.
            timer.Start();
            // Just wait.
            //Console.WriteLine("Waiting for countdown");
            //Console.ReadLine();
        }

        // Method assigned to the timer delegate.
        private void ProcessTimerEvent(Object obj, ElapsedEventArgs e)
        {
            --countdown;
            // If countdown has reached 0, it's time to exit.
            try
            {
                if (countdown == 0 || string.IsNullOrEmpty(Session["FileTransferred"] as string) == true)
                {
                    timer.Stop();
                    timer.Close();
                }
                else
                {
                    MakeDummyRequest();
                }
            }
            catch (Exception ex)
            {
                //countdown = 0;
                //timer.Stop();
                //timer.Close();
            }
        }

        private void MakeDummyRequest()
        {
            bool create = CreateWS();

            if (create)
            {
                try
                {
                    XML_DOC_DSC_TYPE xmlDoc = new XML_DOC_DSC_TYPE();
                    ARG_SETTABLE_TYPE arg = new ARG_SETTABLE_TYPE();
                    arg.Item = (E_MFP_JOB_TYPE)E_MFP_JOB_TYPE.PRINT;
                    xmlDoc = mfpWebService.GetJobSettableElements(arg, ref OsaDirectManager.Core.g_WSDLGeneric);

                }
                catch (Exception)
                {

                }
            }

        }

        private bool CreateWS()
        {
            try
            {
                string mfpIPAddress = Request.Params["REMOTE_ADDR"].ToString();
                string mfpUri = OsaDirectManager.Core.GetMFPURL(mfpIPAddress);
                mfpWebService = new MFPCoreWS();
                mfpWebService.Url = mfpUri;
                SECURITY_SOAPHEADER_TYPE securityHeader = new SECURITY_SOAPHEADER_TYPE();
                securityHeader.licensekey = OsaDirectManager.Core.OSA_LICENSE_KEY;
                mfpWebService.Security = securityHeader;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
