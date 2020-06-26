using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace LispInterpreter.Lexer
{
    public class Lexer
    {
        public Lexer()
        {
            rules = new List<LexerRule>();

        }

        IList<LexerRule> rules;

        public void AddRule(LexerRule rule)
        {
            rules.Add(rule);
        }

        public LexerRule IgnoreRule { get; set; }


        public IList<LexerToken> Execute(string str)
        {
            var i = 0;
            var tokens = new List<LexerToken>();


            while (i < str.Length)
            {
                if (IgnoreRule.Handles(str[i]))
                {
                    i++;
                    continue;
                }

                var rule = rules.FirstOrDefault(x => x.Handles(str[i]));
                if (rule == null)
                    throw new Exception("No rule matching character");
                var token = rule.Match(str, i);
                tokens.Add(token);
                i = token.EndIndex + 1;

            }

            return tokens;


        }
    }
}
