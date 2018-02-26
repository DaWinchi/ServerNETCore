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
            Console.SetCursorPosition(Console.WindowWidth - 1, Console.WindowHeight);
            Console.CursorVisible = false;
            Console.ReadKey();
        }
    }
}
