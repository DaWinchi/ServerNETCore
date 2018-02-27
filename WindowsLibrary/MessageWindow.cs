using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
    public class MessageWindow : MainWindow
    {
        public MessageWindow(int p_left, int p_top, int p_width, int p_height)
        {
            left = p_left;
            top = p_top;
            width = p_width;
            height = p_height;
            isActive = true;
            title = "MessageWindow";
        }
       
    }
}

