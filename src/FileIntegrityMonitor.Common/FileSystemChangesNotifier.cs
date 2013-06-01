using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace FileIntegrityMonitor.Common
{
    public class FileSystemChangesNotifier : IDisposable
    {
        private readonly List<FileSystemWatcher> _watchers = new List<FileSystemWatcher>();
        private Configuration _configuration;

        public FileSystemChangesNotifier()
        {
            _configuration = new Configuration();
            _configuration.Load(true);

            foreach (var watcher in _configuration.Watchers)
                AddPath(watcher);

            ThreadPool.QueueUserWorkItem(state =>
            {
                _configuration.ValidateHash();
            });

            Logger.LogInformation("Initialize Service" + Environment.NewLine + _configuration.ToString());
        }

        private void AddPath(WatcherConfiguration watcherConfiguration)
        {
            if (watcherConfiguration == null) throw new ArgumentNullException("watcherConfiguration");


            try
            {
                FileSystemWatcher watcher;

                string path;
                string filter;
                if (File.Exists(watcherConfiguration.Path))
                {
                    path = Path.GetDirectoryName(watcherConfiguration.Path);
                    Debug.Assert(path != null);
                    filter = Path.GetFileName(watcherConfiguration.Path);
                }
                else if (Directory.Exists(watcherConfiguration.Path))
                {
                    path = watcherConfiguration.Path;
                    filter = watcherConfiguration.Filter;
                }
                else
                {
                    Logger.Log(new FileEventArgs() { ChangeType = WatcherChangeTypes.Deleted, Date = DateTime.Now, FullPath = watcherConfiguration.Path });
                    return;
                }


                if (string.IsNullOrWhiteSpace(filter))
                    watcher = new FileSystemWatcher(path);
                else
                    watcher = new FileSystemWatcher(path, filter);

                watcher.IncludeSubdirectories = watcherConfiguration.IncludeSubdirectories;

                watcher.Changed += Watcher_Changed;
                watcher.Created += Watcher_Changed;
                watcher.Deleted += Watcher_Changed;
                watcher.Error += Watcher_Error;
                watcher.Renamed += Watcher_Renamed;
                watcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size;
                watcher.EnableRaisingEvents = true;
                _watchers.Add(watcher);
            }
            catch (IOException ex)
            {
                Logger.Log(ex);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
        }

        private void Watcher_Error(object sender, ErrorEventArgs e)
        {
            Logger.Log(e.GetException());
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Logger.Log(FileEventArgs.Create(e));
        }

        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            Logger.Log(FileEventArgs.Create(e));
        }

        public void Dispose()
        {
            Logger.LogInformation("Service stoping");
            if (_configuration != null)
            {
                _configuration.Dispose();
                _configuration = null;
            }

            foreach (var watcher in _watchers)
            {
                watcher.Dispose();
            }

            _watchers.Clear();
        }
    }
}