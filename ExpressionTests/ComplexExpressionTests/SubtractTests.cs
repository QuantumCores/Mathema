using Mathema.Enums.Operators;
using Mathema.Models.Expressions;
using NUnit.Framework;

namespace ExpressionTests.ComplexExpressionTests
{
    [TestFixture]
    public class SubtractTests
    {
        [Test]
        public void Subtract_Complex_Number()
        {
            //Arrange
            var expected = new ComplexExpression(1, 3);
            var c = new ComplexExpression(2, 3);
            var n = new NumberExpression(1);
            var expr = new BinaryExpression(c, OperatorTypes.Subtract, n);

            //Act
            var actual = (ComplexExpression)expr.Execute();

            //Assert
            Assert.AreEqual(expected.Count.Re.ToNumber(), actual.Count.Re.ToNumber());
            Assert.AreEqual(expected.Count.Im.ToNumber(), actual.Count.Im.ToNumber());
        }


        [Test]
        public void Subtract_Complex_NegativeNumber()
        {
            //Arrange
            var expected = new ComplexExpression(3, 3);
            var c = new ComplexExpression(2, 3);
            var n = new NumberExpression(-1);
            var expr = new BinaryExpression(c, OperatorTypes.Subtract, n);

            //Act
            var actual = (ComplexExpression)expr.Execute();

            //Assert
            Assert.AreEqual(expected.Count.Re.ToNumber(), actual.Count.Re.ToNumber());
            Assert.AreEqual(expected.Count.Im.ToNumber(), actual.Count.Im.ToNumber());
        }

        [Test]
        public void Subtract_Complex_ResultAsNumber()
        {
            //Arrange
            var expected = new NumberExpression(1);
            var c = new ComplexExpression(2, 3);
            var n = new ComplexExpression(1, 3);
            var expr = new BinaryExpression(c, OperatorTypes.Subtract, n);

            //Act
            var actual = (NumberExpression)expr.Execute();

            //Assert
            Assert.AreEqual(expected.Count.Re.ToNumber(), actual.Count.Re.ToNumber());
        }
    }
}
