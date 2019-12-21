using Mathema.Interfaces;
using Mathema.Models.Functions;
using Mathema.Models.Numerics;
using System;
using System.Collections.Generic;
using System.Text;
using static Mathema.Enums.Functions.Functions;

namespace Mathema.Models.Expressions
{
    public class FunctionExpression : IExpression
    {
        private FunctionTypes type;
        private IExpression argument;

        public string DimensionKey { get; } = nameof(FunctionExpression);

        public IFraction Count { get; set; } = new Fraction();

        public FunctionExpression(FunctionTypes type, IExpression argument)
        {
            this.type = type;
            this.argument = argument;
        }

        public IExpression Value()
        {
            var arg = this.argument.Value();
            if (arg is INumberExpression)
            {
                return new NumberExpression(Functions.Functions.Get(type).Projection(arg.Count.ToNumber()));
            }
            else
            {
                return this;
            }
        }

        public override string ToString()
        {
            return " " + type.ToString() + "(" + argument.ToString() + ")";
        }
    }
}
