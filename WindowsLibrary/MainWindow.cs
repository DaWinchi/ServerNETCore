using System;

namespace WindowsLibrary
{
    public class MainWindow
    {
        private int left, top, width, height;
        private string title;
        private bool isActive;

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

        MainWindow()
        {
            left = Console.WindowWidth / 2 - width / 2;
            top = Console.WindowHeight / 2 - height / 2;
            width = 30;
            height = 30;
            isActive = false;
            title = "Window1";
        }
    }
}
