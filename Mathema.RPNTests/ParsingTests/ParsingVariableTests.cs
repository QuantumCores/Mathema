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
