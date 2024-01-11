﻿using Webshop_GruppE.Models;

namespace Webshop_GruppE
{
    internal class Helpers
    {
        public static void StartScreen()
        {
            Console.Clear();
            while (true)
            {
                List<string> loginText = new List<string> { "Welcome to FashionCode website", "Login as", "[A]dmin", "[U]ser", "[E]xit" };
                var loginWindow = new Window("", 1, 1, loginText);
                loginWindow.DrawWindow();
                var key = Console.ReadKey(true);

                // ändra så detta blir snyggare
                switch (key.KeyChar)
                {
                    case 'a':
                        LogInAdminMenu();
                        break;
                    case 'u':
                        LogInCustomerMenu();
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

        public static void LogInAdminMenu()
        {
            while (true)
            {
                Console.Clear();
                List<string> profileText = new List<string> { "[1] Emergency Log in ", "[2] Log in ", "[3] Sign up", "[B] Back" };
                var userWindow = new Window("Sign in as admin", 1, 1, profileText);
                userWindow.DrawWindow();
                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case '1':
                        Console.WriteLine("Emergency Log in");
                        AdminHomePage();
                        break;
                    case '2':
                        Console.WriteLine("Log in");
                        LogInAdmin();
                        break;
                    case '3':
                        Console.WriteLine("Sign up");
                        SignUpAdmin();
                        break;
                    case 'b':
                        Console.WriteLine("Back");
                        StartScreen();
                        break;
                    default:
                        Console.WriteLine("Wrong input");
                        Console.ReadKey();
                        break;
                }
            }
        }
        public static void LogInAdmin()
        {
            while (true)
            {
                using (var myDb = new MyDbContext())
                {
                    Console.WriteLine("Input Username: ");
                    string userName = Console.ReadLine();
                    var findUserName = (from c in myDb.Admins
                                        where c.AdminName == userName
                                        select c.AdminName).SingleOrDefault();

                    Console.WriteLine("Input Password: ");
                    string password = Console.ReadLine();
                    var findUserPassword = (from c in myDb.Admins
                                            where c.AdminPassword == password
                                            select c.AdminPassword).SingleOrDefault();

                    if (findUserName == null || findUserPassword == null)
                    {
                        Console.WriteLine("Error, username or password doesn´t exist!");
                        Console.ReadKey(true);
                    }
                    else if (findUserName.Contains(userName) && findUserPassword.Contains(password))
                    {
                        AdminHomePage();
                    }
                }
            }
        }

        

        public static void SignUpAdmin()
        {
            using (var myDb = new MyDbContext())
            {
                Console.Write("Input your first name: ");
                string adminFirstName = Console.ReadLine();
                Console.Write("Input your last name: ");
                string adminLastName = Console.ReadLine();
                Console.Write("Input your user name: ");
                string adminUserName = Console.ReadLine();
                Console.Write("Input your password: ");
                string adminPassword = Console.ReadLine();
                Console.Write("Input your mail address: ");
                string adminMailAddress = Console.ReadLine();

                myDb.Add(new Models.Admin
                {
                    FirstName = adminFirstName,
                    LastName = adminLastName,
                    AdminName = adminUserName,
                    AdminPassword = adminPassword,
                    EMailAdress = adminMailAddress

                });
                Console.WriteLine("You have successfully created your account!");
                Console.ReadKey(true);
                myDb.SaveChanges();
            }
            Console.ReadKey(true);
            Console.Clear();
        }

        public static void AdminHomePage()
        {
            Console.Clear();

            while (true)
            {
                List<string> adminText = new List<string> { "[1] Edit Product", "[2] Edit Category", "[3] Product Overview", "[4] Edit Suppliers", "[P] Profile Page", "[C] Customer Page", "[Q] Queries", "[L] Logout" };
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
                    case '3':
                        Console.WriteLine("Product Overview");
                        ProductOverview();
                        break;
                    case '4':
                        Console.WriteLine("Edit Suppliers");
                        SupplierMenu();
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
                        Console.WriteLine("Logout");
                        StartScreen();
                        break;
                    default:
                        Console.WriteLine("Wrong Input");
                        Console.ReadKey(true);
                        break;
                }
            }
        }

        public static void SupplierMenu()
        {
            Console.Clear();

            while (true)
            {
                Database.DisplayAllSuppliers();
                List<string> supplierText = new List<string> { "[1] Add supplier", "[2] Edit supplier", "[3] Remove supplier", "[B] Back" };
                var userWindow = new Window("Supplier Menu", 1, 1, supplierText);
                userWindow.DrawWindow();

                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case '1':
                        AddSupplier();
                        break;
                    case '2':
                        EditSupplier();
                        break;
                    case '3':
                        RemoveSupplier();
                        break;
                    case 'b':
                        AdminHomePage();
                        break;
                }
            }
        }

        public static void AddSupplier()
        {
            using (var myDb = new MyDbContext())
            {
                Console.WriteLine("Input Supplier Name: ");
                string supplierName = Console.ReadLine();
                myDb.Add(new Models.ProductSupplier { Name = supplierName });
                myDb.SaveChanges();
                Console.WriteLine("You have added " + supplierName + " to the list");
            }
            Console.ReadKey(true);
            Console.Clear();
        }

        public static void EditSupplier()
        {

            using (var myDb = new MyDbContext())
            {
                Console.WriteLine("Change supplier name.\n\nInput Supplier Id: ");
                int.TryParse(Console.ReadLine(), out int supplierId);
                Console.WriteLine("Input new name for supplier: ");
                string newNameString = Console.ReadLine();

                var newName = (from c in myDb.ProductSuppliers
                               where c.Id == supplierId
                               select c).SingleOrDefault();

                if (newName != null)
                {
                    newName.Name = newNameString;
                    Console.WriteLine("Succesfully changed supplier name to " + newName.Name);
                    Console.ReadKey();
                    myDb.SaveChanges();

                }
                else
                {
                    Console.WriteLine("Error, wrong Id");
                    Console.ReadKey();
                }
            }
            Console.Clear();
        }

        public static void RemoveSupplier()
        {
            using (var myDb = new MyDbContext())
            {
                Console.WriteLine("Input Id for the supplier you wish to remove");
                int.TryParse(Console.ReadLine(), out int supplierId);

                var removeSupplier = (from c in myDb.ProductSuppliers
                                      where c.Id == supplierId
                                      select c).SingleOrDefault();
                if (removeSupplier != null)
                {
                    myDb.Remove(removeSupplier);
                    Console.WriteLine("You've successfully removed " + removeSupplier.Name + ".");
                    myDb.SaveChanges();
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Error, wrong Id");
                    Console.ReadKey();
                }
                Console.Clear();
            }
        }
        public static void LogInCustomerMenu()
        {
            while (true)
            {
                Console.Clear();
                List<string> profileText = new List<string> { "[1] Emergency Log in ", "[2] Log in ", "[3] Sign up", "[B] Back" };
                var userWindow = new Window("Sign in", 1, 1, profileText);
                userWindow.DrawWindow();
                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case '1':
                        Console.WriteLine("Emergency Log in");
                        CustomerHomePage();
                        break;
                    case '2':
                        Console.WriteLine("Log in");
                        LogInCustomer();
                        break;
                    case '3':
                        Console.WriteLine("Sign up");
                        CustomerSignUp();
                        break;
                    case 'b':
                        Console.WriteLine("Back");
                        StartScreen();
                        break;
                    default:
                        Console.WriteLine("Wrong input");
                        Console.ReadKey();
                        break;
                }
            }
        }



        public static void CustomerHomePage()
        {
            Console.Clear();

            Database.DisplayChosenProducts();
            while (true)
            {
                List<string> userText = new List<string> { "[1] Search Products", "[2] Browse Products", "[S] Shopping Cart", "[P] Profile Page", "[B] Buy Products", "[O] Order History", "[L] Logout" };
                var userWindow = new Window("Customer", 1, 1, userText);
                userWindow.DrawWindow();

                var key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case '1':
                        Console.WriteLine("Search Products");
                        SearchProducts();
                        break;
                    case '2':
                        Console.WriteLine("Browse Products");
                        BrowseProducts();
                        break;
                    case 's':
                        Console.WriteLine("");
                        break;
                    case 'p':
                        Console.WriteLine("");
                        break;
                    case 'b':
                        PurchaseProduct();
                        break;
                    case 'o':
                        Console.WriteLine("");
                        break;
                    case 'l':
                        Console.Clear();
                        StartScreen();
                        break;
                    default:
                        Console.WriteLine("Wrong Input");
                        Console.ReadKey(true);
                        break;
                }

            }
        }

        public static void LogInCustomer()
        {
            while (true)
            {
                using (var myDb = new MyDbContext())
                {
                    Console.WriteLine("Input Username: ");
                    string userName = Console.ReadLine();
                    var findUserName = (from c in myDb.Customers
                                           where c.CustomerUserName == userName
                                           select c.CustomerUserName).SingleOrDefault();

                    Console.WriteLine("Input Password: ");
                    string password = Console.ReadLine();
                    var findUserPassword = (from c in myDb.Customers
                                               where c.CustomerPassword == password
                                               select c.CustomerPassword).SingleOrDefault();

                    if(findUserName == null || findUserPassword == null)
                    {
                        Console.WriteLine("Error, username or password doesn´t exist!");
                        Console.ReadKey(true);
                    }
                    else if (findUserName.Contains(userName) && findUserPassword.Contains(password))
                    {
                        CustomerHomePage();
                    }
                }
            }

        }

        public static void CustomerSignUp()
        {
            using (var myDb = new MyDbContext())
            {

                Console.Write("Input your first name: ");
                string customerFirstName = Console.ReadLine();
                Console.Write("Input your last name: ");
                string customerLastName = Console.ReadLine();
                Console.Write("Input your age: ");
                int.TryParse(Console.ReadLine(), out int customerAge);
                Console.Write("Input your user name: ");
                string customerUserName = Console.ReadLine();
                Console.Write("Input your password: ");
                string customerPassword = Console.ReadLine();
                Console.Write("Input your country: ");
                string customerCountry = Console.ReadLine();
                Console.Write("Input your street adress: ");
                string customerStreetAddress = Console.ReadLine();
                Console.Write("Input your postal code: ");
                int.TryParse(Console.ReadLine(), out int customerPostalCode);
                Console.Write("Input card number: ");
                int.TryParse(Console.ReadLine(), out int customerCardNumber);
                Console.Write("Input your mail address: ");
                string customerMailAddress = Console.ReadLine();

                // Listan products dyker inte upp i SSMS FIXA!
                myDb.Add(new Models.ShoppingCart
                {
                    Products = new List<Product>(),
                    TotalCost = null,
                    Quantity = null
                });

                myDb.SaveChanges();
                int x = 0;

                var shoppingCartId = (from c in myDb.ShoppingCarts
                                      select c.Id).Max();

                myDb.Add(new Models.Customer
                {
                    FirstName = customerFirstName,
                    LastName = customerLastName,
                    Age = customerAge,
                    CustomerUserName = customerUserName,
                    CustomerPassword = customerPassword,
                    Country = customerCountry,
                    StreetAddress = customerStreetAddress,
                    PostalCode = customerPostalCode,
                    CardNumber = customerCardNumber,
                    EMailAdress = customerMailAddress,
                    ShoppingCartId = shoppingCartId

                });
                Console.WriteLine("You have successfully created your account!");
                Console.ReadKey(true);
                myDb.SaveChanges();

            }
            Console.ReadKey(true);
            Console.Clear();
        }
        public static void SearchProducts()
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
                            PurchaseProduct();
                            break;
                        case 'b':
                            Console.WriteLine("Back");
                            CustomerHomePage();
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

        public static void BrowseProducts()
        {
            Console.Clear();
            Database.DisplayAllCategories();

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
                            PurchaseProduct();
                            break;
                        case 'b':
                            Console.WriteLine("Back");
                            CustomerHomePage();
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

        public static void PurchaseProduct()
        {
            Console.Write("Input product Id: ");
            int productId = int.Parse(Console.ReadLine());
            using (var myDb = new MyDbContext())
            {
                var chosenProduct = (from c in myDb.Products
                                     where c.Id == productId
                                     select c).SingleOrDefault();

                if (chosenProduct != null)
                {
                    Console.WriteLine("Id: " + chosenProduct.Id + " " + " Name: " + chosenProduct.Name + " Price: " + chosenProduct.Price + " Units In Stock: " + chosenProduct.StockBalance +
                         " Product Info: " + chosenProduct.ProductInfo);
                    Console.WriteLine("Buy this product? y/n");
                    var answer = Console.ReadKey();

                    //Lägg till funktionerlig köpfunktion efter att shoppingcart fungerar
                    switch (answer.KeyChar)
                    {
                        case 'y':
                            break;
                        case 'n':
                            CustomerHomePage();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("The inputted product Id doesn't match with any of our products, please try again.");
                    Console.ReadKey(true);
                }
            }
        }

        public static void ProductMenu()
        {
            Console.Clear();

            while (true)
            {
                List<string> productText = new List<string> { "[A] Add Product", "[E] Edit Product", "[R] Remove Product", "[B] Back" };
                var productWindow = new Window("Product Menu", 1, 1, productText);
                Database.DisplayAllCategories();
                Database.DisplayAllProducts();
                Database.DisplayAllSuppliers();

                productWindow.DrawWindow();

                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case 'a':
                        Console.WriteLine("Add Product");
                        AddProduct();
                        break;
                    case 'c':
                        Console.WriteLine("Edit Product");
                        EditProduct();
                        break;
                    case 'r':
                        Console.WriteLine("Remove Product");
                        RemoveProduct();
                        break;
                    case 'b':
                        Console.WriteLine("Back");
                        AdminHomePage();
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
                while (true)
                {
                    Console.Write("Type Category ID: ");

                    int.TryParse(Console.ReadLine(), out int category);

                    var category3 = (from c in myDb.Categories
                                     where c.Id == category
                                     select c);

                    if (category3 != null && category3.Any())
                    {
                        Console.Write("Type Product Name: ");
                        string productName = Console.ReadLine();

                        Console.Write("Type product Price: ");
                        float.TryParse(Console.ReadLine(), out float productPrice);

                        if (productPrice != null && productPrice > 0)
                        {
                            Console.Write("Type productsupplier ID: ");
                            int.TryParse(Console.ReadLine(), out int productSupplierId);

                            var productSupplierId2 = (from c in myDb.ProductSuppliers
                                                      where c.Id == productSupplierId
                                                      select c);

                            if (productSupplierId2 != null && productSupplierId2.Any())
                            {
                                Console.Write("Type productinfo: ");
                                string productInfo = Console.ReadLine();

                                Console.Write("Type stockbalance: ");
                                int.TryParse(Console.ReadLine(), out int stockBalance);

                                if (stockBalance != null && stockBalance > 0)
                                {
                                    Console.WriteLine("Show product on Homepage ? Type Y/N");
                                    bool selectedProduct = true;

                                    while (true)
                                    {
                                        var key = Console.ReadKey();

                                        switch (key.KeyChar)
                                        {
                                            case 'y':
                                                selectedProduct = true;
                                                break;
                                            case 'n':
                                                selectedProduct = false;
                                                break;
                                            default:
                                                Console.WriteLine("Error, wrong input.");
                                                break;
                                        }
                                        myDb.Add(new Models.Product
                                        {
                                            Name = productName,
                                            Categories = category3.ToList(),
                                            Price = productPrice,
                                            ProductSupplierId = productSupplierId,
                                            ProductInfo = productInfo,
                                            StockBalance = stockBalance,
                                            SelectedProduct = selectedProduct
                                        });
                                        myDb.SaveChanges();
                                        Console.WriteLine("You have added " + productName + " to the list");
                                        ProductMenu();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Error, invalid input");
                                    Console.ReadLine();
                                    ProductMenu();
                                }
                                //Console.Write("Show product on Homepage? Type 'true' or 'false': ");
                                //bool selectedProduct = bool.Parse(Console.ReadLine());
                            }
                            else
                            {
                                Console.WriteLine("Error, invalid supplier Id");
                                Console.ReadLine();
                                ProductMenu();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error, invalid price!");
                            Console.ReadLine();
                            ProductMenu();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error category Id does not exist!");
                        Console.ReadLine();
                        ProductMenu();
                    }
                }
            }
        }

        public static void EditProduct()
        {
            using (var myDb = new MyDbContext())
            {
                List<string> changeProductText = new List<string> { "[1] Edit product name", "[2] Edit product price", "[3] Edit product supplier Id",
                        "[4] Edit product info", "[5] Edit product stock balance", "[B] Back" };
                var changeProductWindow = new Window("Change Product Menu", 0, 3, changeProductText);
                changeProductWindow.DrawWindow();

                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case '1':                       
                        EditProductName();
                        break;
                    case '2':
                        EditProductPrice();
                        break;
                    case '3':
                        EditProductSupplier();
                        break;
                    case '4':
                        EditProductInfo();
                        break;
                    case '5':
                        EditStockBalance();
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

        public static void ProductOverview()
        {
            using (var myDb = new MyDbContext())
            {
                List<string> productList = new List<string>();

                foreach (var products in myDb.Products)
                {
                    productList.Add("Id: " + products.Id + " " + " Name: " + products.Name + " Price: " + products.Price + " Units In Stock: " + products.StockBalance +
                        " Product Supplier Id: " + products.ProductSupplierId + " Selected Product: " + (products.SelectedProduct == true ? " Yes " : " No ") +
                        "Product Info: " + products.ProductInfo);
                }

                if (productList.Count == 0)
                {
                    productList.Add("Empty");
                }
                var productsWindow = new Window("Product Overview", 1, 20, productList);
                productsWindow.DrawWindow();

                Console.WriteLine("Press any key to return!");
                Console.ReadKey();
                AdminHomePage();
            }
        }
        public static void EditProductName()
        {
            using (var myDb = new MyDbContext())
            {

                Console.Write("Input product Id: ");
                int.TryParse(Console.ReadLine(), out int productId);
                var productId2 = (from c in myDb.Products
                                  where c.Id == productId
                                  select c).SingleOrDefault();
                if (productId2 != null)
                {
                    Console.Write("Input new product Name: ");
                    string productName2 = Console.ReadLine();


                    if (productId2 != null)
                    {
                        productId2.Name = productName2;
                        Console.WriteLine("You have successfully changed the product Name to " + productName2);
                        Console.ReadKey();
                        myDb.SaveChanges();
                    }
                    //else
                    //{
                    //    Console.WriteLine("Error wrong ID");
                    //    Console.ReadKey();
                    //}
                }
                else
                {
                    Console.WriteLine("Error, wrong Id input.");
                    Console.ReadKey();

                }
                Console.Clear();
            }
        }
        public static void EditProductPrice()
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
        //public static void ChangeCategoryId()
        //{
        //    using (var myDb = new MyDbContext())
        //    {

        //        Console.Write("Input product Id: ");
        //        int productId = int.Parse(Console.ReadLine());
        //        Console.Write("Input new category Id: ");
        //        int categoryId2 = int.Parse(Console.ReadLine());
        //        var newCategoryId = (from c in myDb.Products
        //                        where c.Id == productId
        //                        select c).SingleOrDefault();

        //        if (newCategoryId != null)
        //        {
        //            newCategoryId.CategoryId = categoryId2;
        //            Console.WriteLine("You have successfully changed the category Id to: " + categoryId2);
        //            Console.ReadKey();
        //            myDb.SaveChanges();
        //        }
        //        else
        //        {
        //            Console.WriteLine("Error wrong ID");
        //            Console.ReadKey();
        //        }
        //    }
        //    Console.Clear();
        //}
        public static void EditProductSupplier()
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

        public static void EditProductInfo()
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
        public static void EditStockBalance()
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
                int.TryParse(Console.ReadLine(), out int productId);

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
                    Console.ReadKey(true);
                }
            }
            Console.Clear();
        }

        public static void CategoryMenu()
        {
            Console.Clear();

            while (true)
            {

                List<string> categoryText = new List<string> { "[A] Add Category", "[E] Edit Category", "[R] Remove Category", "[B] Back" };
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
                        AddCategory();
                        break;
                    case 'c':
                        EditCategory();
                        break;
                    case 'r':
                        RemoveCategory();
                        break;
                    case 'b':
                        Console.WriteLine("Back");
                        AdminHomePage();
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
            Console.ReadKey(true);
            Console.Clear();
        }
        public static void EditCategory()
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
