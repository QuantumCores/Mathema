using Mathema.Enums.Operators;
using Mathema.Models.Expressions;
using NUnit.Framework;

namespace ExpressionTests.ComplexExpressionTests
{
    [TestFixture]
    public class PowerTests
    {
        [Test]
        public void Pow_Complex_0()
        {
            //Arrange
            var expected = new NumberExpression(1);
            var c = new ComplexExpression(-2, 3);
            var n = new NumberExpression(0);
            var expr = new BinaryExpression(c, OperatorTypes.Power, n);

            //Act
            var actual = (NumberExpression)expr.Execute();

            //Assert
            Assert.AreEqual(expected.Count.Re.ToNumber(), actual.Count.Re.ToNumber());
        }

        [Test]
        public void Pow_Complex_1()
        {
            //Arrange
            var expected = new ComplexExpression(2, 3);
            var c = new ComplexExpression(2, 3);
            var n = new NumberExpression(1);
            var expr = new BinaryExpression(c, OperatorTypes.Power, n);

            //Act
            var actual = (ComplexExpression)expr.Execute();

            //Assert
            Assert.AreEqual(expected.Count.Re.ToNumber(), actual.Count.Re.ToNumber());
            Assert.AreEqual(expected.Count.Im.ToNumber(), actual.Count.Im.ToNumber());
        }


        [Test]
        public void Pow_Complex_2()
        {
            //Arrange
            var expected = new ComplexExpression(-5, 12);
            var c = new ComplexExpression(2, 3);
            var n = new NumberExpression(2);
            var expr = new BinaryExpression(c, OperatorTypes.Power, n);

            //Act
            var actual = (ComplexExpression)expr.Execute();

            //Assert
            Assert.AreEqual(expected.Count.Re.ToNumber(), actual.Count.Re.ToNumber());
            Assert.AreEqual(expected.Count.Im.ToNumber(), actual.Count.Im.ToNumber());
        }

        [Test]
        public void Pow_Complex_3()
        {
            //Arrange
            var expected = new ComplexExpression(-46, 9);
            var c = new ComplexExpression(2, 3);
            var n = new NumberExpression(3);
            var expr = new BinaryExpression(c, OperatorTypes.Power, n);

            //Act
            var actual = (ComplexExpression)expr.Execute();

            //Assert
            Assert.AreEqual(expected.Count.Re.ToNumber(), actual.Count.Re.ToNumber());
            Assert.AreEqual(expected.Count.Im.ToNumber(), actual.Count.Im.ToNumber());
        }

        //[Test]
        //public void PowDoubles()
        //{
        //    //Arrange
        //    var text = 2.0.ToString() + "^" + 3.0.ToString();
        //    var expected = 8;

        //    //Act
        //    var rpn = RPNParser.Parse(text);
        //    var actual = ExpressionBuilder.Build(rpn.Output).Execute().Count.ToNumber();

        //    //Assert
        //    Assert.AreEqual(expected, actual);
        //}

        //[Test]
        //public void PowDecimals()
        //{
        //    //Arrange
        //    var text = 2m.ToString() + "^" + 4m.ToString();
        //    var expected = 16;

        //    //Act
        //    var rpn = RPNParser.Parse(text);
        //    var actual = ExpressionBuilder.Build(rpn.Output).Execute().Count.ToNumber();

        //    //Assert
        //    Assert.AreEqual(expected, actual);
        //}
    }
}
