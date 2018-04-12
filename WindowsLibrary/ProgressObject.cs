using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
    public class ProgressObject : Element
    {

        /*Целочисленный процент*/
        public int Percent { get; set; }

        public ConsoleColor BackgroundTextColor;
        public ConsoleColor PercentColor;

        public string Min { get; set; }
        public string Max { get; set; }
        public string Value { get; set; }

        /*Конструктор прогресс-бара
      @ p_Left - координата левой верхней границы  по горизонтали
      @ p_Top - координата левой верхней границы  по вертикали
      @ p_Width - ширина 
      @ p_Height - высота 
      @ p_active - флаг активности кнопки при инициализации(при большом количестве элементов в окне в положении true может быть только у одного элемента)
      @ p_parentActive - флаг активности родителя(устанавливается согласно родителю)       
      */
        public ProgressObject(int p_Left, int p_Top, int p_Width, int p_Height, bool p_active, bool p_parentActive)
        {
            Left = p_Left;
            Top = p_Top;
            Width = p_Width;
            Height = p_Height;
            Percent = 50;
            IsClicked = false;
            IsActive = p_active;
            IsParentActive = p_parentActive;
            BackgroundColor = ConsoleColor.Gray;
            TextColor = ConsoleColor.Black;
            PercentColor = ConsoleColor.DarkBlue;
            BackgroundTextColor = ConsoleColor.White;
            Min = "1";
            Max = "100";
            Value = "50";
        }

        /*Метод перерисовывает прогресс-бар и отображает процент*/
        internal override void ReDraw()
        {

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height-1; j++)
                {
                    Console.SetCursorPosition(Left + i, Top + j);
                    Console.BackgroundColor = BackgroundColor;
                    Console.Write(" ");
                }
            }
            for (int i=0; i<Width; ++i)
            {
                Console.SetCursorPosition(Left + i, Top + Height - 1);
                Console.BackgroundColor = BackgroundTextColor;
                Console.Write(" ");
            }

            float real_percent = (float)Width / 100 * Percent;

            for (int i = 0; i < (int)real_percent; i++)
            {
                for (int j = 0; j < Height - 1; j++)
                {
                    Console.SetCursorPosition(Left + i, Top + j);
                    Console.BackgroundColor = PercentColor;
                    Console.Write(" ");
                }
            }

            Console.ForegroundColor = TextColor;
            Console.BackgroundColor = BackgroundTextColor;
            Console.SetCursorPosition(Left + Width / 2 - Value.Length / 2, Top + Height-1);
            Console.WriteLine(Value);

            Console.SetCursorPosition(Left, Top + Height-1);
            Console.WriteLine(Min);

            Console.SetCursorPosition(Left + Width - Max.Length, Top + Height-1);
            Console.WriteLine(Max);

            Console.ResetColor();

            Console.SetCursorPosition(Console.WindowWidth - 2, Console.WindowHeight - 2);
        }


        /*Метод обработки нажатой на клавиатуре клавиши для данного элемента*/
        internal override void ReadKey(ConsoleKey key)
        {
            if (IsActive)
            {
                switch (key)
                {
                    case ConsoleKey.Tab: IsActive = false; break;
                    default: break;
                }
            }
        }

        /*Метод перерисовки дочерних элементов(не используется)*/
        internal override void ReDrawChildren()
        {
            throw new NotImplementedException();
        }

        /*Метод добавления дочерних элементов(не используется)*/
        public override void AddChildren(Element p_element)
        {
            throw new NotImplementedException();
        }
    }
}
