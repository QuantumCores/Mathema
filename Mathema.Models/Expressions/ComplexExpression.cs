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

        public IComplex Count { get; set; } = new Complex();

        public Dictionary<OperatorTypes, Func<IExpression, IExpression, IExpression>> BinaryOperations { get; } = ComplexOperations.BinaryOperations;

        public Dictionary<OperatorTypes, Func<IExpression, IExpression>> UnaryOperations { get; } = ComplexOperations.UnaryOperations;

        public ComplexExpression(string imVal)
        {
            if (decimal.TryParse(imVal, out var conv))
            {
                this.Count.Im = new Fraction(conv, 1);
            }
            else
            {
                throw new ArgumentException($"Couldn't parse {imVal} into number.");
            }
        }

        public ComplexExpression(IComplex complex)
        {
            this.Count = complex.Clone();
        }

        public ComplexExpression(string reVal, string imVal)
        {
            if (decimal.TryParse(reVal, out var re))
            {
                this.Count.Re = new Fraction(re, 1);
            }
            else
            {
                throw new ArgumentException($"Couldn't parse {reVal} into number.");
            }

            if (decimal.TryParse(imVal, out var im))
            {
                this.Count.Im = new Fraction(im, 1);
            }
            else
            {
                throw new ArgumentException($"Couldn't parse {imVal} into number.");
            }
        }

        public ComplexExpression(decimal im)
        {
            this.Count.Re = new Fraction(0, 1);
            this.Count.Im = new Fraction(im, 1);
        }

        public ComplexExpression(decimal re, decimal im)
        {
            this.Count.Re = new Fraction(re, 1);
            this.Count.Im = new Fraction(im, 1);
        }

        public ComplexExpression(IFraction im)
        {
            this.Count.Re = new Fraction(0, 1);
            this.Count.Im = im;
        }

        public ComplexExpression(IFraction re, IFraction im)
        {
            this.Count.Re = re;
            this.Count.Im = im;
        }

        public IComplexExpression Conjugation()
        {
            return new ComplexExpression(this.Count.Re.Clone(), -(Fraction)this.Count.Im.Clone());
        }

        public IExpression Execute()
        {
            if (this.Count.Im.Numerator == 0)
            {
                return new NumberExpression(this.Count.Re);
            }

            return this;
        }

        public void UpdateDimensionKey(bool deep)
        {
            return;
        }

        public IExpression Clone()
        {
            return new ComplexExpression(this.Count.Re.Clone(), this.Count.Im.Clone());
        }

        public string AsString()
        {
            return this.Count.AsString();
        }

        public override string ToString()
        {
            return this.AsString();
        }
    }
}
