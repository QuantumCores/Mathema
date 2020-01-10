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

        void Subtract(IFraction frc);

        void Multiply(IFraction frc);

        void Divide(IFraction frc);

        void Pow(IFraction frc);

        decimal ToNumber();

        IFraction Clone();

        string AsString();

    }
}
