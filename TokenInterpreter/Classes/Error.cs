namespace TokenInterpreter.Classes
{
    /// <summary>
    /// Represents a position-specific error.
    /// </summary>
    public struct Error
    {
        /// <summary>
        /// Initializes an instance of the Error structure.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="line">The line of the error.</param>
        /// <param name="col">The column of the error.</param>
        public Error(string message, int line, int col)
        {
            Message = message;
            Line = line;
            Column = col;
        }

        /// <summary>
        /// Gets the message of this Error.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the line of this Error.
        /// </summary>
        public int Line { get; }

        /// <summary>
        /// Gets the column of this Error.
        /// </summary>
        public int Column { get; }

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        public override string ToString()
        {
            return $"{Column}@{Line}: \"{Message}\"";
        }
    }
}
