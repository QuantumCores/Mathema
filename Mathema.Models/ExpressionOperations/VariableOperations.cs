using Mathema.Enums.Operators;
using Mathema.Interfaces;
using Mathema.Models.Dimension;
using Mathema.Models.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mathema.Models.ExpressionOperations
{
    public class VariableOperations
    {
        static VariableOperations()
        {
            BinaryOperations = new Dictionary<OperatorTypes, Func<IExpression, IExpression, IExpression>>();
            UnaryOperations = new Dictionary<OperatorTypes, Func<IExpression, IExpression>>();
            RegiterOperations();
        }

        public static Dictionary<OperatorTypes, Func<IExpression, IExpression, IExpression>> BinaryOperations { get; }

        public static Dictionary<OperatorTypes, Func<IExpression, IExpression>> UnaryOperations { get; }

        private static void RegiterOperations()
        {
            BinaryOperations.Add(OperatorTypes.Add, Add);
            BinaryOperations.Add(OperatorTypes.Subtract, Subtract);
            BinaryOperations.Add(OperatorTypes.Divide, Divide);
            BinaryOperations.Add(OperatorTypes.Multiply, Multiply);
            BinaryOperations.Add(OperatorTypes.Power, Pow);

            UnaryOperations.Add(OperatorTypes.Sign, Sign);
        }

        public static IExpression Add(IExpression lhe, IExpression rhe)
        {
            var res = lhe.Clone();
            if (rhe is IVariableExpression)
            {
                if (DimensionKey.Compare(rhe.DimensionKey, res.DimensionKey))
                {
                    res.Count.Add(rhe.Count);
                    return res;
                }
            }

            return null;
        }

        public static IExpression Subtract(IExpression lhe, IExpression rhe)
        {
            var res = lhe.Clone();
            if (rhe is IVariableExpression)
            {
                if (DimensionKey.Compare(res.DimensionKey, rhe.DimensionKey))
                {
                    res.Count.Subtract(rhe.Count);
                    return res;
                }
            }

            return null;
        }

        public static IExpression Multiply(IExpression lhe, IExpression rhe)
        {
            var res = lhe.Clone();
            if (rhe is IVariableExpression)
            {
                res.Count.Multiply(rhe.Count);
                foreach (var key in rhe.DimensionKey.Key)
                {
                    res.DimensionKey.Add(string.Copy(key.Key), key.Value);
                }

                if (res.DimensionKey.Key.Count == 0)
                {
                    return new NumberExpression(lhe.Count.ToNumber());
                }

                return res;
            }

            return null;
        }

        public static IExpression Divide(IExpression lhe, IExpression rhe)
        {
            var res = lhe.Clone();
            if (rhe is IVariableExpression)
            {
                res.Count.Divide(rhe.Count);

                foreach (var key in rhe.DimensionKey.Key)
                {
                    res.DimensionKey.Remove(key.Key, key.Value);
                }

                if (res.DimensionKey.Key.Count == 0)
                {
                    return new NumberExpression(res.Count.ToNumber());
                }

                return res.Clone();
            }

            return null;
        }

        public static IExpression Pow(IExpression lhe, IExpression rhe)
        {
            var res = lhe.Clone();
            if (rhe is INumberExpression)
            {
                var n = rhe.Count.ToNumber();

                if (n == 0)
                {
                    return new NumberExpression(1);
                }

                res.Count.Pow(rhe.Count);
                var k = res.DimensionKey.Key.ElementAt(0).Key;
                res.DimensionKey.Multiply(k, rhe.Count.ToNumber());

                return res;
            }

            return null;
        }

        public static IExpression Sign(IExpression rhe)
        {
            var res = rhe.Clone();
            res.Count.Numerator *= -1;
            return res;
        }
    }
}
