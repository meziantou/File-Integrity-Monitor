using System;
using System.ServiceProcess;
using FileIntegrityMonitor.Common;
using FileIntegrityMonitor.Common.Utilities;

namespace FileIntegrityMonitor.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (Environment.UserInteractive)
            {
                try
                {

                    ConsoleHelpers.EnsureConsole();

                    using (var host = new WcfHoster())
                    {
                        host.Start();

                        Logger.Notifiers.Add(new ConsoleNotifier());
                        Logger.Notifiers.Add(new EventLogNotifier());
                        Logger.Notifiers.Add(host.Instance);

                        using (FileSystemChangesNotifier fileSystemChangesNotifier = new FileSystemChangesNotifier())
                        {
                            Console.WriteLine("Press a key to exit...");
                            Console.Read();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                }
            }
            else
            {
                var servicesToRun = new ServiceBase[] 
                { 
                    new IntegrityCheckerService() 
                };
                ServiceBase.Run(servicesToRun);
            }
        }
    }
}
