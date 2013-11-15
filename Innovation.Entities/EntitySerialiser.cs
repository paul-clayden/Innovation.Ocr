using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace IaG.State.Innovation.Entities
{
    public class EntitySerialiser<T> 
    {
        private XmlSerializer _serializer = new XmlSerializer(typeof(T));

        public T Deserialise<T>(string xml)
        {
            var s = Encoding.UTF8.GetBytes(@"<?xml version=" + "\"1.0\"" + " encoding=\"utf-8\" ?>\n" + xml);
            var memStream = new MemoryStream();
            memStream.Write(s, 0, s.Length);
            memStream.Position = 0;
            return (T)_serializer.Deserialize(memStream);            
        }
    }
}
