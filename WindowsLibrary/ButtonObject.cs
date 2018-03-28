using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
    public class ButtonObject : Element
    {

        /*Событие нажатия кнопки*/
        public event EventHandler ButtonClicked;
        public ConsoleColor TextActiveColor { get; set; }
        public ConsoleColor BackgroundActiveColor { get; set; }
        /*Конструктор кнопки
        @ p_Left - координата левой верхней границы кнопки по горизонтали
        @ p_Top - координата левой верхней границы кнопки по вертикали
        @ p_Width - ширина кнопки
        @ p_Height - высота кнопки
        @ p_active - флаг активности кнопки при инициализации(при большом количестве элементов в окне в положении true может быть только у одного элемента)
        @ p_parentActive - флаг активности родителя
        @ p_Title - надпись на кнопке        
        */
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
            BackgroundActiveColor = ConsoleColor.DarkGray;
            TextActiveColor = ConsoleColor.Black;
        }

        /*Пустой обработчик события клика*/
        protected void ButtonObject_ButtonClicked(object sender, EventArgs e)
        {
            
        }

        /*Метод перерисовывает кнопку и отображает на ней текст*/
        internal override void ReDraw()
        {
            if (IsActive)
            {
                Console.BackgroundColor = BackgroundActiveColor;
                Console.ForegroundColor = TextActiveColor;
                for (int i = 0; i < Width; i++)
                {
                    for (int j = 0; j < Height; j++)
                    {
                        Console.SetCursorPosition(Left + i, Top + j);
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write(" ");
                    }
                }

                string bufstring;
                if (Title.Length >= Width) bufstring = Title.Substring(0, Width);
                else bufstring = Title;
                Console.ForegroundColor = ConsoleColor.Black;
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
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write(" ");
                    }
                }

                string bufstring;
                if (Title.Length >= Width) bufstring = Title.Substring(0, Width);
                else bufstring = Title;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(Left + Width / 2 - bufstring.Length / 2, Top + Height / 2);
                Console.WriteLine(bufstring);
                Console.ResetColor();
            }

            Console.SetCursorPosition(Console.WindowWidth - 2, Console.WindowHeight - 2);
        }

        /*Метод обработки нажатой на клавиатуре клавиши для данной кнопки*/
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
