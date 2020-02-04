using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Interfaces
{
    public interface IDimensionKey
    {
        //Dictionary<string, decimal> Key { get; set; }
        string Key { get; set; }

        IFraction Value { get; set; }

        void Set(string dim);

        void Set(string dim, decimal val);

        void Add(IDimensionKey dimensionKey);

        void Multiply(string dim, decimal val);

        IDimensionKey Clone();
    }
}
