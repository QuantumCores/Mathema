using Mathema.Models.Functions;
using Mathema.Models.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mathema.Texts
{
    public class AllOperatorsText
    {
        public static string Write()
        {
            var allLines = new List<string>();
            allLines.Add("Operators that you can use to write equations.");
            var str = string.Join(Environment.NewLine, Operators.All.Select(o => o.Symbol + " - " + o.Type + "  ( " + o.OperationType.ToString() + " )"));
            var str2 = string.Join(Environment.NewLine, Functions.All.Select(f => f.Value.Type + "()"));

            return str + Environment.NewLine + str2;
        }
    }
}
