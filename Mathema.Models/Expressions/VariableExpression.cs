using Mathema.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Models.Expressions
{
    public class VariableExpression : IExpression
    {
        public string symbol;

        public decimal value;

        public string DimensionType { get; private set; } = "x";

        public VariableExpression(string symbol, decimal value)
        {
            this.symbol = symbol;
            this.value = value;
            this.DimensionType = symbol;
        }

        public IExpression Value()
        {
            return this;
        }
    }
}
