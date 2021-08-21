using System;

namespace HeimerNET.Lor.DeckCodes
{
    /// <summary>
    /// Represents errors that occur during Decode/encode deckcode.
    /// </summary>
    public class DecodingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DecodingException"/> class.
        /// </summary>
        public DecodingException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DecodingException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DecodingException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DecodingException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a <see langword="null"/> reference, if no inner exception is specified.</param>
        public DecodingException(string message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
