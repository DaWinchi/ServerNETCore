using System;

namespace WindowsLibrary
{
    public class MainWindow
    {
        protected int left, top, width, height;
        protected string title;
        protected bool isActive;

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
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public MainWindow()
        {
            left = Console.WindowWidth / 2 - width / 2;
            top = Console.WindowHeight / 2 - height / 2;
            width = 20;
            height = 20;
            isActive = false;
            title = "Window1";
        }

        protected virtual void CreateFrame() {
            if (isActive)
            {
                for (int i = 0; i < width; i++)
                {

                    for (int j = 0; j < height; j++)
                    {
                        Console.SetCursorPosition(left + i, top + j);
                        if ((i == 0) && (j == 0)) Console.Write("╔");
                        else if ((i == (width - 1)) && (j == 0)) Console.Write("╗");
                        else if ((i == 0) && (j == (height - 1))) Console.Write("╚");
                        else if ((i == (width - 1)) && (j == (height - 1))) Console.Write("╝");
                        else if ((i != 0 || i != width - 1) && (j == 0 || j == height - 1)) Console.Write("═");
                        else if ((i == 0 || i == width - 1) && (j != 0 || j != height - 1)) Console.Write("║");
                        else Console.Write(" ");
                    }
                }
            }
            else
            {
                for (int i = 0; i < width; i++)
                {

                    for (int j = 0; j < height; j++)
                    {
                        Console.SetCursorPosition(left + i, top + j);
                        if ((i == 0) && (j == 0)) Console.Write("┌");
                        else if ((i == (width - 1)) && (j == 0)) Console.Write("┐");
                        else if ((i == 0) && (j == (height - 1))) Console.Write("└");
                        else if ((i == (width - 1)) && (j == (height - 1))) Console.Write("┘");
                        else if ((i != 0 || i != width - 1) && (j == 0 || j == height - 1)) Console.Write("─");
                        else if ((i == 0 || i == width - 1) && (j != 0 || j != height - 1)) Console.Write("│");
                        else Console.Write(" ");
                    }
                }
            }
        }
        protected virtual void WriteTitle() 
        {
            string bufTitle;
            if (title.Length >= width) bufTitle = title.Substring(0, width - 2);
            else bufTitle = title;
            Console.SetCursorPosition(left + width / 2 - bufTitle.Length / 2, top);
            Console.WriteLine(bufTitle);
        }

        public virtual void Update()
        {
            CreateFrame();
            WriteTitle();
        }
    }
}
