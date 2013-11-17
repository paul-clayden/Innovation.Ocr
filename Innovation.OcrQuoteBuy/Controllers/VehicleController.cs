using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IaG.State.Innovation.Data;
using IaG.State.Innovation.Entities;

namespace Innovation.OcrQuoteBuy.Controllers
{
    public class VehicleController : ApiController
    {
        private VehicleRepository _repository = new VehicleRepository();

        /// <summary>
        /// api/vehicle/Get - Gets all vehicles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Get")]
        public IEnumerable<Vehicle> Get()
        {
            return _repository.Get();
        }

        /// <summary>
        /// GET api/vehicle/GetById/{id} - Gets a vehicle by Id (Guid) string
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetById")]
        public Vehicle Get(string id)
        {
            Guard.IsNotAGuid(id);

            return _repository.Get(Guid.Parse(id));
        }

        /// <summary>
        /// GET api/vehicle/GetByPlate/{plateNo} - Gets a vehicle by plate number (string)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetByPlate")]
        public Vehicle GetByPlate(string id)
        {          
            return _repository.Get(id);
        }

        
    }
}
