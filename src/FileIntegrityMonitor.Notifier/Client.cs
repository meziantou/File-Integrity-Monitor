using System;
using System.ComponentModel;
using System.ServiceModel;
using System.ServiceModel.Security.Tokens;
using System.Threading;
using System.Windows.Forms;
using FileIntegrityMonitor.Common;
using Timer = System.Threading.Timer;

namespace FileIntegrityMonitor.Notifier
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class Client : IFileSystemChangesNotifierCallback, IDisposable
    {
        IFileSystemChangesNotifier _chat;
        private DuplexChannelFactory<IFileSystemChangesNotifier> _channelFactory;

        public Client()
        {
            _worker = new BackgroundWorker();
            _worker.DoWork += worker_DoWork;
            _worker.RunWorkerAsync();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                Thread.Sleep(5000);
                if (e.Cancel)
                    return;
                EnsureConnection();
                if (e.Cancel)
                    return;
                Thread.Sleep(5000);
            }
        }

        private DuplexChannelFactory<IFileSystemChangesNotifier> GetChannelFactory()
        {
            return new DuplexChannelFactory<IFileSystemChangesNotifier>(this, "WSDualHttpBinding_IFileSystemChangesNotifier");
        }

        private void EnsureConnection()
        {
            if (_disposed)
                return;

            try
            {
                if (_chat != null)
                {
                    _chat.Ping();
                }
            }
            catch
            {
                _chat = null;
            }

            if (_chat == null)
            {
                try
                {
                    if (_channelFactory != null)
                    {
                        try
                        {
                            _channelFactory.Close();
                        }
                        catch
                        {

                        }
                    }
                    _channelFactory = GetChannelFactory();
                    _chat = _channelFactory.CreateChannel();
                    _chat.Subscribe();
                }
                catch
                {
                    _chat = null;
                }
            }
        }

        public void OnFileChangeEvent(FileEventArgs e)
        {
            MessageBox.Show(e.ChangeType.ToString());
        }

        private bool _disposed;
        private BackgroundWorker _worker;

        public void Dispose()
        {
            _disposed = true;

            if (_chat != null && _channelFactory.State == CommunicationState.Opened)
            {
                _chat.Unsubscribe();
            }

            try
            {
                _worker.CancelAsync();
                _channelFactory.Close();
            }
            catch
            {
            }
        }
    }
}