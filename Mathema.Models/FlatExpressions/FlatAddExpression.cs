using System;
using System.Collections.Generic;
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

            this.Dimensions = dims;

            var result = new NumberExpression(0m);
            var numKey = result.DimensionKey;
            if (this.Dimensions.Count == 1 && this.Dimensions.ContainsKey(numKey))
            {
                this.Dimensions[numKey].ForEach(n => result = (NumberExpression)(result + (INumberExpression)n));
                return result;
            }
            else
            {
                return this;
            }
        }
    }
}
