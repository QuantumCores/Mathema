﻿using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Helpers;
using Mathema.Algorithms.Parsers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlatExpressionTests.VariableTests
{
    [TestFixture]
    public class FlatAddAndMultTests
    {
        [Test]
        public void Add_TheSame_FlatMultiply()
        {
            //Arrange
            var text = "y * 2 * x  + 3 * x * y";
            var expText = " 5 * x * y";
            var expected = RPNParser.Parse(expText);

            //Act
            var rpn = RPNParser.Parse(text);
            var expr = ExpressionBuilder.BuildFlat(rpn.Output).Execute();
            var actual = RPNParser.Parse(expr.ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output), $"Expected: {expText} but was {expr.ToString()}");

        }

        [Test]
        public void Add_Different_FlatMultiply()
        {
            //Arrange
            var text = "y * 2 * x  + 3 * x * y + x + y";
            var expText = " 5 * x * y + x + y";
            var expected = RPNParser.Parse(expText);

            //Act
            var rpn = RPNParser.Parse(text);
            var expr = ExpressionBuilder.BuildFlat(rpn.Output).Execute();
            var actual = RPNParser.Parse(expr.ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output), $"Expected: {expText} but was {expr.ToString()}");

        }

        [Test]
        public void Squash_To_Quadratic()
        {
            //Arrange
            var text = "2 * x * x + 5 + 3 * x - 1";// "2 * x  - 1 + 3 * x * x + x + 5 - x*x";
            var expText = "2 * x * x + 3 * x + 4";
            var expected = RPNParser.Parse(expText);

            //Act
            var rpn = RPNParser.Parse(text);
            var expr = ExpressionBuilder.BuildFlat(rpn.Output).Execute();
            var actual = RPNParser.Parse(expr.ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output), $"Expected: {expText} but was {expr.ToString()}");

        }
    }
}
