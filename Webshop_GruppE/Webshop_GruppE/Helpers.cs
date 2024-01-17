using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Webshop_GruppE.Methods;
using Webshop_GruppE.Models;

namespace Webshop_GruppE
{
    internal class Helpers
    {
        public static void AdminLogInMenu()
        {
            while (true)
            {
                using (var myDb = new MyDbContext())
                {
                    var emergencyLogIn = (from c in myDb.Admins
                                          where c.Id == 1
                                          select c.Id).SingleOrDefault();

                    Console.Clear();
                    LogoWindow.LogoWindowMeth(1, 1, 24, 7);
                    List<string> profileText = new List<string> { "[1] Express Log in ", "[2] Log in ", "[3] Sign up", "[B] Back" };
                    var userWindow = new Window("Sign in as admin", 1, 10, profileText);
                    userWindow.DrawWindow();
                    var key = Console.ReadKey(true);
                    switch (key.KeyChar)
                    {
                        case '1':
                            if (emergencyLogIn == 1)
                            {
                                AdminHomePage(1);
                            }
                            else
                            {
                                Console.WriteLine("No account has been made, try adding a test account from the main menu or create a new one.");
                                Console.ReadKey(true);
                            }
                            break;
                        case '2':
                            Console.WriteLine("Log in");
                            AdminLogIn();
                            break;
                        case '3':
                            Console.WriteLine("Sign up");
                            SignUpAdmin();
                            break;
                        case 'b':
                            Console.WriteLine("Back");
                            Program.StartScreen();
                            break;
                        default:
                            Console.WriteLine("Wrong input");
                            Console.ReadKey();
                            break;
                    }
                }

            }
        }
        public static void AdminLogIn()
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
                        AdminLogInMenu();
                    }
                    else if (findUserName.Contains(userName) && findUserPassword.Contains(password))
                    {
                        var adminId = (from c in myDb.Admins
                                       where c.AdminPassword == password
                                       select c.Id).SingleOrDefault();

                        AdminHomePage(adminId);
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
        public static void AdminHomePage(int adminId)
        {
            Console.Clear();

            while (true)
            {
                using (var myDb = new MyDbContext())
                {
                    var adminUserName = (from c in myDb.Admins
                                         where c.Id == adminId
                                         select c.AdminName).SingleOrDefault();
                    LogoWindow.LogoWindowMeth(1, 1, 24, 7);
                    List<string> adminText = new List<string> { "[1] Edit Product", "[2] Edit Category", "[3] Product Overview", "[4] Edit Suppliers", "[P] Profile Page", "[C] Customer Page", "[Q] Queries", "[L] Logout" };
                    var adminWindow = new Window("Welcome " + adminUserName, 1, 10, adminText);
                    adminWindow.DrawWindow();
                }
                var key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case '1':
                        Console.WriteLine("Add Product");
                        ProductMenu(adminId);
                        break;
                    case '2':
                        Console.WriteLine("Add Category");
                        CategoryMenu(adminId);
                        break;
                    case '3':
                        Console.WriteLine("Product Overview");
                        ProductOverview(adminId);
                        break;
                    case '4':
                        Console.WriteLine("Edit Suppliers");
                        SupplierMenu(adminId);
                        break;
                    case 'p':
                        Console.WriteLine("Profile Page");
                        Database.DisplayAdminDetails(adminId);
                        break;
                    case 'c':
                        Console.WriteLine("Customer Page");
                        ListCustomers(adminId);
                        break;
                    case 'q':
                        Console.WriteLine("Queries");
                        break;
                    case 'l':
                        Console.WriteLine("Logout");
                        Program.StartScreen();
                        break;
                    default:
                        Console.WriteLine("Wrong Input");
                        Console.ReadKey(true);
                        break;
                }
            }
        }
        public static void SupplierMenu(int adminId)
        {
            Console.Clear();

            while (true)
            {
                LogoWindow.LogoWindowMeth(1, 1, 24, 7);
                Database.DisplayAllSuppliers();
                List<string> supplierText = new List<string> { "[1] Add supplier", "[2] Edit supplier", "[3] Remove supplier", "[B] Back" };
                var userWindow = new Window("Supplier Menu", 1, 10, supplierText);
                userWindow.DrawWindow();

                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case '1':
                        AddSupplier(adminId);
                        break;
                    case '2':
                        EditSupplier(adminId);
                        break;
                    case '3':
                        RemoveSupplier(adminId);
                        break;
                    case 'b':
                        AdminHomePage(adminId);
                        break;
                }
            }
        }
        public static void AddSupplier(int adminId)
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

        public static void EditSupplier(int adminId)
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

        public static void RemoveSupplier(int adminId)
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
        public static void CustomerLogInMenu()
        {
            while (true)
            {
                using (var myDb = new MyDbContext())
                {
                    var emergencyLogIn = (from c in myDb.Customers
                                          where c.Id == 1
                                          select c.Id).SingleOrDefault();
                    Console.Clear();
                    LogoWindow.LogoWindowMeth(1, 1, 24, 7);
                    List<string> profileText = new List<string> { "[1] Express Log in ", "[2] Log in ", "[3] Sign up", "[B] Back" };
                    var userWindow = new Window("Sign in", 0, 10, profileText);
                    userWindow.DrawWindow();
                    var key = Console.ReadKey(true);
                    switch (key.KeyChar)
                    {
                        case '1':
                            if (emergencyLogIn == 1)
                            {
                                List<int> boughtProducts = new List<int>();
                                CustomerHomePage(1);
                            }
                            else
                            {
                                Console.WriteLine("No account has been made, try adding a test account from the main menu or create a new one.");
                                Console.ReadKey(true);
                            }
                            break;
                        case '2':
                            Console.WriteLine("Log in");
                            CustomerLogIn();
                            break;
                        case '3':
                            Console.WriteLine("Sign up");
                            CustomerSignUp();
                            break;
                        case 'b':
                            Console.WriteLine("Back");
                            Program.StartScreen();
                            break;
                        default:
                            Console.WriteLine("Wrong input");
                            Console.ReadKey();
                            break;
                    }
                }

            }
        }

        public static void CustomerHomePage(int customerId)
        {
            Console.Clear();

            while (true)
            {
                LogoWindow.LogoWindowMeth(1, 1, 24, 7);
                Database.DisplayChosenProducts();
                using (var myDb = new MyDbContext())
                {
                    var customerUserName = (from c in myDb.Customers
                                            where c.Id == customerId
                                            select c.CustomerUserName).SingleOrDefault();

                    List<string> userText = new List<string> { "[1] Search Products", "[2] Browse Products", "[S] Shopping Cart", "[P] Profile Page", "[B] Buy Products", "[O] Order History", "[L] Logout" };
                    var userWindow = new Window("Welcome " + customerUserName, 1, 10, userText);
                    userWindow.DrawWindow();
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
                        Database.DisplayCustomerDetails(customerId);
                        break;
                    case 'b':
                        PurchaseProduct(customerId);
                        break;
                    case 'o':
                        Console.WriteLine(""); // orderhistory
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

        public static void CustomerLogIn()
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

                    if (findUserName == null || findUserPassword == null)
                    {
                        Console.WriteLine("Error, username or password doesn´t exist!");
                        Console.ReadKey(true);
                    }
                    else if (findUserName.Contains(userName) && findUserPassword.Contains(password))
                    {
                        var customerId = (from c in myDb.Customers
                                          where c.CustomerUserName == userName
                                          select c.Id).SingleOrDefault();

                        CustomerHomePage(customerId);
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
                });

                Console.WriteLine("You have successfully created your account!");
                Console.ReadKey(true);
                myDb.SaveChanges();

            }
            Console.ReadKey(true);
            Console.Clear();
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
            Console.Clear();
            LogoWindow.LogoWindowMeth(1, 1, 24, 7);
            List<string> categoryText = Database.DisplayAllCategories();
            var categoryWindow = new Window("Category List", 30, 1, categoryText);
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
                             " Product Info: " + chosenProduct.ProductInfoText);
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


                                Console.WriteLine("You have added " + chosenProduct.Name + " to your shopping cart.");
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

        public static void ProductMenu(int adminId)
        {
            Console.Clear();

            while (true)
            {
                LogoWindow.LogoWindowMeth(1, 1, 24, 7);
                List<string> productText = new List<string> { "[A] Add Product", "[E] Edit Product", "[R] Remove Product", "[B] Back" };
                var productWindow = new Window("Product Menu", 1, 10, productText);
                productWindow.DrawWindow();

                List<string> categoryText = Database.DisplayAllCategories();
                var categoryWindow = new Window("Categories", 1, 17, categoryText);
                categoryWindow.DrawWindow();

                List<string> supplierText = Database.DisplayAllSuppliers();
                var supplierWindow = new Window("Suppliers", 27, 1, supplierText);
                supplierWindow.DrawWindow();

                List<string> productsText = Database.DisplayAllProducts();
                var productsWindow = new Window("Products", 70, 1, productsText);
                productsWindow.DrawWindow();

                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case 'a':
                        Console.WriteLine("Add Product");
                        AddProduct(adminId);
                        break;
                    case 'e':
                        Console.WriteLine("Edit Product");
                        EditProduct(adminId);
                        break;
                    case 'r':
                        Console.WriteLine("Remove Product");
                        RemoveProduct(adminId);
                        break;
                    case 'b':
                        Console.WriteLine("Back");
                        AdminHomePage(adminId);
                        break;
                    default:
                        Console.WriteLine("Wrong Input");
                        break;
                }
            }
        }
        public static void AddProduct(int adminId)
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
                                Console.Write("Type product brand: ");
                                string productBrand = Console.ReadLine();

                                Console.Write("Type productinfo: ");
                                string productInfo = Console.ReadLine();

                                bool chosenSize = false;
                                int productSize = 0;
                                string sizeText = "";
                                while (chosenSize == false)
                                {
                                    Console.WriteLine("Size Guide\n1.Small\t2.Medium\t3.Large");
                                    Console.Write("Type size: ");
                                    productSize = int.Parse(Console.ReadLine());

                                    switch (productSize)
                                    {
                                        case 1:
                                            sizeText = "Small";
                                            chosenSize = true;
                                            break;
                                        case 2:
                                            sizeText = "Medium";
                                            chosenSize = true;
                                            break;
                                        case 3:
                                            sizeText = "Large";
                                            chosenSize = true;
                                            break;
                                        default:
                                            Console.WriteLine("Error, size does not exist! Type in 1 to 3!");
                                            break;
                                    }

                                }

                                Console.Write("Type stockbalance: ");
                                int.TryParse(Console.ReadLine(), out int stockBalance);

                                myDb.Add(new Models.Product
                                {
                                    Name = productName,
                                    Categories = category3.ToList(),
                                    Price = productPrice,
                                    ProductSupplierId = productSupplierId,
                                    ProductInfoText = productInfo,
                                    StockBalance = stockBalance,
                                    ProductBrand = productBrand,
                                    Size = sizeText


                                }); ;
                                myDb.SaveChanges();

                                var newProductId = (from c in myDb.Products
                                                    select c.Id).Max();

                                var selectNewProductId = (from c in myDb.Products
                                                          where c.Id == newProductId
                                                          select c).SingleOrDefault();

                                if (stockBalance != null && stockBalance > 0)
                                {
                                    Console.WriteLine("Show product in selected deals frontpage? Type Y/N");
                                    bool selectedProduct = true;

                                    while (true)
                                    {
                                        var key = Console.ReadKey(true);

                                        switch (key.KeyChar)
                                        {
                                            case 'y':
                                                selectedProduct = true;
                                                myDb.Add(new Models.SelectTopDealItem { Product = selectNewProductId });
                                                myDb.SaveChanges();
                                                Console.WriteLine("Items successfully added to deals!");
                                                Console.ReadKey(true);
                                                break;
                                            case 'n':
                                                selectedProduct = false;
                                                break;
                                            default:
                                                Console.WriteLine("Error, wrong input.");
                                                break;
                                        }



                                        Console.WriteLine("You have added " + productName + " to the list");
                                        ProductMenu(adminId);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Error, invalid input");
                                    Console.ReadLine();
                                    ProductMenu(adminId);
                                }
                                //Console.Write("Show product on Homepage? Type 'true' or 'false': ");
                                //bool selectedProduct = bool.Parse(Console.ReadLine());
                            }
                            else
                            {
                                Console.WriteLine("Error, invalid supplier Id");
                                Console.ReadLine();
                                ProductMenu(adminId);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error, invalid price!");
                            Console.ReadLine();
                            ProductMenu(adminId);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error category Id does not exist!");
                        Console.ReadLine();
                        ProductMenu(adminId);
                    }
                }
            }
        }

        public static void EditProduct(int adminId)
        {
            using (var myDb = new MyDbContext())
            {
                LogoWindow.LogoWindowMeth(1, 1, 24, 7);
                List<string> changeProductText = new List<string> { "[1] Edit product name", "[2] Edit product price", "[3] Edit product supplier Id",
                        "[4] Edit product info", "[5] Edit product stock balance", "[6] Edit if on deal", "[B] Back" };
                var changeProductWindow = new Window("Change Product Menu", 1, 13, changeProductText);
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
                    case '6':
                        EditIfDeal();
                        break;
                    case 'b':
                        ProductMenu(adminId);
                        break;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }
            }
        }

        public static void ProductOverview(int adminId)
        {
            using (var myDb = new MyDbContext())
            {
                List<string> productList = new List<string>();

                foreach (var products in myDb.Products)
                {
                    productList.Add("Id: " + products.Id + " " + " Name: " + products.Name + " Price: " + products.Price + "$" + " Units In Stock: " + products.StockBalance +
                        " Product Supplier Id: " + products.ProductSupplierId + " Product Info: " + products.ProductInfoText);
                }

                if (productList.Count == 0)
                {
                    productList.Add("Empty");
                }
                var productsWindow = new Window("Product Overview", 1, 30, productList);
                productsWindow.DrawWindow();

                Console.WriteLine("Press any key to return!");
                Console.ReadKey();
                AdminHomePage(adminId);
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
                    newProductInfo.ProductInfoText = productInfo2;
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
        public static void EditIfDeal()
        {
            using (var myDb = new MyDbContext())
            {

                Console.Write("Input product Id: ");
                int.TryParse(Console.ReadLine(), out int productId);

                var selectedProduct = (from c in myDb.Products
                                       where c.Id == productId
                                       select c).FirstOrDefault();
                if (selectedProduct != null)
                {
                    var productList = (from c in myDb.SelectTopDealItems
                                       select c).ToList();
                    myDb.Add(new Models.SelectTopDealItem { Product = selectedProduct });

                    if (productList.Count <= 5)
                    {
                        myDb.SaveChanges();
                        Console.WriteLine("Items successfully added to deals!");
                        Console.ReadKey(true);
                    }
                    else
                    {
                        Console.WriteLine("Too many items in product deals, please remove at least one");
                    }

                }
                else
                {
                    Console.WriteLine("Invalid input, please try again.");
                }
            }

        }
        public static void RemoveProduct(int adminId)
        {
            using (var myDb = new MyDbContext())
            {

                Console.Write("Input product Id: ");
                int.TryParse(Console.ReadLine(), out int productId);

                var removeProduct = (from c in myDb.Products
                                     where c.Id == productId
                                     select c).FirstOrDefault();

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

        public static void CategoryMenu(int adminId)
        {
            Console.Clear();

            while (true)
            {
                LogoWindow.LogoWindowMeth(1, 1, 24, 7);
                List<string> categoryText = new List<string> { "[A] Add Category", "[E] Edit Category", "[R] Remove Category", "[B] Back" };
                var categoryWindow = new Window("Category Menu", 1, 10, categoryText);
                categoryWindow.DrawWindow();
                List<string> categoryText2 = Database.DisplayAllCategories();
                var categoryWindow2 = new Window("Category List", 30, 1, categoryText2);
                categoryWindow2.DrawWindow();
                using (var myDb = new MyDbContext())
                {

                }

                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case 'a':
                        AddCategory(adminId);
                        break;
                    case 'c':
                        EditCategory(adminId);
                        break;
                    case 'r':
                        RemoveCategory(adminId);
                        break;
                    case 'b':
                        Console.WriteLine("Back");
                        AdminHomePage(adminId);
                        break;
                    default:
                        Console.WriteLine("Wrong Input");
                        break;
                }

            }
        }

        public static void AddCategory(int adminId)
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
        public static void EditCategory(int adminId)
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
        // fixa detta
        public static void RemoveCategory(int adminId)
        {
            using (var myDb = new MyDbContext())
            {
                Console.Write("Input category Id: ");
                int categoryId = int.Parse(Console.ReadLine());
                var removeCategory = (from c in myDb.Categories
                                      where c.Id == categoryId
                                      select c).SingleOrDefault();

                if (removeCategory != null )   
                {
                    var findProductInCategory = myDb.Products.Include(p => p.Categories)
                                               .Where(p => p.Categories.Any(c => c.Id == categoryId)).ToList();

                    if (findProductInCategory.Count() == 0)
                    {
                        myDb.Remove(removeCategory);
                        Console.WriteLine("You have successfully removed category Id " + categoryId);
                        Console.ReadKey(true);
                        myDb.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("Error: There are still products in the category. Remove or move the products before deleting the category.");
                        Console.ReadKey(true);
                    }
                }
                else
                {
                    Console.WriteLine("Error wrong ID or category not found.");
                    Console.ReadKey(true);
                }
            }
            Console.Clear();
        }

        public static List<Customer> GetAllCustomers()
        {
            var connstring = "Server=.\\SQLExpress;Database=FashionCode;Trusted_Connection=True;TrustServerCertificate=True;";
            string sql = "SELECT * FROM Customers";
            List<Customer> customers = new List<Customer>();
            using (var myDb = new SqlConnection(connstring))
            {
                customers = myDb.Query<Customer>(sql).ToList();
            }
            return customers;
        }

        public static void ListCustomers(int adminId)
        {
            Console.Clear();

            Console.WriteLine("All customer details!\n");
            List<Customer> customers = GetAllCustomers();
            Console.WriteLine("{0,-5}{1,-15}{2,-15}{3,-5}{4,-15}{5,-15}{6,-10}{7,-20}{8,-15}{9,-20}{10,-20}{11,-15}",
                      "ID", "First Name", "Last Name", "Age", "Username", "Password", "Country",
                      "Street Address", "Postal Code", "Card Number", "E-Mail");

            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------");

            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.Id,-5}{customer.FirstName,-15}{customer.LastName,-15}" +
                                  $"{customer.Age,-5}{customer.CustomerUserName,-15}{customer.CustomerPassword,-15}" +
                                  $"{customer.Country,-10}{customer.StreetAddress,-20}{customer.PostalCode,-15}" +
                                  $"{customer.CardNumber,-20}{customer.EMailAdress,-20}");
            }

            Console.WriteLine("\n[1] Edit first name\n[2] Edit last name\n[3] Edit Age\n[4] Edit username\n" +
                "[5] Edit password\n[6] Edit country\n[7] Edit Address\n[8] Edit postal code\n" +
                "[9] Edit card number\n[E] Edit E-Mail\n[B] Back");
            var key = Console.ReadKey(true);
            switch (key.KeyChar)
            {
                case '1':
                    EditCustomerHelper.EditFirstName(adminId);
                    break;
                case '2':
                    EditCustomerHelper.EditLastName(adminId);
                    break;
                case '3':
                    EditCustomerHelper.EditCustomerAge(adminId);
                    break;
                case '4':
                    EditCustomerHelper.EditCustomerUserName(adminId);
                    break;
                case '5':
                    EditCustomerHelper.EditCustomerPassword(adminId);
                    break;
                case '6':
                    EditCustomerHelper.EditCustomerCountry(adminId);
                    break;
                case '7':
                    EditCustomerHelper.EditCustomerAddress(adminId);
                    break;
                case '8':
                    EditCustomerHelper.EditCustomerPostalCode(adminId);
                    break;
                case '9':
                    EditCustomerHelper.EditCustomerCardNumber(adminId);
                    break;
                case 'e':
                    EditCustomerHelper.EditCustomerEmail(adminId);
                    break;
                case 'b':
                    AdminHomePage(adminId);
                    break;
                default:
                    Console.WriteLine("Wrong Input");
                    break;
            }
            Console.ReadKey(true);
        }

        public static void CreateTestData()
        {

            using (var myDb = new MyDbContext())
            {
                var testCustomer = (from c in myDb.Customers
                                    where c.CustomerUserName == "TestCustomer"
                                    select c).SingleOrDefault();

                if (testCustomer == null)
                {

                    myDb.Add(new Models.Customer()
                    {
                        FirstName = "Test",
                        LastName = "Customer",
                        Age = 20,
                        CustomerUserName = "TestCustomer",
                        CustomerPassword = "test1",
                        Country = "TestCountry",
                        StreetAddress = "TestAddress",
                        PostalCode = 11111,
                        CardNumber = 11111111,
                        EMailAdress = "test@mail.com",

                    });

                    myDb.SaveChanges();
                    Console.WriteLine("Customer Test account was successfully created!");
                    Console.ReadKey(true);
                }
                else
                {
                    Console.WriteLine("A customer test account already exists!");
                    Console.ReadKey(true);
                }

                var testAdmin = (from c in myDb.Admins
                                 where c.AdminName == "TestAdmin"
                                 select c).SingleOrDefault();

                if (testAdmin == null)
                {
                    myDb.Add(new Models.Admin() { FirstName = "Test", LastName = "Admin", AdminName = "TestAdmin", AdminPassword = "test1", EMailAdress = "admin@mail.com" });
                    myDb.SaveChanges();
                    Console.WriteLine("Admin Test account was successfully created!");
                    Console.ReadKey(true);
                }
                else
                {
                    Console.WriteLine("An admin test account already exists!");
                    Console.ReadKey(true);
                }

                var findCategories = (from c in myDb.Categories
                                      where c.CategoryName == "Jackets" || c.CategoryName == "Trousers" || c.CategoryName == "Tops"
                                      || c.CategoryName == "Men's Clothing" || c.CategoryName == "Women's Clothing"
                                      select c).ToList();

                if (findCategories.Count == 0)
                {
                    myDb.AddRange(new Models.Category() { CategoryName = "Jackets" }, new Models.Category() { CategoryName = "Trousers" }, new Models.Category() { CategoryName = "Tops" },
                    new Models.Category() { CategoryName = "Men's Clothing" }, new Models.Category() { CategoryName = "Women's Clothing" });
                    myDb.SaveChanges();
                    Console.WriteLine("Category test data was successfully created!");
                    Console.ReadKey(true);
                }
                else
                {
                    Console.WriteLine("Category test data already exists!");

                    Console.ReadKey(true);
                }

                var findProductSuppliers = (from c in myDb.ProductSuppliers
                                            where c.Name == "India Export" || c.Name == "MadeinChina" || c.Name == "Children Exploits"
                                            select c).ToList();

                if (findProductSuppliers.Count == 0)
                {
                    myDb.AddRange(new Models.ProductSupplier() { Name = "India Export", Country = "India" }, new Models.ProductSupplier() { Name = "MadeinChina", Country = "China" },
                                           new Models.ProductSupplier() { Name = "Children Exploits", Country = "Thailand" });
                    myDb.SaveChanges();
                    Console.WriteLine("Supplier test data was successfully created!");
                    Console.ReadKey(true);
                }
                else
                {
                    Console.WriteLine("Supplier test data already exists!");

                    Console.ReadKey(true);
                }

                var categoryList = (from c in myDb.Categories
                                    where c.CategoryName == "Jackets" || c.CategoryName == "Trousers" || c.CategoryName == "Tops" ||
                                    c.CategoryName == "Men's Clothing" || c.CategoryName == "Women's Clothing"
                                    select c).ToList();

                var supplierList = (from c in myDb.ProductSuppliers
                                    where c.Name == "India Export" || c.Name == "MadeinChina" || c.Name == "Children Exploits"
                                    select c.Id).ToList();
                var productList = (from c in myDb.Products
                                   where c.Name == "Summer Jacket" && c.ProductSupplierId == 1
                                   select c).ToList();

                if (categoryList != null && supplierList != null)
                {
                    if (productList.Count < 1)
                    {
                        myDb.AddRange(
                        new Models.Product()
                        {
                            Name = "Summer Jacket",
                            Categories = new List<Models.Category>() { categoryList[0], categoryList[4] },
                            ProductSupplierId = supplierList[0],
                            Price = (float)(59.99),
                            StockBalance = 12,
                            ProductInfoText = "A nice jacket for the summer",
                            ProductBrand = "Summertime Jam",
                            Size = "Small"
                        },
                        new Models.Product()
                        {
                            Name = "Winter Jacket",
                            Categories = new List<Models.Category>() { categoryList[0], categoryList[3] },
                            ProductSupplierId = supplierList[2],
                            Price = (float)(25.49),
                            StockBalance = 22,
                            ProductInfoText = "A warm jacket for winter",
                            ProductBrand = "Wintertime Jam",
                            Size = "Medium"
                        },
                        new Models.Product()
                        {
                            Name = "Leather Jacket",
                            Categories = new List<Models.Category>() { categoryList[0] },
                            ProductSupplierId = supplierList[1],
                            Price = (float)(39.29),
                            StockBalance = 5,
                            ProductInfoText = "A cool jacket made of leather",
                            ProductBrand = "Coolio",
                            Size = "Medium"

                        },
                        new Models.Product()
                        {
                            Name = "Denim Jacket",
                            Categories = new List<Models.Category>() { categoryList[0], },
                            ProductSupplierId = supplierList[0],
                            Price = (float)(19.99),
                            StockBalance = 17,
                            ProductInfoText = "A jacket made of Denim",
                            ProductBrand = "Reita",
                            Size = "Large"
                        },
                        new Models.Product()
                        {
                            Name = "Rain Coat",
                            Categories = new List<Models.Category>() { categoryList[0] },
                            ProductSupplierId = supplierList[2],
                            Price = (float)(18.99),
                            StockBalance = 36,
                            ProductInfoText = "A jacket that keeps you dry",
                            ProductBrand = "Dry Timez",
                            Size = "Medium"
                        },
                        new Models.Product()
                        {
                            Name = "Denim Shorts",
                            Categories = new List<Models.Category>() { categoryList[1], categoryList[4] },
                            ProductSupplierId = supplierList[0],
                            Price = (float)(29.99),
                            StockBalance = 7,
                            ProductInfoText = "Short denim shorts",
                            ProductBrand = "Shortys",
                            Size = "Small"
                        },
                        new Models.Product()
                        {
                            Name = "Sweatpants",
                            Categories = new List<Models.Category>() { categoryList[1], categoryList[3] },
                            ProductSupplierId = supplierList[1],
                            Price = (float)(59.99),
                            StockBalance = 5,
                            ProductInfoText = "Pants made for sweatin'",
                            ProductBrand = "Gymrat",
                            Size = "Medium"
                        },
                        new Models.Product()
                        {
                            Name = "Cargo Pants",
                            Categories = new List<Models.Category>() { categoryList[1], categoryList[3] },
                            ProductSupplierId = supplierList[2],
                            Price = (float)(49.99),
                            StockBalance = 27,
                            ProductInfoText = "Pants, nothing more nothing less",
                            ProductBrand = "Radz",
                            Size = "Large"
                        },
                        new Models.Product()
                        {
                            Name = "Leggings",
                            Categories = new List<Models.Category>() { categoryList[1], categoryList[4] },
                            ProductSupplierId = supplierList[2],
                            Price = (float)(39.99),
                            StockBalance = 17,
                            ProductInfoText = "Meant to cover up your legs",
                            ProductBrand = "Fo'Shoz",
                            Size = "Large"
                        },
                        new Models.Product()
                        {
                            Name = "T-Shirt",
                            Categories = new List<Models.Category>() { categoryList[2] },
                            ProductSupplierId = supplierList[2],
                            Price = (float)(9.99),
                            StockBalance = 41,
                            ProductInfoText = "As basic as it comes",
                            ProductBrand = "Basic",
                            Size = "Small"
                        },
                        new Models.Product()
                        {
                            Name = "Off-Brand Shirt",
                            Categories = new List<Models.Category>() { categoryList[2], categoryList[3] },
                            ProductSupplierId = supplierList[1],
                            Price = (float)(19.99),
                            StockBalance = 11,
                            ProductInfoText = "If you can't make a successfull brand, copy another one!",
                            ProductBrand = "Wannabe",
                            Size = "Medium"
                        },
                        new Models.Product()
                        {
                            Name = "Brand Shirt",
                            Categories = new List<Models.Category>() { categoryList[2], categoryList[3] },
                            ProductSupplierId = supplierList[1],
                            Price = (float)(499.99),
                            StockBalance = 41,
                            ProductInfoText = "You pay for the brand, not the clothes",
                            ProductBrand = "Brand'biz",
                            Size = "Small"
                        },
                        new Models.Product()
                        {
                            Name = "Women's Blouse",
                            Categories = new List<Models.Category>() { categoryList[2], categoryList[4] },
                            ProductSupplierId = supplierList[0],
                            Price = (float)(29.99),
                            StockBalance = 14,
                            ProductInfoText = "A pretty blouse for every occation",
                            ProductBrand = "Sozies",
                            Size = "Medium"
                        },
                        new Models.Product()
                        {
                            Name = "Office Shirt",
                            Categories = new List<Models.Category>() { categoryList[2], categoryList[3] },
                            ProductSupplierId = supplierList[1],
                            Price = (float)(29.99),
                            StockBalance = 14,
                            ProductInfoText = "To be in an office, you got to look the part",
                            ProductBrand = "Workaholic",
                            Size = "Large"
                        }
                        );

                        myDb.SaveChanges();
                        Console.WriteLine("Product data was successfully created");
                        Console.ReadKey(true);
                    }
                    else
                    {
                        Console.WriteLine("Product test data already exists!");
                        Console.ReadKey(true);
                    }
                }
                else
                {
                    Console.WriteLine("An error has occured");
                    Console.ReadKey(true);
                }

            }

        }

    }
}









