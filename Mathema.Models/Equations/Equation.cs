using Mathema.Enums.Equations;
using Mathema.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Models.Equations
{
	public class Equation
	{
		public string Original { get; }

		public IExpression Left { get; }

		public IExpression Right { get; }

		public ClassificationResult Classification { get; set; }

		public Dictionary<string, int> Variables { get; set; } = new Dictionary<string, int>();

		public Equation(string equation, IExpression left, IExpression right, Dictionary<string, int> variables = null)
		{
			this.Left = left;
			this.Right = right;
			this.Variables = variables ?? new Dictionary<string, int>();
		}
	}
}
