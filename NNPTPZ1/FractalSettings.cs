using System;

namespace NNPTPZ1
{
    /// <summary>
    /// Image size, rendered area of the complex plane and output path.
    /// </summary>
    public class FractalSettings
    {
        private const int RequiredArgumentCount = 6;
        private const string DefaultOutputPath = "../../../out.png";

        public const string Usage =
            "Usage: NNPTPZ1 <width> <height> <xMin> <xMax> <yMin> <yMax> [outputPath]";

        public int Width { get; set; }
        public int Height { get; set; }
        public double XMin { get; set; }
        public double XMax { get; set; }
        public double YMin { get; set; }
        public double YMax { get; set; }
        public string OutputPath { get; set; }

        public double XStep => (XMax - XMin) / Width;
        public double YStep => (YMax - YMin) / Height;

        public static FractalSettings Parse(string[] args)
        {
            if (args == null || args.Length < RequiredArgumentCount)
            {
                throw new ArgumentException("Not enough arguments.");
            }

            return new FractalSettings
            {
                Width = int.Parse(args[0]),
                Height = int.Parse(args[1]),
                XMin = double.Parse(args[2]),
                XMax = double.Parse(args[3]),
                YMin = double.Parse(args[4]),
                YMax = double.Parse(args[5]),
                OutputPath = args.Length > RequiredArgumentCount ? args[RequiredArgumentCount] : DefaultOutputPath
            };
        }
    }
}
