using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FileIntegrityMonitor.Common.Utilities
{
    public static class ChecksumUtilities
    {
        public static ICollection<byte[]> FromPath(string path, string filter, bool includeSubdirectories, ICollection<HashAlgorithm> hashAlgorithms)
        {
            if (Directory.Exists(path))
            {
                return FromDirectory(path, filter, hashAlgorithms);
            }
            else
            {
                return FromFile(path, hashAlgorithms);
            }
        }

        private static ICollection<byte[]> FromFile(string path, ICollection<HashAlgorithm> hashAlgorithms)
        {
            List<byte[]> result = new List<byte[]>();
            TransformFile(path, hashAlgorithms);
            foreach (HashAlgorithm hashAlgorithm in hashAlgorithms)
            {
                hashAlgorithm.TransformFinalBlock(new byte[] { }, 0, 0);
                result.Add(hashAlgorithm.Hash);
            }

            return result;
        }

        private static ICollection<byte[]> FromDirectory(string path, string filter, ICollection<HashAlgorithm> hashAlgorithms)
        {
            List<byte[]> result = new List<byte[]>();

            TransformDirectory(path, filter, hashAlgorithms);

            foreach (HashAlgorithm hashAlgorithm in hashAlgorithms)
            {
                hashAlgorithm.TransformFinalBlock(new byte[] { }, 0, 0);
                result.Add(hashAlgorithm.Hash);
            }

            return result;
        }

        private static void TransformDirectory(string path, string filter, ICollection<HashAlgorithm> hashAlgorithms)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (FileInfo fileInfo in di.GetFiles(filter, SearchOption.TopDirectoryOnly).OrderBy(_ => _.Name))
            {
                TransformString(fileInfo.Name, hashAlgorithms);
                TransformFile(fileInfo.FullName, hashAlgorithms);
            }

            foreach (DirectoryInfo directoryInfo in di.GetDirectories(filter, SearchOption.TopDirectoryOnly).OrderBy(_ => _.Name))
            {
                TransformString(directoryInfo.Name, hashAlgorithms);
                TransformDirectory(directoryInfo.FullName, filter, hashAlgorithms);
            }

        }
        private static void TransformString(string text, ICollection<HashAlgorithm> hashAlgorithms)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(text);

            foreach (var algorithm in hashAlgorithms)
            {
                algorithm.TransformBlock(bytes, 0, bytes.Length, null, 0);
            }
        }
        private static void TransformFile(string path, ICollection<HashAlgorithm> hashAlgorithms)
        {
            using (Stream stream = File.OpenRead(path))
            {
                const int bufferSize = 4096;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                while ((readCount = stream.Read(buffer, 0, bufferSize)) > 0)
                {
                    foreach (var algorithm in hashAlgorithms)
                    {
                        algorithm.TransformBlock(buffer, 0, readCount, null, 0);
                    }
                }
            }
        }
    }
}
