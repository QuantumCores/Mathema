using Mathema.Enums.Operators;
using Mathema.Interfaces;
using Mathema.Models.FlatExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mathema.Models.Operators
{
    public class Operations
    {
        public static Dictionary<OperatorTypes, Func<IExpression, IExpression, IExpression>> BinaryOperations { get; } = GetAllBinary();

        public static Dictionary<OperatorTypes, Func<IExpression, IExpression>> UnaryOperations { get; } = GetAllUnary();

        private static Dictionary<OperatorTypes, Func<IExpression, IExpression, IExpression>> GetAllBinary()
        {
            var result = new Dictionary<OperatorTypes, Func<IExpression, IExpression, IExpression>>();

            //result.Add(OperatorTypes.Add, (lhe, rhe) => Add(lhe, rhe));
            //result.Add(OperatorTypes.Subtract, (lhe, rhe) => Subtract(lhe, rhe));
            //result.Add(OperatorTypes.Multiply, (lhe, rhe) => Multiply(lhe, rhe));
            //result.Add(OperatorTypes.Divide, (lhe, rhe) => Divide(lhe, rhe));
            //result.Add(OperatorTypes.Power, (lhe, rhe) => Power(lhe, rhe));

            return result;
        }

        private static IExpression Add(IExpression lhe, IExpression rhe)
        {
            if (lhe is INumberExpression && rhe is INumberExpression)
            {
                lhe.Count.Add(rhe.Count);
                return lhe;
            }
            else if (lhe is IVariableExpression && rhe is IVariableExpression && ((IVariableExpression)lhe).CompareDimensions((IVariableExpression)rhe))
            {
                lhe.Count.Add(rhe.Count);
                return lhe;
            }

            return null;
        }

        private static IExpression Subtract(IExpression lhe, IExpression rhe)
        {
            if (lhe is INumberExpression && rhe is INumberExpression)
            {
                lhe.Count.Subtract(rhe.Count);
                return lhe;
            }
            else if (lhe is IVariableExpression && rhe is IVariableExpression && ((IVariableExpression)lhe).CompareDimensions((IVariableExpression)rhe))
            {
                lhe.Count.Subtract(rhe.Count);
                return lhe;
            }

            return null;
        }

        private static IExpression Multiply(IExpression lhe, IExpression rhe)
        {
            if (lhe is INumberExpression && rhe is INumberExpression)
            {
                lhe.Count.Multiply(rhe.Count);
                return lhe;
            }
            else if (lhe is IVariableExpression && rhe is IVariableExpression && ((IVariableExpression)lhe).CompareDimensions((IVariableExpression)rhe))
            {
                lhe.Count.Multiply(rhe.Count);
                return lhe;
            }

            return null;
        }

        private static IExpression Divide(IExpression lhe, IExpression rhe)
        {
            if (lhe is INumberExpression && rhe is INumberExpression)
            {
                lhe.Count.Divide(rhe.Count);
                return lhe;
            }
            else if (lhe is IVariableExpression && rhe is IVariableExpression && ((IVariableExpression)lhe).CompareDimensions((IVariableExpression)rhe))
            {
                lhe.Count.Divide(rhe.Count);
                return lhe;
            }

            return null;
        }

        private static IExpression Power(IExpression lhe, IExpression rhe)
        {
            if (lhe is INumberExpression && rhe is INumberExpression)
            {
                lhe.Count.Pow(rhe.Count);
                return lhe;
            }
            else if (lhe is IFlatAddExpression && rhe is INumberExpression && rhe.Count.ToNumber() % 1 == 0)
            {
                var lc = (FlatAddExpression)lhe;
                var all = lc.Dimensions.SelectMany(t => t.Value).ToList();
                foreach (var keya in lc.Dimensions.Keys)
                {
                    foreach (var keyb in lc.Dimensions.Keys)
                    {

                    }
                }
            }
            else if (lhe is IFlatMultExpression && rhe is INumberExpression && rhe.Count.ToNumber() % 1 == 0)
            {
                var lc = (FlatMultExpression)lhe;                
            }

            return null;
        }

        private static Dictionary<OperatorTypes, Func<IExpression, IExpression>> GetAllUnary()
        {
            var result = new Dictionary<OperatorTypes, Func<IExpression, IExpression>>();

            //result.Add(OperatorTypes.Sign, rhe => { rhe.Count.Numerator *= -1; return rhe; });

            return result;
        }
    }
}
