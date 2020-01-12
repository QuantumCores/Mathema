using Mathema.Enums.DimensionKeys;
using Mathema.Enums.Operators;
using Mathema.Interfaces;
using Mathema.Models.Dimension;
using Mathema.Models.ExpressionOperations;
using Mathema.Models.Numerics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Models.Expressions
{
    public class NumberExpression : INumberExpression
    {
        public IDimensionKey DimensionKey { get; set; } = new DimensionKey(Dimensions.Number);

        public IComplex Count { get; set; } = new Complex();

        public Dictionary<OperatorTypes, Func<IExpression, IExpression, IExpression>> BinaryOperations { get; } = NumberOperations.BinaryOperations;

        public Dictionary<OperatorTypes, Func<IExpression, IExpression>> UnaryOperations { get; } = NumberOperations.UnaryOperations;

        public NumberExpression(string val)
        {
            if (decimal.TryParse(val, out var conv))
            {
                this.Count = new Complex(conv, 0);
            }
            else
            {
                throw new ArgumentException($"Couldn't parse {val} into number.");
            }
        }

        public NumberExpression(decimal val)
        {
            this.Count = new Complex(val, 0);
        }

        public NumberExpression(IFraction frac)
        {
            this.Count = new Complex(frac, new Fraction(0, 1));
        }

        public IExpression Execute()
        {
            return this;
        }

        public IExpression Clone()
        {
            return new NumberExpression(this.Count.Re.Clone());
        }

        public string AsString()
        {
            return this.ToString();
        }

        public override string ToString()
        {
            return this.Count.AsString();
        }

        //public static INumberExpression operator +(NumberExpression lhn, INumberExpression rhn)
        //{
        //    lhn.Count.Add(rhn.Count);
        //    return new NumberExpression(lhn.Count);
        //}

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
