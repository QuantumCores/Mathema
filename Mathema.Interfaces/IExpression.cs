using System;

namespace Mathema.Interfaces
{
    public interface IExpression : IOperationDispatcher
    {
        IFraction Count { get; set; }

        IDimensionKey DimensionKey { get; set; }

        IExpression Execute();
    }
}
