using System;
using System.Collections.Generic;
using System.Threading;

namespace WindowsLibrary
{
    public class Window : Element
    {
        /*Приложение, которому принадлежит данное окно*/
        protected Application ParentApp;

        /*Свойство, отвечающее за активность окна*/
        public override bool IsActive
        {
            get => base.IsActive;
            set
            {
                base.IsActive = value;
                ChangeActive(this, new EventArgs());
            }
        }

        /*Свойство, показывающее не закрыто ли окно*/
        public bool IsClosed { get; set; }

        /*Целочисленный идентификатор окна*/
        public int IdentificationNumber { get; set; }

        /*Набор дочерних элементов*/
        public List<Element> Children;

        public Timer Timer;
        /*Событие, возникающее при изменении свойства IsActive окна*/
        protected event EventHandler ChangeActive;


        /*Конструктор окна
         @ p_left - координата левой верхней границы окна по горизонтали
         @ p_top - координата левой верхней границы окна по вертикали
         @ p_width - ширина окна
         @ p_height - высота окна
         @ p_title - заголовок окна
         @ p_isactive - флаг активности окна при инициализации(при большом количестве окон в положении true может быть только у одного окна)
         @ p_identification - целочисленный идентификатор окна
         @ application - ссылка на приложение, которому принадлежит данное окно
         */
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

        }


        /*Метод отображает окно и дочерние элементы*/
        internal override void ReDraw()
        {
            CreateFrame();
            WriteTitle();
            ReDrawChildren();
        }

        /*Метод отображает все дочерние элементы*/
        internal override void ReDrawChildren()
        {
            int size = Children.Count;
            for (int i = 0; i < size; i++)
            {
                Children[i].ReDraw();
            }
        }

        /*Метод отображает все дочерние элементы*/
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


        /*Обработчик события изменения активности окна*/
        protected void Window_ChangeActive(object sender, EventArgs e)
        {
            foreach (Element child in Children) child.IsParentActive = IsActive;
        }

        /*Метод рисует рамку окна*/
        protected virtual void CreateFrame()
        {
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

        /*Метод отображает заголовок окна*/
        public virtual void WriteTitle()
        {
            if (Title != null)
            {
                string bufTitle;
                if (Title.Length >= Width) bufTitle = Title.Substring(0, Width - 2);
                else bufTitle = Title;
                Console.SetCursorPosition(Left + Width / 2 - bufTitle.Length / 2, Top);
                Console.WriteLine(bufTitle);
            }
            Console.SetCursorPosition(Console.WindowWidth - 2, Console.WindowHeight - 2);
        }

        /*Метод обработки нажатой на клавиатуре клавиши для данного окна*/
        public override void ReadKey(ConsoleKey key)
        {
            if (IsActive)
            {
                switch (key)
                {
                    case ConsoleKey.Enter: { IsActive = false; Update(); break; }
                }
            }

        }

        /*Метод добавляет дочерний элемент в окно*/
        public override void AddChildren(Element p_element)
        {
            Children.Add(p_element);
        }

        /*Метод добавляет сообщении о закрытии в очередь сообщений приложения*/
        public void CloseWindow()
        {
            Message msg = new Message
            {
                window = Message.Window.Exit
            };
            IsClosed = true;
            if (Timer != null) Timer.Dispose();
            ParentApp.queue_messages.Enqueue(msg);
        }


    }
}
