using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1;

namespace NNPTPZ1Tests
{
    [TestClass]
    public class FractalOptionsTests
    {
        [TestMethod]
        public void TryParse_AcceptsValidArguments()
        {
            FractalOptions options;
            string error;
            bool success = FractalOptions.TryParse(
                new[] { "640", "480", "-2.5", "2.5", "-1.5", "1.5", "fractal.png" },
                out options, out error);

            Assert.IsTrue(success, error);
            Assert.AreEqual(640, options.Width);
            Assert.AreEqual(480, options.Height);
            Assert.AreEqual(-2.5, options.MinX);
            Assert.AreEqual("fractal.png", options.OutputPath);
        }

        [TestMethod]
        public void TryParse_RejectsMissingParameters()
        {
            AssertInvalid(new[] { "640", "480" });
        }

        [TestMethod]
        public void TryParse_RejectsBadDimensionsAndBounds()
        {
            AssertInvalid(new[] { "0", "10", "-1", "1", "-1", "1", "out.png" });
            AssertInvalid(new[] { "10", "-1", "-1", "1", "-1", "1", "out.png" });
            AssertInvalid(new[] { "10", "10", "1", "1", "-1", "1", "out.png" });
            AssertInvalid(new[] { "10", "10", "-1", "1", "2", "1", "out.png" });
            AssertInvalid(new[] { "10", "10", "NaN", "1", "-1", "1", "out.png" });
            AssertInvalid(new[] { "10", "10", "-1", "1", "-1", "1", "" });
        }

        [TestMethod]
        public void TryParse_RejectsOverflowingCoordinateRange()
        {
            AssertInvalid(new[] { "10", "10", "-1e308", "1e308", "-1", "1", "out.png" });
        }

        private static void AssertInvalid(string[] arguments)
        {
            FractalOptions options;
            string error;
            Assert.IsFalse(FractalOptions.TryParse(arguments, out options, out error));
            Assert.IsNull(options);
            Assert.IsFalse(string.IsNullOrWhiteSpace(error));
        }
    }
}
