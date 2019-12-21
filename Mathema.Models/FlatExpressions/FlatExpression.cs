using Mathema.Enums.Operators;
using Mathema.Interfaces;
using Mathema.Models.Dimension;
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

        public IDimensionKey DimensionKey { get; set; } = new DimensionKey(nameof(FlatExpression));

        public IFraction Count { get; set; } = new Fraction();

        public Dictionary<OperatorTypes, Func<IExpression, IExpression, IExpression>> BinaryOperations { get; set; }

        public Dictionary<OperatorTypes, Func<IExpression, IExpression>> UnaryOperations { get; set; }

        public abstract IExpression Execute();

        public abstract void Squash();

        public void Add(IExpression expression)
        {
            var tmp = expression.DimensionKey.ToString();
            if (!Dimensions.ContainsKey(tmp))
            {
                this.Dimensions.Add(tmp, new List<IExpression>());
            }

            this.Dimensions[tmp].Add(expression);
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
