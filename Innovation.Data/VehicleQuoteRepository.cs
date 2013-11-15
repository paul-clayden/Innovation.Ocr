using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IaG.State.Innovation.Entities;

namespace IaG.State.Innovation.Data
{
    public class VehicleQuoteRepository : RespositoryBase<VehicleQuote>,  IDataRepository<VehicleQuoteRepository>
    {
        public IEnumerable<VehicleQuoteRepository> Get()
        {
            throw new NotImplementedException();
        }

        public VehicleQuoteRepository Get(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
