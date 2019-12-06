using Mathema.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Models.Expressions
{
    public class NumberExpression : IExpression
    {
        private decimal val;

        public NumberExpression(string val)
        {
            if (decimal.TryParse(val, out var conv))
            {
                this.val = conv;
            }
            //TODO
        }

        public IExpressionResult Value()
        {
            return val;
        }

        public override string ToString()
        {
            return val.ToString();
        }
    }
}
