using Mathema.Classifier;
using Mathema.Enums.Equations;
using Mathema.Interfaces;
using Mathema.Models.Equations;
using Mathema.Models.Expressions;
using Mathema.Models.Extensions;
using Mathema.Models.FlatExpressions;
using Mathema.Solver.Solvers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mathema.Solver
{
    public class Solver
    {
        public static IEquationSolutions Solve(Equation equation, string variable)
        {
            EquationClassifier.Classify(equation, variable);
            var sols = Solve(variable, equation.Left, equation.Classification);

            var newSols = new EquationSolutions();
            RecursiveSolve(variable, sols, newSols);

            return newSols;
        }

        private static void RecursiveSolve(string variable, IEquationSolutions sols, IEquationSolutions newSols)
        {
            var keys = sols.Solutions.Keys.ToList();
            foreach (var k in keys)
            {
                if (k != variable)
                {
                    var s = sols.Solutions[k];
                    foreach (var e in s.Item2)
                    {
                        var tmpEq = new FlatAddExpression();
                        tmpEq.Add(s.Item1);
                        tmpEq.Add(new UnaryExpression(Enums.Operators.OperatorTypes.Sign, e));
                        var emp = tmpEq.Execute();

                        var c = EquationClassifier.Classify(emp, variable);
                        var tmp = Solve(variable, tmpEq, c);
                        foreach (var ns in tmp.Solutions)
                        {
                            RecursiveSolve(variable, tmp, newSols);
                        }
                    }
                }
                else
                {
                    var s = sols.Solutions[k];
                    if (!newSols.Solutions.ContainsKey(variable))
                    {
                        newSols.Solutions.Add(variable, new Tuple<IExpression, List<IExpression>>(s.Item1, new List<IExpression>()));
                    }

                    newSols.Solutions[variable].Item2.AddRange(s.Item2);
                }
            }
        }

        private static IEquationSolutions Solve(string variable, IExpression expression, ClassificationResult classification)
        {
            if (classification.EquationType == EquationTypes.Undefined)
            {
                return new EquationSolutions();
            }

            if (classification.SearchResult.ElementAt(0).Key != variable)
            {
                //var expression = equation.Left;
                //var classification = equation.Classification;
                //var clone = expression.Clone();
                //substitute = new Tuple<string, string>(variable, GetNewVariableForSub(equation));
                //var sub = new VariableExpression(substitute.Item2, 1);
                //clone.Substitute(ref clone, sub, classification.SearchResult.ElementAt(0).Key);

                var sols = Solve(classification.SearchResult.ElementAt(0).Key, expression, classification);
                return sols;

            }
            else
            {
                if (classification.EquationType == EquationTypes.Linear)
                {
                    var solver = new LinearSolver();
                    return solver.Solve(expression, variable, classification);
                }
                else if (classification.EquationType == EquationTypes.Quadratic)
                {
                    var solver = new QuadraticSolver();
                    return solver.Solve(expression, variable, classification);
                }
                else if (classification.EquationType == EquationTypes.Trigonometric)
                {
                    var solver = new TrigonometricSolver();
                    return solver.Solve(expression, variable, classification);
                }
                else
                {
                    return null;
                }
            }
        }

        private static string GetNewVariableForSub(Equation equation)
        {
            var goodVars = new List<string>() { "k", "l", "m", "u", "v", "w", "r", "s", "t" };

            foreach (var v in goodVars)
            {
                if (!equation.Variables.ContainsKey(v))
                {
                    return v;
                }
            }

            return "dem";
        }
    }
}
