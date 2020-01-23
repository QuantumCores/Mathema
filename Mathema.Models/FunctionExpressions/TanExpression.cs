using Mathema.Enums.Functions;
using Mathema.Enums.Operators;
using Mathema.Interfaces;
using Mathema.Models.Dimension;
using Mathema.Models.ExpressionOperations;
using Mathema.Models.Expressions;
using Mathema.Models.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mathema.Models.FunctionExpressions
{
    public class TanExpression : IFunctionExpression
    {
        public FunctionTypes Type { get; private set; }

        public IExpression Argument { get; set; }

        public IDimensionKey DimensionKey { get; set; } = new DimensionKey(nameof(TanExpression));

        public IComplex Count { get; set; } = new Complex();

        public Dictionary<OperatorTypes, Func<IExpression, IExpression, IExpression>> BinaryOperations { get; } = FunctionOperations.BinaryOperations;

        public Dictionary<OperatorTypes, Func<IExpression, IExpression>> UnaryOperations { get; } = FunctionOperations.UnaryOperations;

		public TanExpression(IExpression argument, decimal count)
		{
			this.Type = FunctionTypes.Tan;
			this.Argument = argument;
			this.Count = new Complex(count, 0);
			UpdateDimensionKey();
		}

		public TanExpression(IExpression argument, IComplex count)
		{
			this.Type = FunctionTypes.Tan;
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
                return new NumberExpression((decimal)Math.Tan((double)arg.Count.Re.ToNumber()));
            }
            else
            {
                this.Argument = arg;
                return this;
            }
        }

        public IExpression Clone()
        {
            return new TanExpression(this.Argument.Clone(), this.Count.Clone());
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

            var kv = this.DimensionKey.Key.ElementAt(0);
            if (Math.Abs(kv.Value) != 1)
            {
                if (kv.Value > 0)
                {
                    return "(" + kv.Key + ")^" + kv.Value;
                }
                else
                {
                    return "(" + kv.Key + ")^(" + kv.Value + ")";
                }
            }

            return this.ExpressionKey();
        }

        private void UpdateDimensionKey()
        {
            var newDim = new Dictionary<string, decimal>();
            newDim.Add(this.ExpressionKey(), this.DimensionKey.Key.ElementAt(0).Value);
            this.DimensionKey.Key = newDim;
        }

        private string ExpressionKey()
        {
            return Type.ToString() + "(" + Argument.ToString() + ")";
        }
    }
}
