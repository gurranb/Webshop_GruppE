﻿using Webshop_GruppE.Models;

namespace Webshop_GruppE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Helpers.StartMessage();

            using (var myDb = new Models.MyDbContext())
            {
               // List<int> existingCategoryIds = new List<int> { 1, 2 };

               // List<Category> existingCategories = myDb.Categories.Where(c => existingCategoryIds.Contains(c.Id)).ToList();
                //myDb.Categories.Add(new Models.Category { CategoryName = "Herr" });
               // var product1 = new Models.Product { Name = "Herr Shorts", SelectedProduct = false, Categories = new List<Category> { new Category { CategoryName = "Herr" }, new Category { CategoryName = "Byxor" } } };
               // myDb.Products.AddRange(new Models.Product { Name = "Herrshorts, Jeans", Categories = existingCategories });
                //myDb.Products.AddRange(new Models.Product { Name = "HerrShorts", SelectedProduct = false, Categories = new List<Category> {}, });

                //new Actor { Name = "Tom Cruise", Movies = new List<Movie> { movie1, movie3 } },
                //myDb.Products.Add(product1);
                myDb.SaveChanges();
            }
        }

    }
}