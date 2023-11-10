using System;
using System.Collections.Generic;
using System.Linq;

namespace LispInterpreter.REPL
{
    public class EvaluatorVisitor : SExpressionVisitor
    {
        Evaluator evaluator;
        SExpression result = null;
        SExpression expression;
        Environment env;
        public EvaluatorVisitor(Evaluator evaluator, SExpression expression, Environment env)
        {
            this.expression = expression;
            this.env = env;
            this.evaluator = evaluator;
          
        }

        

        public SExpression Eval()
        {
            expression.Accept(this);
            return result;

           
        }

        private void NotImplemented() 
        {
            throw new Exception("Eval: Invalid expression");

        }

        public virtual void VisitAtom(SExpressionAtom exp)
        {
            NotImplemented();
        }

        public virtual void VisitNil(SExpressionNil exp)
        {
            NotImplemented();
        }

        public virtual void VisitPair(SExpressionPair pair)
        {

            if (pair.CAR.ToString() == "lambda")
            {
                var args = pair.At(1);
                var body = pair.At(2);

                result = SExpressionPair.List(new SExpression[] {
                        new SExpressionSymbol("closure"),
                        args,
                        body,
                        env
                    });
            }
            else
            {
                var func = evaluator.Eval(pair.CAR, env);
                var args = pair.CDR;
                var argsAsList = args.ToList();

                if (func is IPrimitive && !((IPrimitive)func).EvaluatedArguments)
                {
                    var prim = (IPrimitive)func;
                    var res = prim.Invoke(argsAsList.ToArray(), evaluator, env);
                    result = res;
                }
                else
                {
                    var evaluatedArgsAsList = argsAsList.Select(x => evaluator.Eval(x, env)).ToList();
                    result = Apply(func, evaluatedArgsAsList, env);
                }


            }

        }
    

        public virtual void VisitString(SExpressionString exp)
        {
            result = exp;
        }

        public void VisitSymbol(SExpressionSymbol exp)
        {
            if (exp.IsNumber)
                result = exp.AsNumberExpression();
            else
                result = env.Lookup(exp.ToString());
        }

        public void VisitPluggablePrimitive(PluggablePrimitive exp)
        {
            throw new NotImplementedException();
        }

        private SExpression Apply(SExpression func, IList<SExpression> evaluatedArgsAsList, Environment env)
        {
            if (func is IPrimitive)
            {
                var frameEnv = new Environment();
                frameEnv.Parent = env;
                var prim = (IPrimitive)func;
                return prim.Invoke(evaluatedArgsAsList.ToArray(), evaluator, frameEnv);
            }
            else
            {
                //var pair = (SExpressionPair)func;
                var closureSymbol = func.At(0);
                if (closureSymbol.ToString() != "closure")
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

                return  evaluator.Eval(body, frameEnv);

            }
        }

      

    }
}
