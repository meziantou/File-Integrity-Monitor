using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceProcess;
using System.Windows.Forms;
using FileIntegrityMonitor.Common;
using FileIntegrityMonitor.Common.Utilities;
using FileIntegrityMonitor.Service;
using Microsoft.Win32;

namespace FileIntegrityMonitor.Configurator
{
    public partial class MainForm : Form
    {
        private const string RegistryRunKeyName = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        private const string RegistryValueName = "FileIntegrityMonitor";
        private readonly Configuration _configuration;

        public MainForm()
        {
            InitializeComponent();
            _notifierComboBox.SelectedIndex = 2;

            try
            {
                _configuration = new Configuration();
                _configuration.Load(false);

                _configurationBindingSource.DataSource = _configuration;
                watcherBindingSource.DataSource = _configuration.Watchers;
                UpdateServiceStatus();
                UpdateCertificateText();
            }
            //// ReSharper disable EmptyGeneralCatchClause
            catch
            //// ReSharper restore EmptyGeneralCatchClause
            {
            }
        }

        private static void Install()
        {
            ServiceUtils.Install(
                IntegrityCheckerService.ServiceNameConst,
                "File Integrity Monitor",
                "",
                typeof(IntegrityCheckerService).Assembly.Location);

            ServiceController serviceController =
                ServiceController.GetServices()
                    .FirstOrDefault(a => a.ServiceName == IntegrityCheckerService.ServiceNameConst);
            if (serviceController != null && serviceController.Status == ServiceControllerStatus.Stopped)
                serviceController.Start();
        }

        private static void Uninstall()
        {
            ServiceUtils.Uninstall(IntegrityCheckerService.ServiceNameConst);
        }

        private void UpdateServiceStatus()
        {
            try
            {
                ServiceController serviceController = new ServiceController(IntegrityCheckerService.ServiceNameConst);
                _serviceStateLabel.Text = serviceController.Status.ToString();
            }
            catch
            {
                _serviceStateLabel.Text = "Not installed";
            }
        }

        private void InstallButton_Click(object sender, EventArgs e)
        {
            Uninstall();
            _configuration.ComputeHash();
            _configuration.Save();
            Install();
            InstallNotifier();
            UpdateServiceStatus();
            MessageBox.Show("Service installed", "", MessageBoxButtons.OK);
        }

        private void InstallNotifier()
        {
            RemoveAutoStart();
            var selectedIndex = _notifierComboBox.SelectedIndex;

            if (selectedIndex == 0)
                return;

            AddAutoStart(selectedIndex == 1 ? RegistryHive.CurrentUser : RegistryHive.Users);
        }

        private void RemoveAutoStart()
        {
            using (var key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(RegistryRunKeyName, true))
            {
                if (key != null && key.GetValue(RegistryValueName) != null)
                {
                    try
                    {
                        key.DeleteValue(RegistryValueName);
                    }
                    catch
                    {
                    }
                }
            }


            using (var key = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64).OpenSubKey(RegistryRunKeyName, true))
            {
                if (key != null && key.GetValue(RegistryValueName) != null)
                {
                    try
                    {
                        key.DeleteValue(RegistryValueName);
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void AddAutoStart(RegistryHive hive)
        {

            using (var key = RegistryKey.OpenBaseKey(hive, RegistryView.Registry64).OpenSubKey(RegistryRunKeyName, true))
            {
                if (key != null)
                {
                    key.SetValue(RegistryValueName, typeof(Notifier.Program).Assembly.Location);
                }
            }
        }

        private void UninstallButton_Click(object sender, EventArgs e)
        {
            Uninstall();
            UpdateServiceStatus();
            MessageBox.Show("Service uninstalled", "", MessageBoxButtons.OK);
        }

        private void SaveConfigurationButton_Click(object sender, EventArgs e)
        {
            bool isRunning = false;
            try
            {
                ServiceController serviceController = new ServiceController(IntegrityCheckerService.ServiceNameConst);

                if (serviceController.Status != ServiceControllerStatus.Stopped)
                {
                    isRunning = true;
                    serviceController.Stop();
                    serviceController.WaitForStatus(ServiceControllerStatus.Stopped);
                }
            }
            //// ReSharper disable EmptyGeneralCatchClause
            catch
            //// ReSharper restore EmptyGeneralCatchClause
            {

            }

            _configuration.ComputeHash();
            _configuration.Save();
            if (isRunning)
            {
                ServiceController serviceController = new ServiceController(IntegrityCheckerService.ServiceNameConst);
                serviceController.Start();
            }

            MessageBox.Show("Configuration Saved", "", MessageBoxButtons.OK);
        }

        private void AddFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog().ToBoolean())
                {
                    watcherBindingSource.Add(new WatcherConfiguration() { Path = openFileDialog1.FileName });
                }
            }
            //// ReSharper disable EmptyGeneralCatchClause
            catch
            //// ReSharper restore EmptyGeneralCatchClause
            {
            }
        }

        private void AddFolderButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (folderBrowserDialog1.ShowDialog().ToBoolean())
                {
                    watcherBindingSource.Add(new WatcherConfiguration() { Path = folderBrowserDialog1.SelectedPath });
                }
            }
            //// ReSharper disable EmptyGeneralCatchClause
            catch
            //// ReSharper restore EmptyGeneralCatchClause
            {
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateServiceStatus();
        }

        private void _selectCertificateButton_Click(object sender, EventArgs e)
        {
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection(store.Certificates.OfType<X509Certificate2>().Where(cert => cert.HasPrivateKey && cert.Verify()).ToArray());
                X509Certificate2Collection selectedCertificate = X509Certificate2UI.SelectFromCollection(x509Certificate2Collection, "Select Certificate", null, X509SelectionFlag.SingleSelection, Handle);
                foreach (X509Certificate2 x509Certificate2 in selectedCertificate)
                {
                    _configuration.Certificate = x509Certificate2;
                    break;
                }
            }
            finally
            {
                store.Close();
            }

            UpdateCertificateText();
        }

        private void _clearCertificateButton_Click(object sender, EventArgs e)
        {
            _configuration.Certificate = null;
            UpdateCertificateText();
        }

        private void UpdateCertificateText()
        {
            _certificateLabel.Text = _configuration.Certificate == null ? "" : _configuration.Certificate.Subject;
        }
    }
}