using Mathema.Enums.Operators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Interfaces
{
    public interface IExpressionOperations<T>
    {
        Dictionary<OperatorTypes, Func<T, IExpression, IExpression>> BinaryOperations { get; }

        Dictionary<OperatorTypes, Func<T, IExpression>> UnaryOperations { get; }

        IExpression Add(T lhe, IExpression rhe);

        IExpression Subtract(T lhe, IExpression rhe);

        IExpression Multiply(T lhe, IExpression rhe);

        IExpression Divide(T lhe, IExpression rhe);

        IExpression Pow(T lhe, IExpression rhe);

        IExpression Sign(T target);
    }
}
