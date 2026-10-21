using System;
using System.Globalization;

namespace NNPTPZ1
{
    public class FractalSettings
    {
        public const string DefaultOutputPath = "../../../out.png";

        private const int MinimumArgumentCount = 6;

        public int Width { get; set; }

        public int Height { get; set; }

        public double XMin { get; set; }

        public double XMax { get; set; }

        public double YMin { get; set; }

        public double YMax { get; set; }

        public string OutputPath { get; set; } = DefaultOutputPath;

        public double XStep => (XMax - XMin) / Width;

        public double YStep => (YMax - YMin) / Height;

        public static FractalSettings FromArguments(string[] args)
        {
            if (args == null || args.Length < MinimumArgumentCount)
            {
                throw new ArgumentException(
                    "Expected at least 6 arguments: <width> <height> <xmin> <xmax> <ymin> <ymax> [outputPath].");
            }

            string outputPath = args.Length > MinimumArgumentCount && !string.IsNullOrWhiteSpace(args[6])
                ? args[6]
                : DefaultOutputPath;

            return new FractalSettings
            {
                Width = ParsePositiveInt(args[0], nameof(Width)),
                Height = ParsePositiveInt(args[1], nameof(Height)),
                XMin = ParseDouble(args[2], nameof(XMin)),
                XMax = ParseDouble(args[3], nameof(XMax)),
                YMin = ParseDouble(args[4], nameof(YMin)),
                YMax = ParseDouble(args[5], nameof(YMax)),
                OutputPath = outputPath
            };
        }

        private static int ParsePositiveInt(string value, string parameterName)
        {
            if (!int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out int result) || result <= 0)
            {
                throw new ArgumentException($"Parameter '{parameterName}' must be a positive integer, got '{value}'.");
            }

            return result;
        }

        private static double ParseDouble(string value, string parameterName)
        {
            if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out double result) ||
                double.TryParse(value, NumberStyles.Float, CultureInfo.CurrentCulture, out result))
            {
                return result;
            }

            throw new ArgumentException($"Parameter '{parameterName}' must be a valid floating-point number, got '{value}'.");
        }
    }
}
