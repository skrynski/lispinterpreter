using System;
using System.Collections.Generic;

namespace LispInterpreter
{
    public abstract class SExpression 
    {
        static Lazy<SExpressionNil> nilV = new Lazy<SExpressionNil>(() => new SExpressionNil());
        static Lazy<SExpression> trueV = new Lazy<SExpression>(() => new SExpressionSymbol("t"));
       

        public static SExpression True { get { return trueV.Value; } }
        public static SExpression NIL { get { return nilV.Value; } }

        public static SExpression False { get { return NIL; } }

        public static SExpression CreateLambda(SExpression args, SExpression body)
        {
            return SExpressionPair.List(new[]
            {
                new SExpressionSymbol("LAMBDA"),
                args,
                body

            });
        }


        public abstract void Accept(SExpressionVisitor visitor);

        public virtual SExpression At(int index)
        {
            throw new NotImplementedException();
        }


        public virtual bool IsAtom
        {
            get { return false; }
        }

        public virtual bool IsPair { get { return false; } }

        public virtual IList<SExpression> ToList()
        { 
            return new List<SExpression>();

        }

        public virtual bool IsTrue()
        {
            return true;

        }

        public virtual bool IsFalse()
        {
            return !IsTrue();

        }

        public static SExpression Bool(bool v)
        {
            return v ? SExpression.True : SExpression.False;
        }

        public virtual bool IsList { get { return false; } }
    }
}
