using System;
namespace LispInterpreter
{
    public class PluggablePrimitive : SExpression,  IPrimitive
    {
        Func<SExpression[], LispInterpreter.REPL.Environment, SExpression> action;

        public PluggablePrimitive(Func<SExpression[], LispInterpreter.REPL.Environment, SExpression> f)
        {
            action = f;
        }

        public SExpression Invoke(SExpression[] args, LispInterpreter.REPL.Environment env)
        {
            return action(args, env);

        }
    }
}
