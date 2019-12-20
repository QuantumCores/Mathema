using Mathema.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Models.Expressions
{
    public class NumberExpression : IExpression , INumberExpression
    {
        public decimal Val { get; }

        public string DimensionKey { get; } = "";

        public NumberExpression(string val)
        {
            if (decimal.TryParse(val, out var conv))
            {
                this.Val = conv;
            }
            //TODO
        }

        public NumberExpression(decimal val)
        {
            this.Val = val;
        }

        public IExpression Value()
        {
            return this;
        }

        public override string ToString()
        {
            return Val.ToString();
        }

        public static INumberExpression operator +(NumberExpression lhn, INumberExpression rhn)
        {
            return new NumberExpression(lhn.Val + rhn.Val);
        }

        public static INumberExpression operator +(INumberExpression lhn, NumberExpression rhn)
        {
            return new NumberExpression(lhn.Val + rhn.Val);
        }

        public static INumberExpression operator *(NumberExpression lhn, INumberExpression rhn)
        {
            return new NumberExpression(lhn.Val * rhn.Val);
        }

        public static INumberExpression operator /(NumberExpression lhn, INumberExpression rhn)
        {
            return new NumberExpression(lhn.Val / rhn.Val);
        }
    }
}
