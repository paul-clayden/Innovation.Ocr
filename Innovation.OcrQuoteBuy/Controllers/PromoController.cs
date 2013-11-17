using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IaG.State.Innovation.Data;
using Innovation.Entities;

namespace Innovation.OcrQuoteBuy.Controllers
{
    public class PromoController : ApiController
    {
        private VehicleRepository _vehicleRepository = new VehicleRepository();

        /// <summary>
        /// GET api/Promo/GetById . Checks for a prize winner by vehicle Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetById")]
        public Promotion Get(string id)
        {
            Guard.IsNotAGuid(id);
            Guard.IsNotNull(id);
            //Make sure id is in the list of vehicle id's
            if (!_vehicleRepository.Get().Select(_ => _.VehicleId).Contains(Guid.Parse(id)))
                throw new HttpResponseException(HttpStatusCode.InternalServerError);

            return CheckPromotions(id);
        }

        private Promotion CheckPromotions(string id)
        {

            // Normally would do a random pick of id. But for demo purposes the 50/50 odds are used
            // Randomly pick a winnner for the promotion
            var rand = new Random();
            //50 50 odds
            if (rand.Next(9) > 4)
                return new Promotion()
                {
                    Id = Guid.Parse("0B0B8BEF-27D7-4C4D-8236-C2C7EE81371E"),
                    PrizeDetails = "Congratulations. You're a Prize winner in our random draw for a brand new Hyudai Sante Fe. Prizes claimed at MatchBox cars"
                };
            return null;
        }
    }
}
