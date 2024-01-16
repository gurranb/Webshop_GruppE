using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop_GruppE
{
    internal class LogoWindow
    {
        public static void LogoWindowMeth(int left, int top, int width, int height)
        {
            
            Console.SetCursorPosition(left - 1, top - 1);
            Console.Write('┌');
            for (int i = 0; i < width; i++)
            {
                Console.Write('─');
            }
            Console.WriteLine('┐');
            for (int i = 0; i <= height; i++)
            {
                Console.SetCursorPosition(left - 1, top + i);
                Console.WriteLine('│');
            }
            for (int i = 0; i <= height; i++)
            {
                Console.SetCursorPosition(left + width, top + i);
                Console.WriteLine('│');
            }
            Console.SetCursorPosition(left - 1, top + height);
            Console.Write('└');
            for (int i = 0; i < width; i++)
            {
                Console.Write('─');
            }
            Console.WriteLine('┘');
            LogoMeth(left + 1, top + 1);
        }
        public static void LogoMeth(int left, int top)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(left + 1, top);
            Console.WriteLine("          ");
            Console.SetCursorPosition(left + 1, top + 1);
            Console.WriteLine("   ");
            Console.SetCursorPosition(left + 1, top + 2);
            Console.WriteLine("       ");
            Console.SetCursorPosition(left + 1, top + 3);
            Console.WriteLine("   ");
            Console.SetCursorPosition(left + 1, top + 4);
            Console.WriteLine("   ");
            Console.SetCursorPosition(left + 10, top + 2);
            Console.WriteLine("       ");    
            Console.SetCursorPosition(left + 10, top + 3);
            Console.WriteLine("   ");        
            Console.SetCursorPosition(left + 10, top + 4);
            Console.WriteLine("       ");
            Console.BackgroundColor = 0;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(left + 12, top);
            Console.ForegroundColor = 0;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Write("F");
            Console.BackgroundColor = 0;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("ASHION");
            Console.SetCursorPosition(left + 18, top + 4);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("COD");
            Console.ForegroundColor = 0;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write("E");
            Console.BackgroundColor = 0;
            Console.BackgroundColor = 0;
        
        }

    }
}
