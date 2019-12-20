using Mathema.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Models.Operators
{
    public class Operations
    {
        public static Dictionary<OperatorTypes, Func<decimal, decimal, decimal>> BinaryOperations { get; } = GetAllBinary();

        public static Dictionary<OperatorTypes, Func<decimal, decimal>> UnaryOperations { get; } = GetAllUnary();

        private static Dictionary<OperatorTypes, Func<decimal, decimal, decimal>> GetAllBinary()
        {
            var result = new Dictionary<OperatorTypes, Func<decimal, decimal, decimal>>();

            result.Add(OperatorTypes.Add, (lhe, rhe) => lhe + rhe);
            result.Add(OperatorTypes.Subtract, (lhe, rhe) => lhe - rhe);
            result.Add(OperatorTypes.Multiply, (lhe, rhe) => lhe * rhe);
            result.Add(OperatorTypes.Divide, (lhe, rhe) => lhe / rhe);
            result.Add(OperatorTypes.Power, (lhe, rhe) => (decimal)Math.Pow((double)lhe, (double)rhe));

            return result;
        }

        private static Dictionary<OperatorTypes, Func<decimal, decimal>> GetAllUnary()
        {
            var result = new Dictionary<OperatorTypes, Func<decimal, decimal>>();

            result.Add(OperatorTypes.Sign, rhe => -1 * rhe);

            return result;
        }
    }
}
