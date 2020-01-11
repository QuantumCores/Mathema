using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Parsers;
using Mathema.Interfaces;
using Mathema.Shared.Constants;
using NUnit.Framework;
using System;

namespace ExpressionTests.ConstantTests
{
    [TestFixture]
    public class ValuesTests
    {
        [Test]
        public void Constant_e()
        {
            //Arrange
            var text = "e";
            var expected = (decimal)(Constants.e);

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.Build(rpn.Output).Execute().Count.Re.ToNumber();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Constant_pi()
        {
            //Arrange
            var text = "pi";
            var expected = (decimal)(Constants.PI);

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.Build(rpn.Output).Execute().Count.Re.ToNumber();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Constant_Em()
        {
            //Arrange
            var text = "Em";
            var expected = (decimal)(Constants.EM);

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.Build(rpn.Output).Execute().Count.Re.ToNumber();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Constant_phi()
        {
            //Arrange
            var text = "phi";
            var expected = (decimal)(Constants.Phi);

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.Build(rpn.Output).Execute().Count.Re.ToNumber();

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
