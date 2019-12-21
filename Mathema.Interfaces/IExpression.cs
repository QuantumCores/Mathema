using System;

namespace Mathema.Interfaces
{
    public interface IExpression
    {
        IFraction Count { get; set; }

        string DimensionKey { get; set; }

        IExpression Value();
    }
}
