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
        public static IEquationSolutions Solve(Equation equation)
        {
            var equationType = EquationClassifier.Classify(equation);
            if (equation.Type == EquationTypes.Linear)
            {
                var solver = new LinearSolver();
                return solver.Solve(equation.Left);
            }
            else if (equation.Type == EquationTypes.Quadratic)
            {
                var solver = new QuadraticSolver();
                return solver.Solve(equation.Left);
            }
            else
            {
                return null;
            }
        }
    }
}
