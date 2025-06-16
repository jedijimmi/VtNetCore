using VtNetCore.VirtualTerminal.Enums;

namespace VtNetCore.VirtualTerminal.Model
{
    /// <summary>
    ///     Abstracts VT terminal character attributes
    /// </summary>
    public class TerminalAttribute
    {
        private static readonly ushort BrightBit = 0x0040;
        private static readonly ushort UnderscoreBit = 0x0080;
        private static readonly ushort StandoutBit = 0x0100;
        private static readonly ushort BlinkBit = 0x0200;
        private static readonly ushort ReverseBit = 0x0400;
        private static readonly ushort HiddenBit = 0x0800;
        private static readonly ushort ProtectionBits = 0x3000;

        private ushort InternalBits =
            (ushort)ETerminalColor.White | // ForegroundColor
            (ushort)ETerminalColor.Black; // BackgroundColor

        public TerminalColor ForegroundRgb { get; set; }

        public TerminalColor BackgroundRgb { get; set; }

        /// <summary>
        ///     The foreground color of the text
        /// </summary>
        public ETerminalColor ForegroundColor
        {
            get => (ETerminalColor)(InternalBits & 0x7);
            set => InternalBits = (ushort)((InternalBits & 0xFFF8) | (ushort)value);
        }


        /// <summary>
        ///     The background color of the text
        /// </summary>
        public ETerminalColor BackgroundColor
        {
            get => (ETerminalColor)((InternalBits >> 3) & 0x7);
            set => InternalBits = (ushort)((InternalBits & 0xFFC7) | ((int)value << 3));
        }

        /// <summary>
        ///     Sets the text as bold.
        /// </summary>
        /// <remarks>
        ///     This is an old naming system and should be udpated
        /// </remarks>
        public bool Bright
        {
            get => (InternalBits & BrightBit) == BrightBit;
            set
            {
                if (value)
                    InternalBits |= BrightBit;
                else
                    InternalBits = (ushort)(InternalBits & ~BrightBit & 0xFFFF);
            }
        }

        /// <summary>
        ///     Unclear what this is
        /// </summary>
        /// TODO : Figure out what Standout text is
        public bool Standout
        {
            get => (InternalBits & StandoutBit) == StandoutBit;
            set
            {
                if (value)
                    InternalBits |= StandoutBit;
                else
                    InternalBits = (ushort)(InternalBits & ~StandoutBit & 0xFFFF);
            }
        }

        /// <summary>
        ///     Sets the text as having a line beneath the character
        /// </summary>
        public bool Underscore
        {
            get => (InternalBits & UnderscoreBit) == UnderscoreBit;
            set
            {
                if (value)
                    InternalBits |= UnderscoreBit;
                else
                    InternalBits = (ushort)(InternalBits & ~UnderscoreBit & 0xFFFF);
            }
        }

        /// <summary>
        ///     Sets the blink attribute
        /// </summary>
        public bool Blink
        {
            get => (InternalBits & BlinkBit) == BlinkBit;
            set
            {
                if (value)
                    InternalBits |= BlinkBit;
                else
                    InternalBits = (ushort)(InternalBits & ~BlinkBit & 0xFFFF);
            }
        }

        /// <summary>
        ///     Reverses the foreground and the background colors of the text
        /// </summary>
        public bool Reverse
        {
            get => (InternalBits & ReverseBit) == ReverseBit;
            set
            {
                if (value)
                    InternalBits |= ReverseBit;
                else
                    InternalBits = (ushort)(InternalBits & ~ReverseBit & 0xFFFF);
            }
        }

        /// <summary>
        ///     Specifies that the character should not be displayed.
        /// </summary>
        public bool Hidden
        {
            get => (InternalBits & HiddenBit) == HiddenBit;
            set
            {
                if (value)
                    InternalBits |= HiddenBit;
                else
                    InternalBits = (ushort)(InternalBits & ~HiddenBit & 0xFFFF);
            }
        }

        /// <summary>
        ///     Specifies that the character should not be erased on SEL and SED operations.
        /// </summary>
        public int Protected
        {
            get => (InternalBits & ProtectionBits) >> 12;
            set => InternalBits = (ushort)((InternalBits & ~ProtectionBits) | ((value & 0x3) << 12));
        }

        /// <summary>
        ///     Returns a deep copy of the attribute with reversed colors
        /// </summary>
        /// <returns>A deep copy operation</returns>
        public TerminalAttribute Inverse =>
            new TerminalAttribute
            {
                InternalBits = (ushort)((InternalBits & 0xFFC0) | ((InternalBits & 0x0007) << 3) |
                                        ((InternalBits >> 3) & 0x0007)),
                ForegroundRgb = BackgroundRgb == null ? null : new TerminalColor(BackgroundRgb),
                BackgroundRgb = ForegroundRgb == null ? null : new TerminalColor(ForegroundRgb)
            };

        /// <summary>
        ///     Returns the foreground color as a web color
        /// </summary>
        public string WebColor
        {
            get
            {
                if (ForegroundRgb != null)
                    return ForegroundRgb.WebColor;

                return new TerminalColor(ForegroundColor, Bright).WebColor;
            }
        }

        /// <summary>
        ///     Returns the background color as a web color
        /// </summary>
        public string BackgroundWebColor
        {
            get
            {
                if (BackgroundRgb != null)
                    return BackgroundRgb.WebColor;

                return new TerminalColor(BackgroundColor, false).WebColor;
            }
        }

        public override bool Equals(object obj)
        {
            if (this == null && obj == null)
                return true;

            if (this == null || obj == null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            var other = obj as TerminalAttribute;

            if (other == null)
                return false;

            if (
                (ForegroundRgb == null && other.ForegroundRgb != null) ||
                (ForegroundRgb != null && other.ForegroundRgb == null) ||
                (
                    ForegroundRgb != null && other.ForegroundRgb != null &&
                    !ForegroundRgb.Equals(other.ForegroundRgb)
                )
            )
                return false;

            if (
                (BackgroundRgb == null && other.BackgroundRgb != null) ||
                (BackgroundRgb != null && other.BackgroundRgb == null) ||
                (
                    BackgroundRgb != null && other.BackgroundRgb != null &&
                    !BackgroundRgb.Equals(other.BackgroundRgb)
                )
            )
                return false;

            return
                InternalBits == other.InternalBits;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        ///     Returns a deep copy of the attribute
        /// </summary>
        /// <returns>A deep copy operation</returns>
        public TerminalAttribute Clone()
        {
            return new TerminalAttribute
            {
                InternalBits = InternalBits,
                ForegroundRgb = ForegroundRgb == null ? null : new TerminalColor(ForegroundRgb),
                BackgroundRgb = BackgroundRgb == null ? null : new TerminalColor(BackgroundRgb)
            };
        }

        /// <summary>
        ///     Returns a verbose indented string for debugging
        /// </summary>
        /// <returns>a debugging string</returns>
        public override string ToString()
        {
            return
                "  ForegroundColor: " + ForegroundColor + "\n" +
                "  ForegroundRgb: " + ForegroundRgb == null ? "<null>" :
                ForegroundRgb + "\n" +
                "  BackgroundColor: " + BackgroundColor + "\n" +
                "  BackgroundRgb: " + BackgroundRgb == null ? "<null>" : BackgroundRgb + "\n" +
                                                                         "  Bright: " + Bright + "\n" +
                                                                         "  Standout: " + Standout + "\n" +
                                                                         "  Underscore: " + Underscore + "\n" +
                                                                         "  Blink: " + Blink + "\n" +
                                                                         "  Reverse: " + Reverse + "\n" +
                                                                         "  Hidden: " + Hidden + "\n" +
                                                                         "  Protected: " + Protected + "\n"
                ;
        }
    }
}