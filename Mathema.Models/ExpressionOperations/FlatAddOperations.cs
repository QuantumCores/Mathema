using Mathema.Enums.Operators;
using Mathema.Interfaces;
using System;
using System.Collections.Generic;
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
            if (lhe.DimensionKey == rhe.DimensionKey)
            {
                lhe.Count.Add(rhe.Count);
                return lhe;
            }

            return null;
        }

        public static IExpression Subtract(IExpression lhe, IExpression rhe)
        {
            if (lhe.DimensionKey == rhe.DimensionKey)
            {
                lhe.Count.Subtract(rhe.Count);
                return lhe;
            }

            return null;
        }

        public static IExpression Multiply(IExpression lhe, IExpression rhe)
        {
            var lc = (IFlatExpression)lhe;
            if (rhe is INumberExpression)
            {
                foreach (var key in rhe.DimensionKey.Key)
                {
                    lc.Dimensions[""].Add(rhe);
                }

                return lhe;
            }
            else if (rhe is IVariableExpression)
            {
                var tmp = rhe.DimensionKey.ToString();
                if (lc.Dimensions.ContainsKey(tmp))
                {
                    lc.Dimensions[tmp].Add(rhe);
                }
                else
                {
                    lc.Dimensions.Add(tmp, new List<IExpression>() { rhe });
                }

                foreach (var key in rhe.DimensionKey.Key)
                {
                    lhe.DimensionKey.Add(key.Key, key.Value);
                }

                return lhe;
            }
            else if (rhe is IFlatMultExpression)
            {
                foreach (var key in rhe.DimensionKey.Key)
                {
                    lhe.DimensionKey.Add(key.Key, key.Value);
                }

                return lhe;
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

                return lhe;
            }

            return null;
        }

        public static IExpression Pow(IExpression lhe, IExpression rhe)
        {
            var lc = (IFlatExpression)lhe;
            if (rhe is INumberExpression)
            {
                foreach (var kv in lc.Dimensions)
                {
                    kv.Value.ForEach(e => e.BinaryOperations[OperatorTypes.Power](e, rhe));
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
