using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
    /// <summary>
    /// Класс списка
    /// </summary>
    public class ListObject:Element
    {
        /// <summary>
        /// Список строк
        /// </summary>
        public List<string> List;

        /// <summary>
        /// Задаёт или получает цвет текста активной строки
        /// </summary>
        public ConsoleColor TextActiveColor { get; set; }
        /// <summary>
        /// Задаёт или получает цвет фона активной строки
        /// </summary>
        public ConsoleColor BackgroundActiveColor { get; set; }

       /// <summary>
       /// Задаёт или получает номер активной строки
       /// </summary>
        public int ActiveLine { get; private set; }
        /// <summary>
        /// Событие нажатия на элемент списка
        /// </summary>
        public event EventHandler ButtonClicked;

        /// <summary>
        /// Конструктор списка
        /// </summary>
        /// <param name="p_Left">горизонтальная координата левого верхнего угла списка</param>
        /// <param name="p_Top">горизонтальная координата левого верхнего угла списка</param>
        /// <param name="p_Width">ширина списка</param>
        /// <param name="p_Height">высота</param>
        /// <param name="p_active">активность списка</param>
        /// <param name="p_parentActive">активность окна-родителя</param>
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

            BackgroundColor = ConsoleColor.Black;
            TextColor = ConsoleColor.White;
            BackgroundActiveColor = ConsoleColor.DarkGray;
            TextActiveColor = ConsoleColor.Black;
        }

        /// <summary>
        /// Пустой обработчик события клика
        /// </summary>
        /// <param name="sender">объект, вызвавший событие</param>
        /// <param name="e">набор аргументов</param>
        private void ListObject_ButtonClicked(object sender, EventArgs e)
        {
           
        }


       /// <summary>
       /// Перерисовывает список
       /// </summary>
        internal override void ReDraw()
        {
            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = TextColor;
            for (int i = 0; i < Height; i++)
            {
                Console.SetCursorPosition(Left, Top + i);
                for (int j = 0; j < Width; j++)
                {
                    Console.Write(" ");
                }
            }

           
            if (List.Count <= Height)
            {
                int size = List.Count;
                for (int i = 0; i < size; i++)
                {
                    string bufstring;

                    if (List[i].Length >= Width) bufstring = List[i].Substring(0, Width);
                    else bufstring = List[i];
                    Console.SetCursorPosition(Left, Top + i);
                    if ((i == ActiveLine) && (IsActive) && (IsParentActive))
                    {
                        Console.ForegroundColor = TextActiveColor;
                        Console.BackgroundColor = BackgroundActiveColor;
                        Console.WriteLine(bufstring);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.BackgroundColor = BackgroundColor;
                        Console.ForegroundColor = TextColor;
                        Console.WriteLine(bufstring);
                        Console.ResetColor();
                    }
                }           
                
            }
            else
            {
                if (ActiveLine> Height-1)
                {
                    int begin = ActiveLine - Height+1;
                    int size = Height;
                    for (int i = begin; i < size + begin; i++)
                    {
                        string bufstring;

                        if (List[i].Length >= Width) bufstring = List[i].Substring(0, Width);
                        else bufstring = List[i];
                        Console.SetCursorPosition(Left, Top + i-begin);
                        if ((i == size+begin-1) && (IsActive) && (IsParentActive))
                        {
                            Console.ForegroundColor = TextActiveColor;
                            Console.BackgroundColor = BackgroundActiveColor;
                            Console.WriteLine(bufstring);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.BackgroundColor = BackgroundColor;
                            Console.ForegroundColor = TextColor;
                            Console.WriteLine(bufstring);
                            Console.ResetColor();
                        }
                    }
                }
                else
                {
                    int size = Height;
                    for (int i = 0; i < size; i++)
                    {
                        string bufstring;

                        if (List[i].Length >= Width) bufstring = List[i].Substring(0, Width);
                        else bufstring = List[i];
                        Console.SetCursorPosition(Left, Top + i);
                        if ((i == ActiveLine) && (IsActive) && (IsParentActive))
                        {
                            Console.ForegroundColor = TextActiveColor;
                            Console.BackgroundColor = BackgroundActiveColor;
                            Console.WriteLine(bufstring);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.BackgroundColor = BackgroundColor;
                            Console.ForegroundColor = TextColor;
                            Console.WriteLine(bufstring);
                            Console.ResetColor();
                        }
                    }
                }
            }
            
            Console.SetCursorPosition(Console.WindowWidth - 2, Console.WindowHeight - 2);
        }
        /// <summary>
        ///  Обрабатывает событие нажатия клавиши клавиатуры на списке
        /// </summary>
        /// <param name="key">  Информация о нажатой клавише  </param>
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
        /// <summary>
        /// Перерисовывает дочерние объекты (не используется) 
        /// </summary>
        internal override void ReDrawChildren()
        {
           
        }

        /// <summary>
        /// Добавляет элемент в список дочерних объектов (не используется)
        /// </summary>
        /// <param name="p_element"> Добавляемый объект </param>
        public override void AddChildren(Element p_element)
        {
            
        }

    }

    
}
