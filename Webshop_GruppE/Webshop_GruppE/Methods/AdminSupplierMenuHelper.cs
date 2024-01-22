using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop_GruppE.Models;

namespace Webshop_GruppE.Methods
{
    internal class AdminSupplierMenuHelper
    {
        public static void SupplierMenu(int adminId)
        {
            Console.Clear();

            while (true)
            {
                LogoWindow.LogoWindowMeth(1, 1, 24, 7);
                List<string> supplierList = DisplayDatabase.DisplayAllSuppliers();
                List<string> supplierText = new List<string> { "[1] Add supplier", "[2] Edit supplier", "[3] Remove supplier", "[B] Back" };
                var userWindow = new Window("Supplier Menu", 1, 10, supplierText);
                userWindow.DrawWindow();
                var supplierWindow = new Window("Suppliers", 30, 1, supplierList);
                supplierWindow.DrawWindow();

                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case '1':
                        AddSupplier(adminId);
                        break;
                    case '2':
                        EditSupplier(adminId);
                        break;
                    case '3':
                        RemoveSupplier(adminId);
                        break;
                    case 'b':
                        AdminHelper.AdminHomePage(adminId);
                        break;
                    default:
                        Console.WriteLine("Error: Wrong input!");
                        Console.ReadKey(true);
                        Console.Clear();
                        break;
                }
            }
        }
        public static void AddSupplier(int adminId)
        {
            using (var myDb = new MyDbContext())
            {
                Console.WriteLine("Add supplier.\nInput supplier name: ");
                string supplierName = Console.ReadLine();
                Console.WriteLine("Input supplier country: ");
                string supplierCountry = Console.ReadLine();
                myDb.Add(new Models.ProductSupplier { Name = supplierName, Country = supplierCountry });
                myDb.SaveChanges();
                Console.WriteLine("You have added " + supplierName + " to the list");
            }
            Console.ReadKey(true);
            Console.Clear();
        }

        public static void EditSupplier(int adminId)
        {

            using (var myDb = new MyDbContext())
            {
                Console.WriteLine("Edit suppler.\n\nInput supplier Id: ");
                int.TryParse(Console.ReadLine(), out int supplierId);
                

                var newName = (from c in myDb.ProductSuppliers
                               where c.Id == supplierId
                               select c).SingleOrDefault();

                if (newName != null)
                {
                    Console.WriteLine("Input new name for supplier: ");
                    string newNameString = Console.ReadLine();
                    {
                        newName.Name = newNameString;
                        Console.WriteLine("Succesfully changed supplier name to " + newName.Name +"\nChange current country " + newName.Country + "? Y/N");
                        var key = Console.ReadKey(true);
                        switch(key.KeyChar)
                        {
                            case 'y':
                                Console.WriteLine("Input new supplier country: ");
                                string newCountry = Console.ReadLine();
                                newName.Country = newCountry;
                                Console.WriteLine("Country successfully changed into " + newName.Country);
                                break;
                            case 'n':
                                Console.WriteLine("Country will not be changed.");
                                Console.ReadKey(true);
                                break;
                            default:
                                Console.WriteLine("Error, wrong input!");
                                Console.ReadKey(true);
                                break;
                        }
                    }
                    
                    myDb.SaveChanges();

                }
                else
                {
                    Console.WriteLine("Error, wrong Id");
                    Console.ReadKey();
                }
            }
            Console.Clear();
        }

        public static void RemoveSupplier(int adminId)
        {
            using (var myDb = new MyDbContext())
            {
                Console.WriteLine("Remove supplier.\nInput Id for the supplier you wish to remove");
                int.TryParse(Console.ReadLine(), out int supplierId);

                var removeSupplier = (from c in myDb.ProductSuppliers
                                      where c.Id == supplierId
                                      select c).SingleOrDefault();
                var findProduct = (from c in myDb.Products
                                   where c.ProductSupplierId == supplierId
                                   select c).ToList();
                if (removeSupplier != null)
                {
                    if(findProduct.Count == 0)
                    {
                        myDb.Remove(removeSupplier);
                        Console.WriteLine("You've successfully removed " + removeSupplier.Name + " from the suppler list.");
                        myDb.SaveChanges();
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("There is one or more products in " + removeSupplier.Name + ". Please try again after removing the products.");
                        Console.ReadKey(true);
                    }    
                    
                    
                }
                else
                {
                    Console.WriteLine("Error, wrong Id");
                    Console.ReadKey();
                }
                Console.Clear();
            }
        }
    }
}
