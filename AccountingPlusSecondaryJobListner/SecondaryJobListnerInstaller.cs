using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;


namespace AccountingPlusSecondaryJobListner
{
    [RunInstaller(true)]
    public partial class SecondaryJobListnerInstaller : System.Configuration.Install.Installer
    {
        private ServiceInstaller service;
        private ServiceProcessInstaller process;

        public SecondaryJobListnerInstaller()
        {
            process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;
            service = new ServiceInstaller();
            service.ServiceName = "AccountingPlusSecondaryJobListner";
            service.Description = "Accounting Plus Secondary Job Listner";
            service.StartType = ServiceStartMode.Automatic;
            Installers.Add(process);
            Installers.Add(service);
        }
    }
}
