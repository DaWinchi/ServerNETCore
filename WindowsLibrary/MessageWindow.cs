using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
    class MessageWindow : MainWindow
    {
        MessageWindow(int p_left, int p_top, int p_width, int p_height)
        {
            left = p_left;
            top = p_top;
            width = p_width;
            height = p_height;
            isActive = true;
            title = "MessageWindow";
        }
        protected override void CreateFrame()
        {
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

    }
}
}
