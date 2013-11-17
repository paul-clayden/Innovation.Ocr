using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using IaG.State.Innovation.Entities;

namespace IaG.State.Innovation.Data
{
    public class VehicleQuoteRepository : RespositoryBase<VehicleQuote>,  IDataRepository<VehicleQuote>
    {
        private IEnumerable<XElement> _vehicleQuoteDataSource;

        public VehicleQuoteRepository()
        {
            _vehicleQuoteDataSource = base.DataSource.Document.Descendants("VehicleQuote");
        }
        public IEnumerable<VehicleQuote> Get()
        {
            throw new NotSupportedException();
        }

        public VehicleQuote Get(Guid id)
        {
            var data = _vehicleQuoteDataSource.Where(x => Guid.Parse(x.Descendants("VehicleQuoteId").FirstOrDefault().Value) == id);
            return Serialiser.Deserialise<VehicleQuote>(data.First().ToString()); 
        }
    }
}
