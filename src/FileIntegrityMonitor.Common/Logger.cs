using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.ServiceModel;
using System.Text;

namespace FileIntegrityMonitor.Common
{
    public abstract class NotifierBase
    {
        public abstract void Log(FileEventArgs args);
        public abstract void Log(Exception exception);
        public abstract void LogInformation(string message);
        public abstract void LogError(string message);

        public static string GetMessage(FileEventArgs args)
        {
            if (args == null) throw new ArgumentNullException("args");

            string message;
            switch (args.ChangeType)
            {
                case WatcherChangeTypes.Created:
                case WatcherChangeTypes.Deleted:
                case WatcherChangeTypes.Changed:
                    message = string.Format("{0}: {1}", args.ChangeType, args.FullPath);
                    break;
                case WatcherChangeTypes.Renamed:
                    message = string.Format("{0}: from <{1}>, to <{2}>", args.ChangeType, args.FullPath, args.OldFullPath);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return message;
        }

        public static string GetMessage(Exception exception)
        {
            StringBuilder sb = new StringBuilder();

            while (exception != null)
            {
                sb.AppendLine(exception.ToString());
                exception = exception.InnerException;
            }

            return sb.ToString();
        }
    }

    public class EventLogNotifier : NotifierBase
    {
        private readonly EventLog _eventLog;
        public EventLogNotifier()
        {
            _eventLog = new EventLog();
            _eventLog.Source = "FileIntegrityMonitor";
            _eventLog.Log = "Application";
        }

        public EventLogNotifier(EventLog eventLog)
        {
            if (eventLog == null) throw new ArgumentNullException("eventLog");
            _eventLog = eventLog;
        }

        public override void Log(FileEventArgs args)
        {
            _eventLog.WriteEntry(GetMessage(args), EventLogEntryType.Error, (int)args.ChangeType);
        }

        public override void Log(Exception exception)
        {
            _eventLog.WriteEntry(GetMessage(exception), EventLogEntryType.Error);
        }

        public override void LogInformation(string message)
        {
            _eventLog.WriteEntry(message, EventLogEntryType.Information);
        }

        public override void LogError(string message)
        {
            _eventLog.WriteEntry(message, EventLogEntryType.Error);
        }
    }

    public class ConsoleNotifier : NotifierBase
    {
        public override void Log(FileEventArgs args)
        {
            Console.WriteLine(GetMessage(args));
        }

        public override void Log(Exception exception)
        {
            Console.WriteLine(GetMessage(exception));
        }

        public override void LogInformation(string message)
        {
            Console.WriteLine(message);
        }

        public override void LogError(string message)
        {
            Console.WriteLine(message);
        }
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class WcfServiceNotifier : NotifierBase, IFileSystemChangesNotifier
    {
        private readonly HashSet<IFileSystemChangesNotifierCallback> _subscribers = new HashSet<IFileSystemChangesNotifierCallback>();
        private readonly object _lock = new object();

        private void Notify(FileEventArgs args)
        {
            lock (_lock)
            {
                foreach (var fileSystemChangesNotifierCallback in _subscribers)
                {
                    try
                    {
                        fileSystemChangesNotifierCallback.OnFileChangeEvent(args);
                    }
                    catch { }
                }
            }
        }

        public void Subscribe()
        {
            IFileSystemChangesNotifierCallback caller = OperationContext.Current.GetCallbackChannel<IFileSystemChangesNotifierCallback>();

            if (caller == null)
                return;

            if (_subscribers.Contains(caller))
                return;

            lock (_lock)
            {
                if (_subscribers.Add(caller))
                {
                    Logger.LogInformation("Subscribers: " + _subscribers.Count);
                }
            }
        }

        public void Ping()
        {
        }

        public void Unsubscribe()
        {
            IFileSystemChangesNotifierCallback caller = OperationContext.Current.GetCallbackChannel<IFileSystemChangesNotifierCallback>();

            lock (_lock)
            {
                if (_subscribers.Remove(caller))
                {
                    Logger.LogInformation("Subscribers: " + _subscribers.Count);
                }
            }
        }

        public override void Log(FileEventArgs args)
        {
            Notify(args);
        }

        public override void Log(Exception exception)
        {
        }

        public override void LogInformation(string message)
        {
        }

        public override void LogError(string message)
        {
        }
    }

    public static class Logger
    {
        private static readonly IList<NotifierBase> _notifiers = new List<NotifierBase>();
        public static IList<NotifierBase> Notifiers
        {
            get { return _notifiers; }
        }

        public static void Log(FileEventArgs args)
        {
            foreach (var notifier in Notifiers)
            {
                notifier.Log(args);
            }
        }

        public static void Log(Exception exception)
        {
            foreach (var notifier in Notifiers)
            {
                notifier.Log(exception);
            }
        }

        public static void LogInformation(string message)
        {
            foreach (var notifier in Notifiers)
            {
                notifier.LogInformation(message);
            }
        }
        public static void LogError(string message)
        {
            foreach (var notifier in Notifiers)
            {
                notifier.LogError(message);
            }
        }
    }
}