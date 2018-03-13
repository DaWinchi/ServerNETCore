using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
   public  class ProgressObject:Element
    {
        public int Percent { get; set; }
        public override void UpdateChildren()
        {
            throw new NotImplementedException();
        }

        public ProgressObject(int p_Left, int p_Top, int p_Width, int p_Height)
        {
            Left = p_Left;
            Top = p_Top;
            Width = p_Width;
            Height = p_Height;
            Percent = 50;
            IsClicked = false;

        }

        public override void Update()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Console.SetCursorPosition(Left + i, Top + j);
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write(" ");
                }
            }

            float real_percent = (float)Width / 100 * Percent;
        
            for (int i=0; i < (int)real_percent; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Console.SetCursorPosition(Left + i, Top + j);
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write(" ");
                }
            }

            string bufstring = Title+Percent.ToString()+"%";
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(Left + Width / 2 - bufstring.Length / 2, Top + Height / 2);
            Console.WriteLine(bufstring);
            Console.ResetColor();
        }

        public override void ReadKey(ConsoleKeyInfo keyInfo)
        {
            throw new NotImplementedException();
        }
    }
}
