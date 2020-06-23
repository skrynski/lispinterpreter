using System;
namespace LispInterpreter
{
    public class SExpressionNil : SExpressionAtom
    {
    

        public SExpressionNil()
        {
        }

        public override string ToString()
        {
            return "()";
        }
    }
}
