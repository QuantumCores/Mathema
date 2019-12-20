using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Interfaces
{
    public interface INumberExpression : IExpression
    {
        decimal Val { get; }
    }
}
