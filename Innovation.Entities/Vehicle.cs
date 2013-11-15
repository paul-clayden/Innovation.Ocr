using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IaG.State.Innovation.Entities
{
    public class Vehicle
    {
        public Guid VehicleId { get; set; }
        public string Registration { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Colour { get; set; }
        public Guid VehicleQuoteId { get; set; }
    }
}
