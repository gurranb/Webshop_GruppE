using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop_GruppE.Models;

namespace Webshop_GruppE.Methods
{
    internal class EditCustomerHelper
    {
        public static void EditFirstName(int adminId)
        {
            using (var myDb = new MyDbContext())
            {
                Console.Write("Edit first name.\nInput customer Id: ");
                int.TryParse(Console.ReadLine(), out int nameId);
                var firstName2 = (from c in myDb.Customers
                                  where c.Id == nameId
                                  select c).SingleOrDefault();
                if (firstName2 != null)
                {
                    Console.Write("Input new customer name: ");
                    string customerName2 = Console.ReadLine();


                    if (customerName2 != null)
                    {
                        firstName2.FirstName = customerName2;
                        Console.WriteLine("You have successfully changed the customer name to " + customerName2);
                        Console.ReadKey();
                        myDb.SaveChanges();
                    }
                }
                else
                {
                    Console.WriteLine("Error, wrong Id input.");
                    Console.ReadKey();

                }
                Console.Clear();
                AdminHelper.ListCustomers(adminId);
            }
        }

        public static void EditLastName(int adminId)
        {
            using (var myDb = new MyDbContext())
            {
                Console.WriteLine("Edit last name\n");
                Console.Write("Input customer Id: ");
                int.TryParse(Console.ReadLine(), out int customerId);
                var lastName2 = (from c in myDb.Customers
                               where c.Id == customerId
                                 select c).SingleOrDefault();
                if (lastName2 != null)
                {
                    Console.Write("Input new customer name: ");
                    string customerLastName2 = Console.ReadLine();


                    if (customerLastName2 != null)
                    {
                        lastName2.LastName = customerLastName2;
                        Console.WriteLine("You have successfully changed the customer name to " + customerLastName2);
                        Console.ReadKey();
                        myDb.SaveChanges();
                    }
                }
                else
                {
                    Console.WriteLine("Error, wrong Id input.");
                    Console.ReadKey();

                }
                Console.Clear();
                AdminHelper.ListCustomers(adminId);
            }
        }
        public static void EditCustomerAge(int adminId)
        {
            using (var myDb = new MyDbContext())
            {
                Console.WriteLine("Edit age\n");
                Console.Write("Input customer Id: ");
                int.TryParse(Console.ReadLine(), out int customerId);
                var customerAge = (from c in myDb.Customers
                                 where c.Id == customerId
                                 select c).SingleOrDefault();
                if (customerAge != null)
                {
                    Console.Write("Input new customer age: ");
                    int.TryParse(Console.ReadLine(), out int customerAge2);


                    if (customerAge2 != null)
                    {
                        customerAge.Age = customerAge2;
                        Console.WriteLine("You have successfully changed the customer age to " + customerAge2);
                        Console.ReadKey();
                        myDb.SaveChanges();
                    }
                }
                else
                {
                    Console.WriteLine("Error, wrong Id input.");
                    Console.ReadKey();

                }
                Console.Clear();
                AdminHelper.ListCustomers(adminId);
            }
        }

        public static void EditCustomerUserName(int adminId)
        {
            using (var myDb = new MyDbContext())
            {
                Console.WriteLine("Edit username\n");
                Console.Write("Input customer Id: ");
                int.TryParse(Console.ReadLine(), out int customerId);
                var customerUsername = (from c in myDb.Customers
                                 where c.Id == customerId
                                 select c).SingleOrDefault();
                if (customerUsername != null)
                {
                    Console.Write("Input new customer username: ");
                    string customerUsername2 = Console.ReadLine();


                    if (customerUsername2 != null)
                    {
                        customerUsername.CustomerUserName = customerUsername2;
                        Console.WriteLine("You have successfully changed the customer username to " + customerUsername2);
                        Console.ReadKey();
                        myDb.SaveChanges();
                    }
                }
                else
                {
                    Console.WriteLine("Error, wrong Id input.");
                    Console.ReadKey();

                }
                Console.Clear();
                AdminHelper.ListCustomers(adminId);
            }
        }

        public static void EditCustomerPassword(int adminId)
        {
            using (var myDb = new MyDbContext())
            {
                Console.WriteLine("Edit password\n");
                Console.Write("Input customer Id: ");
                int.TryParse(Console.ReadLine(), out int customerId);
                var customerPassword = (from c in myDb.Customers
                                        where c.Id == customerId
                                        select c).SingleOrDefault();
                if (customerPassword != null)
                {
                    Console.Write("Input new customer password: ");
                    string customerPassword2 = Console.ReadLine();


                    if (customerPassword2 != null)
                    {
                        customerPassword.CustomerPassword = customerPassword2;
                        Console.WriteLine("You have successfully changed the customer password to " + customerPassword2);
                        Console.ReadKey();
                        myDb.SaveChanges();
                    }
                }
                else
                {
                    Console.WriteLine("Error, wrong Id input.");
                    Console.ReadKey();

                }
                Console.Clear();
                AdminHelper.ListCustomers(adminId);
            }
        }
        public static void EditCustomerCountry(int adminId)
        {
            using (var myDb = new MyDbContext())
            {
                Console.WriteLine("Edit Country\n");
                Console.Write("Input customer Id: ");
                int.TryParse(Console.ReadLine(), out int customerId);
                var customerCountry = (from c in myDb.Customers
                                        where c.Id == customerId
                                        select c).SingleOrDefault();
                if (customerCountry != null)
                {
                    Console.Write("Input new customer country: ");
                    string customerCountry2 = Console.ReadLine();


                    if (customerCountry2 != null)
                    {
                        customerCountry.Country = customerCountry2;
                        Console.WriteLine("You have successfully changed the customer country to " + customerCountry2);
                        Console.ReadKey();
                        myDb.SaveChanges();
                    }
                }
                else
                {
                    Console.WriteLine("Error, wrong Id input.");
                    Console.ReadKey();

                }
                Console.Clear();
                AdminHelper.ListCustomers(adminId);
            }
        }

        public static void EditCustomerAddress(int adminId)
        {
            using (var myDb = new MyDbContext())
            {
                Console.WriteLine("Edit Address\n");
                Console.Write("Input customer Id: ");
                int.TryParse(Console.ReadLine(), out int customerId);
                var customerAddress = (from c in myDb.Customers
                                       where c.Id == customerId
                                       select c).SingleOrDefault();
                if (customerAddress != null)
                {
                    Console.Write("Input new customer address: ");
                    string customerAddress2 = Console.ReadLine();


                    if (customerAddress2 != null)
                    {
                        customerAddress.StreetAddress = customerAddress2;
                        Console.WriteLine("You have successfully changed the customer address to " + customerAddress2);
                        Console.ReadKey();
                        myDb.SaveChanges();
                    }
                }
                else
                {
                    Console.WriteLine("Error, wrong Id input.");
                    Console.ReadKey();

                }
                Console.Clear();
                AdminHelper.ListCustomers(adminId);
            }
        }

        public static void EditCustomerPostalCode(int adminId)
        {
            using (var myDb = new MyDbContext())
            {
                Console.WriteLine("Edit postal code\n");
                Console.Write("Input customer Id: ");
                int.TryParse(Console.ReadLine(), out int customerId);
                var customerPostalCode = (from c in myDb.Customers
                                   where c.Id == customerId
                                   select c).SingleOrDefault();
                if (customerPostalCode != null)
                {
                    Console.Write("Input new customer postal code: ");
                    int.TryParse(Console.ReadLine(), out int customerPostalCode2);


                    if (customerPostalCode2 != null)
                    {
                        customerPostalCode.PostalCode = customerPostalCode2;
                        Console.WriteLine("You have successfully changed the customer postal code to " + customerPostalCode2);
                        Console.ReadKey();
                        myDb.SaveChanges();
                    }
                }
                else
                {
                    Console.WriteLine("Error, wrong Id input.");
                    Console.ReadKey();

                }
                Console.Clear();
                AdminHelper.ListCustomers(adminId);
            }
        }

        public static void EditCustomerCardNumber(int adminId)
        {
            using (var myDb = new MyDbContext())
            {
                Console.WriteLine("Edit card number\n");
                Console.Write("Input customer Id: ");
                int.TryParse(Console.ReadLine(), out int customerId);
                var customerCardNumber = (from c in myDb.Customers
                                       where c.Id == customerId
                                       select c).SingleOrDefault();
                if (customerCardNumber != null)
                {
                    Console.Write("Input new customer card number: ");
                    int.TryParse(Console.ReadLine(), out int customerCardNumber2);


                    if (customerCardNumber2 != null)
                    {
                        customerCardNumber.CardNumber = customerCardNumber2;
                        Console.WriteLine("You have successfully changed the customer card number to " + customerCardNumber2);
                        Console.ReadKey();
                        myDb.SaveChanges();
                    }
                }
                else
                {
                    Console.WriteLine("Error, wrong Id input.");
                    Console.ReadKey();

                }
                Console.Clear();
                AdminHelper.ListCustomers(adminId);
            }
        }

        public static void EditCustomerEmail(int adminId)
        {
            using (var myDb = new MyDbContext())
            {
                Console.WriteLine("Edit E-Mail\n");
                Console.Write("Input customer Id: ");
                int.TryParse(Console.ReadLine(), out int customerId);
                var customerEmail = (from c in myDb.Customers
                                       where c.Id == customerId
                                       select c).SingleOrDefault();
                if (customerEmail != null)
                {
                    Console.Write("Input new customer E-Mail: ");
                    string customerEmail2 = Console.ReadLine();


                    if (customerEmail2 != null)
                    {
                        customerEmail.EMailAdress = customerEmail2;
                        Console.WriteLine("You have successfully changed the customer E-Mail to " + customerEmail2);
                        Console.ReadKey();
                        myDb.SaveChanges();
                    }
                }
                else
                {
                    Console.WriteLine("Error, wrong Id input.");
                    Console.ReadKey();

                }
                Console.Clear();
                AdminHelper.ListCustomers(adminId);
            }
        }
    }
}

