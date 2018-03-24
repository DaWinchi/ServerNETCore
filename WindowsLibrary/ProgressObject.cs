using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
   public  class ProgressObject:Element
    {

        /*Целочисленный процент*/
        public int Percent { get; set; }


        /*Конструктор прогресс-бара
      @ p_Left - координата левой верхней границы  по горизонтали
      @ p_Top - координата левой верхней границы  по вертикали
      @ p_Width - ширина 
      @ p_Height - высота 
      @ p_active - флаг активности кнопки при инициализации(при большом количестве элементов в окне в положении true может быть только у одного элемента)
      @ p_parentActive - флаг активности родителя(устанавливается согласно родителю)       
      */
        public ProgressObject(int p_Left, int p_Top, int p_Width, int p_Height, bool p_active, bool p_parentActive )
        {
            Left = p_Left;
            Top = p_Top;
            Width = p_Width;
            Height = p_Height;
            Percent = 50;
            IsClicked = false;
            IsActive = p_active;
            IsParentActive = p_parentActive;
        }

        /*Метод обновляет прогресс-бар и отображает процент*/
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

            Console.SetCursorPosition(Console.WindowWidth - 2, Console.WindowHeight - 2);
        }


        /*Метод обработки нажатой на клавиатуре клавиши для данного элемента*/
        public override void ReadKey(ConsoleKey key)
        {
            if (IsActive)
            {
                switch (key)
                {
                    case ConsoleKey.Tab: IsActive = false; Update(); break;                    
                    default: break;
                }
            }
        }

        /*Метод обновления дочерних элементов(не используется)*/
        public override void UpdateChildren()
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
