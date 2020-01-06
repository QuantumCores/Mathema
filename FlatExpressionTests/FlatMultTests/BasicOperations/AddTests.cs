using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Helpers;
using Mathema.Algorithms.Parsers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlatExpressionTests.FlatMultTests.BasicOperations
{
    [TestFixture]
    public class AddTests
    {
        [Test]
        public void Add_FlatMult_Variable()
        {
            //Arrange
            var text = "2*x + x";
            var expText = "3*x";
            var expected = RPNParser.Parse(expText);

            //Act
            var rpn = RPNParser.Parse(text);
            var expr = ExpressionBuilder.BuildFlat(rpn.Output).Execute();
            var actual = RPNParser.Parse(ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output), $"Expected: {expText} but was {expr.ToString()}");
        }

        [Test]
        public void Add_FlatMult_Variable_Number()
        {
            //Arrange
            var text = "2*x +2 + x";
            var expected = RPNParser.Parse("3*x + 2");

            //Act
            var rpn = RPNParser.Parse(text);
            var expr = ExpressionBuilder.BuildFlat(rpn.Output).Execute();
            var actual = RPNParser.Parse(expr.ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output));
        }

        [Test]
        public void AddDoubles()
        {
            //Arrange
            var text = 2.2.ToString() + "+" + 4.3.ToString();
            var expected = 6.5;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.BuildFlat(rpn.Output).Execute().Count.ToNumber();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddDecimals()
        {
            //Arrange
            var text = 2.2m.ToString() + "+" + 4.3m.ToString();
            var expected = 6.5;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.BuildFlat(rpn.Output).Execute().Count.ToNumber();

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
