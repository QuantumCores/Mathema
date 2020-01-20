using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Interfaces
{
    public interface ISolver
    {
        IEquationSolutions Solve(IExpression expression, string variable);
    }
}
