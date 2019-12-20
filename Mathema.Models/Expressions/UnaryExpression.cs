using Mathema.Enums.Operators;
using Mathema.Interfaces;
using Mathema.Models.Numerics;
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

        public string DimensionKey { get; } = "UnaryExpression";

        public IFraction Count { get; set; } = new Fraction();

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
                return new NumberExpression(Operations.UnaryOperations[op](arg).Count);
            }
            else
            {
                return Operations.UnaryOperations[op](arg);
            }
        }

        public override string ToString()
        {
            if (op == OperatorTypes.Sign)
            {
                return " -(" + this.rhe.Value() + ")";
            }
            else
            {
                return " " + Operators.Operators.Get(op).Symbol + "(" + this.rhe.Value() + ")";
            }
        }
    }
}
