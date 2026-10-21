using System.Collections.Generic;
using System.Text;

namespace NNPTPZ1
{

    namespace Mathematics
    {
        public class Polynomial
        {
            public List<ComplexNumber> Coefficients { get; set; }

            public Polynomial() => Coefficients = new List<ComplexNumber>();

            public void Add(ComplexNumber coef) =>
                Coefficients.Add(coef);

            /// <summary>
            /// Derives this polynomial and creates new one
            /// </summary>
            /// <returns>Derivated polynomial</returns>
            public Polynomial Derive()
            {
                Polynomial derivative = new Polynomial();
                for (int exponent = 1; exponent < Coefficients.Count; exponent++)
                {
                    derivative.Coefficients.Add(Coefficients[exponent].Multiply(new ComplexNumber() { Real = exponent }));
                }

                return derivative;
            }

            /// <summary>
            /// Evaluates polynomial at given point
            /// </summary>
            /// <param name="x">point of evaluation</param>
            /// <returns>y</returns>
            public ComplexNumber Eval(double x)
            {
                return Eval(new ComplexNumber { Real = x, Imaginary = 0 });
            }

            /// <summary>
            /// Evaluates polynomial at given point
            /// </summary>
            /// <param name="x">point of evaluation</param>
            /// <returns>y</returns>
            public ComplexNumber Eval(ComplexNumber x)
            {
                ComplexNumber sum = ComplexNumber.Zero;
                for (int i = 0; i < Coefficients.Count; i++)
                {
                    ComplexNumber coef = Coefficients[i];
                    ComplexNumber xPower = x;
                    int power = i;

                    if (i > 0)
                    {
                        for (int j = 0; j < power - 1; j++)
                            xPower = xPower.Multiply(x);

                        coef = coef.Multiply(xPower);
                    }

                    sum = sum.Add(coef);
                }

                return sum;
            }

            public override string ToString()
            {
                var result = new StringBuilder();

                for (int exponent = 0; exponent < Coefficients.Count; exponent++)
                {
                    if (exponent > 0)
                    {
                        result.Append(" + ");
                    }

                    result.Append(Coefficients[exponent]);
                    result.Append('x', exponent);
                }

                return result.ToString();
            }
        }
    }
}
