using System;
using System.Collections.Generic;
using System.Text;

namespace TokenInterpreter.Classes.Nodes
{
    /// <summary>
    /// Represents a C-type structure.
    /// </summary>
    public sealed class StructureNode : BaseNode
    {
        /// <summary>
        /// Initializes an instance of the BaseNode class.
        /// </summary>
        /// <param name="token">The token.</param>
        public StructureNode(Token token) : base(token)
        {
            Fields = new List<FieldNode>();
        }

        /// <summary>
        /// Gets an optional name for this StructureNode.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the fields of this StructureNode.
        /// </summary>
        public List<FieldNode> Fields { get; set; } 

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
            return base.ToString() + $", fields: {Fields.Count}";
        }
    }
}
