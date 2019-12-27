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
    public class FlatAddExpression : FlatExpression, IFlatAddExpression
    {
        public FlatAddExpression()
        {
            this.BinaryOperations = FlatAddOperations.BinaryOperations;
            this.UnaryOperations = FlatAddOperations.UnaryOperations;
            this.DimensionKey = new DimensionKey(nameof(FlatAddExpression));
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
                    if (exec is FlatAddExpression mult)
                    {
                        foreach (var mel in mult.Expressions)
                        {
                            foreach (var me in mel.Value)
                            {
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
                var key = exp.DimensionKey.ToString();
                if (!dims.ContainsKey(key))
                {
                    dims.Add(key, new List<IExpression>() { exp });
                }
                else
                {
                    if (key != nameof(BinaryExpression) && key != nameof(UnaryExpression) && key != nameof(FunctionExpression))
                    {
                        dims[key][0] = dims[key][0].BinaryOperations[OperatorTypes.Add](dims[key][0], exp);
                    }
                    else
                    {
                        dims[key].Add(exp);
                    }
                }
            }

            this.Expressions = dims;
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
            var res = new FlatAddExpression();

            foreach (var dim in this.Expressions.Values)
            {
                foreach (var exp in dim)
                {
                    res.Add(exp.Clone());
                }
            }

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

        public override string ToString()
        {
            var sb = new List<string>();
            foreach (var key in this.Expressions.Keys.OrderBy(k => k))
            {
                var sub = string.Join(" + ", this.Expressions[key].OrderBy(e => e.ToString()));
                sb.Add(sub);
            }

            return "( " + string.Join(" + ", sb) + ")";
        }
    }
}
