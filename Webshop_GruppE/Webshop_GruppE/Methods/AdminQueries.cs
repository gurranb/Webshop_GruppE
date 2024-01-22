using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop_GruppE.Models;

namespace Webshop_GruppE.Methods
{
    internal class AdminQueries
    {

        

        
        
        

        

        public static void AdminQueriesMeth() 
        {

            //Högsta och Lägsta pris.

            //List<Product> highLowPrice = HighLowPrice();
           // foreach (var prod in highLowPrice)
            //{
            //    Console.WriteLine();
            //    Console.WriteLine("Highest price: " + prod.Name + "Price: " + prod.Price);
            //    Console.WriteLine("Lowest price: " + prod.Name + "Price: " + prod.Price);

            //}

            //Högsta och Lägsta antal.

            //List<Product> highLowAmount = HighLowAmount();

            ////Totalt antal produkter.

            //List<Product> totalProducts = TotalProducts();

            ////Totlt summa på alla produkter.

            //List<Product> totalSumMoney = TotalSumMoney();
            Console.ReadKey(true);
        }

        public static float HighLowPrice()
        {
            var connstring = "Server=.\\SQLExpress;Database=FashionCode;Trusted_Connection=True;TrustServerCertificate=True;";
            
            string sql = @"select Max(Price) as MaxPrice from Products";


            var highPrice = 0;
            //List<Product> highLowPrice = new List<Product>();
            using (var myDb = new SqlConnection(connstring))
            {
                //highPrice = myDb.Query<float>(sql).FirstOrDefault();
            }
            return highPrice;
        }
        
        public static List<Product> HighLowAmount()
        {
            var connstring = "Server=.\\SQLExpress;Database=FashionCode;Trusted_Connection=True;TrustServerCertificate=True;";
            
            string sql = "SELECT * FROM Customers";
            
            List<Product> highLowAmount = new List<Product>();
            using (var myDb = new SqlConnection(connstring))
            {
                highLowAmount = myDb.Query<Product>(sql).ToList();
            }
            return highLowAmount;
        }
        
        public static List<Product> TotalProducts()
        {
            var connstring = "Server=.\\SQLExpress;Database=FashionCode;Trusted_Connection=True;TrustServerCertificate=True;";
            
            string sql = "SELECT * FROM Customers";
            
            List<Product> totalProducts = new List<Product>();
            using (var myDb = new SqlConnection(connstring))
            {
                totalProducts = myDb.Query<Product>(sql).ToList();
            }
            return totalProducts;
        }
        
        public static List<Product> TotalSumMoney()
        {
            var connstring = "Server=.\\SQLExpress;Database=FashionCode;Trusted_Connection=True;TrustServerCertificate=True;";
           
            string sql = "SELECT * FROM Customers";
            
            List<Product> totalSumMoney = new List<Product>();
            using (var myDb = new SqlConnection(connstring))
            {
                totalSumMoney = myDb.Query<Product>(sql).ToList();
            }
            return totalSumMoney;
        }
    }
}
