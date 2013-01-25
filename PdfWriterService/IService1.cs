using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PdfWriterService.Models;

namespace PdfWriterService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(Namespace = "PdfWriterService.V1", Name = "PdfWriterServiceV1")]
    public interface IService1
    {
        [OperationContract(Name = "HelloWorld")]
        String HelloWorld();
    }

    [ServiceContract(Namespace = "PdfWriterService.V2", Name = "PdfWriterServiceV2")]
    public interface IService2 : IService1
    {
        [OperationContract(Name = "HelloWorld2")]
        String HelloWorld(String name);
    }

    [ServiceContract(Namespace = "PdfWriterService.V3", Name = "PdfWriterServiceV3")]
    public interface IService3 : IService2
    {
        [OperationContract(Name = "GenerateReburnPDF")]
        Byte[] GenerateReburnPDF(ReburnDTO reburnDto);
    }
}
