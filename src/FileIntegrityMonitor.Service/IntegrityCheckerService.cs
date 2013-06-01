using System.ServiceProcess;
using FileIntegrityMonitor.Common;

namespace FileIntegrityMonitor.Service
{
    public partial class IntegrityCheckerService : ServiceBase
    {
        public const string ServiceNameConst = "FileIntegrityMonitor";
        private WcfHoster _hoster;
        private FileSystemChangesNotifier _fileSystemChangesNotifier;

        public IntegrityCheckerService()
        {
            InitializeComponent();
            this.ServiceName = ServiceNameConst;
        }

        protected override void OnStart(string[] args)
        {
            _hoster = new WcfHoster();
            _hoster.Start();

            Logger.Notifiers.Add(new EventLogNotifier());
            Logger.Notifiers.Add(_hoster.Instance);

            _fileSystemChangesNotifier = new FileSystemChangesNotifier();
        }

        protected override void OnStop()
        {
            if (_hoster != null)
            {
                _hoster.Dispose();
                _hoster = null;
            }

            if (_fileSystemChangesNotifier != null)
            {
                _fileSystemChangesNotifier.Dispose();
                _fileSystemChangesNotifier = null;
            }
        }
    }
}
