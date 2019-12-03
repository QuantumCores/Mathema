using Mathema.Interfaces;
using Mathema.Models.Expressions;
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

            for (int i = 0; i < RPNStack.Count; i++)
            {
                var s = RPNStack[i];
                if (s.Type != SymbolTypes.Number)
                {
                    var tmp = new BinaryExpression(stack[stack.Count - 2], Operators.All[s.Value].Type, stack[stack.Count - 1]);
                    stack.RemoveAt(stack.Count - 1);
                    stack.RemoveAt(stack.Count - 1);
                    stack.Add(tmp);
                }
                else
                {
                    stack.Add(new NumberExpression(s.Value));
                }
            }

            return stack[0];
        }
    }
}
