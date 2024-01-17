using Microsoft.EntityFrameworkCore;
using Webshop_GruppE.Models;

namespace Webshop_GruppE.Methods
{
    internal class ShoppingCartHelper
    {
        public static void DisplayAllShoppingCartProducts(int customerId)
        {
            using (var myDb = new MyDbContext())
            {
                Console.Clear();
                float? totalSum = 0f;
                float? moms = 1.25f;

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
                Console.WriteLine("[1] Buy all products\n[2] Remove Product\n[B] Back");
                var key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case '1':
                        ShippingHelper.ChooseDeliveryMethod(customerId, totalMoms);
                        break;
                    case '2':
                        RemoveProductFromShoppingList(customerId);
                        break;
                    case 'b':
                        Helpers.CustomerHomePage(customerId);
                        break;

                }

            }
        }

        public static void RemoveProductFromShoppingList(int customerId)
        {
            using (var myDB = new MyDbContext())
            {
                var removeItemFromList = myDB.ShoppingCarts
                                   .Include(c => c.ShoppingCartItems)
                                   .ThenInclude(p => p.Product)
                                   .FirstOrDefault(c => c.CustomerId == customerId);

                if (removeItemFromList != null)
                {
                    Console.Write("Input product Id: ");
                    int.TryParse(Console.ReadLine(), out var productId);
                
                    Console.WriteLine("How many would you like to remove?: ");
                    int.TryParse(Console.ReadLine(), out var removeAmount);


                    if (removeAmount > 0 || removeAmount <= removeItemFromList.ShoppingCartItems.Count)
                    {
                        //int.TryParse(Console.ReadLine(), out int itemId);
                        foreach (var shoppingItem in removeItemFromList.ShoppingCartItems)
                        {
                            var product = shoppingItem.Product;

                            if (product.Id == productId)
                            {
                                myDB.Remove(shoppingItem);
                                shoppingItem.Product.StockBalance += 1;
                                removeAmount -= 1;
                            }
                            if(removeAmount == 0)
                            {
                                break;
                            }
                        }
                    }

                    Console.WriteLine("You've succesfully removed an item from your shopping cart.");
                    myDB.SaveChanges();
                }




                Console.ReadKey(true);
                Helpers.CustomerHomePage(customerId);

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
