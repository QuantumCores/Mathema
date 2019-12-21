using Mathema.Enums.Operators;
using Mathema.Interfaces;
using Mathema.Models.Dimension;
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

        public IDimensionKey DimensionKey { get; set; } = new DimensionKey(nameof(BinaryExpression));

        public IFraction Count { get; set; } = new Fraction();

        public Dictionary<OperatorTypes, Func<IExpression, IExpression, IExpression>> BinaryOperations { get; }

        public Dictionary<OperatorTypes, Func<IExpression, IExpression>> UnaryOperations { get; }

        public BinaryExpression(IExpression lhe, OperatorTypes op, IExpression rhe)
        {
            this.lhe = lhe;
            this.rhe = rhe;
            this.op = op;
        }

        public IExpression Execute()
        {
            var arg1 = this.lhe.Execute();
            var arg2 = this.rhe.Execute();

            //var res = Operations.BinaryOperations[op](arg1, arg2);

            var res = arg1.BinaryOperations[op](arg1, arg2);

            if (res == null)
            {
                return this;
            }

            return res;
        }

        public override string ToString()
        {
            return " (" + this.lhe.ToString() + Operators.Operators.Get(op).Symbol + this.rhe.ToString() + ")";
        }
    }
}
