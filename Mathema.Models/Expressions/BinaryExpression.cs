using Mathema.Enums.Operators;
using Mathema.Interfaces;
using Mathema.Models.Numerics;
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

        public string DimensionKey { get; set; } = nameof(BinaryExpression);
        public IFraction Count { get; set; } = new Fraction();

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
                return new NumberExpression(Operations.BinaryOperations[op](arg1, arg2).Count);
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
