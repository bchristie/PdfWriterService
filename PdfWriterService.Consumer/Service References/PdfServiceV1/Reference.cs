﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PdfWriterService.Consumer.PdfServiceV1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="PdfWriterService.V1", ConfigurationName="PdfServiceV1.PdfWriterServiceV1")]
    public interface PdfWriterServiceV1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="PdfWriterService.V1/PdfWriterServiceV1/HelloWorld", ReplyAction="PdfWriterService.V1/PdfWriterServiceV1/HelloWorldResponse")]
        string HelloWorld();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface PdfWriterServiceV1Channel : PdfWriterService.Consumer.PdfServiceV1.PdfWriterServiceV1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PdfWriterServiceV1Client : System.ServiceModel.ClientBase<PdfWriterService.Consumer.PdfServiceV1.PdfWriterServiceV1>, PdfWriterService.Consumer.PdfServiceV1.PdfWriterServiceV1 {
        
        public PdfWriterServiceV1Client() {
        }
        
        public PdfWriterServiceV1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PdfWriterServiceV1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PdfWriterServiceV1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PdfWriterServiceV1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string HelloWorld() {
            return base.Channel.HelloWorld();
        }
    }
}
