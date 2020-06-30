using System;
using System.Collections.Generic;
using System.IO;
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
            return new PluggablePrimitive((args, eval, env) =>
            {
                if (args[0] is SExpressionPair)
                {
                    return ((SExpressionPair)args[0]).CAR;
                }
                throw new Exception("CAR error");

            }, true);
        }

        private SExpression CreateCDR()
        {
            return new PluggablePrimitive((args,eval, env) =>
            {
                if (args[0] is SExpressionPair)
                {
                    return ((SExpressionPair)args[0]).CDR;
                }
                throw new Exception("CDR error");

            }, true);
        }
        private SExpression CreateCONS()
        {
            return new PluggablePrimitive((args, eval, env) =>
            {
                return new SExpressionPair(args[0], args[1]);
            }, true);
        }

        private SExpression CreatePlus()
        {
            return new PluggablePrimitive((args,eval, env) =>
            {
                return new SExpressionSymbol(
                    (
                        int.Parse(args[0].ToString()) +
                        int.Parse(args[1].ToString())
                    ).ToString()) ;
            }, true);
        }

        private SExpression CreateList()
        {
            return new PluggablePrimitive((args, eval, env) =>
            {
                return SExpressionPair.List(args);

            }, true);
        }

        private SExpression CreatePrint()
        {
            return new PluggablePrimitive((args, eval,  env) =>
            {
                Console.WriteLine(args[0].ToString());
                return SExpression.NIL;
                //throw new Exception("s");

            }, true);
        }

        private SExpression CreateEval()
        {
            return new PluggablePrimitive((args, eval, env) =>
            {
                return eval.Eval(args[0], env);      

            }, true);
        }



        private SExpression CreateEQ()
        {
            return new PluggablePrimitive((args, eval, env) =>
            {
                if (args[0] is SExpressionSymbol && args[1] is SExpressionSymbol)
                    return SExpression.Bool(((SExpressionSymbol)args[0]).Value == ((SExpressionSymbol)args[1]).Value);
                else if (args[0] is SExpressionString && args[1] is SExpressionString)
                    return SExpression.Bool(((SExpressionString)args[0]).Value == ((SExpressionString)args[1]).Value);

                return SExpression.False;
            }, true);


        }


        private SExpression CreateIsSymbol()
        {
            return new PluggablePrimitive((args, eval, env) =>
            {
                return SExpression.Bool(args[0] is SExpressionSymbol);
            }, true);
        }

        private SExpression CreateIsString()
        {
            return new PluggablePrimitive((args, eval, env) =>
            {
                return SExpression.Bool(args[0] is SExpressionString);
            }, true);
        }

        private SExpression CreateIf()
        {
            return new PluggablePrimitive((args, eval, env) =>
            {
                if (args[0].IsTrue())
                    return eval.Eval(args[1], env);
                else
                {
                    if (args.Length == 3)
                        return eval.Eval(args[2], env);

                }

                return SExpression.NIL;
            }, false);
          
            
        }


     

        private SExpression CreateQuote2()
        {
            return new PluggablePrimitive((args, eval, env) =>
            {
                return args[0];

            }, false);

  
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


            var parser = new LispParser();


            var globalEnvironment  = new Environment();
            globalEnvironment.Define("CAR", CreateCAR());
            globalEnvironment.Define("CDR", CreateCDR());
            globalEnvironment.Define("CONS", CreateCONS());
            globalEnvironment.Define("+", CreatePlus());
            globalEnvironment.Define("ADD", CreatePlus());
            globalEnvironment.Define("LIST", CreateList());
            globalEnvironment.Define("PRINT", CreatePrint());
            globalEnvironment.Define("IF", CreateIf());
            globalEnvironment.Define("EQ", CreateEQ());
            globalEnvironment.Define("QUOTE", CreateQuote2());
            globalEnvironment.Define("ISSYMBOL", CreateIsSymbol());
            globalEnvironment.Define("ISSTRING", CreateIsString());
            globalEnvironment.Define("EVAL", CreateEval());

            // (IF 1 (ADD 2 3) 3)
            // (IF () (ADD 2 3) 3)


            var evaluator = new Evaluator();

 

            var moduleLines = File.ReadAllLines("REPL/lisp-modules/BaseModule.lisp");
            foreach (var l in moduleLines)
            {
                if (string.IsNullOrWhiteSpace(l))
                    continue;
                var tokens = lexer.Execute(l);
                var expression = parser.Parse(tokens);
                var result = evaluator.Eval(expression, globalEnvironment);

            }



            while (true)
            {
                try {

                var line = Console.ReadLine();

                var tokens = lexer.Execute(line);

                if (tokens.Count == 0)
                    continue;

               
                var expression = parser.Parse(tokens);
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
