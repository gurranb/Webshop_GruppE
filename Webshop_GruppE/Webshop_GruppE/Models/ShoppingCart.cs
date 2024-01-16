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
        public int? CustomerId { get; set; }
        public int? ProductId { get; set; }

        public virtual ICollection<ShoppingCartItem>? ShoppingCartItems { get; set; }
    }
}
