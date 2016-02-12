using System.Text;

namespace TokenInterpreter.Classes.Nodes
{
    /// <summary>
    /// Represents a parameter inside a function or constructor.
    /// </summary>
    public class ParameterNode : BaseNode
    {
        /// <summary>
        /// Initializes an instance of the ParameterNode class.
        /// </summary>
        /// <param name="token">The token.</param>
        public ParameterNode(Token token) : base(token)
        {}

        /// <summary>
        /// Gets the name of this ParameterNode.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Determines whether this ParameterNode is a pointer.
        /// </summary>
        public bool IsPointer { get; set; }

        /// <summary>
        /// Converts the node to text using the specified string builder.
        /// </summary>
        /// <param name="sb">The string builder.</param>
        public override void ToSource(StringBuilder sb)
        {
            throw new System.NotImplementedException();
        }
    }
}
