using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
    public class ListObject:Element
    {
        /*Список имен строк списка*/
        public List<string> List;

        /*Размер списка до изменения*/
        private int oldSize;

        /*Свойство отображает или задаёт какая строка в данный момент активна*/
        public int ActiveLine { get; private set; }

        /*Событие нажатия на элемент списка*/
        public event EventHandler ButtonClicked;


        /*Конструктор списка
       @ p_Left - координата левой верхней границы кнопки по горизонтали
       @ p_Top - координата левой верхней границы кнопки по вертикали
       @ p_Width - ширина кнопки
       @ p_Height - высота кнопки
       @ p_active - флаг активности кнопки при инициализации(при большом количестве элементов в окне в положении true может быть только у одного элемента)
       @ p_parentActive - флаг активности родителя(устанавливается согласно родителю)       
       */
        public ListObject(int p_Left, int p_Top, int p_Width, int p_Height, bool p_active, bool p_parentActive)
        {
            ButtonClicked += ListObject_ButtonClicked;
            Left = p_Left;
            Top = p_Top;
            Width = p_Width;
            Height = p_Height;
            List = new List<string>();
            IsActive = p_active;
            IsParentActive = p_parentActive;
            IsClicked = false;
            if (IsActive) ActiveLine = 0;
            oldSize = 0;
        }

        /*Пустой обработчик события клика*/
        private void ListObject_ButtonClicked(object sender, EventArgs e)
        {
           
        }

        /*Метод перерисовывает список на экране*/
        internal override void ReDraw()
        {
            for (int i = 0; i < oldSize; i++)
            {
                Console.SetCursorPosition(Left, Top + i);
                for (int j = 0; j < Width; j++)
                {
                    Console.Write(" ");
                }
            }

            int size = List.Count;

            for (int i = 0; i < size; i++)
            {
                string bufstring;
                if (List[i].Length >= Width) bufstring = List[i].Substring(0, Width);
                else bufstring = List[i];
                Console.SetCursorPosition(Left, Top + i);
                if ((i == ActiveLine)&&(IsActive)&&(IsParentActive))
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine(bufstring);
                    Console.ResetColor();
                }
                else Console.WriteLine(bufstring);
            }
            oldSize = size;
            Console.SetCursorPosition(Console.WindowWidth - 2, Console.WindowHeight - 2);
        }

        /*Метод обработки нажатой на клавиатуре клавиши для данной кнопки*/
        internal override void ReadKey(ConsoleKey key)
        {
            if (IsActive)
            {
                switch (key)
                {
                    case ConsoleKey.DownArrow: if(ActiveLine<List.Count-1) ActiveLine++; break;
                    case ConsoleKey.UpArrow: if(ActiveLine>=1)ActiveLine--; break;
                    case ConsoleKey.Tab: IsActive = false;  break;
                    case ConsoleKey.Spacebar:
                        IsClicked = true;
                        ButtonClicked(this, new EventArgs());
                        break;
                    default: break;
                }
            }
        }

        /*Метод перерисовки дочерних элементов(не используется)*/
        internal override void ReDrawChildren()
        {
           
        }

        /*Метод добавления дочерних элементов(не используется)*/
        public override void AddChildren(Element p_element)
        {
            
        }

    }

    
}
