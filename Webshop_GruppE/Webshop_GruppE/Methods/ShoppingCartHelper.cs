using Microsoft.EntityFrameworkCore;
using Webshop_GruppE.Models;

namespace Webshop_GruppE.Methods
{
    internal class ShoppingCartHelper
    {
        public static void DisplayAllShoppingCartProducts(int customerId, List<int> boughtProducts)
        {
            using (var myDb = new MyDbContext())
            {
                Console.Clear();
                float? totalSum = 0f;
                float? moms = 1.25f;
                //var productInfo = (from c in myDb.Products
                //                   select c).ToList();

                var productList = myDb.ShoppingCarts
                                   .Include(c => c.ShoppingCartItems)
                                   .ThenInclude(p => p.Product)
                                   .FirstOrDefault(c => c.CustomerId == customerId);

                if (productList != null)
                {
                    foreach (var shoppingItem in productList.ShoppingCartItems)
                    {
                        var product = shoppingItem.Product;
                        Console.WriteLine("Id: " + product.Id + " Name: " + product.Name + " Price: " + product.Price + "$");
                        totalSum += product.Price;
                    }
                }

                float? totalMoms = totalSum * moms;
                Console.WriteLine("Total cost for products: " + Math.Round((decimal)totalSum, 2) + "$\nTotal cost inclusive moms: " + Math.Round((decimal)totalMoms, 2) + "$");
                Console.ReadKey(true);

                Console.WriteLine("[1] Buy all products\n[2] Remove Product\n[B] Back");

                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case '1':
                        ShippingHelper.ChooseDeliveryMethod(customerId, boughtProducts, totalMoms);
                        break;
                    case '2':
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
            using (var myDB = new MyDbContext())
            {
                var removeItemFromList = myDB.ShoppingCarts
                                   .Include(c => c.ShoppingCartItems)
                                   .ThenInclude(p => p.Product)
                                   .FirstOrDefault(c => c.CustomerId == customerId);

                

                Console.Write("Input product Id: ");
                int.TryParse(Console.ReadLine(), out var productId);

                if (removeItemFromList != null)
                {
                    //int.TryParse(Console.ReadLine(), out int itemId);
                    foreach (var shoppingItem in removeItemFromList.ShoppingCartItems)
                    {
                        var product = shoppingItem.Product;

                        if (product.Id == productId)
                        {                            
                            myDB.Remove(product);
                        }
                    }
                        
                    
                    Console.WriteLine("You've succesfully removed an item from your shopping cart.");
                    myDB.SaveChanges();
                }
                            
                        
                    

                Console.ReadKey(true);
                Helpers.CustomerHomePage(customerId, boughtProducts);

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
