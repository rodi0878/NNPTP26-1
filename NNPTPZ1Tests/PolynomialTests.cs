using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass]
    public class PolynomialTests
    {
        private static Polynomial CreateOnePlusXSquared()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Add(new ComplexNumber(1, 0));
            polynomial.Add(new ComplexNumber(0, 0));
            polynomial.Add(new ComplexNumber(1, 0));
            return polynomial;
        }

        [TestMethod]
        public void Evaluate_RealPoints_ReturnsValues()
        {
            Polynomial polynomial = CreateOnePlusXSquared();

            Assert.AreEqual(new ComplexNumber(1, 0), polynomial.Evaluate(new ComplexNumber(0, 0)));
            Assert.AreEqual(new ComplexNumber(2, 0), polynomial.Evaluate(new ComplexNumber(1, 0)));
            Assert.AreEqual(new ComplexNumber(5, 0), polynomial.Evaluate(new ComplexNumber(2, 0)));
            Assert.AreEqual(new ComplexNumber(5, 0), polynomial.Evaluate(2));
        }

        [TestMethod]
        public void Evaluate_Root_ReturnsZero()
        {
            Polynomial polynomial = CreateOnePlusXSquared();

            Assert.AreEqual(ComplexNumber.Zero, polynomial.Evaluate(new ComplexNumber(0, 1)));
        }

        [TestMethod]
        public void Derive_ReturnsDerivative()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Add(new ComplexNumber(1, 0));
            polynomial.Add(ComplexNumber.Zero);
            polynomial.Add(ComplexNumber.Zero);
            polynomial.Add(new ComplexNumber(1, 0));

            Polynomial derivative = polynomial.Derive();

            CollectionAssert.AreEqual(
                new[] { ComplexNumber.Zero, ComplexNumber.Zero, new ComplexNumber(3, 0) },
                derivative.Coefficients);
        }

        [TestMethod]
        public void ToString_FormatsTerms()
        {
            Assert.AreEqual("(1 + 0i) + (0 + 0i)x + (1 + 0i)xx", CreateOnePlusXSquared().ToString());
        }
    }
}
