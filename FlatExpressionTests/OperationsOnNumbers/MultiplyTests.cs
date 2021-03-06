﻿using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Helpers;
using Mathema.Algorithms.Parsers;
using Mathema.Interfaces;
using NUnit.Framework;

namespace FlatExpressionTests.OperationsOnNumbers
{
    [TestFixture]
    public class MultiplyTests
    {
        [Test]
        public void MultiplyIntegers()
        {
            //Arrange
            var text = 5.ToString() + "*" + 2.ToString();
            var expected = 10;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.BuildFlat(rpn.Output).Execute().Count.Re.ToNumber();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MultiplyDoubles()
        {
            //Arrange
            var text = 4.0.ToString() + "*" + 2.5.ToString();
            var expected = 10;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.BuildFlat(rpn.Output).Execute().Count.Re.ToNumber();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MultiplyDecimals()
        {
            //Arrange
            var text = 4.0m.ToString() + "*" + 2.5m.ToString();
            var expected = 10;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.BuildFlat(rpn.Output).Execute().Count.Re.ToNumber();

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
