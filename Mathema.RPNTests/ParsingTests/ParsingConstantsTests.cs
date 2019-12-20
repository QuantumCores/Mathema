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

            var text = "Pi";
            var expected = Constants.PI.ToString();

            //Act
            var rpn = RPNParser.Parse(text);

            //Assert
            Assert.IsTrue(RPNComparer.Compare(rpn, expected));
        }

        [Test]
        public void Parse_Phi()
        {
            //Arrange

            var text = "Phi";
            var expected = Constants.Phi.ToString();

            //Act
            var rpn = RPNParser.Parse(text);

            //Assert
            Assert.IsTrue(RPNComparer.Compare(rpn, expected));
        }

        [Test]
        public void Parse_e()
        {
            //Arrange

            var text = "e";
            var expected = Constants.e.ToString();

            //Act
            var rpn = RPNParser.Parse(text);

            //Assert
            Assert.IsTrue(RPNComparer.Compare(rpn, expected));
        }

        [Test]
        public void Parse_EM()
        {
            //Arrange

            var text = "em";
            var expected = Constants.EM.ToString();

            //Act
            var rpn = RPNParser.Parse(text);

            //Assert
            Assert.IsTrue(RPNComparer.Compare(rpn, expected));
        }

        [Test]
        public void Parse_Pi_Sin()
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
        public void Parse_e_Sin()
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
