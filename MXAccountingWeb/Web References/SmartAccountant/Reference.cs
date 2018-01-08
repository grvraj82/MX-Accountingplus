﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace AccountingPlusWeb.SmartAccountant {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="CounterAccountantSoap", Namespace="http://tempuri.org/")]
    public partial class CounterAccountant : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ProcessCounterRequestOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public CounterAccountant() {
            this.Url = global::AccountingPlusWeb.Properties.Settings.Default.AccountingPlusWeb_SmartAccountant_CounterAccountant;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event ProcessCounterRequestCompletedEventHandler ProcessCounterRequestCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ProcessCounterRequest", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void ProcessCounterRequest(string customerID, [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")] byte[] counterDetails, string accessID, string tokenID) {
            this.Invoke("ProcessCounterRequest", new object[] {
                        customerID,
                        counterDetails,
                        accessID,
                        tokenID});
        }
        
        /// <remarks/>
        public void ProcessCounterRequestAsync(string customerID, byte[] counterDetails, string accessID, string tokenID) {
            this.ProcessCounterRequestAsync(customerID, counterDetails, accessID, tokenID, null);
        }
        
        /// <remarks/>
        public void ProcessCounterRequestAsync(string customerID, byte[] counterDetails, string accessID, string tokenID, object userState) {
            if ((this.ProcessCounterRequestOperationCompleted == null)) {
                this.ProcessCounterRequestOperationCompleted = new System.Threading.SendOrPostCallback(this.OnProcessCounterRequestOperationCompleted);
            }
            this.InvokeAsync("ProcessCounterRequest", new object[] {
                        customerID,
                        counterDetails,
                        accessID,
                        tokenID}, this.ProcessCounterRequestOperationCompleted, userState);
        }
        
        private void OnProcessCounterRequestOperationCompleted(object arg) {
            if ((this.ProcessCounterRequestCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ProcessCounterRequestCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void ProcessCounterRequestCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591