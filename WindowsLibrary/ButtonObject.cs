using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
    /// <summary>
    /// Класс кнопки
    /// </summary>
    public class ButtonObject : Element
    {

        /// <summary>
        /// Событие нажатия кнопки
        /// </summary>
        public event EventHandler ButtonClicked;
        /// <summary>
        /// Задаёт или получает цвет текста на активной кнопке
        /// </summary>
        public ConsoleColor TextActiveColor { get; set; }
        /// <summary>
        /// Задаёт или получает цвет фона на активной кнопке
        /// </summary>
        public ConsoleColor BackgroundActiveColor { get; set; }
        /// <summary>
        /// Конструктор кнопки
        /// </summary>
        /// <param name="p_Left">горизонтальная координата левого верхнего угла кнопки</param>
        /// <param name="p_Top">вертикальная координата левого верхнего угла окна </param>
        /// <param name="p_Width">ширина кнопки</param>
        /// <param name="p_Height">высота кнопки</param>
        /// <param name="p_active">активность кнопки</param>
        /// <param name="p_parentActive">акиовность окна-родителя</param>
        /// <param name="p_Title">заголовок</param>
        public ButtonObject(int p_Left, int p_Top, int p_Width, int p_Height, bool p_active, bool p_parentActive, string p_Title)
        {
            ButtonClicked += ButtonObject_ButtonClicked;
            Left = p_Left;
            Top = p_Top;
            Width = p_Width;
            Height = p_Height;
            IsActive = p_active;
            Title = p_Title;
            IsParentActive = p_parentActive;
            IsClicked = false;
            BackgroundColor = ConsoleColor.White;
            TextColor = ConsoleColor.Black;
            BackgroundActiveColor = ConsoleColor.White;
            TextActiveColor = ConsoleColor.Black;
        }

       /// <summary>
       /// Пустой обработчик события клика
       /// </summary>
       /// <param name="sender">объект, вызвавший событие</param>
       /// <param name="e">набор аргументов</param>
        protected void ButtonObject_ButtonClicked(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Перерисовывает кнопку и отображает заголовок
        /// </summary>
        internal override void ReDraw()
        {
            if (IsActive&&IsActive&&IsParentActive)
            {
                Console.BackgroundColor = BackgroundActiveColor;
                Console.ForegroundColor = TextActiveColor;
                for (int i = 0; i < Width; i++)
                {
                    for (int j = 0; j < Height; j++)
                    {
                        Console.SetCursorPosition(Left + i, Top + j);
                        Console.Write(" ");
                    }
                }

                string bufstring;
                if (Title.Length >= Width) bufstring = Title.Substring(0, Width);
                else bufstring = Title;
                Console.SetCursorPosition(Left + Width / 2 - bufstring.Length / 2, Top + Height / 2);
                Console.WriteLine(bufstring);
                Console.ResetColor();
            }
            else
            {
                Console.BackgroundColor = BackgroundColor;
                Console.ForegroundColor = TextColor;
                for (int i = 0; i < Width; i++)
                {
                    for (int j = 0; j < Height; j++)
                    {
                        Console.SetCursorPosition(Left + i, Top + j);
                        Console.Write(" ");
                    }
                }

                string bufstring;
                if (Title.Length >= Width) bufstring = Title.Substring(0, Width);
                else bufstring = Title;
                Console.ForegroundColor = TextColor;
                Console.SetCursorPosition(Left + Width / 2 - bufstring.Length / 2, Top + Height / 2);
                Console.WriteLine(bufstring);
                Console.ResetColor();
            }

            Console.SetCursorPosition(Console.WindowWidth - 2, Console.WindowHeight - 2);
        }
        /// <summary>
        ///  Обрабатывает событие нажатия клавиши клавиатуры на кнопке
        /// </summary>
        /// <param name="key">  Информация о нажатой клавише  </param>
        internal override void ReadKey(ConsoleKey key)
        {
            if (IsActive)
            {
                switch (key)
                {
                    case ConsoleKey.Tab: IsActive = false;break;
                    case ConsoleKey.Spacebar: IsClicked = true; ButtonClicked(this, new EventArgs()); break;
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
