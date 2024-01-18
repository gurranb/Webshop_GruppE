using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop_GruppE.Models;
using Microsoft.EntityFrameworkCore;

namespace Webshop_GruppE.Methods
{
    internal class AdminCategoryMenuHelper
    {
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
                        AdminHelper.AdminHomePage(adminId);
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
        public static void RemoveCategory(int adminId)
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
    }
}
