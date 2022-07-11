using System.Configuration.Install;
using System.ComponentModel;
using System.ServiceProcess;

namespace ClickServerService
{
    [RunInstaller(true)]
    public class ProjectInstaller : Installer
    {
        private readonly IContainer components = null;
        private ServiceProcessInstaller serviceProcessInstaller1;
        private ServiceInstaller serviceInstaller1;

        public ProjectInstaller() => this.InitializeComponent();

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            serviceProcessInstaller1 = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalService,
                Username = null,
                Password = null
            };

            serviceInstaller1 = new ServiceInstaller
            {
                ServiceName = "ClickServiceTest",
                DisplayName = "ClickServiceTest",
                Description = "ClickServiceTest",
                StartType = ServiceStartMode.Automatic
            };

            Installers.AddRange(new Installer[2] { serviceProcessInstaller1, serviceInstaller1 });
        }
    }
}
