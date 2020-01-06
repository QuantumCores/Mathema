﻿using Mathema.Enums.Functions;
using Mathema.Enums.Operators;
using Mathema.Interfaces;
using Mathema.Models.Dimension;
using Mathema.Models.ExpressionOperations;
using Mathema.Models.Functions;
using Mathema.Models.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mathema.Models.Expressions
{
    public class FunctionExpression : IExpression
    {
        private FunctionTypes type;
        private IExpression argument;

        public IDimensionKey DimensionKey { get; set; } = new DimensionKey(nameof(FunctionExpression));

        public IFraction Count { get; set; } = new Fraction();

        public Dictionary<OperatorTypes, Func<IExpression, IExpression, IExpression>> BinaryOperations { get; } = FunctionOperations.BinaryOperations;

        public Dictionary<OperatorTypes, Func<IExpression, IExpression>> UnaryOperations { get; } = FunctionOperations.UnaryOperations;

        public FunctionExpression(FunctionTypes type, IExpression argument)
        {
            this.type = type;
            this.argument = argument;
            UpdateDimensionKey();
        }

        public IExpression Execute()
        {
            var arg = this.argument.Execute();
            if (arg is INumberExpression)
            {
                return new NumberExpression(Functions.Functions.Get(type).Projection(arg.Count.ToNumber()));
            }
            else
            {
                return this;
            }
        }

        public IExpression Clone()
        {
            return new FunctionExpression(this.type, this.argument.Clone());
        }

        public string AsString()
        {
            return this.ToString();
        }

        public override string ToString()
        {
            if (this.Count.ToNumber() != 1)
            {
                return this.Count.AsString() + "*" + this.ExpressionKey();
            }

            return this.ExpressionKey();
        }

        private void UpdateDimensionKey()
        {
            var newDim = new Dictionary<string, decimal>();
            newDim.Add(this.ExpressionKey(), this.DimensionKey.Key.ElementAt(0).Value);
            this.DimensionKey.Key = newDim;
        }

        private string ExpressionKey()
        {
            return type.ToString() + "(" + argument.ToString() + ")";
        }
    }
}
