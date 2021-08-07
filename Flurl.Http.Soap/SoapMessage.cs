using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Flurl.Http.Soap
{
    /// <summary>
    /// Represents a SOAP Envelope and provides methods to parse raw XML input.
    /// </summary>
    public class SoapEnvelope<T>
    {
        public SoapEnvelope(string xml)
        {
            if (ParseXml(xml, out SoapHeader header, out SoapBody<T> body))
            {
                Header = header;
                Body = body;
            }
        }

        // TODO: Add support for SOAP Header
        public SoapHeader Header { get; set; }

        public SoapBody<T> Body { get; set; }

        /// <summary>
        /// Attempt to parse the XML string for a SOAP Envelope and output the SOAP Header/Body.
        /// </summary>
        /// <param name="xml">String representation of the SOAP Envelope.</param>
        /// <param name="header">POCO representation of the SOAP Header.</param>
        /// <param name="body">POCO representation of the SOAP Body.</param>
        /// <returns>True if parsing was successful or False if there was an issue.</returns>
        private static bool ParseXml(string xml, out SoapHeader header, out SoapBody<T> body)
        {
            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xml);

                var soapBody = xmlDocument.GetElementsByTagName("soap:Body")[0];
                string innerObject = soapBody.InnerXml;

                var serializer = new XmlSerializer(typeof(T));

                using (StringReader reader = new StringReader(innerObject))
                {
                    header = new SoapHeader();
                    body = new SoapBody<T>((T)serializer.Deserialize(reader));

                    return true;
                }
            }
            catch
            {
                header = null;
                body = null;

                return false;
            }
        }
    }

    /// <summary>
    /// Represents the SOAP Header
    /// </summary>
    public class SoapHeader
    {
        // TODO: Add support for SOAP Header
    }

    /// <summary>
    /// Represents the SOAP Body as a wrapped POCO class.
    /// </summary>
    public class SoapBody<T>
    {
        public SoapBody (T value)
        {
            _value = value;
        }

        private T _value;

        /// <summary>
        /// Retrieve the internal value of <see cref="SoapBody{T}"/> as <typeparamref name="T"/>.
        /// </summary>
        public T GetValue()
        {
            return _value;
        }

        /// <summary>
        /// Implicitly retrieve the internal value of <paramref name="soapBody"/> as type <typeparamref name="T"/>.
        /// </summary>
        public static implicit operator T(SoapBody<T> soapBody) => soapBody.GetValue();
    }
}
