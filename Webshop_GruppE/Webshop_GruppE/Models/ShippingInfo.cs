using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop_GruppE.Models
{
    internal class ShippingInfo
    {
        public int Id { get; set; }
        public string? ShippingType { get; set; }
        public float? ShippingCost { get; set; }
    }
}
