using System;
using System.Drawing.Imaging;
using System.IO;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                FractalParameters parameters = FractalParameters.Parse(args);

                // default newton fractal poly f(z) = z^3 + 1
                Polynomial polynomial = new Polynomial();
                polynomial.Add(new ComplexNumber(1, 0));
                polynomial.Add(ComplexNumber.Zero);
                polynomial.Add(ComplexNumber.Zero);
                polynomial.Add(new ComplexNumber(1, 0));

                Console.WriteLine(polynomial);
                Console.WriteLine(polynomial.Derive());

                NewtonFractalRenderer renderer = new NewtonFractalRenderer(parameters, polynomial);

                using (var image = renderer.Render())
                {
                    string outputDir = Path.GetDirectoryName(parameters.OutputPath);
                    if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                    {
                        Directory.CreateDirectory(outputDir);
                    }

                    image.Save(parameters.OutputPath, ImageFormat.Png);
                }

                Console.WriteLine("fractal generated at: " + parameters.OutputPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("error: " + ex.Message);
            }
        }
    }
}
