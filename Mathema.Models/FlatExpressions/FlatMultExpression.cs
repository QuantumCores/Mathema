﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mathema.Enums.DimensionKeys;
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
                    dims.Add(key, new List<NewKeyExpressionPair>() { new NewKeyExpressionPair(key, exp) });
                }
                else
                {
                    if (key != nameof(BinaryExpression) && key != nameof(UnaryExpression) && key != nameof(FunctionExpression))
                    {
                        dims[key][0].Expression.Count.Multiply(exp.Count);
                        if (key != Dimensions.Number)
                        {
                            dims[key][0].NewKey.Add(key);
                            dims[key][0].Expression.DimensionKey = dims[key][0].NewKey;
                        }
                    }
                    else
                    {
                        dims[key].Add(new NewKeyExpressionPair(key, exp));
                    }
                }
            }

            this.Expressions = dims.ToDictionary(k => k.Value[0].NewKey.ToString(), k => k.Value.Select(s => s.Expression).ToList());
            foreach (var item in this.Expressions)
            {
                this.DimensionKey.Add(item.Key);
            }
            this.Count = this.Expressions.ContainsKey(Dimensions.Number) ? this.Expressions[Dimensions.Number][0].Count : this.Count;
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

        public override string ToString()
        {
            var sb = new List<string>();
            foreach (var key in this.Expressions.Keys.OrderBy(k => k))
            {
                var sub = string.Join(" * ", this.Expressions[key].OrderBy(e => e.ToString()));
                sb.Add(sub);
            }

            return "( " + string.Join(" * ", sb) + ")";
        }
    }

    class NewKeyExpressionPair
    {
        public NewKeyExpressionPair(string key, IExpression expr)
        {
            this.NewKey = new DimensionKey(key);
            this.Expression = expr;
        }

        internal IDimensionKey NewKey { get; set; }

        internal IExpression Expression { get; set; }
    }

}
