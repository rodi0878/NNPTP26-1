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

            var polynomial = new Polynomial();
            polynomial.Add(new ComplexNumber { Real = 1 });
            polynomial.Add(ComplexNumber.Zero);
            polynomial.Add(ComplexNumber.Zero);
            polynomial.Add(new ComplexNumber { Real = 1 });

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
            var first = new ComplexNumber { Real = 1, Imaginary = 2 };
            var second = new ComplexNumber { Real = 3, Imaginary = 4 };
            var expected = new ComplexNumber { Real = -5, Imaginary = 10 };

            Assert.AreEqual(expected, first.Multiply(second));
        }

        [TestMethod]
        public void Subtract_ReturnsExpectedComplexNumber()
        {
            var first = new ComplexNumber { Real = 1, Imaginary = 2 };
            var second = new ComplexNumber { Real = 3, Imaginary = 4 };
            var expected = new ComplexNumber { Real = -2, Imaginary = -2 };

            Assert.AreEqual(expected, first.Subtract(second));
        }

        [TestMethod]
        public void Derive_ReturnsExpectedPolynomial()
        {
            var polynomial = new Polynomial();
            polynomial.Add(new ComplexNumber { Real = 1 });
            polynomial.Add(new ComplexNumber { Real = 2 });
            polynomial.Add(new ComplexNumber { Real = 3 });

            Polynomial derivative = polynomial.Derive();

            Assert.AreEqual(
                "(2 + 0i) + (6 + 0i)x",
                derivative.ToString());
        }

        [TestMethod]
        public void Derive_ConstantPolynomialHasNoCoefficients()
        {
            var polynomial = new Polynomial();
            polynomial.Add(new ComplexNumber { Real = 7 });

            Assert.AreEqual(0, polynomial.Derive().Coefficients.Count);
        }

        [TestMethod]
        public void Eval_ImaginaryPointReturnsZeroForOnePlusXSquared()
        {
            var polynomial = new Polynomial();
            polynomial.Add(new ComplexNumber { Real = 1 });
            polynomial.Add(ComplexNumber.Zero);
            polynomial.Add(new ComplexNumber { Real = 1 });

            ComplexNumber actual = polynomial.Eval(new ComplexNumber { Imaginary = 1 });

            Assert.AreEqual(ComplexNumber.Zero, actual);
        }
    }
}