using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Parsers;
using Mathema.Models.Equations;
using Mathema.Solver;
using NUnit.Framework;
using System;

namespace SolverTests.QuadraticEquations
{
    [TestFixture]
    public class Quadratic
    {
        [Test]
        public void Delta_gtZero()
        {
            //Arrange
            var text = "-2*x + 2*x^2 - 2 - 2*x - x^2 - 3";
            var expression = ExpressionBuilder.BuildFlat(RPNParser.Parse(text).Output).Execute();
            var equation = new Equation(text, expression, null);

            //Act
            var sut = Solver.Solve(equation, "x");

            //Assert
            Assert.IsTrue(sut.Solutions["x"].Item2.Count == 2);
            Assert.AreEqual(sut.Solutions["x"].Item2[0].Count.Re.ToNumber(), 5);
            Assert.AreEqual(sut.Solutions["x"].Item2[1].Count.Re.ToNumber(), -1);
        }

        [Test]
        public void Delta_Zero()
        {
            //Arrange
            var text = "x^2 - 2*x + 1";
            var expression = ExpressionBuilder.BuildFlat(RPNParser.Parse(text).Output).Execute();
            var equation = new Equation(text, expression, null);

            //Act
            var sut = Solver.Solve(equation, "x");

            //Assert
            Assert.IsTrue(sut.Solutions["x"].Item2.Count == 2);
            Assert.AreEqual(sut.Solutions["x"].Item2[0].Count.Re.ToNumber(), 1);
            Assert.AreEqual(sut.Solutions["x"].Item2[1].Count.Re.ToNumber(), 1);
        }

        [Test]
        public void Delta_ltZero()
        {
            //Arrange
            var text = "x^2 + 6*x + 10";
            var expression = ExpressionBuilder.BuildFlat(RPNParser.Parse(text).Output).Execute();
            var equation = new Equation(text, expression, null);

            //Act
            var sut = Solver.Solve(equation, "x");

            //Assert
            Assert.IsTrue(sut.Solutions["x"].Item2.Count == 2);
            Assert.AreEqual(sut.Solutions["x"].Item2[0].Count.Re.ToNumber(), -3);
            Assert.AreEqual(sut.Solutions["x"].Item2[0].Count.Im.ToNumber(), 1);
            Assert.AreEqual(sut.Solutions["x"].Item2[1].Count.Re.ToNumber(), -3);
            Assert.AreEqual(sut.Solutions["x"].Item2[1].Count.Im.ToNumber(), -1);
        }


        [Test]
        public void Quadratic_ForFunction()
        {
            //Arrange
            var text = "Cos(x)^2 - 4*Cos(x) - 5";
            var expression = ExpressionBuilder.BuildFlat(RPNParser.Parse(text).Output).Execute();
            var equation = new Equation(text, expression, null);

            //Act
            var sut = Solver.Solve(equation, "x");

            //Assert
            Assert.IsTrue(sut.Solutions["x"].Item2.Count == 2);
            Assert.AreEqual(sut.Solutions["x"].Item2[0].DimensionKey.Key, "ACos(5)");
            Assert.AreEqual(sut.Solutions["x"].Item2[1].Count.Re.ToNumber(), (decimal)Math.PI);
        }
    }
}