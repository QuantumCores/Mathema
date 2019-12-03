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

        public BinaryExpression(IExpression lhe, OperatorTypes op, IExpression rhe)
        {
            this.lhe = lhe;
            this.rhe = rhe;
            this.op = op;
        }

        public double Value()
        {
            return Operations.BinaryOperations[op](lhe, rhe);
        }

        public override string ToString()
        {
            return "(" + this.lhe.Value().ToString() + Operators.Operators.Get(op).Symbol + this.rhe.Value() + ")";
        }
    }
}
