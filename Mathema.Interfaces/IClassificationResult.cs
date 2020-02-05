using Mathema.Enums.Equations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Interfaces
{
    public interface IClassificationResult
    {
        EquationTypes EquationType { get; set; }

        Dictionary<string, Tuple<IExpression, List<IFraction>>> SearchResult { get; set; }
    }
}
