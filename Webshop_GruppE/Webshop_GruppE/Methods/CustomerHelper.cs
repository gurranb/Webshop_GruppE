using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop_GruppE.Models;

namespace Webshop_GruppE.Methods
{
    internal class CustomerHelper
    {
        public static void CustomerHomePage(int customerId)
        {
            Console.Clear();

            while (true)
            {
                LogoWindow.LogoWindowMeth(1, 1, 24, 7);
                var productText = DisplayDatabase.DisplayChosenProducts();
                using (var myDb = new MyDbContext())
                {
                    var customerUserName = (from c in myDb.Customers
                                            where c.Id == customerId
                                            select c.CustomerUserName).SingleOrDefault();

                    List<string> userText = new List<string> { "[1] Search Products", "[2] Browse Products", "[S] Shopping Cart", "[P] Profile Page", "[B] Buy Products", "[L] Logout" };
                    var userWindow = new Window("Welcome " + customerUserName, 1, 10, userText);
                    userWindow.DrawWindow();

                    var productsWindow = new Window("Fashion Deals", 30, 1, productText);
                    productsWindow.DrawWindow();
                }

                var key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case '1':
                        Console.WriteLine("Search Products");
                        SearchProducts(customerId);
                        break;
                    case '2':
                        Console.WriteLine("Browse Products");
                        BrowseProducts(customerId);
                        break;
                    case 's':
                        ShoppingCartHelper.DisplayAllShoppingCartProducts(customerId);
                        break;
                    case 'p':
                        DisplayDatabase.DisplayCustomerDetails(customerId);
                        break;
                    case 'b':
                        PurchaseProduct(customerId);
                        break;                   
                    case 'l':
                        Console.Clear();
                        Program.StartScreen();
                        break;
                    default:
                        Console.WriteLine("Wrong Input");
                        Console.ReadKey(true);
                        break;
                }

            }
        }
        public static void SearchProducts(int customerId)
        {
            Console.Write("Input searchword: ");
            string searchWord = Console.ReadLine();

            using (var myDb = new MyDbContext())
            {
                var searchedProduct = (from c in myDb.Products
                                       where c.Name.Contains(searchWord)
                                       select c);
                foreach (var product in searchedProduct)
                {
                    Console.WriteLine("Id: " + product.Id + ", " + product.Name);
                }

                if (searchedProduct.Count() > 0)
                {
                    Console.WriteLine("Press 'P' to purchase product or 'B' to go back");
                    var key = Console.ReadKey(true);
                    switch (key.KeyChar)
                    {
                        case 'p':
                            Console.WriteLine("Purchase product");
                            PurchaseProduct(customerId);
                            break;
                        case 'b':
                            Console.WriteLine("Back");
                            CustomerHomePage(customerId);
                            break;

                    }
                }
                else
                {
                    Console.WriteLine("The searchword doesn't match with any products, try again.");
                    Console.ReadKey(true);
                }
                Console.Clear();
            }
        }

        public static void BrowseProducts(int customerId)
        {
            
            LogoWindow.LogoWindowMeth(1, 1, 24, 7);

            List<string> categoryText = DisplayDatabase.DisplayAllCategories();
            var categoryWindow = new Window("Category List", 30, 10, categoryText);
            categoryWindow.DrawWindow();

            Console.Write("Input Category Id: ");
            int.TryParse(Console.ReadLine(), out int categoryId);
            using (var myDb = new MyDbContext())
            {
                var chosenCategory = (from c in myDb.Products
                                      where c.Categories.Any(x => x.Id == categoryId)
                                      select c);

                foreach (var product in chosenCategory)
                {
                    Console.WriteLine(product.Id + " " + product.Name);
                }


                if (chosenCategory.Count() > 0)
                {
                    Console.WriteLine("Press 'P' to purchase product or 'B' to go back");
                    var key = Console.ReadKey(true);
                    switch (key.KeyChar)
                    {
                        case 'p':
                            Console.WriteLine("Purchase product");
                            PurchaseProduct(customerId);
                            break;
                        case 'b':
                            Console.WriteLine("Back");
                            CustomerHomePage(customerId);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("The category Id doesn't match with any of our categories, try again.");
                    Console.ReadKey(true);
                }
                Console.Clear();
            }
        }

        public static void PurchaseProduct(int customerId)
        {

            while (true)
            {
                Console.Write("Input product Id: ");

                int.TryParse(Console.ReadLine(), out int productId);
                using (var myDb = new MyDbContext())
                {
                    var chosenProduct = (from c in myDb.Products
                                         where c.Id == productId
                                         select c).SingleOrDefault();


                    if (chosenProduct != null)
                    {
                        Console.WriteLine("Id: " + chosenProduct.Id + " " + " Name: " + chosenProduct.Name + " Price: " + chosenProduct.Price + " Units In Stock: " + chosenProduct.StockBalance +
                             " Product Info: " + chosenProduct.ProductInfoText + " Product Size: " + chosenProduct.Size);
                        Console.WriteLine("Buy this product? y/n");
                        var answer = Console.ReadKey(true);


                        switch (answer.KeyChar)
                        {
                            case 'y':
                                var stockBalance = (from c in myDb.Products
                                                    where c.Id == chosenProduct.Id
                                                    select c).FirstOrDefault();
                                Console.WriteLine("How many? ");
                                int.TryParse(Console.ReadLine(), out int purchaseAmount);

                                for (int i = 0; i < purchaseAmount; i++)
                                {

                                    if (stockBalance.StockBalance < 1)
                                    {
                                        Console.WriteLine("Product is out of stock");
                                        Console.ReadKey(true);
                                        break;
                                    }
                                    stockBalance.StockBalance -= 1;
                                }

                                var shoppingCart = myDb.ShoppingCarts
                                    .Include(c => c.ShoppingCartItems)
                                    .ThenInclude(p => p.Product)
                                    .FirstOrDefault(c => c.CustomerId == customerId);

                                if (shoppingCart == null)
                                {
                                    shoppingCart = new ShoppingCart
                                    {
                                        CustomerId = customerId,
                                        ShoppingCartItems = new List<ShoppingCartItem>()
                                    };
                                    myDb.ShoppingCarts.Add(shoppingCart);
                                }
                                for (int i = 0; i < purchaseAmount; i++)
                                {
                                    shoppingCart.ShoppingCartItems.Add(new ShoppingCartItem
                                    {
                                        Product = chosenProduct
                                    });
                                }


                                Console.WriteLine("You have added " + chosenProduct.Name + " x" + purchaseAmount + " to your shopping cart.");
                                Console.ReadKey(true);
                                break;
                            case 'n':
                                CustomerHomePage(customerId);
                                break;
                        }
                        myDb.SaveChanges();
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("The inputted product Id doesn't match with any of our products, please try again.");

                        Console.ReadKey(true);
                        CustomerHomePage(customerId);
                    }
                }
            }
        }
    }
}
