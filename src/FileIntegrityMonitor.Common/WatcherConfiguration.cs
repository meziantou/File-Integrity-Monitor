using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using FileIntegrityMonitor.Common.Utilities;

namespace FileIntegrityMonitor.Common
{
    public class WatcherConfiguration
    {
        public void ComputeHash()
        {
            List<HashAlgorithm> hashAlgorithms = new List<HashAlgorithm>();
            hashAlgorithms.Add(SHA1.Create());
            hashAlgorithms.Add(MD5.Create());
            ChecksumUtilities.FromPath(Path, Filter, IncludeSubdirectories, hashAlgorithms);
            Sha1 = hashAlgorithms[0].Hash;
            Md5 = hashAlgorithms[1].Hash;
        }

        public bool ValidateHash()
        {
            if (Sha1 == null || Md5 == null)
                return true;

            var oldSha1 = Sha1;
            var oldMd5 = Md5;
            ComputeHash();
            if (!Sha1.SequenceEqual(oldSha1) || !Md5.SequenceEqual(oldMd5))
            {
                Logger.LogError("Invalid Hash: " + Path);
                return false;
            }

            return true;
        }

        public bool IncludeSubdirectories { get; set; }

        public string Path { get; set; }

        public string Filter { get; set; }

        public byte[] Sha1 { get; set; }

        public byte[] Md5 { get; set; }

        public override string ToString()
        {
            return string.Format("Path: {0}, Filter: {1}, IncludeSubdirectories: {2}", Path, Filter, IncludeSubdirectories);
        }
    }
}