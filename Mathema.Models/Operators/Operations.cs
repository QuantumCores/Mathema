using Mathema.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Models.Operators
{
    public class Operations
    {
        public static Dictionary<OperatorTypes, Func<IExpression, IExpression, decimal>> BinaryOperations { get; } = GetAllBinary();

        public static Dictionary<OperatorTypes, Func<IExpression, decimal>> UnaryOperations { get; } = GetAllUnary();

        private static Dictionary<OperatorTypes, Func<IExpression, IExpression, decimal>> GetAllBinary()
        {
            var result = new Dictionary<OperatorTypes, Func<IExpression, IExpression, decimal>>();

            result.Add(OperatorTypes.Add, (lhe, rhe) => lhe.Value() + rhe.Value());
            result.Add(OperatorTypes.Subtract, (lhe, rhe) => lhe.Value() - rhe.Value());
            result.Add(OperatorTypes.Multiply, (lhe, rhe) => lhe.Value() * rhe.Value());
            result.Add(OperatorTypes.Divide, (lhe, rhe) => lhe.Value() / rhe.Value());
            result.Add(OperatorTypes.Power, (lhe, rhe) => (decimal)Math.Pow((double)lhe.Value(), (double)rhe.Value()));

            return result;
        }

        private static Dictionary<OperatorTypes, Func<IExpression, decimal>> GetAllUnary()
        {
            var result = new Dictionary<OperatorTypes, Func<IExpression, decimal>>();

            result.Add(OperatorTypes.Sign, rhe => -1 * rhe.Value());

            return result;
        }
    }
}
