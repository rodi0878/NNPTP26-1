using System;
using System.Globalization;

namespace NNPTPZ1
{
    /// <summary>Dimensions, coordinate bounds and destination for one image.</summary>
    public sealed class FractalOptions
    {
        private const int ExpectedArgumentCount = 7;

        public const string Usage =
            "Usage: NNPTPZ1 <width> <height> <xmin> <xmax> <ymin> <ymax> <output-file>";

        public int Width { get; private set; }
        public int Height { get; private set; }
        public double MinX { get; private set; }
        public double MaxX { get; private set; }
        public double MinY { get; private set; }
        public double MaxY { get; private set; }
        public string OutputPath { get; private set; }

        public FractalOptions(int width, int height, double minX, double maxX,
            double minY, double maxY, string outputPath)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException("width", "Image width must be positive.");
            if (height <= 0)
                throw new ArgumentOutOfRangeException("height", "Image height must be positive.");
            if (!IsFinite(minX) || !IsFinite(maxX) || minX >= maxX || !IsFinite(maxX - minX) ||
                !IsFinite(minY) || !IsFinite(maxY) || minY >= maxY || !IsFinite(maxY - minY))
                throw new ArgumentException("Coordinate bounds must be finite, properly ordered and have finite ranges.");
            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("Output path cannot be empty.", "outputPath");

            Width = width;
            Height = height;
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
            OutputPath = outputPath;
        }

        public static bool TryParse(string[] args, out FractalOptions options, out string error)
        {
            options = null;
            error = null;
            if (args == null || args.Length != ExpectedArgumentCount)
            {
                error = "Expected seven command-line arguments.";
                return false;
            }

            int width;
            int height;
            double minX;
            double maxX;
            double minY;
            double maxY;
            if (!int.TryParse(args[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out width) ||
                !int.TryParse(args[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out height) ||
                !double.TryParse(args[2], NumberStyles.Float, CultureInfo.InvariantCulture, out minX) ||
                !double.TryParse(args[3], NumberStyles.Float, CultureInfo.InvariantCulture, out maxX) ||
                !double.TryParse(args[4], NumberStyles.Float, CultureInfo.InvariantCulture, out minY) ||
                !double.TryParse(args[5], NumberStyles.Float, CultureInfo.InvariantCulture, out maxY))
            {
                error = "Invalid numeric argument. Use a dot as the decimal separator.";
                return false;
            }

            try
            {
                options = new FractalOptions(width, height, minX, maxX, minY, maxY, args[6]);
                return true;
            }
            catch (ArgumentException exception)
            {
                error = exception.Message;
                return false;
            }
        }

        private static bool IsFinite(double number)
        {
            return !double.IsNaN(number) && !double.IsInfinity(number);
        }
    }
}
