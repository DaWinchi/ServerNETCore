using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
    /// <summary>
    /// Абстрактный класс обощенного объекта
    /// </summary>
    public abstract class Element
    {
        /// <summary>
        /// Задаёт или получает координату по горизонтали левого верхнего угла объекта
        /// </summary>
        public virtual int Left { get; set; }
        /// <summary>
        /// Задаёт или получает координату по вертикали левого верхнего угла объекта
        /// </summary>
        public virtual int Top { get; set; }
        /// <summary>
        /// Задаёт или получает ширину объекта
        /// </summary>
        public virtual int Width { get; set; }
        /// <summary>
        /// Задаёт или получает высоту объекта
        /// </summary>
        public virtual int Height { get; set; }
        /// <summary>
        /// Задаёт или получает режим активности объекта
        /// </summary>
        public virtual bool IsActive { get; set; }
        /// <summary>
        /// Задаёт или получает заголовок объекта
        /// </summary>
        public virtual string Title { get; set; }
        /// <summary>
        /// Задаёт или получает цвет текста объекта
        /// </summary>
        public virtual ConsoleColor TextColor { get; set; }
        /// <summary>
        /// Задаёт или получает цвет заднего фона объекта
        /// </summary>
        public virtual ConsoleColor BackgroundColor { get; set; }
        /// <summary>
        /// Задаёт или получает информацию о событии клика на объекте
        /// </summary>
        public virtual bool IsClicked { get; set; }

        /// <summary>
        /// Задаёт или получает режим активности объекта-родителя
        /// </summary>
        public virtual bool IsParentActive { get; set; }
        
        /// <summary>
        /// Перерисовывает объект
        /// </summary>
        internal abstract void ReDraw();

        /// <summary>
        /// Добавляет элемент в список дочерних объектов
        /// </summary>
        /// <param name="p_element"> Добавляемый объект </param>
        public abstract void AddChildren(Element p_element);

        /// <summary>
        /// Обрабатывает событие нажатия клавиши клавиатуры на объекте
        /// </summary>
        /// <param name="keyInfo"> Информация о нажатой клавише </param>
        internal abstract void ReadKey(ConsoleKey keyInfo);

        /// <summary>
        /// Перерисовывает дочерние объекты
        /// </summary>
        internal abstract void ReDrawChildren();

    }
}
