using System;
using System.Text.RegularExpressions;

namespace LispInterpreter.Lexer
{
    public class LexerRule
    {
        string regexPattern;
        string regextFirstCharacterPattern;


        public LexerRule(string regexPattern, string regextFirstCharacterPattern, string tokenType)
        {
            this.regexPattern = regexPattern;
            this.regextFirstCharacterPattern = regextFirstCharacterPattern;
            this.TokenType = tokenType;
        }

        public string TokenType { get; private set; }

        public bool Handles(char aCharacter)
        {
            return new Regex(regextFirstCharacterPattern, RegexOptions.None).Match(aCharacter.ToString()).Success;

        }

        public LexerToken Match(string str, int startIndex)
        {
            var match = new Regex(regexPattern).Match(str, startIndex);
            if (!match.Success)
                throw new Exception("Error while matching token");
            return new LexerToken(match.Value, TokenType, match.Index + match.Length - 1);



        }
    }
}
