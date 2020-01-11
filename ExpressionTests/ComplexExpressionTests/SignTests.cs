using Mathema.Enums.Operators;
using Mathema.Models.Expressions;
using NUnit.Framework;

namespace ExpressionTests.ComplexExpressionTests
{
    [TestFixture]
    public class SignTests
    {
        [Test]
        public void Sign_Complex()
        {
            //Arrange
            var expected = new ComplexExpression(-4, -3);
            var c = new ComplexExpression(4, 3);
            var expr = new UnaryExpression(OperatorTypes.Sign, c);

            //Act
            var actual = (ComplexExpression)expr.Execute();

            //Assert
            Assert.AreEqual(expected.Count.Re.ToNumber(), actual.Count.Re.ToNumber());
            Assert.AreEqual(expected.Count.Im.ToNumber(), actual.Count.Im.ToNumber());
        }

        //[Test]
        //public void ChangeWhenAdding()
        //{
        //    //Arrange
        //    var text = "12+-6";
        //    var expected = 6;

        //    //Act
        //    var rpn = RPNParser.Parse(text);
        //    var actual = ExpressionBuilder.Build(rpn.Output).Execute().Count.ToNumber();

        //    //Assert
        //    Assert.AreEqual(expected, actual);
        //}

        //[Test]
        //public void ChangeWhenSubtracting()
        //{
        //    //Arrange
        //    var text = "12--6";
        //    var expected = 18;

        //    //Act
        //    var rpn = RPNParser.Parse(text);
        //    var actual = ExpressionBuilder.Build(rpn.Output).Execute().Count.ToNumber();

        //    //Assert
        //    Assert.AreEqual(expected, actual);
        //}

        //[Test]
        //public void ChangeWhenMultiplying()
        //{
        //    //Arrange
        //    var text = "3*-6";
        //    var expected = -18;

        //    //Act
        //    var rpn = RPNParser.Parse(text);
        //    var actual = ExpressionBuilder.Build(rpn.Output).Execute().Count.ToNumber();

        //    //Assert
        //    Assert.AreEqual(expected, actual);
        //}

        //[Test]
        //public void ChangeWhenMultiplyingAndPower()
        //{
        //    //Arrange
        //    var text = "3*-2^2";
        //    var expected = -12;

        //    //Act
        //    var rpn = RPNParser.Parse(text);
        //    var actual = ExpressionBuilder.Build(rpn.Output).Execute().Count.ToNumber();

        //    //Assert
        //    Assert.AreEqual(expected, actual);
        //}

        //[Test]
        //public void ChangeInPower()
        //{
        //    //Arrange
        //    var text = "4^(-2)";
        //    var expected = 0.0625;

        //    //Act
        //    var rpn = RPNParser.Parse(text);
        //    var actual = ExpressionBuilder.Build(rpn.Output).Execute().Count.ToNumber();

        //    //Assert
        //    Assert.AreEqual(expected, actual);
        //}

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
