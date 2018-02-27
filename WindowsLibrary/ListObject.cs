using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
    public class ListObject
    {
        protected int left, top, width, height;
        protected bool isActive;
        public List<string> List;
        private int oldSize;
        private int activeLine;

        public int Left
        {
            get { return left; }
            set { left = value; }
        }
        public int Top
        {
            get { return top; }
            set { top = value; }
        }
        public int Width
        {
            get { return width; }
            set { width = value; }
        }
        public int Height
        {
            get { return height; }
            set { height = value; }
        }
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public ListObject(int p_left, int p_top, int p_width, int p_height, bool p_active)
        {
            left = p_left;
            top = p_top;
            width = p_width;
            height = p_height;
            List = new List<string>();
            isActive = p_active;
            if (isActive) activeLine = 0;
            oldSize = 0;
        }

        public void Update()
        {
            for (int i = 0; i < oldSize; i++)
            {
                Console.SetCursorPosition(left, top + i);
                for (int j = 0; j < width; j++)
                {
                    Console.Write(" ");
                }

            }

            int size = List.Count;


            for (int i = 0; i < size; i++)
            {
                string bufstring;
                if (List[i].Length >= width) bufstring = List[i].Substring(0, width);
                else bufstring = List[i];
                Console.SetCursorPosition(left, top + i);
                if (i == activeLine)
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


        public void WaitingPressKey()
        {
            var key = Console.ReadKey();
            if (isActive && key.Key == ConsoleKey.DownArrow) { Update(); }
        }


    }
}
