using Mathema.Enums.Operators;
using Mathema.Models.Expressions;
using NUnit.Framework;

namespace ExpressionTests.ComplexExpressionTests
{
    [TestFixture]
    public class AddTests
    {

        [Test]
        public void Add_Complex_Number()
        {
            //Arrange
            var expected = new ComplexExpression(4, 3);
            var c = new ComplexExpression(2, 3);
            var n = new NumberExpression(2);
            var expr = new BinaryExpression(c, OperatorTypes.Add, n);

            //Act
            var actual = (ComplexExpression)expr.Execute();

            //Assert
            Assert.AreEqual(expected.Re.ToNumber(), actual.Re.ToNumber());
            Assert.AreEqual(expected.Im.ToNumber(), actual.Im.ToNumber());
        }


        [Test]
        public void Add_Complex_NegativeNumber()
        {
            //Arrange
            var expected = new ComplexExpression(1, 3);
            var c = new ComplexExpression(2, 3);
            var n = new NumberExpression(-1);
            var expr = new BinaryExpression(c, OperatorTypes.Add, n);

            //Act
            var actual = (ComplexExpression)expr.Execute();

            //Assert
            Assert.AreEqual(expected.Re.ToNumber(), actual.Re.ToNumber());
            Assert.AreEqual(expected.Im.ToNumber(), actual.Im.ToNumber());
        }

        [Test]
        public void Add_Complex_ResultAsNumber()
        {
            //Arrange
            var expected = new NumberExpression(3);
            var c = new ComplexExpression(2, 3);
            var n = new ComplexExpression(1, -3);
            var expr = new BinaryExpression(c, OperatorTypes.Add, n);

            //Act
            var actual = (NumberExpression)expr.Execute();

            //Assert
            Assert.AreEqual(expected.Count.ToNumber(), actual.Count.ToNumber());
        }
    }
}