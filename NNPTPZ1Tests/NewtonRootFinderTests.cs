using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1.Mathematics.Tests
{
    /// <summary>
    /// Tests Newton root finding logic.
    /// </summary>
    [TestClass()]
    public class NewtonRootFinderTests
    {
        /// <summary>
        /// Verifies that Newton method finds the real root of the default cubic polynomial.
        /// </summary>
        [TestMethod()]
        public void FindRootFindsDefaultPolynomialRealRoot()
        {
            Poly polynomial = new Poly();

            // Polynomial coefficients represent x^3 + 1, which is used by the renderer.
            polynomial.Coe.Add(new Cplx() { Re = 1 });
            polynomial.Coe.Add(Cplx.Zero);
            polynomial.Coe.Add(Cplx.Zero);
            polynomial.Coe.Add(new Cplx() { Re = 1 });

            NewtonRootFinder rootFinder = new NewtonRootFinder(polynomial);
            NewtonRootResult result = rootFinder.FindRoot(new Cplx() { Re = -2 });

            // Starting from the negative real axis should converge to the real root -1.
            Assert.IsTrue(System.Math.Abs(result.Root.Re + 1) < 0.0001);
            Assert.IsTrue(System.Math.Abs(result.Root.Imaginari) < 0.0001);
        }
    }
}
