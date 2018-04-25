using System;
using System.Collections.Generic;
using System.Threading;

namespace WindowsLibrary
{
    /// <summary>
    /// Класс окна
    /// </summary>
    public class Window : Element
    {
        /// <summary>
        /// Объект класса приложения, которому принадлежит данное окно
        /// </summary>
        protected Application ParentApp;
        /// <summary>
        ///  Задаёт или получает режим активности окна
        /// </summary>
        public override bool IsActive
        {
            get => base.IsActive;
            set
            {
                base.IsActive = value;
                ChangeActive(this, new EventArgs());
            }
        }
        /// <summary>
        ///  Задаёт или получает информацию о закрытии окна
        /// </summary>
        public bool IsClosed { get; set; }
       /// <summary>
       /// Задаёт или получает идентификационный номер окна
       /// </summary>
        public int IdentificationNumber { get; set; }
        /// <summary>
        /// Список дочерних объектов
        /// </summary>
        public List<Element> Children;


        /// <summary>
        /// Событие, возникающее при изменении состояния активности окна
        /// </summary>
        protected event EventHandler ChangeActive;


        /// <summary>
        /// Конструктор окна
        /// </summary>
        /// <param name="p_left"> горизонтальная координата левого верхнего угла окна </param>
        /// <param name="p_top"> вертикальная координата левого верхнего угла окна  </param>
        /// <param name="p_width"> ширина окна </param>
        /// <param name="p_height"> высота окна </param>
        /// <param name="p_title"> заголовок окна </param>
        /// <param name="p_isactive"> активость окна </param>
        /// <param name="p_identification"> идентификационный номер </param>
        /// <param name="application"> приложение, которому окно принадлежит </param>
        public Window(int p_left, int p_top, int p_width, int p_height,
            string p_title, bool p_isactive, int p_identification, ref Application application)
        {
            Children = new List<Element>();
            ChangeActive += Window_ChangeActive;
            Left = p_left;
            Top = p_top;
            Width = p_width;
            Height = p_height;
            IsActive = p_isactive;
            IsClosed = false;
            IdentificationNumber = p_identification;
            Title = p_title;
            ParentApp = application;

            TimerTick += Window_TimerTick;

            TextColor = ConsoleColor.White;
            BackgroundColor = ConsoleColor.Black;

        }
        
        #region Таймер окна
        /// <summary>
        /// Объект таймера окна
        /// </summary>
        internal Timer timer;
        /// <summary>
        /// Событие, возникающее при очередном "тике"
        /// </summary>
        public event EventHandler TimerTick;
        /// <summary>
        /// Запускает таймер
        /// </summary>
        /// <param name="timeInterval"> шаг таймера в мс </param>
        public void TimerStart(int timeInterval)
        {
            if (timer != null) timer.Dispose();
            timer = new Timer(AddTimerMessage, null, 0, timeInterval);
        }
        /// <summary>
        /// Останавливает таймер
        /// </summary>
        public void TimerStop()
        {
            if (timer != null) timer.Dispose();
        }
        /// <summary>
        /// Добавляет сообщение таймера в очередь сообщений приложения
        /// </summary>
        /// <param name="obj"> стандартный параметр функции таймера </param>
        private void AddTimerMessage(object obj)
        {
            Message.TimerMsg msgtimer = new Message.TimerMsg
            {
                timer = Message.Timer.Tick,
                identificatorWindow = IdentificationNumber
            };
            Message msg = new Message { timermsg = msgtimer };
            ParentApp.queue_messages.Enqueue(msg);
        }
        /// <summary>
        /// Вызывает событие "тика"
        /// </summary>
        internal void TickFunction()
        {
            TimerTick(this, new EventArgs());
        }
        /// <summary>
        /// Пустая  функция события
        /// </summary>
        /// <param name="sender"> объект, вызвавший событие </param>
        /// <param name="e"> набор аргументов </param>
        protected virtual void Window_TimerTick(object sender, EventArgs e) { }

        #endregion

        /// <summary>
        /// Перерисовывает окно
        /// </summary>
        internal override void ReDraw()
        {
            CreateFrame();
            WriteTitle();
            ReDrawChildren();
        }

        /// <summary>
        /// Перерисовывает дочерние объекты 
        /// </summary>
        internal override void ReDrawChildren()
        {
            int size = Children.Count;
            for (int i = 0; i < size; i++)
            {
                Children[i].ReDraw();
            }
        }

        /// <summary>
        /// Ставит в очередь на перерисовку объект с заданным индексом
        /// </summary>
        /// <param name="numberElement"> индекс дочернего объекта в списке дочерних объектов </param>
        public void UpdateChildren(int numberElement)
        {
            Message.UpdateElement update = new Message.UpdateElement
            {
                type = Message.TypeElement.Children,
                identificatorWindow = IdentificationNumber,
                identificatorChild = numberElement
            };
            Message msgupdate = new Message { update = update };
            ParentApp.queue_messages.Enqueue(msgupdate);

        }

        /// <summary>
        /// Ставит в очередь на перерисовку данное окно
        /// </summary>
        public void Update()
        {
            Message.UpdateElement update = new Message.UpdateElement
            {
                type = Message.TypeElement.Window,
                identificatorWindow = IdentificationNumber,
                identificatorChild = 0                          //при обновлении окна идентификатор дочернего элемента может быть любым
            };

            Message msg = new Message
            {
                update = update
            };

            ParentApp.queue_messages.Enqueue(msg);
        }


        /// <summary>
        /// Обработчик события изменения состояния активности окна
        /// </summary>
        /// <param name="sender"> sender </param>
        /// <param name="e"> набор аргументов </param>
        protected void Window_ChangeActive(object sender, EventArgs e)
        {
            foreach (Element child in Children) child.IsParentActive = IsActive;
        }

        /// <summary>
        /// Рисует рамку окна
        /// </summary>
        protected virtual void CreateFrame()
        {
            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = TextColor;
            if (IsActive)
            {
                for (int i = 0; i < Width; i++)
                {

                    for (int j = 0; j < Height; j++)
                    {
                        Console.SetCursorPosition(Left + i, Top + j);
                        if ((i == 0) && (j == 0)) Console.Write("╔");
                        else if ((i == (Width - 1)) && (j == 0)) Console.Write("╗");
                        else if ((i == 0) && (j == (Height - 1))) Console.Write("╚");
                        else if ((i == (Width - 1)) && (j == (Height - 1))) Console.Write("╝");
                        else if ((i != 0 || i != Width - 1) && (j == 0 || j == Height - 1)) Console.Write("═");
                        else if ((i == 0 || i == Width - 1) && (j != 0 || j != Height - 1)) Console.Write("║");
                        else Console.Write(" ");
                    }
                }
            }
            else
            {
                for (int i = 0; i < Width; i++)
                {

                    for (int j = 0; j < Height; j++)
                    {
                        Console.SetCursorPosition(Left + i, Top + j);
                        if ((i == 0) && (j == 0)) Console.Write("┌");
                        else if ((i == (Width - 1)) && (j == 0)) Console.Write("┐");
                        else if ((i == 0) && (j == (Height - 1))) Console.Write("└");
                        else if ((i == (Width - 1)) && (j == (Height - 1))) Console.Write("┘");
                        else if ((i != 0 || i != Width - 1) && (j == 0 || j == Height - 1)) Console.Write("─");
                        else if ((i == 0 || i == Width - 1) && (j != 0 || j != Height - 1)) Console.Write("│");
                        else Console.Write(" ");
                    }
                }
            }
            Console.SetCursorPosition(Console.WindowWidth - 2, Console.WindowHeight - 2);
        }

        /// <summary>
        /// Отображает заголовок окна
        /// </summary>
        public virtual void WriteTitle()
        {
            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = TextColor;
            if (Title != null)
            {
                string bufTitle;
                if (Title.Length >= Width) bufTitle = " " + Title.Substring(0, Width - 2) + " ";
                else bufTitle = " " + Title + " ";
                Console.SetCursorPosition(Left + Width / 2 - bufTitle.Length / 2, Top);
                Console.WriteLine(bufTitle);
            }
            Console.SetCursorPosition(Console.WindowWidth - 2, Console.WindowHeight - 2);
        }
        /// <summary>
        ///  Обрабатывает событие нажатия клавиши клавиатуры на окне
        /// </summary>
        /// <param name="key">  Информация о нажатой клавише  </param>
        internal override void ReadKey(ConsoleKey key)
        {
            if (IsActive)
            {
                switch (key)
                {
                    case ConsoleKey.Enter: { IsActive = false; Update(); break; }
                }
            }

        }

        /// <summary>
        /// Добавляет элемент в список дочерних объектов
        /// </summary>
        /// <param name="p_element"> Добавляемый объект </param>
        public override void AddChildren(Element p_element)
        {
            Children.Add(p_element);
        }

       /// <summary>
       /// Добавляет сообщение о закрытии окна в очередь сообщений
       /// </summary>
        public void CloseWindow()
        {
            Message msg = new Message
            {
                window = Message.Window.Exit
            };
            IsClosed = true;
            TimerStop();
            ParentApp.queue_messages.Enqueue(msg);
        }


    }
}
