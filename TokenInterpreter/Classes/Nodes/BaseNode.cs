using System.Text;
using TokenInterpreter.Interfaces;

namespace TokenInterpreter.Classes.Nodes
{
    /// <summary>
    /// Represents the class which has to be inherited by all nodes.
    /// </summary>
    public abstract class BaseNode : ISourceCode
    {
        /// <summary>
        /// Initializes an instance of the BaseNode class.
        /// </summary>
        /// <param name="token">The token.</param>
        protected BaseNode(Token token)
        {
            Token = token;
        }

        /// <summary>
        /// Gets the token of this BaseNode.
        /// </summary>
        public Token Token { get; }

        /// <summary>
        /// Converts the node to text using the specified string builder..
        /// </summary>
        /// <param name="sb">The string builder.</param>
        public abstract void ToSource(StringBuilder sb);

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return Token.ToString();
        }
    }
}
