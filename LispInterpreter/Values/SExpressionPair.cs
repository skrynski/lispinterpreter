using System;
using System.Collections.Generic;
using System.Linq;

namespace LispInterpreter
{
    public class SExpressionPair : SExpression
    {
        public static SExpression  List(IEnumerable<SExpression> elements)
        {
            if (!elements.Any())
                return SExpression.NIL;

            else
                return new SExpressionPair(elements.First(), List(elements.Skip(1).ToList()));

        }

        


        public SExpression CAR { get; private set; }
        public SExpression CDR { get; private set; }

        public SExpressionPair(SExpression head, SExpression tail)
        {
            CAR = head;
            CDR = tail;
        }


        public override bool IsPair { get { return true; } }

        public override bool IsList { get { return CDR.IsList; } }

        public override string ToString()
        {
            if (!IsList)
                return "(" + CAR.ToString() + " . " + CDR.ToString() + ")";
            else
                return "(" + string.Join(" ",  ToList().Select(x => x.ToString())) + ")";

        }

        public override SExpression At(int index)
        {
            if (index < 0)
                throw new Exception("bad index");

            if (index == 0)
                return CAR;

            return CDR.At(index - 1);

           
        }

        public override IList<SExpression> ToList()
        {
            return new[] { CAR }.Concat(CDR.ToList()).ToList();
        }

    }
}
