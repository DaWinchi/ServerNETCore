using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
    /*Структура сообщения*/
    public struct Message
    {
        public enum KeyPressed { Null, Enter=1, Space=2, Up=3, Down=4, Tab=5 };
        public enum Window { Null, Exit, Update};

        public enum TypeElement {Null, Window, Children};
        public struct Update
        {
            public TypeElement type; //тип обновляемого элемента
            public int identificatorWindow; //индентификатор окна
            public int identificatorChild; //идентификатор дочернего элемента
        }

        public Update update;
        public KeyPressed keyPressed;
        public Window window;

    }
}
