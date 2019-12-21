using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Interfaces
{
    public interface IExpressionResult
    {
        Dictionary<string, decimal> Dimensions { get; }
    }
}
