using Mathema.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Models.Equations
{
    public class EquationSolutions : IEquationSolutions
    {
        public Dictionary<string, Tuple<IExpression, List<IExpression>>> Solutions { get; set; } = new Dictionary<string, Tuple<IExpression, List<IExpression>>>();
    }
}
