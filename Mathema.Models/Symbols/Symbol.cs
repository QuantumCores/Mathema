using Mathema.Enums.Symbols;
using Mathema.Interfaces;
using System;

namespace Mathema.Models.Symbols
{
    public class Symbol : ISymbol
    {
        public Symbol(string value, SymbolTypes type)
        {
            this.Value = value;
            this.Type = type;
        }

        public string Value { get; set; }

        public SymbolTypes Type { get; set; }
    }
}
