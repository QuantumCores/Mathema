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
    public class SinExpression : IFunctionExpression
    {
        private FunctionTypes type;
        private IExpression argument;

        public IDimensionKey DimensionKey { get; set; } = new DimensionKey(nameof(SinExpression));

        public IComplex Count { get; set; } = new Complex();

        public Dictionary<OperatorTypes, Func<IExpression, IExpression, IExpression>> BinaryOperations { get; } = FunctionOperations.BinaryOperations;

        public Dictionary<OperatorTypes, Func<IExpression, IExpression>> UnaryOperations { get; } = FunctionOperations.UnaryOperations;

        public SinExpression(FunctionTypes type, IExpression argument)
        {
            this.type = type;
            this.argument = argument;
            UpdateDimensionKey();
        }

        public IExpression Execute()
        {
            var arg = this.argument.Execute();

            if (arg == null)
            {
                return this;
            }

            if (arg is INumberExpression)
            {
                return new NumberExpression((decimal)Math.Sin((double)arg.Count.Re.ToNumber()));
            }
            else
            {
                this.argument = arg;
                return this;
            }
        }

        public IExpression Clone()
        {
            return new SinExpression(this.type, this.argument.Clone());
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
            return type.ToString() + "(" + argument.ToString() + ")";
        }
    }
}
