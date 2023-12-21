using Webshop_GruppE.Models;

namespace Webshop_GruppE
{
    internal class Helpers
    {
        public static void StartMessage()
        {
            List<string> welcomeText = new List<string> { "Welcome to FashionCode website" };
            var welcomeWindow = new Window("", 0, 0, welcomeText);
            welcomeWindow.DrawWindow();
            Console.WriteLine("Let us do the code so you can do the fashion");
        }

        public static void Login()
        {
            bool loggedin = false;
            Console.Clear();
            while (loggedin == false)
            {
                List<string> loginText = new List<string> { "Welcome to FashionCode website", "Login as", "[A]dmin", "[U]ser", "[E]xit" };
                var loginWindow = new Window("", 0, 0, loginText);
                loginWindow.DrawWindow();
                var key = Console.ReadKey(true);

                // ändra så detta blir snyggare
                switch (key.KeyChar)
                {
                    case 'a':
                        loggedin = true;
                        Admin();
                        break;
                    case 'u':
                        loggedin = true;
                        User();
                        break;
                    case 'e':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("wrong input");
                        Console.ReadKey();
                        break;
                }
                Console.Clear();
            }
        }

        public static void Admin()
        {
            Console.Clear();
            bool loggedin = true;
            while (loggedin)
            {
                List<string> adminText = new List<string> { "[1] Add product", "[2] Add Category", "[P] Profile page", "[C] Customer page", "[Q] Queries", "[L] Logout" };
                var adminWindow = new Window("Admin", 0, 0, adminText);
                adminWindow.DrawWindow();

                var key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case '1':
                        Console.WriteLine("Add Product");
                        ProductMenu();
                        break;
                    case '2':
                        Console.WriteLine("Add Category");
                        CategoryMenu(); 
                        break;
                    case 'p':
                        Console.WriteLine("Profile page");
                        break;
                    case 'c':
                        Console.WriteLine("Customer page");
                        break;
                    case 'q':
                        Console.WriteLine("Queries");
                        break;
                    case 'l':
                        loggedin = false;
                        Console.WriteLine("LogOut");
                        Login();
                        break;
                    default:
                        Console.WriteLine("Wrong Input");
                        Console.ReadKey(true);
                        break;
                }
            }
        }
        public static void User()
        {
            Console.Clear();
            bool loggedin = true;
            while (loggedin)
            {
                List<string> userText = new List<string> { "[S] Shopping cart", "[P] Profile page", "[B] Buy products", "[O] Order history", "[L] Logout" };
                var userWindow = new Window("Customer", 0, 0, userText);
                userWindow.DrawWindow();

                var key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case 's':
                        Console.WriteLine("");
                        break;
                    case 'p':
                        Console.WriteLine("");
                        break;
                    case 'b':
                        Console.WriteLine("");
                        break;
                    case 'o':
                        Console.WriteLine("");
                        break;
                    case 'l':
                        loggedin = false;
                        Console.Clear();
                        Login();
                        break;
                    default:
                        Console.WriteLine("wrong input");
                        Console.ReadKey(true);
                        break;
                }
            }
        }

        public static void ProductMenu()
        {
            Console.Clear();
            
            while (true)
            {
                List<string> productText = new List<string> { "[A] Add product", "[C] Change Product", "[R] Remove Product", "[B] Back" };
                var productWindow = new Window("Products", 0, 0, productText);
                productWindow.DrawWindow();

                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case 'a':
                       Console.WriteLine("Add Product");
                        break;
                    case 'c':
                        Console.WriteLine("Change Product");
                        break;
                    case 'r':
                        Console.WriteLine("Remove Product");
                        break;
                    case 'b':
                        Console.WriteLine("Back");
                       
                        Admin();
                        break;
                    default: Console.WriteLine("Wrong input");
                        break; 
                }
            }
        }

        public static void CategoryMenu()
        {
            Console.Clear();
         
            while (true)
            {
                
                List<string> categoryText = new List<string> { "[A] Add Category", "[C] Change Category", "[R] Remove Category", "[B] Back" };
                var categoryWindow = new Window("Categories", 0, 0, categoryText);
                categoryWindow.DrawWindow();
                using (var myDb = new MyDbContext())
                {
                    
                }

                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case 'a':
                        Console.WriteLine("Add Category");
                        AddCategory();
                        break;
                    case 'c':
                        Console.WriteLine("Change Category");
                        ChangeCategory();
                        break;
                    case 'r':
                        Console.WriteLine("Remove Category");
                        break;
                    case 'b':
                        Console.WriteLine("Back");
                        
                        Admin();
                        break;
                    default:
                        Console.WriteLine("Wrong input");
                        break;
                }
            }
        }
   
        public static void AddCategory() 
        {
            
            using(var myDb = new MyDbContext()) 
            {
                Console.Write("Type Category name: ");
                string categoryName = Console.ReadLine();
                myDb.Add(new Models.Category { CategoryName = categoryName });
                myDb.SaveChanges();
            }
        
        }
        public static void ChangeCategory()
        {
            
           
            using(var myDb = new MyDbContext()) 
            {
                Console.Write("Change Category name: ");
                string categoryName = Console.ReadLine();
                myDb.Add(new Models.Category { CategoryName = categoryName });
                myDb.SaveChanges();
            }
        
        }





    }
}
