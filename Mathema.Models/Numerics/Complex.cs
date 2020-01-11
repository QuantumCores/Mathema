using Mathema.Interfaces;
using System;

namespace Mathema.Models.Numerics
{
    public class Complex : IComplex
    {
        public IFraction Re { get; set; }

        public IFraction Im { get; set; }

        /// <summary>
        /// Creates only real part 1 + 0i
        /// </summary>
        public Complex()
        {
            this.Re = new Fraction(1, 1);
            this.Im = new Fraction(0, 1);
        }

        public Complex(decimal re, decimal im)
        {
            this.Re = new Fraction(re, 1);
            this.Im = new Fraction(im, 1);
        }

        public Complex(IFraction re, IFraction im)
        {
            this.Re = re;
            this.Im = im;
        }

        public void Add(IComplex z)
        {
            this.Re.Add(z.Re);
            this.Im.Add(z.Im);
        }

        public void Add(int n)
        {
            this.Re.Add(n);
        }

        public void Subtract(IComplex z)
        {
            this.Re.Subtract(z.Re);
            this.Im.Subtract(z.Im);
        }

        public void Subtract(int n)
        {
            this.Re.Subtract(n);
        }

        public void Multiply(IComplex z)
        {
            if (z.Im.Numerator == 0)
            {
                this.Re.Multiply(z.Re);

                if (this.Im.Numerator != 0)
                {
                    this.Im.Multiply(z.Re);
                }
            }
            else
            {
                var ar = (Fraction)this.Re * z.Re;
                var br = (Fraction)this.Im * z.Im;
                var ai = (Fraction)this.Re * z.Im;
                var bi = (Fraction)this.Im * z.Re;

                this.Re = ar - br;
                this.Im = ai + bi;
            }
        }

        public void Multiply(double n)
        {
            this.Re = (Fraction)this.Re * n;
            this.Im = (Fraction)this.Im * n;
        }

        public void Divide(IComplex z)
        {
            if (z.Im.Numerator == 0)
            {
                this.Re.Divide(z.Re);

                if (this.Im.Numerator != 0)
                {
                    this.Im.Divide(z.Re);
                }
            }
            else
            {
                var ar = (Fraction)this.Re * z.Re;
                var br = (Fraction)this.Im * z.Im;
                var ai = (Fraction)this.Re * z.Im;
                var bi = (Fraction)this.Im * z.Re;
                var r2 = (Fraction)z.Re * z.Re + (Fraction)z.Im * z.Im;

                this.Re = (ar + br) / r2;
                this.Im = (-ai + bi) / r2;
            }
        }

        public void Divide(double n)
        {
            this.Re = (Fraction)this.Re / n;
            this.Im = (Fraction)this.Im / n;
        }

        public void Pow(IComplex z)
        {
            if (z.Im.Numerator == 0)
            {
                if (this.Im.Numerator == 0)
                {
                    this.Re.Pow(z.Re);
                }
                else
                {
                    var n = (double)z.Re.ToNumber();

                    if (n == 0)
                    {
                        this.Re = new Fraction();
                        this.Im = new Fraction(0, 1);
                    }
                    else if (n == 1)
                    {

                    }
                    if (z.Re.Numerator > 1 && z.Re.Denominator > 1)
                    {
                        //first power using numeratora and this function then root using this function and denominator
                        throw new NotImplementedException("Complex number to power of fraction is not implemnted");
                    }
                    else if (z.Re.Numerator > 1 && z.Re.Denominator == 1)
                    {
                        var r2 = (double)((Fraction)this.Re * this.Re + (Fraction)this.Im * this.Im).ToNumber();
                        var r = Math.Pow(r2, 0.5);
                        var y = (double)this.Im.ToNumber();
                        var rn = Math.Pow(r, n);

                        var phi = Math.Asin(y / r);

                        this.Re = new Fraction((decimal)(rn * Math.Cos(n * phi)), 1);
                        this.Im = new Fraction((decimal)(rn * Math.Sin(n * phi)), 1);
                    }
                    else if (z.Re.Numerator == 1 && z.Re.Denominator > 1)
                    {
                        var r2 = (double)((Fraction)this.Re * this.Re + (Fraction)this.Im * this.Im).ToNumber();
                        var r = Math.Pow(r2, 0.5);
                        var y = (double)this.Im.ToNumber();
                        var rn = Math.Pow(r, n);

                        var phi = Math.Asin(y / r);

                        this.Re = new Fraction((decimal)(rn * Math.Cos(n * phi)), 1);
                        this.Im = new Fraction((decimal)(rn * Math.Sin(n * phi)), 1);
                    }
                }
            }
            else
            {
                throw new NotImplementedException("Complex number to power of complex number is not implemented");
            }
        }

        public IComplex Clone()
        {
            return new Complex(new Fraction(this.Re.Numerator, this.Re.Denominator), new Fraction(this.Im.Numerator, this.Im.Denominator));
        }
        public string AsString()
        {
            if (this.Re.Numerator == 0)
            {
                if (this.Im.Numerator == 0)
                {
                    return "0";
                }
                else if (this.Im.ToNumber() == 1)
                {
                    return "i";
                }

                return this.Im.AsString() + " * i";
            }
            else if (this.Im.Numerator == 0)
            {
                return this.Re.AsString();
            }

            return this.Re.AsString() + " + " + this.Im.AsString() + " * i";
        }

        public override string ToString()
        {
            return this.AsString();
        }

        public static Complex operator +(Complex l, Complex r)
        {
            var res = (Complex)l.Clone();
            res.Add(r);

            return res;
        }

        public static Complex operator +(Complex l, IComplex r)
        {
            var res = (Complex)l.Clone();
            res.Add(r);

            return res;
        }

        public static Complex operator +(IComplex l, Complex r)
        {
            var res = (Complex)l.Clone();
            res.Add(r);

            return res;
        }

        public static Complex operator +(Complex l, int r)
        {
            var res = (Complex)l.Clone();
            res.Re.Add(new Fraction(r, 1));

            return res;
        }

        public static Complex operator +(int l, Complex r)
        {
            var res = (Complex)r.Clone();
            res.Re.Add(new Fraction(l, 1));

            return res;
        }


        public static Complex operator -(Complex l, Complex r)
        {
            var res = (Complex)l.Clone();
            res.Subtract(r);

            return res;
        }

        public static Complex operator -(Complex l, IComplex r)
        {
            var res = (Complex)l.Clone();
            res.Subtract(r);

            return res;
        }

        public static Complex operator -(IComplex l, Complex r)
        {
            var res = l.Clone();
            res.Subtract(r);

            return (Complex)res;
        }

        public static Complex operator -(Complex l, int r)
        {
            var res = (Complex)l.Clone();
            res.Re.Subtract(r);


            return res;
        }

        public static Complex operator -(int l, Complex r)
        {
            var res = new Complex(l, 0);
            res.Subtract(r);

            return res;
        }

        public static Complex operator *(Complex l, Complex r)
        {
            var res = (Complex)l.Clone();
            res.Multiply(r);

            return res;
        }

        public static Complex operator *(Complex l, IComplex r)
        {
            var res = (Complex)l.Clone();
            res.Multiply(r);

            return res;
        }

        public static Complex operator *(IComplex l, Complex r)
        {
            var res = (Complex)l.Clone();
            res.Multiply(r);

            return res;
        }

        public static Complex operator *(Complex l, int r)
        {
            var res = (Complex)l.Clone();
            res.Multiply(new Complex(r, 1));

            return res;
        }

        public static Complex operator *(int l, Complex r)
        {
            var res = (Complex)r.Clone();
            res.Multiply(l);

            return res;
        }

        public static Complex operator *(Complex l, double r)
        {
            var res = (Complex)l.Clone();
            res.Multiply(new Complex((decimal)r, 1));

            return res;
        }

        public static Complex operator *(double l, Complex r)
        {
            var res = (Complex)r.Clone();
            res.Multiply(l);

            return res;
        }

        public static Complex operator /(Complex l, Complex r)
        {
            var res = (Complex)l.Clone();
            res.Divide(r);

            return res;
        }

        public static Complex operator /(Complex l, IComplex r)
        {
            var res = (Complex)l.Clone();
            res.Divide(r);

            return res;
        }

        public static Complex operator /(IComplex l, Complex r)
        {
            var res = (Complex)l.Clone();
            res.Divide(r);

            return res;
        }

        public static Complex operator /(Complex l, int r)
        {
            var res = (Complex)l.Clone();
            res.Divide(r);

            return res;
        }

        public static Complex operator /(int l, Complex r)
        {
            var res = new Complex(l, 0);
            res.Divide(r);

            return res;
        }

        public static Complex operator /(Complex l, double r)
        {
            var res = (Complex)l.Clone();
            res.Divide(r);

            return res;
        }

        public static Complex operator /(double l, Complex r)
        {
            var res = new Complex((decimal)l, 0);
            res.Divide(r);

            return res;
        }

        public static Complex operator -(Complex r)
        {
            return new Complex(-(Fraction)r.Re, -(Fraction)r.Im);
        }
    }
}
