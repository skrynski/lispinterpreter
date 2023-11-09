using System;
using System.Collections.Generic;
using System.Text;

namespace LispInterpreter
{
    public interface SExpressionVisitor
    {
        void VisitNil(SExpressionNil exp);
        void VisitAtom(SExpressionAtom exp);
        void VisitPair(SExpressionPair exp);
        void VisitString(SExpressionString exp);
        void VisitSymbol(SExpressionSymbol exp);
        void VisitPluggablePrimitive(PluggablePrimitive exp);
    }
}
