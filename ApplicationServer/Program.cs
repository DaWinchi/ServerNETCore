using System;
using System.Collections.Generic;
using WindowsLibrary;

namespace ApplicationServer
{
    
    class Program
    {
       static int a = 0;
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
            mnWnd3 = new Window(30, 10, 50, 8, "Окно 3", false, 2, ref app);

           
            ListObject list1 = new ListObject(mnWnd1.Left + 1, mnWnd1.Top + 1, 14, 4, true, mnWnd1.IsActive);
            list1.ButtonClicked += List1_ButtonClicked;
            ListObject list2 = new ListObject(mnWnd3.Left + 1, mnWnd3.Top + 1, 14, 2, true, false);
            list2.ButtonClicked += List2_ButtonClicked;
            ButtonObject button1 = new ButtonObject(mnWnd1.Left + mnWnd1.Width - 10, mnWnd1.Top + mnWnd1.Height - 2, 8, 1, false, true, "Exit");
            ButtonObject button2 = new ButtonObject(mnWnd2.Left + mnWnd2.Width/2 - 4, mnWnd2.Top +2, 8, 1, true, false, "Пуск");
           // ButtonObject button3 = new ButtonObject(mnWnd2.Left + mnWnd2.Width / 2 - 4, mnWnd2.Top + 2, 8, 1, true, "Пуск");
            LabelObject label = new LabelObject(mnWnd3.Left + 30, mnWnd3.Top + 1, 19, 2, false, false, "Hello world!Hello world!Hello world!Hello world!Hello world!Hello world!Hello world!");
            progress1 = new ProgressObject(mnWnd2.Left + 1, mnWnd2.Top + mnWnd2.Height - 2, mnWnd2.Width - 2, 1, false, false)
            {
                Percent = 78
            };
            label.Text = "Это просто\n текстовая метка";
            button1.ButtonClicked += Button1_ButtonClicked;
            button2.ButtonClicked += Button2_ButtonClicked;
            
            list1.List.Add("Открыть окно 2");
            list1.List.Add("Открыть окно 3");
            list1.List.Add("Строка 3");
            list1.List.Add("Строка 4");
            list1.List.Add("Строка 5");
            list1.List.Add("Строка 6");

            list2.List.Add("Закрыть окно 2");
            list2.List.Add("Закрыть");
            list2.List.Add("Включить часы");

            

            mnWnd1.AddChildren(list1);
            mnWnd1.AddChildren(button1);
            mnWnd2.AddChildren(progress1);
            mnWnd2.AddChildren(button2);
            mnWnd3.AddChildren(list2);
            mnWnd3.AddChildren(label);

            
            app.windows.Add(mnWnd1);
            app.Run();
            

        }

        static void Setter(object o)
        {
            foreach (Window win in app.windows)
            {
                if (win.IdentificationNumber == 1)
                {
                    if (a > 100) a = 0;
                    ((ProgressObject)win.Children[0]).Percent = a++;
                    win.UpdateChildren(0);
                }

            }
        }

        static void Time(object o)
        {
            foreach (Window win in app.windows)
            {
                if (win.IdentificationNumber == 2)
                {
                    ((LabelObject)win.Children[1]).Text = DateTime.Now.ToLongTimeString();
                    win.UpdateChildren(1);
                }

            }
        }

        private static void Button2_ButtonClicked(object sender, EventArgs e)
        {
            foreach(Window win in app.windows)
            {
                if (win.IdentificationNumber == 1)
                {
                    a = 0;
                    win.Timer = new System.Threading.Timer(Setter, null, 1000, 500);
                }
                
            }
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
            if(((ListObject)sender).ActiveLine==2)
            {
                foreach (Window win in app.windows) if (win.IdentificationNumber == 2) win.Timer=
                            new System.Threading.Timer(Time, null, 1, 1000);
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
