using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1Tests
{
    [TestClass]
    public class ComplexNumberTests
    {
        [TestMethod]
        public void Add_ReturnsCorrectSum()
        {
            var a = new ComplexNumber(10, 20);
            var b = new ComplexNumber(1, 2);

            var result = a.Add(b);

            Assert.AreEqual(new ComplexNumber(11, 22), result);
        }

        [TestMethod]
        public void Subtract_ReturnsCorrectDifference()
        {
            var a = new ComplexNumber(10, 20);
            var b = new ComplexNumber(4, 5);

            var result = a.Subtract(b);

            Assert.AreEqual(new ComplexNumber(6, 15), result);
        }

        [TestMethod]
        public void Multiply_ReturnsCorrectProduct()
        {
            var a = new ComplexNumber(2, 3);
            var b = new ComplexNumber(4, 5);

            // (2 + 3i)*(4 + 5i) = 8 + 10i + 12i - 15 = -7 + 22i
            var result = a.Multiply(b);

            Assert.AreEqual(new ComplexNumber(-7, 22), result);
        }

        [TestMethod]
        public void Divide_ReturnsCorrectQuotient()
        {
            var a = new ComplexNumber(-7, 22);
            var b = new ComplexNumber(4, 5);

            var result = a.Divide(b);

            Assert.AreEqual(new ComplexNumber(2, 3), result);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void Divide_ByZero_ThrowsException()
        {
            var a = new ComplexNumber(1, 1);
            var b = ComplexNumber.Zero;

            a.Divide(b);
        }

        [TestMethod]
        public void GetAbsoluteValue_ReturnsMagnitude()
        {
            var number = new ComplexNumber(3, 4);

            Assert.AreEqual(5.0, number.GetAbsoluteValue(), 1e-9);
            Assert.AreEqual(5.0, number.GetAbS(), 1e-9);
        }

        [TestMethod]
        public void GetAngleInDegrees_ReturnsCorrectAngle()
        {
            var number = new ComplexNumber(1, 1);

            Assert.AreEqual(45.0, number.GetAngleInDegrees(), 1e-5);
        }

        [TestMethod]
        public void ToString_FormatsComplexNumber()
        {
            var number = new ComplexNumber(5, -3);

            Assert.AreEqual("(5 + -3i)", number.ToString());
        }
    }
}
