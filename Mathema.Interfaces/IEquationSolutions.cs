using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Interfaces
{
    public interface IEquationSolutions
    {
        List<IExpression> Solutions { get; set; }
    }
}
