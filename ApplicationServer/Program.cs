using System;
using System.Collections.Generic;
using WindowsLibrary;

namespace ApplicationServer
{
    class Program
    {
        static void Main(string[] args)
        {
            FrameObject mnWnd1 = new FrameObject(0, 0, 30, 8, "Окно 1", true);
            FrameObject mnWnd2 = new FrameObject(32, 9, 30, 8, "Окно 2", false);

            List<Element> frames = new List<Element>();
            frames.Add(mnWnd1); frames[0].Update();
            frames.Add(mnWnd2); frames[1].Update();
            int size = frames.Count;
            while (true)
            {
               for (int i=0; i<size; i++)
                {
                    if (frames[i].IsActive)
                    {
                        frames[i].ReadKey();
                        if ((i + 1) < size) { frames[i + 1].IsActive = true; frames[i + 1].Update(); }
                        else { frames[0].IsActive = true; frames[0].Update(); }
                    }
                }
            }
           
          


            Console.SetCursorPosition(Console.WindowWidth - 1, Console.WindowHeight);
            Console.CursorVisible = false;
            Console.ReadKey();
        }
    }
}
