﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ApplicationAuditor.PrintReleaseAdministration {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="PrintReleaseAdministration.AccountingConfiguratorSoap")]
    public interface AccountingConfiguratorSoap {
        
        // CODEGEN: Generating message contract since element name DebugToolStausResult from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DebugToolStaus", ReplyAction="*")]
        ApplicationAuditor.PrintReleaseAdministration.DebugToolStausResponse DebugToolStaus(ApplicationAuditor.PrintReleaseAdministration.DebugToolStausRequest request);
        
        // CODEGEN: Generating message contract since element name JobConfigurationResult from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/JobConfiguration", ReplyAction="*")]
        ApplicationAuditor.PrintReleaseAdministration.JobConfigurationResponse JobConfiguration(ApplicationAuditor.PrintReleaseAdministration.JobConfigurationRequest request);
        
        // CODEGEN: Generating message contract since element name AnonymousPrintingStatusResult from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AnonymousPrintingStatus", ReplyAction="*")]
        ApplicationAuditor.PrintReleaseAdministration.AnonymousPrintingStatusResponse AnonymousPrintingStatus(ApplicationAuditor.PrintReleaseAdministration.AnonymousPrintingStatusRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class DebugToolStausRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="DebugToolStaus", Namespace="http://tempuri.org/", Order=0)]
        public ApplicationAuditor.PrintReleaseAdministration.DebugToolStausRequestBody Body;
        
        public DebugToolStausRequest() {
        }
        
        public DebugToolStausRequest(ApplicationAuditor.PrintReleaseAdministration.DebugToolStausRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class DebugToolStausRequestBody {
        
        public DebugToolStausRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class DebugToolStausResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="DebugToolStausResponse", Namespace="http://tempuri.org/", Order=0)]
        public ApplicationAuditor.PrintReleaseAdministration.DebugToolStausResponseBody Body;
        
        public DebugToolStausResponse() {
        }
        
        public DebugToolStausResponse(ApplicationAuditor.PrintReleaseAdministration.DebugToolStausResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class DebugToolStausResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string DebugToolStausResult;
        
        public DebugToolStausResponseBody() {
        }
        
        public DebugToolStausResponseBody(string DebugToolStausResult) {
            this.DebugToolStausResult = DebugToolStausResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class JobConfigurationRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="JobConfiguration", Namespace="http://tempuri.org/", Order=0)]
        public ApplicationAuditor.PrintReleaseAdministration.JobConfigurationRequestBody Body;
        
        public JobConfigurationRequest() {
        }
        
        public JobConfigurationRequest(ApplicationAuditor.PrintReleaseAdministration.JobConfigurationRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class JobConfigurationRequestBody {
        
        public JobConfigurationRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class JobConfigurationResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="JobConfigurationResponse", Namespace="http://tempuri.org/", Order=0)]
        public ApplicationAuditor.PrintReleaseAdministration.JobConfigurationResponseBody Body;
        
        public JobConfigurationResponse() {
        }
        
        public JobConfigurationResponse(ApplicationAuditor.PrintReleaseAdministration.JobConfigurationResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class JobConfigurationResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string JobConfigurationResult;
        
        public JobConfigurationResponseBody() {
        }
        
        public JobConfigurationResponseBody(string JobConfigurationResult) {
            this.JobConfigurationResult = JobConfigurationResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AnonymousPrintingStatusRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="AnonymousPrintingStatus", Namespace="http://tempuri.org/", Order=0)]
        public ApplicationAuditor.PrintReleaseAdministration.AnonymousPrintingStatusRequestBody Body;
        
        public AnonymousPrintingStatusRequest() {
        }
        
        public AnonymousPrintingStatusRequest(ApplicationAuditor.PrintReleaseAdministration.AnonymousPrintingStatusRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class AnonymousPrintingStatusRequestBody {
        
        public AnonymousPrintingStatusRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AnonymousPrintingStatusResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="AnonymousPrintingStatusResponse", Namespace="http://tempuri.org/", Order=0)]
        public ApplicationAuditor.PrintReleaseAdministration.AnonymousPrintingStatusResponseBody Body;
        
        public AnonymousPrintingStatusResponse() {
        }
        
        public AnonymousPrintingStatusResponse(ApplicationAuditor.PrintReleaseAdministration.AnonymousPrintingStatusResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class AnonymousPrintingStatusResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string AnonymousPrintingStatusResult;
        
        public AnonymousPrintingStatusResponseBody() {
        }
        
        public AnonymousPrintingStatusResponseBody(string AnonymousPrintingStatusResult) {
            this.AnonymousPrintingStatusResult = AnonymousPrintingStatusResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface AccountingConfiguratorSoapChannel : ApplicationAuditor.PrintReleaseAdministration.AccountingConfiguratorSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AccountingConfiguratorSoapClient : System.ServiceModel.ClientBase<ApplicationAuditor.PrintReleaseAdministration.AccountingConfiguratorSoap>, ApplicationAuditor.PrintReleaseAdministration.AccountingConfiguratorSoap {
        
        public AccountingConfiguratorSoapClient() {
        }
        
        public AccountingConfiguratorSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AccountingConfiguratorSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AccountingConfiguratorSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AccountingConfiguratorSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ApplicationAuditor.PrintReleaseAdministration.DebugToolStausResponse ApplicationAuditor.PrintReleaseAdministration.AccountingConfiguratorSoap.DebugToolStaus(ApplicationAuditor.PrintReleaseAdministration.DebugToolStausRequest request) {
            return base.Channel.DebugToolStaus(request);
        }
        
        public string DebugToolStaus() {
            ApplicationAuditor.PrintReleaseAdministration.DebugToolStausRequest inValue = new ApplicationAuditor.PrintReleaseAdministration.DebugToolStausRequest();
            inValue.Body = new ApplicationAuditor.PrintReleaseAdministration.DebugToolStausRequestBody();
            ApplicationAuditor.PrintReleaseAdministration.DebugToolStausResponse retVal = ((ApplicationAuditor.PrintReleaseAdministration.AccountingConfiguratorSoap)(this)).DebugToolStaus(inValue);
            return retVal.Body.DebugToolStausResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ApplicationAuditor.PrintReleaseAdministration.JobConfigurationResponse ApplicationAuditor.PrintReleaseAdministration.AccountingConfiguratorSoap.JobConfiguration(ApplicationAuditor.PrintReleaseAdministration.JobConfigurationRequest request) {
            return base.Channel.JobConfiguration(request);
        }
        
        public string JobConfiguration() {
            ApplicationAuditor.PrintReleaseAdministration.JobConfigurationRequest inValue = new ApplicationAuditor.PrintReleaseAdministration.JobConfigurationRequest();
            inValue.Body = new ApplicationAuditor.PrintReleaseAdministration.JobConfigurationRequestBody();
            ApplicationAuditor.PrintReleaseAdministration.JobConfigurationResponse retVal = ((ApplicationAuditor.PrintReleaseAdministration.AccountingConfiguratorSoap)(this)).JobConfiguration(inValue);
            return retVal.Body.JobConfigurationResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ApplicationAuditor.PrintReleaseAdministration.AnonymousPrintingStatusResponse ApplicationAuditor.PrintReleaseAdministration.AccountingConfiguratorSoap.AnonymousPrintingStatus(ApplicationAuditor.PrintReleaseAdministration.AnonymousPrintingStatusRequest request) {
            return base.Channel.AnonymousPrintingStatus(request);
        }
        
        public string AnonymousPrintingStatus() {
            ApplicationAuditor.PrintReleaseAdministration.AnonymousPrintingStatusRequest inValue = new ApplicationAuditor.PrintReleaseAdministration.AnonymousPrintingStatusRequest();
            inValue.Body = new ApplicationAuditor.PrintReleaseAdministration.AnonymousPrintingStatusRequestBody();
            ApplicationAuditor.PrintReleaseAdministration.AnonymousPrintingStatusResponse retVal = ((ApplicationAuditor.PrintReleaseAdministration.AccountingConfiguratorSoap)(this)).AnonymousPrintingStatus(inValue);
            return retVal.Body.AnonymousPrintingStatusResult;
        }
    }
}