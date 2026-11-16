using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    public class NewtonFractalRenderer
    {
        private static readonly Color[] DefaultColors = new Color[]
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow,
            Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        public FractalParameters Parameters { get; }
        public Polynomial Polynomial { get; }
        public Polynomial DerivedPolynomial { get; }
        public Color[] Colors { get; }

        public NewtonFractalRenderer(FractalParameters parameters, Polynomial polynomial, Color[] colors = null)
        {
            Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            Polynomial = polynomial ?? throw new ArgumentNullException(nameof(polynomial));
            DerivedPolynomial = polynomial.Derive();
            Colors = colors ?? DefaultColors;
        }

        // render whole fractal into bitmap
        public Bitmap Render()
        {
            Bitmap bitmap = new Bitmap(Parameters.Width, Parameters.Height);
            double xStep = (Parameters.XMax - Parameters.XMin) / Parameters.Width;
            double yStep = (Parameters.YMax - Parameters.YMin) / Parameters.Height;

            List<ComplexNumber> knownRoots = new List<ComplexNumber>();

            for (int y = 0; y < Parameters.Height; y++)
            {
                for (int x = 0; x < Parameters.Width; x++)
                {
                    // find world coords
                    double worldX = Parameters.XMin + x * xStep;
                    double worldY = Parameters.YMin + y * yStep;

                    ComplexNumber start = new ComplexNumber(worldX, worldY);
                    if (Math.Abs(start.Real) < 1e-12) start.Real = 0.0001;
                    if (Math.Abs(start.Imaginary) < 1e-12) start.Imaginary = 0.0001;

                    var iterationResult = PerformNewtonIteration(start);
                    int rootIndex = FindOrRegisterRoot(knownRoots, iterationResult.root);
                    Color pixelColor = CalculatePixelColor(rootIndex, iterationResult.iterations);

                    // set pixel with correct x and y order
                    bitmap.SetPixel(x, y, pixelColor);
                }
            }

            return bitmap;
        }

        // calculate root using newton formula
        private (ComplexNumber root, int iterations) PerformNewtonIteration(ComplexNumber initial)
        {
            ComplexNumber current = initial;
            int iterations = 0;
            const int maxSteps = 30;

            for (int step = 0; step < maxSteps; step++)
            {
                ComplexNumber pVal = Polynomial.Evaluate(current);
                ComplexNumber pdVal = DerivedPolynomial.Evaluate(current);

                double denom = pdVal.Real * pdVal.Real + pdVal.Imaginary * pdVal.Imaginary;
                // avoid zero derivative
                if (denom < 1e-14)
                {
                    break;
                }

                ComplexNumber diff = pVal.Divide(pdVal);
                current = current.Subtract(diff);

                if (diff.Real * diff.Real + diff.Imaginary * diff.Imaginary >= 0.5)
                {
                    // step backward if not converged enough
                    step--;
                }

                iterations++;
                if (iterations >= maxSteps * 2)
                {
                    // break to prevent infinite loop on divergence
                    break;
                }
            }

            return (current, iterations);
        }

        // match root to existing one or add new
        private int FindOrRegisterRoot(List<ComplexNumber> roots, ComplexNumber current)
        {
            for (int i = 0; i < roots.Count; i++)
            {
                double distSq = Math.Pow(current.Real - roots[i].Real, 2) + Math.Pow(current.Imaginary - roots[i].Imaginary, 2);
                if (distSq <= 0.01)
                {
                    return i;
                }
            }

            roots.Add(current);
            return roots.Count - 1;
        }

        private Color CalculatePixelColor(int rootIndex, int iterations)
        {
            Color baseColor = Colors[rootIndex % Colors.Length];
            int r = Math.Min(255, Math.Max(0, baseColor.R - iterations * 2));
            int g = Math.Min(255, Math.Max(0, baseColor.G - iterations * 2));
            int b = Math.Min(255, Math.Max(0, baseColor.B - iterations * 2));

            return Color.FromArgb(r, g, b);
        }
    }
}
