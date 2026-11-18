using System;

namespace NNPTPZ1
{
    /// <summary>
    /// Holds command line parameters needed for Newton fractal rendering.
    /// </summary>
    public class FractalParameters
    {
        /// <summary>
        /// Gets image width in pixels.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets image height in pixels.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Gets minimum X coordinate in the complex plane.
        /// </summary>
        public double XMin { get; private set; }

        /// <summary>
        /// Gets maximum X coordinate in the complex plane.
        /// </summary>
        public double XMax { get; private set; }

        /// <summary>
        /// Gets minimum Y coordinate in the complex plane.
        /// </summary>
        public double YMin { get; private set; }

        /// <summary>
        /// Gets maximum Y coordinate in the complex plane.
        /// </summary>
        public double YMax { get; private set; }

        /// <summary>
        /// Gets output file path for the generated bitmap.
        /// </summary>
        public string OutputPath { get; private set; }

        /// <summary>
        /// Creates parameters from already parsed values.
        /// </summary>
        /// <param name="width">Image width in pixels.</param>
        /// <param name="height">Image height in pixels.</param>
        /// <param name="xMin">Minimum X coordinate.</param>
        /// <param name="xMax">Maximum X coordinate.</param>
        /// <param name="yMin">Minimum Y coordinate.</param>
        /// <param name="yMax">Maximum Y coordinate.</param>
        /// <param name="outputPath">Output file path.</param>
        public FractalParameters(int width, int height, double xMin, double xMax, double yMin, double yMax, string outputPath)
        {
            // Store validated values so rendering code does not need to parse raw arguments.
            Width = width;
            Height = height;
            XMin = xMin;
            XMax = xMax;
            YMin = yMin;
            YMax = yMax;
            OutputPath = outputPath;
        }

        /// <summary>
        /// Parses command line arguments into fractal parameters.
        /// </summary>
        /// <param name="args">Command line arguments in original application order.</param>
        /// <returns>Parsed fractal parameters.</returns>
        public static FractalParameters Parse(string[] args)
        {
            // Keep the original required argument order and allow the output path to stay optional.
            if (args.Length < 6)
            {
                throw new ArgumentException("Expected arguments: width height xmin xmax ymin ymax [output]");
            }

            int width = int.Parse(args[0]);
            int height = int.Parse(args[1]);
            double xMin = double.Parse(args[2]);
            double xMax = double.Parse(args[3]);
            double yMin = double.Parse(args[4]);
            double yMax = double.Parse(args[5]);
            string output = args.Length > 6 ? args[6] : "../../../out.png";

            // Validate image size early because Bitmap cannot be created with non-positive dimensions.
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentException("Width and height must be positive numbers.");
            }

            return new FractalParameters(width, height, xMin, xMax, yMin, yMax, output);
        }
    }
}
