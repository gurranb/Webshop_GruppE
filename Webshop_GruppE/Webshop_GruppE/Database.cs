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
                var categoryWindow = new Window("Categories", 40, 2, categoryText);
                categoryWindow.DrawWindow();
            }
        }
    }
}
