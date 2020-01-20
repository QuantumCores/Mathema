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
            return Solve(equationType.Type, equation.Left, "x");

        }

        public static IEquationSolutions Solve(EquationTypes type, IExpression expression, string variable)
        {
            if (type == EquationTypes.Linear)
            {
                var solver = new LinearSolver();
                return solver.Solve(expression, variable);
            }
            else if (type == EquationTypes.Quadratic)
            {
                var solver = new QuadraticSolver();
                return solver.Solve(expression, variable);
            }
            else
            {
                return null;
            }
        }
    }
}
