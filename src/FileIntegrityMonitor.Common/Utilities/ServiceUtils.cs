using System.Collections;
using System.Configuration.Install;
using System.ServiceProcess;

namespace FileIntegrityMonitor.Common.Utilities
{
    public static class ServiceUtils
    {
        public static void Uninstall(string serviceName)
        {
            if (!IsServiceInstalled(serviceName))
                return;

            ServiceInstaller si = new ServiceInstaller();
            ServiceProcessInstaller spi = new ServiceProcessInstaller();
            si.Parent = spi;
            si.ServiceName = serviceName;

            si.Context = new InstallContext(null, null);
            si.Uninstall(null);
        }

        public static void Install(string serviceName, string serviceDisplayName, string description, string path)
        {
            if (IsServiceInstalled(serviceName))
                return;

            ServiceInstaller si = new ServiceInstaller();
            ServiceProcessInstaller spi = new ServiceProcessInstaller();
            spi.Account = ServiceAccount.LocalSystem;
            si.Parent = spi;
            si.DisplayName = serviceDisplayName;
            si.Description = description;
            si.ServiceName = serviceName;
            si.StartType = ServiceStartMode.Automatic;
            si.Context = new InstallContext(null, null);

            si.Context.Parameters["assemblypath"] = path;
            IDictionary stateSaver = new Hashtable();
            si.Install(stateSaver);
        }

        public static bool IsServiceInstalled(string serviceName)
        {
            // get list of Windows services
            ServiceController[] services = ServiceController.GetServices();

            // try to find service name
            foreach (ServiceController service in services)
            {
                if (service.ServiceName == serviceName)
                    return true;
            }
            return false;
        }
    }
}