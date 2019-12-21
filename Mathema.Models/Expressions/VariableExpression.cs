using Mathema.Interfaces;
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

        public string DimensionKey { get; set; } = "x";

        public IFraction Count { get; set; } = new Fraction();

        public VariableExpression(string symbol, decimal value)
        {
            this.Symbol = symbol;
            this.Val = value;
            this.DimensionKey = symbol;
        }

        public IExpression Value()
        {
            return this;
        }        

        public bool CompareDimensions(IVariableExpression variable)
        {
            var dims1 = this.DimensionKey.Split("*").OrderBy(x => x);
            var dims2 = variable.DimensionKey.Split("*").OrderBy(x => x);

            return dims1.SequenceEqual(dims2);
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

            return num == -1 ? "-" + this.DimensionKey : this.DimensionKey;
        }
    }
}
