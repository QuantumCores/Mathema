using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Parsers;
using Mathema.Interfaces;
using NUnit.Framework;
using System;

namespace ExpressionTests.BasicOperations
{
    [TestFixture]
    public class SignTests
    {
        [Test]
        public void ChangeOnly()
        {
            //Arrange
            var text = "-6";
            var expected = -6;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.Build(rpn.Output).Value().Count.ToNumber();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ChangeWhenAdding()
        {
            //Arrange
            var text = "12+-6";
            var expected = 6;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.Build(rpn.Output).Value().Count.ToNumber();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ChangeWhenSubtracting()
        {
            //Arrange
            var text = "12--6";
            var expected = 18;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.Build(rpn.Output).Value().Count.ToNumber();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ChangeWhenMultiplying()
        {
            //Arrange
            var text = "3*-6";
            var expected = -18;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.Build(rpn.Output).Value().Count.ToNumber();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ChangeWhenMultiplyingAndPower()
        {
            //Arrange
            var text = "3*-2^2";
            var expected = -12;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.Build(rpn.Output).Value().Count.ToNumber();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ChangeInPower()
        {
            //Arrange
            var text = "4^(-2)";
            var expected = 0.0625;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual =ExpressionBuilder.Build(rpn.Output).Value().Count.ToNumber();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        //[Test]
        //public void ChangeInPowerWithoutParents()
        //{
        //    //Arrange
        //    var text = "4^-2";
        //    var expected = 0.0625;

        //    //Act
        //    var rpn = RPNParser.Parse(text);
        //    var actual = ExpressionBuilder.Build(rpn).Value();

        //    //Assert
        //    Assert.That(() => ExpressionBuilder.Build(rpn), Throws.ArgumentException);
        //}
    }
}
