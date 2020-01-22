using Mathema.Enums.Equations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Interfaces
{
    public interface IClassificationResult
    {
        EquationTypes EquationType { get; set; }

        Dictionary<string, List<decimal>> SearchResult { get; set; }
    }
}
