using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
    public abstract class Element
    {
        /*Параметры элемента*/
        public virtual int Left { get; set; }
        public virtual int Top { get; set; }
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual string Title { get; set; }
        public virtual ConsoleColor TextColor { get; set; }
        public virtual ConsoleColor BackgroundColor { get; set; }
        /*Свойство показывает было ли событие клика на элементе*/
        public virtual bool IsClicked { get; set; }

        /*Свойство показывает активность элемента-родителя*/
        public virtual bool IsParentActive { get; set; }
        
        /*Метод перерисовки элемента*/
        internal abstract void ReDraw();

        /*Метод добавления дочернего элемента*/
        public abstract void AddChildren(Element p_element);

        /*Метод обработки нажатой на клавиатуре клавиши для данного элемента*/
        internal abstract void ReadKey(ConsoleKey keyInfo);

        /*Метод перерисовки дочерних элементов*/
        internal abstract void ReDrawChildren();

    }
}
