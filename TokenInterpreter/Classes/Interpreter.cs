using System.Collections.Generic;
using System.Linq;
using TokenInterpreter.Classes.Nodes;
using TokenInterpreter.Enums;

namespace TokenInterpreter.Classes
{
    public class Interpreter
    {
        private readonly TokenCollection _tokens;
        private int _lineCount;

        private readonly List<string> _structures;
        private readonly Dictionary<string, string> _typeDefinitions; 

        /// <summary>
        /// Initializes an instance of the <see cref="Interpreter"/> class.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        public Interpreter(TokenCollection tokens)
        {
            Errors = new List<Error>();

            _structures = new List<string>();
            _typeDefinitions = new Dictionary<string, string>();

            _tokens = tokens;
            Init();
        }

        /// <summary>
        /// Gets the current position of this <see cref="Interpreter"/>.
        /// </summary>
        public int Position { get; private set; }

        /// <summary>
        /// Gets the current token of this <see cref="Interpreter"/>.
        /// </summary>
        public Token Current { get; private set; }

        /// <summary>
        /// Gets the error messages of this <see cref="Interpreter"/>.
        /// </summary>
        public List<Error> Errors { get; }

        /// <summary>
        /// Initializes this <see cref="Interpreter"/>.
        /// </summary>
        private void Init()
        {
            Current = _tokens.First.Value;
        }

        /// <summary>
        /// Loops through all tokens, and builds a tree.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BaseNode> Build()
        {
            var nodes = new List<BaseNode>();
            while (Current.Type != TokenId.Eof)
            {
                Next();
                switch (Current.Type)
                {
                    case TokenId.Struct:
                    {
                        var value = ParseStructureNode();
                        if (value != null)
                        {
                            nodes.Add(value);
                        }
                        break;
                    }
                    case TokenId.TypeDef:
                    {
                        var value = ParseTypeDefNode();
                        if (value != null)
                        {
                            nodes.Add(value);
                        }
                        break;
                    }
                }
            }
            return nodes;
        }

        /// <summary>
        /// Parses a typedef node at the current position.
        /// </summary>
        /// <returns></returns>
        private TypeDefNode ParseTypeDefNode()
        {
            var node = new TypeDefNode(Current);

            if (Current.Type != TokenId.TypeDef)
            {
                Error($"Unexpected token (expected: {TokenId.TypeDef}): {Current.Type}");
                return node;
            }

            Next();

            switch (Current.Type)
            {
                case TokenId.Struct:
                    node.IsStructure = true;
                    break;
            }

            Next();

            if (Current.Type != TokenId.Identifier)
            {
                Error($"Unexpected token (expected: {TokenId.Identifier}): {Current.Type}");
                return node;
            }

            node.Type = (string) Current.Object;
            Next();

            if (Current.Type == TokenId.Star)
                node.Type += '*';

            Next();

            if (Current.Type != TokenId.Identifier)
            {
                Error($"Unexpected token (expected: {TokenId.Identifier}): {Current.Type}");
                return node;
            }

            node.Alias = (string) Current.Object;
            _typeDefinitions.Add(node.Alias, node.Type);

            return node;
        }

        /// <summary>
        /// Parses a structure node at the current position.
        /// </summary>
        /// <returns></returns>
        private StructureNode ParseStructureNode()
        {
            var node = new StructureNode(Current);

            /*
                struct example {};
                ^
            */
            if (Current.Type != TokenId.Struct)
            {
                Error($"Unexpected token (expected: {TokenId.Struct}): {Current.Type}");
                return node;
            }

            Next();

            /*
                struct example {}; or struct {};
                       ^                    ^ No identifier
            */
            if (Current.Type == TokenId.Identifier)
            {
                node.Name = (string) Current.Object;
                _structures.Add(node.Name);
            }

            Next();

            /*
                struct example {};
                               ^
            */
            if (Current.Type != TokenId.LCurly)
            {
                Error($"Unexpected token (expected: {TokenId.LCurly}): {Current.Type}");
                return null;
            }

            Next();

            // TODO: Parse the members of the structure.
            while (Current.Type != TokenId.RCurly)
            {
                var fieldNode = ParseFieldNode();
                if (fieldNode != null)
                {
                    node.Fields.Add(fieldNode);
                }
                
                Next();
            }

            return node;
        }

        /// <summary>
        /// Parses a field node at the current position.
        /// </summary>
        /// <returns></returns>
        private FieldNode ParseFieldNode()
        {
            var node = new FieldNode(Current);

            /*
                int var1;
                <->
            */
            if (_structures.Contains((string) Current.Object))
            {
                node.Type = (string) Current.Object;
            }
            else
            {
                if (Constants.Types.HasFlag(Current.Type))
                {
                    node.Type = Current.Type.ToString();
                }
                else
                {
                    Error($"Unknown field type: {Current.Type}");
                    return null;
                }
            }

            Next();
            node.Declaration = ParseDeclarationTokens();

            return node;
        }

        /// <summary>
        /// Parses a declaration at the current position.
        /// </summary>
        private TokenCollection ParseDeclarationTokens()
        {
            var tokens = new TokenCollection();

            /*
                void (*pt) (int par1, int par2);
                     <------------------------>
            */
            while (true)
            {
                if (Current.Type == TokenId.Semicolon || Current.Type == TokenId.Equals)
                {
                    return tokens;
                }

                tokens.AddLast(Current);
                Next();
            }
        }

        /// <summary>
        /// Adds an error to this <see cref="Interpreter"/>.
        /// </summary>
        /// <param name="error"></param>
        private void Error(Error error)
        {
            Errors.Add(error);
        }

        /// <summary>
        /// Adds an error with the specified message.
        /// </summary>
        /// <param name="msg">The message.</param>
        /// <param name="token">The token to retrieve the position from.</param>
        private void Error(string msg, Token token)
        {
            Errors.Add(new Error(msg, token.Line, token.Position));
        }

        /// <summary>
        /// Adds an error with the specified message using the current token.
        /// </summary>
        /// <param name="msg">The message.</param>
        private void Error(string msg)
        {
            Error(msg, Current);
        }

        /// <summary>
        /// Peeks the upcoming token without moving forward.
        /// </summary>
        /// <returns></returns>
        public Token Peek()
        {
            if (Position + 1 > _tokens.Count - 1)
            {
                return null;
            }

            return _tokens.ToArray()[Position + 1];
        }

        /// <summary>
        /// Moves to the next token.
        /// </summary>
        public void Next()
        {
            if (Position + 1 > _tokens.Count - 1)
            {
                // Reached the end of the collection.
                return;
            }

            Position++;
            Current = _tokens.ToArray()[Position];

            switch (Current.Type)
            {
                case TokenId.NewLine:
                    _lineCount++;
                    Next();
                    break;
            }
        }

        /// <summary>
        /// Moves the previous token.
        /// </summary>
        public void Previous()
        {
            if (Position - 1 < 0)
            {
                // Reached the beginning of the collection.
                return;
            }

            Position++;
            Current = _tokens.ToArray()[Position];
        }
    }
}
