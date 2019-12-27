using Mathema.Enums.Operators;
using Mathema.Interfaces;
using Mathema.Models.Dimension;
using Mathema.Models.ExpressionOperations;
using Mathema.Models.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mathema.Models.Expressions
{
    public class VariableExpression : IVariableExpression
    {
        public string Symbol { get; private set; }

        public decimal Val { get; set; }

        public IDimensionKey DimensionKey { get; set; } = new DimensionKey();

        public IFraction Count { get; set; } = new Fraction();

        public Dictionary<OperatorTypes, Func<IExpression, IExpression, IExpression>> BinaryOperations { get; } = VariableOperations.BinaryOperations;

        public Dictionary<OperatorTypes, Func<IExpression, IExpression>> UnaryOperations { get; } = VariableOperations.UnaryOperations;

        public VariableExpression(string symbol, decimal value)
        {
            this.Symbol = symbol;
            this.Val = value;
            this.DimensionKey.Add(symbol);
        }

        public IExpression Execute()
        {
            return this;
        }

        public IExpression Clone()
        {
            var res = new VariableExpression(string.Copy(this.Symbol), this.Val);
            res.DimensionKey = new DimensionKey();
            res.Count.Numerator = this.Count.Numerator;
            res.Count.Denominator = this.Count.Denominator;
            foreach (var key in this.DimensionKey.Key)
            {
                res.DimensionKey.Add(string.Copy(key.Key), key.Value);
            }

            return res;
        }

        public bool CompareDimensions(IVariableExpression variable)
        {
            return Dimension.DimensionKey.Compare(this.DimensionKey, variable.DimensionKey);
        }

        public override string ToString()
        {
            var num = this.Count.ToNumber();
            if (num != 1 && num != -1)
            {
                if (this.Count.Denominator % 1 == 0 && this.Count.Denominator != 1 && this.Count.Numerator % 1 == 0)
                {
                    return this.Count.Numerator.ToString() + " / " + this.Count.Denominator + " * " + this.DimensionKey.ToString();
                }

                if (num % 1 == 0)
                {
                    return num.ToString() + " * " + this.DimensionKey.ToString();
                }
            }

            return num == -1 ? "-" + this.DimensionKey.ToString() : this.DimensionKey.ToString();
        }
    }
}
