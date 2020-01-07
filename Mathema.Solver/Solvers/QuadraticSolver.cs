using Mathema.Interfaces;
using Mathema.Models.Equations;
using Mathema.Models.Expressions;
using Mathema.Models.FlatExpressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Solver.Solvers
{
    public class QuadraticSolver : ISolver
    {
        public IEquationSolutions Solve(IExpression expression)
        {
            var res = new EquationSolutions();
            if (expression is FlatAddExpression fa)
            {
                var a = fa.Expressions["x * x"][0].Count;
                var b = fa.Expressions["x"][0].Count;
                var c = fa.Expressions["1"][0].Count;

                var delta = b * b - 4 * a * c;

                if (delta < 0)
                {
                    return res;
                }
                else if (delta == 0)
                {
                    res.Solutions.Add(new NumberExpression(-b / 2 * a));
                }
                else
                {
                    var sqrt = delta.Sqrt();
                    res.Solutions.Add(new NumberExpression((-b - sqrt) / 2 * a));
                    res.Solutions.Add(new NumberExpression((-b + sqrt) / 2 * a));
                }

            }
        }
    }
}
