using System;
using System.IO;
using System.Text;
using TokenInterpreter.Enums;

namespace TokenInterpreter.Classes
{
    /// <summary>
    /// Represents a reader which reads tokens in its order.
    /// </summary>
    public class TokenReader
    {
        private readonly TextReader src;

        /// <summary>
        /// Initializes an instance of the TokenReader class.
        /// </summary>
        /// <param name="reader">The reader to read from.</param>
        public TokenReader(TextReader reader)
        {
            src = reader;
            Tokens = new TokenCollection();
        }

        /// <summary>
        /// Gets the current character of this TokenReader.
        /// </summary>
        public int Char { get; private set; }

        /// <summary>
        /// Gets the current line of this TokenReader.
        /// </summary>
        public int Line { get; private set; } = 1;

        /// <summary>
        /// Gets the current column of this TokenReader.
        /// </summary>
        public int Column { get; private set; } = 1;

        /// <summary>
        /// Gets the current zero-based position of this TokenReader.
        /// </summary>
        public int Position { get; private set; }

        /// <summary>
        /// Gets the tokens of this TokenReader.
        /// </summary>
        public TokenCollection Tokens { get; }

        private void Add(TokenId id)
        {
            Add(id, Column, Line, Position);
        }

        private void Add(TokenId id, dynamic value)
        {
            Add(id, value, Column, Line, Position);
        }

        private void Add(TokenId id, int col)
        {
            Add(id, col, Line, Position);
        }

        private void Add(TokenId id, int col, int line, int pos)
        {
            Add(id, null, col, line, pos);
        }

        private void Add(TokenId id, dynamic obj, int col, int line, int pos)
        {
            Tokens.AddLast(new Token(obj, col, line, pos, id));
        }

        private void NewLine()
        {
            Column = 1;
            Line++;
        }

        private void AddKeyword(string id, int startCol)
        {
            if (string.IsNullOrEmpty(id))
            {
                return;
            }

            switch (id)
            {
                case "void":
                    {
                        Add(TokenId.Void, startCol);
                        break;
                    }
                case "return":
                    {
                        Add(TokenId.Return, startCol);
                        break;
                    }
                case "int":
                    {
                        Add(TokenId.Int, startCol);
                        break;
                    }
                case "const":
                    {
                        Add(TokenId.Const, startCol);
                        break;
                    }
                case "short":
                    {
                        Add(TokenId.Short, startCol);
                        break;
                    }
                case "long":
                    {
                        Add(TokenId.Long, startCol);
                        break;
                    }
                case "default":
                    {
                        Add(TokenId.Default, startCol);
                        break;
                    }
                case "auto":
                    {
                        Add(TokenId.Auto, startCol);
                        break;
                    }
                case "break":
                    {
                        Add(TokenId.Break, startCol);
                        break;
                    }
                case "case":
                    {
                        Add(TokenId.Case, startCol);
                        break;
                    }
                case "char":
                    {
                        Add(TokenId.Char, startCol);
                        break;
                    }
                case "continue":
                    {
                        Add(TokenId.Continue, startCol);
                        break;
                    }
                case "else":
                    {
                        Add(TokenId.Else, startCol);
                        break;
                    }
                case "enum":
                    {
                        Add(TokenId.Enum, startCol);
                        break;
                    }
                case "double":
                    {
                        Add(TokenId.Double, startCol);
                        break;
                    }
                case "extern":
                    {
                        Add(TokenId.Extern, startCol);
                        break;
                    }
                case "float":
                    {
                        Add(TokenId.Float, startCol);
                        break;
                    }
                case "for":
                    {
                        Add(TokenId.For, startCol);
                        break;
                    }
                case "goto":
                    {
                        Add(TokenId.Goto, startCol);
                        break;
                    }
                case "if":
                    {
                        Add(TokenId.If, startCol);
                        break;
                    }
                case "register":
                    {
                        Add(TokenId.Register, startCol);
                        break;
                    }
                case "signed":
                    {
                        Add(TokenId.Signed, startCol);
                        break;
                    }
                case "sizeof":
                    {
                        Add(TokenId.SizeOf, startCol);
                        break;
                    }
                case "static":
                    {
                        Add(TokenId.Static, startCol);
                        break;
                    }
                case "struct":
                    {
                        Add(TokenId.Struct, startCol);
                        break;
                    }
                case "switch":
                    {
                        Add(TokenId.Switch, startCol);
                        break;
                    }
                case "typedef":
                    {
                        Add(TokenId.TypeDef, startCol);
                        break;
                    }
                case "union":
                    {
                        Add(TokenId.Union, startCol);
                        break;
                    }
                case "unsigned":
                    {
                        Add(TokenId.Unsigned, startCol);
                        break;
                    }
                case "volatile":
                    {
                        Add(TokenId.Volatile, startCol);
                        break;
                    }
                case "while":
                    {
                        Add(TokenId.While, startCol);
                        break;
                    }
                default:
                    {
                        Add(TokenId.Identifier, id, startCol, Line, Position);
                        break;
                    }
            }
        }

        /// <summary>
        /// Reads the next character from the underlying reader.
        /// </summary>
        /// <param name="colIncrement">The amount to increment the current column by.</param>
        private void Read(int colIncrement = 1)
        {
            Char = src.Read();
            Column += colIncrement;
        }

        /// <summary>
        /// Reads all the tokens from the underlying reader.
        /// </summary>
        public void ReadAll()
        {
            var sb = new StringBuilder();
            Read();

            readLoop:
            while (Char != -1)
            {
                switch (Char)
                {
                    case -1:
                        {
                            goto readLoop;
                        }

                    case '\t':
                        {
                            while (Char == '\t')
                            {
                                Read(4);
                            }
                            break;
                        }

                    case ' ':
                        while (Char == ' ')
                        {
                            Read();
                        }
                        break;

                    case '\r':
                        {
                            Read();
                            if (Char == '\n')
                                Read();
                            goto addNewLine;
                        }

                    case '\n':

                        Read();

                        addNewLine:
                        Add(TokenId.NewLine);
                        NewLine();

                        break;

                    case '/':
                        {
                            Read();

                            #region Single comment

                            if (Char == '/')
                            {
                                var startColumn = Column;

                                Read();
                                sb.Length = 0;

                                while (Char != -1)
                                {
                                    if (Char == '\n' || Char == '\r' || Char == '\t')
                                    {
                                        break;
                                    }

                                    sb.Append((char)Char);
                                    Read();
                                }

                                Add(TokenId.Comment, sb.ToString(), startColumn, Line, Position);
                            }

                            #endregion Single comment

                            #region Multi comment

                            else if (Char == '*')
                            {
                                var startColumn = Column;

                                Read();

                                sb.Length = 0;
                                Read();

                                for (bool exit = false; !exit;)
                                {
                                    switch (Char)
                                    {
                                        case '*':
                                            {
                                                Read();
                                                if (Char == -1 || Char == '/')
                                                {
                                                    Read();
                                                    exit = true;
                                                }
                                                else
                                                {
                                                    sb.Append('*');
                                                }
                                                break;
                                            }
                                        case '\n':
                                            {
                                                Read();
                                                Column = 1;
                                                break;
                                            }
                                        case '\r':
                                            {
                                                Read();

                                                if (Char == '\n')
                                                    Read();

                                                sb.Append(Environment.NewLine);

                                                NewLine();
                                                break;
                                            }
                                        case -1:
                                            {
                                                exit = true;
                                                break;
                                            }
                                        default:
                                            {
                                                sb.Append((char)Char);
                                                Read();
                                                break;
                                            }
                                    }
                                }
                                Add(TokenId.MultiComment, sb.ToString(), startColumn, Line, Position);
                            }

                            #endregion Multi comment

                            break;
                        }

                    case '#':
                        {
                            #region Include

                            var startCol = Column;

                            sb.Length = 0;
                            Read();
                            Column++;

                            while (Char != ' ')
                            {
                                var c = (char)Char;
                                sb.Append(c);
                                Read();
                                Column++;
                            }

                            if (sb.ToString() == "include")
                            {
                                Read();

                                var c = (char)Char;
                                if (c != '<')
                                {
                                    break;
                                }

                                Read();
                                Column++;

                                sb.Length = 0;
                                while (Char != '>')
                                {
                                    sb.Append((char)Char);
                                    Read();
                                    Column++;
                                }

                                Tokens.AddLast(new Token(sb.ToString(), startCol, Line, Position, TokenId.Include));
                            }

                            #endregion Include

                            break;
                        }

                    case '{':
                        {
                            Read();
                            Add(TokenId.LCurly);
                            break;
                        }

                    case '}':
                        {
                            Read();
                            Add(TokenId.RCurly);
                            break;
                        }

                    case '(':
                        {
                            Read();
                            Add(TokenId.LParant);
                            break;
                        }

                    case ')':
                        {
                            Read();
                            Add(TokenId.RParant);
                            break;
                        }

                    case '-':
                        {
                            Read();
                            Tokens.AddLast(Char == '>' ? new Token(Column - 1, Line, Position, TokenId.RArrow) : new Token(Column, Line, Position, TokenId.GreaterThan));
                            break;
                        }

                    case '&':
                        {
                            Read();
                            Add(TokenId.Ampersand);
                            break;
                        }

                    case ';':
                        {
                            Read();
                            Add(TokenId.Semicolon);
                            break;
                        }

                    case ',':
                        {
                            Read();
                            Add(TokenId.Comma);
                            break;
                        }

                    case '*':
                        {
                            Read();
                            Add(TokenId.Star);
                            break;
                        }

                    case '"':
                        {
                            #region String literal (double-quote)

                            var startCol = Column;

                            sb.Length = 0;
                            var quote = Char;

                            Read();

                            while (Char != -1)
                            {
                                var c = (char)Char;
                                if (Char == '\"')
                                {
                                    break;
                                }

                                if (Char == '\r')
                                {
                                    sb.Append("\\r");
                                    Read();

                                    if (Char != '\n')
                                    {
                                        continue;
                                    }

                                    sb.Append("\\n");
                                    Read();

                                    NewLine();
                                }
                                else if (Char == '\n')
                                {
                                    sb.Append("\\n");
                                    Read();

                                    NewLine();
                                }
                                else
                                {
                                    if (Char == quote)
                                    {
                                        break;
                                    }

                                    sb.Append(c);
                                    Read();
                                }
                            }

                            if (Char != -1)
                            {
                                if (Char == quote)
                                {
                                    Read();
                                }

                                Add(TokenId.StringLiteral, sb.ToString(), startCol, Line, Position);
                            }

                            #endregion String literal (double-quote)

                            break;
                        }

                    case '\'':
                        {
                            #region Character literal (single-quote)

                            var startCol = Column;
                            sb.Length = 0;

                            Read();
                            switch (Char)
                            {
                                case '\\':
                                    {
                                        Read();
                                        switch (Char)
                                        {
                                            case 'n':
                                                sb.Append("\\n");
                                                break;

                                            case 't':
                                                sb.Append("\\t");
                                                break;

                                            case 'r':
                                                sb.Append("\\r");
                                                break;
                                        }
                                        break;
                                    }
                                default:
                                    {
                                        sb.Append((char)Char);
                                        break;
                                    }
                            }

                            Read();
                            if (Char != '\'')
                            {
                                // Char literals can only have one character.
                                break;
                            }

                            Add(TokenId.CharLiteral, sb.ToString(), startCol, Line, Position);

                            #endregion Character literal (single-quote)

                            break;
                        }

                    default:
                        {
                            #region Keywords & Identifiers

                            var startColumn = Column;
                            var character = (char)Char;

                            if (char.IsLetter(character) && !char.IsSymbol(character))
                            {
                                if (character == '\n')
                                {
                                    goto case '\n';
                                }

                                if (character == '\r')
                                {
                                    goto case '\r';
                                }

                                sb.Length = 0;
                                sb.Append(character);

                                Read();

                                while (Char != -1)
                                {
                                    var c = (char)Char;

                                    if (c == '\r' || c == '\t' || c == ' ' || c == '(' || c == ')' || c == '*' || c == ';' || c == ',' || c == '#')
                                    {
                                        break;
                                    }

                                    if (c == ',')
                                    {
                                        goto case ',';
                                    }

                                    if (c == '-')
                                    {
                                        Read();
                                        if (Char == '>')
                                        {
                                            Tokens.AddLast(new Token(Column, Line, Position, TokenId.RArrow));
                                            break;
                                        }
                                    }

                                    sb.Append(c);
                                    Read();
                                }

                                AddKeyword(sb.ToString(), startColumn);
                            }
                            else
                            {
                                Read();
                            }

                            #endregion Keywords & Identifiers

                            break;
                        }
                }
                Position++;
            }
            Add(TokenId.Eof);
        }
    }
}