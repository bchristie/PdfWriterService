using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PdfWriterService.Models
{
    [DataContract(Namespace = "PdfWriterService.Models", Name = "ReburnDTO")]
    public class ReburnDTO
    {
        [DataMember(Name = "CustomerName", IsRequired = true)]
        public String CustomerName { get; set; }
        
        [DataMember(Name = "EmployeeResponsibleID", IsRequired = true)]
        public String EmployeeResponsibleID { get; set; }
        
        [DataMember(Name = "Issue", IsRequired = true)]
        public String Issue { get; set; }

        [DataMember(Name = "Comments", IsRequired = false)]
        public String Comments { get; set; }
    }
}
