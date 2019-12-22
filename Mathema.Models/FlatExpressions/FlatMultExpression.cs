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

        public override void Squash()
        {
            var all = new List<IExpression>();
            foreach (var expressions in this.Expressions)
            {
                foreach (var exp in expressions.Value)
                {
                    // Calling Vlaue simplifies expressions
                    all.Add(exp.Execute());
                }
            }

            var dims = new Dictionary<string, List<NewKeyExpressionPair>>();
            foreach (var exp in all)
            {
                var key = exp.DimensionKey.ToString();
                if (!dims.ContainsKey(key))
                {
                    dims.Add(key, new List<NewKeyExpressionPair>() { new NewKeyExpressionPair(exp) });
                }
                else
                {
                    if (key != nameof(BinaryExpression) && key != nameof(UnaryExpression) && key != nameof(FunctionExpression))
                    {
                        dims[key][0].Expression.Count.Multiply(exp.Count);
                        if (key != Dimensions.Number)
                        {
                            Reduce(dims, exp, key);
                        }
                    }
                    else
                    {
                        dims[key].Add(new NewKeyExpressionPair(key, exp));
                    }
                }
            }

            this.Expressions = dims.ToDictionary(k => k.Key, k => k.Value.Select(s => s.Expression).ToList());
            foreach (var item in this.Expressions)
            {
                this.DimensionKey.Add(item.Key);
            }
            this.Count = this.Expressions.ContainsKey(Dimensions.Number) ? this.Expressions[Dimensions.Number][0].Count : this.Count;
        }

        private static void Reduce(Dictionary<string, List<NewKeyExpressionPair>> dims, IExpression exp, string key)
        {
            var res = dims[key][0].Expression.BinaryOperations[OperatorTypes.Multiply](dims[key][0].Expression, exp);
            if (!Dimension.DimensionKey.Compare(dims[key][0].NewKey, res.DimensionKey))
            {
                dims.Remove(key);
                var resKey = res.DimensionKey.ToString();
                if (dims.ContainsKey(resKey))
                {
                    Reduce(dims, res, resKey);
                }
                else
                {
                    dims.Add(resKey, new List<NewKeyExpressionPair>() { new NewKeyExpressionPair(res) });
                }
            }
        }

        public override IExpression Execute()
        {
            this.Squash();
            if (this.Expressions.Count == 1 && this.Expressions.ContainsKey(Dimensions.Number))
            {
                return this.Expressions[Dimensions.Number][0];
            }
            else
            {
                return this;
            }
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
            for (int i = 0; i < rhe.DimensionKey.Key.Count; i++)
            {
                var key = rhe.DimensionKey.Key.ElementAt(i).Key;
                rhe.DimensionKey.Multiply(key, -1);
            }

            lhe.Add(rhe);

            return lhe;
        }

        public static FlatMultExpression operator /(IExpression lhe, FlatMultExpression rhe)
        {
            foreach (var kv in lhe.DimensionKey.Key)
            {
                lhe.DimensionKey.Multiply(kv.Key, -1);
            }

            rhe.Add(lhe);

            return rhe;
        }

        public override string ToString()
        {
            var sb = new List<string>();
            IExpression prev = null;
            if (this.Expressions.ContainsKey(Dimensions.Number))
            {
                sb.Add(this.Expressions[Dimensions.Number][0].ToString());
                prev = this.Expressions[Dimensions.Number][0];
            }

            //TODO order by expr.ToString
            foreach (var kv in this.Expressions)
            {
                if (kv.Key != Dimensions.Number)
                {
                    var expr = this.Expressions[kv.Key][0];
                    if (prev != null)
                    {
                        if (expr.DimensionKey.Key.ElementAt(0).Value < 0)
                        {
                            sb.Add(" / ");
                        }
                        else
                        {
                            sb.Add(" * ");
                        }
                    }
                    sb.Add(expr.ToString());
                    prev = expr;
                }
            }

            return "( " + string.Join("", sb) + ")";
        }
    }

    class NewKeyExpressionPair
    {
        public NewKeyExpressionPair(IExpression expr)
        {
            this.NewKey = expr.DimensionKey.Clone();
            this.Expression = expr;
        }

        public NewKeyExpressionPair(string key, IExpression expr)
        {
            this.NewKey = new DimensionKey(key);
            this.Expression = expr;
        }

        internal IDimensionKey NewKey { get; set; }

        internal IExpression Expression { get; set; }
    }

}
