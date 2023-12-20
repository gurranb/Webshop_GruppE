using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop_GruppE.Models
{
    internal class Order
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? ShippingId { get; set; }

        

    }
}
