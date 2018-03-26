using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
    /*Структура сообщения*/
    public struct Message
    {
        public enum KeyPressed { Null, Enter=1, Space=2, Up=3, Down=4, Tab=5 };
        public enum Window { Null, Exit};

        public enum TypeElement {Null, Window, Children};
        public struct Update
        {
            public TypeElement type;
            public int identificator;
        }
        public KeyPressed keyPressed;
        public Window window;

    }
}
