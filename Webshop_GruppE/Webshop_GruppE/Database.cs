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

                
                if (categoryText.Count > 0) 
                {
                    foreach (var categories in database.Categories)
                    {
                        categoryText.Add("Id: " + categories.Id + " " + "Name: " + categories.CategoryName);
                    }
                }
                else { categoryText.Add("Empty"); }
                    var categoryWindow = new Window("Categories", 40, 2, categoryText);
                    categoryWindow.DrawWindow();
            }
        }
        public static void DisplayAllProducts()
        {
            using (var database = new MyDbContext())
            {
                List<string> productsText = new List<string>();
                if(productsText.Count > 0) 
                {
                    foreach (var products in database.Products)
                    {
                        productsText.Add("Id: " + products.Id + " " + "Name: " + products.Name);
                    }
                }
                else { productsText.Add("Empty"); }                    
                    var productsWindow = new Window("Products", 80, 2, productsText);
                    productsWindow.DrawWindow();
            }
        }
    
    
    
    
    
    
    
    
    
    }


}
