using System;

namespace Mathema.Interfaces
{
    public interface IExpression
    {
        string DimensionType { get; }

        IExpression Value();
    }
}
