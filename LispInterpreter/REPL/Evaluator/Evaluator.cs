using System;
using System.Collections.Generic;
using System.Text;

namespace LispInterpreter.REPL
{
    public class Evaluator
    {
        public Evaluator()
        { 
        }

        public SExpression Eval(SExpression expression, Environment env)
        {
            return new EvaluatorVisitor(expression, env).Eval();
        }

    }
}
