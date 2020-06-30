using System;
namespace LispInterpreter
{
    public class SExpressionSymbol : SExpressionAtom
    {

        string stringRepresentation;

        public SExpressionSymbol(string fromString)
        {
            stringRepresentation = fromString;
        }

        public string Value { get { return stringRepresentation; } }

        public bool IsNumber
        {
            get
            {
                int x;
                return int.TryParse(stringRepresentation, out x);
            }
        }

        public SExpression AsNumberExpression()
        {
            return new SExpressionSymbol(int.Parse(stringRepresentation).ToString());
   
        }

        public override string ToString()
        {
            return stringRepresentation;
        }
    }
}
