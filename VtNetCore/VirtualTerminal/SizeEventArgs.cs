using System;

namespace VtNetCore.VirtualTerminal
{
    public class SizeEventArgs : EventArgs
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
}