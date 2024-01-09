﻿using Webshop_GruppE.Models;

namespace Webshop_GruppE
{
    internal class Helpers
    {

        public static void Login()
        {
            bool loggedin = false;
            Console.Clear();
            while (loggedin == false)
            {
                List<string> loginText = new List<string> { "Welcome to FashionCode website", "Login as", "[A]dmin", "[U]ser", "[E]xit" };
                var loginWindow = new Window("", 1, 1, loginText);
                loginWindow.DrawWindow();
                var key = Console.ReadKey(true);

                // ändra så detta blir snyggare
                switch (key.KeyChar)
                {
                    case 'a':
                        loggedin = true;
                        Admin();
                        break;
                    case 'u':
                        loggedin = true;
                        User();
                        break;
                    case 'e':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Wrong Input");
                        Console.ReadKey();
                        break;
                }
                Console.Clear();
            }
        }

        public static void Admin()
        {
            Console.Clear();
            bool loggedin = true;
            while (loggedin)
            {
                List<string> adminText = new List<string> { "[1] Edit Product", "[2] Edit Category", "[P] Profile Page", "[C] Customer Page", "[Q] Queries", "[L] Logout" };
                var adminWindow = new Window("Admin", 1, 1, adminText);
                adminWindow.DrawWindow();

                var key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case '1':
                        Console.WriteLine("Add Product");
                        ProductMenu();
                        break;
                    case '2':
                        Console.WriteLine("Add Category");
                        CategoryMenu();
                        break;
                    case 'p':
                        Console.WriteLine("Profile Page");
                        break;
                    case 'c':
                        Console.WriteLine("Customer Page");
                        break;
                    case 'q':
                        Console.WriteLine("Queries");
                        break;
                    case 'l':
                        loggedin = false;
                        Console.WriteLine("Logout");
                        Login();
                        break;
                    default:
                        Console.WriteLine("Wrong Input");
                        Console.ReadKey(true);
                        break;
                }
            }
        }
        public static void User()
        {
            Console.Clear();
            bool loggedin = true;
            while (loggedin)
            {
                List<string> userText = new List<string> { "[S] Shopping Cart", "[P] Profile Page", "[B] Buy Products", "[O] Order History", "[L] Logout" };
                var userWindow = new Window("Customer", 1, 1, userText);
                userWindow.DrawWindow();

                var key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case 's':
                        Console.WriteLine("");
                        break;
                    case 'p':
                        Console.WriteLine("");
                        break;
                    case 'b':
                        Console.WriteLine("");
                        break;
                    case 'o':
                        Console.WriteLine("");
                        break;
                    case 'l':
                        loggedin = false;
                        Console.Clear();
                        Login();
                        break;
                    default:
                        Console.WriteLine("Wrong Input");
                        Console.ReadKey(true);
                        break;
                }
            }
        }

        public static void ProductMenu()
        {
            Console.Clear();

            while (true)
            {
                List<string> productText = new List<string> { "[A] Add Product", "[C] Change Product", "[R] Remove Product", "[B] Back" };
                var productWindow = new Window("Product Menu", 1, 1, productText);
                Database.DisplayAllCategories();
                Database.DisplayAllProducts();

                productWindow.DrawWindow();

                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case 'a':
                        Console.WriteLine("Add Product");
                        AddProduct();
                        break;
                    case 'c':
                        ChangeProduct();
                        break;
                    case 'r':
                        Console.WriteLine("Remove Product");
                        RemoveProduct();
                        break;
                    case 'b':
                        Console.WriteLine("Back");
                        Admin();
                        break;
                    default:
                        Console.WriteLine("Wrong Input");
                        break;
                }
            }
        }
        public static void AddProduct()
        {
            // Fixa felinmatningsmetod
            using (var myDb = new MyDbContext())
            {
                Console.Write("Type Category ID: ");
                int categoryId = int.Parse(Console.ReadLine());
                Console.Write("Type Product Name: ");
                string productName = Console.ReadLine();
                Console.Write("Type product Price: ");
                float productPrice = float.Parse(Console.ReadLine());
                Console.Write("Type productsupplier ID: ");
                int productSupplierId = int.Parse(Console.ReadLine());
                Console.Write("Type productinfo: ");
                string productInfo = Console.ReadLine();
                Console.Write("Type stockbalance: ");
                int stockBalance = int.Parse(Console.ReadLine());
                Console.Write("Show product on Homepage? Type 'true' or 'false': ");
                bool selectedProduct = bool.Parse(Console.ReadLine());

                myDb.Add(new Models.Product
                {
                    Name = productName,
                    CategoryId = categoryId,
                    Price = productPrice,
                    ProductSupplierId = productSupplierId,
                    ProductInfo = productInfo,
                    StockBalance = stockBalance,
                    SelectedProduct = selectedProduct
                });
                myDb.SaveChanges();
                Console.WriteLine("You have added " + productName + " to the list");
            }
            Console.Clear();
        }

        public static void ChangeProduct()
        {

            using (var myDb = new MyDbContext())
            {
                List<string> changeProductText = new List<string> { "[1] Change product name", "[2] Change product price","[3] Change category Id", "[4] Change product supplier Id",
                        "[5] Change product info", "[6] Change product stock balance", "[B] Back" };
                var changeProductWindow = new Window("Change Product Menu", 0, 3, changeProductText);
                changeProductWindow.DrawWindow();

                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case '1':
                        Console.WriteLine("[1] Change product name");
                        ChangeProductName();
                        break;
                    case '2':
                        Console.WriteLine("[2] Change product price");
                        ChangeProductPrice();
                        break;
                    case '3':
                        Console.WriteLine("[3] Change category Id");
                        ChangeCategoryId();
                        break;
                    case '4':
                        Console.WriteLine("[4] Change product supplier Id");
                        ChangeProductSupplier();
                        break;
                    case '5':
                        Console.WriteLine("[5] Change product info");
                        ChangeProductInfo();
                        break;
                    case '6':
                        Console.WriteLine("[6] Change product stock balance");
                        ChangeStockBalance();
                        break;
                    case 'b':
                        ProductMenu();
                        break;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;

                }
            }

        }
        public static void ChangeProductName()
        {
            using (var myDb = new MyDbContext())
            {

                Console.Write("Input product Id: ");
                int productId = int.Parse(Console.ReadLine());
                Console.Write("Input new product Name: ");
                string productName2 = Console.ReadLine();
                var newName = (from c in myDb.Products
                               where c.Id == productId
                               select c).SingleOrDefault();

                if (newName != null)
                {
                    newName.Name = productName2;
                    Console.WriteLine("You have successfully changed the product Name to " + productName2);
                    Console.ReadKey();
                    myDb.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Error wrong ID");
                    Console.ReadKey();
                }
            }
            Console.Clear();
        }
        public static void ChangeProductPrice()
        {
            using (var myDb = new MyDbContext())
            {

                Console.Write("Input product Id: ");
                int productId = int.Parse(Console.ReadLine());
                Console.Write("Input new product price: ");
                float productPrice2 = float.Parse(Console.ReadLine());
                var newPrice = (from c in myDb.Products
                                where c.Id == productId
                                select c).SingleOrDefault();

                if (newPrice != null)
                {
                    newPrice.Price = productPrice2;
                    Console.WriteLine("You have successfully changed the product price to " + productPrice2 + " $");
                    Console.ReadKey();
                    myDb.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Error wrong ID");
                    Console.ReadKey();
                }
            }
            Console.Clear();
        }
        public static void ChangeCategoryId()
        {
            using (var myDb = new MyDbContext())
            {

                Console.Write("Input product Id: ");
                int productId = int.Parse(Console.ReadLine());
                Console.Write("Input new category Id: ");
                int categoryId2 = int.Parse(Console.ReadLine());
                var newCategoryId = (from c in myDb.Products
                                where c.Id == productId
                                select c).SingleOrDefault();

                if (newCategoryId != null)
                {
                    newCategoryId.CategoryId = categoryId2;
                    Console.WriteLine("You have successfully changed the category Id to: " + categoryId2);
                    Console.ReadKey();
                    myDb.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Error wrong ID");
                    Console.ReadKey();
                }
            }
            Console.Clear();
        }
        public static void ChangeProductSupplier()
        {
            using (var myDb = new MyDbContext())
            {

                Console.Write("Input product Id: ");
                int productId = int.Parse(Console.ReadLine());
                Console.Write("Input new product supplier Id: ");
                int productSupplierId2 = int.Parse(Console.ReadLine());
                var newSupplierId = (from c in myDb.Products
                                where c.Id == productId
                                select c).SingleOrDefault();

                if (newSupplierId != null)
                {
                    newSupplierId.ProductSupplierId = productSupplierId2;
                    Console.WriteLine("You have successfully changed the product supplier Id to " + productSupplierId2);
                    Console.ReadKey();
                    myDb.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Error wrong ID");
                    Console.ReadKey();
                }
            }
            Console.Clear();
        }

        public static void ChangeProductInfo()
        {
            using (var myDb = new MyDbContext())
            {

                Console.Write("Input product Id: ");
                int productId = int.Parse(Console.ReadLine());
                Console.WriteLine("Input new product info: ");
                string productInfo2 = Console.ReadLine();
                var newProductInfo = (from c in myDb.Products
                                     where c.Id == productId
                                     select c).SingleOrDefault();

                if (newProductInfo != null)
                {
                    newProductInfo.ProductInfo = productInfo2;
                    Console.WriteLine("You have successfully changed the product info to\n" + productInfo2);
                    Console.ReadKey();
                    myDb.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Error wrong ID");
                    Console.ReadKey();
                }
            }
            Console.Clear();
        }
        public static void ChangeStockBalance()
        {
            using (var myDb = new MyDbContext())
            {

                Console.Write("Input product Id: ");
                int productId = int.Parse(Console.ReadLine());
                Console.Write("Input new stock balance: ");
                int productStockBalance2 = int.Parse(Console.ReadLine());
                var newStockBalance = (from c in myDb.Products
                                      where c.Id == productId
                                      select c).SingleOrDefault();

                if (newStockBalance != null)
                {
                    newStockBalance.StockBalance = productStockBalance2;
                    Console.WriteLine("You have successfully changed the product stock balance to: " + productStockBalance2 + " units");
                    Console.ReadKey();
                    myDb.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Error wrong ID");
                    Console.ReadKey();
                }
            }
            Console.Clear();
        }

        public static void RemoveProduct()
        {
            using (var myDb = new MyDbContext())
            {

                Console.Write("Input product Id: ");
                int productId = int.Parse(Console.ReadLine());
                var removeProduct = (from c in myDb.Products
                                     where c.Id == productId
                                     select c).SingleOrDefault();

                if (removeProduct != null)
                {
                    myDb.Remove(removeProduct);
                    myDb.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Error wrong ID");
                }
            }
            Console.Clear();

        }
        public static void CategoryMenu()
        {
            Console.Clear();

            while (true)
            {

                List<string> categoryText = new List<string> { "[A] Add Category", "[C] Change Category", "[R] Remove Category", "[B] Back" };
                var categoryWindow = new Window("Category Menu", 1, 1, categoryText);
                categoryWindow.DrawWindow();
                Database.DisplayAllCategories();
                using (var myDb = new MyDbContext())
                {

                }

                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case 'a':
                        Console.WriteLine("Add Category");
                        AddCategory();
                        break;
                    case 'c':
                        Console.WriteLine("Change Category Name");
                        ChangeCategory();
                        break;
                    case 'r':
                        Console.WriteLine("Remove Category");
                        RemoveCategory();
                        break;
                    case 'b':
                        Console.WriteLine("Back");
                        Admin();
                        break;
                    default:
                        Console.WriteLine("Wrong Input");
                        break;
                }

            }
        }

        public static void AddCategory()
        {
            using (var myDb = new MyDbContext())
            {
                Console.Write("Type Category Name: ");
                string categoryName = Console.ReadLine();
                myDb.Add(new Models.Category { CategoryName = categoryName });
                myDb.SaveChanges();
                Console.WriteLine("You have added " + categoryName + " to the list");
            }
            Console.Clear();
        }
        public static void ChangeCategory()
        {
            using (var myDb = new MyDbContext())
            {

                Console.Write("Input category Id: ");
                int categoryId = int.Parse(Console.ReadLine());
                Console.Write("Input new category Name: ");
                string categoryName2 = Console.ReadLine();
                var newName = (from c in myDb.Categories
                               where c.Id == categoryId
                               select c).SingleOrDefault();

                if (newName != null)
                {
                    newName.CategoryName = categoryName2;
                    Console.WriteLine("You have successfully changed the category Name to " + categoryName2);
                    Console.ReadKey();
                    myDb.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Error wrong ID");
                    Console.ReadKey();
                }
            }
            Console.Clear();

        }
        public static void RemoveCategory()
        {
            using (var myDb = new MyDbContext())
            {

                Console.Write("Input category Id: ");
                int categoryId = int.Parse(Console.ReadLine());
                var removeCategory = (from c in myDb.Categories
                                      where c.Id == categoryId
                                      select c).SingleOrDefault();

                if (removeCategory != null)
                {
                    myDb.Remove(removeCategory);
                    Console.WriteLine("You have successfully removed category Id " + categoryId);
                    Console.ReadKey();
                    myDb.SaveChanges();

                }
                else
                {
                    Console.WriteLine("Error wrong ID");
                    Console.ReadKey();
                }

            }
            Console.Clear();

        }
    }
}
