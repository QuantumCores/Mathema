using Mathema.Enums.Operators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Interfaces
{
    public interface IExpressionOperations
    {
        Dictionary<OperatorTypes, Func<IExpression, IExpression>> BinaryOperations { get; }

        Dictionary<OperatorTypes, Func<IExpression>> UnaryOperations { get; }

        IExpression Add(IExpression rhe);

        IExpression Subtract(IExpression rhe);

        IExpression Multiply(IExpression rhe);

        IExpression Divide(IExpression rhe);

        IExpression Pow(IExpression rhe);

        IExpression Sign();
    }
}
