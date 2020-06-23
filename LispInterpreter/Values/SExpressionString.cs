using System;
namespace LispInterpreter
{
    public class SExpressionString : SExpressionAtom
    {
        string value;

        public SExpressionString(string s)
        {
            value = s;
        }

        public override string ToString()
        {
            return "\"" + value + "\"";
        }
    }
}
