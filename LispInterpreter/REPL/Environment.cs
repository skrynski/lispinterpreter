using System;
using System.Collections.Generic;

namespace LispInterpreter.REPL
{
    public class Environment :SExpression
    {
        Dictionary<string, SExpression> dict;

        public Environment()
        {
            dict = new Dictionary<string, SExpression>();
        }

        public Environment Parent { get; set; }

        public SExpression Lookup(string symbol)
        {
            if (dict.ContainsKey(symbol))
                return dict[symbol];

            if (Parent != null)
                return Parent.Lookup(symbol);

            throw new Exception($"Env: Symbol '{symbol}' not found");
        }


        public void Define(string symbol, SExpression value)
        {
           dict[symbol] = value;
        }


    }
}
