using Mathema.Enums.Operators;
using Mathema.Interfaces;
using Mathema.Models.Expressions;
using Mathema.Models.Numerics;
using System;
using System.Collections.Generic;

namespace Mathema.Models.ExpressionOperations
{
    public class ComplexOperations
    {
        static ComplexOperations()
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
            var res = (IComplexExpression)lhe.Clone();
            if (rhe is IComplexExpression rc)
            {
                res.Count.Re.Add(rc.Count.Re);
                res.Count.Im.Add(rc.Count.Im);

                if (res.Count.Im.ToNumber() == 0)
                {
                    return new NumberExpression(res.Count.Re);
                }

                return res;
            }
            else if (rhe is INumberExpression nrc)
            {
                res.Count.Add(nrc.Count);
                return new ComplexExpression(res.Count.Re, res.Count.Im);
            }

            return null;
        }

        public static IExpression Subtract(IExpression lhe, IExpression rhe)
        {
            var res = (IComplexExpression)lhe.Clone();
            if (rhe is IComplexExpression rc)
            {
                res.Count.Re.Subtract(rc.Count.Re);
                res.Count.Im.Subtract(rc.Count.Im);

                if (res.Count.Im.ToNumber() == 0)
                {
                    return new NumberExpression(res.Count.Re);
                }

                return res;
            }
            else if (rhe is INumberExpression nrc)
            {
                res.Count.Subtract(nrc.Count);
                return new ComplexExpression(res.Count.Re, res.Count.Im);
            }

            return null;
        }

        public static IExpression Multiply(IExpression lhe, IExpression rhe)
        {
            var res = (IComplexExpression)lhe.Clone();
            if (rhe is IComplexExpression rc)
            {
                res.Count.Multiply(rhe.Count);

                return res;
            }
            else if (rhe is INumberExpression nrc)
            {
                res.Count.Multiply(nrc.Count);
                return new ComplexExpression(res.Count.Re, res.Count.Im);
            }

            return null;
        }

        public static IExpression Divide(IExpression lhe, IExpression rhe)
        {
            var res = (IComplexExpression)lhe.Clone();
            if (rhe is IComplexExpression rc)
            {
                res.Count.Divide(rc.Count);

                return res;
            }
            else if (rhe is INumberExpression nrc)
            {
                res.Count.Divide(nrc.Count);
                return new ComplexExpression(res.Count.Re, res.Count.Im);
            }

            return null;
        }

        public static IExpression Pow(IExpression lhe, IExpression rhe)
        {
            var res = (IComplexExpression)lhe.Clone();
            if (rhe is INumberExpression)
            {
                var n = (double)rhe.Count.Re.ToNumber();

                if (n == 0)
                {
                    return new NumberExpression(1);
                }
                else if (n == 1)
                {
                    return res;
                }
                if (rhe.Count.Re.Numerator > 1 && rhe.Count.Re.Denominator > 1)
                {
                    //first power using numeratora and this function then root using this function and denominator
                }
                else if (rhe.Count.Re.Numerator > 1 && rhe.Count.Re.Denominator == 1)
                {
                    var r2 = (double)((Fraction)res.Count.Re * res.Count.Re + (Fraction)res.Count.Im * res.Count.Im).ToNumber();
                    var r = Math.Pow(r2, 0.5);
                    var y = (double)res.Count.Im.ToNumber();
                    var rn = Math.Pow(r, n);

                    var phi = Math.Asin(y / r);

                    return new ComplexExpression((decimal)(rn * Math.Cos(n * phi)), (decimal)(rn * Math.Sin(n * phi)));
                }
                else if (rhe.Count.Re.Numerator == 1 && rhe.Count.Re.Denominator > 1)
                {
                    var r2 = (double)((Fraction)res.Count.Re * res.Count.Re + (Fraction)res.Count.Im * res.Count.Im).ToNumber();
                    var r = Math.Pow(r2, 0.5);
                    var y = (double)res.Count.Im.ToNumber();
                    var rn = Math.Pow(r, n);

                    var phi = Math.Asin(y / r);

                    return new ComplexExpression((decimal)(rn * Math.Cos(n * phi)), (decimal)(rn * Math.Sin(n * phi)));
                }

                res.Count.Pow(rhe.Count);
                return res;
            }

            return null;
        }

        public static IExpression Sign(IExpression rhe)
        {
            var res = (IComplexExpression)rhe.Clone();
            res.Count.Re.Numerator *= -1;
            res.Count.Im.Numerator *= -1;
            return res;
        }
    }
}
