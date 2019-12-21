using Mathema.Interfaces;
using Mathema.Models.Numerics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Models.Expressions
{
    public class NumberExpression : INumberExpression
    {
        public decimal Val { get; }

        public string DimensionKey { get; set; } = "";

        public IFraction Count { get; set; } = new Fraction();

        public NumberExpression(string val)
        {
            if (decimal.TryParse(val, out var conv))
            {
                this.Val = conv;
                this.Count = new Fraction(conv, 1);
            }
            //TODO
        }

        public NumberExpression(decimal val)
        {
            this.Val = val;
            this.Count = new Fraction(val, 1);
        }

        public NumberExpression(IFraction frac)
        {
            this.Count = frac;
            //TODO
        }

        public IExpression Value()
        {
            return this;
        }

        public override string ToString()
        {
            return this.Count.ToNumber().ToString();
        }

        public static INumberExpression operator +(NumberExpression lhn, INumberExpression rhn)
        {
            lhn.Count.Add(rhn.Count);
            return new NumberExpression(lhn.Count);
        }

        //public static INumberExpression operator +(INumberExpression lhn, NumberExpression rhn)
        //{
        //    return new NumberExpression(lhn.Val + rhn.Val);
        //}

        //public static INumberExpression operator *(NumberExpression lhn, INumberExpression rhn)
        //{
        //    return new NumberExpression(lhn.Val * rhn.Val);
        //}

        //public static INumberExpression operator /(NumberExpression lhn, INumberExpression rhn)
        //{
        //    return new NumberExpression(lhn.Val / rhn.Val);
        //}
    }
}
