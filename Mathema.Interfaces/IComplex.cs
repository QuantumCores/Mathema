using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Interfaces
{
    public interface IComplex
    {
        IFraction Re { get; set; }

        IFraction Im { get; set; }

        void Add(IComplex frc);

        void Add(int n);

        void Subtract(IComplex frc);

        void Subtract(int n);

        void Multiply(IComplex frc);

        void Multiply(double n);

        void Divide(IComplex frc);

        void Divide(double n);

        void Pow(IComplex frc);

        //decimal ToNumber();

        IComplex Clone();

        string AsString();
    }
}
