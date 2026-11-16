using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1Tests
{
    [TestClass]
    public class PolynomialTests
    {
        [TestMethod]
        public void Evaluate_HornerMethod_ComputesCorrectResult()
        {
            // poly: 1 + 2x + 3x^2
            var poly = new Polynomial();
            poly.Add(new ComplexNumber(1, 0));
            poly.Add(new ComplexNumber(2, 0));
            poly.Add(new ComplexNumber(3, 0));

            // at x = 2 -> 1 + 4 + 12 = 17
            var result = poly.Evaluate(2.0);

            Assert.AreEqual(new ComplexNumber(17, 0), result);
        }

        [TestMethod]
        public void Derive_CalculatesDerivativeProperly()
        {
            // poly: 1 + 0x + 0x^2 + 1x^3 -> derivative should be 0 + 0x + 3x^2
            var poly = new Polynomial();
            poly.Add(new ComplexNumber(1, 0));
            poly.Add(ComplexNumber.Zero);
            poly.Add(ComplexNumber.Zero);
            poly.Add(new ComplexNumber(1, 0));

            var derived = poly.Derive();

            // at x = 2 -> 3 * (2^2) = 12
            var result = derived.Evaluate(2.0);

            Assert.AreEqual(new ComplexNumber(12, 0), result);
        }

        [TestMethod]
        public void ToString_FormatsPolynomialTerms()
        {
            var poly = new Polynomial();
            poly.Add(new ComplexNumber(1, 0));
            poly.Add(new ComplexNumber(2, 0));
            poly.Add(new ComplexNumber(3, 0));

            Assert.AreEqual("(1 + 0i) + (2 + 0i)x + (3 + 0i)x^2", poly.ToString());
        }
    }
}
