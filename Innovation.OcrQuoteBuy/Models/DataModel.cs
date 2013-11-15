using IaG.State.Innovation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation.OcrQuoteBuy.Models
{
    public class DummyModel
    {
        public IEnumerable<Vehicle> Vehicles { get; set; }
        public IEnumerable<VehicleQuote> VehicleQuotes { get; set; }

        public DummyModel()
        {
            Vehicles = new List<Vehicle>();
        }
    }
}