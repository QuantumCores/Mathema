using Mathema.Enums.Operators;
using Mathema.Models.Expressions;
using Mathema.Models.FlatExpressions;
using Mathema.Models.FunctionExpressions;
using Mathema.Models.Numerics;
using NUnit.Framework;

namespace ExpressionTests.NumberExpressionTests
{
	[TestFixture]
	public class MultiplyTests
	{
		[Test]
		public void Multiply_Number_Number()
		{
			//Arrange
			var expected = new NumberExpression(4);
			var c = new NumberExpression(2);
			var n = new NumberExpression(2);
			var expr = new BinaryExpression(c, OperatorTypes.Multiply, n);

			//Act
			var actual = expr.Execute();

			//Assert
			Assert.AreEqual(expected.Count.Re.ToNumber(), actual.Count.Re.ToNumber());
			Assert.AreEqual(expected.Count.Im.ToNumber(), actual.Count.Im.ToNumber());
		}

		[Test]
		public void Multiply_Number_NegativeNumber()
		{
			//Arrange
			var expected = new NumberExpression(-4);
			var c = new NumberExpression(-2);
			var n = new NumberExpression(2);
			var expr = new BinaryExpression(c, OperatorTypes.Multiply, n);

			//Act
			var actual = expr.Execute();

			//Assert
			Assert.AreEqual(expected.Count.Re.ToNumber(), actual.Count.Re.ToNumber());
			Assert.AreEqual(expected.Count.Im.ToNumber(), actual.Count.Im.ToNumber());
		}

		[Test]
		public void Multiply_Number_Complex()
		{
			//Arrange
			var expected = new ComplexExpression(2, -6);
			var c = new NumberExpression(2);
			var n = new ComplexExpression(1, -3);
			var expr = new BinaryExpression(c, OperatorTypes.Multiply, n);

			//Act
			var actual = (ComplexExpression)expr.Execute();

			//Assert
			Assert.AreEqual(expected.Count.Re.ToNumber(), actual.Count.Re.ToNumber());
			Assert.AreEqual(expected.Count.Im.ToNumber(), actual.Count.Im.ToNumber());
		}


		[Test]
		public void Multiply_Number_Variable()
		{
			//Arrange
			var expected = new VariableExpression("x", 12);
			var c = new NumberExpression(3);
			var n = new VariableExpression("x", 4);
			var expr = new BinaryExpression(c, OperatorTypes.Multiply, n);

			//Act
			var actual = expr.Execute();

			//Assert
			Assert.AreEqual(expected.Count.Re.ToNumber(), actual.Count.Re.ToNumber());
			Assert.AreEqual(expected.Count.Im.ToNumber(), actual.Count.Im.ToNumber());
		}

		[Test]
		public void Multiply_Number_Function()
		{
			//Arrange
			var expected = new CotExpression(new VariableExpression("x", 1), 12);
			var c = new NumberExpression(3);
			var n = new CotExpression(new VariableExpression("x", 1), 4);
			var expr = new BinaryExpression(c, OperatorTypes.Multiply, n);

			//Act
			var actual = expr.Execute();

			//Assert
			Assert.AreEqual(expected.Count.Re.ToNumber(), actual.Count.Re.ToNumber());
			Assert.AreEqual(expected.Count.Im.ToNumber(), actual.Count.Im.ToNumber());
		}

		[Test]
		public void Multiply_Number_FlatAdd()
		{
			//Arrange
			var expected = new FlatAddExpression();
			expected.Add(new VariableExpression("u", 2));
			expected.Add(new NumberExpression(3));
			expected.Count = new Complex(12, 0);
			expected.Execute();

			var c = new NumberExpression(3);
			var n = new FlatAddExpression();
			n.Add(new VariableExpression("u", 2));
			n.Add(new NumberExpression(3));
			n.Count = new Complex(4, 0);
			var expr = new BinaryExpression(c, OperatorTypes.Multiply, n);

			//Act
			var actual = expr.Execute();

			//Assert
			Assert.AreEqual(expected.Count.Re.ToNumber(), actual.Count.Re.ToNumber());
			Assert.AreEqual(expected.Count.Im.ToNumber(), actual.Count.Im.ToNumber());
		}

		[Test]
		public void Multiply_Number_FlatMult()
		{
			//Arrange
			var expected = new FlatMultExpression();
			expected.Add(new VariableExpression("u", 2));
			expected.Add(new VariableExpression("y", 3));
			expected.Count = new Complex(12, 0);
			expected.Execute();

			var c = new NumberExpression(3);
			var n = new FlatMultExpression();
			n.Add(new VariableExpression("u", 2));
			n.Add(new VariableExpression("y", 3));
			n.Count = new Complex(4, 0);
			var expr = new BinaryExpression(c, OperatorTypes.Multiply, n);

			//Act
			var actual = expr.Execute();

			//Assert
			Assert.AreEqual(expected.Count.Re.ToNumber(), actual.Count.Re.ToNumber());
			Assert.AreEqual(expected.Count.Im.ToNumber(), actual.Count.Im.ToNumber());
		}
	}
}
