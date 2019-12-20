using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Interfaces
{
    public interface IRPN
    {
        List<ISymbol> Output { get; set; }

        List<string> Variables { get; set; }
    }
}
