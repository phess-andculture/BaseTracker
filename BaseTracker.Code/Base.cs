using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace BaseTracker.Code {
    public class Base {
        public static HttpWebResponse CreateWebRequest(string address, string method, string token, XmlDocument requestXML = null) {
            var request = (HttpWebRequest)WebRequest.Create(address);
            request.Method = method;
            request.ContentType = "application/xml";
            request.Accept = "application/xml";
            request.UserAgent = "BaseTracker (phess@andculture.com)";
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(token));
            if (requestXML != null) {
                request.ContentLength = requestXML.InnerXml.Length;
                request.ServicePoint.Expect100Continue = false;
                using (var stream = new StreamWriter(request.GetRequestStream())) {
                    stream.Write(requestXML.InnerXml);
                    stream.Close();
                }
            }
            return (HttpWebResponse)request.GetResponse();
        }
        public static T DeserializeFromResponse<T>(string input) {
            dynamic xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(input);
            var deserializer = new XmlSerializer(typeof(T));
            var nodeReader = new XmlNodeReader(xmlDocument.DocumentElement);
            T retVal = default(T);
            try {
                retVal = (T)deserializer.Deserialize(nodeReader);
            } finally {
                if (nodeReader != null) { nodeReader.Close(); }
            }
            return retVal;
        }
        public static XmlDocument SerializeToDocument<T>(T input) {
            dynamic xmlDocument = new XmlDocument();
            var serializer = new XmlSerializer(typeof(T));
            using (var ms = new MemoryStream()) {
                using (var writer = new StreamWriter(ms, Encoding.UTF8)) {
                    serializer.Serialize(writer, input);
                    ms.Seek(0, SeekOrigin.Begin);
                    using (var reader = new StreamReader(ms, Encoding.UTF8)) {
                        xmlDocument.LoadXml(reader.ReadToEnd());
                    }
                }
            }
            return xmlDocument;
        }
    }
}
