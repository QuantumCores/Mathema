using Mathema.Enums.Operators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Interfaces
{
    public interface IOperationDispatcher
    {
        Dictionary<OperatorTypes, Func<IExpression, IExpression, IExpression>> BinaryOperations { get; }

        Dictionary<OperatorTypes, Func<IExpression, IExpression>> UnaryOperations { get; }
    }
}
