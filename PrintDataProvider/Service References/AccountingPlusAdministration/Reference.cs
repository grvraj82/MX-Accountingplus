﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PrintDataProviderService.AccountingPlusAdministration {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AccountingPlusAdministration.AccountingConfiguratorSoap")]
    public interface AccountingConfiguratorSoap {
        
        // CODEGEN: Generating message contract since element name DebugToolStausResult from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DebugToolStaus", ReplyAction="*")]
        PrintDataProviderService.AccountingPlusAdministration.DebugToolStausResponse DebugToolStaus(PrintDataProviderService.AccountingPlusAdministration.DebugToolStausRequest request);
        
        // CODEGEN: Generating message contract since element name JobConfigurationResult from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/JobConfiguration", ReplyAction="*")]
        PrintDataProviderService.AccountingPlusAdministration.JobConfigurationResponse JobConfiguration(PrintDataProviderService.AccountingPlusAdministration.JobConfigurationRequest request);
        
        // CODEGEN: Generating message contract since element name AnonymousPrintingStatusResult from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AnonymousPrintingStatus", ReplyAction="*")]
        PrintDataProviderService.AccountingPlusAdministration.AnonymousPrintingStatusResponse AnonymousPrintingStatus(PrintDataProviderService.AccountingPlusAdministration.AnonymousPrintingStatusRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class DebugToolStausRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="DebugToolStaus", Namespace="http://tempuri.org/", Order=0)]
        public PrintDataProviderService.AccountingPlusAdministration.DebugToolStausRequestBody Body;
        
        public DebugToolStausRequest() {
        }
        
        public DebugToolStausRequest(PrintDataProviderService.AccountingPlusAdministration.DebugToolStausRequestBody Body) {
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
        public PrintDataProviderService.AccountingPlusAdministration.DebugToolStausResponseBody Body;
        
        public DebugToolStausResponse() {
        }
        
        public DebugToolStausResponse(PrintDataProviderService.AccountingPlusAdministration.DebugToolStausResponseBody Body) {
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
        public PrintDataProviderService.AccountingPlusAdministration.JobConfigurationRequestBody Body;
        
        public JobConfigurationRequest() {
        }
        
        public JobConfigurationRequest(PrintDataProviderService.AccountingPlusAdministration.JobConfigurationRequestBody Body) {
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
        public PrintDataProviderService.AccountingPlusAdministration.JobConfigurationResponseBody Body;
        
        public JobConfigurationResponse() {
        }
        
        public JobConfigurationResponse(PrintDataProviderService.AccountingPlusAdministration.JobConfigurationResponseBody Body) {
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
        public PrintDataProviderService.AccountingPlusAdministration.AnonymousPrintingStatusRequestBody Body;
        
        public AnonymousPrintingStatusRequest() {
        }
        
        public AnonymousPrintingStatusRequest(PrintDataProviderService.AccountingPlusAdministration.AnonymousPrintingStatusRequestBody Body) {
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
        public PrintDataProviderService.AccountingPlusAdministration.AnonymousPrintingStatusResponseBody Body;
        
        public AnonymousPrintingStatusResponse() {
        }
        
        public AnonymousPrintingStatusResponse(PrintDataProviderService.AccountingPlusAdministration.AnonymousPrintingStatusResponseBody Body) {
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
    public interface AccountingConfiguratorSoapChannel : PrintDataProviderService.AccountingPlusAdministration.AccountingConfiguratorSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AccountingConfiguratorSoapClient : System.ServiceModel.ClientBase<PrintDataProviderService.AccountingPlusAdministration.AccountingConfiguratorSoap>, PrintDataProviderService.AccountingPlusAdministration.AccountingConfiguratorSoap {
        
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
        PrintDataProviderService.AccountingPlusAdministration.DebugToolStausResponse PrintDataProviderService.AccountingPlusAdministration.AccountingConfiguratorSoap.DebugToolStaus(PrintDataProviderService.AccountingPlusAdministration.DebugToolStausRequest request) {
            return base.Channel.DebugToolStaus(request);
        }
        
        public string DebugToolStaus() {
            PrintDataProviderService.AccountingPlusAdministration.DebugToolStausRequest inValue = new PrintDataProviderService.AccountingPlusAdministration.DebugToolStausRequest();
            inValue.Body = new PrintDataProviderService.AccountingPlusAdministration.DebugToolStausRequestBody();
            PrintDataProviderService.AccountingPlusAdministration.DebugToolStausResponse retVal = ((PrintDataProviderService.AccountingPlusAdministration.AccountingConfiguratorSoap)(this)).DebugToolStaus(inValue);
            return retVal.Body.DebugToolStausResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        PrintDataProviderService.AccountingPlusAdministration.JobConfigurationResponse PrintDataProviderService.AccountingPlusAdministration.AccountingConfiguratorSoap.JobConfiguration(PrintDataProviderService.AccountingPlusAdministration.JobConfigurationRequest request) {
            return base.Channel.JobConfiguration(request);
        }
        
        public string JobConfiguration() {
            PrintDataProviderService.AccountingPlusAdministration.JobConfigurationRequest inValue = new PrintDataProviderService.AccountingPlusAdministration.JobConfigurationRequest();
            inValue.Body = new PrintDataProviderService.AccountingPlusAdministration.JobConfigurationRequestBody();
            PrintDataProviderService.AccountingPlusAdministration.JobConfigurationResponse retVal = ((PrintDataProviderService.AccountingPlusAdministration.AccountingConfiguratorSoap)(this)).JobConfiguration(inValue);
            return retVal.Body.JobConfigurationResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        PrintDataProviderService.AccountingPlusAdministration.AnonymousPrintingStatusResponse PrintDataProviderService.AccountingPlusAdministration.AccountingConfiguratorSoap.AnonymousPrintingStatus(PrintDataProviderService.AccountingPlusAdministration.AnonymousPrintingStatusRequest request) {
            return base.Channel.AnonymousPrintingStatus(request);
        }
        
        public string AnonymousPrintingStatus() {
            PrintDataProviderService.AccountingPlusAdministration.AnonymousPrintingStatusRequest inValue = new PrintDataProviderService.AccountingPlusAdministration.AnonymousPrintingStatusRequest();
            inValue.Body = new PrintDataProviderService.AccountingPlusAdministration.AnonymousPrintingStatusRequestBody();
            PrintDataProviderService.AccountingPlusAdministration.AnonymousPrintingStatusResponse retVal = ((PrintDataProviderService.AccountingPlusAdministration.AccountingConfiguratorSoap)(this)).AnonymousPrintingStatus(inValue);
            return retVal.Body.AnonymousPrintingStatusResult;
        }
    }
}
