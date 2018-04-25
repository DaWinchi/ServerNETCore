using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
    /// <summary>
    /// Класс сообщений
    /// </summary>
    public struct Message
    {
        /// <summary>
        /// Перечисление обрабатываемых клавиш
        /// </summary>
        public enum KeyPressed { Null, TabShift=1, Enter=2, Up=3, Down=4, Tab=5 };
        /// <summary>
        /// Перечисление обрабатываемых сообщений с окном
        /// </summary>
        public enum Window { Null, Exit};
        /// <summary>
        /// Перечисление обрабатываемых сообщений с таймером
        /// </summary>
        public enum Timer {Null, Tick };
        /// <summary>
        /// Перечисление типов объектов для сообщения обновления
        /// </summary>
        public enum TypeElement {Null, Window, Children};
        /// <summary>
        /// Структура сообщения на обновление объекта
        /// </summary>
        public struct UpdateElement
        {
            public TypeElement type; //тип обновляемого элемента
            public int identificatorWindow; //индентификатор окна
            public int identificatorChild; //идентификатор дочернего элемента
        }
        /// <summary>
        /// Структура сообщения для таймера
        /// </summary>
        public struct TimerMsg {public Timer timer; public int identificatorWindow; }
        /// <summary>
        /// Сообщение обновления элемента
        /// </summary>
        public UpdateElement update;
        /// <summary>
        /// Сообщение клавиатуры
        /// </summary>
        public KeyPressed keyPressed;
        /// <summary>
        /// Сообщение от окна 
        /// </summary>
        public Window window;
        /// <summary>
        /// Сообщение таймера
        /// </summary>
        public TimerMsg timermsg;

    }
}
