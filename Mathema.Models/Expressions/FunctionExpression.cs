using Mathema.Interfaces;
using Mathema.Models.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Models.Expressions
{
    public class FunctionExpression : IExpression
    {
        private FunctionTypes type;
        private IExpression argument;

        public string DimensionType { get; } = "FunctionExpression";

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
                return new NumberExpression(Functions.Functions.Get(type).Projection(((INumberExpression)arg).Val));
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
