using System;
namespace LispInterpreter
{
    public class PluggablePrimitive : SExpression,  IPrimitive
    {
        Func<SExpression[], SExpression> action;

        public PluggablePrimitive(Func<SExpression[], SExpression> f)
        {
            action = f;
        }

        public SExpression Invoke(SExpression[] args)
        {
            return action(args);

        }
    }
}
