using Mathema.Models.Constants;
using Mathema.Models.Functions;
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

            var previousSymbol = new Symbol("", SymbolTypes.Undefined);

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
                    if (previousSymbol.Type == SymbolTypes.Undefined && previousSymbol.Value != "")
                    {
                        FindSymbolForUndefined(ref previousSymbol, operators, output);
                    }

                    if (symbol.Type == SymbolTypes.Number)
                    {
                        if (previousSymbol.Type == SymbolTypes.Number)
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
                        if (previousSymbol.Type != SymbolTypes.Number && symbol.Value == "-")
                        {
                            symbol.Type = SymbolTypes.UnaryOperator;
                            symbol.Value = OperatorTypes.Sign.ToString();
                        }

                        while (operators.Count > 0 &&
                            (operators.Last().Type == SymbolTypes.Function ||
                            Operators.Get(operators.Last().Value).Precedence > Operators.Get(symbol.Value).Precedence ||
                            ((Operators.Get(operators.Last().Value).Precedence == Operators.Get(symbol.Value).Precedence) && (Operators.Get(operators.Last().Value).AssociativityType == AssociativityTypes.Left))) &&
                            operators[operators.Count - 1].Value != "(")
                        {
                            Pop(output, operators);
                        }
                        operators.Add(symbol);
                    }

                    previousSymbol = symbol;
                }
                else
                {
                    if (previousSymbol.Type == SymbolTypes.Undefined)
                    {
                        previousSymbol.Value += c.ToString();
                    }
                    else
                    {
                        previousSymbol = new Symbol(c.ToString(), SymbolTypes.Undefined);
                    }
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

        private static void FindSymbolForUndefined(ref Symbol previousSymbol, List<Symbol> operators, List<Symbol> output)
        {
            if (Constants.TryGetValue(previousSymbol.Value, out var c))
            {
                previousSymbol = new Symbol(c.Value.ToString(), SymbolTypes.Number);
                output.Add(previousSymbol);
            }
            else if (Functions.TryGetValue(previousSymbol.Value, out var f))
            {
                previousSymbol = new Symbol(previousSymbol.Value, SymbolTypes.Function);
                operators.Add(previousSymbol);
            }
            else
            {
                previousSymbol = new Symbol(previousSymbol.Value, SymbolTypes.Variable);
                output.Add(previousSymbol);
            }
        }

        private static void Pop(List<Symbol> numbers, List<Symbol> operators)
        {
            numbers.Add(operators[operators.Count - 1]);
            operators.RemoveAt(operators.Count - 1);
        }
    }
}
