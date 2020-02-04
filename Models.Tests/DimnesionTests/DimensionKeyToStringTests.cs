using Mathema.Enums.DimensionKeys;
using Mathema.Models.Dimension;
using Mathema.Models.Expressions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsTests.DimnesionTests
{
    [TestFixture]
    public class DimensionKeyToStringTests
    {
        [Test]
        public void Multiply_x_by_y()
        {
            //Arrange
            var expr = new BinaryExpression(new VariableExpression("x", 1), Mathema.Enums.Operators.OperatorTypes.Multiply, new VariableExpression("y", 1));
            var expected = "x * y";

            //Act
            var res = expr.Execute();
            var actual = res.DimensionKey.ToString();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Divide_x_by_y()
        {
            //Arrange
            var expr = new BinaryExpression(new VariableExpression("x", 1), Mathema.Enums.Operators.OperatorTypes.Divide, new VariableExpression("y", 1));
            var expected = "x / y";

            //Act
            var res = expr.Execute();
            var actual = res.DimensionKey.ToString();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Divide_1_by_x_And_y()
        {
            //Arrange
            var x = new BinaryExpression(new NumberExpression(1), Mathema.Enums.Operators.OperatorTypes.Divide, new VariableExpression("x", 1));
            var expr = new BinaryExpression(x, Mathema.Enums.Operators.OperatorTypes.Divide, new VariableExpression("y", 1));
            var expected = "1 / x / y";

            //Act
            var res = expr.Execute();
            var actual = res.DimensionKey.ToString();

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
