using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsLibrary
{
    public struct Message
    {
        public enum KeyPressed { Null, Enter=1, Space=2, Up=3, Down=4, Tab=5 };
        public enum ButtonClicked { Null, Exit};
        public KeyPressed keyPressed;
        public ButtonClicked buttonClicked;

    }
}
