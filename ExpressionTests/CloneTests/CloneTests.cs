using Mathema.Models.Dimension;
using Mathema.Models.Expressions;
using Mathema.Models.FlatExpressions;
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
            var expression = new VariableExpression("x", 2);

            //Act
            var clone = (VariableExpression)expression.Clone();

            //Assert
            Assert.AreEqual(1, 2);
        }

        [Test]
        public void Clone_VariableExpression()
        {
            //Arrange
            var expression = new VariableExpression("x", 2);
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
            var expression = new VariableExpression("x", 2);

            //Act
            var clone = (VariableExpression)expression.Clone();

            //Assert
            Assert.AreEqual(1, 2);
        }

        [Test]
        public void Clone_NumberExpression()
        {
            //Arrange
            var expression = new VariableExpression("x", 2);

            //Act
            var clone = (VariableExpression)expression.Clone();

            //Assert
            Assert.AreEqual(expression.Count.ToNumber(), clone.Count.ToNumber());
        }

        [Test]
        public void Clone_UnaryExpression()
        {
            //Arrange
            var expression = new VariableExpression("x", 2);

            //Act
            var clone = (VariableExpression)expression.Clone();

            //Assert
            Assert.AreEqual(1, 2);
        }       
    }
}
