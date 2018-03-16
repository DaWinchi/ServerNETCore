using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace WindowsLibrary
{
    public class Application
    {
        public List<Window> windows;
        bool exit = false;
        public Application()
        {
            windows = new List<Window>();
        }

        Thread tracking_adding_queue_from_keyboard;
        Thread tracking_adding_queue_from_windows;
        static object locker = new object();
        public Queue<Message> queue_messages;

        private void TrackingWindows()
        {
            while (true)
            {
                lock (locker)
                    for (int i = 0; i < windows.Count; i++)
                        if (windows[i].IsClosed && !windows[i].IsInQueueToClose)
                        {
                            Message msg = new Message();
                            msg.window = Message.Window.Exit;
                            queue_messages.Enqueue(msg);
                            windows[i].IsInQueueToClose = true;
                        }
            }
        }

        private void TrackingKeyboard()
        {
            ConsoleKeyInfo pressed_key;
            while (true)
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
                    case ConsoleKey.Spacebar:
                        msg.keyPressed = Message.KeyPressed.Space; lock (locker)
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
                        msg.keyPressed = Message.KeyPressed.Tab; lock (locker)
                        {
                            queue_messages.Enqueue(msg);
                        }
                        break;
                    default: break;
                }



            }
        }

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
                                    foreach (Element child in win.Children)
                                    {
                                        if (child.IsActive) { child.ReadKey(ConsoleKey.DownArrow); break; }
                                    }
                            }
                            break;

                        case Message.KeyPressed.Up:
                            foreach (Window win in windows)
                            {
                                if (win.IsActive)
                                    foreach (Element child in win.Children)
                                    {
                                        if (child.IsActive) { child.ReadKey(ConsoleKey.UpArrow); break; }
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
                                                win.Children[i + 1].Update(); break;
                                            }
                                            else
                                            {
                                                win.Children[0].IsActive = true;
                                                win.Children[0].Update(); break;
                                            }
                                        }
                                    }
                            }
                            break;


                        case Message.KeyPressed.Space:
                            foreach (Window win in windows)
                                if (win.IsActive)
                                foreach (Element child in win.Children)
                                {
                                    if (child.IsActive)
                                    { child.ReadKey(ConsoleKey.Spacebar); break; }
                                }
                            break;
                        case Message.KeyPressed.Enter:
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
                            lock (locker)
                                windows.RemoveAt(m);
                            Console.Clear();
                            foreach (Window win in windows) win.Update();
                            break;
                    }
                }

            }
        }

        public void Run()
        {
            tracking_adding_queue_from_keyboard = new Thread(TrackingKeyboard);
            tracking_adding_queue_from_windows = new Thread(TrackingWindows);
            queue_messages = new Queue<Message>();

            foreach (Window win in windows) win.InitializeWindow();
            foreach (Window win in windows) if (win.IsActive) win.Update();


            tracking_adding_queue_from_keyboard.Start();
            tracking_adding_queue_from_windows.Start();
            HandlingMessages();
        }
    }
}

