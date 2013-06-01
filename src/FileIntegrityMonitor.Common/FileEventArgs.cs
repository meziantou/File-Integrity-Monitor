using System;
using System.IO;
using System.Runtime.Serialization;

namespace FileIntegrityMonitor.Common
{
    [DataContract]
    public class FileEventArgs : EventArgs
    {
        public static FileEventArgs Create(FileSystemEventArgs args)
        {
            return new FileEventArgs
            {
                Date = DateTime.Now,
                FullPath = args.FullPath,
                ChangeType = args.ChangeType
            };
        }

        public static FileEventArgs Create(RenamedEventArgs args)
        {
            return new FileEventArgs
            {
                Date = DateTime.Now,
                FullPath = args.FullPath,
                OldFullPath = args.OldFullPath,
                ChangeType = args.ChangeType
            };
        }

        [DataMember]
        public WatcherChangeTypes ChangeType { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public string FullPath { get; set; }
        [DataMember]
        public string OldFullPath { get; set; }
    }
}
