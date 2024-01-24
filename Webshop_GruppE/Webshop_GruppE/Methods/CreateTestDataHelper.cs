using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop_GruppE.Models;

namespace Webshop_GruppE.Methods
{
    internal class CreateTestDataHelper
    {
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

                    var testCustomerId = (from c in myDb.Customers
                                          select c.Id).Max();

                    var shoppingCart = new ShoppingCart
                    {
                        CustomerId = testCustomerId,
                        ShoppingCartItems = new List<ShoppingCartItem>()
                    };
                    
                    myDb.ShoppingCarts.Add(shoppingCart);
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
