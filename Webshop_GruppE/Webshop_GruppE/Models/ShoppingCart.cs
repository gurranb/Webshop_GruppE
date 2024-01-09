using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop_GruppE.Models
{
    internal class ShoppingCart
    {
        public int Id { get; set; }
        public List<Product>? Products { get; set; }
        public float? TotalCost { get; set; }
        public int? Quantity { get; set; }

        public virtual Customer? Customer { get; set; }
    }
}
