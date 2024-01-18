﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop_GruppE.Models;
using Dapper;

namespace Webshop_GruppE.Methods
{
    internal class AdminHelper
    {
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
                    List<string> adminText = new List<string> { "[1] Edit Products", "[2] Edit Categories", "[3] Edit Suppliers", "[4] Product Overview", "[P] Profile Page", "[C] Customer Page", "[Q] Queries", "[L] Logout" };
                    var adminWindow = new Window("Welcome " + adminUserName, 1, 10, adminText);
                    adminWindow.DrawWindow();
                }
                var key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case '1':
                        AdminProductMenuHelper.ProductMenu(adminId);
                        break;
                    case '2':
                        AdminCategoryMenuHelper.CategoryMenu(adminId);
                        break;
                    case '3':
                        AdminSupplierMenuHelper.SupplierMenu(adminId);
                        break;
                    case '4':
                        ProductOverview(adminId);
                        break;                   
                    case 'p':
                        Database.DisplayAdminDetails(adminId);
                        break;
                    case 'c':
                        ListCustomers(adminId);
                        break;
                    case 'q':
                        Console.WriteLine("Queries");
                        break;
                    case 'l':
                        Program.StartScreen();
                        break;
                    default:
                        Console.WriteLine("Wrong Input");
                        Console.ReadKey(true);
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
            Console.WriteLine("{0,-5}{1,-15}{2,-15}{3,-5}{4,-15}{5,-15}{6,-15}{7,-20}{8,-15}{9,-20}{10,-20}",
                      "ID", "First Name", "Last Name", "Age", "Username", "Password", "Country",
                      "Street Address", "Postal Code", "Card Number", "E-Mail");

            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------");

            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.Id,-5}{customer.FirstName,-15}{customer.LastName,-15}" +
                                  $"{customer.Age,-5}{customer.CustomerUserName,-15}{customer.CustomerPassword,-15}" +
                                  $"{customer.Country,-15}{customer.StreetAddress,-20}{customer.PostalCode,-15}" +
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

    }
}
