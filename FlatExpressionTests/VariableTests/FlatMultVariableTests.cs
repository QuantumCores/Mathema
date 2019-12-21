using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Parsers;
using Mathema.Models.Expressions;
using Mathema.Models.FlatExpressions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlatExpressionTests.VariableTests
{
    [TestFixture]
    public class FlatMultVariableTests
    {
        [Test]
        public void CreatesFlatExpression()
        {
            //Arrange
            var text = "2 * x";

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.BuildFlat(rpn.Output).Execute();

            //Assert
            Assert.IsTrue(actual is FlatMultExpression);
        }

        [Test]
        public void RecognizesVariable()
        {
            //Arrange
            var text = "2 * x";
            var expected = new FlatMultExpression();
            expected.Add(new NumberExpression(2));
            expected.Add(new VariableExpression("x", 1m));

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = (FlatMultExpression)(ExpressionBuilder.BuildFlat(rpn.Output).Execute());

            //Assert
            Assert.IsTrue(actual.Dimensions.ContainsKey(""));
            Assert.IsTrue(actual.Dimensions.ContainsKey("x"));
        }

        [Test]
        public void Squash_Numbers()
        {
            //Arrange
            var text = "2 * x  *3";
            var expected = "( 6 * x)";

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Squash_OneVariable()
        {
            //Arrange
            var text = "2 * x  * 3 * x";
            var expected = "( 6 * x * x)";

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Value_Squash_TwoVariables()
        {
            //Arrange
            var text = "y * 2 * x  * 3 * x * y * y";
            var expected = "( 6 * x * x * y * y * y)";

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString();

            //Assert
            Assert.AreEqual(expected, actual);
        }


    }
}
