using System;

namespace Mathema.Interfaces
{
    public interface IExpression
    {
        string DimensionKey { get; }

        IExpression Value();
    }
}
