using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mathema.Enums.DimensionKeys;
using Mathema.Enums.Operators;
using Mathema.Interfaces;
using Mathema.Models.Dimension;
using Mathema.Models.ExpressionOperations;
using Mathema.Models.Expressions;
using Mathema.Models.FunctionExpressions;
using Mathema.Models.Numerics;

namespace Mathema.Models.FlatExpressions
{
    public class FlatAddExpression : FlatExpression, IFlatAddExpression
    {
        public FlatAddExpression()
        {
            this.BinaryOperations = FlatAddOperations.BinaryOperations;
            this.UnaryOperations = FlatAddOperations.UnaryOperations;
            this.DimensionKey = new DimensionKey(nameof(FlatAddExpression));
        }

        public override void Add(IExpression expression)
        {
            //TODO
            //if (expression is FlatExpression)
            //{

            //}
            //else
            {
                var tmp = expression.DimensionKey.ToString();
                if (!Expressions.ContainsKey(tmp))
                {
                    this.Expressions.Add(tmp, new List<IExpression>());
                }
                this.Expressions[tmp].Add(expression);
                this.UpdateDimensionKey(false);
            }
        }

        public override void Squash()
        {
            var all = new List<IExpression>();
            foreach (var expressions in this.Expressions)
            {
                foreach (var exp in expressions.Value)
                {
                    var exec = exp.Execute();
                    // Calling Vlaue simplifies expressions
                    if (exec is FlatAddExpression fae)
                    {
                        foreach (var mel in fae.Expressions)
                        {
                            foreach (var me in mel.Value)
                            {
                                me.Count.Multiply(exec.Count);
                                all.Add(me);
                            }
                        }
                    }
                    else if (exec is FlatMultExpression add)
                    {
                        all.Add(exec);
                    }
                    else
                    {
                        all.Add(exec);
                    }
                }
            }

            var dims = new Dictionary<string, List<IExpression>>();
            foreach (var exp in all)
            {
                if (exp.Count.Re.Numerator != 0 || exp.Count.Im.Numerator != 0)
                {
                    var key = exp.DimensionKey.ToString();
                    if (key == Dimensions.Complex || key == Dimensions.Number)
                    {
                        key = Dimensions.Complex;
                    }

                    if (!dims.ContainsKey(key))
                    {
                        dims.Add(key, new List<IExpression>() { exp });
                    }
                    else
                    {
                        if (key != nameof(BinaryExpression) && key != nameof(UnaryExpression))
                        {
                            var res = dims[key][0].BinaryOperations[OperatorTypes.Add](dims[key][0], exp);
                            if (res.Count.Re.Numerator == 0 && res.Count.Im.Numerator == 0)
                            {
                                dims.Remove(key);
                                break;
                            }
                            else
                            {
                                dims[key][0] = res;
                            }
                        }
                        else
                        {
                            dims[key].Add(exp);
                        }
                    }
                }
            }

            this.Expressions = dims;
            this.UpdateDimensionKey(false);
        }

        public override IExpression Execute()
        {
            this.Squash();
            if (this.Expressions.Count == 0)
            {
                return new NumberExpression(0);
            }
            else if (this.Expressions.Count == 1 && this.Expressions.ElementAt(0).Key == Dimensions.Number)
            {
                return this.Expressions[Dimensions.Number][0];
            }
            else if (this.Expressions.Count == 1 && this.Expressions.ElementAt(0).Key == Dimensions.Complex)
            {
                return this.Expressions[Dimensions.Complex][0];
            }
            else
            {
                return this;
            }
        }

        public override IExpression Clone()
        {
            var res = new FlatAddExpression();

            foreach (var kv in this.Expressions)
            {
                res.Expressions.Add(kv.Key, kv.Value);
            }

            res.DimensionKey = this.DimensionKey.Clone();
            res.Count = this.Count.Clone();

            return res;
        }

        public static FlatAddExpression operator +(FlatAddExpression lhe, FlatAddExpression rhe)
        {
            foreach (var key in lhe.Expressions.Keys)
            {
                if (rhe.Expressions.ContainsKey(key))
                {
                    lhe.Expressions[key].AddRange(rhe.Expressions[key]);
                }
            }

            foreach (var key in rhe.Expressions.Keys)
            {
                if (!lhe.Expressions.ContainsKey(key))
                {
                    lhe.Expressions.Add(key, rhe.Expressions[key]);
                }
            }

            return lhe;
        }

        public static FlatAddExpression operator +(FlatAddExpression lhe, IExpression rhe)
        {
            lhe.Add(rhe);

            return lhe;
        }

        public static FlatAddExpression operator +(IExpression lhe, FlatAddExpression rhe)
        {
            rhe.Add(lhe);

            return rhe;
        }

        public override string AsString()
        {
            return this.ToString();
        }

        public override string ToString()
        {
            var count = "";

            if (this.Count.Re.ToNumber() != 1 && this.Count.Im.ToNumber() == 0)
            {
                count = this.Count.AsString() + " * ";
            }

            var dim = this.DimensionKey;
            if (dim.Value.Numerator != 1 && dim.Value.Denominator !=1 )
            {
                if (dim.Value.ToNumber() > 0)
                {
                    return count + "(" + dim.Key + ")^" + dim.Value;
                }
                else
                {
                    return count + "(" + dim.Key + ")^(" + dim.Value + ")";
                }
            }
            else
            {
                return count + dim.Key;
            }
        }

        public override void UpdateDimensionKey(bool deep)
        {
            if (deep)
            {
                foreach (var kv in this.Expressions)
                {
                    kv.Value.ForEach(e => e.UpdateDimensionKey(true));
                }
            }

            this.DimensionKey.Key = this.ExpressionKey();
        }

        private string ExpressionKey()
        {
            var sb = new List<string>();
            foreach (var key in this.Expressions.Keys.OrderBy(k => k))
            {
                var expr = this.Expressions[key][0];
                var sub = string.Join(" + ", this.Expressions[key].OrderBy(e => e.ToString()));

                if (sb.Count != 0)
                {
                    if (expr.Count.Im.Numerator == 0)
                    {
                        if (expr.Count.Re.ToNumber() > 0)
                        {
                            sub = " + " + sub;
                        }
                    }
                    else if (expr.Count.Re.Numerator == 0)
                    {
                        if (expr.Count.Im.ToNumber() > 0)
                        {
                            sub = " + " + sub;
                        }
                    }
                }

                sb.Add(sub);
            }

            if (sb.Count == 1)
            {
                return sb[0];
            }
            else
            {
                return "( " + string.Join("", sb) + ")";
            }

        }

        public override void Remove(string key, decimal value)
        {
            if (this.Expressions.ContainsKey(key))
            {
                this.Expressions.Remove(key);
            }
        }
    }
}
