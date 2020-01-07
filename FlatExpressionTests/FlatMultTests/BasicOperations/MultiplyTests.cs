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
    public class MultiplyTests
    {
        [Test]
        public void Multiply_FlatMult_FlatThatIsZero()
        {
            //Arrange
            var text = "x*(x -x)";
            var expText = "0";
            var expected = RPNParser.Parse(expText);

            //Act
            var rpn = RPNParser.Parse(text);
            var expr = ExpressionBuilder.BuildFlat(rpn.Output).Execute();
            var actual = RPNParser.Parse(ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output), $"Expected: {expText} but was {expr.ToString()}");
        }
    }
}
