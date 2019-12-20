using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mathema.Interfaces;
using Mathema.Models.Expressions;

namespace Mathema.Models.FlatExpressions
{
    public class FlatAddExpression : FlatExpression
    {
        public FlatAddExpression()
        {
            this.DimensionKey = "FlatAddExpression";
        }

        public override void Squash()
        {
            throw new NotImplementedException();
        }

        public override IExpression Value()
        {
            var all = new List<IExpression>();
            foreach (var expressions in this.Dimensions)
            {
                foreach (var exp in expressions.Value)
                {
                    all.Add(exp.Value());
                }
            }

            var dims = new Dictionary<string, List<IExpression>>();
            foreach (var exp in all)
            {
                if (!dims.ContainsKey(exp.DimensionKey))
                {
                    dims.Add(exp.DimensionKey, new List<IExpression>());
                }

                dims[exp.DimensionKey].Add(exp);
            }

            var result = new NumberExpression(0m);
            var numKey = result.DimensionKey;
            if (dims.ContainsKey(numKey))
            {
                dims[numKey].ForEach(n => result = (NumberExpression)(result + (INumberExpression)n));
                dims[numKey] = new List<IExpression>() { result };
            }
            this.Dimensions = dims;


            if (this.Dimensions.Count == 1 && this.Dimensions.ContainsKey(numKey))
            {
                return this.Dimensions[numKey][0];
            }
            else
            {
                return this;
            }
        }

        public static FlatAddExpression operator +(FlatAddExpression lhe, FlatAddExpression rhe)
        {
            foreach (var key in lhe.Dimensions.Keys)
            {
                if (rhe.Dimensions.ContainsKey(key))
                {
                    lhe.Dimensions[key].AddRange(rhe.Dimensions[key]);
                }
            }

            foreach (var key in rhe.Dimensions.Keys)
            {
                if (!lhe.Dimensions.ContainsKey(key))
                {
                    lhe.Dimensions.Add(key, rhe.Dimensions[key]);
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
            foreach (var key in this.Dimensions.Keys.OrderBy(k => k))
            {
                var sub = string.Join(" + ", this.Dimensions[key].OrderBy(e => e));
                sb.Add(sub);
            }

            return string.Join(" + ", sb);
        }
    }
}
