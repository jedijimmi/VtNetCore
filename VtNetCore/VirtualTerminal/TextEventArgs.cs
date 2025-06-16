using System;

namespace VtNetCore.VirtualTerminal
{
    public class TextEventArgs : EventArgs
    {
        public string Text { get; set; }
    }
}