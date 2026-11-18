using System;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        /// <summary>
        /// Application entry point that parses arguments, renders fractal and saves bitmap.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        static void Main(string[] args)
        {
            try
            {
                FractalParameters parameters = FractalParameters.Parse(args);
                Poly polynomial = CreateDefaultPolynomial();
                FractalPainter painter = new FractalPainter();

                // Keep console output from the original program so users can see the selected polynomial.
                Console.WriteLine(polynomial);
                Console.WriteLine(polynomial.Derive());

                using (Bitmap bitmap = painter.Paint(parameters, polynomial))
                {
                    bitmap.Save(parameters.OutputPath);
                }
            }
            catch (Exception ex)
            {
                // Print a readable error instead of ending with an unhandled exception.
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Creates default polynomial used by the original fractal renderer.
        /// </summary>
        /// <returns>Polynomial x^3 + 1.</returns>
        private static Poly CreateDefaultPolynomial()
        {
            Poly polynomial = new Poly();

            // Coefficients are ordered from constant term to the highest power.
            polynomial.Coe.Add(new Cplx() { Re = 1 });
            polynomial.Coe.Add(Cplx.Zero);
            polynomial.Coe.Add(Cplx.Zero);
            polynomial.Coe.Add(new Cplx() { Re = 1 });

            return polynomial;
        }
    }
}
