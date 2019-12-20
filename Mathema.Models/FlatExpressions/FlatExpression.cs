using Mathema.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Models.FlatExpressions
{
    public abstract class FlatExpression : IFlatExpression
    {
        public Dictionary<string, List<IExpression>> Dimensions { get; set; } = new Dictionary<string, List<IExpression>>();

        public string DimensionKey { get; internal set; } = "FlatExpression";

        public abstract IExpression Value();        

        public abstract void Squash();

        public void Add(IExpression expression)
        {
            if (!Dimensions.ContainsKey(expression.DimensionKey))
            {
                this.Dimensions.Add(expression.DimensionKey, new List<IExpression>());
            }

            this.Dimensions[expression.DimensionKey].Add(expression);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var expressions in this.Dimensions)
            {
                foreach (var exp in expressions.Value)
                {
                    sb.Append(exp.ToString());
                }                
            }

            return sb.ToString();
        }
    }
}
