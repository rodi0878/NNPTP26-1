using System;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            FractalSettings settings = FractalSettings.FromArguments(args);
            Polynomial polynomial = CreateDefaultPolynomial();
            Polynomial derivativePolynomial = polynomial.Derive();

            Console.WriteLine(polynomial);
            Console.WriteLine(derivativePolynomial);

            NewtonFractalRenderer renderer = new NewtonFractalRenderer(polynomial);
            using (Bitmap bitmap = renderer.Render(settings))
            {
                bitmap.Save(settings.OutputPath);
            }
        }

        private static Polynomial CreateDefaultPolynomial()
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
