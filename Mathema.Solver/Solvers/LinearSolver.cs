using Mathema.Interfaces;
using Mathema.Models.Equations;
using Mathema.Models.Expressions;
using Mathema.Models.FlatExpressions;
using Mathema.Models.Numerics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Solver.Solvers
{
    public class LinearSolver : ISolver
    {
        public IEquationSolutions Solve(IExpression expression)
        {
            var res = new EquationSolutions();
            if (expression is FlatAddExpression fa)
            {
                var a = (Fraction)fa.Expressions["x"][0].Count;
                var b = (Fraction)fa.Expressions["1"][0].Count;

                res.Solutions.Add(new NumberExpression(-b / a));
            }

            return res;
        }
    }
}
