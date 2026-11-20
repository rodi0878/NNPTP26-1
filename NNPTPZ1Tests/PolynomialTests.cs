using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass]
    public class PolynomialTests
    {
        private static Polynomial CreateQuadratic()
        {
            return new Polynomial(new[]
            {
                new ComplexNumber(1, 0),
                ComplexNumber.Zero,
                new ComplexNumber(1, 0)
            });
        }

        [TestMethod]
        public void Evaluate_ReturnsValueAtPoint()
        {
            Polynomial polynomial = CreateQuadratic();

            Assert.AreEqual(new ComplexNumber(1, 0), polynomial.Evaluate(ComplexNumber.Zero));
            Assert.AreEqual(new ComplexNumber(2, 0), polynomial.Evaluate(new ComplexNumber(1, 0)));
            Assert.AreEqual(new ComplexNumber(5, 0), polynomial.Evaluate(new ComplexNumber(2, 0)));
            Assert.AreEqual(new ComplexNumber(5, 0), polynomial.Evaluate(2.0));
        }

        [TestMethod]
        public void Evaluate_AtRoot_ReturnsZero()
        {
            Assert.AreEqual(ComplexNumber.Zero, CreateQuadratic().Evaluate(new ComplexNumber(0, 1)));
        }

        [TestMethod]
        public void Derive_ReturnsDerivative()
        {
            Polynomial derivative = CreateQuadratic().Derive();

            Assert.AreEqual(2, derivative.Coefficients.Count);
            Assert.AreEqual(ComplexNumber.Zero, derivative.Coefficients[0]);
            Assert.AreEqual(new ComplexNumber(2, 0), derivative.Coefficients[1]);
        }

        [TestMethod]
        public void ToString_ListsTermsWithPowers()
        {
            Assert.AreEqual("(1 + 0i) + (0 + 0i)x + (1 + 0i)xx", CreateQuadratic().ToString());
        }
    }
}
