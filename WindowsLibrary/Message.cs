using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
    public struct Message
    {
       public enum   KeyPressed  { Enter, Space, Up, Down   };

       public KeyPressed keyPressed;
    }
}
