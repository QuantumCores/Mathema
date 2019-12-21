using Mathema.Algorithms.Handlers;
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
            var expected = RPNParser.Parse(" 5 * x * y");

            //Act
            var rpn = RPNParser.Parse(text);
            var the = ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString();
            var actual = RPNParser.Parse(ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output));

        }

        [Test]
        public void Add_Different_FlatMultiply()
        {
            //Arrange
            var text = "y * 2 * x  + 3 * x * y + x + y";
            var expected = RPNParser.Parse(" 5 * x * y + x + y");

            //Act
            var rpn = RPNParser.Parse(text);
            var the = ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString();
            var actual = RPNParser.Parse(ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output));

        }
    }
}
