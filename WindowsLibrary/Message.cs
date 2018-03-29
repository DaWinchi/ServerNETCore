using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
    /*Структура сообщения*/
    public struct Message
    {
        public enum KeyPressed { Null, TabShift=1, Enter=2, Up=3, Down=4, Tab=5 };
        public enum Window { Null, Exit};
        public enum Timer {Null, Tick };
        public enum TypeElement {Null, Window, Children};
        public struct UpdateElement
        {
            public TypeElement type; //тип обновляемого элемента
            public int identificatorWindow; //индентификатор окна
            public int identificatorChild; //идентификатор дочернего элемента
        }
        public struct TimerMsg {public Timer timer; public int identificatorWindow; }
        public UpdateElement update;
        public KeyPressed keyPressed;
        public Window window;
        public TimerMsg timermsg;

    }
}
