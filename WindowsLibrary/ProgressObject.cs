using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
   /// <summary>
   /// Класс прогресс-бара
   /// </summary>
    public class ProgressObject : Element
    {
        /// <summary>
        /// Задаёт или получает процент
        /// </summary>
        public int Percent { get; set; }
        /// <summary>
        /// Задаёт или получает цвет за текстом
        /// </summary>
        public ConsoleColor BackgroundTextColor { get; set; }
        /// <summary>
        /// Задаёт или получает цвет текста
        /// </summary>
        public ConsoleColor PercentColor { get; set; }

        /// <summary>
        /// Задаёт или получает минимум
        /// </summary>
        public string Min { get; set; }
        /// <summary>
        /// Задаёт или получает максимум
        /// </summary>
        public string Max { get; set; }
        /// <summary>
        /// Задаёт или получает значение строки процента
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Конструктор прогресс-бара
        /// </summary>
        /// <param name="p_Left">горизонтальная координата левого верхнего угла прогресс-бара</param>
        /// <param name="p_Top">горизонтальная координата левого верхнего угла прогресс-бара</param>
        /// <param name="p_Width">ширина прогресс-бара</param>
        /// <param name="p_Height">высота прогресс-бара</param>
        /// <param name="p_active">активность прогресс-бара</param>
        /// <param name="p_parentActive"> активность окна-родителя</param>
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

        /// <summary>
        /// Перерисовывает прогресс-бар
        /// </summary>
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


        /// <summary>
        ///  Обрабатывает событие нажатия клавиши клавиатуры на прогресс-баре
        /// </summary>
        /// <param name="key">  Информация о нажатой клавише  </param>
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

        /// <summary>
        /// Перерисовывает дочерние объекты (не используется) 
        /// </summary>
        internal override void ReDrawChildren()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Добавляет элемент в список дочерних объектов (не используется)
        /// </summary>
        /// <param name="p_element"> Добавляемый объект </param>
        public override void AddChildren(Element p_element)
        {
            throw new NotImplementedException();
        }
    }
}
