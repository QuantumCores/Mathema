using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Interfaces
{
    public interface IComplex
    {
        IFraction Re { get; set; }

        IFraction Im { get; set; }
    }
}
