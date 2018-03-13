using System;
using System.Collections.Generic;
using WindowsLibrary;

namespace ApplicationServer
{
    class Program
    {
        
        static Window mnWnd1;
       
        static void Main(string[] args)
        {
            Console.Clear();
            Console.CursorVisible = false;
            mnWnd1 = new Window(2, 1, 60, 8, "Окно 1", true);
           
            
            ListObject list1 = new ListObject(mnWnd1.Left + 1, mnWnd1.Top + 1, 14, 4, true, mnWnd1.IsActive);
            ListObject list2 = new ListObject(mnWnd1.Left + 1+14, mnWnd1.Top + 1, 14, 4, false, mnWnd1.IsActive);
           
            ButtonObject button1 = new ButtonObject(mnWnd1.Left + mnWnd1.Width - 10, mnWnd1.Top + mnWnd1.Height - 2, 8, 1, false, "Exit");
        
           

            list2.List.Add("Строка1");
            list2.List.Add("Строка2");
            list2.List.Add("Строка3");
            list2.List.Add("Строка4");            
            list2.List.Add("Строка5");

            list1.List.Add("Строка1");
            list1.List.Add("Строка2");
            list1.List.Add("Строка3");
            list1.List.Add("Строка4");
            list1.List.Add("Строка5");

            
          
            Console.SetCursorPosition(Console.WindowWidth - 1, Console.WindowHeight);
            Console.CursorVisible = false;
            Console.ReadKey();
        }

        
    }
}
