using Mathema.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Models.Operators
{
    public class Operations
    {
        public static Dictionary<OperatorTypes, Func<IExpression, IExpression, double>> BinaryOperations { get; } = GetAllBinary();

        public static Dictionary<OperatorTypes, Func<IExpression, double>> UnaryOperations { get; } = GetAllUnary();

        private static Dictionary<OperatorTypes, Func<IExpression, IExpression, double>> GetAllBinary()
        {
            var result = new Dictionary<OperatorTypes, Func<IExpression, IExpression, double>>();

            result.Add(OperatorTypes.Add, (lhe, rhe) => lhe.Value() + rhe.Value());
            result.Add(OperatorTypes.Subtract, (lhe, rhe) => lhe.Value() - rhe.Value());
            result.Add(OperatorTypes.Multiply, (lhe, rhe) => lhe.Value() * rhe.Value());
            result.Add(OperatorTypes.Divide, (lhe, rhe) => lhe.Value() / rhe.Value());
            result.Add(OperatorTypes.Power, (lhe, rhe) => Math.Pow(lhe.Value(), rhe.Value()));

            return result;
        }

        private static Dictionary<OperatorTypes, Func<IExpression, double>> GetAllUnary()
        {
            var result = new Dictionary<OperatorTypes, Func<IExpression, double>>();

            result.Add(OperatorTypes.Sign, rhe => -1 * rhe.Value());

            return result;
        }
    }
}
