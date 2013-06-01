using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using CodeFluent.Runtime.Utilities;
using FileIntegrityMonitor.Common.Utilities;

namespace FileIntegrityMonitor.Common
{
    public class Configuration : IDisposable
    {
        private FileStream _stream;

        public IList<WatcherConfiguration> Watchers
        {
            get { return _watchers; }
        }

        public X509Certificate2 Certificate { get; set; }

        private readonly IList<WatcherConfiguration> _watchers = new List<WatcherConfiguration>();

        public void Load(bool lockConfigurationFile)
        {
            try
            {
                _stream = new FileStream(GetConfigurationPath(), FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.SequentialScan);

                XmlDocument doc = new XmlDocument();
                doc.Load(_stream);
                X509Certificate2 certificate = null;
                if (XmlHelpers.HasSignature(doc) && !XmlHelpers.VerifySignature(doc, out certificate))
                {
                    throw new Exception("Invalid Signature");
                }

                // Find Certificate
                if (certificate != null)
                {
                    X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                    store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

                    try
                    {
                        X509Certificate2 x509Certificate2 = new X509Certificate2(certificate);
                        X509Certificate2Collection x509Certificate2Collection = store.Certificates.Find(X509FindType.FindByThumbprint, x509Certificate2.Thumbprint, false);
                        foreach (X509Certificate2 certificate2 in x509Certificate2Collection)
                        {
                            Certificate = certificate2;
                            break;
                        }
                    }
                    finally
                    {
                        store.Close();
                    }
                }

                var xmlNodeList = doc.SelectNodes("//Watcher");
                foreach (XmlNode node in xmlNodeList)
                {
                    WatcherConfiguration watcherConfiguration = new WatcherConfiguration();
                    watcherConfiguration.Path = XmlUtilities.GetAttribute(node, "path", (string)null);
                    watcherConfiguration.Filter = XmlUtilities.GetAttribute(node, "filter", (string)null);
                    watcherConfiguration.IncludeSubdirectories = XmlUtilities.GetAttribute(node, "includeSubdirectories", false);
                    var md5 = XmlUtilities.GetAttribute(node, "md5", (string)null);
                    if (md5 != null)
                        watcherConfiguration.Md5 = Convert.FromBase64String(md5);
                    var sha1 = XmlUtilities.GetAttribute(node, "sha1", (string)null);
                    if (sha1 != null)
                        watcherConfiguration.Sha1 = Convert.FromBase64String(sha1);
                    Watchers.Add(watcherConfiguration);
                }
            }
            finally
            {
                if (!lockConfigurationFile && _stream != null)
                {
                    _stream.Dispose();
                    _stream = null;
                }
            }
        }

        public void Save()
        {
            XmlDocument doc = new XmlDocument();
            var root = doc.CreateElement("Configuration");
            doc.AppendChild(root);

            foreach (var watcherDesign in Watchers)
            {
                var xmlElement = doc.CreateElement("Watcher");
                xmlElement.SetAttribute("path", watcherDesign.Path);
                xmlElement.SetAttribute("filter", watcherDesign.Filter);
                xmlElement.SetAttribute("includeSubdirectories", watcherDesign.IncludeSubdirectories.ToString(CultureInfo.InvariantCulture));
                xmlElement.SetAttribute("sha1", Convert.ToBase64String(watcherDesign.Sha1));
                xmlElement.SetAttribute("md5", Convert.ToBase64String(watcherDesign.Md5));
                root.AppendChild(xmlElement);
            }

            if (Certificate != null)
            {
                XmlHelpers.Sign(doc, Certificate);
            }

            doc.Save(GetConfigurationPath());
        }

        private static string GetConfigurationPath()
        {
            return Path.Combine(Path.GetDirectoryName(typeof(Configuration).Assembly.Location), "configuration.xml");
        }

        public void Dispose()
        {
            if (_stream != null)
            {
                _stream.Dispose();
                _stream = null;
            }
        }

        public override string ToString()
        {
            return Concatenate(Watchers, Environment.NewLine);
        }

        /// <summary>
        /// Concatenates the specified objects using their string representation.
        /// </summary>
        /// <param name="objects">The collection of objects to concatenate. If the enumerable is null, null will be returned.</param>
        /// <param name="separator">The separator character.</param>
        /// <returns>A string.</returns>
        public static string Concatenate(IEnumerable objects, string separator)
        {
            if (objects == null)
                return null;

            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (object obj in objects)
            {
                if (i > 0)
                {
                    sb.Append(separator);
                }
                else
                {
                    i++;
                }
                if (obj != null)
                {
                    sb.Append(obj);
                }
            }
            return sb.ToString();
        }

        public void ComputeHash()
        {
            foreach (var watcherConfiguration in Watchers)
            {
                watcherConfiguration.ComputeHash();
            }
        }

        public bool ValidateHash()
        {
            return Watchers.All(_ => _.ValidateHash());
        }
    }
}