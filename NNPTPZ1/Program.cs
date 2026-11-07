using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Linq.Expressions;
using System.Threading;
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
            int width = 300;
            int height = 300;
            double xmin = -1.5;
            double xmax = 1.5;
            double ymin = -1.5;
            double ymax = 1.5;
            string output = "../../../out.png";

            if (args.Length >= 7) 
            {
                width = int.Parse(args[0]);
                height = int.Parse(args[1]);
                xmin = double.Parse(args[2]);
                xmax = double.Parse(args[3]);
                ymin = double.Parse(args[4]);
                ymax = double.Parse(args[5]);
                output = args[6];
            }

            Bitmap bmp = new Bitmap(width,height);

            double xstep = (xmax - xmin) / width;
            double ystep = (ymax - ymin) / height;

            List<Complex> roots = new List<Complex>();

            Polynomial polynomial = new Polynomial();
            polynomial.Coefficients.Add(new Complex(1,0));
            polynomial.Coefficients.Add(Complex.Zero);
            polynomial.Coefficients.Add(Complex.Zero);
            polynomial.Coefficients.Add(new Complex(1,0));
            Polynomial derivedPolynomial = polynomial.Derive();

            Console.WriteLine(polynomial);
            Console.WriteLine(derivedPolynomial);

            var colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            // TODO: cleanup!!!
            // for every pixel in image...
            const int MaxIterations = 30;
            const double MinStepTolerance = 0.5;
            const double RootTolerance = 0.01;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    // find coordinates of pixel
                    double y = ymin + i * ystep;
                    double x = xmin + j * xstep;

                    if (x == 0) x = 0.0001;
                    if (y == 0) y = 0.0001f;

                    Complex complexNumber = new Complex(x,y);

                    int iterations = 0;
                    for (int k = 0; k < MaxIterations; k++)
                    {
                        var differencial = polynomial.Evaluate(complexNumber).Divide(derivedPolynomial.Evaluate(complexNumber));
                        complexNumber = complexNumber.Subtract(differencial);

                        double stepSizeSquared = differencial.Real * differencial.Real + differencial.Imaginary * differencial.Imaginary;

                        if (stepSizeSquared >= MinStepTolerance)
                        {
                            k--;
                        }
                        iterations++;
                    }

                    bool rootFound = false;
                    int rootId = 0;
                    for (int k = 0; k <roots.Count;k++)
                    {
                        double distanceSquared = Math.Pow(complexNumber.Real - roots[k].Real, 2) + Math.Pow(complexNumber.Imaginary - roots[k].Imaginary, 2);
                        if (distanceSquared <= RootTolerance)
                        {
                            rootFound = true;
                            rootId = k;
                            break;
                        }
                    }
                    if (!rootFound)
                    {
                        roots.Add(complexNumber);
                        rootId = roots.Count;
                    }

                    Color baseColor = colors[rootId % colors.Length];

                    int colorDecrement = iterations * 2;

                    int red = Math.Max(0, baseColor.R - colorDecrement);
                    int green = Math.Max(0, baseColor.G - colorDecrement);
                    int blue = Math.Max(0, baseColor.B - colorDecrement);

                    Color finalColor = Color.FromArgb(red, green, blue);
                    bmp.SetPixel(j, i, finalColor);
                }
            }
            
            bmp.Save(output ?? "../../../out.png");
        }
    }
}
