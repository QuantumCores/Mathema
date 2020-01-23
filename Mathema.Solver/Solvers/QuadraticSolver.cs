using Mathema.Classifier;
using Mathema.Interfaces;
using Mathema.Models.Equations;
using Mathema.Models.Expressions;
using Mathema.Models.FlatExpressions;
using Mathema.Models.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mathema.Solver.Solvers
{
	public class QuadraticSolver : ISolver
	{
		public IEquationSolutions Solve(IExpression expression, string variable, IClassificationResult classification)
		{
			var res = new EquationSolutions();
			if (clone is FlatAddExpression fa)
			{
				IExpression a = null;
				IExpression b = null;
				IExpression c = null;
				List<IExpression> ca = new List<IExpression>();

				for (int i = 0; i < fa.Expressions.Count; i++)
				{
					var kv = fa.Expressions.ElementAt(i);
					var deg = kv.Value[0].DimensionKey.Key.ElementAt(0).Value;
					var key = kv.Value[0].DimensionKey.Key.ElementAt(0).Key;
					var expr = kv.Value[0];

					if (deg == 2 && key == varS)
					{
						if (expr is VariableExpression)
						{
							a = new ComplexExpression(kv.Value[0].Count);
						}
						else
						{
							var tmp = (FlatExpression)expr.Clone();
							tmp.Remove(varS, 2);
							a = tmp;
						}
					}
					else if (deg == 1 && key == varS)
					{
						if (expr is VariableExpression)
						{
							b = new ComplexExpression(kv.Value[0].Count);
						}
						else
						{
							var tmp = (FlatExpression)expr.Clone();
							tmp.Remove(varS, 2);
							b = tmp;
						}
					}
					else
					{
						ca.Add(kv.Value[0]);
					}
				}

				if (ca.Count > 1)
				{
					c = new FlatAddExpression();
					var ct = (FlatAddExpression)c;
					foreach (var ci in ca)
					{
						ct.Add(ci);
					}
				}
				else if (ca.Count == 1)
				{
					c = ca[0];
				}

				if (a != null && c != null)
				{
					var delta = this.Delta(a, b, c);
					var ds = new BinaryExpression(delta, Enums.Operators.OperatorTypes.Power, new NumberExpression(new Fraction(1, 2)));

					var dsqrt = ds.Execute();

					var r1 = X1X2(b, dsqrt, a, true);
					var r2 = X1X2(b, dsqrt, a, false);

					res.Solutions.Add(r1.Execute());
					res.Solutions.Add(r2.Execute());
				}
			}

			return res;
		}

		private IExpression Delta(IExpression a, IExpression b, IExpression c)
		{
			var b2 = new BinaryExpression(b, Enums.Operators.OperatorTypes.Power, new NumberExpression(2));
			var ac4 = new FlatMultExpression();
			ac4.Add(new NumberExpression(-4));
			ac4.Add(a);
			ac4.Add(c);

			var d2 = new FlatAddExpression();
			d2.Add(b2);
			d2.Add(ac4);

			return d2;
		}

		private IExpression X1X2(IExpression b, IExpression dsqrt, IExpression a, bool isAdd)
		{
			var oneOvera2 = new BinaryExpression(new BinaryExpression(new NumberExpression(2), Enums.Operators.OperatorTypes.Multiply, a), Enums.Operators.OperatorTypes.Power, new NumberExpression(-1));
			if (isAdd)
			{
				var bds = new FlatAddExpression();
				bds.Add(new UnaryExpression(Enums.Operators.OperatorTypes.Sign, b));
				bds.Add(dsqrt);
				return new BinaryExpression(bds, Enums.Operators.OperatorTypes.Multiply, oneOvera2);
			}
			else
			{
				var bds = new FlatAddExpression();
				bds.Add(new UnaryExpression(Enums.Operators.OperatorTypes.Sign, b));
				bds.Add(new UnaryExpression(Enums.Operators.OperatorTypes.Sign, dsqrt));
				return new BinaryExpression(bds, Enums.Operators.OperatorTypes.Multiply, oneOvera2);
			}
		}
	}
}
