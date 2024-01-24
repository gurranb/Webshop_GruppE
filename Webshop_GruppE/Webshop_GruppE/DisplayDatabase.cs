using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Webshop_GruppE.Models;

namespace Webshop_GruppE
{
    internal class DisplayDatabase
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
                var categoryWindow = new Window("Customer Details", 70, 1, customerText);
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
                var categoryWindow = new Window("Admin Details", 25, 10, adminText);
                categoryWindow.DrawWindow();
                Console.ReadKey(true);
            }
        }
        public static List<string> DisplayAllSuppliers()
        {
            using (var database = new MyDbContext())
            {
                List<string> supplierText = new List<string>();

                foreach (var supplier in database.ProductSuppliers)
                {
                    supplierText.Add($"Id: {supplier.Id, -5} Name: {supplier.Name, -17}  Country:  {supplier.Country}");
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

        public static List<string> DisplayChosenProducts()
        {
            using (var database = new MyDbContext())
            {
                List<string> productText = new List<string>();               
                var productList = database.SelectTopDealItems
                    .Include(c => c.Product).ToList();

                foreach (var product in productList)
                {                  
                    productText.Add($"Id: {product.Product.Id, -5}{product.Product.Name,-16}{product.Product.Price + "$"}");
                    
                }

                if (productText.Count == 0)
                {
                    productText.Add("Empty");
                }
                return productText;
            }
        }

        public static List<string> DisplayAllProducts()
        {
            using (var database = new MyDbContext())
            {
                List<string> productsText = new List<string>();

                foreach (var products in database.Products)
                {
                    productsText.Add($"Id: {products.Id,-5}Name: {products.Name,-20}Price: {products.Price + "$", -10}" +
                        $"Supplier: {products.ProductSupplierId, -5}Stockbalance: {products.StockBalance, -4}Units");
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



