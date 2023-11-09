using System;
namespace LispInterpreter
{
    public abstract class SExpressionAtom : SExpression
    {
        public SExpressionAtom()
        {
        }

        public override bool IsAtom
        {
            get { return true; }
        }

        public override SExpression At(int index)
        {
            if (index != 0)
                throw new Exception("bad index");

            return this;


        }

        public override void Accept(SExpressionVisitor visitor)
        {
            visitor.VisitAtom(this);
        }

    }
}
