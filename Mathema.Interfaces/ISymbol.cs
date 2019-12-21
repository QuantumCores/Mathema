using Mathema.Enums.Symbols;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Interfaces
{
    public interface ISymbol
    {
        string Value { get; set; }

        SymbolTypes Type { get; set; }
    }
}
