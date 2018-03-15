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

        Thread tracking_adding_queue;
        static object locker = new object();
        static Queue<Message> queue_messages;


        private void TrackingKeyboard()
        {
            ConsoleKeyInfo pressed_key;
            while (true)
            {
                pressed_key = Console.ReadKey();
                Message msg = new Message();


                switch (pressed_key.Key)
                {
                    case ConsoleKey.Enter: msg.keyPressed = Message.KeyPressed.Enter; break;
                    case ConsoleKey.Spacebar: msg.keyPressed = Message.KeyPressed.Space; break;
                    case ConsoleKey.UpArrow: msg.keyPressed = Message.KeyPressed.Up; break;
                    case ConsoleKey.DownArrow: msg.keyPressed = Message.KeyPressed.Down; break;
                    case ConsoleKey.Tab: msg.keyPressed = Message.KeyPressed.Tab; break;
                    default: break;
                }


                lock (locker)
                {
                    queue_messages.Enqueue(msg);
                }
            }
        }

        private void HandlingMessages()
        {
            while (!exit)
            {
                lock (locker)
                {
                    while (queue_messages.Count > 0)
                    {
                        Message msg = new Message();
                        msg = queue_messages.Dequeue();

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
                                foreach(Window win in windows)
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
                                            windows[i + 1].Update(); break;
                                        }
                                        else
                                        {
                                            windows[0].IsActive = true;
                                            windows[0].Update(); break;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }

        public void Run()
        {
            tracking_adding_queue = new Thread(TrackingKeyboard);
            queue_messages = new Queue<Message>();

            foreach (Window win in windows) win.InitializeWindow();

            tracking_adding_queue.Start();
            HandlingMessages();
        }
    }
}

