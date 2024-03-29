﻿using System;
namespace LispInterpreter
{
    public class SExpressionNil : SExpressionAtom
    {
    

        public SExpressionNil()
        {
        }


        public override void Accept(SExpressionVisitor visitor)
        {
            visitor.VisitNil(this);
        }


        public override string ToString()
        {
            return "()";
        }

        public override bool IsTrue()
        {
            return false;

        }

        public override bool IsList { get { return true; } }
    }
}
