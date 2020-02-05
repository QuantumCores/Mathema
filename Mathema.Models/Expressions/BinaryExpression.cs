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

        public IComplex Count { get; set; } = new Complex();

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

            if(arg1 != null && arg2 != null)
            {
                this.lhe = arg1;
                this.rhe = arg2;
            }

            var res = arg1.BinaryOperations[op](arg1, arg2);

            //TODO Operation could not be performed in this case convert primitive BinaryExpression into operational expression
            if (res == null)
            {
                return this;
            }

            return res;
        }

        public void UpdateDimensionKey(bool deep)
        {
            if (deep)
            {
                this.rhe.UpdateDimensionKey(deep);
                this.lhe.UpdateDimensionKey(deep);
            }

            return;
        }

        public IExpression Clone()
        {
            return new BinaryExpression(this.lhe.Clone(), this.op, this.rhe.Clone());
        }

        public string AsString()
        {
            return this.ToString();
        }

        public override string ToString()
        {
            return " (" + this.lhe.ToString() + Operators.Operators.Get(op).Symbol + this.rhe.ToString() + ")";
        }
    }
}
