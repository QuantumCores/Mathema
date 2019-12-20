using Mathema.Interfaces;
using Mathema.Models.Symbols;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Algorithms.Parsers
{
    public class RPN : IRPN
    {
        public RPN(List<ISymbol> output, List<string> variables)
        {
            this.Output = output;
            this.Variables = variables;
        }

        public List<ISymbol> Output { get; set; } = new List<ISymbol>();

        public List<string> Variables { get; set; } = new List<string>();
    }
}
