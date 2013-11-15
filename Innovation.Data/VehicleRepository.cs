using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using IaG.State.Innovation.Entities;
using System.Xml;
using IaG.State.Innovation.Ocr.DtkAnpr;

namespace IaG.State.Innovation.Data
{
    public class VehicleRepository : RespositoryBase<Vehicle>, IDataRepository<Vehicle>
    {
        private IEnumerable<XElement> _vehicleDataSource;
 
        public VehicleRepository()
        {
            _vehicleDataSource = base.DataSource.Document.Descendants("Vehicle");
        }
        public IEnumerable<Vehicle> Get()
        {
            var result = new List<Vehicle>();
            foreach (var data in _vehicleDataSource)
            {
                result.Add(Serialiser.Deserialise<Vehicle>(data.ToString()));
            }
            return result;
        }

        public Vehicle Get(Guid id)
        {
            var data = _vehicleDataSource.Where(x => Guid.Parse(x.Descendants("VehicleId").FirstOrDefault().Value) == id);
            return Serialiser.Deserialise<Vehicle>(data.First().ToString()); 
        }

        public Vehicle Get(string plateNo)
        {
            var data = _vehicleDataSource.Where(x => x.Descendants("Registration").FirstOrDefault().Value == plateNo);
            return Serialiser.Deserialise<Vehicle>(data.First().ToString()); 
        }
    }
}
