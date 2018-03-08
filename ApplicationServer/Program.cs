using System;
using System.Collections.Generic;
using WindowsLibrary;

namespace ApplicationServer
{
    class Program
    {
        static FrameObject mnWnd1;
        static FrameObject mnWnd2;
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            mnWnd1 = new FrameObject(1, 1, 60, 8, "Окно 1", true);
            mnWnd2 = new FrameObject(32, 9, 30, 8, "Окно 2", false);
            
            ListObject list1 = new ListObject(mnWnd1.Left + 1, mnWnd1.Top + 1, 14, 4, true, mnWnd1.IsActive);
            ListObject list2 = new ListObject(mnWnd1.Left + 1+14, mnWnd1.Top + 1, 14, 4, false, mnWnd1.IsActive);
            ListObject list3 = new ListObject(mnWnd2.Left +1, mnWnd2.Top + 1, 8, 4, true, mnWnd2.IsActive);
            ButtonObject button1 = new ButtonObject(mnWnd1.Left + mnWnd1.Width - 10, mnWnd1.Top + mnWnd1.Height - 2, 8, 1, false, "Exit");
            list3.List.Add("Test");

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
            frames[0].Children = new List<Element>();
            frames[0].Children.Add(list1);
            frames[0].Children.Add(list2);
            frames[0].Children.Add(button1);

            list1.Update();
            list2.Update();
            button1.Update();
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
                            frames[i].UpdateChildren();
                            if ((i + 1) < size) { frames[i + 1].IsActive = true; frames[i + 1].Update(); frames[i+1].UpdateChildren(); }
                            else { frames[0].IsActive = true; frames[0].Update(); frames[0].UpdateChildren(); }
                        }

                        if (frames[i].Children.Count != 0)
                        {
                            int size_children = frames[i].Children.Count;
                            for (int j=0; j<size_children; j++)
                            {
                                if (((key.Key == ConsoleKey.UpArrow)
                                || (key.Key == ConsoleKey.DownArrow)
                                || (key.Key == ConsoleKey.Tab)
                                || (key.Key == ConsoleKey.Spacebar)) &&frames[i].Children[j].IsActive)
                                {
                                    frames[i].Children[j].ReadKey(key);
                                    if (key.Key == ConsoleKey.Tab)
                                    {
                                        if ((j + 1) < frames[i].Children.Count)
                                        {
                                            frames[i].Children[j + 1].IsActive = true;                                            
                                            frames[i].UpdateChildren();
                                            break;
                                        }
                                        else { frames[i].Children[0].IsActive = true; frames[i].UpdateChildren(); break; }
                                    }
                                    if((key.Key==ConsoleKey.Spacebar)&&j==2)
                                    {
                                        frames[1].Children.Add(list3);
                                        frames[1].UpdateChildren();
                                        break;
                                    }
                                }


                            }
                            
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
