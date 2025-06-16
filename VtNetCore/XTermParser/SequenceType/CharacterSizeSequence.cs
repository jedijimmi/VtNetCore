using VtNetCore.VirtualTerminal.Enums;

namespace VtNetCore.XTermParser.SequenceType
{
    public class CharacterSizeSequence : TerminalSequence
    {
        public ECharacterSize Size { get; set; }

        public override string ToString()
        {
            return "Character size - " + Size;
        }
    }
}