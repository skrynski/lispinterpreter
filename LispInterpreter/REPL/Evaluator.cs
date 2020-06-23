using System;
using System.Collections.Generic;
using System.Linq;

namespace LispInterpreter.REPL
{
    public class Evaluator
    {
        public Evaluator()
        {
        }

        public SExpression Eval(SExpression expression, Environment env)
        {
            if (expression is SExpressionString)
            {
                return expression;
            }
            else if (expression is SExpressionSymbol)
            {
                var symbol = (SExpressionSymbol)expression;
                if (symbol.IsNumber)
                    return symbol.AsNumberExpression();
                else
                    return env.Lookup(symbol.ToString());
            }
            else if (expression is SExpressionPair)
            {
                var pair = (SExpressionPair)expression;
                if (pair.CAR.ToString() == "QUOTE")
                {
                    return ((SExpressionPair)pair.CDR).CAR;
                }
                else if (pair.CAR.ToString() == "LAMBDA")
                {
                    var args = pair.At(1);
                    var body = pair.At(2);

                    return SExpressionPair.List(new SExpression[] {
                        new SExpressionSymbol("CLOSURE"),
                        args,
                        body,
                        env
                    });
                }
                else
                {
                    var func = Eval(pair.CAR, env);
                    var args = pair.CDR;

                    var argsAsList = args.ToList();
                    var evaluatedArgsAsList = argsAsList.Select(x => Eval(x, env)).ToList();
                    //var evaluatedArgs = SExpressionPair.List(evaluatedArgsAsList.ToArray());
                    return Apply(func, evaluatedArgsAsList);


                }

            }


            throw new Exception("Eval: Invalid expression");
        }

        private SExpression Apply(SExpression func, IList<SExpression> evaluatedArgsAsList)
        {
            if (func is IPrimitive)
            {
                var prim = (IPrimitive)func;
                return prim.Invoke(evaluatedArgsAsList.ToArray());
            }
            else
            {
                //var pair = (SExpressionPair)func;
                var closureSymbol = func.At(0);
                if (closureSymbol.ToString() != "CLOSURE")
                    throw new Exception("Apply: Invalid CLOSURE to evaluate");
                var args = func.At(1);
                var body = func.At(2);
                var closureEnv = (Environment) func.At(3);

                var argsAsList = args.ToList();

                var frameEnv = new Environment();
                frameEnv.Parent = closureEnv;
                foreach (var x in argsAsList.Zip(evaluatedArgsAsList))
                {
                    frameEnv.Define(x.First.ToString(), x.Second);
                }

                return Eval(body, frameEnv);

            }
        }
    }
}
