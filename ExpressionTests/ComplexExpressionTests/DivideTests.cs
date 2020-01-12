using Mathema.Enums.Operators;
using Mathema.Models.Expressions;
using Mathema.Models.Numerics;
using NUnit.Framework;

namespace ExpressionTests.ComplexExpressionTests
{
    [TestFixture]
    public class DivideTests
    {
        [Test]
        public void Divide_Complex_Number()
        {
            //Arrange
            var expected = new ComplexExpression(1, 3);
            var c = new ComplexExpression(2, 6);
            var n = new NumberExpression(2);
            var expr = new BinaryExpression(c, OperatorTypes.Divide, n);

            //Act
            var actual = (ComplexExpression)expr.Execute();

            //Assert
            Assert.AreEqual(expected.Count.Re.ToNumber(), actual.Count.Re.ToNumber());
            Assert.AreEqual(expected.Count.Im.ToNumber(), actual.Count.Im.ToNumber());
        }


        [Test]
        public void Divide_Complex_NegativeNumber()
        {
            //Arrange
            var expected = new ComplexExpression(-1, -3);
            var c = new ComplexExpression(3, 9);
            var n = new NumberExpression(-3);
            var expr = new BinaryExpression(c, OperatorTypes.Divide, n);

            //Act
            var actual = (ComplexExpression)expr.Execute();

            //Assert
            Assert.AreEqual(expected.Count.Re.ToNumber(), actual.Count.Re.ToNumber());
            Assert.AreEqual(expected.Count.Im.ToNumber(), actual.Count.Im.ToNumber());
        }

        [Test]
        public void Divide_Complex_Complex()
        {
            //Arrange
            var expected = new ComplexExpression(new Fraction(-7, 10), new Fraction(9, 10));
            var c = new ComplexExpression(2, 3);
            var n = new ComplexExpression(1, -3);
            var expr = new BinaryExpression(c, OperatorTypes.Divide, n);

            //Act
            var actual = (ComplexExpression)expr.Execute();

            //Assert
            Assert.AreEqual(expected.Count.Re.ToNumber(), actual.Count.Re.ToNumber());
            Assert.AreEqual(expected.Count.Im.ToNumber(), actual.Count.Im.ToNumber());
        }
    }
}
