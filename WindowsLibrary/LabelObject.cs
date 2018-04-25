using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
    /// <summary>
    /// Класс текстовой метки
    /// </summary>
    public class LabelObject : Element
    {

        /// <summary>
        /// Задаёт или получает текст метки
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Конструктор текстовой метки
        /// </summary>
        /// <param name="p_Left">горизонтальная координата левого верхнего угла метки</param>
        /// <param name="p_Top">вертикальная координата левого верхнего угла метки</param>
        /// <param name="p_Width">ширина метки</param>
        /// <param name="p_Height">высота метки</param>
        /// <param name="p_active">активность метки</param>
        /// <param name="p_parentActive">активность окна-родителя</param>
        /// <param name="p_text">текст</param>
        public LabelObject(int p_Left, int p_Top, int p_Width, int p_Height, bool p_active, bool p_parentActive, string p_text)
        {
            Left = p_Left;
            Top = p_Top;
            Width = p_Width;
            Height = p_Height;
            Text = p_text;
            IsClicked = false;
            IsActive = p_active;
            IsParentActive = p_parentActive;
            BackgroundColor = ConsoleColor.Gray;
            TextColor= ConsoleColor.Black;
        }

       /// <summary>
       /// Перерисовывает текстовую метку
       /// </summary>
        internal override void ReDraw()
        {
            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = TextColor;
            bool endOftext = false;
            char[] text = Text.ToCharArray();
            if (text.Length < 1) { text = new char[1]; text[0] = '-'; }
            int size = text.Length;
            int counterSymbol = 0;

            for (int i=0; i<Height; i++)
            {
                for (int j=0; j<Width; j++)
                {
                    Console.SetCursorPosition(Left + j, Top + i);
                    Console.Write(" ");
                }
            }


            for (int i = 0; i < Height&&endOftext==false; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Console.SetCursorPosition(Left + j, Top + i);
                    Console.Write(text[counterSymbol]);
                    counterSymbol++;
                    if (counterSymbol == size) { endOftext = true; break; }
                }

            }

            Console.SetCursorPosition(Console.WindowWidth - 2, Console.WindowHeight - 2);

        }
        /// <summary>
        ///  Обрабатывает событие нажатия клавиши клавиатуры на метке
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
