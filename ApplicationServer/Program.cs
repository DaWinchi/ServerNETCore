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
            msgWnd.IsActive = false;
            mnWnd.IsActive = true;
            mnWnd.Update();
            msgWnd.Update();

            ListObject list = new ListObject(msgWnd.Left+2, msgWnd.Top+1, msgWnd.Width - 4, msgWnd.Height - 2, true);
            list.List.Add("Строка12345678910111213141516171819202122232425");
            list.List.Add("Строка 2");
            list.Update();
            list.List.Clear();
            list.WaitingPressKey();
           // list.Update();



            Console.SetCursorPosition(Console.WindowWidth - 1, Console.WindowHeight);
            Console.CursorVisible = false;
            Console.ReadKey();
        }
    }
}
