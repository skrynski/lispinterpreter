using System;
namespace LispInterpreter.Lexer
{
    public class LexerToken
    {


        public LexerToken(string value, string tokenType, int endIndex)
        {
            this.Value = value;
            this.TokenType = tokenType;
            this.EndIndex = endIndex;
        }

        public string Value { get; private set; }
        public string TokenType { get; private set; }
        public int EndIndex { get; private set; }

        public override string ToString()
        {
            return $"{TokenType} : {Value}";

        }
    }
}
