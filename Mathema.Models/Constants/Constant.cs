using Mathema.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Models.Constants
{
    public class Constant
    {
        public string Symbol { get; }

        public double Value { get; }

        public ConstantTypes Type { get; }


        public Constant(string symbol, double value, ConstantTypes type)
        {
            this.Symbol = symbol;
            this.Value = value;
            this.Type = type;
        }
    }
}
