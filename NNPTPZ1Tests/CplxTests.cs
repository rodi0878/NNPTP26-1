using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass]
    public class CplxTests
    {
        [TestMethod]
        public void AddTest()
        {
            ComplexNumber a = new ComplexNumber
            {
                Real = 10,
                Imaginary = 20
            };
            ComplexNumber b = new ComplexNumber
            {
                Real = 1,
                Imaginary = 2
            };

            ComplexNumber actual = a.Add(b);
            ComplexNumber shouldBe = new ComplexNumber
            {
                Real = 11,
                Imaginary = 22
            };

            Assert.AreEqual(shouldBe, actual);

            string expectedString = "(10 + 20i)";
            string actualString = a.ToString();
            Assert.AreEqual(expectedString, actualString);
            expectedString = "(1 + 2i)";
            actualString = b.ToString();
            Assert.AreEqual(expectedString, actualString);

            a = new ComplexNumber
            {
                Real = 1,
                Imaginary = -1
            };
            b = new ComplexNumber { Real = 0, Imaginary = 0 };
            shouldBe = new ComplexNumber { Real = 1, Imaginary = -1 };
            actual = a.Add(b);
            Assert.AreEqual(shouldBe, actual);

            expectedString = "(1 + -1i)";
            actualString = a.ToString();
            Assert.AreEqual(expectedString, actualString);

            expectedString = "(0 + 0i)";
            actualString = b.ToString();
            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void SubtractMultiplyDivideTest()
        {
            ComplexNumber a = new ComplexNumber(3, 4);
            ComplexNumber b = new ComplexNumber(1, -2);

            Assert.AreEqual(new ComplexNumber(2, 6), a.Subtract(b));
            Assert.AreEqual(new ComplexNumber(11, -2), a.Multiply(b));
            Assert.AreEqual(new ComplexNumber(-1, 2), a.Divide(b));
            Assert.AreEqual(5.0, a.GetAbsoluteValue(), 1e-10);
        }

        [TestMethod]
        public void AngleAndEqualityTest()
        {
            ComplexNumber number = new ComplexNumber(1, 1);
            Assert.AreEqual(Math.PI / 4.0, number.GetAngleInRadians(), 1e-10);
            Assert.AreEqual(45.0, number.GetAngleInDegrees(), 1e-10);

            ComplexNumber duplicate = new ComplexNumber(1, 1);
            Assert.AreEqual(number, duplicate);
            Assert.AreEqual(number.GetHashCode(), duplicate.GetHashCode());
            Assert.IsFalse(number.Equals(null));
        }

        [TestMethod]
        public void AddTestPolynome()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Coefficients.Add(new ComplexNumber { Real = 1, Imaginary = 0 });
            polynomial.Coefficients.Add(new ComplexNumber { Real = 0, Imaginary = 0 });
            polynomial.Coefficients.Add(new ComplexNumber { Real = 1, Imaginary = 0 });

            ComplexNumber result = polynomial.Evaluate(new ComplexNumber { Real = 0, Imaginary = 0 });
            ComplexNumber expected = new ComplexNumber { Real = 1, Imaginary = 0 };
            Assert.AreEqual(expected, result);

            result = polynomial.Evaluate(new ComplexNumber { Real = 1, Imaginary = 0 });
            expected = new ComplexNumber { Real = 2, Imaginary = 0 };
            Assert.AreEqual(expected, result);

            result = polynomial.Evaluate(new ComplexNumber { Real = 2, Imaginary = 0 });
            expected = new ComplexNumber { Real = 5.0000000000, Imaginary = 0 };
            Assert.AreEqual(expected, result);

            Assert.AreEqual(expected, polynomial.Evaluate(2.0));

            string actualString = polynomial.ToString();
            string expectedString = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void PolynomialDeriveTest()
        {
            Polynomial polynomial = new Polynomial(
                new ComplexNumber(1, 0),
                ComplexNumber.Zero,
                ComplexNumber.Zero,
                new ComplexNumber(1, 0));

            Polynomial derivative = polynomial.Derive();

            Assert.AreEqual(3, derivative.Coefficients.Count);
            Assert.AreEqual(ComplexNumber.Zero, derivative.Coefficients[0]);
            Assert.AreEqual(ComplexNumber.Zero, derivative.Coefficients[1]);
            Assert.AreEqual(new ComplexNumber(3, 0), derivative.Coefficients[2]);
        }

        [TestMethod]
        public void FractalSettingsFromArgumentsTest()
        {
            FractalSettings settings = FractalSettings.FromArguments(
                new[] { "300", "200", "-1.5", "1.5", "-1", "1", "output.png" });

            Assert.AreEqual(300, settings.Width);
            Assert.AreEqual(200, settings.Height);
            Assert.AreEqual(-1.5, settings.XMin, 1e-10);
            Assert.AreEqual(1.5, settings.XMax, 1e-10);
            Assert.AreEqual(-1.0, settings.YMin, 1e-10);
            Assert.AreEqual(1.0, settings.YMax, 1e-10);
            Assert.AreEqual("output.png", settings.OutputPath);
            Assert.AreEqual(0.01, settings.XStep, 1e-10);
            Assert.AreEqual(0.01, settings.YStep, 1e-10);
        }

        [TestMethod]
        public void RenderNonSquareBitmapTest()
        {
            Polynomial polynomial = new Polynomial(
                new ComplexNumber(1, 0),
                ComplexNumber.Zero,
                ComplexNumber.Zero,
                new ComplexNumber(1, 0));

            FractalSettings settings = new FractalSettings
            {
                Width = 10,
                Height = 6,
                XMin = -1.5,
                XMax = 1.5,
                YMin = -1.5,
                YMax = 1.5
            };

            NewtonFractalRenderer renderer = new NewtonFractalRenderer(polynomial);
            using (Bitmap bitmap = renderer.Render(settings))
            {
                Assert.AreEqual(10, bitmap.Width);
                Assert.AreEqual(6, bitmap.Height);
            }
        }
    }
}
