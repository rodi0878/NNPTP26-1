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
        public void Render_ProducesBitmapWithExpectedSize()
        {
            var parameters = new FractalParameters(50, 40, -1.0, 1.0, -1.0, 1.0, "test.png");

            var poly = new Polynomial();
            poly.Add(new ComplexNumber(1, 0));
            poly.Add(ComplexNumber.Zero);
            poly.Add(ComplexNumber.Zero);
            poly.Add(new ComplexNumber(1, 0));

            var renderer = new NewtonFractalRenderer(parameters, poly);

            using (Bitmap bitmap = renderer.Render())
            {
                Assert.IsNotNull(bitmap);
                Assert.AreEqual(50, bitmap.Width);
                Assert.AreEqual(40, bitmap.Height);
            }
        }
    }
}
