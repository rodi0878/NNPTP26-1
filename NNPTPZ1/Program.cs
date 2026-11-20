using System;
using System.Drawing;
using NNPTPZ1.Fractal;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            FractalSettings settings = FractalSettings.Parse(args);

            var polynomial = new Polynomial(new[]
            {
                new ComplexNumber(1, 0),
                ComplexNumber.Zero,
                ComplexNumber.Zero,
                new ComplexNumber(1, 0)
            });

            Console.WriteLine(polynomial);
            Console.WriteLine(polynomial.Derive());

            var renderer = new NewtonFractalRenderer(polynomial);
            using (Bitmap bitmap = renderer.Render(settings))
            {
                bitmap.Save(settings.OutputPath);
            }
        }
    }
}
