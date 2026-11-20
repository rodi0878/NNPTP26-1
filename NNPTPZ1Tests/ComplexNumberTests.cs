using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass]
    public class ComplexNumberTests
    {
        private const double Delta = 1e-9;

        [TestMethod]
        public void Add_ReturnsSumOfParts()
        {
            ComplexNumber a = new ComplexNumber(10, 20);
            ComplexNumber b = new ComplexNumber(1, 2);

            Assert.AreEqual(new ComplexNumber(11, 22), a.Add(b));
        }

        [TestMethod]
        public void Add_Zero_ReturnsSameValue()
        {
            ComplexNumber a = new ComplexNumber(1, -1);

            Assert.AreEqual(new ComplexNumber(1, -1), a.Add(ComplexNumber.Zero));
        }

        [TestMethod]
        public void Subtract_ReturnsDifferenceOfParts()
        {
            ComplexNumber a = new ComplexNumber(10, 20);
            ComplexNumber b = new ComplexNumber(1, 2);

            Assert.AreEqual(new ComplexNumber(9, 18), a.Subtract(b));
        }

        [TestMethod]
        public void Multiply_ReturnsProduct()
        {
            ComplexNumber a = new ComplexNumber(1, 2);
            ComplexNumber b = new ComplexNumber(3, 4);

            Assert.AreEqual(new ComplexNumber(-5, 10), a.Multiply(b));
        }

        [TestMethod]
        public void Divide_ReturnsQuotient()
        {
            ComplexNumber a = new ComplexNumber(1, 2);
            ComplexNumber b = new ComplexNumber(3, 4);

            ComplexNumber result = a.Divide(b);

            Assert.AreEqual(0.44, result.Real, Delta);
            Assert.AreEqual(0.08, result.Imaginary, Delta);
        }

        [TestMethod]
        public void GetAbsoluteValue_ReturnsMagnitude()
        {
            Assert.AreEqual(5, new ComplexNumber(3, 4).GetAbsoluteValue(), Delta);
        }

        [TestMethod]
        public void GetAngleInDegrees_ReturnsAngleInAllQuadrants()
        {
            Assert.AreEqual(45, new ComplexNumber(1, 1).GetAngleInDegrees(), Delta);
            Assert.AreEqual(90, new ComplexNumber(0, 1).GetAngleInDegrees(), Delta);
            Assert.AreEqual(180, new ComplexNumber(-1, 0).GetAngleInDegrees(), Delta);
            Assert.AreEqual(-135, new ComplexNumber(-1, -1).GetAngleInDegrees(), Delta);
        }

        [TestMethod]
        public void Equals_SameValues_AreEqualWithSameHashCode()
        {
            ComplexNumber a = new ComplexNumber(1.5, -2);
            ComplexNumber b = new ComplexNumber(1.5, -2);

            Assert.AreEqual(a, b);
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
            Assert.IsFalse(a.Equals("(1.5 + -2i)"));
        }

        [TestMethod]
        public void ToString_FormatsBothParts()
        {
            Assert.AreEqual("(10 + 20i)", new ComplexNumber(10, 20).ToString());
            Assert.AreEqual("(1 + -1i)", new ComplexNumber(1, -1).ToString());
            Assert.AreEqual("(0 + 0i)", ComplexNumber.Zero.ToString());
        }
    }
}
