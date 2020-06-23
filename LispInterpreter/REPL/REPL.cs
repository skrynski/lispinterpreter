using System;
using System.Collections.Generic;

namespace LispInterpreter.REPL
{
    public class REPL
    {
        public REPL()
        {
        }


      

        private SExpression CreateCAR()
        {
            return new PluggablePrimitive((args) =>
            {
                if (args[0] is SExpressionPair)
                {
                    return ((SExpressionPair)args[0]).CAR;
                }
                throw new Exception("CAR error");

            });
        }

        private SExpression CreateCDR()
        {
            return new PluggablePrimitive((args) =>
            {
                if (args[0] is SExpressionPair)
                {
                    return ((SExpressionPair)args[0]).CDR;
                }
                throw new Exception("CDR error");

            });
        }
        private SExpression CreateCONS()
        {
            return new PluggablePrimitive((args) =>
            {
                return new SExpressionPair(args[0], args[1]);
            });
        }

        private SExpression CreatePlus()
        {
            return new PluggablePrimitive((args) =>
            {
                return new SExpressionSymbol(
                    (
                        int.Parse(args[0].ToString()) +
                        int.Parse(args[1].ToString())
                    ).ToString()) ;
            });
        }


        public void Start()
        {

            var globalEnvironment = new Environment();
            globalEnvironment.Define("CAR", CreateCAR());
            globalEnvironment.Define("CDR", CreateCDR());
            globalEnvironment.Define("CONS", CreateCONS());
            globalEnvironment.Define("+", CreatePlus());


            globalEnvironment.Define("x", new SExpressionSymbol("34"));
            

            var evaluator = new Evaluator();

            while (true)
            {
                var line = Console.ReadLine();
                var parser = new SExpressionParser();


                //try
                ///{
                //var expression = parser.Parse(line);
                //var expression = new SExpressionString("hola");
                // var expression = new SExpressionPair(
                //     new SExpressionSymbol("QUOTE"),
                //     new SExpressionPair(new SExpressionSymbol("foo"),
                //                         SExpression.NIL));

                /*   var expression = new SExpressionPair(
                       new SExpressionSymbol("LAMBDA"),
                       new SExpressionPair(new SExpressionSymbol("foo"),
                                           SExpression.NIL));  */

                // var expression = new SExpressionString("hola");


                ////////////
                //var args = SExpressionPair.List(new[] { new SExpressionSymbol("a"), new SExpressionSymbol("b") });
                //var body = SExpressionPair.List(new[] {
                //        new SExpressionSymbol("+"),
                //        new SExpressionSymbol("a"),
                //        new SExpressionSymbol("x") });
                //var lambda = SExpression.CreateLambda(args, body);

                //var expression = SExpressionPair.List(new[] {
                //        lambda,
                //        new SExpressionSymbol("2"),
                //        new SExpressionSymbol("3") });

                //////////////
                ///
                var list23 = SExpressionPair.List(new[] {
                       
                        new SExpressionSymbol("2"),
                        new SExpressionSymbol("3") });

                globalEnvironment.Define("l", list23);

                var expression = SExpressionPair.List(new[] {
                        new SExpressionSymbol("CAR"),
                        new SExpressionSymbol("l")
                });




                var result = evaluator.Eval(expression, globalEnvironment);
                    Console.WriteLine(result.ToString());


                //}
                //catch (Exception ex)
                //{
                //    throw;
                //    //Console.WriteLine(ex.Message);
                //}

            }

        }

     

    }
}
