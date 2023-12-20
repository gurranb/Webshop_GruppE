using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop_GruppE.Models
{
    internal class ProductInfo
    {
        public int Id { get; set; }
        public string? Size { get; set; }
        public string? Brand { get; set; }
        public string? Color { get; set; }
        public string? Fabrique { get; set; }
        public int? ProductId { get; set; }
    }
}
