using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IaG.State.Innovation.Entities
{
    public class VehicleQuote
    {
        public decimal Option1 { get; set; }
        public decimal Option2 { get; set; }
        public decimal Option3 { get; set; }
        public Guid VehicleQuoteId { get; set; }
    }
}
