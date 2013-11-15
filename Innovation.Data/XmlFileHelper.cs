using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace IaG.State.Innovation.Data
{
    public static class XmlFileHelper
    {
        public static XDocument GetXmlDoc(string docPath)
        {
            var doc = XDocument.Load(docPath);
            return doc;
        }
    }
}
