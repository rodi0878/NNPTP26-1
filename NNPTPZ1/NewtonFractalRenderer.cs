using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    public class NewtonFractalRenderer
    {
        private const int MaxIterations = 30;
        private const int MaxTotalIterations = 1000;
        private const double StepConvergenceThresholdSquared = 0.5;
        private const double RootDistanceToleranceSquared = 0.01;
        private const double MinimumCoordinateValue = 0.0001;
        private const int ColorDarkeningFactor = 2;
        private const int MaxColorComponentValue = 255;

        private static readonly Color[] RootColors =
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange,
            Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        private readonly Polynomial polynomial;
        private readonly Polynomial derivativePolynomial;
        private readonly List<ComplexNumber> roots;

        public NewtonFractalRenderer(Polynomial polynomial)
        {
            this.polynomial = polynomial ?? throw new ArgumentNullException(nameof(polynomial));
            derivativePolynomial = polynomial.Derive();
            roots = new List<ComplexNumber>();
        }

        public Bitmap Render(FractalSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            roots.Clear();
            Bitmap bitmap = new Bitmap(settings.Width, settings.Height);

            for (int y = 0; y < settings.Height; y++)
            {
                for (int x = 0; x < settings.Width; x++)
                {
                    Color pixelColor = CalculatePixelColor(settings, x, y);
                    bitmap.SetPixel(x, y, pixelColor);
                }
            }

            return bitmap;
        }

        private Color CalculatePixelColor(FractalSettings settings, int x, int y)
        {
            ComplexNumber point = MapPixelToComplexPlane(settings, x, y);
            int iterationCount = PerformNewtonIteration(ref point);
            int rootIndex = FindOrAddRootIndex(point);

            return ApplyIterationShading(rootIndex, iterationCount);
        }

        private static ComplexNumber MapPixelToComplexPlane(FractalSettings settings, int x, int y)
        {
            double real = settings.XMin + x * settings.XStep;
            double imaginary = settings.YMin + y * settings.YStep;

            if (real == 0)
            {
                real = MinimumCoordinateValue;
            }

            if (imaginary == 0)
            {
                imaginary = MinimumCoordinateValue;
            }

            return new ComplexNumber(real, imaginary);
        }

        private int PerformNewtonIteration(ref ComplexNumber point)
        {
            int iterationCount = 0;
            for (int i = 0; i < MaxIterations && iterationCount < MaxTotalIterations; i++)
            {
                ComplexNumber step = polynomial.Evaluate(point).Divide(derivativePolynomial.Evaluate(point));
                point = point.Subtract(step);

                if (GetSquaredMagnitude(step) >= StepConvergenceThresholdSquared)
                {
                    i--;
                }

                iterationCount++;
            }

            return iterationCount;
        }

        private int FindOrAddRootIndex(ComplexNumber point)
        {
            for (int i = 0; i < roots.Count; i++)
            {
                if (GetSquaredDistance(point, roots[i]) <= RootDistanceToleranceSquared)
                {
                    return i;
                }
            }

            roots.Add(point);
            return roots.Count - 1;
        }

        private static double GetSquaredMagnitude(ComplexNumber number)
        {
            return number.Real * number.Real + number.Imaginary * number.Imaginary;
        }

        private static double GetSquaredDistance(ComplexNumber first, ComplexNumber second)
        {
            double realDifference = first.Real - second.Real;
            double imaginaryDifference = first.Imaginary - second.Imaginary;

            return realDifference * realDifference + imaginaryDifference * imaginaryDifference;
        }

        private static Color ApplyIterationShading(int rootIndex, int iterationCount)
        {
            Color baseColor = RootColors[rootIndex % RootColors.Length];
            int colorOffset = iterationCount * ColorDarkeningFactor;

            int red = ClampColorComponent(baseColor.R - colorOffset);
            int green = ClampColorComponent(baseColor.G - colorOffset);
            int blue = ClampColorComponent(baseColor.B - colorOffset);

            return Color.FromArgb(red, green, blue);
        }

        private static int ClampColorComponent(int value)
        {
            return Math.Min(Math.Max(0, value), MaxColorComponentValue);
        }
    }
}
