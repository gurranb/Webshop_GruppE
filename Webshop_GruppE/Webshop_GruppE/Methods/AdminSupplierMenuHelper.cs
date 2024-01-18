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
                Database.DisplayAllSuppliers();
                List<string> supplierText = new List<string> { "[1] Add supplier", "[2] Edit supplier", "[3] Remove supplier", "[B] Back" };
                var userWindow = new Window("Supplier Menu", 1, 10, supplierText);
                userWindow.DrawWindow();

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
                }
            }
        }
        public static void AddSupplier(int adminId)
        {
            using (var myDb = new MyDbContext())
            {
                Console.WriteLine("Input Supplier Name: ");
                string supplierName = Console.ReadLine();
                myDb.Add(new Models.ProductSupplier { Name = supplierName });
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
                Console.WriteLine("Change supplier name.\n\nInput Supplier Id: ");
                int.TryParse(Console.ReadLine(), out int supplierId);
                Console.WriteLine("Input new name for supplier: ");
                string newNameString = Console.ReadLine();

                var newName = (from c in myDb.ProductSuppliers
                               where c.Id == supplierId
                               select c).SingleOrDefault();

                if (newName != null)
                {
                    newName.Name = newNameString;
                    Console.WriteLine("Succesfully changed supplier name to " + newName.Name);
                    Console.ReadKey();
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
                Console.WriteLine("Input Id for the supplier you wish to remove");
                int.TryParse(Console.ReadLine(), out int supplierId);

                var removeSupplier = (from c in myDb.ProductSuppliers
                                      where c.Id == supplierId
                                      select c).SingleOrDefault();
                if (removeSupplier != null)
                {
                    myDb.Remove(removeSupplier);
                    Console.WriteLine("You've successfully removed " + removeSupplier.Name + ".");
                    myDb.SaveChanges();
                    Console.ReadKey();
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
