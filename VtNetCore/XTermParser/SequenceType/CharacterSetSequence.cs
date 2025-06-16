using VtNetCore.VirtualTerminal.Enums;

namespace VtNetCore.XTermParser.SequenceType
{
    public class CharacterSetSequence : TerminalSequence
    {
        public ECharacterSet CharacterSet { get; set; }
        public ECharacterSetMode Mode { get; set; }

        public override string ToString()
        {
            return "Character set - " + Mode + " is " + CharacterSet;
        }
    }
}