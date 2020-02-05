using Mathema.Enums.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Interfaces
{
    public interface IFunctionExpression : IExpression
    {
        FunctionTypes Type { get; }

        IExpression Argument { get; set; }

        FunctionTypes InverseFunction { get; }
    }
}
