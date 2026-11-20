using System;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    /// <summary>
    /// This program produces Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            FractalSettings settings;
            try
            {
                settings = FractalSettings.Parse(args);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is FormatException || ex is OverflowException)
            {
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(FractalSettings.Usage);
                return;
            }

            Polynomial polynomial = CreatePolynomial();
            Console.WriteLine(polynomial);
            Console.WriteLine(polynomial.Derive());

            NewtonFractalRenderer renderer = new NewtonFractalRenderer(polynomial);
            using (Bitmap bitmap = renderer.Render(settings))
            {
                bitmap.Save(settings.OutputPath);
            }
        }

        /// <summary>
        /// Creates polynomial 1 + x^3.
        /// </summary>
        private static Polynomial CreatePolynomial()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Add(new ComplexNumber(1, 0));
            polynomial.Add(ComplexNumber.Zero);
            polynomial.Add(ComplexNumber.Zero);
            polynomial.Add(new ComplexNumber(1, 0));
            return polynomial;
        }
    }
}
