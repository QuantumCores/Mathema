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

        public UnaryExpression(OperatorTypes op, IExpression rhe)
        {
            this.rhe = rhe;
            this.op = op;
        }

        public decimal Value()
        {
            return Operations.UnaryOperations[op](rhe);
        }

        public override string ToString()
        {
            return " " + Operators.Operators.Get(op).Symbol + "(" + this.rhe.Value() + ")";
        }
    }
}
