﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Interfaces
{
    public interface IComplexExpression : IExpression, IComplex
    {
        IComplexExpression Conjugation();
    }
}
