using Mathema.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Models.Expressions
{
    public class NumberExpression : IExpression
    {
        private double val;

        public NumberExpression(string val)
        {
            if (double.TryParse(val, out var conv))
            {
                this.val = conv;
            }
            //TODO
        }

        public double Value()
        {
            return val;
        }

        public override string ToString()
        {
            return val.ToString();
        }
    }
}
