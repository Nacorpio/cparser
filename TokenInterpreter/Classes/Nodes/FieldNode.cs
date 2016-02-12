using System;
using System.Text;

namespace TokenInterpreter.Classes.Nodes
{
    /// <summary>
    /// Represents a C-type field.
    /// </summary>
    public class FieldNode : BaseNode
    {
        /// <summary>
        /// Initializes an instance of the <see cref="FieldNode"/> class.
        /// </summary>
        /// <param name="token">The token.</param>
        public FieldNode(Token token) : base(token)
        {
            Declaration = new TokenCollection();
            Assignment = new TokenCollection();
        }

        /// <summary>
        /// Gets the name of this <see cref="FieldNode"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the data type of this <see cref="FieldNode"/>.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets the declaration of this <see cref="FieldNode"/>.
        /// </summary>
        public TokenCollection Declaration { get; set; }

        /// <summary>
        /// Gets the assignment of this <see cref="FieldNode"/>.
        /// </summary>
        public TokenCollection Assignment { get; set; }

        /// <summary>
        /// Determines whether this <see cref="FieldNode"/> references to a structure.
        /// </summary>
        public bool IsStruct { get; set; }

        /// <summary>
        /// Converts the node to text using the specified string builder.
        /// </summary>
        /// <param name="sb">The string builder.</param>
        public override void ToSource(StringBuilder sb)
        {
            throw new NotImplementedException();
        }
    }
}
