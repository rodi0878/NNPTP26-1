using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    /// <summary>
    /// Paints Newton fractal bitmap from parsed parameters and polynomial.
    /// </summary>
    public class FractalPainter
    {
        private static readonly Color[] RootColors = new Color[]
        {
            Color.Red,
            Color.Blue,
            Color.Green,
            Color.Yellow,
            Color.Orange,
            Color.Fuchsia,
            Color.Gold,
            Color.Cyan,
            Color.Magenta
        };

        private const double RootMatchLimit = 0.01;

        /// <summary>
        /// Renders Newton fractal into a bitmap.
        /// </summary>
        /// <param name="parameters">Rendering parameters parsed from command line.</param>
        /// <param name="polynomial">Polynomial used for Newton root finding.</param>
        /// <returns>Generated bitmap.</returns>
        public Bitmap Paint(FractalParameters parameters, Poly polynomial)
        {
            Bitmap bitmap = new Bitmap(parameters.Width, parameters.Height);
            NewtonRootFinder rootFinder = new NewtonRootFinder(polynomial);
            List<Cplx> roots = new List<Cplx>();

            double xStep = (parameters.XMax - parameters.XMin) / parameters.Width;
            double yStep = (parameters.YMax - parameters.YMin) / parameters.Height;

            // Iterate by image coordinates so rectangular bitmaps do not swap width and height.
            for (int pixelY = 0; pixelY < parameters.Height; pixelY++)
            {
                for (int pixelX = 0; pixelX < parameters.Width; pixelX++)
                {
                    Cplx start = CreateStartPoint(parameters, xStep, yStep, pixelX, pixelY);
                    NewtonRootResult result = rootFinder.FindRoot(start);
                    int rootId = GetRootId(roots, result.Root);
                    Color color = GetRootColor(rootId, result.Iterations);

                    bitmap.SetPixel(pixelX, pixelY, color);
                }
            }

            return bitmap;
        }

        /// <summary>
        /// Creates starting complex point for selected bitmap pixel.
        /// </summary>
        /// <param name="parameters">Rendering parameters with coordinate bounds.</param>
        /// <param name="xStep">Step between neighbouring pixels on X axis.</param>
        /// <param name="yStep">Step between neighbouring pixels on Y axis.</param>
        /// <param name="pixelX">Pixel X coordinate.</param>
        /// <param name="pixelY">Pixel Y coordinate.</param>
        /// <returns>Complex starting point for Newton iteration.</returns>
        private static Cplx CreateStartPoint(FractalParameters parameters, double xStep, double yStep, int pixelX, int pixelY)
        {
            // Map pixel coordinates into the selected rectangle in the complex plane.
            return new Cplx()
            {
                Re = parameters.XMin + pixelX * xStep,
                Imaginari = (float)(parameters.YMin + pixelY * yStep)
            };
        }

        /// <summary>
        /// Gets stable root index and registers new root when needed.
        /// </summary>
        /// <param name="roots">Already known roots.</param>
        /// <param name="root">Root found for current pixel.</param>
        /// <returns>Zero-based root index.</returns>
        private static int GetRootId(List<Cplx> roots, Cplx root)
        {
            // Reuse existing root index when the new root is close enough to a known root.
            for (int i = 0; i < roots.Count; i++)
            {
                if (NewtonRootFinder.GetSquaredDistance(root, roots[i]) <= RootMatchLimit)
                {
                    return i;
                }
            }

            roots.Add(root);

            return roots.Count - 1;
        }

        /// <summary>
        /// Calculates pixel color from root index and number of iterations.
        /// </summary>
        /// <param name="rootId">Zero-based root index.</param>
        /// <param name="iterations">Number of Newton iterations.</param>
        /// <returns>Color for rendered pixel.</returns>
        private static Color GetRootColor(int rootId, int iterations)
        {
            Color rootColor = RootColors[rootId % RootColors.Length];

            // Darken base root color by iteration count to keep the original visual effect.
            return Color.FromArgb(
                ClampColor(rootColor.R - iterations * 2),
                ClampColor(rootColor.G - iterations * 2),
                ClampColor(rootColor.B - iterations * 2));
        }

        /// <summary>
        /// Keeps color channel value inside valid byte range.
        /// </summary>
        /// <param name="value">Calculated color channel value.</param>
        /// <returns>Value clamped to 0..255 range.</returns>
        private static int ClampColor(int value)
        {
            // System.Drawing.Color expects each channel to be between 0 and 255.
            return Math.Min(Math.Max(0, value), 255);
        }
    }
}
