using Mathema.Enums.DimensionKeys;
using Mathema.Models.Dimension;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsTests.DimnesionTests
{
    [TestFixture]
    public class DimensionKeyToStringTests
    {
        [Test]
        public void Multiply_x_by_y()
        {
            //Arrange
            var expected = "x * y";

            //Act
            var dimk = new DimensionKey();
            dimk.Add("x", 1);
            dimk.Add("y", 1);
            var actual = dimk.ToString();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Divide_x_by_y()
        {
            //Arrange
            var expected = "x / y";

            //Act
            var dimk = new DimensionKey();
            dimk.Add("x", 1);
            dimk.Add("y", -1);
            var actual = dimk.ToString();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Divide_1_by_x_And_y()
        {
            //Arrange
            var expected = "1 / x / y";

            //Act
            var dimk = new DimensionKey();
            dimk.Add(Dimensions.Number, 1);
            dimk.Add("x", -1);
            dimk.Add("y", -1);
            var actual = dimk.ToString();

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
