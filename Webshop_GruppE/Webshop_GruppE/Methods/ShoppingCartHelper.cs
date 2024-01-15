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
                var shoppingList = (from c in myDb.ShoppingCarts
                                        where c.Id == customerId
                                        select c);

                foreach(var boughtProduct in boughtProducts)
                {
                    var productInfo = (from c in myDb.Products
                                       select c).ToList();

                    //if(boughtProduct == productInfo)
                    //    Console.WriteLine(boughtProduct);      
                }
                Console.ReadKey(true);
            }
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
