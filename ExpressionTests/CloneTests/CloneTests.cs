using Mathema.Enums.Functions;
using Mathema.Enums.Operators;
using Mathema.Models.Dimension;
using Mathema.Models.Expressions;
using Mathema.Models.FunctionExpressions;
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
            Assert.AreEqual(actual.Execute().Count.ToNumber(), expected.Execute().Count.ToNumber());
        }

        [Test]
        public void Clone_VariableExpression()
        {
            //Arrange
            var expression = new VariableExpression("x", 2);
            expression.Count.Numerator = -1;
            expression.DimensionKey.Add("y", 3);

            //Act
            var clone = (VariableExpression)expression.Clone();

            //Assert
            Assert.AreEqual(expression.Count.ToNumber(), clone.Count.ToNumber());
            Assert.AreEqual(expression.Symbol, clone.Symbol);
            Assert.IsFalse(Object.ReferenceEquals(expression.Symbol, clone.Symbol));
            Assert.IsTrue(DimensionKey.Compare(expression.DimensionKey, clone.DimensionKey));
            Assert.AreEqual(expression.Val, clone.Val);
        }

        [Test]
        public void Clone_FunctionsExpression()
        {
            //Arrange
            var expected = new CosExpression(FunctionTypes.Cos, new NumberExpression(0));

            //Act
            var actual = (CosExpression)expected.Clone();

            //Assert
            Assert.IsTrue(!Object.ReferenceEquals(actual, expected));
            Assert.AreEqual(actual.Execute().Count.ToNumber(), expected.Execute().Count.ToNumber());
        }

        [Test]
        public void Clone_NumberExpression()
        {
            //Arrange
            var expression = new NumberExpression(-2);

            //Act
            var clone = (NumberExpression)expression.Clone();

            //Assert
            Assert.AreEqual(expression.Count.ToNumber(), clone.Count.ToNumber());
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
            Assert.AreEqual(actual.Execute().Count.ToNumber(), expected.Execute().Count.ToNumber());
        }
    }
}
