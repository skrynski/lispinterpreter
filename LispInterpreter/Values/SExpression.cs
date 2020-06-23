using System;
using System.Collections.Generic;

namespace LispInterpreter
{
    public abstract class SExpression 
    {
        static Lazy<SExpressionNil> val = new Lazy<SExpressionNil>(() => new SExpressionNil());
        public static SExpression NIL { get { return val.Value; } }

        public static SExpression CreateLambda(SExpression args, SExpression body)
        {
            return SExpressionPair.List(new[]
            {
                new SExpressionSymbol("LAMBDA"),
                args,
                body

            });
        }


        public virtual SExpression At(int index)
        {
            throw new NotImplementedException();
        }


        public virtual bool IsAtom
        {
            get { return false; }
        }

        public virtual bool IsPair { get { return false; } }


    }
}
