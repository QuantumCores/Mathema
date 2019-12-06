using Mathema.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mathema.Models.Expressions
{
    public class ExpressionResult : IExpressionResult
    {
        public Dictionary<string, decimal> Dimensions { get; } = new Dictionary<string, decimal>();

        public ExpressionResult(string symbol, decimal value)
        {
            if (!this.Dimensions.ContainsKey(symbol))
            {
                this.Dimensions.Add(symbol, value);
            }
            else
            {
                this.Dimensions[symbol] += value;
            }
        }

        public static IExpressionResult operator +(ExpressionResult lhe, IExpressionResult rhe)
        {
            var missing = lhe.Dimensions.Keys.Except(rhe.Dimensions.Keys);
            
            foreach (var m in missing)
            {
                lhe.Dimensions.Add(m, rhe.Dimensions[m]);
            }

            foreach (var kv in rhe.Dimensions)
            {
                lhe.Dimensions[kv.Key] += kv.Value;
            }

            return lhe;
        }
    }
}
