using Mathema.Interfaces;

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
    }
}
