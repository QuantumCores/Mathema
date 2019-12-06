using Mathema.Algorithms.Helpers;
using Mathema.Algorithms.Parsers;
using Mathema.Shared.Constants;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.RPNTests.ParsingTests
{
    [TestFixture]
    public class ParsingConstants
    {
        [Test]
        public void Parse_Pi()
        {
            //Arrange

            var text = "Sin(Pi)";
            var expected = Constants.PI.ToString() + " sin";

            //Act
            var rpn = RPNParser.Parse(text);

            //Assert
            Assert.IsTrue(RPNComparer.Compare(rpn, expected));
        }

        [Test]
        public void Parse_e()
        {
            //Arrange

            var text = "Sin(e)";
            var expected = Constants.e + " sin";

            //Act
            var rpn = RPNParser.Parse(text);

            //Assert
            Assert.IsTrue(RPNComparer.Compare(rpn, expected));
        }
    }
}
