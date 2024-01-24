using Microsoft.EntityFrameworkCore;
using Webshop_GruppE.Models;

namespace Webshop_GruppE.Methods
{
    internal class ShippingHelper

    {
        public static void ChooseDeliveryMethod(int customerId, float? totalMoms)
        {
            using (var myDb = new MyDbContext())
            {
                var productList = myDb.ShoppingCarts
                                                   .Include(c => c.ShoppingCartItems)
                                                   .ThenInclude(p => p.Product)
                                                   .FirstOrDefault(c => c.CustomerId == customerId);

                List<string> deliveryText = new List<string> { "[1] Standard delivery +5$ ", "[2] Express delivery +10$ ", "[B] Back" };
                var userWindow = new Window("Payment", 60, 1, deliveryText);
                userWindow.DrawWindow();

                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case '1':
                        Console.Write("Standard delivery + 5$ total price: " + Math.Round((decimal)(totalMoms += 5.0f), 2) + "$");
                        ChoosePaymentMethod(customerId, totalMoms);
                        break;
                    case '2':
                        Console.WriteLine("Express delivery + 10$ total price: " + Math.Round((decimal)(totalMoms += 10.0f), 2) + "$");
                        ChoosePaymentMethod(customerId, totalMoms);
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
        public static void ChoosePaymentMethod(int customerId, float? totalMoms)
        {
            List<string> paymentText = new List<string> { "[1] Pay now ", "[2] Pay later ", "[B] Back", "Total: " + Math.Round((decimal)(totalMoms), 2) + "$" };
            var userWindow = new Window("Payment", 60, 10, paymentText);
            userWindow.DrawWindow();
            using (var myDb = new MyDbContext())
            {
                var productList = myDb.ShoppingCarts
                                                   .Include(c => c.ShoppingCartItems)
                                                   .ThenInclude(p => p.Product)
                                                   .FirstOrDefault(c => c.CustomerId == customerId);
                var key = Console.ReadKey(true);
                Console.Clear();
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
                        Console.WriteLine("Thank you for your purchase!");
                        Console.ReadKey(true);
                        break;

                    case '2':
                        if (productList != null)
                        {
                            foreach (var shoppingItem in productList.ShoppingCartItems)
                            {
                                myDb.Remove(shoppingItem);
                            }
                        }
                        myDb.SaveChanges();
                        Console.WriteLine("Thank you for your purchase!\nA bill will be sent to you!");
                        Console.ReadKey(true);
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
                Console.Clear();
            }
        }
    }
}
