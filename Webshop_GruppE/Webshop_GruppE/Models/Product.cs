using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop_GruppE.Models
{
    internal class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public float? Price { get; set; }
        public int? ProductSupplierId { get; set; }
        public string? ProductInfoText { get; set; }
        public int? StockBalance { get; set; }       
        public string? ProductBrand { get; set; }
        public string? Size { get; set; }
        public ICollection<Category> Categories { get; set; }
    }   
}
