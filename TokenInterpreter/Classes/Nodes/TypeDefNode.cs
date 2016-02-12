using System;
using System.Text;

namespace TokenInterpreter.Classes.Nodes
{
    public sealed class TypeDefNode : BaseNode
    {
        /// <summary>
        /// Initializes an instance of the BaseNode class.
        /// </summary>
        /// <param name="token">The token.</param>
        public TypeDefNode(Token token) : base(token)
        {}

        /// <summary>
        /// Gets the type to create an alias of.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets the alias of this BaseNode.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Gets whether the type is a structure.
        /// </summary>
        public bool IsStructure { get; set; }

        /// <summary>
        /// Converts the node to text using the specified string builder..
        /// </summary>
        /// <param name="sb">The string builder.</param>
        public override void ToSource(StringBuilder sb)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return base.ToString() + $", alias: {Alias}";
        }
    }
}
