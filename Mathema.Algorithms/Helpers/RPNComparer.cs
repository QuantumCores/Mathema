using Mathema.Interfaces;
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
        public static bool Compare(List<ISymbol> rpn, string rpnAsString)
        {
            var tmp = string.Join("", rpn.Select(r => r.Value));
            var tmp2 = Regex.Replace(rpnAsString, @"\s+", "");
            return tmp == tmp2;
        }

        public static bool Compare(List<ISymbol> rpnA, List<ISymbol> rpnB)
        {
            if (rpnA.Count != rpnB.Count)
            {
                return false;
            }

            var ao = rpnA.OrderBy(s => s.Type).ThenBy(s => s.Value).ToList();
            var bo = rpnA.OrderBy(s => s.Type).ThenBy(s => s.Value).ToList();
            for (int i = 0; i < rpnA.Count; i++)
            {
                var a = ao[i];
                var b = bo[i];
                if (a.Type != b.Type)
                {
                    return false;
                }
                
                if (a.Value != b.Value)
                {
                    return false;
                }
            }            


            return true;
        }
    }
}
