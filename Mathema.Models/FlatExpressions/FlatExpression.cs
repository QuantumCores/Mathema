﻿using Mathema.Enums.Operators;
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
        /// <summary>
        /// String key is just a basic variable symbol should not cotain more information
        /// </summary>        
        public Dictionary<string, List<IExpression>> Expressions { get; set; } = new Dictionary<string, List<IExpression>>();

        public IDimensionKey DimensionKey { get; set; } = new DimensionKey(nameof(FlatExpression));

        public IComplex Count { get; set; } = new Complex();

        public Dictionary<OperatorTypes, Func<IExpression, IExpression, IExpression>> BinaryOperations { get; set; }

        public Dictionary<OperatorTypes, Func<IExpression, IExpression>> UnaryOperations { get; set; }

        public abstract IExpression Execute();

        public abstract void UpdateDimensionKey(bool deep);

        public abstract void Squash();

        public abstract void Add(IExpression expression);

        public abstract void Remove(string key, decimal value);

        public abstract IExpression Clone();

        public abstract string AsString();

        public static bool Compare(FlatExpression lhe, FlatExpression rhe)
        {
            if (!Dimension.DimensionKey.Compare(lhe.DimensionKey, rhe.DimensionKey))
            {
                return false;
            }

            if (lhe.Expressions.Keys.Count != rhe.Expressions.Keys.Count)
            {
                return false;
            }

            foreach (var key in lhe.Expressions.Keys)
            {
                if (!rhe.Expressions.ContainsKey(key))
                {
                    return false;
                }
                else
                {
                    if (lhe.Expressions[key].Count != rhe.Expressions[key].Count)
                    {
                        return false;
                    }

                    for (int i = 0; i < lhe.Expressions[key].Count; i++)
                    {
                        var expl = lhe.Expressions[key][i];
                        var expr = rhe.Expressions[key][i];

                        if (!Dimension.DimensionKey.Compare(expl.DimensionKey, expr.DimensionKey))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }


    }
}
