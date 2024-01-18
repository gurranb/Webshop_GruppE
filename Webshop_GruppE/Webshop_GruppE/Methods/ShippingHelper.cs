using Microsoft.EntityFrameworkCore;
using Webshop_GruppE.Models;

namespace Webshop_GruppE.Methods
{
    internal class ShippingHelper

    {
        public static void ChooseDeliveryMethod(int customerId, float? totalMoms)
        {
            Console.Clear();
            LogoWindow.LogoWindowMeth(1, 1, 24, 7);
            using (var myDb = new MyDbContext())
            {
                var productList = myDb.ShoppingCarts
                                                   .Include(c => c.ShoppingCartItems)
                                                   .ThenInclude(p => p.Product)
                                                   .FirstOrDefault(c => c.CustomerId == customerId);

                List<string> deliveryText = new List<string> { "[1] Standard delivery +5$ ", "[2] Express delivery +10$ ", "[B] Back" };
                var userWindow = new Window("Payment", 62, 1, deliveryText);
                userWindow.DrawWindow();

                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case '1':
                        Console.Write("Standard delivery + 5$ total price: " + (totalMoms += 5.0f) + "$");
                        ChoosePaymentMethod(customerId);
                        break;
                    case '2':
                        Console.WriteLine("Express delivery + 10$ total price: " + (totalMoms += 10f) + "$");
                        ChoosePaymentMethod(customerId);
                        break;
                    case 'b':
                        Console.WriteLine("Back");
                        ShoppingCartHelper.DisplayAllShoppingCartProducts(customerId);
                        break;
                    default:
                        Console.WriteLine("Wrong input");
                        Console.ReadKey();
                        break;
                }

            }
        }
        public static void ChoosePaymentMethod(int customerId)
        {
            List<string> paymentText = new List<string> { "[1] Pay now ", "[2] Pay later ", "[B] Back" };
            var userWindow = new Window("Payment", 30, 1, paymentText);
            userWindow.DrawWindow();
            using (var myDb = new MyDbContext())
            {

                var productList = myDb.ShoppingCarts
                                                   .Include(c => c.ShoppingCartItems)
                                                   .ThenInclude(p => p.Product)
                                                   .FirstOrDefault(c => c.CustomerId == customerId);

                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case '1':
                        var orderList = myDb.Orders
                                    .Include(c => c.OrderItems)
                                    .ThenInclude(p => p.Product)
                                    .FirstOrDefault(c => c.CustomerId == customerId);
                        
                        if (productList != null)
                        {                          
                            foreach (var shoppingItem in productList.ShoppingCartItems)
                            {  
                                myDb.Remove(shoppingItem);
                            }
                        }
                        myDb.SaveChanges();
                        break;

                    case '2':
                        Console.WriteLine("Pay later");
                        if (productList != null)
                        {
                            foreach (var shoppingItem in productList.ShoppingCartItems)
                            {
                                myDb.Remove(shoppingItem);
                            }
                        }
                        myDb.SaveChanges();
                        break;

                    case 'b':
                        Console.WriteLine("Back");
                        ShoppingCartHelper.DisplayAllShoppingCartProducts(customerId);
                        break;
                    default:
                        Console.WriteLine("Wrong input");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
