using Mathema.Enums.Operators;
using Mathema.Interfaces;
using Mathema.Models.Expressions;
using Mathema.Models.FlatExpressions;
using Mathema.Models.Numerics;
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
            var res = lhe.Clone();
            if (rhe is INumberExpression)
            {
                res.Count.Add(rhe.Count);

                return ReduceExpression(res);
            }
            else if (rhe is IComplexExpression)
            {
                res.Count.Add(rhe.Count);

                return ReduceExpression(res);
            }

            return null;
        }

        public static IExpression Subtract(IExpression lhe, IExpression rhe)
        {
            var res = lhe.Clone();
            if (rhe is INumberExpression)
            {
                res.Count.Subtract(rhe.Count);

                return ReduceExpression(res);
            }
            else if (rhe is ComplexExpression rc)
            {
                res.Count.Subtract(rc.Count);

                return ReduceExpression(res);
            }

            return null;
        }

        public static IExpression Multiply(IExpression lhe, IExpression rhe)
        {
            var res = lhe.Clone();
			if (rhe is INumberExpression)
			{
				res.Count.Multiply(rhe.Count);

				return ReduceExpression(res);
			}
			else if (rhe is ComplexExpression rc)
			{
				res.Count.Multiply(rc.Count);

				return ReduceExpression(res);
			}
			else
			{
				var other = rhe.Clone();
				other.Count.Multiply(res.Count);
				return other;
			}
        }

        public static IExpression Divide(IExpression lhe, IExpression rhe)
        {
            var res = lhe.Clone();
            if (rhe is INumberExpression)
            {
                res.Count.Divide(rhe.Count);

                return ReduceExpression(res);
            }
            else if (rhe is ComplexExpression rc)
            {
                res.Count.Divide(rc.Count);

                return ReduceExpression(res);
            }
			else
			{
                var tmp = new FlatMultExpression();
                tmp.Add(lhe);
                tmp.Add(new BinaryExpression(rhe.Clone(), OperatorTypes.Power, new NumberExpression(-1)));
                var r = tmp.Execute();
                return r;
			}
        }

        public static IExpression Pow(IExpression lhe, IExpression rhe)
        {
            var res = lhe.Clone();
            if (rhe is INumberExpression)
            {
                var n = rhe.Count.Re.ToNumber();

                if (n == 0)
                {
                    return new NumberExpression(1);
                }

                if (Math.Abs(n) < 1)
                {
                    if (res.Count.Re.Numerator < 0 && res.Count.Re.Denominator > 0)
                    {
                        res.Count.Re.Numerator *= -1;
                        res.Count.Re.Pow(rhe.Count.Re);
                        res.Count.Im = res.Count.Re;
                        res.Count.Re = new Fraction(0, 1);

                        return new ComplexExpression(res.Count);
                    }
                    else if (res.Count.Re.Numerator > 0 && res.Count.Re.Denominator < 0)
                    {
                        res.Count.Re.Denominator *= -1;
                        res.Count.Re.Pow(rhe.Count.Re);
                        res.Count.Im = res.Count.Re;
                        res.Count.Re = new Fraction(0, 1);

                        return new ComplexExpression(res.Count);
                    }
                }

                res.Count.Pow(rhe.Count);
                return res;
            }
            else if (rhe is IComplexExpression)
            {
                if (rhe.Count.Im.Numerator == 0)
                {
                    var n = rhe.Count.Re.ToNumber();

                    if (n == 0)
                    {
                        return new NumberExpression(1);
                    }

                    res.Count.Pow(rhe.Count);
                    return res;
                }
            }

            return null;
        }

        public static IExpression Sign(IExpression rhe)
        {
            var res = rhe.Clone();
            res.Count.Re.Numerator *= -1;
            return res;
        }

        private static IExpression ReduceExpression(IExpression res)
        {
            if (res is INumberExpression && res.Count.Im.Numerator != 0)
            {
                return new ComplexExpression(res.Count.Re, res.Count.Im);
            }

            if (res is IComplexExpression && res.Count.Im.Numerator == 0)
            {
                return new NumberExpression(res.Count.Re);
            }

            return res;
        }
    }
}
