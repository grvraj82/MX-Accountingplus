using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;


namespace AccountingPlusConfigurator
{
    [RunInstaller(true)]
    public partial class WatcherInstaller : System.Configuration.Install.Installer
    {
        private ServiceInstaller service;
        private ServiceProcessInstaller process;

        public WatcherInstaller()
        {
            InitializeComponent();

            process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;
            service = new ServiceInstaller();
            service.ServiceName = "AccountingPlusConfigurator";
            service.Description = "AccountingPlus Configurator. This Service watches periodically all other AccountingPlus services. This service must be up and running 24 X 7. If it is stoped for some reasons, Please restart it immediately";
            service.StartType = ServiceStartMode.Automatic;
            Installers.Add(process);
            Installers.Add(service);
        }
    }
}
