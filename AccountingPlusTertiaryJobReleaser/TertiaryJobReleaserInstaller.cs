using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;


namespace AccountingPlusTertiaryJobReleaser
{
    [RunInstaller(true)]
    public partial class TertiaryJobReleaserInstaller : System.Configuration.Install.Installer
    {
        private ServiceInstaller service;
        private ServiceProcessInstaller process;

        public TertiaryJobReleaserInstaller()
        {
            InitializeComponent();
            process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;
            service = new ServiceInstaller();
            service.ServiceName = "AccountingPlusTertiaryJobReleaser";
            service.Description = "Accounting Plus Tertiary Job Releaser";
            service.StartType = ServiceStartMode.Automatic;
            Installers.Add(process);
            Installers.Add(service);
        }
    }
}
