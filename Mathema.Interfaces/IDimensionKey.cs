using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Interfaces
{
    public interface IDimensionKey
    {
        Dictionary<string, decimal> Key { get; set; }

        void Add(string dim);

        void Add(string dim, decimal val);
        
        void Remove(string dim);

        void Remove(string dim, decimal val);

        void Multiply(string dim, decimal val);
    }
}
