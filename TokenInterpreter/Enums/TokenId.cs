using System;

namespace TokenInterpreter.Enums
{
    /// <summary>
    /// Represents a token identifier.
    /// </summary>
    [Flags]
    public enum TokenId : byte
    {
        StringLiteral = 0x01,
        IntegerLiteral = 0x02,
        DoubleLiteral = 0x03,
        FloatLiteral = 0x04,
        CharLiteral = 0x05,

        Void = 0x06,
        Const = 0x07,
        Include = 0x08,

        // Keywords
        Short = 0x09,
        Struct = 0x0A,
        Int = 0x0B,
        Double = 0x0C,
        Float = 0x0D,
        Long = 0x0E,
        Return = 0x0F,
        Auto = 0x10,
        Break = 0x11,
        Case = 0x12,
        Char = 0x13,
        Continue = 0x14,
        Default = 0x15,
        Do = 0x16,
        Else = 0x17,
        Enum = 0x18,
        Extern = 0x19,
        For = 0x1A,
        Goto = 0x1B,
        If = 0x1C,
        Register = 0x1D,
        Signed = 0x1E,
        SizeOf = 0x1F,
        Static = 0x20,
        Switch = 0x21,
        TypeDef = 0x22,
        Union = 0x23,
        Unsigned = 0x24,
        Volatile = 0x25,
        While = 0x26,

        Comment = 0x27,
        MultiComment = 0x28,

        RArrow = 0x29,

        Whitespace = 0x2A,
        NewLine = 0x2B,
        Tab = 0x2C,

        Identifier = 0x2D,
        Keyword = 0x2E,
        Unknown = 0x2F,
        Undefined = 0x30,

        Percent = 0x31,
        Hash = 0x32,
        Dollar = 0x33,
        Euro = 0x34,
        Ampersand = 0x35,
        At = 0x36,
        Star = 0x37,

        RSquare = 0x38,
        LSquare = 0x39,
        RCurly = 0x3A,
        LCurly = 0x3B,
        RParant = 0x3C,
        LParant = 0x3D,
        Backslash = 0x3E,

        DoubleQuote = 0x3F,
        SingleQuote = 0x40,

        GreaterThanOrEqual = 0x41,
        LessThanOrEqual = 0x42,
        GreaterThan = 0x43,
        LessThan = 0x44,

        PlusPlus = 0x45,
        MinusMinus = 0x46,
        EqualEqual = 0x47,

        Plus = 0x48,
        Minus = 0x49,
        Exponent = 0x4A,
        Divide = 0x4B,
        RootSign = 0x4C,

        Apostrophe = 0x4D,
        Equals = 0x4E,
        Semicolon = 0x4F,
        Colon = 0x50,
        Underscore = 0x51,
        Period = 0x52,
        Comma = 0x53,
        Hyphen = 0x54,
        QuestionMark = 0x55,
        Line = 0x56,

        Type = 0x57,
        Eof = 0x58
    }
}
