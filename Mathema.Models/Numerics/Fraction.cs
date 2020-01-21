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

        public Fraction(IFraction frc) : this(frc.Numerator, frc.Denominator)
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

                SimplifyFloats();
            }
            else
            {
                var gcf = GCF((int)this.Denominator, (int)frc.Denominator);
                var den = this.Denominator * frc.Denominator / gcf;
                var num = this.Numerator * den / this.Denominator + frc.Numerator * den / frc.Denominator;

                this.Simplify(num, den);
            }
        }

        public void Add(int n)
        {
            this.Numerator += n * this.Denominator;
        }

        public void Subtract(IFraction frc)
        {
            this.Add(-(Fraction)frc);
        }

        public void Subtract(int n)
        {
            this.Numerator -= n * this.Denominator;
        }

        public void Multiply(IFraction frc)
        {
            if (this.Denominator % 1 != 0 || frc.Denominator % 1 != 0 || this.Numerator % 1 != 0 || frc.Numerator % 1 != 0)
            {
                this.Numerator = this.Numerator * frc.Numerator;
                this.Denominator = this.Denominator * frc.Denominator;

                SimplifyFloats();
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

                SimplifyFloats();
            }
            else
            {
                this.Simplify(this.Numerator * frc.Denominator, this.Denominator * frc.Numerator);
            }
        }

        public void Pow(IFraction frc)
        {
            var p = (double)frc.ToNumber();
            this.Numerator = (decimal)Math.Pow((double)this.Numerator, p);
            this.Denominator = (decimal)Math.Pow((double)this.Denominator, p);
            SimplifyFloats();
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

        private void SimplifyFloats()
        {
            var nd = (this.Numerator / this.Denominator);
            if (nd % 1 == 0)
            {
                this.Numerator = nd;
                this.Denominator = 1m;
                return;
            }

            var dn = (this.Denominator / this.Numerator);
            if (dn % 1 == 0)
            {
                this.Numerator = 1m;
                this.Denominator = dn;
                return;
            }
        }

        public IFraction Clone()
        {
            return new Fraction(this.Numerator, this.Denominator);
        }

        public string AsString()
        {
            return this.ToString();
        }

        public override string ToString()
        {
            if (this.Denominator == 1)
            {
                return this.Numerator.ToString();
            }
            else if (this.Denominator == -1)
            {
                return (-1 * this.Numerator).ToString();
            }
            else
            {
                if (this.Numerator < 0 || this.Denominator < 0)
                {
                    return "-" + this.Numerator + "/" + this.Denominator;
                }
                else if (this.Numerator < 0 && this.Denominator > 0)
                {
                    return "-" + (-this.Numerator) + "/" + this.Denominator;
                }
                else if (this.Numerator > 0 && this.Denominator < 0)
                {
                    return "-" + this.Numerator + "/" + (-this.Denominator);
                }
                else
                {
                    return this.Numerator + "/" + this.Denominator;
                }
            }
        }

        public static Fraction operator +(Fraction l, Fraction r)
        {
            var res = new Fraction(l);
            res.Add(r);

            return res;
        }

        public static Fraction operator +(Fraction l, IFraction r)
        {
            var res = new Fraction(l);
            res.Add(r);

            return res;
        }

        public static Fraction operator +(IFraction l, Fraction r)
        {
            var res = new Fraction(l);
            res.Add(r);

            return res;
        }

        public static Fraction operator +(Fraction l, int r)
        {
            var res = new Fraction(l);
            res.Add(new Fraction(r, 1));

            return res;
        }

        public static Fraction operator +(int l, Fraction r)
        {
            var res = new Fraction(l, 1);
            res.Add(r);

            return res;
        }

        public static Fraction operator +(Fraction l, double r)
        {
            var res = new Fraction(l);
            res.Add(new Fraction((decimal)r, 1));

            return res;
        }

        public static Fraction operator +(double l, Fraction r)
        {
            var res = new Fraction((decimal)l, 1);
            res.Add(r);

            return res;
        }


        public static Fraction operator -(Fraction l, Fraction r)
        {
            var res = new Fraction(l);
            res.Subtract(r);

            return res;
        }

        public static Fraction operator -(Fraction l, IFraction r)
        {
            var res = new Fraction(l);
            res.Subtract(r);

            return res;
        }

        public static Fraction operator -(IFraction l, Fraction r)
        {
            var res = new Fraction(l);
            res.Subtract(r);

            return res;
        }

        public static Fraction operator -(Fraction l, int r)
        {
            var res = new Fraction(l);
            res.Subtract(new Fraction(r, 1));

            return res;
        }

        public static Fraction operator -(int l, Fraction r)
        {
            var res = new Fraction(l, 1);
            res.Subtract(r);

            return res;
        }

        public static Fraction operator -(Fraction l, double r)
        {
            var res = new Fraction(l);
            res.Subtract(new Fraction((decimal)r, 1));

            return res;
        }

        public static Fraction operator -(double l, Fraction r)
        {
            var res = new Fraction((decimal)l, 1);
            res.Subtract(r);

            return res;
        }

        public static Fraction operator *(Fraction l, Fraction r)
        {
            var res = new Fraction(l);
            res.Multiply(r);

            return res;
        }

        public static Fraction operator *(Fraction l, IFraction r)
        {
            var res = new Fraction(l);
            res.Multiply(r);

            return res;
        }

        public static Fraction operator *(IFraction l, Fraction r)
        {
            var res = new Fraction(l);
            res.Multiply(r);

            return res;
        }

        public static Fraction operator *(Fraction l, int r)
        {
            var res = new Fraction(l);
            res.Multiply(new Fraction(r, 1));

            return res;
        }

        public static Fraction operator *(int l, Fraction r)
        {
            var res = new Fraction(l, 1);
            res.Multiply(r);

            return res;
        }

        public static Fraction operator *(Fraction l, double r)
        {
            var res = new Fraction(l);
            res.Multiply(new Fraction((decimal)r, 1));

            return res;
        }

        public static Fraction operator *(double l, Fraction r)
        {
            var res = new Fraction((decimal)l, 1);
            res.Multiply(r);

            return res;
        }

        public static Fraction operator /(Fraction l, Fraction r)
        {
            var res = new Fraction(l);
            res.Divide(r);

            return res;
        }

        public static Fraction operator /(Fraction l, IFraction r)
        {
            var res = new Fraction(l);
            res.Divide(r);

            return res;
        }

        public static Fraction operator /(IFraction l, Fraction r)
        {
            var res = new Fraction(l);
            res.Divide(r);

            return res;
        }

        public static Fraction operator /(Fraction l, int r)
        {
            var res = new Fraction(l);
            res.Divide(new Fraction(r, 1));

            return res;
        }

        public static Fraction operator /(int l, Fraction r)
        {
            var res = new Fraction(l, 1);
            res.Divide(r);

            return res;
        }

        public static Fraction operator /(Fraction l, double r)
        {
            var res = new Fraction(l);
            res.Divide(new Fraction((decimal)r, 1));

            return res;
        }

        public static Fraction operator /(double l, Fraction r)
        {
            var res = new Fraction((decimal)l, 1);
            res.Divide(r);

            return res;
        }

        public static Fraction operator -(Fraction r)
        {
            return new Fraction(-r.Numerator, r.Denominator);
        }

    }
}