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
                        var outOfStockProducts = (from c in myDb.Products
                                                  where c.StockBalance == 0
                                                  select c).ToList();
                        if(outOfStockProducts.Count != 0)
                        {
                            foreach (var product in products)
                            {

                                if (product.StockBalance == 0)
                                {
                                    Console.WriteLine("Product " + product.Name + " is out of stock! Resupply as soon as possible!");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No products are out of stock!");
                            Console.ReadKey(true);
                        }
                        
                        break;

                    case '4':
                        Console.Write("Input letter: ");

                        var inputCharacter = Console.ReadLine() ;
                        List<string> productList = new List<string>() ;
                        if (!int.TryParse(inputCharacter, out var number))
                        {
                            inputCharacter = inputCharacter.Substring(0, 1).ToUpper();
                            foreach (var product in products)
                            {
                                if (product.Name.StartsWith(inputCharacter))
                                {
                                    productList.Add("Starts with a " + inputCharacter + ": " + product.Name);
                                }

                            }
                            if(productList.Count > 0)
                            {
                                foreach (var product in productList)
                                {
                                    Console.WriteLine(product);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error! No products starts with the letter " + inputCharacter + "!");
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
        
    }
}
