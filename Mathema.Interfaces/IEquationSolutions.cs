using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Interfaces
{
    public interface IEquationSolutions
    {
        Dictionary<string, Tuple<IExpression, List<IExpression>>> Solutions { get; set; }
    }
}
