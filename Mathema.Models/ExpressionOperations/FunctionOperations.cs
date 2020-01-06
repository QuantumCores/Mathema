using Mathema.Enums.Operators;
using Mathema.Interfaces;
using Mathema.Models.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mathema.Models.ExpressionOperations
{
    public class FunctionOperations
    {
        static FunctionOperations()
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
            if (rhe is FunctionExpression)
            {
                if (lhe.DimensionKey.Key.ElementAt(0).Key == rhe.DimensionKey.Key.ElementAt(0).Key)
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
            if (rhe is FunctionExpression)
            {
                if (lhe.DimensionKey.Key.ElementAt(0).Key == rhe.DimensionKey.Key.ElementAt(0).Key)
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
            if (rhe is FunctionExpression)
            {
                if (lhe.DimensionKey.Key.ElementAt(0).Key == rhe.DimensionKey.Key.ElementAt(0).Key)
                {
                    res.DimensionKey.Key[res.DimensionKey.Key.ElementAt(0).Key] += res.DimensionKey.Key.ElementAt(0).Value;

                    if (res.DimensionKey.Key.ElementAt(0).Value == 0)
                    {
                        return new NumberExpression(1);
                    }

                    return res;
                }
            }

            return null;
        }

        public static IExpression Divide(IExpression lhe, IExpression rhe)
        {
            var res = lhe.Clone();
            if (rhe is FunctionExpression)
            {
                if (lhe.DimensionKey.Key.ElementAt(0).Key == rhe.DimensionKey.Key.ElementAt(0).Key)
                {
                    res.DimensionKey.Key[res.DimensionKey.Key.ElementAt(0).Key] -= res.DimensionKey.Key.ElementAt(0).Value;

                    if (res.DimensionKey.Key.ElementAt(0).Value == 0)
                    {
                        return new NumberExpression(1);
                    }

                    return res;
                }
            }

            return null;
        }

        public static IExpression Pow(IExpression lhe, IExpression rhe)
        {
            var res = lhe.Clone();

            if (rhe is INumberExpression)
            {
                var p = rhe.Count.ToNumber();
                if (p % 1 == 0)
                {
                    if (p == 0)
                    {
                        return new NumberExpression(1);
                    }                    
                    else
                    {
                        res.DimensionKey.Key[res.DimensionKey.Key.ElementAt(0).Key] *= p;
                        return res;
                    }
                }
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
