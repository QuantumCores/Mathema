using Mathema.Models.Symbols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Mathema.Algorithms.Helpers
{
    public class RPNComparer
    {
        public static bool Compare(List<Symbol> rpn, string rpnAsString)
        {
            var tmp = string.Join("", rpn.Select(r => r.Value));
            var tmp2 = Regex.Replace(rpnAsString, @"\s+", "");
            return tmp == tmp2;
        }
    }
}
