using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop_GruppE.Models;
using Dapper;

namespace Webshop_GruppE.Methods
{
    internal class ShoppingCartHelper
    {
        public static void DisplayAllShoppingCartProducts(int customerId, List<int> boughtProducts)
        {
            using (var myDb = new MyDbContext())
            {
                float? totalSum = 0f;
                float? moms = 1.25f;
                var productInfo = (from c in myDb.Products
                                   select c).ToList();


                foreach (var boughtProduct in boughtProducts)
                {
                    Console.Write("Id: " + boughtProduct);
                    foreach(var product in productInfo)
                    {
                        if(product.Id == boughtProduct)
                        {
                            Console.WriteLine(" Name: " + product.Name + " Price: " + product.Price + "$");
                            totalSum += product.Price;
                        }
                    }    
                }
               
                float? totalMoms = totalSum * moms;
                Console.WriteLine("Total cost for products: " + Math.Round((decimal)totalSum, 2) + "$\nTotal cost inclusive moms: " + Math.Round((decimal)totalMoms, 2) + "$");
                Console.ReadKey(true);

                Console.WriteLine("[1] Remove Product\n[B] Back");
                var key = Console.ReadKey(true);
                switch(key.KeyChar)
                {
                    case '1':
                        RemoveProductFromShoppingList(customerId, boughtProducts);
                        break;
                    case 'b':
                        Helpers.CustomerHomePage(customerId, boughtProducts);
                        break;
                }
                
            }
        }

        public static void RemoveProductFromShoppingList(int customerId, List<int> boughtProducts)
        {          
                Console.Write("Input product Id: ");
                int.TryParse(Console.ReadLine(), out int itemId);

                for (int i = 0; i < boughtProducts.Count; i++)
                {
                    if (boughtProducts[i] == itemId)
                    {
                        boughtProducts.Remove(itemId);
                        Console.WriteLine("You've succesfully removed an item from your shopping cart.");
                    }
                }
            
                Console.ReadKey(true);
                Helpers.CustomerHomePage(customerId, boughtProducts);
            
            

        }
        public static void AddProductToShoppingCart()
        {

        }

        //public static List<Product> GetAllProducts()
        //{
        //    var connstring = "Server=.\\SQLExpress;Database=FashionCode;Trusted_Connection=True;TrustServerCertificate=True;";
        //    string sql = "SELECT * FROM Products";
        //    List<Product> products = new List<Product>();
        //    using (var myDb = new SqlConnection(connstring))
        //    {
        //        products = myDb.Query<Product>(sql).ToList();
        //    }
        //    return products;
        //}

        //public static void ListAllProducts(int customerId)
        //{
        //    List<Product> products = GetAllProducts();

        //    foreach(var product in products)
        //    {
        //        Console.WriteLine(product.Name);
        //    }
        //}
    }
}
