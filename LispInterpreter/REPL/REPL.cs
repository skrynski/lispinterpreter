using System;
using System.Collections.Generic;
using LispInterpreter.Lexer;
using LispInterpreter.Parser;

namespace LispInterpreter.REPL
{
    public class REPL
    {
        public REPL()
        {
        }


      

        private SExpression CreateCAR()
        {
            return new PluggablePrimitive((args, env) =>
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
            return new PluggablePrimitive((args,env) =>
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
            return new PluggablePrimitive((args, env) =>
            {
                return new SExpressionPair(args[0], args[1]);
            });
        }

        private SExpression CreatePlus()
        {
            return new PluggablePrimitive((args,env) =>
            {
                return new SExpressionSymbol(
                    (
                        int.Parse(args[0].ToString()) +
                        int.Parse(args[1].ToString())
                    ).ToString()) ;
            });
        }

        private SExpression CreateList()
        {
            return new PluggablePrimitive((args, env) =>
            {
                return SExpressionPair.List(args);

            });
        }


        public void Start()
        {
            var lexer = new Lexer.Lexer();
            lexer.IgnoreRule = new LexerRule(@"\s", "SPACE");
            lexer.AddRule(new LexerRule(@"[(]", "OPEN_PARENS"));
            lexer.AddRule(new LexerRule(@"[)]", "CLOSE_PARENS"));
            lexer.AddRule(new LexerRule(@"[0-9]+", "NUMBER"));
            lexer.AddRule(new LexerRule(@"[a-zA-Z][0-9a-zA-Z]*", "SYMBOL"));
            lexer.AddRule(new LexerRule("\"([^\"]*)\"", "STRING", c => c == '\"'));

            //var str =  "(ADD 1 2)";
           
            


            //return;

        
            //var str = "    (   ( hola998 que   \t\n      tal 23 34  \"soy un string\" ))";
            //var tokens = lexer.Execute(str);



           


            var globalEnvironment  = new Environment();
            globalEnvironment.Define("CAR", CreateCAR());
            globalEnvironment.Define("CDR", CreateCDR());
            globalEnvironment.Define("CONS", CreateCONS());
            globalEnvironment.Define("+", CreatePlus());
            globalEnvironment.Define("ADD", CreatePlus());
            globalEnvironment.Define("LIST", CreateList());



            //globalEnvironment.Define("x", new SExpressionSymbol("34"));
            

            var evaluator = new Evaluator();

            while (true)
            {
                try {

                var line = Console.ReadLine();

                var tokens = lexer.Execute(line);

                if (tokens.Count == 0)
                    continue;

                var parser = new LispParser();
                var expression = parser.Parse(tokens);

                //var parser = new SExpressionParser();


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
                //var list23 = SExpressionPair.List(new[] {
                       
                //        new SExpressionSymbol("2"),
                //        new SExpressionSymbol("3") });

                //globalEnvironment.Define("l", list23);

                //var expression = SExpressionPair.List(new[] {
                //        new SExpressionSymbol("CAR"),
                //        new SExpressionSymbol("l")
                //});

                // ((LAMBDA (x) (ADD x 1)) 3)


                var result = evaluator.Eval(expression, globalEnvironment);
                Console.WriteLine(result.ToString());


                }
                catch (Exception ex)
                {
               
                    Console.WriteLine(ex.Message);
                }

            }

        }

     

    }
}
