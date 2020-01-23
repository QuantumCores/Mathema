using Mathema.Enums.Functions;
using Mathema.Enums.Operators;
using Mathema.Models.Dimension;
using Mathema.Models.Expressions;
using Mathema.Models.FlatExpressions;
using Mathema.Models.FunctionExpressions;
using Mathema.Models.Numerics;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionTests.CloneTests
{
	[TestFixture]
	public class CloneTests
	{
		[Test]
		public void Clone_NumberExpression()
		{
			//Arrange
			var expression = new NumberExpression(-2);

			//Act
			var clone = (NumberExpression)expression.Clone();

			//Assert
			Assert.AreEqual(expression.Count.Re.ToNumber(), clone.Count.Re.ToNumber());
		}

		[Test]
		public void Clone_VariableExpression()
		{
			//Arrange
			var expression = new VariableExpression("x", 2);
			expression.Count = new Complex(3, 2);
			expression.DimensionKey.Add("y", 3);

			//Act
			var clone = (VariableExpression)expression.Clone();

			//Assert
			Assert.AreEqual(expression.Symbol, clone.Symbol);
			Assert.IsFalse(Object.ReferenceEquals(expression.Symbol, clone.Symbol));
			Assert.IsTrue(DimensionKey.Compare(expression.DimensionKey, clone.DimensionKey));
			Assert.AreEqual(expression.Count.Re.Numerator, clone.Count.Re.Numerator);
			Assert.AreEqual(expression.Count.Re.Denominator, clone.Count.Re.Denominator);
			Assert.AreEqual(expression.Count.Im.Numerator, clone.Count.Im.Numerator);
			Assert.AreEqual(expression.Count.Im.Denominator, clone.Count.Im.Denominator);
		}

		[Test]
		public void Clone_CosExpression()
		{
			//Arrange
			var expected = new CosExpression(new NumberExpression(0), new Complex(3, 2));

			//Act
			var actual = (CosExpression)expected.Clone();

			//Assert
			Assert.IsTrue(!Object.ReferenceEquals(actual, expected));
			Assert.IsTrue(!Object.ReferenceEquals(actual.Argument, expected.Argument));
			Assert.AreEqual(expected.Count.Re.Numerator, actual.Count.Re.Numerator);
			Assert.AreEqual(expected.Count.Re.Denominator, actual.Count.Re.Denominator);
			Assert.AreEqual(expected.Count.Im.Numerator, actual.Count.Im.Numerator);
			Assert.AreEqual(expected.Count.Im.Denominator, actual.Count.Im.Denominator);
		}

		[Test]
		public void Clone_SinExpression()
		{
			//Arrange
			var expected = new SinExpression(new NumberExpression(0), new Complex(3, 2));

			//Act
			var actual = (SinExpression)expected.Clone();

			//Assert
			Assert.IsTrue(!Object.ReferenceEquals(actual, expected));
			Assert.IsTrue(!Object.ReferenceEquals(actual.Argument, expected.Argument));
			Assert.AreEqual(expected.Count.Re.Numerator, actual.Count.Re.Numerator);
			Assert.AreEqual(expected.Count.Re.Denominator, actual.Count.Re.Denominator);
			Assert.AreEqual(expected.Count.Im.Numerator, actual.Count.Im.Numerator);
			Assert.AreEqual(expected.Count.Im.Denominator, actual.Count.Im.Denominator);
		}

		[Test]
		public void Clone_CotExpression()
		{
			//Arrange
			var expected = new CosExpression(new NumberExpression(0), new Complex(3, 2));

			//Act
			var actual = (CosExpression)expected.Clone();

			//Assert
			Assert.IsTrue(!Object.ReferenceEquals(actual, expected));
			Assert.IsTrue(!Object.ReferenceEquals(actual.Argument, expected.Argument));
			Assert.AreEqual(expected.Count.Re.Numerator, actual.Count.Re.Numerator);
			Assert.AreEqual(expected.Count.Re.Denominator, actual.Count.Re.Denominator);
			Assert.AreEqual(expected.Count.Im.Numerator, actual.Count.Im.Numerator);
			Assert.AreEqual(expected.Count.Im.Denominator, actual.Count.Im.Denominator);
		}

		[Test]
		public void Clone_TanExpression()
		{
			//Arrange
			var expected = new TanExpression(new NumberExpression(0), new Complex(3, 2));

			//Act
			var actual = (TanExpression)expected.Clone();

			//Assert
			Assert.IsTrue(!Object.ReferenceEquals(actual, expected));
			Assert.IsTrue(!Object.ReferenceEquals(actual.Argument, expected.Argument));
			Assert.AreEqual(expected.Count.Re.Numerator, actual.Count.Re.Numerator);
			Assert.AreEqual(expected.Count.Re.Denominator, actual.Count.Re.Denominator);
			Assert.AreEqual(expected.Count.Im.Numerator, actual.Count.Im.Numerator);
			Assert.AreEqual(expected.Count.Im.Denominator, actual.Count.Im.Denominator);
		}

		[Test]
		public void Clone_LogExpression()
		{
			//Arrange
			var expected = new LogExpression(new NumberExpression(0), new Complex(3, 2));

			//Act
			var actual = (LogExpression)expected.Clone();

			//Assert
			Assert.IsTrue(!Object.ReferenceEquals(actual, expected));
			Assert.IsTrue(!Object.ReferenceEquals(actual.Argument, expected.Argument));
			Assert.AreEqual(expected.Count.Re.Numerator, actual.Count.Re.Numerator);
			Assert.AreEqual(expected.Count.Re.Denominator, actual.Count.Re.Denominator);
			Assert.AreEqual(expected.Count.Im.Numerator, actual.Count.Im.Numerator);
			Assert.AreEqual(expected.Count.Im.Denominator, actual.Count.Im.Denominator);
		}

		[Test]
		public void Clone_FlatAddExpression()
		{
			//Arrange
			var expected = new FlatAddExpression();
			expected.Add(new VariableExpression("x", 1));
			expected.Add(new VariableExpression("y", 1));
			expected.Count = new Complex(3, 2);

			//Act
			var actual = (FlatAddExpression)expected.Clone();

			//Assert
			Assert.IsTrue(!Object.ReferenceEquals(actual, expected));
			CollectionAssert.AreEquivalent(expected.Expressions, actual.Expressions);
			Assert.AreEqual(expected.Count.Re.Numerator, actual.Count.Re.Numerator);
			Assert.AreEqual(expected.Count.Re.Denominator, actual.Count.Re.Denominator);
			Assert.AreEqual(expected.Count.Im.Numerator, actual.Count.Im.Numerator);
			Assert.AreEqual(expected.Count.Im.Denominator, actual.Count.Im.Denominator);
		}

		[Test]
		public void Clone_FlatMultExpression()
		{
			//Arrange
			var expected = new FlatMultExpression();
			expected.Add(new VariableExpression("x", 1));
			expected.Add(new VariableExpression("y", 1));
			expected.Count = new Complex(3, 2);

			//Act
			var actual = (FlatMultExpression)expected.Clone();

			//Assert
			Assert.IsTrue(!Object.ReferenceEquals(actual, expected));
			CollectionAssert.AreEquivalent(expected.Expressions, actual.Expressions);
			Assert.AreEqual(expected.Count.Re.Numerator, actual.Count.Re.Numerator);
			Assert.AreEqual(expected.Count.Re.Denominator, actual.Count.Re.Denominator);
			Assert.AreEqual(expected.Count.Im.Numerator, actual.Count.Im.Numerator);
			Assert.AreEqual(expected.Count.Im.Denominator, actual.Count.Im.Denominator);
		}


		[Test]
		public void Clone_UnaryExpression()
		{
			//Arrange
			var expected = new UnaryExpression(OperatorTypes.Sign, new NumberExpression(0));

			//Act
			var actual = (UnaryExpression)expected.Clone();

			//Assert
			Assert.IsTrue(!Object.ReferenceEquals(actual, expected));
			Assert.AreEqual(actual.Execute().Count.Re.ToNumber(), expected.Execute().Count.Re.ToNumber());
		}

		[Test]
		public void Clone_BinaryExpression()
		{
			//Arrange
			var expr1 = new NumberExpression(-2);
			var expr2 = new NumberExpression(3);
			var actual = new BinaryExpression(expr1, OperatorTypes.Power, expr2);

			//Act
			var expected = (BinaryExpression)actual.Clone();

			//Assert
			Assert.IsTrue(!Object.ReferenceEquals(actual, expected));
			Assert.AreEqual(actual.Execute().Count.Re.ToNumber(), expected.Execute().Count.Re.ToNumber());
		}
	}
}
