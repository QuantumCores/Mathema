using Mathema.Enums.Operators;
using Mathema.Interfaces;
using Mathema.Models.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Models.ExpressionOperations
{
    public static class NumberOperations
    {
        static NumberOperations()
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
            if (rhe is INumberExpression)
            {
                lhe.Count.Add(rhe.Count);
                return new NumberExpression(lhe.Count);
            }

            return null;
        }

        public static IExpression Subtract(IExpression lhe, IExpression rhe)
        {
            if (rhe is INumberExpression)
            {
                lhe.Count.Subtract(rhe.Count);
                return lhe;
            }

            return null;
        }

        public static IExpression Multiply(IExpression lhe, IExpression rhe)
        {
            if (rhe is INumberExpression)
            {
                lhe.Count.Multiply(rhe.Count);
                return lhe;
            }

            return null;
        }

        public static IExpression Divide(IExpression lhe, IExpression rhe)
        {
            if (rhe is INumberExpression)
            {
                lhe.Count.Divide(rhe.Count);
                return lhe;
            }

            return null;
        }

        public static IExpression Pow(IExpression lhe, IExpression rhe)
        {
            if (rhe is INumberExpression)
            {
                lhe.Count.Pow(rhe.Count);
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
