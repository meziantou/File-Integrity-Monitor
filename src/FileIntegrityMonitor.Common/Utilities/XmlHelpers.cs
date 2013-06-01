using System;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;
using System.Xml.Linq;

namespace FileIntegrityMonitor.Common.Utilities
{
    public static class XmlHelpers
    {
        public static XElement AddAttribute(this XElement element, XName name, object value)
        {
            if (element == null) throw new ArgumentNullException("element");

            if (value != null)
                element.Add(new XAttribute(name, value));

            return element;
        }

        public static string AttributeValue(this XElement element, XName name, string defaultValue)
        {
            if (element == null) throw new ArgumentNullException("element");
            if (name == null) throw new ArgumentNullException("name");

            var attribute = element.Attribute(name);
            if (attribute == null)
                return defaultValue;

            return attribute.Value;
        }

        public static bool AttributeValue(this XElement element, XName name, bool defaultValue)
        {
            if (element == null) throw new ArgumentNullException("element");
            if (name == null) throw new ArgumentNullException("name");

            var attribute = element.Attribute(name);
            if (attribute == null)
                return defaultValue;

            return Convert.ToBoolean(attribute.Value);
        }

        public static void Sign(XmlDocument document, X509Certificate2 certificate)
        {
            // Create a SignedXml object.
            SignedXml signedXml = new SignedXml(document);

            // Add the key to the SignedXml document.
            signedXml.SigningKey = certificate.PrivateKey;

            // Create a reference to be signed.
            Reference reference = new Reference();
            reference.Uri = "";

            // Add an enveloped transformation to the reference.
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);

            // Add the reference to the SignedXml object.
            signedXml.AddReference(reference);

            KeyInfo keyInfo = new KeyInfo();
            keyInfo.AddClause(new KeyInfoX509Data(certificate));
            signedXml.KeyInfo = keyInfo;

            // Compute the signature.
            signedXml.ComputeSignature();

            // Get the XML representation of the signature and save
            // it to an XmlElement object.
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            // Append the element to the XML document.
            document.DocumentElement.AppendChild(document.ImportNode(xmlDigitalSignature, true));
        }

        public static bool HasSignature(XmlDocument document)
        {
            XmlNodeList nodeList = document.GetElementsByTagName("Signature");
            return nodeList.Count > 0;
        }

        public static bool VerifySignature(XmlDocument document, out X509Certificate2 certificate)
        {
            // Create a new SignedXml object and pass it
            // the XML document class.
            var signedXml = new SignedXml(document);

            // Find the "Signature" node and create a new
            // XmlNodeList object.
            XmlNodeList nodeList = document.GetElementsByTagName("Signature");

            // Throw an exception if no signature was found.
            if (nodeList.Count <= 0)
            {
                throw new CryptographicException("Verification failed: No Signature was found in the document.");
            }

            // This example only supports one signature for
            // the entire XML document.  Throw an exception 
            // if more than one signature was found.
            if (nodeList.Count >= 2)
            {
                throw new CryptographicException(
                    "Verification failed: More that one signature was found for the document.");
            }

            // Load the first <signature> node.  
            signedXml.LoadXml((XmlElement)nodeList[0]);



            // Check the signature and return the result.

            var keyInfoX509Data = signedXml.KeyInfo.OfType<KeyInfoX509Data>().FirstOrDefault();
            if (keyInfoX509Data == null || keyInfoX509Data.Certificates.Count == 0)
                throw new CryptographicException("No certificate");

            certificate = (X509Certificate2)keyInfoX509Data.Certificates[0];
            //if (!certificate.Verify())
            //    return false;

            return signedXml.CheckSignature(certificate, true);
        }
    }
}