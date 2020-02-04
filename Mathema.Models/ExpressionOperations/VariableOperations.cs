using Mathema.Enums.Operators;
using Mathema.Interfaces;
using Mathema.Models.Dimension;
using Mathema.Models.Expressions;
using Mathema.Models.FlatExpressions;
using Mathema.Models.Numerics;
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

                if (rhe.DimensionKey.Key == res.DimensionKey.Key)
                {
                    res.DimensionKey.Value += (Fraction)rhe.DimensionKey.Value;

                    if (res.DimensionKey.Value.Numerator == 0)
                    {
                        return FromCount(res.Count);
                    }

                    return res;
                }
            }

            var tmp = new FlatMultExpression();
            tmp.Add(res);
            tmp.Add(rhe.Clone());
            var r = tmp.Execute();

            return r;
        }

        private static IExpression FromCount(IComplex count)
        {
            if (count.Im.Numerator != 0)
            {
                return new ComplexExpression(count.Re, count.Im);
            }
            else //(count.Im.Numerator == 0)
            {
                return new NumberExpression(count.Re);
            }
        }

        public static IExpression Divide(IExpression lhe, IExpression rhe)
        {
            var res = lhe.Clone();
            if (rhe is IVariableExpression)
            {
                res.Count.Divide(rhe.Count);

                if (rhe.DimensionKey.Key == res.DimensionKey.Key)
                {
                    res.DimensionKey.Value -= (Fraction)rhe.DimensionKey.Value;

                    if (res.DimensionKey.Value.Numerator == 0)
                    {
                        return new ComplexExpression(res.Count);
                    }

                    return res;
                }
            }
            else if (rhe is NumberExpression || rhe is ComplexExpression)
            {
                res.Count.Divide(rhe.Count);
                return res;
            }

            var tmp = new FlatMultExpression();
            tmp.Add(res);
            tmp.Add(new BinaryExpression(rhe.Clone(), OperatorTypes.Power, new NumberExpression(-1)));
            var r = tmp.Execute();

            return r;
        }

        public static IExpression Pow(IExpression lhe, IExpression rhe)
        {
            var res = lhe.Clone();
            if (rhe is INumberExpression)
            {
                if (rhe.Count.Im.Numerator == 0)
                {
                    var n = rhe.Count.Re.ToNumber();

                    if (n == 0)
                    {
                        return new NumberExpression(1);
                    }

                    res.Count.Pow(rhe.Count);
                    var k = res.DimensionKey.Key;
                    res.DimensionKey.Multiply(k, n);

                    return res;
                }
            }

            return null;
        }

        public static IExpression Sign(IExpression rhe)
        {
            var res = rhe.Clone();
            res.Count.Multiply(-1);
            return res;
        }
    }
}
