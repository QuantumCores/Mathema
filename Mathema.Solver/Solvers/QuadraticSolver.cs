using Mathema.Classifier;
using Mathema.Interfaces;
using Mathema.Models.Equations;
using Mathema.Models.Expressions;
using Mathema.Models.FlatExpressions;
using Mathema.Models.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
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
                Fraction a = null;
                Fraction b = null;
                Fraction c = null;

                IExpression ea = null;
                IExpression eb = null;
                IExpression ec = null;

                for (int i = 0; i < fa.Expressions.Count; i++)
                {
                    var kv = fa.Expressions.ElementAt(i);
                    var deg = kv.Value[0].DimensionKey.Key.ElementAt(0).Value;

                    if (deg == 2)
                    {
                        a = (Fraction)kv.Value[0].Count.Re;
                        ea = kv.Value[0];
                    }
                    else if (deg == 1)
                    {
                        if (kv.Value[0] is NumberExpression)
                        {
                            c = (Fraction)kv.Value[0].Count.Re;
                            ec = kv.Value[0];
                        }
                        else
                        {
                            b = (Fraction)kv.Value[0].Count.Re;
                            eb = kv.Value[0];
                        }
                    }
                }

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
                    res.Solutions.Add(new NumberExpression((-b + sqrt) / 2 * a));
                    res.Solutions.Add(new NumberExpression((-b - sqrt) / 2 * a));
                }

                if(!(eb is VariableExpression))
                {
                    var es = new EquationSolutions();
                    var type = EquationClassifier.Classify(eb);
                    foreach (var s in res.Solutions)
                    {
                        var tmp = new FlatAddExpression();
                        var se = new NumberExpression(-(Fraction)s.Count);
                        tmp.Add(eb);
                        tmp.Add(se);
                        var exec = tmp.Execute();
                        var solutions = Solver.Solve(type, exec);

                        es.Solutions.AddRange(solutions.Solutions);
                    }

                    return es;
                }
            }

            return res;
        }
    }
}
