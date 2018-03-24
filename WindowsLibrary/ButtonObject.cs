using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
    public class ButtonObject : Element
    {

        /*Событие нажатия кнопки*/
        public event EventHandler ButtonClicked;

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
        }

        /*Пустой обработчик события клика*/
        private void ButtonObject_ButtonClicked(object sender, EventArgs e)
        {
            
        }

        /*Метод перерисовывает кнопку и отображает на ней текст*/
        public override void Update()
        {
            if (IsActive)
            {
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
        public override void ReadKey(ConsoleKey key)
        {
            if (IsActive)
            {
                switch (key)
                {
                    case ConsoleKey.Tab: IsActive = false; Update(); break;
                    case ConsoleKey.Spacebar: IsClicked = true; ButtonClicked(this, new EventArgs()); Update(); break;
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
