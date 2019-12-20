using Mathema.Enums.Operators;
using Mathema.Interfaces;
using Mathema.Models.Operators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Models.Expressions
{
    public class BinaryExpression : IExpression
    {
        private IExpression lhe;
        private OperatorTypes op;
        private IExpression rhe;

        public string DimensionKey { get; } = "BinaryExpression";

        public BinaryExpression(IExpression lhe, OperatorTypes op, IExpression rhe)
        {
            this.lhe = lhe;
            this.rhe = rhe;
            this.op = op;
        }

        public IExpression Value()
        {
            var arg1 = this.lhe.Value();
            var arg2 = this.rhe.Value();
            if (arg1 is INumberExpression && arg2 is INumberExpression)
            {
                return new NumberExpression(Operations.BinaryOperations[op](((INumberExpression)arg1).Val, ((INumberExpression)arg2).Val));
            }
            else
            {
                return this;
            }
        }

        public override string ToString()
        {
            return " (" + this.lhe.ToString() + Operators.Operators.Get(op).Symbol + this.rhe.ToString() + ")";
        }
    }
}
