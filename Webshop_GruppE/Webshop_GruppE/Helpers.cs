using System.Linq;
using Webshop_GruppE.Models;

namespace Webshop_GruppE
{
    internal class Helpers
    {

        public static void Start()
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
                        Admin();
                        break;
                    case 'u':
                        UserLogInPage();                       
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
            
            while (true)
            {
                List<string> adminText = new List<string> { "[1] Edit Product", "[2] Edit Category", "[3] Product Overview", "[P] Profile Page", "[C] Customer Page", "[Q] Queries", "[L] Logout" };
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
                        Start();
                        break;
                    default:
                        Console.WriteLine("Wrong Input");
                        Console.ReadKey(true);
                        break;
                }
            }
        }

        public static void UserLogInPage()
        {
            while(true) 
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
                        User();
                        break;
                    case '2':
                        Console.WriteLine("Log in");
                        LogIn();
                        break;
                    case '3':
                        Console.WriteLine("Sign up");
                        break;
                    case 'b':
                        Console.WriteLine("Back");
                        Start();
                        break;
                    default:
                        Console.WriteLine("Wrong input");
                        Console.ReadKey();
                        break;
                }
            }
            
        }

        public static void User()
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
                        Start();
                        break;
                    default:
                        Console.WriteLine("Wrong Input");
                        Console.ReadKey(true);
                        break;
                }
            }
        }

        public static void LogIn()
        {
            Console.WriteLine("Input Username: ");
            string userName = Console.ReadLine();
            Console.WriteLine("Input Password: ");
            string password = Console.ReadLine();
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
                            User();
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
                            User();
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

                if(chosenProduct != null)
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
                            User();
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
                        Console.WriteLine("Change Product");
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
                while (true)
                {
                    // ändra ifall man vill kunna lägga till i fler categories ??

                    Console.Write("Type Category ID: ");
                    int category = int.Parse(Console.ReadLine());

                    var category3 = (from c in myDb.Categories
                                     where c.Id == category
                                     select c);
                    if (category3 != null && category3.Any())
                    {
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
                            Categories = category3.ToList(),
                            Price = productPrice,
                            ProductSupplierId = productSupplierId,
                            ProductInfo = productInfo,
                            StockBalance = stockBalance,
                            SelectedProduct = selectedProduct
                        });
                        myDb.SaveChanges();
                        Console.WriteLine("You have added " + productName + " to the list");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error category Id does not exist!");
                    }
                }
                Console.Clear();
            }

        }

        public static void ChangeProduct()
        {

            using (var myDb = new MyDbContext())
            {
                List<string> changeProductText = new List<string> { "[1] Change product name", "[2] Change product price", "[3] Change product supplier Id",
                        "[4] Change product info", "[5] Change product stock balance", "[B] Back" };
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
                        Console.WriteLine("[4] Change product supplier Id");
                        ChangeProductSupplier();
                        break;
                    case '4':
                        Console.WriteLine("[5] Change product info");
                        ChangeProductInfo();
                        break;
                    case '5':
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
                Admin();
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
                                     where c.Categories.Any(x => x.Id == productId)
                                     select c);

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
