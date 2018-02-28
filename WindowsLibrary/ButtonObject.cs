using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
    public class ButtonObject
    {
        private int left, top, width, height;
        private bool isActive;
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
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

        public ButtonObject(int p_left, int p_top, int p_width, int p_height, bool p_active, string p_title)
        {

            left = p_left;
            top = p_top;
            width = p_width;
            height = p_height;
            isActive = p_active;
            title = p_title;

        }

        public void Update()
        {
            if (isActive)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        Console.SetCursorPosition(left + i, top + j);
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write(" ");
                    }
                }

                string bufstring;
                if (title.Length >= width) bufstring = title.Substring(0, width);
                else bufstring = title;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(left + width / 2 - bufstring.Length / 2, top + height / 2);
                Console.WriteLine(bufstring);
                Console.ResetColor();
            }
            else
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        Console.SetCursorPosition(left + i, top + j);
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write(" ");
                    }
                }

                string bufstring;
                if (title.Length >= width) bufstring = title.Substring(0, width);
                else bufstring = title;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(left + width / 2 - bufstring.Length / 2, top + height / 2);
                Console.WriteLine(bufstring);
                Console.ResetColor();
            }
        }

    }
}
