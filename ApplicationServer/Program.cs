using System;
using System.Collections.Generic;
using WindowsLibrary;

namespace ApplicationServer
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.Clear();
            Console.CursorVisible = false;
            Window mnWnd1 = new Window(2, 1, 60, 8, "Окно 1", true);
            Window mnWnd2 = new Window(2, 11, 60, 8, "Окно 2", false);
            Window mnWnd3 = new Window(6, 5, 60, 8, "Окно 3", false);


            ListObject list1 = new ListObject(mnWnd1.Left + 1, mnWnd1.Top + 1, 14, 4, true, mnWnd1.IsActive);
            ListObject list2 = new ListObject(mnWnd1.Left + 1+14, mnWnd1.Top + 1, 14, 4, false, mnWnd1.IsActive);
            ListObject list3 = new ListObject(mnWnd2.Left + 1, mnWnd2.Top + 1, 14, 4, true, mnWnd2.IsActive);
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

            list3.List.Add("Строка1");
            list3.List.Add("Строка2");
            list3.List.Add("Строка3");
            list3.List.Add("Строка4");
            list3.List.Add("Строка5");


            mnWnd1.AddChildren(list1);
            mnWnd1.AddChildren(button1);
            mnWnd1.AddChildren(list2);
            
            mnWnd2.AddChildren(list3);

            Application app = new Application();
            app.windows.Add(mnWnd1);
            app.windows.Add(mnWnd2);
            app.windows.Add(mnWnd3);
            app.Run();
            
        }

        
    }
}
