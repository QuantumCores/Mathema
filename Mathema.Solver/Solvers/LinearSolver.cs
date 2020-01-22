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
    public class LinearSolver : ISolver
    {
        public IEquationSolutions Solve(IExpression expression, string variable, IClassificationResult classification)
        {
            var res = new EquationSolutions();
            IExpression a = null;
            IExpression b = null;
            List<IExpression> ba = new List<IExpression>();

            if (expression is FlatAddExpression fa)
            {
                foreach (var expr in fa.Expressions)
                {
                    if (expr.Value[0] is FlatExpression fe)
                    {
                        if (fe.Expressions.Any(e => e.Key == variable))
                        {
                            if (expr.Value[0] is VariableExpression)
                            {
                                a = new ComplexExpression(expr.Value[0].Count);
                            }
                            else
                            {
                                var tmp = (FlatExpression)expr.Value[0].Clone();
                                tmp.Remove(variable, 1);
                                a = tmp;
                            }
                        }
                    }
                    else if (expr.Key == variable)
                    {
                        if (expr.Value[0] is VariableExpression)
                        {
                            a = new ComplexExpression(expr.Value[0].Count);
                        }
                        else
                        {
                            var tmp = (FlatExpression)expr.Value[0].Clone();
                            tmp.Remove(variable, 1);
                            a = tmp;
                        }
                    }
                    else
                    {
                        ba.Add(expr.Value[0]);
                    }
                }

                if (ba.Count > 1)
                {
                    b = new FlatAddExpression();
                    var ct = (FlatAddExpression)b;
                    foreach (var ci in ba)
                    {
                        ct.Add(ci);
                    }
                }
                else if (ba.Count == 1)
                {
                    b = ba[0];
                }

                var sol = new BinaryExpression(new UnaryExpression(Enums.Operators.OperatorTypes.Sign, b), Enums.Operators.OperatorTypes.Divide, a).Execute();

                res.Solutions.Add(sol);
            }

            return res;
        }
    }
}
