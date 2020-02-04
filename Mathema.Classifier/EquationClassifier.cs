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

        public ClassificationResult Classify(string variable)
        {
            return EquationClassifier.Classify(this.equation.Left, variable);
        }

        public static Equation Classify(Equation equation, string variable)
        {
            var ec = new EquationClassifier(equation);
            ec.equation.Classification = ec.Classify(variable);

            return ec.equation;
        }


        public static ClassificationResult Classify(IExpression expr, string variable)
        {
            var classification = new ClassificationResult();
            Search(expr, e => e.DimensionKey.Key == variable, classification.SearchResult);
            classification.EquationType = FindType(classification.SearchResult);

            return classification;
        }

        private static bool Search(IExpression expression, Predicate<IExpression> predicate, Dictionary<string, List<IFraction>> result, bool dontAdd = false)
        {
            if (expression is FlatAddExpression fae)
            {
                var isFound = false;
                foreach (var kvel in fae.Expressions)
                {
                    foreach (var e in kvel.Value)
                    {
                        if (Search(e, predicate, result, false) && !dontAdd)
                        {
                            isFound = true;
                        }
                    }
                }

                return isFound;
            }
            else if (expression is FlatMultExpression fme)
            {
                var isFound = false;
                foreach (var kvel in fme.Expressions)
                {
                    foreach (var e in kvel.Value)
                    {
                        if (Search(e, predicate, result, false) && !dontAdd)
                        {
                            isFound = true;
                        }
                    }
                }

                return isFound;
            }
            else if (expression is IFunctionExpression fe)
            {
                if (Search(fe.Argument, predicate, result, true))
                {
                    if (!dontAdd)
                    {
                        var dim = expression.DimensionKey;
                        Add(result, dim.Key, dim.Value);
                    }

                    return true;
                }

                return false;
            }
            else
            {
                if (predicate(expression))
                {
                    if (!dontAdd)
                    {
                        var dim = expression.DimensionKey;
                        Add(result, dim.Key, dim.Value);
                    }
                    return true;
                }

                return false;
            }
        }

        private static void Add(Dictionary<string, List<IFraction>> result, string key, IFraction val)
        {
            if (!result.ContainsKey(key))
            {
                result.Add(key, new List<IFraction>());
            }

            result[key].Add(val);
        }

        private static EquationTypes FindType(Dictionary<string, List<IFraction>> result)
        {
            if (result.Count == 1)
            {
                var dim = result.ElementAt(0);
                var ord = dim.Value.OrderBy(x => x).ToArray();
                if (ord.Length == 1)
                {
                    if (ord[0].ToNumber() == 1)
                    {
                        return EquationTypes.Linear;
                    }
                    else if (ord[0].ToNumber() == 2)
                    {
                        return EquationTypes.Quadratic;
                    }
                }
                else if (ord.Length == 2)
                {
                    if (ord[1].ToNumber() / ord[0].ToNumber() == 2)
                    {
                        return EquationTypes.Quadratic;
                    }
                }
            }
            else
            {

            }

            return EquationTypes.Undefined;
        }
    }
}
