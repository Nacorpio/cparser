using System.Text;

namespace TokenInterpreter.Interfaces
{
    public interface ISourceCode
    {
        /// <summary>
        /// Converts the node to text using the specified string builder..
        /// </summary>
        /// <param name="sb">The string builder.</param>
        void ToSource(StringBuilder sb);
    }
}
