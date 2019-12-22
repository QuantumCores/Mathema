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
    public class UnaryExpression : IExpression
    {
        private OperatorTypes op;
        private IExpression rhe;

        public IDimensionKey DimensionKey { get; set; } = new DimensionKey(nameof(UnaryExpression));

        public IFraction Count { get; set; } = new Fraction();

        public Dictionary<OperatorTypes, Func<IExpression, IExpression, IExpression>> BinaryOperations { get; }

        public Dictionary<OperatorTypes, Func<IExpression, IExpression>> UnaryOperations { get; }

        public UnaryExpression(OperatorTypes op, IExpression rhe)
        {
            this.rhe = rhe;
            this.op = op;
        }

        public IExpression Execute()
        {
            var arg = this.rhe.Execute();
            return arg.UnaryOperations[op](arg);
        }

        public IExpression Clone()
        {
            return new UnaryExpression(this.op, this.rhe.Clone());
        }

        public override string ToString()
        {
            if (op == OperatorTypes.Sign)
            {
                return " -(" + this.rhe.Execute() + ")";
            }
            else
            {
                return " " + Operators.Operators.Get(op).Symbol + "(" + this.rhe.Execute() + ")";
            }
        }
    }
}
