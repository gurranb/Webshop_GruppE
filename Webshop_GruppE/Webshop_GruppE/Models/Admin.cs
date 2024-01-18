using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop_GruppE.Models
{
    internal class Admin
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AdminName { get; set; }
        public string? AdminPassword { get; set; }       
        public string? EMailAdress { get; set; }
        
    }
}
