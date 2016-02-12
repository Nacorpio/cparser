using System.Collections.Generic;
using System.Linq;
using TokenInterpreter.Enums;

namespace TokenInterpreter.Classes
{
    /// <summary>
    /// Represents a collection of tokens.
    /// </summary>
    public sealed class TokenCollection : LinkedList<Token>
    {
        /// <summary>
        /// Gets the identifiers of this <see cref="TokenCollection"/>.
        /// </summary>
        /// <returns></returns>
        public TokenId[] ToIdentifiers()
        {
            return this.ToArray().Select(tid => tid.Type).ToArray();
        }

        /// <summary>
        /// Replace all occurences of the specified token identifier with the specified token.
        /// </summary>
        /// <param name="id">The token identfier to replace.</param>
        /// <param name="token">The token to replace with.</param>
        public void ReplaceAll(TokenId id, Token token)
        {
            var array = this.ToArray();
            for (var i = 0; i < array.Length; i++)
            {
                if (array[i].Type != id)
                    return;

                array.SetValue(token, i);
            }
        }

        /// <summary>
        /// Finds the last index of the specified token identifier.
        /// </summary>
        /// <param name="id">The token identifier.</param>
        /// <param name="startIndex">The starting index.</param>
        /// <returns></returns>
        public int LastIndexOf(TokenId id, int startIndex = 0)
        {
            var indexes = IndexesOf(id, startIndex);
            if (indexes.Length == 0)
            {
                return -1;
            }
            return indexes[indexes.Length - 1];
        }

        /// <summary>
        /// Finds all the indexes of the specified token identifier.
        /// </summary>
        /// <param name="id">The token identifier.</param>
        /// <param name="startIndex">The starting index.</param>
        /// <returns></returns>
        public int[] IndexesOf(TokenId id, int startIndex = 0)
        {
            var results = new List<int>();
            var array = this.ToArray();

            for (var i = startIndex; i < array.Length; i++)
            {
                var index = IndexOf(id, i);
                if (index != -1 && !results.Contains(index))
                {
                    results.Add(index);
                }
            }
            return results.ToArray();
        }

        /// <summary>
        /// Finds the first index of the specified token identifier.
        /// </summary>
        /// <param name="id">The token identifier.</param>
        /// <param name="startIndex">The starting index.</param>
        /// <returns></returns>
        public int IndexOf(TokenId id, int startIndex = 0)
        {
            var array = this.ToArray();
            for (var i = startIndex; i < array.Length; i++)
            {
                if (array[i].Type == id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
