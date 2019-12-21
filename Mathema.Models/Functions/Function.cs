using System;
using System.Collections.Generic;
using System.Text;
using static Mathema.Enums.Functions.Functions;

namespace Mathema.Models.Functions
{
    public class Function
    {
        public FunctionTypes Type { get; set; }

        public Func<decimal, decimal> Projection { get; set; }

        public Function(FunctionTypes type, Func<decimal, decimal> projection)
        {
            this.Type = type;
            this.Projection = projection;
        }
    }
}
