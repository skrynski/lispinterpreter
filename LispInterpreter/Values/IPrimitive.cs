using System;
using LispInterpreter.REPL;

namespace LispInterpreter
{
    public interface IPrimitive
    {
        SExpression Invoke(SExpression[] args, Evaluator evaluator, REPL.Environment env);

        bool EvaluatedArguments { get;  }
    }
}
