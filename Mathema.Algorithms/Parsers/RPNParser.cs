using Mathema.Models.Operators;
using Mathema.Models.Symbols;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mathema.Algorithms.Parsers
{
    public class RPNParser
    {
        public static List<Symbol> Parse(string text)
        {
            var output = new List<Symbol>();
            var operators = new List<Symbol>();

            var previousType = SymbolTypes.Undefined;

            var calc = text.ToLower();
            for (int i = 0; i < calc.Length; i++)
            {
                var c = calc[i];

                if (c == 32)
                {
                    continue;
                }

                if (Symbols.TryGetValue(c.ToString(), out var symbol))
                {
                    if (symbol.Type == SymbolTypes.Number)
                    {
                        if (previousType == SymbolTypes.Number)
                        {
                            output.Last().Value += symbol.Value;
                        }
                        else
                        {
                            output.Add(symbol);
                        }
                    }
                    else if (symbol.Type == SymbolTypes.LeftParenthesis)
                    {
                        operators.Add(symbol);
                    }
                    else if (symbol.Type == SymbolTypes.RightParenthesis)
                    {
                        if (operators.Count > 0)
                        {
                            while (operators.Count > 0 && operators[operators.Count - 1].Type != SymbolTypes.LeftParenthesis)
                            {
                                Pop(output, operators);
                            }

                            if (operators[operators.Count - 1].Type == SymbolTypes.LeftParenthesis)
                            {
                                operators.RemoveAt(operators.Count - 1);
                            }
                        }
                    }
                    else if (symbol.Type == SymbolTypes.BinaryOperator || symbol.Type == SymbolTypes.UnaryOperator)
                    {
                        if (previousType != SymbolTypes.Number && symbol.Value == "-")
                        {
                            symbol.Type = SymbolTypes.UnaryOperator;
                            symbol.Value = OperatorTypes.Sign.ToString();
                        }

                        while (operators.Count > 0 &&
                            (Operators.Get(operators.Last().Value).Precedence > Operators.Get(symbol.Value).Precedence ||
                            ((Operators.Get(operators.Last().Value).Precedence == Operators.Get(symbol.Value).Precedence) && (Operators.Get(operators.Last().Value).AssociativityType == AssociativityTypes.Left))) &&
                            operators[operators.Count - 1].Value != "(")
                        {
                            Pop(output, operators);
                        }
                        operators.Add(symbol);
                    }

                    previousType = symbol.Type;
                }
                else
                {
                    //probably expression
                }
            }

            if (operators.Count != 0)
            {
                while (operators.Count > 0)
                {
                    output.Add(operators[operators.Count - 1]);
                    operators.RemoveAt(operators.Count - 1);
                }
            }

            return output;
        }

        private static void Pop(List<Symbol> numbers, List<Symbol> operators)
        {
            numbers.Add(operators[operators.Count - 1]);
            operators.RemoveAt(operators.Count - 1);
        }
    }
}
