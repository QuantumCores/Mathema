using Mathema.Enums.DimensionKeys;
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
    public class FlatMultOperations
    {
        static FlatMultOperations()
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
            var res = (FlatMultExpression)lhe.Clone();
            if (DimensionKey.Compare(res.DimensionKey, rhe.DimensionKey))
            {
                //if (res.Expressions.ContainsKey(Dimensions.Number))
                //{
                //    res.Expressions[Dimensions.Number][0].Count.Add(rhe.Count);
                //}
                //else
                {
                    res.Count.Add(rhe.Count);
                }
                res.Count.Add(rhe.Count);
                return res;
            }

            return null;
        }

        public static IExpression Subtract(IExpression lhe, IExpression rhe)
        {
            var res = (FlatMultExpression)lhe.Clone();
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
                res.Count.Multiply(rhe.Count);
                return res;
            }
            else if (rhe is IVariableExpression)
            {
                var tmp = rhe.DimensionKey.Key;
                if (lc.Expressions.ContainsKey(tmp))
                {
                    lc.Expressions[tmp].Add(rhe);
                }
                else
                {
                    lc.Expressions.Add(tmp, new List<IExpression>() { rhe });
                }

                return res;
            }
            else if (rhe is IFlatMultExpression)
            {
                lc.Add(rhe.Clone());
                lc.Execute();

                return res;
            }

            return null;
        }

        public static IExpression Divide(IExpression lhe, IExpression rhe)
        {
            var res = lhe.Clone();
            var lc = (IFlatExpression)res;
            if (rhe is IVariableExpression)
            {
                var tmp = new BinaryExpression(rhe.Clone(), OperatorTypes.Power, new NumberExpression(-1));
                lc.Add(tmp);
                lc.Execute();

                return res;
            }

            return null;
        }

        public static IExpression Pow(IExpression lhe, IExpression rhe)
        {
            var res = lhe.Clone();
            var lc = (IFlatExpression)res;
            if (rhe is INumberExpression)
            {
                var n = rhe.Count.Re.ToNumber();

                if (n == 0)
                {
                    return new NumberExpression(1);
                }
                else
                {
                    foreach (var kv in lc.Expressions)
                    {
                        for (int i = 0; i < kv.Value.Count; i++)
                        {
                            var expr = kv.Value[i];
                            kv.Value[i] = expr.BinaryOperations[OperatorTypes.Power](expr, rhe);
                        }
                    }
                }

                return res;
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
