﻿using Mathema.Enums.Operators;
using Mathema.Interfaces;
using Mathema.Models.Dimension;
using Mathema.Models.Numerics;
using Mathema.Models.Operators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Models.Expressions
{
    public class UnaryExpression : IExpression
    {
        private OperatorTypes op;
        private IExpression rhe;

        public IDimensionKey DimensionKey { get; set; } = new DimensionKey(nameof(UnaryExpression));

        public IComplex Count { get; set; } = new Complex();

        public Dictionary<OperatorTypes, Func<IExpression, IExpression, IExpression>> BinaryOperations { get; }

        public Dictionary<OperatorTypes, Func<IExpression, IExpression>> UnaryOperations { get; }

        public UnaryExpression(OperatorTypes op, IExpression rhe)
        {
            this.rhe = rhe;
            this.op = op;
        }

        public IExpression Execute()
        {
            var arg = this.rhe.Execute();

            if (arg != null)
            {
                this.rhe = arg;
            }

            var res = arg.UnaryOperations[op](arg);

            if (res == null)
            {
                return this;
            }

            return res;
        }
        public void UpdateDimensionKey(bool deep)
        {
            if (deep)
            {
                this.rhe.UpdateDimensionKey(deep);
            }

            return;
        }

        public IExpression Clone()
        {
            return new UnaryExpression(this.op, this.rhe.Clone());
        }

        public string AsString()
        {
            return this.ToString();
        }

        public override string ToString()
        {
            if (op == OperatorTypes.Sign)
            {
                return " -(" + this.rhe.Execute() + ")";
            }
            else
            {
                return " " + Operators.Operators.Get(op).Symbol + "(" + this.rhe.Execute() + ")";
            }
        }
    }
}
