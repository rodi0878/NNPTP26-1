using System;
using System.IO;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            FractalOptions options;
            string error;
            if (!FractalOptions.TryParse(args, out options, out error))
            {
                Console.Error.WriteLine(error);
                Console.Error.WriteLine(FractalOptions.Usage);
                return 1;
            }

            // f(z) = z^3 + 1 (the same polynomial as in the original program).
            var polynomial = new Polynomial(
                new ComplexNumber(1, 0),
                ComplexNumber.Zero,
                ComplexNumber.Zero,
                new ComplexNumber(1, 0));

            try
            {
                Console.WriteLine(polynomial);
                Console.WriteLine(polynomial.Derivative());

                var renderer = new NewtonFractalRenderer(polynomial);
                using (var image = renderer.Render(options))
                {
                    image.Save(options.OutputPath);
                }
                return 0;
            }
            catch (Exception exception) when (
                exception is IOException ||
                exception is UnauthorizedAccessException ||
                exception is ArgumentException ||
                exception is System.Runtime.InteropServices.ExternalException)
            {
                Console.Error.WriteLine("Cannot create fractal image: " + exception.Message);
                return 2;
            }
        }
    }
}
