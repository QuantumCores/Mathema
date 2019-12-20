using Mathema.Interfaces;
using Mathema.Models.Exceptions;
using Mathema.Models.Expressions;
using Mathema.Models.FlatExpressions;
using Mathema.Models.Functions;
using Mathema.Models.Operators;
using Mathema.Models.Symbols;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Algorithms.Handlers
{
    public class ExpressionBuilder
    {
        public static IExpression Build(List<Symbol> RPNStack)
        {
            List<IExpression> stack = new List<IExpression>();
            var i = 0;
            try
            {
                for (i = 0; i < RPNStack.Count; i++)
                {
                    var s = RPNStack[i];
                    if (s.Type != SymbolTypes.Number)
                    {
                        if (s.Type == SymbolTypes.BinaryOperator)
                        {
                            var tmp = new BinaryExpression(stack[stack.Count - 2], Operators.Get(s.Value).Type, stack[stack.Count - 1]);
                            stack.RemoveAt(stack.Count - 1);
                            stack.RemoveAt(stack.Count - 1);
                            stack.Add(tmp);
                        }
                        else if (s.Type == SymbolTypes.UnaryOperator)
                        {
                            var tmp = new UnaryExpression(Operators.Get(s.Value).Type, stack[stack.Count - 1]);
                            stack.RemoveAt(stack.Count - 1);
                            stack.Add(tmp);
                        }
                        else if (s.Type == SymbolTypes.Function)
                        {
                            var tmp = new FunctionExpression(Functions.Get(s.Value).Type, stack[stack.Count - 1]);
                            stack.RemoveAt(stack.Count - 1);
                            stack.Add(tmp);
                        }
                        else if (s.Type == SymbolTypes.Variable)
                        {
                            var tmp = new VariableExpression(s.Value, 1m);
                            stack.RemoveAt(stack.Count - 1);
                            stack.Add(tmp);
                        }
                    }
                    else
                    {
                        stack.Add(new NumberExpression(s.Value));
                    }
                }
            }
            catch (Exception)
            {
                var msg = "I couldn't undertand what you mean by '" + RPNStack[i].Value + "' after " + string.Join("", stack);
                throw new WrongSyntaxException(msg);
            }

            return stack[0];
        }

        public static IExpression BuildFlat(List<Symbol> RPNStack)
        {
            List<IExpression> stack = new List<IExpression>();
            var i = 0;
            try
            {
                for (i = 0; i < RPNStack.Count; i++)
                {
                    var s = RPNStack[i];
                    if (s.Type != SymbolTypes.Number)
                    {
                        if (s.Type == SymbolTypes.BinaryOperator)
                        {
                            var type = Operators.Get(s.Value).Type;
                            if (type == OperatorTypes.Add || type == OperatorTypes.Subtract)
                            {
                                var flat = new FlatAddExpression();
                                flat.Add(stack[stack.Count - 1]);
                                flat.Add(stack[stack.Count - 1]);
                            }
                            else
                            {
                                var tmp = new BinaryExpression(stack[stack.Count - 2], Operators.Get(s.Value).Type, stack[stack.Count - 1]);
                                stack.Add(tmp);
                            }

                            stack.RemoveAt(stack.Count - 1);
                            stack.RemoveAt(stack.Count - 1);
                        }
                        else if (s.Type == SymbolTypes.UnaryOperator)
                        {
                            var tmp = new UnaryExpression(Operators.Get(s.Value).Type, stack[stack.Count - 1]);
                            stack.RemoveAt(stack.Count - 1);
                            stack.Add(tmp);
                        }
                        else if (s.Type == SymbolTypes.Function)
                        {
                            var tmp = new FunctionExpression(Functions.Get(s.Value).Type, stack[stack.Count - 1]);
                            stack.RemoveAt(stack.Count - 1);
                            stack.Add(tmp);
                        }
                        else if (s.Type == SymbolTypes.Variable)
                        {
                            var tmp = new VariableExpression(s.Value, 1m);
                            stack.RemoveAt(stack.Count - 1);
                            stack.Add(tmp);
                        }
                    }
                    else
                    {
                        stack.Add(new NumberExpression(s.Value));
                    }
                }
            }
            catch (Exception)
            {
                var msg = "I couldn't undertand what you mean by '" + RPNStack[i].Value + "' after " + string.Join("", stack);
                throw new WrongSyntaxException(msg);
            }

            return stack[0];
        }
    }
}
