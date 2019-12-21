using Mathema.Interfaces;
using Mathema.Models.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mathema.Models.FlatExpressions
{
    public abstract class FlatExpression : IFlatExpression
    {
        public Dictionary<string, List<IExpression>> Dimensions { get; set; } = new Dictionary<string, List<IExpression>>();

        public string DimensionKey { get; set; } = nameof(FlatExpression);

        public IFraction Count { get; set; } = new Fraction();

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

        public static bool Compare(FlatExpression lhe, FlatExpression rhe)
        {
            if (lhe.DimensionKey != rhe.DimensionKey)
            {
                return false;
            }

            if (lhe.Dimensions.Keys.Count != rhe.Dimensions.Keys.Count)
            {
                return false;
            }
            
            foreach (var key in lhe.Dimensions.Keys)
            {
                if (!rhe.Dimensions.ContainsKey(key))
                {
                    return false;
                }
                else
                {
                    if (lhe.Dimensions[key].Count != rhe.Dimensions[key].Count)
                    {
                        return false;
                    }

                    //compare insides
                }
            }

            return true;
        }       
    }
}
