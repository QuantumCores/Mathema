using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Interfaces
{
    public interface IVariableExpression : IExpression
    {
        string Symbol { get; }

        decimal Val { get; set; }

        bool CompareDimensions(IVariableExpression variable);
    }
}
