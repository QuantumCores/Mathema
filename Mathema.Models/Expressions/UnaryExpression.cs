using Mathema.Interfaces;
using Mathema.Models.Operators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Models.Expressions
{
    public class UnaryExpression : IExpression
    {
        private OperatorTypes op;
        private IExpression rhe;

        public string DimensionType { get; } = "UnaryExpression";

        public UnaryExpression(OperatorTypes op, IExpression rhe)
        {
            this.rhe = rhe;
            this.op = op;
        }

        public IExpression Value()
        {
            var arg = this.rhe.Value();
            if (arg is INumberExpression)
            {
                return new NumberExpression(Operations.UnaryOperations[op](((INumberExpression)arg).Val));
            }
            else
            {
                return this;
            }
        }

        public override string ToString()
        {
            return " " + Operators.Operators.Get(op).Symbol + "(" + this.rhe.Value() + ")";
        }
    }
}
