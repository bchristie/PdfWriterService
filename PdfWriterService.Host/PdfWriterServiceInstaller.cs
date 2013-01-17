using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace PdfWriterService.Host
{
    [RunInstaller(true)]
    public class PdfWriterServiceInstaller : Installer
    {
        private const String SERVICE_NAME = @"PDF Writer Service";

        private ServiceInstaller ServiceInstaller;
        private ServiceProcessInstaller ServiceProcessInstaller;

        public PdfWriterServiceInstaller()
            : base()
        {
            this.ServiceProcessInstaller = new ServiceProcessInstaller();
            this.ServiceProcessInstaller.Account = ServiceAccount.LocalSystem;

            this.ServiceInstaller = new ServiceInstaller();
            this.ServiceInstaller.StartType = ServiceStartMode.Automatic;
            this.ServiceInstaller.ServiceName = SERVICE_NAME;
            this.ServiceInstaller.DisplayName = SERVICE_NAME;
            this.ServiceInstaller.Description = "Service for generating PDFs";

            this.Installers.Add(this.ServiceInstaller);
            this.Installers.Add(this.ServiceProcessInstaller);
        }

        public override void Commit(System.Collections.IDictionary savedState)
        {
            ServiceController controller = new ServiceController(SERVICE_NAME);
            controller.Start();
        }
    }
}
