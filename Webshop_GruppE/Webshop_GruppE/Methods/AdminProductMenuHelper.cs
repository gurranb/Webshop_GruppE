using Microsoft.EntityFrameworkCore.Storage;
using Webshop_GruppE.Models;

namespace Webshop_GruppE.Methods
{
    internal class AdminProductMenuHelper
    {
        public static void ProductMenu(int adminId)
        {
            Console.Clear();

            while (true)
            {
                LogoWindow.LogoWindowMeth(1, 1, 24, 7);
                List<string> productText = new List<string> { "[A] Add product", "[E] Edit product", "[R] Remove product", "[B] Back" };
                var productWindow = new Window("Product Menu", 1, 10, productText);
                productWindow.DrawWindow();

                List<string> categoryText = DisplayDatabase.DisplayAllCategories();
                var categoryWindow = new Window("Categories", 35, 10, categoryText);
                categoryWindow.DrawWindow();

                List<string> supplierText = DisplayDatabase.DisplayAllSuppliers();
                var supplierWindow = new Window("Suppliers", 27, 1, supplierText);
                supplierWindow.DrawWindow();

                List<string> productsText = DisplayDatabase.DisplayAllProducts();
                var productsWindow = new Window("Products", 84, 1, productsText);
                productsWindow.DrawWindow();

                var dealText = DisplayDatabase.DisplayChosenProducts();
                var dealWindow = new Window("Fashion Deals", 35, 20, dealText);
                dealWindow.DrawWindow();

                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case 'a':
                        AddProduct(adminId);
                        break;
                    case 'e':
                        EditProductMenu(adminId);
                        break;
                    case 'r':
                        RemoveProduct(adminId);
                        break;
                    case 'b':
                        AdminHelper.AdminHomePage(adminId);
                        break;
                    default:
                        Console.WriteLine("Wrong input!");
                        Console.ReadKey(true);
                        Console.Clear();
                        break;
                }
            }
        }
        public static void AddProduct(int adminId)
        {

            using (var myDb = new MyDbContext())
            {
                while (true)
                {
                    Console.Write("Add product.\nType category Id: ");

                    int.TryParse(Console.ReadLine(), out int category);

                    var category3 = (from c in myDb.Categories
                                     where c.Id == category
                                     select c);

                    if (category3 != null && category3.Any())
                    {
                        Console.Write("Type product name: ");
                        string productName = Console.ReadLine();

                        Console.Write("Type product price: ");
                        float.TryParse(Console.ReadLine(), out float productPrice);

                        if (productPrice != null && productPrice > 0)
                        {
                            Console.Write("Type product supplier Id: ");
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

                                    Console.WriteLine("Size guide:\n" +
                                        "1.Small    2.Medium    3.Large");
                                    Console.Write("Type size Id: ");
                                    int.TryParse(Console.ReadLine(), out productSize);

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
                                    Console.WriteLine("Show product in selected deals frontpage? Y/N");
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
                                        Console.ReadKey(true);
                                        ProductMenu(adminId);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Error, invalid input");
                                    Console.ReadLine();
                                    ProductMenu(adminId);
                                }

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
        public static void EditProductMenu(int adminId)
        {
            using (var myDb = new MyDbContext())
            {
                Console.Clear();
                LogoWindow.LogoWindowMeth(1, 1, 24, 7);
                List<string> changeProductText = new List<string> { "[1] Edit product name", "[2] Edit product price", "[3] Edit product supplier Id",
                        "[4] Edit product info", "[5] Edit product stock balance", "[6] Edit if on deal", "[B] Back" };
                var changeProductWindow = new Window("Edit Product Menu", 1, 10, changeProductText);
                changeProductWindow.DrawWindow();

                List<string> categoryText = DisplayDatabase.DisplayAllCategories();
                var categoryWindow = new Window("Categories", 35, 10, categoryText);
                categoryWindow.DrawWindow();

                List<string> supplierText = DisplayDatabase.DisplayAllSuppliers();
                var supplierWindow = new Window("Suppliers", 27, 1, supplierText);
                supplierWindow.DrawWindow();

                List<string> productsText = DisplayDatabase.DisplayAllProducts();
                var productsWindow = new Window("Products", 84, 1, productsText);
                productsWindow.DrawWindow();

                var dealText = DisplayDatabase.DisplayChosenProducts();
                var dealWindow = new Window("Fashion Deals", 35, 20, dealText);
                dealWindow.DrawWindow();

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
                        Console.ReadKey(true);
                        Console.Clear();
                        break;
                }
            }
        }

        public static void EditProductName()
        {
            using (var myDb = new MyDbContext())
            {

                Console.Write("Edit product name\nInput product Id: ");
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

                Console.Write("Edit product price\nInput product Id: ");
                int.TryParse(Console.ReadLine(), out int productId);

                var newPrice = (from c in myDb.Products
                                where c.Id == productId
                                select c).SingleOrDefault();

                if (newPrice != null)
                {
                    Console.Write("Input new product price: ");
                    float.TryParse(Console.ReadLine(), out float productPrice2);

                    if (productPrice2 < 0.1f)
                    {
                        Console.WriteLine("Error, " + productPrice2 + " is an incorrect price Value");
                        Console.ReadKey(true);
                    }
                    else
                    {
                        newPrice.Price = productPrice2;
                        Console.WriteLine("You have successfully changed the product price to " + productPrice2 + " $");
                        Console.ReadKey(true);
                        myDb.SaveChanges();
                    }
                }
                else
                {
                    Console.WriteLine("Error: Product does not exist!");
                    Console.ReadKey(true);
                }
            }


            Console.Clear();
        }

        public static void EditProductSupplier()
        {
            using (var myDb = new MyDbContext())
            {
                Console.Write("Edit product supplier.\nInput product Id: ");
                int.TryParse(Console.ReadLine(), out int productId);

                var findProductId = (from c in myDb.Products
                                     where c.Id == productId
                                     select c).SingleOrDefault();

                if (findProductId != null)
                {
                    Console.Write("Input new product supplier Id: ");
                    int.TryParse(Console.ReadLine(), out int productSupplierId2);

                    var findSupplerId = (from c in myDb.ProductSuppliers
                                         where c.Id == productSupplierId2
                                         select c).SingleOrDefault();

                    if(findSupplerId != null)
                    {
                        findProductId.ProductSupplierId = productSupplierId2;
                        Console.WriteLine("You have successfully changed the product supplier Id to " + productSupplierId2);
                        Console.ReadKey();
                        myDb.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("Error: Wrong ID");
                        Console.ReadKey(true);
                    }       
                }
                else
                {
                    Console.WriteLine("Error: Wrong Id!");
                    Console.ReadKey();
                }
            }
            Console.Clear();
        }

        public static void EditProductInfo()
        {
            using (var myDb = new MyDbContext())
            {

                Console.Write("Edit product info.\nInput product Id: ");
                int.TryParse(Console.ReadLine(), out int productId);
                
                var newProductInfo = (from c in myDb.Products
                                      where c.Id == productId
                                      select c).SingleOrDefault();

                if (newProductInfo != null)
                {
                    Console.WriteLine("Input new product info: ");
                    string productInfo2 = Console.ReadLine();

                    newProductInfo.ProductInfoText = productInfo2;
                    Console.WriteLine("You have successfully changed the product info to\n" + productInfo2);
                    Console.ReadKey(true);
                    myDb.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Error: Wrong ID");
                    Console.ReadKey();
                }
            }
            Console.Clear();
        }
        public static void EditStockBalance()
        {
            using (var myDb = new MyDbContext())
            {

                Console.Write("Edit products in stock.\nInput product Id: ");
                int.TryParse(Console.ReadLine(), out int productId);
        
                var newStockBalance = (from c in myDb.Products
                                       where c.Id == productId
                                       select c).SingleOrDefault();

                if (newStockBalance != null)
                {
                    Console.Write("Input new stock balance: ");
                    int.TryParse(Console.ReadLine(), out int productStockBalance2);

                    if(productStockBalance2 !> 0)
                    {
                        newStockBalance.StockBalance = productStockBalance2;
                        Console.WriteLine("You have successfully changed the product stock balance to: " + productStockBalance2 + " units");
                        Console.ReadKey(true);
                        myDb.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("Error: Invalid stockvalue!");
                        Console.ReadKey(true);
                    }                 
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
                var dealText = DisplayDatabase.DisplayChosenProducts();
                var dealWindow = new Window("Fashion Deals", 35, 20, dealText);
                dealWindow.DrawWindow();

                Console.Write("Select product for frontpage.\nInput product Id: ");
                int.TryParse(Console.ReadLine(), out int productId);

                var selectedProduct = (from c in myDb.Products
                                       where c.Id == productId
                                       select c).FirstOrDefault();

                var selectedDeal = (from c in myDb.SelectTopDealItems
                                             where c.Product.Id == productId
                                             select c).FirstOrDefault();
                var productList = (from c in myDb.SelectTopDealItems
                                   select c).ToList();
                if (productList.Count <= 5)
                {
                    if (selectedProduct != null && selectedDeal == null)
                    {

                        myDb.Add(new Models.SelectTopDealItem { Product = selectedProduct });
                        myDb.SaveChanges();
                        Console.WriteLine("Items successfully added to deals!");
                        Console.ReadKey(true);

                    }
                    else
                    {
                        Console.WriteLine("Error: Wrong Id or product already exists in deals window!.");
                        Console.ReadKey(true);
                    }
                    
                }
                else
                {
                    Console.WriteLine("Too many items in product deals, please remove at least one");
                    Console.ReadKey(true);
                }
                
                Console.Clear();
            }

        }
        public static void RemoveProduct(int adminId)
        {
            using (var myDb = new MyDbContext())
            {

                Console.Write("Remove product.\nInput product Id: ");
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
    }
}
