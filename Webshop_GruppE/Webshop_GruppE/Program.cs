using Microsoft.EntityFrameworkCore;
using Webshop_GruppE.Models;

namespace Webshop_GruppE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            StartScreen();

        }

        public static void StartScreen()
        {
            Console.Clear();
            
            while (true)
            {
                LogoWindow.LogoWindowMeth(1, 1, 24, 7);
                List<string> loginText = new List<string> {"Login as", "[A] Admin", "[U] User", "[C] Ceate test data", "[E] Exit" };
                var loginWindow = new Window("Welcome to FashionCode website", 0, 10, loginText);
                loginWindow.DrawWindow();
                var key = Console.ReadKey(true);

                // ändra så detta blir snyggare
                switch (key.KeyChar)
                {
                    case 'a':
                        Helpers.AdminLogInMenu();
                        break;
                    case 'u':
                        Helpers.CustomerLogInMenu();
                        break;
                    case 'c':
                        Helpers.CreateTestData();
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

    }
}