using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
    public abstract class Element
    {
        public virtual int Left { get; set; }
        public virtual int Top { get; set; }
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }
        public bool IsActive { get; set; }
        public abstract void Update();

    }
}
