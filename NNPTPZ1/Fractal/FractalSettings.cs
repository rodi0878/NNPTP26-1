using System;
using System.Globalization;

namespace NNPTPZ1.Fractal
{
    public class FractalSettings
    {
        private const string DefaultOutputPath = "out.png";
        private const int RequiredArgumentCount = 6;

        public int Width { get; }
        public int Height { get; }
        public double MinX { get; }
        public double MaxX { get; }
        public double MinY { get; }
        public double MaxY { get; }
        public string OutputPath { get; }

        public double StepX => (MaxX - MinX) / Width;
        public double StepY => (MaxY - MinY) / Height;

        public FractalSettings(int width, int height, double minX, double maxX, double minY, double maxY, string outputPath)
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentException("Image width and height must be positive.");
            }

            Width = width;
            Height = height;
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
            OutputPath = outputPath;
        }

        public static FractalSettings Parse(string[] args)
        {
            if (args.Length < RequiredArgumentCount)
            {
                throw new ArgumentException("Usage: NNPTPZ1 <width> <height> <minX> <maxX> <minY> <maxY> [outputPath]");
            }

            return new FractalSettings(
                ParseInt(args[0]),
                ParseInt(args[1]),
                ParseDouble(args[2]),
                ParseDouble(args[3]),
                ParseDouble(args[4]),
                ParseDouble(args[5]),
                args.Length > RequiredArgumentCount ? args[6] : DefaultOutputPath);
        }

        private static int ParseInt(string value)
        {
            return int.Parse(value, CultureInfo.InvariantCulture);
        }

        private static double ParseDouble(string value)
        {
            return double.Parse(value, CultureInfo.InvariantCulture);
        }
    }
}
