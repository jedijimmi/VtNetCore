using System;
using System.Text;
using VtNetCore.XTermParser.SequenceType;

namespace VtNetCore.Exceptions
{
    /// <summary>
    ///     An exception which is thrown when there is a problem decoding or processing an escape sequence.
    /// </summary>
    public class EscapeSequenceException : Exception
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="message">The message for the exception</param>
        /// <param name="buffer">The raw data buffer containing the problem</param>
        public EscapeSequenceException(string message, byte[] buffer) :
            base(message)
        {
            Buffer = buffer;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="message">The message for the exception</param>
        /// <param name="sequence">The decoded sequence</param>
        public EscapeSequenceException(string message, TerminalSequence sequence) :
            base(message)
        {
            Sequence = sequence;
        }

        /// <summary>
        ///     The raw buffer if present
        /// </summary>
        public byte[] Buffer { get; set; }

        /// <summary>
        ///     The decoded sequence if present
        /// </summary>
        public TerminalSequence Sequence { get; set; }

        /// <summary>
        ///     A conversion function to make debugging a little easier.
        /// </summary>
        public string BufferTextUTF8 => Encoding.UTF8.GetString(Buffer);
    }
}