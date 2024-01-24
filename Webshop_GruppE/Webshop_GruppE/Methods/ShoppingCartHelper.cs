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
                                   .Include(c => c.ShoppingCartItems.OrderBy(c => c.Product.Id))
                                   .ThenInclude(p => p.Product).
                                   Where(c => c.CustomerId == customerId).FirstOrDefault();



                if (productList != null) //&& productList.ShoppingCartItems.Count > 0)
                {
                    Product previousProduct = null;
                    foreach (var shoppingItem in productList.ShoppingCartItems)
                    {
                        int x = 0;
                        var product = shoppingItem.Product;

                        if (product != previousProduct)
                        {
                            foreach (var shoppingItem2 in productList.ShoppingCartItems)
                            {
                                var product2 = shoppingItem2.Product;

                                if (product.Id == product2.Id)
                                {
                                    x++;
                                }
                            }
                            Console.WriteLine("Id: " + product.Id + " Name: " + product.Name + " Price: " + product.Price + "$" + " x" + x);

                        }

                        previousProduct = product;
                        totalSum += product.Price;
                        
                    }
                }
                else 
                {
                    Console.WriteLine("Shopping cart is empty.");
                }

                float? totalMoms = totalSum * moms;
                Console.WriteLine("===============================================\nTotal cost for products: " + Math.Round((decimal)totalSum, 2) + "$\n===============================================\n" +
                    "Total cost inclusive moms: " + Math.Round((decimal)totalMoms, 2) + "$");
                Console.WriteLine("\n[1] Buy all products\n[2] Add product\n[3] Remove product\n[B] Back");
                var key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case '1':
                        if(productList.ShoppingCartItems.Count > 0)
                        {
                            ShippingHelper.ChooseDeliveryMethod(customerId, totalMoms);
                        }
                        else
                        {
                            Console.WriteLine("Error: Shoppingcart is empty!");
                            Console.ReadKey(true);
                            DisplayAllShoppingCartProducts(customerId);
                        }                       
                        break;
                    case '2':
                        AddProductToShoppingList(customerId);
                        break;
                    case '3':
                        if (productList.ShoppingCartItems.Count > 0)
                        {
                            RemoveProductFromShoppingList(customerId);
                        }
                        else
                        {
                            Console.WriteLine("Error: Shoppingcart is empty!");
                            Console.ReadKey(true);                           
                        }
                        break;
                    case 'b':
                        CustomerHelper.CustomerHomePage(customerId);
                        break;
                    default:
                        Console.Clear();
                        break;

                }

            }
        }
        public static void AddProductToShoppingList(int customerId)
        {
            using (var myDb = new MyDbContext())
            {
                Console.Write("Input product Id: ");
                int.TryParse(Console.ReadLine(), out int productId);

                var chosenProduct = (from c in myDb.Products
                                     where c.Id == productId
                                     select c).SingleOrDefault();

                if (chosenProduct != null)
                {

                    var stockBalance = (from c in myDb.Products
                                        where c.Id == chosenProduct.Id
                                        select c).FirstOrDefault();
                    Console.WriteLine("How many? ");
                    int.TryParse(Console.ReadLine(), out int purchaseAmount);
                    if (purchaseAmount > 0 && purchaseAmount <= chosenProduct.StockBalance)
                    {
                        for (int i = 0; i < purchaseAmount; i++)
                        {

                            if (stockBalance.StockBalance < 1)
                            {
                                Console.WriteLine("Product is out of stock");
                                Console.ReadKey(true);

                            }
                            stockBalance.StockBalance -= 1;
                        }

                        var shoppingCart = myDb.ShoppingCarts
                            .Include(c => c.ShoppingCartItems)
                            .ThenInclude(p => p.Product)
                            .FirstOrDefault(c => c.CustomerId == customerId);


                        for (int i = 0; i < purchaseAmount; i++)
                        {
                            shoppingCart.ShoppingCartItems.Add(new ShoppingCartItem
                            {
                                Product = chosenProduct
                            });
                        }
                        myDb.SaveChanges();
                        Console.WriteLine("You have added " + chosenProduct.Name + " x" + purchaseAmount + " to your shopping cart.");
                        Console.ReadKey(true);

                    }
                    else
                    {
                        Console.WriteLine("Error: Input value is invalid! Maybe the value exceeded the stock amount?");
                        Console.ReadKey(true);
                    }
                    

                }
                else
                {
                    Console.WriteLine("The inputted product Id doesn't match with any of our products, please try again.");
                    Console.ReadKey(true);

                }
            }
            Console.Clear();
            DisplayAllShoppingCartProducts(customerId);
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
                        foreach (var shoppingItem in removeItemFromList.ShoppingCartItems)
                        {
                            var product = shoppingItem.Product;

                            if (product.Id == productId)
                            {
                                myDB.Remove(shoppingItem);
                                shoppingItem.Product.StockBalance += 1;
                                removeAmount -= 1;
                            }
                            if (removeAmount == 0)
                            {
                                break;
                            }

                        }
                    }

                    Console.WriteLine("You've succesfully removed an item from your shopping cart.");
                    myDB.SaveChanges();
                    DisplayAllShoppingCartProducts(customerId);
                }
                Console.ReadKey(true);              

            }

        }
    }
}
