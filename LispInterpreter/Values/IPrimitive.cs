using System;

namespace LispInterpreter
{
    public interface IPrimitive
    {
        SExpression Invoke(SExpression[] args, LispInterpreter.REPL.Environment env);
    }
}
