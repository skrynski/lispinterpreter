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

        public string Value { get { return value; } }

        public override string ToString()
        {
            return "\"" + value + "\"";
        }

        public override void Accept(SExpressionVisitor visitor)
        {
            visitor.VisitString(this);
        }

    }
}
