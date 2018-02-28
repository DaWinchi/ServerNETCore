using System;
using WindowsLibrary;

namespace ApplicationServer
{
    class Program
    {
        static void Main(string[] args)
        {
            MainWindow mnWnd = new MainWindow();
            MessageWindow msgWnd = new MessageWindow(5, 3, 40, 10);
            msgWnd.IsActive = true;
            mnWnd.IsActive = false;
            mnWnd.Update();
            msgWnd.Update();

            ListObject list = new ListObject(msgWnd.Left+2, msgWnd.Top+1, msgWnd.Width - 4, msgWnd.Height - 2, msgWnd.IsActive);
            list.List.Add("Строка 1");
            list.List.Add("Строка 2");
            list.Update();
            ButtonObject button = new ButtonObject(msgWnd.Left + msgWnd.Width / 2-5, msgWnd.Top + msgWnd.Height - 2, 10, 1, false, "Exit");
            button.Update();
            
           // list.List.Clear();
           while(true)
            list.WaitingPressKey();
           // list.Update();
          


            Console.SetCursorPosition(Console.WindowWidth - 1, Console.WindowHeight);
            Console.CursorVisible = false;
            Console.ReadKey();
        }
    }
}
