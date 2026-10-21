using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1Tests
{
    [TestClass]
    public class ComplexNumberTests
    {
        [TestMethod]
        public void Add_CombinesRealAndImaginaryParts()
        {
            var first = new ComplexNumber(10, 20);
            var second = new ComplexNumber(1, 2);

            Assert.AreEqual(new ComplexNumber(11, 22), first.Add(second));
            Assert.AreEqual(new ComplexNumber(10, 20), first);
        }

        [TestMethod]
        public void Subtract_ComputesDifference()
        {
            Assert.AreEqual(new ComplexNumber(2, -3),
                new ComplexNumber(5, 1).Subtract(new ComplexNumber(3, 4)));
        }

        [TestMethod]
        public void Multiply_UsesComplexArithmetic()
        {
            Assert.AreEqual(new ComplexNumber(-2, 10),
                new ComplexNumber(2, 3).Multiply(new ComplexNumber(2, 2)));
        }

        [TestMethod]
        public void Divide_ReconstructsTheDividend()
        {
            var dividend = new ComplexNumber(5, 3);
            var divisor = new ComplexNumber(2, -1);
            var result = dividend.Divide(divisor).Multiply(divisor);

            Assert.AreEqual(dividend.Real, result.Real, 1e-12);
            Assert.AreEqual(dividend.Imaginary, result.Imaginary, 1e-12);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void Divide_RejectsZeroDivisor()
        {
            new ComplexNumber(1, 1).Divide(ComplexNumber.Zero);
        }

        [TestMethod]
        public void Equality_UsesBothComponentsAndMatchingHashCodes()
        {
            var left = new ComplexNumber(1, -1);
            var same = new ComplexNumber(1, -1);
            var different = new ComplexNumber(1, 1);

            Assert.AreEqual(left, same);
            Assert.AreEqual(left.GetHashCode(), same.GetHashCode());
            Assert.AreNotEqual(left, different);
            Assert.AreEqual("(1 - 1i)", left.ToString());
        }
    }
}
