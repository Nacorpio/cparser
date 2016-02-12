using System;
using System.Text;

namespace TokenInterpreter.Classes.Nodes
{
    public class AssignmentNode : BaseNode
    {
        /// <summary>
        /// Initializes an instance of the AssignmentNode class.
        /// </summary>
        /// <param name="token">The token.</param>
        public AssignmentNode(Token token) : base(token)
        {}

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
