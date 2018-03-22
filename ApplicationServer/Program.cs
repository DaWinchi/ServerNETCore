using System;
using System.Collections.Generic;
using WindowsLibrary;

namespace ApplicationServer
{
    
    class Program
    {
        static int a = 1;
        static Window mnWnd1;
        static Window mnWnd2;
        static Window mnWnd3;
        static Application app;
        static ProgressObject progress1;

        static void Main(string[] args)
        {
            app = new Application();
            Console.Clear();
            Console.CursorVisible = false;
            mnWnd1 = new Window(2, 1, 30, 8, "Окно 1", true, 0, ref app);
            mnWnd2 = new Window(50, 1, 50, 8, "Тест таймера", false, 1, ref app);
            mnWnd3 = new Window(30, 10, 30, 8, "Окно 3", false, 2, ref app);

           
            ListObject list1 = new ListObject(mnWnd1.Left + 1, mnWnd1.Top + 1, 14, 2, true, mnWnd1.IsActive);
            list1.ButtonClicked += List1_ButtonClicked;
            ListObject list2 = new ListObject(mnWnd3.Left + 1, mnWnd3.Top + 1, 14, 2, true, false);
            list2.ButtonClicked += List2_ButtonClicked;
            ButtonObject button1 = new ButtonObject(mnWnd1.Left + mnWnd1.Width - 10, mnWnd1.Top + mnWnd1.Height - 2, 8, 1, false, true, "Exit");
            ButtonObject button2 = new ButtonObject(mnWnd2.Left + mnWnd2.Width/2 - 4, mnWnd2.Top +2, 8, 1, true, false, "Пуск");
            //ButtonObject button3 = new ButtonObject(mnWnd2.Left + mnWnd2.Width / 2 - 4, mnWnd2.Top + 2, 8, 1, true, "Пуск");

            progress1 = new ProgressObject(mnWnd2.Left + 1, mnWnd2.Top + mnWnd2.Height - 2, mnWnd2.Width - 2, 1)
            {
                Percent = 78
            };

            button1.ButtonClicked += Button1_ButtonClicked;
            button2.ButtonClicked += Button2_ButtonClicked;
            
            list1.List.Add("Открыть окно 2");
            list1.List.Add("Открыть окно 3");
           

            list2.List.Add("Закрыть окно 2");
            list2.List.Add("Закрыть");


            mnWnd1.AddChildren(list1);
            mnWnd1.AddChildren(button1);
            mnWnd2.AddChildren(progress1);
            mnWnd2.AddChildren(button2);
            mnWnd3.AddChildren(list2);

            
            app.windows.Add(mnWnd1);
            app.Run();
            

        }

        private static void Button2_ButtonClicked(object sender, EventArgs e)
        {
            foreach (Window win in app.windows) if (win.IdentificationNumber == 1)
                {
                    //win.TimerTick += Test_TimerTick;
                    //win.timer = new System.Threading.Timer(Test_TimerTick, null, 0, 2000);
                }
            
        }

        private static void Test_TimerTick(object state)
        {
            foreach (Window win in app.windows) if (win.IdentificationNumber == 1)
                {
                    if (a < 100) progress1.Percent = a++;
                    else progress1.Percent = 0;
                    progress1.Update();
                }
        }

        private static void MnWnd1_TimerTick(object state)
        {
           
        }

        private static void List2_ButtonClicked(object sender, EventArgs e)
        {
            if (((ListObject)sender).ActiveLine == 0)
            {
                foreach (Window win in app.windows) if (win.IdentificationNumber == 1) win.CloseWindow();

            }

            if (((ListObject)sender).ActiveLine == 1)
            {
                foreach (Window win in app.windows) if (win.IdentificationNumber == 2) win.CloseWindow();

            }
        }

        private static void List1_ButtonClicked(object sender, EventArgs e)
        {
            if(((ListObject)sender).ActiveLine==0)
            {
                mnWnd2.Update();
                app.AddWindow(mnWnd2);
                
            }

            if (((ListObject)sender).ActiveLine == 1)
            {
                mnWnd3.Update();
                app.AddWindow(mnWnd3);

            }
        }

        private static void Button1_ButtonClicked(object sender, EventArgs e)
        {
            
            app.windows[0].CloseWindow();

        }
    }

    
}
