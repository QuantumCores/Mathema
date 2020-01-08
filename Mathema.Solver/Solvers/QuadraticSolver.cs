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
    public class QuadraticSolver : ISolver
    {
        public IEquationSolutions Solve(IExpression expression)
        {
            var res = new EquationSolutions();
            if (expression is FlatAddExpression fa)
            {
                var a = (Fraction)fa.Expressions["x * x"][0].Count;
                var b = (Fraction)fa.Expressions["x"][0].Count;
                var c = (Fraction)fa.Expressions["1"][0].Count;

                var delta = b * b - (4 * a * c);
                var nd = delta.ToNumber();

                if (nd < 0)
                {
                    return res;
                }
                else if (nd == 0)
                {
                    res.Solutions.Add(new NumberExpression(-1 * b / 2 * a));
                }
                else
                {
                    var sqrt = Math.Sqrt((double)nd);
                    res.Solutions.Add(new NumberExpression((-1 * b + sqrt) / 2 * a));
                    res.Solutions.Add(new NumberExpression((-1 * b - sqrt) / 2 * a));
                }
            }

            return res;
        }
    }
}
