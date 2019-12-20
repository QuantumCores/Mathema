using Mathema.Enums.Operators;
using Mathema.Enums.Symbols;
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
        public static IExpression Build(List<ISymbol> RPNStack)
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
                var msg = "I couldn't undertand what you mean by '" + RPNStack[i].Value + "' before " + string.Join("", stack);
                throw new WrongSyntaxException(msg);
            }

            return stack[0];
        }

        public static IExpression BuildFlat(List<ISymbol> RPNStack)
        {
            List<IExpression> stack = new List<IExpression>();
            var i = 0;
            try
            {
                for (i = 0; i < RPNStack.Count; i++)
                {
                    var s = RPNStack[i];
                    if (s.Type != SymbolTypes.Number && s.Type != SymbolTypes.Variable)
                    {
                        if (s.Type == SymbolTypes.BinaryOperator)
                        {
                            var type = Operators.Get(s.Value).Type;
                            
                            if (type == OperatorTypes.Add)
                            {
                                var tmp = new FlatAddExpression();
                                if (stack[stack.Count - 1] is FlatAddExpression)
                                {
                                    tmp = (FlatAddExpression)stack[stack.Count - 1] + stack[stack.Count - 2];
                                }
                                else if (stack[stack.Count - 2] is FlatAddExpression)
                                {
                                    tmp = (FlatAddExpression)stack[stack.Count - 2] + stack[stack.Count - 1];
                                }
                                else
                                {
                                    tmp.Add(stack[stack.Count - 1]);
                                    tmp.Add(stack[stack.Count - 2]);
                                }


                                stack.RemoveAt(stack.Count - 1);
                                stack.RemoveAt(stack.Count - 1);
                                stack.Add(tmp);
                            }
                            else if (type == OperatorTypes.Subtract)
                            {
                                var tmp = new FlatAddExpression();
                                tmp.Add(new UnaryExpression(OperatorTypes.Sign, stack[stack.Count - 1]));
                                tmp.Add(stack[stack.Count - 2]);
                                stack.RemoveAt(stack.Count - 1);
                                stack.RemoveAt(stack.Count - 1);
                                stack.Add(tmp);
                            }
                            else
                            {
                                var tmp = new BinaryExpression(stack[stack.Count - 2], Operators.Get(s.Value).Type, stack[stack.Count - 1]);
                                stack.RemoveAt(stack.Count - 1);
                                stack.RemoveAt(stack.Count - 1);
                                stack.Add(tmp);
                            }
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
                    }
                    else
                    {
                        if (s.Type == SymbolTypes.Number)
                        {
                            stack.Add(new NumberExpression(s.Value));
                        }
                        else if (s.Type == SymbolTypes.Variable)
                        {
                            stack.Add(new VariableExpression(s.Value, 1m));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = "I couldn't undertand what you mean by '" + RPNStack[i].Value + "' after " + string.Join(" ", stack);
                throw new WrongSyntaxException(msg, ex);
            }

            if (stack.Count > 1)
            {
                var msg = "I couldn't understand what you mean. Please rephrase your equation. What I understood " + Environment.NewLine + stack[0].ToString();
                throw new WrongSyntaxException(msg);
            }

            return stack[0];
        }
    }
}
