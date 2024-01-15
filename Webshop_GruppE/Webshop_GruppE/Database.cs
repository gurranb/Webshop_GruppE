using Dapper;
using Microsoft.Data.SqlClient;
using Webshop_GruppE.Models;

namespace Webshop_GruppE
{
    internal class Database
    {
        public static void DisplayCustomerDetails(int customerId)
        {
            using (var database = new MyDbContext())
            {
                List<string> customerText = new List<string>();

                var customerProfileDetails = (from c in database.Customers
                                        where c.Id == customerId
                                        select c).SingleOrDefault();

                customerText.Add("Name: " + customerProfileDetails.FirstName + " " + customerProfileDetails.LastName);
                customerText.Add("Age: " + customerProfileDetails.Age + " y/o");
                customerText.Add("Country: " + customerProfileDetails.Country);
                customerText.Add("Street Address: " + customerProfileDetails.StreetAddress);
                customerText.Add("Postal Code: " + customerProfileDetails.PostalCode);
                customerText.Add("Card Number: " + customerProfileDetails.CardNumber);
                customerText.Add("E-mail: " + customerProfileDetails.EMailAdress);
                

                if (customerText.Count == 0)
                {
                    customerText.Add("Empty");
                }
                var categoryWindow = new Window("Customer Details", 55, 01, customerText);
                categoryWindow.DrawWindow();
            }
        }
        public static void DisplayAdminDetails(int adminId)
        {
            using (var database = new MyDbContext())
            {
                List<string> adminText = new List<string>();

                var adminProfileDetails = (from c in database.Admins
                                        where c.Id == adminId
                                        select c).SingleOrDefault();

                adminText.Add("Name: " + adminProfileDetails.FirstName + " " + adminProfileDetails.LastName);
                adminText.Add("E-mail: " + adminProfileDetails.EMailAdress);
                

                if (adminText.Count == 0)
                {
                    adminText.Add("Empty");
                }
                var categoryWindow = new Window("Admin Details", 25, 01, adminText);
                categoryWindow.DrawWindow();
            }
        }
        public static List<string> DisplayAllSuppliers()
        {
            using (var database = new MyDbContext())
            {
                List<string> supplierText = new List<string>();

                foreach (var supplier in database.ProductSuppliers)
                {
                    supplierText.Add("Id: " + supplier.Id + " " + "Name: " + supplier.Name);
                }

                if (supplierText.Count == 0)
                {
                    supplierText.Add("Empty");
                }
                return supplierText;
            }
        }
        public static List<string> DisplayAllCategories()
        {
            using (var database = new MyDbContext())
            {
                List<string> categoryText = new List<string>();

                foreach (var categories in database.Categories)
                {
                    categoryText.Add("Id: " + categories.Id + " " + "Name: " + categories.CategoryName);
                }

                if (categoryText.Count == 0)
                {
                    categoryText.Add("Empty");
                }
                return categoryText;
            }
        }

        public static void DisplayChosenProducts()
        {
            using (var database = new MyDbContext())
            {
                List<string> productsText = new List<string>();



                foreach (var products in database.Products)
                {
                    if(products.SelectedProduct == true)
                    {
                        productsText.Add("Id: " + products.Id + " " + "Name: " + products.Name + " Pris: " + products.Price + "$");
                    }                   
                }

                if (productsText.Count == 0)
                {
                    productsText.Add("Empty");
                }
                var productsWindow = new Window("Fashion Deals", 30, 1, productsText);
                productsWindow.DrawWindow();
            }
        }

        public static List<string> DisplayAllProducts()
        {
            using (var database = new MyDbContext())
            {
                List<string> productsText = new List<string>();

                foreach (var products in database.Products)
                {
                    productsText.Add("Id: " + products.Id + " " + "Name: " + products.Name + " Brand: " + products.ProductBrand);
                }

                if (productsText.Count == 0)
                {
                    productsText.Add("Empty");
                }
                return productsText;
            }
        }
    }
}



