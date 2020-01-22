using Mathema.Classifier;
using Mathema.Enums.Equations;
using Mathema.Interfaces;
using Mathema.Models.Equations;
using Mathema.Solver.Solvers;
using System;
using System.Collections.Generic;

namespace Mathema.Solver
{
    public class Solver
    {
        public static IEquationSolutions Solve(Equation equation, string variable)
        {
            EquationClassifier.Classify(equation, variable);
            return Solve(equation.Classification, equation.Left, variable);
        }

        public static IEquationSolutions Solve(ClassificationResult classification, IExpression expression, string variable)
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
            else
            {
                return null;
            }
        }
    }
}
