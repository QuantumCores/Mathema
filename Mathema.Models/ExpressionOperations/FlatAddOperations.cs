using Mathema.Enums.DimensionKeys;
using Mathema.Enums.Operators;
using Mathema.Interfaces;
using Mathema.Models.Dimension;
using Mathema.Models.FlatExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mathema.Models.ExpressionOperations
{
    public class FlatAddOperations
    {
        static FlatAddOperations()
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
            if (DimensionKey.Compare(res.DimensionKey, rhe.DimensionKey))
            {
                res.Count.Add(rhe.Count);
                return res;
            }

            return null;
        }

        public static IExpression Subtract(IExpression lhe, IExpression rhe)
        {
            var res = lhe.Clone();
            if (DimensionKey.Compare(res.DimensionKey, rhe.DimensionKey))
            {
                res.Count.Subtract(rhe.Count);
                return res;
            }

            return null;
        }

        public static IExpression Multiply(IExpression lhe, IExpression rhe)
        {
            var res = lhe.Clone();
            var lc = (IFlatExpression)res;
            if (rhe is INumberExpression)
            {
                foreach (var key in rhe.DimensionKey.Key)
                {
                    lc.Expressions[Dimensions.Number].Add(rhe);
                }

                return res;
            }
            else if (rhe is IVariableExpression)
            {
                var tmp = rhe.DimensionKey.Key.ElementAt(0).Key;
                if (lc.Expressions.ContainsKey(tmp))
                {
                    lc.Expressions[tmp].Add(rhe);
                }
                else
                {
                    lc.Expressions.Add(tmp, new List<IExpression>() { rhe });
                }

                foreach (var key in rhe.DimensionKey.Key)
                {
                    res.DimensionKey.Add(key.Key, key.Value);
                }

                return res;
            }
            else if (rhe is IFlatMultExpression)
            {
                foreach (var key in rhe.DimensionKey.Key)
                {
                    res.DimensionKey.Add(key.Key, key.Value);
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

                return res;
            }

            return null;
        }

        public static IExpression Pow(IExpression lhe, IExpression rhe)
        {
            var res = lhe.Clone();
            var lc = (IFlatExpression)res;
            var allA = lc.Expressions.SelectMany(kv => kv.Value).ToList();
            var allB = allA.Select(e => e.Clone()).ToList();
            var result = new List<IExpression>();
            var p = rhe.Count.ToNumber();

            if (rhe is INumberExpression && p % 1 == 0)
            {
                for (int i = 1; i < (int)p; i++)
                {
                    foreach (var expA in allA)
                    {
                        foreach (var expB in allB)
                        {
                            result.Add(expA.BinaryOperations[OperatorTypes.Multiply](expA, expB));
                        }
                    }

                    allB = result.ToList();
                    result.Clear();
                }

                var flat = new FlatAddExpression();
                foreach (var expB in allB)
                {
                    flat.Add(expB);
                }

                return flat.Execute();
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
