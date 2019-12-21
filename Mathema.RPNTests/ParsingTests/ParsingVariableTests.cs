using Mathema.Algorithms.Helpers;
using Mathema.Algorithms.Parsers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.RPNTests.ParsingTests
{
    [TestFixture]
    public class ParsingVariableTests
    {
        [Test]
        public void Parse_X_InVariables()
        {
            //Arrange
            var text = "x+1";

            //Act
            var rpn = RPNParser.Parse(text);

            //Assert
            Assert.IsTrue(rpn.Variables.Count == 1);
            Assert.IsTrue(rpn.Variables.Contains("x"));
        }

        [Test]
        public void Parse_XAndY_InVariables()
        {
            //Arrange
            var text = "x+1+y";

            //Act
            var rpn = RPNParser.Parse(text);

            //Assert
            Assert.IsTrue(rpn.Variables.Count == 2);
            Assert.IsTrue(rpn.Variables.Contains("x"));
            Assert.IsTrue(rpn.Variables.Contains("y"));
        }

        [Test]
        public void Parse_X_Add_Number()
        {
            //Arrange
            var text = "x+1";
            var expected = "x1+";

            //Act
            var rpn = RPNParser.Parse(text);

            //Assert
            Assert.IsTrue(RPNComparer.Compare(rpn.Output, expected));
        }

        [Test]
        public void Parse_X_Subtract_Number()
        {
            //Arrange
            var text = "x-1";
            var expected = "x1Sign";

            //Act
            var rpn = RPNParser.Parse(text);

            //Assert
            Assert.IsTrue(RPNComparer.Compare(rpn.Output, expected));
        }

        [Test]
        public void Parse_Number_Subtract_X()
        {
            //Arrange
            var text = "1-x";
            var expected = "1x-";

            //Act
            var rpn = RPNParser.Parse(text);

            //Assert
            Assert.IsTrue(RPNComparer.Compare(rpn.Output, expected));
        }

        [Test]
        public void Parse_X_WithNumbers()
        {
            //Arrange
            var text = "2*x+1";
            var expected = "2x*1+";

            //Act
            var rpn = RPNParser.Parse(text);

            //Assert
            Assert.IsTrue(RPNComparer.Compare(rpn.Output, expected));
        }
    }
}
