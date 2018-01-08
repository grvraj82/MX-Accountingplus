﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.18444.
// 
#pragma warning disable 1591

namespace AccountingPlusConfigurator.ProductActivator {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ProductActivationSoap", Namespace="http://www.sharp.com/ApplicationRegistration")]
    public partial class ProductActivation : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback RegisterOperationCompleted;
        
        private System.Threading.SendOrPostCallback UnRegisterOperationCompleted;
        
        private System.Threading.SendOrPostCallback ImportLicenseOperationCompleted;
        
        private System.Threading.SendOrPostCallback ExportLicenseOperationCompleted;
        
        private System.Threading.SendOrPostCallback UpdateCustomFieldsOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetMfpListOperationCompleted;
        
        private System.Threading.SendOrPostCallback ValidateLicensePeriodicallyOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public ProductActivation() {
            this.Url = global::AccountingPlusConfigurator.Properties.Settings.Default.AccountingPlusConfigurator_ProductActivator_ProductActivation;
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
        public event RegisterCompletedEventHandler RegisterCompleted;
        
        /// <remarks/>
        public event UnRegisterCompletedEventHandler UnRegisterCompleted;
        
        /// <remarks/>
        public event ImportLicenseCompletedEventHandler ImportLicenseCompleted;
        
        /// <remarks/>
        public event ExportLicenseCompletedEventHandler ExportLicenseCompleted;
        
        /// <remarks/>
        public event UpdateCustomFieldsCompletedEventHandler UpdateCustomFieldsCompleted;
        
        /// <remarks/>
        public event GetMfpListCompletedEventHandler GetMfpListCompleted;
        
        /// <remarks/>
        public event ValidateLicensePeriodicallyCompletedEventHandler ValidateLicensePeriodicallyCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.sharp.com/ApplicationRegistration/Register", RequestNamespace="http://www.sharp.com/ApplicationRegistration", ResponseNamespace="http://www.sharp.com/ApplicationRegistration", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Register(string productAccessId, string productAccessPassword, string registrationXmlData) {
            object[] results = this.Invoke("Register", new object[] {
                        productAccessId,
                        productAccessPassword,
                        registrationXmlData});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void RegisterAsync(string productAccessId, string productAccessPassword, string registrationXmlData) {
            this.RegisterAsync(productAccessId, productAccessPassword, registrationXmlData, null);
        }
        
        /// <remarks/>
        public void RegisterAsync(string productAccessId, string productAccessPassword, string registrationXmlData, object userState) {
            if ((this.RegisterOperationCompleted == null)) {
                this.RegisterOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRegisterOperationCompleted);
            }
            this.InvokeAsync("Register", new object[] {
                        productAccessId,
                        productAccessPassword,
                        registrationXmlData}, this.RegisterOperationCompleted, userState);
        }
        
        private void OnRegisterOperationCompleted(object arg) {
            if ((this.RegisterCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RegisterCompleted(this, new RegisterCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.sharp.com/ApplicationRegistration/UnRegister", RequestNamespace="http://www.sharp.com/ApplicationRegistration", ResponseNamespace="http://www.sharp.com/ApplicationRegistration", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool UnRegister(string productAccessId, string productAccessPassword, string serialKey, string clientCode) {
            object[] results = this.Invoke("UnRegister", new object[] {
                        productAccessId,
                        productAccessPassword,
                        serialKey,
                        clientCode});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void UnRegisterAsync(string productAccessId, string productAccessPassword, string serialKey, string clientCode) {
            this.UnRegisterAsync(productAccessId, productAccessPassword, serialKey, clientCode, null);
        }
        
        /// <remarks/>
        public void UnRegisterAsync(string productAccessId, string productAccessPassword, string serialKey, string clientCode, object userState) {
            if ((this.UnRegisterOperationCompleted == null)) {
                this.UnRegisterOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUnRegisterOperationCompleted);
            }
            this.InvokeAsync("UnRegister", new object[] {
                        productAccessId,
                        productAccessPassword,
                        serialKey,
                        clientCode}, this.UnRegisterOperationCompleted, userState);
        }
        
        private void OnUnRegisterOperationCompleted(object arg) {
            if ((this.UnRegisterCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UnRegisterCompleted(this, new UnRegisterCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.sharp.com/ApplicationRegistration/ImportLicense", RequestNamespace="http://www.sharp.com/ApplicationRegistration", ResponseNamespace="http://www.sharp.com/ApplicationRegistration", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ImportLicense(string productAccessId, string productAccessPassword, string serialKey, string oldClientCode, string oldActivationCode, string newClientCode, string newActivationCode, string xmlRegistrationData) {
            object[] results = this.Invoke("ImportLicense", new object[] {
                        productAccessId,
                        productAccessPassword,
                        serialKey,
                        oldClientCode,
                        oldActivationCode,
                        newClientCode,
                        newActivationCode,
                        xmlRegistrationData});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ImportLicenseAsync(string productAccessId, string productAccessPassword, string serialKey, string oldClientCode, string oldActivationCode, string newClientCode, string newActivationCode, string xmlRegistrationData) {
            this.ImportLicenseAsync(productAccessId, productAccessPassword, serialKey, oldClientCode, oldActivationCode, newClientCode, newActivationCode, xmlRegistrationData, null);
        }
        
        /// <remarks/>
        public void ImportLicenseAsync(string productAccessId, string productAccessPassword, string serialKey, string oldClientCode, string oldActivationCode, string newClientCode, string newActivationCode, string xmlRegistrationData, object userState) {
            if ((this.ImportLicenseOperationCompleted == null)) {
                this.ImportLicenseOperationCompleted = new System.Threading.SendOrPostCallback(this.OnImportLicenseOperationCompleted);
            }
            this.InvokeAsync("ImportLicense", new object[] {
                        productAccessId,
                        productAccessPassword,
                        serialKey,
                        oldClientCode,
                        oldActivationCode,
                        newClientCode,
                        newActivationCode,
                        xmlRegistrationData}, this.ImportLicenseOperationCompleted, userState);
        }
        
        private void OnImportLicenseOperationCompleted(object arg) {
            if ((this.ImportLicenseCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ImportLicenseCompleted(this, new ImportLicenseCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.sharp.com/ApplicationRegistration/ExportLicense", RequestNamespace="http://www.sharp.com/ApplicationRegistration", ResponseNamespace="http://www.sharp.com/ApplicationRegistration", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ExportLicense(string productAccessId, string productAccessPassword, string serialKey, string clientCode, string activationCode) {
            object[] results = this.Invoke("ExportLicense", new object[] {
                        productAccessId,
                        productAccessPassword,
                        serialKey,
                        clientCode,
                        activationCode});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ExportLicenseAsync(string productAccessId, string productAccessPassword, string serialKey, string clientCode, string activationCode) {
            this.ExportLicenseAsync(productAccessId, productAccessPassword, serialKey, clientCode, activationCode, null);
        }
        
        /// <remarks/>
        public void ExportLicenseAsync(string productAccessId, string productAccessPassword, string serialKey, string clientCode, string activationCode, object userState) {
            if ((this.ExportLicenseOperationCompleted == null)) {
                this.ExportLicenseOperationCompleted = new System.Threading.SendOrPostCallback(this.OnExportLicenseOperationCompleted);
            }
            this.InvokeAsync("ExportLicense", new object[] {
                        productAccessId,
                        productAccessPassword,
                        serialKey,
                        clientCode,
                        activationCode}, this.ExportLicenseOperationCompleted, userState);
        }
        
        private void OnExportLicenseOperationCompleted(object arg) {
            if ((this.ExportLicenseCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ExportLicenseCompleted(this, new ExportLicenseCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.sharp.com/ApplicationRegistration/UpdateCustomFields", RequestNamespace="http://www.sharp.com/ApplicationRegistration", ResponseNamespace="http://www.sharp.com/ApplicationRegistration", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool UpdateCustomFields(string productAccessId, string productAccessPassword, string serialKey, string clientCode, string customFieldXmlData) {
            object[] results = this.Invoke("UpdateCustomFields", new object[] {
                        productAccessId,
                        productAccessPassword,
                        serialKey,
                        clientCode,
                        customFieldXmlData});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void UpdateCustomFieldsAsync(string productAccessId, string productAccessPassword, string serialKey, string clientCode, string customFieldXmlData) {
            this.UpdateCustomFieldsAsync(productAccessId, productAccessPassword, serialKey, clientCode, customFieldXmlData, null);
        }
        
        /// <remarks/>
        public void UpdateCustomFieldsAsync(string productAccessId, string productAccessPassword, string serialKey, string clientCode, string customFieldXmlData, object userState) {
            if ((this.UpdateCustomFieldsOperationCompleted == null)) {
                this.UpdateCustomFieldsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateCustomFieldsOperationCompleted);
            }
            this.InvokeAsync("UpdateCustomFields", new object[] {
                        productAccessId,
                        productAccessPassword,
                        serialKey,
                        clientCode,
                        customFieldXmlData}, this.UpdateCustomFieldsOperationCompleted, userState);
        }
        
        private void OnUpdateCustomFieldsOperationCompleted(object arg) {
            if ((this.UpdateCustomFieldsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateCustomFieldsCompleted(this, new UpdateCustomFieldsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.sharp.com/ApplicationRegistration/GetMfpList", RequestNamespace="http://www.sharp.com/ApplicationRegistration", ResponseNamespace="http://www.sharp.com/ApplicationRegistration", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetMfpList(string productAccessId, string productAccessPassword) {
            object[] results = this.Invoke("GetMfpList", new object[] {
                        productAccessId,
                        productAccessPassword});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetMfpListAsync(string productAccessId, string productAccessPassword) {
            this.GetMfpListAsync(productAccessId, productAccessPassword, null);
        }
        
        /// <remarks/>
        public void GetMfpListAsync(string productAccessId, string productAccessPassword, object userState) {
            if ((this.GetMfpListOperationCompleted == null)) {
                this.GetMfpListOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetMfpListOperationCompleted);
            }
            this.InvokeAsync("GetMfpList", new object[] {
                        productAccessId,
                        productAccessPassword}, this.GetMfpListOperationCompleted, userState);
        }
        
        private void OnGetMfpListOperationCompleted(object arg) {
            if ((this.GetMfpListCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetMfpListCompleted(this, new GetMfpListCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.sharp.com/ApplicationRegistration/ValidateLicensePeriodically", RequestNamespace="http://www.sharp.com/ApplicationRegistration", ResponseNamespace="http://www.sharp.com/ApplicationRegistration", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool ValidateLicensePeriodically(string productAccessId, string productAccessPassword, string serialKey, string clientCode, string macAddress, string activationCode, bool isValidServerID, string totalServer, string totalClient) {
            object[] results = this.Invoke("ValidateLicensePeriodically", new object[] {
                        productAccessId,
                        productAccessPassword,
                        serialKey,
                        clientCode,
                        macAddress,
                        activationCode,
                        isValidServerID,
                        totalServer,
                        totalClient});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void ValidateLicensePeriodicallyAsync(string productAccessId, string productAccessPassword, string serialKey, string clientCode, string macAddress, string activationCode, bool isValidServerID, string totalServer, string totalClient) {
            this.ValidateLicensePeriodicallyAsync(productAccessId, productAccessPassword, serialKey, clientCode, macAddress, activationCode, isValidServerID, totalServer, totalClient, null);
        }
        
        /// <remarks/>
        public void ValidateLicensePeriodicallyAsync(string productAccessId, string productAccessPassword, string serialKey, string clientCode, string macAddress, string activationCode, bool isValidServerID, string totalServer, string totalClient, object userState) {
            if ((this.ValidateLicensePeriodicallyOperationCompleted == null)) {
                this.ValidateLicensePeriodicallyOperationCompleted = new System.Threading.SendOrPostCallback(this.OnValidateLicensePeriodicallyOperationCompleted);
            }
            this.InvokeAsync("ValidateLicensePeriodically", new object[] {
                        productAccessId,
                        productAccessPassword,
                        serialKey,
                        clientCode,
                        macAddress,
                        activationCode,
                        isValidServerID,
                        totalServer,
                        totalClient}, this.ValidateLicensePeriodicallyOperationCompleted, userState);
        }
        
        private void OnValidateLicensePeriodicallyOperationCompleted(object arg) {
            if ((this.ValidateLicensePeriodicallyCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ValidateLicensePeriodicallyCompleted(this, new ValidateLicensePeriodicallyCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void RegisterCompletedEventHandler(object sender, RegisterCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RegisterCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RegisterCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void UnRegisterCompletedEventHandler(object sender, UnRegisterCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UnRegisterCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UnRegisterCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void ImportLicenseCompletedEventHandler(object sender, ImportLicenseCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ImportLicenseCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ImportLicenseCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void ExportLicenseCompletedEventHandler(object sender, ExportLicenseCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ExportLicenseCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ExportLicenseCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void UpdateCustomFieldsCompletedEventHandler(object sender, UpdateCustomFieldsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UpdateCustomFieldsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UpdateCustomFieldsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void GetMfpListCompletedEventHandler(object sender, GetMfpListCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetMfpListCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetMfpListCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void ValidateLicensePeriodicallyCompletedEventHandler(object sender, ValidateLicensePeriodicallyCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ValidateLicensePeriodicallyCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ValidateLicensePeriodicallyCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591