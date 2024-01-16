using Webshop_GruppE.Models;

namespace Webshop_GruppE.Methods
{
    internal class ShippingHelper

    {
        public static void ChooseDeliveryMethod(int customerId, List<int> boughtProducts, float? totalMoms)
        {
            List<string> deliveryText = new List<string> { "[1] Standard delivery ", "[2] Express delivery ", "[B] Back" };
            var userWindow = new Window("Payment", 60, 1, deliveryText);
            userWindow.DrawWindow();

            var key = Console.ReadKey(true);
            switch (key.KeyChar)
            {
                case '1':
                    Console.Write("Standard delivery + 5$ total price: " + (totalMoms += 5.0f) + "$");
                    break;
                case '2':
                    Console.WriteLine("Express delivery + 10$ total price: " + (totalMoms += 10f) + "$");
                    break;

                case 'b':
                    Console.WriteLine("Back");
                    ShoppingCartHelper.DisplayAllShoppingCartProducts(customerId, boughtProducts);
                    break;
                default:
                    Console.WriteLine("Wrong input");
                    Console.ReadKey();
                    break;
            }
        }
        public static void ChoosePaymentMethod(int customerId, List<int> boughtProducts)
        {
            List<string> paymentText = new List<string> { "[1] Pay now ", "[2] Pay later ", "[B] Back" };
            var userWindow = new Window("Payment", 60, 1, paymentText);
            userWindow.DrawWindow();

            var key = Console.ReadKey(true);
            switch (key.KeyChar)
            {
                case '1':
                    Console.Write("Pay now");
                    break;
                case '2':
                    Console.WriteLine("Pay later");

                    break;

                case 'b':
                    Console.WriteLine("Back");
                    ShoppingCartHelper.DisplayAllShoppingCartProducts(customerId, boughtProducts);
                    break;
                default:
                    Console.WriteLine("Wrong input");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
