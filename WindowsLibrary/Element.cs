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

        /*Свойство показывает было ли событие клика на элементе*/
        public virtual bool IsClicked { get; set; }

        /*Свойство показывает активность элемента-родителя*/
        public virtual bool IsParentActive { get; set; }
        
        /*Метод обновления элемента*/
        public abstract void Update();

        /*Метод добавления дочернего элемента*/
        public abstract void AddChildren(Element p_element);

        /*Метод обработки нажатой на клавиатуре клавиши для данного элемента*/
        public abstract void ReadKey(ConsoleKey keyInfo);

        /*Метод обновления дочерних элементов*/
        public abstract void UpdateChildren();

    }
}
