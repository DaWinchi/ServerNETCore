using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
    public class LabelObject : Element
    {

        /*Текст в метке*/
        public string Text { get; set; }

        /*Конструктор прогресс-бара
    @ p_Left - координата левой верхней границы  по горизонтали
    @ p_Top - координата левой верхней границы  по вертикали
    @ p_Width - ширина 
    @ p_Height - высота 
    @ p_active - флаг активности кнопки при инициализации(при большом количестве элементов в окне в положении true может быть только у одного элемента)
    @ p_parentActive - флаг активности родителя(устанавливается согласно родителю)   
    @ p_text - текст в метке
    */
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

        /*Метод перерисовывает элемент*/
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
