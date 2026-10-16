using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1Tests
{
    [TestClass]
    public class MathTests
    {
        [TestMethod]
        public void Render_RectangularImage_ReturnsRequestedSize()
        {
            var options = RenderOptions.Parse(new[]
            {
        "2",
        "3",
        "-1",
        "1",
        "-1",
        "1",
        "unused.png"
    });

            var polynomial = new Poly();
            polynomial.Add(new Cplx { Real = 1 });
            polynomial.Add(Cplx.Zero);
            polynomial.Add(Cplx.Zero);
            polynomial.Add(new Cplx { Real = 1 });

            var solver = new NewtonSolver(
                polynomial,
                polynomial.Derive());

            var renderer = new NewtonFractalRenderer(options, solver);

            using (var bitmap = renderer.Render())
            {
                Assert.AreEqual(2, bitmap.Width);
                Assert.AreEqual(3, bitmap.Height);
            }
        }

        [TestMethod]
        public void Multiply_ReturnsExpectedComplexNumber()
        {
            var first = new Cplx { Real = 1, Imaginary = 2 };
            var second = new Cplx { Real = 3, Imaginary = 4 };
            var expected = new Cplx { Real = -5, Imaginary = 10 };

            Assert.AreEqual(expected, first.Multiply(second));
        }

        [TestMethod]
        public void Subtract_ReturnsExpectedComplexNumber()
        {
            var first = new Cplx { Real = 1, Imaginary = 2 };
            var second = new Cplx { Real = 3, Imaginary = 4 };
            var expected = new Cplx { Real = -2, Imaginary = -2 };

            Assert.AreEqual(expected, first.Subtract(second));
        }

        [TestMethod]
        public void Derive_ReturnsExpectedPolynomial()
        {
            var polynomial = new Poly();
            polynomial.Add(new Cplx { Real = 1 });
            polynomial.Add(new Cplx { Real = 2 });
            polynomial.Add(new Cplx { Real = 3 });

            Poly derivative = polynomial.Derive();

            Assert.AreEqual(
                "(2 + 0i) + (6 + 0i)x",
                derivative.ToString());
        }

        [TestMethod]
        public void Derive_ConstantPolynomialHasNoCoefficients()
        {
            var polynomial = new Poly();
            polynomial.Add(new Cplx { Real = 7 });

            Assert.AreEqual(0, polynomial.Derive().Coe.Count);
        }

        [TestMethod]
        public void Eval_ImaginaryPointReturnsZeroForOnePlusXSquared()
        {
            var polynomial = new Poly();
            polynomial.Add(new Cplx { Real = 1 });
            polynomial.Add(Cplx.Zero);
            polynomial.Add(new Cplx { Real = 1 });

            Cplx actual = polynomial.Eval(new Cplx { Imaginary = 1 });

            Assert.AreEqual(Cplx.Zero, actual);
        }
    }
}