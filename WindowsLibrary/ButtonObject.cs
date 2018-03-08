using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
    public class ButtonObject : Element
    {
        public bool IsClicked{ get; set; }
        public ButtonObject(int p_Left, int p_Top, int p_Width, int p_Height, bool p_active, string p_Title)
        {
            Left = p_Left;
            Top = p_Top;
            Width = p_Width;
            Height = p_Height;
            IsActive = p_active;
            Title = p_Title;
            IsClicked = false;

        }

        public override void Update()
        {
            if (IsActive)
            {
                for (int i = 0; i < Width; i++)
                {
                    for (int j = 0; j < Height; j++)
                    {
                        Console.SetCursorPosition(Left + i, Top + j);
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write(" ");
                    }
                }

                string bufstring;
                if (Title.Length >= Width) bufstring = Title.Substring(0, Width);
                else bufstring = Title;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(Left + Width / 2 - bufstring.Length / 2, Top + Height / 2);
                Console.WriteLine(bufstring);
                Console.ResetColor();
            }
            else
            {
                for (int i = 0; i < Width; i++)
                {
                    for (int j = 0; j < Height; j++)
                    {
                        Console.SetCursorPosition(Left + i, Top + j);
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write(" ");
                    }
                }

                string bufstring;
                if (Title.Length >= Width) bufstring = Title.Substring(0, Width);
                else bufstring = Title;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(Left + Width / 2 - bufstring.Length / 2, Top + Height / 2);
                Console.WriteLine(bufstring);
                Console.ResetColor();
            }
        }

        public override void ReadKey(ConsoleKeyInfo keyInfo)
        {
            if (IsActive)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.Tab: IsActive = false; Update(); break;
                    case ConsoleKey.Select: IsActive = false; Update(); break;
                    default: break;
                }
            }
        }
        public override void UpdateChildren()
        {
            throw new NotImplementedException();
        }



    }
}
