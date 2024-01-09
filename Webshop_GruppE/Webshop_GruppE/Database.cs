using Dapper;
using Microsoft.Data.SqlClient;
using Webshop_GruppE.Models;

namespace Webshop_GruppE
{
    internal class Database
    {
        public static void DisplayAllCategories()
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
                var categoryWindow = new Window("Categories", 40, 2, categoryText);
                categoryWindow.DrawWindow();
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
                        productsText.Add("Id: " + products.Id + " " + "Name: " + products.Name + " Pris: " + products.Price);
                    }
                    
                }

                if (productsText.Count == 0)
                {
                    productsText.Add("Empty");
                }
                var productsWindow = new Window("Super Products", 30, 1, productsText);
                productsWindow.DrawWindow();
            }
        }

        public static void DisplayAllProducts()
        {
            using (var database = new MyDbContext())
            {
                List<string> productsText = new List<string>();

                foreach (var products in database.Products)
                {
                    productsText.Add("Id: " + products.Id + " " + "Name: " + products.Name);
                }

                if (productsText.Count == 0)
                {
                    productsText.Add("Empty");
                }
                var productsWindow = new Window("Products", 80, 2, productsText);
                productsWindow.DrawWindow();
            }
        }

        // Använd till dapper senare ??


        //static string connString = "data source = .\\SQLEXPRESS; initial catalog = FashionCode; persist security info = True; integrated security = True;";
        //public static bool CategoryExist(int categoryId)

        //{
        //    try
        //    {
        //        using (var connection = new SqlConnection(connString))
        //        {
        //            connection.Open();
        //            string sql = "SELECT COUNT(*) FROM Categories WHERE Id = @categoryId";
        //            int count = connection.QuerySingle<int>(sql, new { categoryId });
        //            return count > 0;
        //        }
        //    }
        //    catch
        //    {
        //        Console.WriteLine("Invalid categoryID. Please enter a valid categoryId.");
        //        return false;
        //    }

        //}

    }
}



