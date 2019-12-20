using System;
using System.Collections.Generic;
using System.Text;
using Mathema.Interfaces;

namespace Mathema.Models.Numerics
{
    public class Fraction : IFraction
    {
        public decimal Numerator { get; set; } = 1m;

        public decimal Denominator { get; set; } = 1m;

        public Fraction()
        {

        }

        public Fraction(decimal num, decimal den)
        {
            this.Numerator = num;
            this.Denominator = den;
        }

        public void Add(IFraction frc)
        {
            if (this.Denominator % 1 != 0 || frc.Denominator % 1 != 0 || this.Numerator % 1 != 0 || frc.Numerator % 1 != 0)
            {
                this.Numerator = this.Denominator * frc.Numerator + this.Numerator * frc.Denominator;
                this.Denominator *= frc.Denominator;
            }
            else
            {
                var gcf = GCF((int)this.Denominator, (int)frc.Denominator);
                var den = this.Denominator * frc.Denominator / gcf;
                var num = this.Numerator * den / this.Denominator + frc.Numerator * den / frc.Denominator;

                this.Simplify(num, den);
            }
        }

        public void Subtract(IFraction frc)
        {
            frc.Numerator *= -1;
            this.Add(frc);
        }

        public void Multiply(IFraction frc)
        {
            if (this.Denominator % 1 != 0 || frc.Denominator % 1 != 0 || this.Numerator % 1 != 0 || frc.Numerator % 1 != 0)
            {
                this.Numerator = this.Numerator * frc.Numerator;
                this.Denominator = this.Denominator * frc.Denominator;
            }
            else
            {
                this.Simplify(this.Numerator * frc.Numerator, this.Denominator * frc.Denominator);
            }
        }

        public void Divide(IFraction frc)
        {
            if (this.Denominator % 1 != 0 || frc.Denominator % 1 != 0 || this.Numerator % 1 != 0 || frc.Numerator % 1 != 0)
            {
                this.Numerator = this.Numerator * frc.Denominator;
                this.Denominator = this.Denominator * frc.Numerator;
            }
            else
            {
                this.Simplify(this.Numerator * frc.Denominator, this.Denominator * frc.Numerator);
            }
        }

        public decimal ToNumber()
        {
            return this.Numerator / this.Denominator;
        }

        /// <summary>
        /// Greatest Common Factor
        /// </summary>
        private int GCF(int a, int b)
        {
            if (a == 0)
            {
                return b;
            }

            return GCF(b % a, a);
        }

        private void Simplify(decimal num, decimal den)
        {
            var gcf = this.GCF((int)num, (int)den);

            this.Numerator = num / gcf;
            this.Denominator = den / gcf;
        }
    }
}