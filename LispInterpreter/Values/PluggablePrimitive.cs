using System;
using LispInterpreter.REPL;

namespace LispInterpreter
{
    public class PluggablePrimitive : SExpression,  IPrimitive
    {
        Func<SExpression[], Evaluator, REPL.Environment, SExpression> action;

        public PluggablePrimitive(Func<SExpression[], Evaluator, REPL.Environment, SExpression> f, bool evaluatedArguments)
        {
            action = f;
            this.evaluatedArguments = evaluatedArguments;
        }

        public bool evaluatedArguments;
        public virtual bool EvaluatedArguments { get { return evaluatedArguments; } }



        public SExpression Invoke(SExpression[] args, Evaluator evaluator, REPL.Environment env)
        {
            return action(args, evaluator, env);

        }

        public override void Accept(SExpressionVisitor visitor)
        {
            visitor.VisitPluggablePrimitive(this);
        }

    }
}
