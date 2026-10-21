using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    /// <summary>Computes each pixel of a Newton fractal and assigns a color per root.</summary>
    public sealed class NewtonFractalRenderer
    {
        private const int MaxIterations = 100;
        private const double ConvergenceToleranceSquared = 1e-12;
        private const double RootMatchToleranceSquared = 1e-4;
        private const int BrightnessLossPerIteration = 2;
        private const double ZeroCoordinateOffset = 0.0001;

        private static readonly Color[] RootColors =
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange,
            Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        private readonly Polynomial polynomial;
        private readonly Polynomial derivative;

        public NewtonFractalRenderer(Polynomial polynomial)
        {
            if (polynomial == null)
                throw new ArgumentNullException("polynomial");
            derivative = polynomial.Derivative();
            bool hasVariableTerm = false;
            foreach (ComplexNumber coefficient in derivative.Coefficients)
                hasVariableTerm |= !coefficient.Equals(ComplexNumber.Zero);

            if (!hasVariableTerm)
                throw new ArgumentException("Polynomial must not be constant.", "polynomial");

            this.polynomial = polynomial;
        }

        public Bitmap Render(FractalOptions options)
        {
            if (options == null)
                throw new ArgumentNullException("options");

            var image = new Bitmap(options.Width, options.Height);
            var knownRoots = new List<ComplexNumber>();
            double xStep = (options.MaxX - options.MinX) / options.Width;
            double yStep = (options.MaxY - options.MinY) / options.Height;

            try
            {
                for (int y = 0; y < options.Height; y++)
                {
                    for (int x = 0; x < options.Width; x++)
                    {
                        var point = new ComplexNumber(
                            AvoidZero(options.MinX + x * xStep),
                            AvoidZero(options.MinY + y * yStep));
                        var result = ApproximateRoot(point);
                        Color color = result.HasConverged
                            ? GetRootColor(result, knownRoots)
                            : Color.Black;
                        image.SetPixel(x, y, color);
                    }
                }
                return image;
            }
            catch
            {
                image.Dispose();
                throw;
            }
        }

        private NewtonResult ApproximateRoot(ComplexNumber point)
        {
            for (int iteration = 1; iteration <= MaxIterations; iteration++)
            {
                ComplexNumber slope = derivative.Evaluate(point);
                if (!slope.IsFinite || slope.SquaredMagnitude == 0)
                {
                    ComplexNumber value = polynomial.Evaluate(point);
                    if (value.IsFinite && value.SquaredMagnitude <= ConvergenceToleranceSquared)
                        return new NewtonResult(point, iteration - 1, true);
                    break;
                }

                ComplexNumber step = polynomial.Evaluate(point).Divide(slope);
                point = point.Subtract(step);
                if (!point.IsFinite || !step.IsFinite)
                    break;

                if (step.SquaredMagnitude <= ConvergenceToleranceSquared)
                    return new NewtonResult(point, iteration, true);
            }
            return new NewtonResult(point, MaxIterations, false);
        }

        private static Color GetRootColor(NewtonResult result, List<ComplexNumber> roots)
        {
            int rootIndex = FindRootIndex(result.Root, roots);
            Color baseColor = RootColors[rootIndex % RootColors.Length];
            int darkness = result.Iterations * BrightnessLossPerIteration;
            return Color.FromArgb(
                Math.Max(0, baseColor.R - darkness),
                Math.Max(0, baseColor.G - darkness),
                Math.Max(0, baseColor.B - darkness));
        }

        private static int FindRootIndex(ComplexNumber root, List<ComplexNumber> roots)
        {
            for (int index = 0; index < roots.Count; index++)
            {
                if (root.Subtract(roots[index]).SquaredMagnitude <= RootMatchToleranceSquared)
                    return index;
            }
            roots.Add(root);
            return roots.Count - 1;
        }

        private static double AvoidZero(double coordinate)
        {
            return coordinate == 0 ? ZeroCoordinateOffset : coordinate;
        }

        private struct NewtonResult
        {
            public ComplexNumber Root { get; private set; }
            public int Iterations { get; private set; }
            public bool HasConverged { get; private set; }

            public NewtonResult(ComplexNumber root, int iterations, bool hasConverged)
                : this()
            {
                Root = root;
                Iterations = iterations;
                HasConverged = hasConverged;
            }
        }
    }
}
