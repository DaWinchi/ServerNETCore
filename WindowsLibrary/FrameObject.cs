using System;

namespace WindowsLibrary
{
    public class FrameObject : Element
    {

        public string Title { get; set; }
        public FrameObject()
        {
            Left = Console.WindowWidth / 2 - Width / 2;
            Top = Console.WindowHeight / 2 - Height / 2;
            Width = 20;
            Height = 20;
            IsActive = false;
            Title = "Window1";
        }

        public FrameObject(int p_left, int p_top, int p_width, int p_height, string p_title, bool p_isactive)
        {
            Left = p_left;
            Top = p_top;
            Width = p_width;
            Height = p_height;
            IsActive = p_isactive;
            Title = p_title;
        }


        protected virtual void CreateFrame()
        {
            if (IsActive)
            {
                for (int i = 0; i < Width; i++)
                {

                    for (int j = 0; j < Height; j++)
                    {
                        Console.SetCursorPosition(Left + i, Top + j);
                        if ((i == 0) && (j == 0)) Console.Write("╔");
                        else if ((i == (Width - 1)) && (j == 0)) Console.Write("╗");
                        else if ((i == 0) && (j == (Height - 1))) Console.Write("╚");
                        else if ((i == (Width - 1)) && (j == (Height - 1))) Console.Write("╝");
                        else if ((i != 0 || i != Width - 1) && (j == 0 || j == Height - 1)) Console.Write("═");
                        else if ((i == 0 || i == Width - 1) && (j != 0 || j != Height - 1)) Console.Write("║");
                        else Console.Write(" ");
                    }
                }
            }
            else
            {
                for (int i = 0; i < Width; i++)
                {

                    for (int j = 0; j < Height; j++)
                    {
                        Console.SetCursorPosition(Left + i, Top + j);
                        if ((i == 0) && (j == 0)) Console.Write("┌");
                        else if ((i == (Width - 1)) && (j == 0)) Console.Write("┐");
                        else if ((i == 0) && (j == (Height - 1))) Console.Write("└");
                        else if ((i == (Width - 1)) && (j == (Height - 1))) Console.Write("┘");
                        else if ((i != 0 || i != Width - 1) && (j == 0 || j == Height - 1)) Console.Write("─");
                        else if ((i == 0 || i == Width - 1) && (j != 0 || j != Height - 1)) Console.Write("│");
                        else Console.Write(" ");
                    }
                }
            }
        }
        protected virtual void WriteTitle()
        {
            if (Title != null)
            {
                string bufTitle;
                if (Title.Length >= Width) bufTitle = Title.Substring(0, Width - 2);
                else bufTitle = Title;
                Console.SetCursorPosition(Left + Width / 2 - bufTitle.Length / 2, Top);
                Console.WriteLine(bufTitle);
            }
        }

        public override void ReadKey()
        {
            if (IsActive)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.Enter: { IsActive = false; Update(); break; }
                }
            }

        }

        public override void Update()
        {
            CreateFrame();
            WriteTitle();
        }
    }
}
