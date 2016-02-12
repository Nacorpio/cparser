using TokenInterpreter.Enums;

namespace TokenInterpreter.Classes
{
    /// <summary>
    /// This class stores all constants.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Gets the types that are in the C-language by default.
        /// </summary>
        public static TokenId Types = TokenId.Short | TokenId.Int | TokenId.Long | TokenId.Char | TokenId.Double | TokenId.Float | TokenId.Void;

        /// <summary>
        /// Gets the modififers that can be combined with a parameter.
        /// </summary>
        public static TokenId ParameterModifiers = TokenId.Const | TokenId.Volatile;
    }
}
