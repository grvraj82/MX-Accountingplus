

#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Bhaskar Pujari, Rajshekhar D
  File Name: Enums.cs
  Description: Database Connection 
  Date Created : Sept 2010
  */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.          
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Threading;
using System.IO;

namespace MFPDiscovery
{
    
    public partial class MFPDiscoverer : IDisposable
    {
        private IntPtr _handle = IntPtr.Zero;
        private BackgroundWorker _discoveryWorker;
        private DiscoveryType _discoveryMode = DiscoveryType.SNMP;

        public event EventHandler<MfpDiscoveredEventArgs> MfpDiscovered;
        public event EventHandler<EventArgs> DiscoverMfpsAsyncCompleted;
        //private static MFPDiscoverer self = new MFPDiscoverer();

        public bool IsInitialized
        {
            get
            {
                
                return !IntPtr.Equals(_handle, IntPtr.Zero);
            }
        }

        public Boolean IsBusy
        {
            get
            {
                return _discoveryWorker.IsBusy;
            }
        }

        public Boolean IsCancellationPending
        {
            get
            {
                return _discoveryWorker.CancellationPending;
            }
        }

        public DiscoveryType DiscoveryMode
        {
            get
            {
                return _discoveryMode;
            }

            set
            {
                MFPCOMM_ERROR mfpCommError = MFPComm_SetDiscoveryType(_handle, value);
                if (mfpCommError == MFPCOMM_ERROR.MFPCOMM_OK)
                {
                    _discoveryMode = value;
                }
            }
        }

        /// <summary>
        /// Returns the number of MFPs currently discovered
        /// </summary>
        public int Count
        {
            get
            {
                if (!IsInitialized)
                {
                    throw new NullReferenceException();
                }

                return MFPComm_GetDiscoverMFPCount(_handle);
            }
        }


        //private MFPDiscoverer()
        //{

        //}


        //public static MFPDiscoverer Discoverer()
        //{
        //    return self;
        //}
        /// <summary>
        /// Method to initialize memory and resources required internally. 
        /// </summary>
        /// <returns></returns>
        public bool Initialize()
        {
            if (IsInitialized)
            {
                return true;
            }

            try
            {
                _handle = MFPComm_Initialize();

                _discoveryWorker = new BackgroundWorker();
                _discoveryWorker.WorkerSupportsCancellation = true;

                _discoveryWorker.DoWork += new DoWorkEventHandler(DiscoveryDoWork);
                _discoveryWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(DiscoveryCompleted);
            }
            catch (Exception e)
            {

            }

            return IsInitialized;
        }

        public void DiscoverMfpsAsync()
        {
            if (!IsInitialized)
            {
                throw new NullReferenceException();
            }

            if (IsBusy)
            {
                // throw Exception
            }

            _discoveryWorker.RunWorkerAsync(new DiscoveryWorkerArgument(DiscoverMfp.All, null));
        }

        public void DiscoverMfpsAsync(string startMfpIP, string endMfpIP)
        {
            if (!IsInitialized)
            {
                throw new NullReferenceException();
            }

            if (IsBusy)
            {
                // throw Exception
            }

            _discoveryWorker.RunWorkerAsync(new DiscoveryWorkerArgument(DiscoverMfp.InRange, startMfpIP, endMfpIP));
        }

        public void DiscoverMfpAsync(string mfpIP)
        {
            if (string.IsNullOrEmpty(mfpIP))
            {
                throw new ArgumentException();
            }

            if (!IsInitialized)
            {
                throw new NullReferenceException();
            }

            if (IsBusy)
            {
                // throw Exception
            }


            _discoveryWorker.RunWorkerAsync(new DiscoveryWorkerArgument(DiscoverMfp.IP, mfpIP));
        }

        public bool DiscoverAsyncCancel()
        {
            if (!IsBusy || IsCancellationPending)
            {
                return true;
            }

            MFPCOMM_ERROR mfpCommError = MFPComm_CancelDiscovery(_handle);
            if (mfpCommError == MFPCOMM_ERROR.MFPCOMM_OK)
            {
                _discoveryWorker.CancelAsync();
                return true;
            }
            
            return false;
        }

        private void DiscoveryDoWork(object sender, DoWorkEventArgs e)
        {
            DiscoveryWorkerArgument argument = e.Argument as DiscoveryWorkerArgument;

            MFPCOMM_ERROR mfpCommError = MFPCOMM_ERROR.MFPCOMM_OK;
            switch (argument.Type)
            {
                case DiscoverMfp.All:
                    mfpCommError = MFPComm_DiscoverMFPs(_handle);
                    break;

                case DiscoverMfp.InRange:
                    mfpCommError = MFPComm_DiscoverMFPRange(_handle, argument.StartMfpIP, argument.EndMfpIP);
                    break;

                case DiscoverMfp.IP:
                    mfpCommError = MFPComm_DiscoverMFP(_handle, argument.MfpIPorName);
                    break;

                //case DiscoverMfp.Name:
                //    break;

                default:
                    return;
            }

            switch (mfpCommError)
            {
                case MFPCOMM_ERROR.MFPCOMM_OK:
                    while (true)
                    {
                        // Create a struct
                        MFP_T mfp_t = new MFP_T();

                        // Initialize unmanged memory to hold the struct
                        IntPtr pnt = Marshal.AllocHGlobal(Marshal.SizeOf(mfp_t));

                        // Copy the struct to unmanaged memory
                        Marshal.StructureToPtr(mfp_t, pnt, false);

                        switch (MFPComm_GetMFP(_handle, ref pnt))
                        {
                            case MFPCOMM_ERROR.MFPCOMM_FAIL:
                            case MFPCOMM_ERROR.MFPCOMM_FINISHED:
                                Marshal.FreeHGlobal(pnt);
                                return;

                            case MFPCOMM_ERROR.MFPCOMM_WAIT:
                                Marshal.FreeHGlobal(pnt);
                                break;

                            case MFPCOMM_ERROR.MFPCOMM_CONTINUE:
                                MFP_T discoveredMfp = (MFP_T)Marshal.PtrToStructure(pnt, typeof(MFP_T));
                                RaiseMfpDiscovered(this, new MfpDiscoveredEventArgs(discoveredMfp));
                                break;
                        }

                        Thread.Sleep(1000);
                    }

                case MFPCOMM_ERROR.MFPCOMM_FAIL:
                    break;
            }
        }

        private void DiscoveryCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RaiseDiscoverAsyncCompleted(sender, e);
        }

        private void RaiseMfpDiscovered(object sender, MfpDiscoveredEventArgs e)
        {
            if (MfpDiscovered != null)
            {
                MfpDiscovered(sender, e);
            }
        }

        private void RaiseDiscoverAsyncCompleted(object sender, EventArgs e)
        {
            if (DiscoverMfpsAsyncCompleted != null)
            {
                DiscoverMfpsAsyncCompleted(sender, e);
            }
        }

        #region IDisposable Members

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        private void Dispose(bool disposing)
        {
            // free managed resources
            if (disposing)
            {
                
            }

            // free unmanaged resources
            if (_handle != IntPtr.Zero)
            {
                MFPCOMM_ERROR mfpCommError = MFPComm_Uninitialize(_handle);
            }
        }

        #endregion
    }
}
