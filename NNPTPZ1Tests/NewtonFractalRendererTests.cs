using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1Tests
{
    [TestClass]
    public class NewtonFractalRendererTests
    {
        [TestMethod]
        public void Render_UsesCorrectDimensionsForNonSquareImages()
        {
            var polynomial = new Polynomial(
                new ComplexNumber(1, 0), new ComplexNumber(1, 0));
            var renderer = new NewtonFractalRenderer(polynomial);
            var options = new FractalOptions(7, 3, -2, 2, -1, 1, "unused.png");

            using (Bitmap image = renderer.Render(options))
            {
                Assert.AreEqual(7, image.Width);
                Assert.AreEqual(3, image.Height);
                Assert.AreNotEqual(Color.Black.ToArgb(), image.GetPixel(0, 0).ToArgb());
            }
        }

        [TestMethod]
        public void Render_RecognizesRepeatedRootWhenDerivativeIsZero()
        {
            // (z - (1 + i))^2 has a root at z = 1 + i, where its derivative is zero.
            var polynomial = new Polynomial(
                new ComplexNumber(0, 2), new ComplexNumber(-2, -2),
                new ComplexNumber(1, 0));
            var renderer = new NewtonFractalRenderer(polynomial);
            var options = new FractalOptions(1, 1, 1, 2, 1, 2, "unused.png");

            using (Bitmap image = renderer.Render(options))
                Assert.AreNotEqual(Color.Black.ToArgb(), image.GetPixel(0, 0).ToArgb());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_RejectsConstantPolynomial()
        {
            new NewtonFractalRenderer(new Polynomial(new ComplexNumber(1, 0)));
        }
    }
}
