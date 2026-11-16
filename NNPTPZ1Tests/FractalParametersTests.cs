using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1;

namespace NNPTPZ1Tests
{
    [TestClass]
    public class FractalParametersTests
    {
        [TestMethod]
        public void Parse_ValidArguments_PopulatesAllFields()
        {
            string[] args = new string[] { "800", "600", "-2.0", "2.0", "-1.5", "1.5", "out.png" };

            var parameters = FractalParameters.Parse(args);

            Assert.AreEqual(800, parameters.Width);
            Assert.AreEqual(600, parameters.Height);
            Assert.AreEqual(-2.0, parameters.XMin, 1e-9);
            Assert.AreEqual(2.0, parameters.XMax, 1e-9);
            Assert.AreEqual(-1.5, parameters.YMin, 1e-9);
            Assert.AreEqual(1.5, parameters.YMax, 1e-9);
            Assert.AreEqual("out.png", parameters.OutputPath);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_TooFewArguments_ThrowsException()
        {
            string[] args = new string[] { "800", "600" };

            FractalParameters.Parse(args);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_InvalidDimension_ThrowsException()
        {
            string[] args = new string[] { "-100", "600", "-2", "2", "-1", "1", "out.png" };

            FractalParameters.Parse(args);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_InvalidBounds_ThrowsException()
        {
            // xmin is greater than xmax
            string[] args = new string[] { "800", "600", "2", "-2", "-1", "1", "out.png" };

            FractalParameters.Parse(args);
        }
    }
}
