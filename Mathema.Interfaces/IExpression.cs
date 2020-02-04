using System;

namespace Mathema.Interfaces
{
    public interface IExpression : IOperationDispatcher
    {
        IComplex Count { get; set; }

        IDimensionKey DimensionKey { get; set; }

        IExpression Execute();

        IExpression Clone();

        void UpdateDimensionKey(bool deep);

        string AsString();
    }
}
