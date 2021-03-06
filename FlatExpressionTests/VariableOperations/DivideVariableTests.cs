﻿using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Helpers;
using Mathema.Algorithms.Parsers;
using Mathema.Interfaces;
using NUnit.Framework;

namespace FlatExpressionTests.VariableOperations
{
    [TestFixture]
    public class DivideVariableTests
    {
        [Test]
        public void Divide_x_by_x()
        {
            //Arrange
            var text = "x/x";
            var expected = RPNParser.Parse("1");

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = RPNParser.Parse(ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output));
        }

        [Test]
        public void Divide_x_by_y()
        {
            //Arrange
            var text = "x/y";
            var expText = "x/y";
            var expected = RPNParser.Parse(expText);

            //Act
            var rpn = RPNParser.Parse(text);
            var expr = ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString();
            var actual = RPNParser.Parse(expr.ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output), $"Expected: {expText} but was {expr.ToString()}");           
        }

        [Test]
        public void Divide_x_by_y_fun_x()
        {
            //Arrange
            var text = "x/y/fun/x";
            var expText = "1 / fun / y";
            var expected = RPNParser.Parse(expText);

            //Act
            var rpn = RPNParser.Parse(text);
            var expr = ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString();
            var actual = RPNParser.Parse(expr.ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output), $"Expected: {expText} but was {expr.ToString()}");
        }

        [Test]
        public void Divide_1_by_x_y_fun_x()
        {
            //Arrange
            var text = "1/x/y/fun/x";
            var expText = "1 / fun / y / (x)^2";
            var expected = RPNParser.Parse(expText);

            //Act
            var rpn = RPNParser.Parse(text);
            var expr = ExpressionBuilder.BuildFlat(rpn.Output).Execute();
            var actual = RPNParser.Parse(expr.ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output), $"Expected: {expText} but was {expr.ToString()}");
        }

        [Test]
        public void Divide_x_by_x_by_y_()
        {
            //Arrange
            var text = "x/(x/y)";
            var expText = "1*y";
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
