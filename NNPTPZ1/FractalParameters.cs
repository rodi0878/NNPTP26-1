using System;
using System.Globalization;

namespace NNPTPZ1
{
    public class FractalParameters
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public double XMin { get; set; }
        public double XMax { get; set; }
        public double YMin { get; set; }
        public double YMax { get; set; }
        public string OutputPath { get; set; }

        public FractalParameters()
        {
        }

        public FractalParameters(int width, int height, double xMin, double xMax, double yMin, double yMax, string outputPath)
        {
            Width = width;
            Height = height;
            XMin = xMin;
            XMax = xMax;
            YMin = yMin;
            YMax = yMax;
            OutputPath = outputPath;
        }

        // parse and check console arguments
        public static FractalParameters Parse(string[] args)
        {
            if (args == null || args.Length < 7)
            {
                throw new ArgumentException("usage: <width> <height> <xmin> <xmax> <ymin> <ymax> <outputpath>");
            }

            if (!int.TryParse(args[0], out int width) || width <= 0)
            {
                throw new ArgumentException("width must be positive number");
            }

            if (!int.TryParse(args[1], out int height) || height <= 0)
            {
                throw new ArgumentException("height must be positive number");
            }

            // use invariant culture so dot work on any system
            CultureInfo culture = CultureInfo.InvariantCulture;

            if (!double.TryParse(args[2], NumberStyles.Float, culture, out double xMin) ||
                !double.TryParse(args[3], NumberStyles.Float, culture, out double xMax) ||
                !double.TryParse(args[4], NumberStyles.Float, culture, out double yMin) ||
                !double.TryParse(args[5], NumberStyles.Float, culture, out double yMax))
            {
                throw new ArgumentException("coordinates must be valid floating numbers");
            }

            if (xMin >= xMax)
            {
                throw new ArgumentException("xmin must be less then xmax");
            }

            if (yMin >= yMax)
            {
                throw new ArgumentException("ymin must be less then ymax");
            }

            string outputPath = args[6];
            if (string.IsNullOrWhiteSpace(outputPath))
            {
                throw new ArgumentException("output path can not be empty");
            }

            return new FractalParameters(width, height, xMin, xMax, yMin, yMax, outputPath);
        }
    }
}
