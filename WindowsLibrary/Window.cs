using System;
using System.Collections.Generic;
using System.Threading;

namespace WindowsLibrary
{
    public class Window : Element
    {
        public event EventHandler ChangeActive;
        public override bool IsActive
        {
            get => base.IsActive;
            set
            {
                
                base.IsActive = value;
                ChangeActive(this, new EventArgs());
            }
        }
        public List<Element> Children;
        

        public Window()
        {
            Children = new List<Element>();
           ChangeActive += Window_ChangeActive;
            Left = Console.WindowWidth / 2 - Width / 2;
            Top = Console.WindowHeight / 2 - Height / 2;
            Width = 20;
            Height = 20;
            IsActive = false;
            Title = "Window1";

        }

        public Window(int p_left, int p_top, int p_width, int p_height, string p_title, bool p_isactive)
        {
            Children = new List<Element>();
            ChangeActive += Window_ChangeActive;
            Left = p_left;
            Top = p_top;
            Width = p_width;
            Height = p_height;
            IsActive = p_isactive;
            Title = p_title;

        }

        public void InitializeWindow()
        {

            Update();
            UpdateChildren();

        }

        private void Window_ChangeActive(object sender, EventArgs e)
        {
            foreach (Element child in Children) child.IsParentActive = IsActive;
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

        public override void ReadKey(ConsoleKey key)
        {
            if (IsActive)
            {
                switch (key)
                {
                    case ConsoleKey.Enter: { IsActive = false; Update(); break; }
                }
            }

        }

        public override void Update()
        {
            CreateFrame();
            WriteTitle();
            UpdateChildren();
        }

        public override void UpdateChildren()
        {
            int size = Children.Count;
            for (int i = 0; i < size; i++)
            {
                Children[i].Update();
            }
        }

        public override void AddChildren(Element p_element)
        {
            Children.Add(p_element);
        }

        public void DestroyWindow()
        {
            Children.Clear();

            for (int i = 0; i < Width; i++)
            {

                for (int j = 0; j < Height; j++)
                {
                    Console.SetCursorPosition(Left + i, Top + j);
                    Console.Write(" ");
                }
            }

        }


    }
}
