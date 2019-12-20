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
            this.DimensionType = "FlatAddExpression";
        }

        public override void Squash()
        {
            throw new NotImplementedException();
        }

        public override IExpression Value()
        {
            if (this.Dimensions.Count == 1 && this.Dimensions.ContainsKey(typeof(NumberExpression).ToString()))
            {
                var result = new NumberExpression(0m);
                this.Dimensions[typeof(NumberExpression).ToString()].ForEach(n => result = (NumberExpression)(result + (INumberExpression)n));
                return result;
            }
            else
            {
                return this;
            }
        }
    }
}
