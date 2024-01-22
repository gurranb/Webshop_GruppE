using Microsoft.EntityFrameworkCore;
using Webshop_GruppE.Methods;
using Webshop_GruppE.Models;

namespace Webshop_GruppE
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            
            await StartScreen();

        }

        public static async Task StartScreen()
        {
            Console.Clear();
            
            while (true)
            {
                LogoWindow.LogoWindowMeth(1, 1, 24, 7);
                List<string> loginText = new List<string> {"Login as", "[A] Admin", "[C] Customer", "[T] Create test data", "[E] Exit" };
                var loginWindow = new Window("Welcome to FashionCode website", 0, 10, loginText);
                loginWindow.DrawWindow();

                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case 'a':
                       await SignInSignUpHelper.AdminLogInMenu();
                        break;
                    case 'c':
                        SignInSignUpHelper.CustomerLogInMenu();
                        break;
                    case 't':
                        CreateTestDataHelper.CreateTestData();
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