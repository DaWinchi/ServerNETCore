using System;
using System.Collections.Generic;
using WindowsLibrary;

namespace ApplicationServer
{
    class Program
    {
        static Window mnWnd1;
        static Window mnWnd2;
        static Window mnWnd3;
        static Application app;

        static void Main(string[] args)
        {
            Console.Clear();
            Console.CursorVisible = false;
            mnWnd1 = new Window(2, 1, 30, 8, "Окно 1", true);
            mnWnd2 = new Window(50, 1, 50, 8, "Окно 2", false);
            mnWnd3 = new Window(30, 10, 30, 8, "Окно 3", false);


            ListObject list1 = new ListObject(mnWnd1.Left + 1, mnWnd1.Top + 1, 14, 2, true, mnWnd1.IsActive);
            list1.ButtonClicked += List1_ButtonClicked;
            ListObject list2 = new ListObject(mnWnd3.Left + 1, mnWnd3.Top + 1, 14, 2, true, false);
            list2.ButtonClicked += List2_ButtonClicked;
            ButtonObject button1 = new ButtonObject(mnWnd1.Left + mnWnd1.Width - 10, mnWnd1.Top + mnWnd1.Height - 2, 8, 1, false, "Exit");

            ProgressObject progress1 = new ProgressObject(mnWnd2.Left + 1, mnWnd2.Top + mnWnd2.Height - 2, mnWnd2.Width - 2, 1);
            progress1.Percent = 78;

            button1.ButtonClicked += Button1_ButtonClicked;


            
            list1.List.Add("Открыть окно 2");
            list1.List.Add("Открыть окно 3");
           

            list2.List.Add("Закрыть окно 2");
            list2.List.Add("Закрыть");


            mnWnd1.AddChildren(list1);
            mnWnd1.AddChildren(button1);
            mnWnd2.AddChildren(progress1);
            mnWnd3.AddChildren(list2);

            app = new Application();
            app.windows.Add(mnWnd1);           
            app.Run();

        }

        private static void List2_ButtonClicked(object sender, EventArgs e)
        {
            if (((ListObject)sender).ActiveLine == 0)
            {
                
                

            }

            if (((ListObject)sender).ActiveLine == 1)
            {
                mnWnd3.InitializeWindow();
                app.AddWindow(mnWnd3);

            }
        }

        private static void List1_ButtonClicked(object sender, EventArgs e)
        {
            if(((ListObject)sender).ActiveLine==0)
            {
                mnWnd2.InitializeWindow();
                app.AddWindow(mnWnd2);
                
            }

            if (((ListObject)sender).ActiveLine == 1)
            {
                mnWnd3.InitializeWindow();
                app.AddWindow(mnWnd3);

            }
        }

        private static void Button1_ButtonClicked(object sender, EventArgs e)
        {
            app.windows[0].CloseWindow();

        }
    }

    
}
