using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
    class ButtonObject
    {
        private int left, top, width, height;
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
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
    }
}
