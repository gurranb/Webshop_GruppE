using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Webshop_GruppE.Models;

namespace Webshop_GruppE.Methods
{
    internal class SignInSignUpHelper
    {
        public static async Task AdminLogInMenu()
        {
            while (true)
            {
                using (var myDb = new MyDbContext())
                {
                    var emergencyLogIn = (from c in myDb.Admins
                                          where c.Id == 1
                                          select c.Id).SingleOrDefault();

                    Console.Clear();
                    LogoWindow.LogoWindowMeth(1, 1, 24, 7);
                    List<string> profileText = new List<string> { "[1] Express Log in ", "[2] Log in ", "[3] Sign up", "[B] Back" };
                    var userWindow = new Window("Sign in as admin", 1, 10, profileText);
                    userWindow.DrawWindow();
                    var key = Console.ReadKey(true);
                    switch (key.KeyChar)
                    {
                        case '1':
                            if (emergencyLogIn == 1)
                            {
                                await AdminHelper.AdminHomePage(1);
                            }
                            else
                            {
                                Console.WriteLine("No account has been made, try adding a test account from the main menu or create a new one.");
                                Console.ReadKey(true);
                            }
                            break;
                        case '2':
                            Console.WriteLine("Log in");
                            AdminLogIn();
                            break;
                        case '3':
                            Console.WriteLine("Sign up");
                            SignUpAdmin();
                            break;
                        case 'b':
                            Console.WriteLine("Back");
                            Program.StartScreen();
                            break;
                        default:
                            Console.WriteLine("Wrong input");
                            Console.ReadKey();
                            break;
                    }
                }

            }
        }
        public static async Task AdminLogIn()
        {
            while (true)
            {
                using (var myDb = new MyDbContext())
                {
                    Console.WriteLine("Input Username: ");
                    string userName = Console.ReadLine();
                    var findUserName = (from c in myDb.Admins
                                        where c.AdminName == userName
                                        select c.AdminName).SingleOrDefault();

                    Console.WriteLine("Input Password: ");
                    string password = Console.ReadLine();
                    var findUserPassword = (from c in myDb.Admins
                                            where c.AdminPassword == password
                                            select c.AdminPassword).SingleOrDefault();

                    if (findUserName == null || findUserPassword == null)
                    {
                        Console.WriteLine("Error, username or password doesn´t exist!");
                        Console.ReadKey(true);
                        AdminLogInMenu();
                    }
                    else if (findUserName.Contains(userName) && findUserPassword.Contains(password))
                    {
                        var adminId = (from c in myDb.Admins
                                       where c.AdminPassword == password
                                       select c.Id).SingleOrDefault();

                        await AdminHelper.AdminHomePage(adminId);
                    }
                }
            }
        }

        public static void SignUpAdmin()
        {
            using (var myDb = new MyDbContext())
            {
                Console.Write("Input your first name: ");
                string adminFirstName = Console.ReadLine();
                Console.Write("Input your last name: ");
                string adminLastName = Console.ReadLine();
                Console.Write("Input your user name: ");
                string adminUserName = Console.ReadLine();
                Console.Write("Input your password: ");
                string adminPassword = Console.ReadLine();
                Console.Write("Input your mail address: ");
                string adminMailAddress = Console.ReadLine();

                myDb.Add(new Models.Admin
                {
                    FirstName = adminFirstName,
                    LastName = adminLastName,
                    AdminName = adminUserName,
                    AdminPassword = adminPassword,
                    EMailAdress = adminMailAddress

                });
                Console.WriteLine("You have successfully created your account!");
                Console.ReadKey(true);
                myDb.SaveChanges();
            }
            Console.ReadKey(true);
            Console.Clear();
        }
        public static void CustomerLogInMenu()
        {
            while (true)
            {
                using (var myDb = new MyDbContext())
                {
                    var emergencyLogIn = (from c in myDb.Customers
                                          where c.Id == 1
                                          select c.Id).SingleOrDefault();
                    Console.Clear();
                    LogoWindow.LogoWindowMeth(1, 1, 24, 7);
                    List<string> profileText = new List<string> { "[1] Express Log in ", "[2] Log in ", "[3] Sign up", "[B] Back" };
                    var userWindow = new Window("Sign in", 0, 10, profileText);
                    userWindow.DrawWindow();
                    var key = Console.ReadKey(true);
                    switch (key.KeyChar)
                    {
                        case '1':
                            if (emergencyLogIn == 1)
                            {                               
                                CustomerHelper.CustomerHomePage(1);
                            }
                            else
                            {
                                Console.WriteLine("No account has been made, try adding a test account from the main menu or create a new one.");
                                Console.ReadKey(true);
                            }
                            break;
                        case '2':
                            Console.WriteLine("Log in");
                            CustomerLogIn();
                            break;
                        case '3':
                            Console.WriteLine("Sign up");
                            CustomerSignUp();
                            break;
                        case 'b':
                            Console.WriteLine("Back");
                            Program.StartScreen();
                            break;
                        default:
                            Console.WriteLine("Wrong input");
                            Console.ReadKey();
                            break;
                    }
                }

            }
        }
        public static void CustomerLogIn()
        {
            while (true)
            {
                using (var myDb = new MyDbContext())
                {
                    Console.WriteLine("Input Username: ");
                    string userName = Console.ReadLine();
                    var findUserName = (from c in myDb.Customers
                                        where c.CustomerUserName == userName
                                        select c.CustomerUserName).SingleOrDefault();

                    Console.WriteLine("Input Password: ");
                    string password = Console.ReadLine();
                    var findUserPassword = (from c in myDb.Customers
                                            where c.CustomerPassword == password
                                            select c.CustomerPassword).SingleOrDefault();

                    if (findUserName == null || findUserPassword == null)
                    {
                        Console.WriteLine("Error, username or password doesn´t exist!");
                        Console.ReadKey(true);
                    }
                    else if (findUserName.Contains(userName) && findUserPassword.Contains(password))
                    {
                        var customerId = (from c in myDb.Customers
                                          where c.CustomerUserName == userName
                                          select c.Id).SingleOrDefault();

                        CustomerHelper.CustomerHomePage(customerId);
                    }
                }
            }
        }

        public static void CustomerSignUp()
        {
            using (var myDb = new MyDbContext())
            {
                try
                {
                    Console.Write("Input your first name: ");
                    string customerFirstName = Console.ReadLine();
                    Console.Write("Input your last name: ");
                    string customerLastName = Console.ReadLine();
                    Console.Write("Input your age: ");
                    int.TryParse(Console.ReadLine(), out int customerAge);
                    Console.Write("Input your user name: ");
                    string customerUserName = Console.ReadLine();
                    Console.Write("Input your password: ");
                    string customerPassword = Console.ReadLine();
                    Console.Write("Input your country: ");
                    string customerCountry = Console.ReadLine();
                    Console.Write("Input your street adress: ");
                    string customerStreetAddress = Console.ReadLine();
                    Console.Write("Input your postal code: ");
                    int.TryParse(Console.ReadLine(), out int customerPostalCode);
                    Console.Write("Input card number: ");
                    int.TryParse(Console.ReadLine(), out int customerCardNumber);
                    Console.Write("Input your mail address: ");
                    string customerMailAddress = Console.ReadLine();

                    myDb.Add(new Models.Customer
                    {
                        FirstName = customerFirstName,
                        LastName = customerLastName,
                        Age = customerAge,
                        CustomerUserName = customerUserName,
                        CustomerPassword = customerPassword,
                        Country = customerCountry,
                        StreetAddress = customerStreetAddress,
                        PostalCode = customerPostalCode,
                        CardNumber = customerCardNumber,
                        EMailAdress = customerMailAddress,
                    });

                    Console.WriteLine("You have successfully created your account!");
                    Console.ReadKey(true);
                    myDb.SaveChanges();

                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error: Unable to create account");
                }
                
               
                Console.ReadKey(true);
                Console.Clear();
            }
           
        }
    }
}
