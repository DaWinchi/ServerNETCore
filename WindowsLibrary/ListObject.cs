using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
    public class ListObject:Element
    {
        public List<string> List;
        private int oldSize;
        private int activeLine;

       
        public ListObject(int p_Left, int p_Top, int p_Width, int p_Height, bool p_active, bool p_parentActive)
        {
            Left = p_Left;
            Top = p_Top;
            Width = p_Width;
            Height = p_Height;
            List = new List<string>();
            IsActive = p_active;
            IsParentActive = p_parentActive;

            if (IsActive) activeLine = 0;
            oldSize = 0;
        }

        public override void Update()
        {
            for (int i = 0; i < oldSize; i++)
            {
                Console.SetCursorPosition(Left, Top + i);
                for (int j = 0; j < Width; j++)
                {
                    Console.Write(" ");
                }
            }

            int size = List.Count;

            for (int i = 0; i < size; i++)
            {
                string bufstring;
                if (List[i].Length >= Width) bufstring = List[i].Substring(0, Width);
                else bufstring = List[i];
                Console.SetCursorPosition(Left, Top + i);
                if ((i == activeLine)&&(IsActive))
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine(bufstring);
                    Console.ResetColor();
                }
                else Console.WriteLine(bufstring);
            }
            oldSize = size;
        }


        public override void ReadKey(ConsoleKeyInfo keyInfo)
        {
            if (IsActive)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow: activeLine++; Update(); break;
                    case ConsoleKey.UpArrow: activeLine--; Update(); break;
                    case ConsoleKey.Tab: IsActive = false; break;
                    default: break;
                }
            }
        }


    }
}
