using System;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {

        static void Main(string[] args)
        {
            RenderOptions options = RenderOptions.Parse(args);
            Poly polynomial = CreatePolynomial();
            Poly derivative = polynomial.Derive();

            Console.WriteLine(polynomial);
            Console.WriteLine(derivative);

            var solver = new NewtonSolver(polynomial, derivative);
            var renderer = new NewtonFractalRenderer(options, solver);

            using (var bitmap = renderer.Render())
            {
                bitmap.Save(options.OutputPath ?? "../../../out.png");
            }
        }

        private static Poly CreatePolynomial()
        {
            Poly polynomial = new Poly();

            polynomial.Coe.Add(new Cplx() { Real = 1 });
            polynomial.Coe.Add(Cplx.Zero);
            polynomial.Coe.Add(Cplx.Zero);
            polynomial.Coe.Add(new Cplx() { Real = 1 });

            return polynomial;
        }
    }
}
