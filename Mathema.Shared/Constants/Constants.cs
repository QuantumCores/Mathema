using System;

namespace Mathema.Shared.Constants
{
    public enum ConstantTypes
    {
        PI,
        e,
        EM,
        Phi
    }


    public class Constants
    {
        public static double PI { get; } = Math.PI;

        public static double e { get; } = Math.E;

        public static double EM { get; } = 0.577215664901532860606512090082402431042159335;

        public static double Phi { get; } = (1.0 * Math.Sqrt(5)) / 2.0;
    }
}
