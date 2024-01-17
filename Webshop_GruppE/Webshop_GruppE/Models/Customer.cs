using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Webshop_GruppE.Models
{
    internal class Customer
    {       
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? CustomerUserName { get; set; }
        public string? CustomerPassword { get; set; }
        public string? Country { get; set; }
        public string? StreetAddress { get; set; }
        public int? PostalCode { get; set; }
        public int? CardNumber { get; set; }
        public string? EMailAdress { get; set;}
        public ICollection<Order> Orders { get; set; }
       
    }
}
