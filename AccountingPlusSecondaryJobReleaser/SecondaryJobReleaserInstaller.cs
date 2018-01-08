using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;


namespace AccountingPlusSecondaryJobReleaser
{
    [RunInstaller(true)]
    public partial class SecondaryJobReleaserInstaller : System.Configuration.Install.Installer
    {
        private ServiceInstaller service;
        private ServiceProcessInstaller process;

        public SecondaryJobReleaserInstaller()
        {
            InitializeComponent();
            process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;
            service = new ServiceInstaller();
            service.ServiceName = "AccountingPlusSecondaryJobReleaser";
            service.Description = "Accounting Plus Secondary Job Releaser";
            service.StartType = ServiceStartMode.Automatic;
            Installers.Add(process);
            Installers.Add(service);
        }
    }
}
