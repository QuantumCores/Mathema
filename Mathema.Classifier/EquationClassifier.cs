using Mathema.Enums.DimensionKeys;
using Mathema.Enums.Equations;
using Mathema.Interfaces;
using Mathema.Models.Equations;
using Mathema.Models.Expressions;
using Mathema.Models.FlatExpressions;
using Mathema.Models.FunctionExpressions;
using Mathema.Models.Numerics;
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

        private static bool Search(IExpression expression, Predicate<IExpression> predicate, Dictionary<string, Tuple<IExpression, List<IFraction>>> result, bool dontAdd = false)
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
                        Add(result, expression);
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
                        Add(result, expression);
                    }
                    return true;
                }

                return false;
            }
        }

        private static void Add(Dictionary<string, Tuple<IExpression, List<IFraction>>> result, IExpression expression)
        {
            var key = expression.DimensionKey.Key;
            if (!result.ContainsKey(key))
            {
                var clone = expression.Clone();
                clone.Count = new Complex();
                result.Add(key, new Tuple<IExpression, List<IFraction>>(clone, new List<IFraction>()));
            }

            result[key].Item2.Add(expression.DimensionKey.Value.Clone());
        }

        private static EquationTypes FindType(Dictionary<string, Tuple<IExpression, List<IFraction>>> result)
        {
            if (result.Count == 1)
            {
                var expr = result.ElementAt(0).Value.Item1;
                var ord = result.ElementAt(0).Value.Item2.OrderBy(x => x.ToNumber()).ToArray();
                if (ord.Length == 1)
                {
                    if (ord[0].ToNumber() == 1)
                    {
                        if (expr is IFunctionExpression)
                        {
                            if (expr is CosExpression || expr is SinExpression || expr is TanExpression || expr is CotExpression)
                            {
                                return EquationTypes.Trigonometric;
                            }
                        }
                        else if (expr is VariableExpression)
                        {
                            return EquationTypes.Linear;
                        }
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
