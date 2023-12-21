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

            while (loggedin == false)
            {
                List<string> loginText = new List<string> { "Welcome to FashionCode website", "Login as", "[A]dmin", "[U]ser" };
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
                List<string> adminText = new List<string> { "[A] Add product/Category", "[P] Profile page", "[C] Customer page", "[Q] Queries", "[L] Logout" };
                var adminWindow = new Window("Admin", 0, 0, adminText);
                adminWindow.DrawWindow();

                var key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case 'a':
                        Console.WriteLine("");
                        break;
                    case 'p':
                        Console.WriteLine("");
                        break;
                    case 'c':
                        Console.WriteLine("");
                        break;
                    case 'q':
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

    }
}
