using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1.Fractal
{
    public class NewtonFractalRenderer
    {
        private const int ConvergedIterationsRequired = 30;
        private const double ConvergenceThreshold = 0.5;
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
            roots.Clear();
            var bitmap = new Bitmap(settings.Width, settings.Height);

            for (int row = 0; row < settings.Height; row++)
            {
                for (int column = 0; column < settings.Width; column++)
                {
                    ComplexNumber startPoint = ToComplexPlane(settings, column, row);
                    ComplexNumber root = FindRoot(startPoint, out int iterations);
                    int rootIndex = GetRootIndex(root);

                    bitmap.SetPixel(column, row, GetColor(rootIndex, iterations));
                }
            }

            return bitmap;
        }

        private static ComplexNumber ToComplexPlane(FractalSettings settings, int column, int row)
        {
            double x = settings.MinX + column * settings.StepX;
            double y = settings.MinY + row * settings.StepY;

            if (x == 0)
            {
                x = ZeroReplacement;
            }

            var imaginary = (float)y;
            if (imaginary == 0)
            {
                imaginary = (float)ZeroReplacement;
            }

            return new ComplexNumber(x, imaginary);
        }

        private ComplexNumber FindRoot(ComplexNumber point, out int iterations)
        {
            iterations = 0;
            int convergedIterations = 0;

            while (convergedIterations < ConvergedIterationsRequired)
            {
                ComplexNumber step = polynomial.Evaluate(point).Divide(derivative.Evaluate(point));
                point = point.Subtract(step);
                iterations++;

                if (step.SquaredMagnitude() < ConvergenceThreshold)
                {
                    convergedIterations++;
                }
            }

            return point;
        }

        private int GetRootIndex(ComplexNumber point)
        {
            for (int i = 0; i < roots.Count; i++)
            {
                if (point.SquaredDistanceTo(roots[i]) <= RootTolerance)
                {
                    return i;
                }
            }

            roots.Add(point);
            return roots.Count - 1;
        }

        private static Color GetColor(int rootIndex, int iterations)
        {
            Color baseColor = RootColors[rootIndex % RootColors.Length];
            int darkening = iterations * DarkeningPerIteration;

            return Color.FromArgb(
                Darken(baseColor.R, darkening),
                Darken(baseColor.G, darkening),
                Darken(baseColor.B, darkening));
        }

        private static int Darken(int channel, int amount)
        {
            return Math.Max(0, channel - amount);
        }
    }
}
