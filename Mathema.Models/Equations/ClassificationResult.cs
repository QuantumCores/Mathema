using Mathema.Enums.Equations;
using Mathema.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Models.Equations
{
    public class ClassificationResult : IClassificationResult
    {
        public EquationTypes EquationType { get; set; }

        public Dictionary<string, Tuple<IExpression, List<IFraction>>> SearchResult { get; set; } = new Dictionary<string, Tuple<IExpression, List<IFraction>>>();
    }
}
