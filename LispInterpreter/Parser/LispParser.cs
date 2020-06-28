using System;
using System.Collections.Generic;
using LispInterpreter.Lexer;

namespace LispInterpreter.Parser
{
    public class LispParser
    {

        /*
         
  
        S -> ATOM | LIST
        ATOM -> SYMBOL | NUMBER | STRING
        LIST -> ( MEMBERS ) |  ()
        MEMBERS -> S MEMBERS
        
        */

        public SExpression Parse(IList<LexerToken> tokens)
        {
            var s = ParseS(tokens, 0);

            if (s == null)
                throw new Exception("Error parsing expression");


            return s;
        }

        private SExpression ParseS(IList<LexerToken> tokens, int index)
        {
            

            var atom = ParseAtom(tokens[index]);

            if (atom != null)
            {
                lastParsedIndex = index;
                return atom;
            }

            var list = ParseList(tokens, index);

            if (list != null)
                return list;

            return null;
        }

        int lastParsedIndex = 0;

        private SExpression ParseList(IList<LexerToken> tokens, int index)
        {
          

            var openParens = (tokens[index].TokenType == "OPEN_PARENS");
            if (!openParens)
                return null;

            var closeParens = (tokens[index+1].TokenType == "CLOSE_PARENS");
            if (closeParens)
            {
                lastParsedIndex = index + 1;
                return SExpression.NIL;
            }

            lastParsedIndex = index;
            var members = ParseMembers(tokens, index + 1);

            if (members == null)
                return null;

            var closeParens2 = (tokens[lastParsedIndex + 1].TokenType == "CLOSE_PARENS");
            if (closeParens2)
            {
                lastParsedIndex++;
                return members;

            }
                


          

            return null;
        }

        private SExpression ParseMembers(IList<LexerToken> tokens, int index)
        {
            if (index >= tokens.Count)
                return null;

            var head = ParseS(tokens, index);
            if (head == null)
                return null;

            var rest = ParseMembers(tokens, lastParsedIndex + 1);

            if (rest == null)
                return new SExpressionPair(head, SExpression.NIL);

            return new SExpressionPair(head, rest);


        }


        private SExpression ParseAtom(LexerToken token)
        {
            if (token.TokenType == "SYMBOL" || token.TokenType == "NUMBER")
            {
                //lastParsedIndex++;
                return new SExpressionSymbol(token.Value);
            }

            else if (token.TokenType == "STRING")
            {
                var x = token.Value.Substring(1);
                return new SExpressionString(x.Substring(0, x.Length -1)) ;
            }

            return null;


        }


       
    }
}
