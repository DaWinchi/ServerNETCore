using System;
using System.Collections.Generic;
using WindowsLibrary;

namespace ApplicationServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            FrameObject mnWnd1 = new FrameObject(0, 0, 30, 8, "Окно 1", true);
            FrameObject mnWnd2 = new FrameObject(32, 9, 30, 8, "Окно 2", false);
            FrameObject mnWnd3 = new FrameObject(32, 0, 30, 8, "Окно 3", false);
            ListObject list1 = new ListObject(mnWnd1.Left + 1, mnWnd1.Top + 1, 14, 4, true, mnWnd1.IsActive);
            ListObject list2 = new ListObject(mnWnd1.Left + 1+14, mnWnd1.Top + 1, 28, 4, false, mnWnd1.IsActive);

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


            List<Element> frames = new List<Element>();
            frames.Add(mnWnd1); frames[0].Update();
            frames.Add(mnWnd2); frames[1].Update();
            frames.Add(mnWnd3); frames[2].Update();

            list1.Update();
            list2.Update();
            int size = frames.Count;
            while (true)
            {
                for (int i = 0; i < size; i++)
                {
                    if (frames[i].IsActive)
                    {
                        var key = Console.ReadKey();
                        if (key.Key == ConsoleKey.Enter)
                        {
                            frames[i].ReadKey(key);
                            list1.Update();
                            list1.IsParentActive = frames[i].IsActive;
                            if ((i + 1) < size) { frames[i + 1].IsActive = true; frames[i + 1].Update(); list1.Update(); }
                            else { frames[0].IsActive = true; frames[0].Update(); list1.Update(); }
                        }
                        if (((key.Key == ConsoleKey.UpArrow) 
                            || (key.Key == ConsoleKey.DownArrow) 
                            || (key.Key == ConsoleKey.Tab))
                            &&list1.IsParentActive)
                        {
                            list1.ReadKey(key);
                        }
                    }
                }
            }




            Console.SetCursorPosition(Console.WindowWidth - 1, Console.WindowHeight);
            Console.CursorVisible = false;
            Console.ReadKey();
        }
    }
}
