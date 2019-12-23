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

        //TODO write operations to return new objects and doesn't change input
        public static IExpression Add(IExpression lhe, IExpression rhe)
        {
            if (rhe is IVariableExpression)
            {
                if (DimensionKey.Compare(rhe.DimensionKey, lhe.DimensionKey))
                {
                    lhe.Count.Add(rhe.Count);
                    return lhe;
                }
            }

            return null;
        }

        public static IExpression Subtract(IExpression lhe, IExpression rhe)
        {
            if (rhe is IVariableExpression)
            {
                if (DimensionKey.Compare(lhe.DimensionKey, rhe.DimensionKey))
                {
                    lhe.Count.Subtract(rhe.Count);
                    return lhe;
                }
            }

            return null;
        }

        public static IExpression Multiply(IExpression lhe, IExpression rhe)
        {
            if (rhe is IVariableExpression)
            {
                var result = lhe.Clone();

                result.Count.Multiply(rhe.Count);
                foreach (var key in rhe.DimensionKey.Key)
                {
                    result.DimensionKey.Add(string.Copy(key.Key), key.Value);
                }

                if (result.DimensionKey.Key.Count == 0)
                {
                    return new NumberExpression(lhe.Count.ToNumber());
                }

                return result;
            }

            return null;
        }

        public static IExpression Divide(IExpression lhe, IExpression rhe)
        {
            if (rhe is IVariableExpression)
            {
                lhe.Count.Divide(rhe.Count);

                foreach (var key in rhe.DimensionKey.Key)
                {
                    lhe.DimensionKey.Remove(key.Key, key.Value);
                }

                if (lhe.DimensionKey.Key.Count == 0)
                {
                    return new NumberExpression(lhe.Count.ToNumber());
                }

                return lhe.Clone();
            }

            return null;
        }

        public static IExpression Pow(IExpression lhe, IExpression rhe)
        {
            if (rhe is INumberExpression)
            {
                lhe.Count.Pow(rhe.Count);
                var keys = lhe.DimensionKey.Key.Select(k => k.Key).ToArray();
                foreach (var k in keys)
                {
                    lhe.DimensionKey.Multiply(k, rhe.Count.ToNumber());
                }

                return lhe;
            }

            return null;
        }

        public static IExpression Sign(IExpression rhe)
        {
            rhe.Count.Numerator *= -1;
            return rhe;
        }
    }
}
