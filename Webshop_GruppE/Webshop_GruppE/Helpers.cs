using Webshop_GruppE.Models;

namespace Webshop_GruppE
{
    internal class Helpers
    {

        public static void Login()
        {
            bool loggedin = false;
            Console.Clear();
            while (loggedin == false)
            {
                List<string> loginText = new List<string> { "Welcome to FashionCode website", "Login as", "[A]dmin", "[U]ser", "[E]xit" };
                var loginWindow = new Window("", 1, 1, loginText);
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
                        Console.WriteLine("Wrong Input");
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
                List<string> adminText = new List<string> { "[1] Edit Product", "[2] Edit Category", "[P] Profile Page", "[C] Customer Page", "[Q] Queries", "[L] Logout" };
                var adminWindow = new Window("Admin", 1, 1, adminText);
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
                        Console.WriteLine("Profile Page");
                        break;
                    case 'c':
                        Console.WriteLine("Customer Page");
                        break;
                    case 'q':
                        Console.WriteLine("Queries");
                        break;
                    case 'l':
                        loggedin = false;
                        Console.WriteLine("Logout");
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
                List<string> userText = new List<string> { "[S] Shopping Cart", "[P] Profile Page", "[B] Buy Products", "[O] Order History", "[L] Logout" };
                var userWindow = new Window("Customer", 1, 1, userText);
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
                        Console.WriteLine("Wrong Input");
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
                List<string> productText = new List<string> { "[A] Add Product", "[C] Change Product", "[R] Remove Product", "[B] Back" };
                var productWindow = new Window("Products", 1, 1, productText);
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
                    default: Console.WriteLine("Wrong Input");
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
                var categoryWindow = new Window("Category Menu", 1, 1, categoryText);
                categoryWindow.DrawWindow();
                Database.DisplayAllCategories();
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
                        Console.WriteLine("Change Category Name");
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
                        Console.WriteLine("Wrong Input");
                        break;
                }
                
            }
        }
   
        public static void AddCategory() 
        {
            
            using(var myDb = new MyDbContext()) 
            {
                Console.Write("Type Category Name: ");
                string categoryName = Console.ReadLine();
                myDb.Add(new Models.Category { CategoryName = categoryName });
                myDb.SaveChanges();
                Console.WriteLine("You have added " + categoryName + " to the list");
            }
            Console.Clear();
        }
        public static void ChangeCategory()
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
                    myDb.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Error wrong ID");
                }
            }
            Console.Clear();

        }

    }
}
