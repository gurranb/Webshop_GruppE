using Microsoft.EntityFrameworkCore;
using Webshop_GruppE.Models;

namespace Webshop_GruppE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            //using(var myDb = new MyDbContext())
            //{
            //    Console.WriteLine("Input two categories: ");
            //    int.TryParse(Console.ReadLine(), out int categoryIdFirst);
            //    int.TryParse(Console.ReadLine(), out int categoryIdSecond);

            //    List<Category> categoryList = myDb.Categories.Where(c => c.Id == categoryIdFirst && c.Id == categoryIdSecond).ToList();
            //    var categoryList = myDb.Categories.Where(c => c.Id == categoryIdFirst);
            //    var categoryList2 = myDb.Categories.Where(c => c.Id == categoryIdSecond);
               

            //    myDb.Add(new Product { Name = "Jeans, Herr", Categories = categoryList });
            //    myDb.SaveChanges();
            //    Console.WriteLine("You have added a new item");
            //    Console.ReadKey();
            //}
            Helpers.Login();

        }

    }
}