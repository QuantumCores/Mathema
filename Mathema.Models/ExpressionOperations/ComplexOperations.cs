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
                res.Re.Add(rc.Re);
                res.Im.Add(rc.Im);

                if (res.Im.ToNumber() == 0)
                {
                    return new NumberExpression(res.Re);
                }

                return res;
            }
            else if (rhe is INumberExpression nrc)
            {
                res.Re.Add(nrc.Count);
                return new ComplexExpression(res.Re, res.Im);
            }

            return null;
        }

        public static IExpression Subtract(IExpression lhe, IExpression rhe)
        {
            var res = (IComplexExpression)lhe.Clone();
            if (rhe is IComplexExpression rc)
            {
                res.Re.Subtract(rc.Re);
                res.Im.Subtract(rc.Im);

                if (res.Im.ToNumber() == 0)
                {
                    return new NumberExpression(res.Re);
                }

                return res;
            }
            else if (rhe is INumberExpression nrc)
            {
                res.Re.Subtract(nrc.Count);
                return new ComplexExpression(res.Re, res.Im);
            }

            return null;
        }

        public static IExpression Multiply(IExpression lhe, IExpression rhe)
        {
            var res = (IComplexExpression)lhe.Clone();
            if (rhe is IComplexExpression rc)
            {
                var ar = (Fraction)res.Re * rc.Re;
                var br = (Fraction)res.Im * rc.Im;
                var ai = (Fraction)res.Re * rc.Im;
                var bi = (Fraction)res.Im * rc.Re;

                return new ComplexExpression(ar - br, ai + bi);
            }
            else if (rhe is INumberExpression nrc)
            {
                res.Re.Multiply(nrc.Count);
                res.Im.Multiply(nrc.Count);
                return new ComplexExpression(res.Re, res.Im);
            }

            return null;
        }

        public static IExpression Divide(IExpression lhe, IExpression rhe)
        {
            var res = (IComplexExpression)lhe.Clone();
            if (rhe is IComplexExpression rc)
            {
                var ar = (Fraction)res.Re * rc.Re;
                var br = (Fraction)res.Im * rc.Im;
                var ai = (Fraction)res.Re * rc.Im;
                var bi = (Fraction)res.Im * rc.Re;
                var r2 = (Fraction)rc.Re * rc.Re + (Fraction)rc.Im * rc.Im;

                return new ComplexExpression((ar + br) / r2, (-ai + bi) / r2);
            }
            else if (rhe is INumberExpression nrc)
            {
                res.Re.Divide(nrc.Count);
                res.Im.Divide(nrc.Count);
                return new ComplexExpression(res.Re, res.Im);
            }

            return null;
        }

        public static IExpression Pow(IExpression lhe, IExpression rhe)
        {
            var res = (IComplexExpression)lhe.Clone();
            if (rhe is INumberExpression)
            {
                var n = (double)rhe.Count.ToNumber();

                if (n == 0)
                {
                    return new NumberExpression(1);
                }
                else if (n == 1)
                {
                    return res;
                }
                if (rhe.Count.Numerator > 1 && rhe.Count.Denominator > 1)
                {
                    //first power using numeratora and this function then root using this function and denominator
                }
                else if (rhe.Count.Numerator > 1 && rhe.Count.Denominator == 1)
                {
                    var r2 = (double)((Fraction)res.Re * res.Re + (Fraction)res.Im * res.Im).ToNumber();
                    var r = Math.Pow(r2, 0.5);
                    var y = (double)res.Im.ToNumber();
                    var rn = Math.Pow(r, n);

                    var phi = Math.Asin(y / r);

                    return new ComplexExpression((decimal)(rn * Math.Cos(n * phi)), (decimal)(rn * Math.Sin(n * phi)));
                }
                else if (rhe.Count.Numerator == 1 && rhe.Count.Denominator > 1)
                {
                    var r2 = (double)((Fraction)res.Re * res.Re + (Fraction)res.Im * res.Im).ToNumber();
                    var r = Math.Pow(r2, 0.5);
                    var y = (double)res.Im.ToNumber();
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
            res.Re.Numerator *= -1;
            res.Im.Numerator *= -1;
            return res;
        }
    }
}
