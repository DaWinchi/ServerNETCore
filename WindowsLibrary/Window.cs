using System;
using System.Collections.Generic;
using System.Threading;

namespace WindowsLibrary
{
    public class Window : Element
    {
        Thread tracking;
        static object locker = new object();
        static Queue<Message> queue_messages;

        public Window()
        {
            Left = Console.WindowWidth / 2 - Width / 2;
            Top = Console.WindowHeight / 2 - Height / 2;
            Width = 20;
            Height = 20;
            IsActive = false;
            Title = "Window1";
            Children = new List<Element>();
            queue_messages = new Queue<Message>();
        }

        public Window(int p_left, int p_top, int p_width, int p_height, string p_title, bool p_isactive)
        {
            Left = p_left;
            Top = p_top;
            Width = p_width;
            Height = p_height;
            IsActive = p_isactive;
            Title = p_title;
            Children = new List<Element>();
            queue_messages = new Queue<Message>();
        }

        public void InitializeWindow()
        {


        }


        static void InitializeTracking()
        {
            ConsoleKeyInfo pressed_key;
            while (true)
            {
                pressed_key = Console.ReadKey();
                Message.KeyPressed msg;


                switch (pressed_key.Key)
                {
                    case ConsoleKey.Enter: msg = Message.KeyPressed.Enter; break;
                    case ConsoleKey.Spacebar: msg = Message.KeyPressed.Space; break;
                    case ConsoleKey.UpArrow: msg = Message.KeyPressed.Up; break;
                    case ConsoleKey.DownArrow: msg = Message.KeyPressed.Down; break;
                    default: break;
                }


                msg = Message.KeyPressed.Enter;
                pressed_key = Console.ReadKey();
                lock (locker)
                {

                    queue_messages.Enqueue
                }

            }
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

        public override void ReadKey(ConsoleKeyInfo keyInfo)
        {
            if (IsActive)
            {
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

        public override void UpdateChildren()
        {
            int size = Children.Count;
            for (int i = 0; i < size; i++)
            {
                Children[i].IsParentActive = IsActive;
                Children[i].Update();
            }
        }
    }
}
