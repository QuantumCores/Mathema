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

        public Dictionary<string, List<decimal>> SearchResult { get; set; } = new Dictionary<string, List<decimal>>();
    }
}
