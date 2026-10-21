using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    internal sealed class NewtonFractalRenderer
    {
        private const double RootMatchSquaredDistance = 0.01;
        private const double ZeroRealOffset = 0.0001;
        private const float ZeroImaginaryOffset = 0.0001f;
        private const int DarkeningPerIteration = 2;
        private const int MaximumColorChannel = 255;

        private static readonly Color[] RootColors =
        {
            Color.Red, Color.Blue, Color.Green,
            Color.Yellow, Color.Orange, Color.Fuchsia,
            Color.Gold, Color.Cyan, Color.Magenta
        };

        private readonly RenderOptions options;
        private readonly NewtonSolver solver;
        private readonly List<ComplexNumber> roots = new List<ComplexNumber>();

        public NewtonFractalRenderer(
            RenderOptions options,
            NewtonSolver solver)
        {
            this.options = options;
            this.solver = solver;
        }

        public Bitmap Render()
        {
            roots.Clear();
            var bitmap = new Bitmap(options.Width, options.Height);

            try
            {
                RenderPixels(bitmap);
                return bitmap;
            }
            catch
            {
                bitmap.Dispose();
                throw;
            }
        }

        private void RenderPixels(Bitmap bitmap)
        {
            for (int row = 0; row < options.Height; row++)
            {
                for (int column = 0; column < options.Width; column++)
                {
                    bitmap.SetPixel(column, row, GetPixelColor(row, column));
                }
            }
        }

        private Color GetPixelColor(int row, int column)
        {
            NewtonResult result = solver.Solve(GetInitialPoint(row, column));
            int rootIndex = GetOrAddRootIndex(result.Point);
            Color rootColor = RootColors[rootIndex % RootColors.Length];

            return DarkenColor(
                rootColor,
                (int)result.IterationCount * DarkeningPerIteration);
        }

        private ComplexNumber GetInitialPoint(int row, int column)
        {
            double xStep = (options.XMax - options.XMin) / options.Width;
            double yStep = (options.YMax - options.YMin) / options.Height;

            double real = options.XMin + column * xStep;
            float imaginary = (float)(options.YMin + row * yStep);

            return new ComplexNumber
            {
                Real = real == 0 ? ZeroRealOffset : real,
                Imaginary = imaginary == 0 ? ZeroImaginaryOffset : imaginary
            };
        }

        private int GetOrAddRootIndex(ComplexNumber point)
        {
            int rootIndex = FindLastMatchingRoot(point);

            if (rootIndex >= 0)
            {
                return rootIndex;
            }

            roots.Add(point);

            // Preserve the original color index for a newly discovered root.
            return roots.Count;
        }

        private int FindLastMatchingRoot(ComplexNumber point)
        {
            int matchingIndex = -1;

            for (int index = 0; index < roots.Count; index++)
            {
                if (IsSameRoot(point, roots[index]))
                {
                    matchingIndex = index;
                }
            }

            return matchingIndex;
        }

        private static bool IsSameRoot(ComplexNumber first, ComplexNumber second)
        {
            double squaredDistance =
                Math.Pow(first.Real - second.Real, 2)
                + Math.Pow(first.Imaginary - second.Imaginary, 2);

            return squaredDistance <= RootMatchSquaredDistance;
        }

        private static Color DarkenColor(Color color, int amount)
        {
            return Color.FromArgb(
                ClampColorChannel(color.R - amount),
                ClampColorChannel(color.G - amount),
                ClampColorChannel(color.B - amount));
        }

        private static int ClampColorChannel(int value)
        {
            return Math.Min(Math.Max(0, value), MaximumColorChannel);
        }
    }
}