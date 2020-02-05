using Mathema.Enums.Functions;
using Mathema.Enums.Operators;
using Mathema.Interfaces;
using Mathema.Models.Dimension;
using Mathema.Models.ExpressionOperations;
using Mathema.Models.Expressions;
using Mathema.Models.Numerics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Models.FunctionExpressions
{
    public class ATanExpression : IFunctionExpression
    {
        public FunctionTypes Type { get; private set; } = FunctionTypes.ATan;

        public FunctionTypes InverseFunction { get; } = FunctionTypes.Tan;

        public IExpression Argument { get; set; }

        public IDimensionKey DimensionKey { get; set; } = new DimensionKey(nameof(CosExpression));

        public IComplex Count { get; set; } = new Complex();

        public Dictionary<OperatorTypes, Func<IExpression, IExpression, IExpression>> BinaryOperations { get; } = FunctionOperations.BinaryOperations;

        public Dictionary<OperatorTypes, Func<IExpression, IExpression>> UnaryOperations { get; } = FunctionOperations.UnaryOperations;

        public ATanExpression(IExpression argument, decimal count)
        {
            this.Argument = argument;
            this.Count = new Complex(count, 0);
            UpdateDimensionKey();
        }

        public ATanExpression(IExpression argument, IComplex count)
        {
            this.Argument = argument;
            this.Count = count;
            UpdateDimensionKey();
        }

        public IExpression Execute()
        {
            var arg = this.Argument.Execute();

            if (arg == null)
            {
                return this;
            }

            if (arg is INumberExpression)
            {
                return new NumberExpression((decimal)Math.Atan((double)arg.Count.Re.ToNumber()));
            }
            else
            {
                this.Argument = arg;
                return this;
            }
        }

        public void UpdateDimensionKey(bool deep)
        {
            this.DimensionKey.Key = this.AsString();
            return;
        }

        public IExpression Clone()
        {
            return new CosExpression(this.Argument.Clone(), this.Count.Clone());
        }

        public string AsString()
        {
            return this.ToString();
        }

        public override string ToString()
        {
            if (this.Count.Re.ToNumber() != 1)
            {
                return this.Count.AsString() + "*" + this.ExpressionKey();
            }

            var dim = this.DimensionKey;
            if (dim.Value.Numerator != 1 || dim.Value.Denominator != 1)
            {
                if (dim.Value.ToNumber() > 0)
                {
                    return "(" + dim.Key + ")^" + dim.Value;
                }
                else
                {
                    return "(" + dim.Key + ")^(" + dim.Value + ")";
                }
            }

            return this.ExpressionKey();
        }

        private void UpdateDimensionKey()
        {
            this.DimensionKey.Key = this.ExpressionKey();
        }

        private string ExpressionKey()
        {
            return Type.ToString() + "(" + Argument.ToString() + ")";
        }
    }
}
