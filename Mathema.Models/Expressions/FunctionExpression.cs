﻿using Mathema.Interfaces;
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

        public FunctionExpression(FunctionTypes type, IExpression argument)
        {
            this.type = type;
            this.argument = argument;
        }

        public decimal Value()
        {
            return Functions.Functions.Get(type).Projection(argument.Value());
        }
    }
}
