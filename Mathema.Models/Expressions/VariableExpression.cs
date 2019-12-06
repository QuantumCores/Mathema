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

        public VariableExpression(string symbol, decimal value)
        {
            this.symbol = symbol;
            this.value = value;
        }

        public decimal Value()
        {
            throw new NotImplementedException();
        }
    }
}
