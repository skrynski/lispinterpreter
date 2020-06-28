using System;
using System.Text.RegularExpressions;

namespace LispInterpreter.Lexer
{
    public class LexerRule
    {
        string regexPattern;
        Func<char, bool> matchFirstCharacter;




        public LexerRule(string regexPattern, string tokenType, Func<char, bool> matchFirstCharacter = null)
        {
            this.regexPattern = regexPattern;
           
            this.TokenType = tokenType;
            this.matchFirstCharacter = matchFirstCharacter;
        }

        public string TokenType { get; private set; }

        public bool Handles(char aCharacter)
        {
            if (matchFirstCharacter != null)
                return matchFirstCharacter(aCharacter);

            return new Regex(regexPattern, RegexOptions.None).Match(aCharacter.ToString()).Success;

        }

        public LexerToken Match(string str, int startIndex)
        {
            var match = new Regex(regexPattern).Match(str, startIndex);
            if (!match.Success)
                throw new Exception("Error while matching token");
            if (match.Index != startIndex)
                throw new Exception("Error while matching token");

            return new LexerToken(match.Value, TokenType, match.Index + match.Length - 1);



        }
    }
}
