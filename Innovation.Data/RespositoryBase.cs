using IaG.State.Innovation.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IaG.State.Innovation.Data
{
    public abstract class RespositoryBase<T>
    {
        protected XDocument DataSource { get; set; }
        protected EntitySerialiser<T> Serialiser = new EntitySerialiser<T>();
        protected RespositoryBase()
        {
            //var path = new FileInfo(new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath).Directory.FullName;
            var path = AppDomain.CurrentDomain.BaseDirectory;
            DataSource = XmlFileHelper.GetXmlDoc(Path.Combine(path, @"bin\Datasource.xml"));
        }
    }
}
