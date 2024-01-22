using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
        public static void LinqMethods()
        {
            using (var myDb = new MyDbContext())
            {
                var products = myDb.Products.ToList();

                Console.WriteLine("[1] Highest/Lowest Price\n[2] Highest stockvalue\n[3] Products out of stock\n" +
                    "[4] Products that starts with...\n[B] Back");
                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case '1':
                        var maxPrice = (from c in myDb.Products
                                        select c.Price).Max();
                        var minPrice = (from c in myDb.Products
                                        select c.Price).Min();
                        foreach(var product in products)
                        {
                            if (product.Price == maxPrice)
                            {
                                Console.WriteLine("Highest Price: " + product.Name + " Cost: " + product.Price + "$");
                            }
                            if (product.Price == minPrice)
                            {
                                Console.WriteLine("Lowest Price: " + product.Name + " Cost: " + product.Price + "$");
                            }
                        }
                        break;
                    case '2':
                        var highestAmount = (from c in myDb.Products
                                             select c.StockBalance).Max();
                        foreach(var product in products)
                        {
                            if (product.StockBalance == highestAmount)
                            {
                                Console.WriteLine("Highest stock value: " + product.Name + " Units in stock " + product.StockBalance + " units");
                            }
                        }
                        break;
                    case '3':                        
                        foreach(var product in products)
                        {
                            if(product.StockBalance == 0)
                            {
                                Console.WriteLine("Product " + product.Name + " is out of stock! Resupply as soon as possible!");
                            }
                        }
                        break;

                    case '4':
                        Console.Write("Input letter: ");

                        var inputCharacter = Console.ReadLine() ;

                        if (!int.TryParse(inputCharacter, out var number))
                        {
                            inputCharacter = inputCharacter.Substring(0, 1).ToUpper();
                            foreach (var product in products)
                            {
                                if (product.Name.StartsWith(inputCharacter))
                                {
                                    Console.WriteLine("Starts with a " + inputCharacter + ": " + product.Name);
                                }

                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: No numbers");
                        }
                        break;
                    case 'b':                      
                        break;
                }
                Console.ReadKey(true);
                Console.Clear();
            }
                    
        }

        public static void AdminQueriesMethods() 
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
