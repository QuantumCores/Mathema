using Mathema.Enums.DimensionKeys;
using Mathema.Enums.Equations;
using Mathema.Interfaces;
using Mathema.Models.Equations;
using Mathema.Models.Expressions;
using Mathema.Models.FlatExpressions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mathema.Classifier
{
    public class EquationClassifier
    {
        private Equation equation;

        public EquationClassifier(Equation equation)
        {
            this.equation = equation;
        }

        public EquationTypes Classify(string variable)
        {
            return EquationClassifier.Classify(this.equation.Left, variable);
        }

        public static Equation Classify(Equation equation, string variable)
        {
            var ec = new EquationClassifier(equation);
            ec.equation.Type = ec.Classify(variable);

            return ec.equation;
        }

        public static EquationTypes Classify(IExpression expr, string variable)
        {
            if (expr is FlatMultExpression fm)
            {
                var ord = fm.Expressions.SelectMany(x => x.Value).OrderBy(x => x.DimensionKey.Key.ElementAt(0).Value);
            }
            else if (expr is FlatAddExpression fa)
            {
                var all = fa.Expressions.SelectMany(x => x.Value).ToList();
                var keys = all.Select(x => x.DimensionKey.Key.ElementAt(0));
                var result = new Dictionary<string, List<decimal>>();

                foreach (var e in fa.Expressions)
                {
                    if (e.Value[0] is FlatExpression flat)
                    {
                        var t = flat.Expressions.Where(x => x.Key == variable).FirstOrDefault().Value[0];
                        if (t != null)
                        {
                            if (!result.ContainsKey(variable))
                            {
                                result.Add(variable, new List<decimal>());
                            }

                            result[variable].Add(t.DimensionKey.Key.ElementAt(0).Value);
                        }
                    }
                    else
                    {
                        var k = e.Value[0].DimensionKey.Key.ElementAt(0).Key;
                        if (k == variable)
                        {
                            if (!result.ContainsKey(variable))
                            {
                                result.Add(variable, new List<decimal>());
                            }

                            result[variable].Add(e.Value[0].DimensionKey.Key.ElementAt(0).Value);
                        }
                    }
                }

                if (result.Count == 1)
                {
                    var dim = result.ElementAt(0);
                    var ord = dim.Value.OrderBy(x => x).ToArray();
                    if (ord.Length == 1)
                    {
                        if (ord[0] == 1)
                        {
                            return EquationTypes.Linear;
                        }
                        else if (ord[0] == 2)
                        {
                            return EquationTypes.Quadratic;
                        }
                    }
                    else if (ord.Length == 2)
                    {
                        if (ord[1] / ord[0] == 2)
                        {
                            return EquationTypes.Quadratic;
                        }
                    }
                }
                else
                {

                }
            }

            return EquationTypes.Undefined;
        }
    }
}
