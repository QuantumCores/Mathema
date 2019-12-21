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
        public string Symbol { get; set; }

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

        public bool CompareDimensions(IVariableExpression variable)
        {
            return this.DimensionKey == variable.DimensionKey;
        }

        public override string ToString()
        {
            var num = this.Count.ToNumber();
            if (num != 1 && num != -1)
            {
                if (num % 1 == 0)
                {
                    return num.ToString() + " * " + this.Symbol;
                }
                if (this.Count.Denominator % 1 == 0 && this.Count.Numerator % 1 == 0)
                {
                    return this.Count.Numerator.ToString() + " / " + this.Count.Denominator + " * " + this.Symbol;
                }
            }

            return num == -1 ? "-" + this.DimensionKey.ToString() : this.DimensionKey.ToString();
        }       
    }
}
