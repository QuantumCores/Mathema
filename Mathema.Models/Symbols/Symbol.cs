using System;

namespace Mathema.Models.Symbols
{
    public class Symbol
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
