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
    public class FlatMultExpression : FlatExpression, IFlatMultExpression
    {
        public FlatMultExpression()
        {
            this.BinaryOperations = FlatMultOperations.BinaryOperations;
            this.UnaryOperations = FlatMultOperations.UnaryOperations;
            this.DimensionKey = new DimensionKey();//nameof(FlatMultExpression)
            //this.Expressions.Add(Dimensions.Number, new List<IExpression>() { new NumberExpression(1) });
        }

        public override void Add(IExpression expression)
        {
            this.Count.Multiply(expression.Count);
            expression.Count = new Complex(1, 0);

            if (expression is FlatAddExpression)
            {
                var key = expression.ToString();
                if (!this.Expressions.ContainsKey(key))
                {
                    this.Expressions.Add(key, new List<IExpression>());
                }

                this.Expressions[key].Add(expression);
            }
            else if (expression is NumberExpression || expression is ComplexExpression)
            {
                //do not add beacuse we use them only to calculate Count
            }
            else
            {
                var key = expression.DimensionKey.ToString();
                if (!Expressions.ContainsKey(key))
                {
                    this.Expressions.Add(key, new List<IExpression>());
                }
                this.Expressions[key].Add(expression);
            }
        }

        public override void Remove(string key, decimal value)
        {
            if (this.Expressions.ContainsKey(key))
            {
                this.Expressions.Remove(key);
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
                    // Calling Value simplifies expressions
                    if (exec is FlatMultExpression mult)
                    {
                        foreach (var mel in mult.Expressions)
                        {
                            foreach (var me in mel.Value)
                            {
                                var mex = me.Execute();
                                all.Add(mex);
                            }
                        }
                    }
                    else if (exec is FlatAddExpression add)
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
            //TODO what if flat expression? Shouldn't we merge both together?
            //TODO FlatMult DimensionKey is invalid
            foreach (var exp in all)
            {
                if (exp.Count.Re.Numerator != 0 || exp.Count.Im.Numerator != 0)
                {
                    var key = exp.DimensionKey.Key;
                    if (key == Dimensions.Number || key == Dimensions.Complex)
                    {
                        this.Count.Multiply(exp.Count);
                        continue;
                    }

                    if (!dims.ContainsKey(key))
                    {
                        //this.Count.Multiply(exp.Count);
                        dims.Add(key, new List<IExpression>() { exp });
                    }
                    else
                    {
                        if (key != nameof(BinaryExpression) && key != nameof(UnaryExpression))
                        {
                            //TODO when exp has DimKey value 2 then count is (n)^2?
                            this.Count.Multiply(exp.Count);
                            exp.Count = new Complex();

                            Reduce(dims, exp, key);
                        }
                        else
                        {
                            dims[key].Add(exp);
                        }
                    }
                }
                else
                {
                    dims = new Dictionary<string, List<IExpression>>();
                    this.Count.Re.Numerator = 0;
                    this.Count.Im.Numerator = 0;
                    break;
                }
            }

            this.Expressions = dims;
            //foreach (var item in this.Expressions)
            //{
            //    this.DimensionKey.Add(item.Value[0].DimensionKey);
            //}

            this.UpdateDimensionKey(false);
        }

        private static void Reduce(Dictionary<string, List<IExpression>> dims, IExpression exp, string key)
        {
            var res = dims[key][0].BinaryOperations[OperatorTypes.Multiply](dims[key][0], exp);
            if (key != res.DimensionKey.Key)
            {
                dims.Remove(key);
                var resKey = res.DimensionKey.ToString();
                if (dims.ContainsKey(resKey))
                {
                    Reduce(dims, res, resKey);
                }
                else
                {
                    dims.Add(resKey, new List<IExpression>() { res });
                }
            }
            else
            {
                //TODO else
                dims[key][0] = res;
                //dims[key][0].NewKey = res.DimensionKey;
                //throw new NotImplementedException("Just Wondering if we ever get here");
            }
        }

        public override IExpression Execute()
        {
            this.Squash();
            if (this.Expressions.Count == 0)
            {
                if (this.Count.Im.Numerator == 0)
                {
                    return new NumberExpression(this.Count.Re);
                }
                else
                {
                    return new ComplexExpression(this.Count);
                }
            }
            else if (this.Expressions.Count == 1)
            {
                var res = this.Expressions.ElementAt(0).Value[0];
                res.Count.Multiply(this.Count);
                //Reset counter when extracting
                this.Count = new Complex();
                return res;
            }
            else
            {
                return this;
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

            this.DimensionKey.Key = string.Join("", this.Dimension());

            return;
        }

        private List<string> Dimension()
        {
            var sb = new List<string>();
            var addOne = true;

            foreach (var kv in this.Expressions.OrderByDescending(e => e.Value[0].DimensionKey.Value.ToNumber()).ThenBy(e => e.Value[0].DimensionKey.Key))
            {
                var expr = kv.Value[0];
                var p = expr.DimensionKey.Value.ToNumber();
                if (p > 0)
                {
                    addOne = false;
                }

                if (sb.Count > 0)
                {
                    if (expr.DimensionKey.Value.ToNumber() < 0)
                    {
                        sb.Add(" / ");
                    }
                    else
                    {
                        sb.Add(" * ");
                        addOne = false;
                    }
                }

                if (kv.Value.Count > 1)
                {
                    sb.Add(string.Join("*", kv.Value.Select(e => e.DimensionKey.ToString())));
                }
                else
                {
                    if (p < 0)
                    {
                        if (p == -1)
                        {
                            sb.Add(expr.DimensionKey.Key);
                        }
                        else
                        {
                            var tmp = expr.DimensionKey.Value.Clone();
                            tmp.Multiply(new Fraction(-1, 1));
                            sb.Add("(" + expr.DimensionKey.Key + ")^" + tmp.ToString());
                        }
                    }
                    else
                    {
                        if (p == 1)
                        {
                            sb.Add(expr.DimensionKey.Key);
                        }
                        else
                        {
                            sb.Add("(" + expr.DimensionKey.Key + ")^" + expr.DimensionKey.Value.ToString());
                        }
                    }
                }
            }

            if (addOne)
            {
                sb.Insert(0, "1 / ");
            }

            //return "( " + string.Join("", sb) + ")";
            return sb;
        }

        public override IExpression Clone()
        {
            var res = new FlatMultExpression();

            foreach (var dim in this.Expressions.Values)
            {
                foreach (var exp in dim)
                {
                    res.Add(exp.Clone());
                }
            }

            res.DimensionKey = this.DimensionKey.Clone();
            res.Count = this.Count.Clone();

            return res;
        }

        public static FlatMultExpression operator *(FlatMultExpression lhe, FlatMultExpression rhe)
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

        public static FlatMultExpression operator *(FlatMultExpression lhe, IExpression rhe)
        {
            lhe.Add(rhe);

            return lhe;
        }

        public static FlatMultExpression operator *(IExpression lhe, FlatMultExpression rhe)
        {
            rhe.Add(lhe);

            return rhe;
        }

        public static FlatMultExpression operator /(FlatMultExpression lhe, IExpression rhe)
        {
            //TODO this is wrong because we change rhe
            var res = (FlatMultExpression)lhe.Clone();
            //for (int i = 0; i < rhe.DimensionKey.Key.Count; i++)
            //{
            //    var key = rhe.DimensionKey.Key.ElementAt(i).Key;
            //    rhe.DimensionKey.Multiply(key, -1);
            //}

            res.Add(rhe);

            return lhe;
        }

        public static FlatMultExpression operator /(IExpression lhe, FlatMultExpression rhe)
        {
            var res = lhe.Clone();
            //foreach (var kv in lhe.DimensionKey.Key)
            //{
            //    lhe.DimensionKey.Multiply(kv.Key, -1);
            //}

            rhe.Add(lhe);

            return rhe;
        }

        public override string AsString()
        {
            return this.ToString();
        }

        public override string ToString()
        {
            var sb = this.Dimension();
            if (this.Count.Re.ToNumber() != 1 && this.Count.Im.ToNumber() == 0)
            {
                if (sb[0] == "1 / ")
                {
                    sb.RemoveAt(0);
                }
                sb.Insert(0, this.Count.AsString() + " * ");
            }

            //return "( " + string.Join("", sb) + ")";
            return string.Join("", sb);
        }
    }
}
