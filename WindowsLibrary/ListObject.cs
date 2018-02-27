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
        }

        public void CreateList()
        {
            int size = List.Count;
            for (int i=0; i<size; i++)
            {
                string bufstring;
                if (List[i].Length >= width) bufstring = List[i].Substring(0, width - 2);
                else bufstring = List[i];
                Console.SetCursorPosition(left, top+i);
                Console.WriteLine(bufstring);
            }
        }


        public void WaitingPressKey()
        {
            var key=Console.ReadKey();
            if (isActive && Console.ReadKey().Key == ConsoleKey.DownArrow) { List.Clear(); }
        }


    }
}
