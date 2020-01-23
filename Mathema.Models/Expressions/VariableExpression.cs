using Mathema.Enums.Operators;
using Mathema.Interfaces;
using Mathema.Models.Dimension;
using Mathema.Models.ExpressionOperations;
using Mathema.Models.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mathema.Models.Expressions
{
	public class VariableExpression : IVariableExpression
	{
		public string Symbol { get; private set; }

		public decimal Val { get; set; }

		public IDimensionKey DimensionKey { get; set; } = new DimensionKey();

		public IComplex Count { get; set; } = new Complex();

		public Dictionary<OperatorTypes, Func<IExpression, IExpression, IExpression>> BinaryOperations { get; } = VariableOperations.BinaryOperations;

		public Dictionary<OperatorTypes, Func<IExpression, IExpression>> UnaryOperations { get; } = VariableOperations.UnaryOperations;

		public VariableExpression(string symbol, decimal count)
		{
			this.Symbol = symbol;
			this.Count = new Complex(count, 0);
			this.DimensionKey.Add(symbol);
		}

		public VariableExpression(string symbol, IComplex count)
		{
			this.Symbol = symbol;
			this.Count = count;
			this.DimensionKey.Add(symbol);
		}

		public IExpression Execute()
		{
			return this;
		}

		public IExpression Clone()
		{
			var res = new VariableExpression(string.Copy(this.Symbol), this.Count.Clone());
			res.DimensionKey = new DimensionKey();
			res.Count = this.Count.Clone();
			foreach (var key in this.DimensionKey.Key)
			{
				res.DimensionKey.Add(string.Copy(key.Key), key.Value);
			}

			return res;
		}

		public bool CompareDimensions(IVariableExpression variable)
		{
			return Dimension.DimensionKey.Compare(this.DimensionKey, variable.DimensionKey);
		}

		public string AsString()
		{
			return this.ToString();
		}

		public override string ToString()
		{
			if (this.Count.Im.Numerator == 0)
			{
				var num = this.Count.Re.ToNumber();
				if (num != 1 && num != -1)
				{
					if (this.Count.Re.Denominator % 1 == 0 && this.Count.Re.Denominator != 1 && this.Count.Re.Numerator % 1 == 0)
					{
						return this.Count.Re.Numerator.ToString() + " / " + this.Count.Re.Denominator + " * " + this.DimensionKey.ToString();
					}

					if (num % 1 == 0)
					{
						return num.ToString() + " * " + this.DimensionKey.ToString();
					}
				}

				return num == -1 ? "-" + this.DimensionKey.ToString() : this.DimensionKey.ToString();
			}
			else if (this.Count.Re.Numerator == 0)
			{
				var num = this.Count.Im.ToNumber();
				if (num != 1 && num != -1)
				{
					if (this.Count.Im.Denominator % 1 == 0 && this.Count.Im.Denominator != 1 && this.Count.Im.Numerator % 1 == 0)
					{
						return this.Count.Im.Numerator.ToString() + " / " + this.Count.Im.Denominator + " * " + this.DimensionKey.ToString();
					}

					if (num % 1 == 0)
					{
						return num.ToString() + " * " + this.DimensionKey.ToString();
					}
				}

				return num == -1 ? "-" + this.DimensionKey.ToString() : this.DimensionKey.ToString();
			}

			return "(" + this.Count.ToString() + ") * " + this.DimensionKey.ToString();
		}
	}
}
