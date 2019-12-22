using Mathema.Models.Dimension;
using Mathema.Models.Expressions;
using Mathema.Models.FlatExpressions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlatExpressionTests.CloneTests
{
    [TestFixture]
    public class CloneTests
    {
        [Test]
        public void Clone_FlatAddExpression()
        {
            //Arrange
            var expression = new FlatAddExpression();
            expression.Add(new VariableExpression("x", 1));
            expression.Add(new NumberExpression(3));

            //Act
            var clone = (FlatAddExpression)expression.Clone();

            //Assert
            Assert.IsTrue(DimensionKey.Compare(expression.DimensionKey, clone.DimensionKey));
            Assert.IsTrue(FlatExpression.Compare(expression, clone));
        }

        [Test]
        public void Clone_FlatMultExpression()
        {
            //Arrange
            var expression = new FlatMultExpression();
            expression.Add(new VariableExpression("x", 1));
            expression.Add(new NumberExpression(3));

            //Act
            var clone = (FlatMultExpression)expression.Clone();

            //Assert
            Assert.IsTrue(DimensionKey.Compare(expression.DimensionKey, clone.DimensionKey));
            Assert.IsTrue(FlatExpression.Compare(expression, clone));
        }
    }
}
