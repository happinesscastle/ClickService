using System.Configuration.Install;
using System.ComponentModel;
using System.ServiceProcess;

namespace ClickServerService
{
    [RunInstaller(true)]
    public class ProjectInstaller : Installer
    {
        private readonly IContainer components = (IContainer)null;
        private ServiceProcessInstaller serviceProcessInstaller1;
        private ServiceInstaller serviceInstaller1;

        public ProjectInstaller() => this.InitializeComponent();

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.serviceProcessInstaller1 = new ServiceProcessInstaller();
            this.serviceInstaller1 = new ServiceInstaller();
            this.serviceProcessInstaller1.Account = ServiceAccount.LocalService;
            this.serviceProcessInstaller1.Password = (string)null;
            this.serviceProcessInstaller1.Username = (string)null;
            this.serviceInstaller1.Description = "ClickServiceTest";
            this.serviceInstaller1.DisplayName = "ClickServiceTest";
            this.serviceInstaller1.ServiceName = "ClickServiceTest";
            this.serviceInstaller1.StartType = ServiceStartMode.Automatic;
            this.Installers.AddRange(new Installer[2]
            {
        (Installer) this.serviceProcessInstaller1,
        (Installer) this.serviceInstaller1
            });
        }
    }
}
