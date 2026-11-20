using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass]
    public class ComplexNumberTests
    {
        [TestMethod]
        public void Add_ReturnsSumOfParts()
        {
            var a = new ComplexNumber(10, 20);
            var b = new ComplexNumber(1, 2);

            Assert.AreEqual(new ComplexNumber(11, 22), a.Add(b));
            Assert.AreEqual(new ComplexNumber(11, 22), a + b);
        }

        [TestMethod]
        public void Add_Zero_ReturnsSameNumber()
        {
            var a = new ComplexNumber(1, -1);

            Assert.AreEqual(new ComplexNumber(1, -1), a.Add(ComplexNumber.Zero));
        }

        [TestMethod]
        public void Subtract_ReturnsDifferenceOfParts()
        {
            var a = new ComplexNumber(10, 20);
            var b = new ComplexNumber(1, 2);

            Assert.AreEqual(new ComplexNumber(9, 18), a - b);
        }

        [TestMethod]
        public void Multiply_ReturnsProduct()
        {
            var a = new ComplexNumber(1, 2);
            var b = new ComplexNumber(3, 4);

            Assert.AreEqual(new ComplexNumber(-5, 10), a * b);
        }

        [TestMethod]
        public void Divide_ReturnsQuotient()
        {
            var a = new ComplexNumber(-5, 10);
            var b = new ComplexNumber(3, 4);

            Assert.AreEqual(new ComplexNumber(1, 2), a / b);
        }

        [TestMethod]
        public void Magnitude_ReturnsAbsoluteValue()
        {
            Assert.AreEqual(5.0, new ComplexNumber(3, 4).Magnitude(), 1e-12);
        }

        [TestMethod]
        public void Equals_SameValues_HaveSameHashCode()
        {
            var a = new ComplexNumber(1.5, -2);
            var b = new ComplexNumber(1.5, -2);

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
            Assert.IsFalse(a.Equals(null));
        }

        [TestMethod]
        public void ToString_FormatsRealAndImaginaryPart()
        {
            Assert.AreEqual("(10 + 20i)", new ComplexNumber(10, 20).ToString());
            Assert.AreEqual("(1 + 2i)", new ComplexNumber(1, 2).ToString());
            Assert.AreEqual("(1 + -1i)", new ComplexNumber(1, -1).ToString());
            Assert.AreEqual("(0 + 0i)", ComplexNumber.Zero.ToString());
        }
    }
}
