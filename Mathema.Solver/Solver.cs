using Mathema.Classifier;
using Mathema.Enums.Equations;
using Mathema.Interfaces;
using Mathema.Models.Equations;
using Mathema.Models.Expressions;
using Mathema.Models.Extensions;
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
			
			var expression = equation.Left;
			var classification = equation.Classification;
			var clone = expression.Clone();
			var varS = variable;
			if (classification.SearchResult.ElementAt(0).Key != variable)
			{
				varS = GetNewVariableForSub(equation);
				var sub = new VariableExpression("u", 1);
				clone.Substitute(ref clone, sub, classification.SearchResult.ElementAt(0).Key);
			}

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
