using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1Tests
{
    [TestClass]
    public class PolynomialTests
    {
        [TestMethod]
        public void Evaluate_ComputesPolynomialAtRealAndComplexPoints()
        {
            var polynomial = new Polynomial(
                new ComplexNumber(1, 0), ComplexNumber.Zero, new ComplexNumber(1, 0));

            Assert.AreEqual(new ComplexNumber(1, 0), polynomial.Evaluate(0));
            Assert.AreEqual(new ComplexNumber(2, 0), polynomial.Evaluate(1));
            Assert.AreEqual(new ComplexNumber(5, 0), polynomial.Evaluate(2));
            Assert.AreEqual(ComplexNumber.Zero, polynomial.Evaluate(new ComplexNumber(0, 1)));
        }

        [TestMethod]
        public void Derivative_ComputesEachPowerCorrectly()
        {
            var polynomial = new Polynomial(
                new ComplexNumber(1, 0), ComplexNumber.Zero,
                ComplexNumber.Zero, new ComplexNumber(1, 0));

            Assert.AreEqual(new ComplexNumber(12, 0), polynomial.Derivative().Evaluate(2));
        }

        [TestMethod]
        public void Derivative_OfConstantIsZero()
        {
            var constant = new Polynomial(new ComplexNumber(7, 0));
            Assert.AreEqual(ComplexNumber.Zero, constant.Derivative().Evaluate(4));
        }

        [TestMethod]
        public void Constructor_CopiesInputCoefficients()
        {
            var coefficients = new[] { new ComplexNumber(1, 0) };
            var polynomial = new Polynomial(coefficients);
            coefficients[0] = new ComplexNumber(99, 0);

            Assert.AreEqual(new ComplexNumber(1, 0), polynomial.Evaluate(3));
        }
    }
}
