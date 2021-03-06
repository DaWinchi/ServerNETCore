﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace WindowsLibrary
{
    /// <summary>
    /// Класс приложения
    /// </summary>
    public class Application
    {
        /// <summary>
        /// Поле, отвечающее за выход из приложения
        /// </summary>
        public bool exit = false;
        /// <summary>
        /// Задаёт или получает фоновый цвет консоли
        /// </summary>
        public ConsoleColor GlobalBackgroundColor { get; set; }

        /// <summary>
        /// Поток, следящий за клавиатурой
        /// </summary>
       private Thread tracking_adding_queue_from_keyboard;
        /// <summary>
        /// Объект синхронизации
        /// </summary>
        static object locker = new object();
        /// <summary>
        /// Очередь сообщений
        /// </summary>
        public Queue<Message> queue_messages;
        /// <summary>
        /// Список окон, принадлежащих данному приложению
        /// </summary>
        public List<Window> windows;
        

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Application()
        {
            windows = new List<Window>();
            GlobalBackgroundColor = ConsoleColor.DarkGray;
            Console.BackgroundColor = GlobalBackgroundColor;
            Console.Clear();
        }

        /// <summary>
        /// Прикрепляет окно к приложению
        /// </summary>
        /// <param name="win"> добавляемое окно </param>
        public void AddWindow(Window win)
        {
            bool isNewWindow = true;
            if(windows!=null)
            foreach(Window window in windows)
            {
                if (window.IdentificationNumber == win.IdentificationNumber) isNewWindow = false;                
            }
            if (isNewWindow)
            {
                win.IsClosed = false;
                windows.Add(win);
            }
        }
      /// <summary>
      /// Функция потока, следящего за клавиатурой
      /// </summary>
        private void TrackingKeyboard()
        {
            ConsoleKeyInfo pressed_key;
            while (!exit)
            {
                pressed_key = Console.ReadKey();
                Message msg = new Message();


                switch (pressed_key.Key)
                {                   
                    case ConsoleKey.Enter:
                        msg.keyPressed = Message.KeyPressed.Enter; lock (locker)
                        {
                            queue_messages.Enqueue(msg);
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        msg.keyPressed = Message.KeyPressed.Up; lock (locker)
                        {
                            queue_messages.Enqueue(msg);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        msg.keyPressed = Message.KeyPressed.Down; lock (locker)
                        {
                            queue_messages.Enqueue(msg);
                        }
                        break;
                    case ConsoleKey.Tab:
                        if (ConsoleModifiers.Shift == pressed_key.Modifiers)
                        {
                            msg.keyPressed = Message.KeyPressed.TabShift; lock (locker)
                            {
                                queue_messages.Enqueue(msg);
                            }
                            break;
                        }
                        else
                        {
                            msg.keyPressed = Message.KeyPressed.Tab; lock (locker)
                            {
                                queue_messages.Enqueue(msg);
                            }
                            break;
                        }
                    default: break;
                }

                Thread.Sleep(50);

            }
        }

      /// <summary>
      /// Цикл обработки сообщений
      /// </summary>
        private void HandlingMessages()
        {
            while (!exit)
            {

                while (queue_messages.Count > 0)
                {
                    Message msg = new Message();
                    lock (locker) msg = queue_messages.Dequeue();

                    switch (msg.keyPressed)
                    {
                        case Message.KeyPressed.Down:
                            foreach (Window win in windows)
                            {
                                if (win.IsActive)
                                {
                                    int i = 0;
                                    foreach (Element child in win.Children)
                                    {
                                        if (child.IsActive)
                                        {
                                            child.ReadKey(ConsoleKey.DownArrow);
                                            Message.UpdateElement update = new Message.UpdateElement
                                            {
                                                type = Message.TypeElement.Children,
                                                identificatorWindow = win.IdentificationNumber,
                                                identificatorChild = i
                                            };
                                            Message msgupdate = new Message { update = update };
                                            lock (locker) queue_messages.Enqueue(msgupdate);
                                            break;
                                        }
                                        i++;
                                    }
                                }
                            }
                            break;

                        case Message.KeyPressed.Up:
                            foreach (Window win in windows)
                            {
                                if (win.IsActive)
                                {
                                    int i = 0;
                                    foreach (Element child in win.Children)
                                    {
                                        if (child.IsActive)
                                        {
                                            child.ReadKey(ConsoleKey.UpArrow);
                                            Message.UpdateElement update = new Message.UpdateElement
                                            {
                                                type = Message.TypeElement.Children,
                                                identificatorWindow = win.IdentificationNumber,
                                                identificatorChild = i
                                            };
                                            Message msgupdate = new Message { update = update };
                                            lock (locker) queue_messages.Enqueue(msgupdate);
                                            break;
                                        }
                                        i++;
                                    }
                                }
                            }
                            break;


                        case Message.KeyPressed.Tab:
                            foreach (Window win in windows)
                            {
                                if (win.IsActive)
                                    for (int i = 0; i < win.Children.Count; i++)
                                    {

                                        if (win.Children[i].IsActive)
                                        {
                                            win.Children[i].ReadKey(ConsoleKey.Tab);
                                            if ((i + 1) < win.Children.Count)
                                            {
                                                win.Children[i + 1].IsActive = true;
                                                Message.UpdateElement update = new Message.UpdateElement
                                                {
                                                    type = Message.TypeElement.Children,
                                                    identificatorWindow = win.IdentificationNumber,
                                                    identificatorChild = i
                                                };
                                                Message msgupdate = new Message { update = update };
                                                lock (locker) queue_messages.Enqueue(msgupdate);

                                                update.identificatorChild = i + 1;
                                                msgupdate.update = update;
                                                lock (locker) queue_messages.Enqueue(msgupdate);

                                                break;
                                            }
                                            else
                                            {
                                                win.Children[0].IsActive = true;
                                                Message.UpdateElement update = new Message.UpdateElement
                                                {
                                                    type = Message.TypeElement.Children,
                                                    identificatorWindow = win.IdentificationNumber,
                                                    identificatorChild = i
                                                };
                                                Message msgupdate = new Message { update = update };
                                                lock (locker) queue_messages.Enqueue(msgupdate);

                                                update.identificatorChild = 0;
                                                msgupdate.update = update;
                                                lock (locker) queue_messages.Enqueue(msgupdate);

                                                break;
                                            }
                                        }
                                    }
                            }
                            break;


                        case Message.KeyPressed.Enter:
                            lock (locker) foreach (Window win in windows)
                                    if (win.IsActive)
                                    {
                                        int i = 0;
                                        foreach (Element child in win.Children)
                                        {
                                            if (child.IsActive)
                                            {
                                                child.ReadKey(ConsoleKey.Spacebar);

                                                Message.UpdateElement update = new Message.UpdateElement
                                                {
                                                    type = Message.TypeElement.Children,
                                                    identificatorWindow = win.IdentificationNumber,
                                                    identificatorChild = i
                                                };
                                                Message msgupdate = new Message { update = update };
                                                lock (locker) queue_messages.Enqueue(msgupdate);

                                                break;
                                            }
                                            i++;
                                        }
                                        break;
                                    }
                            break;
                        case Message.KeyPressed.TabShift:
                            for (int i = 0; i < windows.Count; i++)
                            {

                                if (windows[i].IsActive)
                                {
                                    windows[i].ReadKey(ConsoleKey.Enter);
                                    if ((i + 1) < windows.Count)
                                    {
                                        windows[i + 1].IsActive = true;
                                        windows[i + 1].Update();
                                        break;
                                    }
                                    else
                                    {
                                        windows[0].IsActive = true;
                                        windows[0].Update();
                                        break;
                                    }
                                }
                            }
                            break;
                    }

                    switch (msg.window)
                    {
                        case Message.Window.Exit:
                            int i = 0, m = 0;

                            while (m < windows.Count)
                            {
                                if (windows[m].IsClosed) break;
                                m++;
                            }

                            while (i < windows.Count)
                            {
                                if (windows[i].IsActive) break;
                                i++;
                            }

                            if (i == m)
                            {
                                for (int j = 0; j < windows.Count; j++)
                                {

                                    if (windows[j].IsActive)
                                    {
                                        windows[j].ReadKey(ConsoleKey.Enter);
                                        if ((j + 1) < windows.Count)
                                        {
                                            windows[j + 1].IsActive = true;
                                            windows[j + 1].Update();
                                            break;
                                        }
                                        else
                                        {
                                            windows[0].IsActive = true;
                                            windows[0].Update();
                                            break;
                                        }
                                    }
                                }
                            }
                            lock (locker) windows.RemoveAt(m);
                            Console.BackgroundColor = GlobalBackgroundColor;
                            Console.Clear();

                            foreach (Window win in windows) win.Update();
                            foreach (Window win in windows) if (win.IsActive) win.Update();
                            break;
                    }

                    switch(msg.update.type)
                    {
                        case Message.TypeElement.Window:
                            {
                                foreach(Window win in windows)
                                {
                                    if (win.IdentificationNumber == msg.update.identificatorWindow) win.ReDraw();
                                }
                                break;
                            }
                        case Message.TypeElement.Children:
                            {
                                foreach (Window win in windows)
                                {
                                    if (win.IdentificationNumber == msg.update.identificatorWindow)
                                    {
                                        win.Children[msg.update.identificatorChild].ReDraw();
                                    }
                                }
                                break;
                            }
                    }
                    switch(msg.timermsg.timer)
                    {
                        case Message.Timer.Tick:
                            {
                                foreach (Window win in windows)
                                {
                                    if(win.IdentificationNumber==msg.timermsg.identificatorWindow)
                                    {
                                        win.TickFunction();
                                    }
                                }
                                break;
                            }
                    }
                }
                Thread.Sleep(50);
            }
            Console.Clear();
            tracking_adding_queue_from_keyboard.Join();
            
            
        }

        /// <summary>
        /// Запускает приложение
        /// </summary>
        public void Run()
        {
            tracking_adding_queue_from_keyboard = new Thread(TrackingKeyboard);
            queue_messages = new Queue<Message>();

            foreach (Window win in windows) win.Update();
            foreach (Window win in windows) if (win.IsActive) win.Update();
            
            tracking_adding_queue_from_keyboard.Start();
            HandlingMessages();
        }
    }
}

