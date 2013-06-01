using System;
using System.ServiceModel;

namespace FileIntegrityMonitor.Common
{
    public class WcfHoster : IDisposable
    {
        private ServiceHost _host;
        private readonly WcfServiceNotifier _instance = new WcfServiceNotifier();

        public WcfServiceNotifier Instance { get { return _instance; } }

        public void Start()
        {
            _host = new ServiceHost(Instance);
            _host.Open();
        }

        public void Dispose()
        {
            if (_host != null)
            {
                _host.Close();
                _host = null;
            }
        }
    }
}
