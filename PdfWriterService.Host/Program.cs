using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PdfWriterService.Host
{
    // this is generally only ran while you're in the IDE running the service. A windows service does not use Main()
    // so this essentially does what windows does in the backgroudn (in terms of spinning up the service and llowing
    // you to access it)
    public class Program
    {
        [STAThread]
        public static void Main(String[] args)
        {
            PdfWriterServiceHost.HostWithinConsoleApplication(args);
        }
    }
}
