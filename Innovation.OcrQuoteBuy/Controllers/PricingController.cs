using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IaG.State.Innovation.Data;
using IaG.State.Innovation.Entities;

namespace Innovation.OcrQuoteBuy.Controllers
{
    public class PricingController : ApiController
    {
        private VehicleQuoteRepository _repository = new VehicleQuoteRepository();

        /// <summary>
        /// Get /api/Quote/GetById/id . Retrieves vehicle insurance price by vehiclequoteid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetById")]
        public VehicleQuote Get(string id)
        {
            Guard.IsNotAGuid(id);
            
            return _repository.Get(Guid.Parse(id));
        }
    }
}
