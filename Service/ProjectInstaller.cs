using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.ServiceProcess;

namespace Service
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        private readonly string _source = "Service_Source";
        private readonly string _log = "Service_Log";

        public ProjectInstaller()
        {
            InitializeComponent();
            InstallEventLog();
            AfterInstall += new InstallEventHandler(Service_AfterInstall);
        }

        /// <summary>
        /// Install EventLog during Installation
        /// </summary>
        private void InstallEventLog()
        {
            EventLogInstaller logInstaller = new EventLogInstaller
            {
                Source = _source,
                Log = _log,
            };
            Installers.Add(logInstaller);
        }

        /// <summary>
        /// Remove Installed EventLog after uninstall
        /// </summary>
        /// <param name="savedState"></param>
        protected override void OnAfterUninstall(IDictionary savedState)
        {
            base.OnAfterUninstall(savedState);
            try
            {
                if (EventLog.SourceExists(_source))
                {
                    EventLog.DeleteEventSource(_source);
                }
                if (EventLog.Exists(_log))
                {
                    EventLog.Delete(_log);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// Run the service after installation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Service_AfterInstall(object sender, InstallEventArgs e)
        {
            using (ServiceController sc = new ServiceController(serviceInstaller.ServiceName))
            {
                sc.Start();
            }
        }
    }
}
