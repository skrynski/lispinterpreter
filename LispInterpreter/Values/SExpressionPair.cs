using System;
using System.Collections.Generic;
using System.Linq;

namespace LispInterpreter
{
    public class SExpressionPair : SExpression
    {
        public static SExpression  List(IList<SExpression> elements)
        {
            if (elements.Count == 0)
                return SExpression.NIL;

            else
                return new SExpressionPair(elements.First(), List(elements.Skip(1).ToList()));

        }

        public static IList<SExpression> ToList(SExpression exp)
        {
            if (exp is SExpressionPair)
            {
                var pair = (SExpressionPair)exp;
                 return new[] { pair.CAR}.Concat(ToList(pair.CDR)).ToList();
            }

            else
                return new List<SExpression>();

        }


        public SExpression CAR { get; private set; }
        public SExpression CDR { get; private set; }

        public SExpressionPair(SExpression head, SExpression tail)
        {
            CAR = head;
            CDR = tail;
        }


        public override bool IsPair { get { return true; } }

        public override string ToString()
        {
            return "(" + CAR.ToString() + " . " + CDR.ToString() + ")";
        }

        public override SExpression At(int index)
        {
            if (index < 0)
                throw new Exception("bad index");

            if (index == 0)
                return CAR;

            return CDR.At(index - 1);

           
        }
    }
}
