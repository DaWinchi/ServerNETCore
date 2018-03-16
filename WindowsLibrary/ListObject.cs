﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
    public class ListObject:Element
    {
        public List<string> List;
        private int oldSize;
        public int ActiveLine { get; set; }
        public event EventHandler ButtonClicked;
       
        public ListObject(int p_Left, int p_Top, int p_Width, int p_Height, bool p_active, bool p_parentActive)
        {
            ButtonClicked += ListObject_ButtonClicked;
            Left = p_Left;
            Top = p_Top;
            Width = p_Width;
            Height = p_Height;
            List = new List<string>();
            IsActive = p_active;
            IsParentActive = p_parentActive;
            IsClicked = false;
            if (IsActive) ActiveLine = 0;
            oldSize = 0;
        }

        private void ListObject_ButtonClicked(object sender, EventArgs e)
        {
           
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
                if ((i == ActiveLine)&&(IsActive)&&(IsParentActive))
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine(bufstring);
                    Console.ResetColor();
                }
                else Console.WriteLine(bufstring);
            }
            oldSize = size;
            Console.SetCursorPosition(Console.WindowWidth - 2, Console.WindowHeight - 2);
        }


        public override void ReadKey(ConsoleKey key)
        {
            if (IsActive)
            {
                switch (key)
                {
                    case ConsoleKey.DownArrow: if(ActiveLine<List.Count-1) ActiveLine++; Update(); break;
                    case ConsoleKey.UpArrow: if(ActiveLine>=1)ActiveLine--; Update(); break;
                    case ConsoleKey.Tab: IsActive = false; Update();  break;
                    case ConsoleKey.Spacebar:
                        IsClicked = true;
                        ButtonClicked(this, new EventArgs());
                        Update(); break;
                    default: break;
                }
            }
        }

        public override void UpdateChildren()
        {
           
        }

        public override void AddChildren(Element p_element)
        {
            
        }

    }

    
}
