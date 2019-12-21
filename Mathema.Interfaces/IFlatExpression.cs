using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Interfaces
{
    public interface IFlatExpression : IExpression
    {
        Dictionary<string, List<IExpression>> Dimensions { get; }

        void Add(IExpression expression);
    }
}
