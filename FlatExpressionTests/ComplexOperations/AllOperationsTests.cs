﻿using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Parsers;
using Mathema.Interfaces;
using NUnit.Framework;
using System;

namespace FlatExpressionTests.ComplexOperations
{
    [TestFixture]
    public class FlatExpressionValueTests
    {
        [Test]
        public void AllOperators()
        {
            //Arrange
            var text = "(4- 6 * -2 )^1/(2 + 2)^(1/2)";
            var expected = 8;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.BuildFlat(rpn.Output).Execute().Count.ToNumber();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AllOperatorsAndFunctions()
        {
            //Arrange
            var text = "Sin((4- 6 * -2 )^1/(2 + 2)^(1/2))";
            var expected = (decimal)Math.Sin(8);

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.BuildFlat(rpn.Output).Execute().Count.ToNumber();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void VariableWithFunction()
        {
            //Arrange
            var text = "Sin(x) + x + Sin(x)";
            var expected = "2 * Sin(x) + x";

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString();

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
