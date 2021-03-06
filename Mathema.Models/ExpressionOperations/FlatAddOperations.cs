﻿using Mathema.Enums.DimensionKeys;
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
                lc.Count.Multiply(rhe.Count);

                return res;
            }
            //else if (rhe is IVariableExpression)
            //{
            //    var tmp = rhe.DimensionKey.Key;
            //    if (lc.Expressions.ContainsKey(tmp))
            //    {
            //        lc.Expressions[tmp].Add(rhe);
            //    }
            //    else
            //    {
            //        lc.Expressions.Add(tmp, new List<IExpression>() { rhe });
            //    }

            //    foreach (var key in rhe.DimensionKey.Key)
            //    {
            //        res.DimensionKey.Add(key.Key, key.Value);
            //    }

            //    return res;
            //}
            //else if (rhe is IFlatMultExpression)
            //{
            //    foreach (var key in rhe.DimensionKey.Key)
            //    {
            //        res.DimensionKey.Add(key.Key, key.Value);
            //    }

            //    return res;
            //}
            else if (rhe is FlatAddExpression rc)
            {
                var ret = new FlatAddExpression();
                if (lc.DimensionKey.Key == rc.DimensionKey.Key)
                {
                    lc.DimensionKey.Value += (Fraction)rc.DimensionKey.Value;

                    if (lc.DimensionKey.Value.Numerator == 0)
                    {
                        return new NumberExpression(1);
                    }

                    return lc;
                }
                else
                {
                    foreach (var expl in lc.Expressions)
                    {
                        foreach (var li in expl.Value)
                        {
                            foreach (var expr in rc.Expressions)
                            {
                                foreach (var ri in expr.Value)
                                {
                                    var tmp = li.BinaryOperations[OperatorTypes.Multiply](li, ri);
                                    ret.Add(tmp);
                                }
                            }
                        }
                    }
                }

                return ret;
            }

            return null;
        }

        public static IExpression Divide(IExpression lhe, IExpression rhe)
        {
            if (rhe is NumberExpression || rhe is ComplexExpression)
            {
                var res = lhe.Clone();
                res.Count.Divide(rhe.Count);
                return res;
            }

            //var res = lhe.Clone();
            //if (rhe is IVariableExpression)
            //{
            //    res.Count.Divide(rhe.Count);

            //    foreach (var key in rhe.DimensionKey.Key)
            //    {
            //        res.DimensionKey.Remove(key.Key, key.Value);
            //    }

            //    return res;
            //}

            return null;
        }

        public static IExpression Pow(IExpression lhe, IExpression rhe)
        {
            var lc = (IFlatExpression)lhe.Clone();
            var allA = lc.Expressions.SelectMany(kv => kv.Value).ToList();
            var allB = allA.Select(e => e.Clone()).ToList();
            var result = new List<IExpression>();

            if (rhe is INumberExpression)
            {
                if (rhe.Count.Im.ToNumber() == 0)
                {
                    var p = rhe.Count.Re.ToNumber();
                    if (p % 1 == 0)
                    {
                        if (p == 0)
                        {
                            return new NumberExpression(1);
                        }
                        //else if (p == 1)
                        //{
                        //    return lc;
                        //}
                        //else if (p > 1)
                        //{
                        //    for (int i = 1; i < (int)p; i++)
                        //    {
                        //        foreach (var expA in allA)
                        //        {
                        //            foreach (var expB in allB)
                        //            {
                        //                result.Add(expA.BinaryOperations[OperatorTypes.Multiply](expA, expB));
                        //            }
                        //        }

                        //        allB = result.ToList();
                        //        result.Clear();
                        //    }

                        //    var flat = new FlatAddExpression();
                        //    foreach (var expB in allB)
                        //    {
                        //        flat.Add(expB);
                        //    }

                        //    return flat.Execute();
                        //}
                        else
                        {
                            lc.DimensionKey.Value.Multiply(new Fraction(p, 1));
                            return lc;
                        }
                    }
                }
            }

            return null;
        }

        public static IExpression Sign(IExpression rhe)
        {
            var res = rhe.Clone();

            if (res is FlatAddExpression fa)
            {
                foreach (var kve in fa.Expressions)
                {
                    kve.Value[0].Count.Multiply(-1);
                }

                fa.UpdateDimensionKey(false);
            }

            return res;
        }
    }
}
