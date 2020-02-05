using Mathema.Interfaces;
using Mathema.Models.Equations;
using Mathema.Models.Expressions;
using Mathema.Models.FlatExpressions;
using Mathema.Models.FunctionExpressions;
using Mathema.Models.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mathema.Solver.Solvers
{
    public class TrigonometricSolver : ISolver
    {
        public IEquationSolutions Solve(IExpression expression, string variable, IClassificationResult classification)
        {
            var res = new EquationSolutions();
            List<IExpression> al = new List<IExpression>();
            List<IExpression> bl = new List<IExpression>();
            List<IExpression> cl = new List<IExpression>();


            if (expression is FlatAddExpression fa)
            {
                foreach (var kv in fa.Expressions)
                {
                    var expr = kv.Value[0];

                    if (expr is IFunctionExpression)
                    {
                        if (expr is CosExpression)
                        {
                            al.Add(expr);
                        }
                        else if (expr is SinExpression)
                        {
                            al.Add(expr);
                        }
                        else if (expr is TanExpression)
                        {
                            al.Add(expr);
                        }
                        else if (expr is CotExpression)
                        {
                            al.Add(expr);
                        }
                        else
                        {
                            bl.Add(expr);
                        }
                    }
                    else if (expr is FlatExpression fe)
                    {
                        if (fe.Expressions.Any(e => e.Key == variable))
                        {
                            bl.Add(expr);
                        }
                        else
                        {
                            cl.Add(expr);
                        }
                    }
                    else if (expr.DimensionKey.Key == variable)
                    {
                        bl.Add(expr);
                    }
                    else
                    {
                        cl.Add(expr);
                    }
                }

                if (bl.Count > 0)
                {
                    //General way of solving
                }

                if (al.Count > 1)
                {
                    //General way of solving ?
                }

                if (al.Count == 1)
                {
                    var expr = (IFunctionExpression)al[0];
                    
                    if (expr.InverseFunction != 0)
                    {
                        var arg = new FlatAddExpression();
                        foreach (var e in cl)
                        {
                            arg.Add(new UnaryExpression(Enums.Operators.OperatorTypes.Sign,e).Execute());
                        }
                        arg.Execute();

                        var sol = Functions.Get(expr.InverseFunction.ToString(), arg).Execute();
                        var solFor = expr.Argument.DimensionKey.Key;
                        res.Solutions.Add(solFor, new Tuple<IExpression, List<IExpression>>(expr.Argument, new List<IExpression>() { sol }));
                    }
                    else
                    {
                        //No inverse function => Numeric solution ?
                    }                    
                }
            }

            return res;
        }
    }
}
