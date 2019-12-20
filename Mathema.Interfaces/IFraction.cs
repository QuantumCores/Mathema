using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Interfaces
{
    public interface IFraction
    {
        decimal Numerator { get; set; }

        decimal Denominator { get; set; }

        void Add(IFraction frc);

        void Subtract(IFraction count);

        void Multiply(IFraction count);

        void Divide(IFraction count);

        decimal ToNumber();
    }
}
