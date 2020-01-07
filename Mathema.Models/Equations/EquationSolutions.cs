using Mathema.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Models.Equations
{
    public class EquationSolutions : IEquationSolutions
    {
        public List<IExpression> Solutions { get; set; }
    }
}
