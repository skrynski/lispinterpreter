using System;
namespace LispInterpreter
{
    public class SExpressionParser
    {
        public SExpressionParser()
        {
        }


        public SExpression Parse(string s)
        {
            return Parse(s.Trim(), 0);
        }
        public SExpression Parse(string s, int startingIndex)
        {
            var input = s.Substring(startingIndex);
            if (input.StartsWith("\""))
            {
                var endQuoteIndex = input.IndexOf("\"", 1);
                var value = input.Substring(1, endQuoteIndex - 1);
                return new SExpressionString(value);

            }
            else if (!input.StartsWith("("))
            {
                return new SExpressionSymbol(input);
            } 
            else if (input.StartsWith("("))
            {

            }
            else
            { }



           throw new Exception ("Parse error");
        }
    }
}
