using Mathema.Enums.DimensionKeys;
using Mathema.Enums.Equations;
using Mathema.Interfaces;
using Mathema.Models.Equations;
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

        public EquationTypes Classify()
        {
            return this.Classify(this.equation.Left);
        }

        public static Equation Classify(Equation equation)
        {
            var ec = new EquationClassifier(equation);
            ec.equation.Type = ec.Classify();

            return ec.equation;
        }

        private EquationTypes Classify(IExpression expr)
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
                foreach (var k in keys)
                {
                    if (k.Key != Dimensions.Number)
                    {
                        if (!result.ContainsKey(k.Key))
                        {
                            result.Add(k.Key, new List<decimal>());
                        }

                        result[k.Key].Add(k.Value);
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
