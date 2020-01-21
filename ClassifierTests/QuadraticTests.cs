using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Parsers;
using Mathema.Classifier;
using Mathema.Enums.Equations;
using Mathema.Models.Equations;
using NUnit.Framework;

namespace ClassifierTests
{
    [TestFixture]
    public class QuadraticTests
    {

        [Test]
        public void Quadratic_1()
        {
            //Arrange
            var text = "2*x + 2*x^2 - 2 + 3*x - 4*x^2 + 1";
            var expression = ExpressionBuilder.BuildFlat(RPNParser.Parse(text).Output).Execute();
            var equation = new Equation(text, expression, null);

            //Act
            var sut = EquationClassifier.Classify(equation, "x");

            //Assert
            Assert.AreEqual(EquationTypes.Quadratic, sut.Type);
        }
    }
}