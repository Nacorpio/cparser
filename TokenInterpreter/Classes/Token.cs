using TokenInterpreter.Enums;

namespace TokenInterpreter.Classes
{
    /// <summary>
    /// Represents a token, which is used to build nodes.
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Initializes an instance of the Token class.
        /// </summary>
        /// <param name="col">The column.</param>
        /// <param name="line">The line.</param>
        /// <param name="pos">The position.</param>
        /// <param name="type">The type.</param>
        public Token(int col, int line, int pos, TokenId type) : this(null, col, line, pos, type)
        {}

        /// <summary>
        /// Initializes an instance of the Token class.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="col">The column.</param>
        /// <param name="line">The line.</param>
        /// <param name="pos">The position.</param>
        /// <param name="type">The type.</param>
        public Token(dynamic obj, int col, int line, int pos, TokenId type)
        {
            Object = obj;
            Column = col;
            Line = line;
            Position = pos;
            Type = type;
        }

        /// <summary>
        /// Gets the object of this Token.
        /// </summary>
        public dynamic Object { get; }

        /// <summary>
        /// Gets the type of this Token.
        /// </summary>
        public TokenId Type { get; }

        /// <summary>
        /// Gets the column of this Token.
        /// </summary>
        public int Column { get; }

        /// <summary>
        /// Gets the line number of this Token.
        /// </summary>
        public int Line { get; }

        /// <summary>
        /// Gets the position of this Token.
        /// </summary>
        public int Position { get; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return $"type: {Type}, col: {Column}, line: {Line}, pos: {Position}";
        }
    }
}
