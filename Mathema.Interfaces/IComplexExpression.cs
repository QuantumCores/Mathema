using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Interfaces
{
    public interface IComplexExpression : IExpression
    {
        IFraction Re { get; set; }

        IFraction Im { get; set; }

        IComplexExpression Conjugation();
    }
}
