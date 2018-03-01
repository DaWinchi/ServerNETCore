using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
    public abstract class Element
    {
        /*Parametrs of Element (rect)*/
        public virtual int Left { get; set; }
        public virtual int Top { get; set; }
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual string Title { get; set; }

        public virtual bool IsParentActive { get; set; }

        /*Parametrs of Element (background color)*/
        public virtual ConsoleColor Color { get; set; }
        /*Method to update object Element*/
        public abstract void Update();
        /*Method to read pressed key object Element*/
        public abstract void ReadKey(ConsoleKeyInfo keyInfo);

    }
}
