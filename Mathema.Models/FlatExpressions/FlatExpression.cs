using Mathema.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Models.FlatExpressions
{
    public abstract class FlatExpression : IFlatExpression
    {
        public Dictionary<string, List<IExpression>> Dimensions { get; }

        public string DimensionType { get; internal set; } = "FlatExpression";

        public abstract IExpression Value();        

        public abstract void Squash();

        public void Add(IExpression expression)
        {
            this.Dimensions[expression.GetType().ToString()].Add(expression);
        }
    }
}
