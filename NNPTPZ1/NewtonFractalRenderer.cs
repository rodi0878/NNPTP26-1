using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    public class NewtonFractalRenderer
    {
        private const int ConvergedStepsRequired = 30;
        private const double LargeStepThreshold = 0.5;
        private const double RootTolerance = 0.01;
        private const double ZeroReplacement = 0.0001;
        private const int DarkeningPerIteration = 2;

        private static readonly Color[] RootColors =
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange,
            Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        private readonly Polynomial polynomial;
        private readonly Polynomial derivative;
        private readonly List<ComplexNumber> roots = new List<ComplexNumber>();

        public NewtonFractalRenderer(Polynomial polynomial)
        {
            this.polynomial = polynomial;
            derivative = polynomial.Derive();
        }

        public Bitmap Render(FractalSettings settings)
        {
            Bitmap bitmap = new Bitmap(settings.Width, settings.Height);

            for (int pixelY = 0; pixelY < settings.Height; pixelY++)
            {
                for (int pixelX = 0; pixelX < settings.Width; pixelX++)
                {
                    ComplexNumber startPoint = GetWorldCoordinates(settings, pixelX, pixelY);
                    ComplexNumber root = FindRoot(startPoint, out int iterations);
                    int rootIndex = GetRootIndex(root);

                    bitmap.SetPixel(pixelX, pixelY, GetPixelColor(rootIndex, iterations));
                }
            }

            return bitmap;
        }

        private static ComplexNumber GetWorldCoordinates(FractalSettings settings, int pixelX, int pixelY)
        {
            ComplexNumber point = new ComplexNumber(
                settings.XMin + pixelX * settings.XStep,
                settings.YMin + pixelY * settings.YStep);

            if (point.Real == 0)
            {
                point.Real = ZeroReplacement;
            }
            if (point.Imaginary == 0)
            {
                point.Imaginary = ZeroReplacement;
            }

            return point;
        }

        private ComplexNumber FindRoot(ComplexNumber point, out int iterations)
        {
            iterations = 0;
            int convergedSteps = 0;
            while (convergedSteps < ConvergedStepsRequired)
            {
                ComplexNumber step = polynomial.Evaluate(point).Divide(derivative.Evaluate(point));
                point = point.Subtract(step);

                bool isLargeStep = SquaredDistance(step, ComplexNumber.Zero) >= LargeStepThreshold;
                if (!isLargeStep)
                {
                    convergedSteps++;
                }
                iterations++;
            }

            return point;
        }

        private int GetRootIndex(ComplexNumber point)
        {
            int index = 0;
            bool known = false;
            for (int i = 0; i < roots.Count; i++)
            {
                if (SquaredDistance(point, roots[i]) <= RootTolerance)
                {
                    known = true;
                    index = i;
                }
            }

            if (!known)
            {
                roots.Add(point);
                index = roots.Count - 1;
            }

            return index;
        }

        private static Color GetPixelColor(int rootIndex, int iterations)
        {
            Color baseColor = RootColors[rootIndex % RootColors.Length];
            int darkening = iterations * DarkeningPerIteration;

            return Color.FromArgb(
                ClampColorComponent(baseColor.R - darkening),
                ClampColorComponent(baseColor.G - darkening),
                ClampColorComponent(baseColor.B - darkening));
        }

        private static int ClampColorComponent(int value)
        {
            return Math.Min(Math.Max(0, value), 255);
        }

        private static double SquaredDistance(ComplexNumber a, ComplexNumber b)
        {
            return Math.Pow(a.Real - b.Real, 2) + Math.Pow(a.Imaginary - b.Imaginary, 2);
        }
    }
}
