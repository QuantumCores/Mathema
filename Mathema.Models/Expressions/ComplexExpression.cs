using Mathema.Enums.DimensionKeys;
using Mathema.Enums.Operators;
using Mathema.Interfaces;
using Mathema.Models.Dimension;
using Mathema.Models.ExpressionOperations;
using Mathema.Models.Numerics;
using System;
using System.Collections.Generic;

namespace Mathema.Models.Expressions
{
    public class ComplexExpression : IComplexExpression
    {
        public IDimensionKey DimensionKey { get; set; } = new DimensionKey(Dimensions.Complex);

        public IFraction Count { get { return Re; } set { Re = value; } }

        public IFraction Re { get; set; } = new Fraction();

        public IFraction Im { get; set; } = new Fraction();

        public Dictionary<OperatorTypes, Func<IExpression, IExpression, IExpression>> BinaryOperations { get; } = ComplexOperations.BinaryOperations;

        public Dictionary<OperatorTypes, Func<IExpression, IExpression>> UnaryOperations { get; } = ComplexOperations.UnaryOperations;

        public ComplexExpression(string imVal)
        {
            if (decimal.TryParse(imVal, out var conv))
            {
                this.Im = new Fraction(conv, 1);
            }
            else
            {
                throw new ArgumentException($"Couldn't parse {imVal} into number.");
            }
        }

        public ComplexExpression(string reVal, string imVal)
        {
            if (decimal.TryParse(reVal, out var re))
            {
                this.Re = new Fraction(re, 1);
            }
            else
            {
                throw new ArgumentException($"Couldn't parse {reVal} into number.");
            }

            if (decimal.TryParse(imVal, out var im))
            {
                this.Im = new Fraction(im, 1);
            }
            else
            {
                throw new ArgumentException($"Couldn't parse {imVal} into number.");
            }
        }

        public ComplexExpression(decimal im)
        {
            this.Re = new Fraction(0, 1);
            this.Im = new Fraction(im, 1);
        }

        public ComplexExpression(decimal re, decimal im)
        {
            this.Re = new Fraction(re, 1);
            this.Im = new Fraction(im, 1);
        }

        public ComplexExpression(IFraction im)
        {
            this.Re = new Fraction(0, 1);
            this.Im = im;
        }

        public ComplexExpression(IFraction re, IFraction im)
        {
            this.Re = re;
            this.Im = im;
        }

        public IComplexExpression Conjugation()
        {
            return new ComplexExpression(this.Re.Clone(), -(Fraction)this.Im.Clone());
        }

        public IExpression Execute()
        {
            return this;
        }

        public IExpression Clone()
        {
            return new ComplexExpression(this.Re.Clone(), this.Im.Clone());
        }

        public string AsString()
        {
            if (this.Re.Numerator == 0)
            {
                return this.Im.AsString() + " * i";
            }

            return this.Re.AsString() + " + " + this.Im.AsString() + " * i";
        }

        public override string ToString()
        {
            return this.AsString();
        }
    }
}
