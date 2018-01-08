using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;


namespace AccountingPlusPrimaryJobReleaser
{
    [RunInstaller(true)]
    public partial class PrimaryJobReleaserInstaller : System.Configuration.Install.Installer
    {
        private ServiceInstaller service;
        private ServiceProcessInstaller process;

        public  PrimaryJobReleaserInstaller()
        {
            InitializeComponent();
            process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;
            service = new ServiceInstaller();
            service.ServiceName = "AccountingPlusPrimaryJobReleaser";
            service.Description = "Accounting Plus Primary Job Releaser";
            service.StartType = ServiceStartMode.Automatic;
            Installers.Add(process);
            Installers.Add(service);
        }
    }
}
