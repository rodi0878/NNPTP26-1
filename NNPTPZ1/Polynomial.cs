using System.Collections.Generic;

namespace NNPTPZ1.Mathematics
{

    public class Polynomial
    {
        /// <summary>
        /// Coefficients
        /// </summary>
        public List<Complex> Coefficients { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Polynomial() => Coefficients = new List<Complex>();

        public void Add(Complex coefficient) =>
            Coefficients.Add(coefficient);

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynomial Derive()
        {
            Polynomial polynomial = new Polynomial();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                polynomial.Coefficients.Add(Coefficients[i].Multiply(new Complex(i, 0)));
            }

            return polynomial;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="complexNumber">point of evaluation</param>
        /// <returns>result as evaluation</returns>
        public Complex Evaluate(Complex complexNumber)
        {
            Complex result = Complex.Zero;
            Complex currentPowerOfX = new Complex(1, 0);

            for (int i = 0; i < Coefficients.Count; i++)
            {
                result = result.Add(Coefficients[i].Multiply(currentPowerOfX));
                currentPowerOfX = currentPowerOfX.Multiply(complexNumber);
            }

            return result;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String representation of polynomial</returns>
        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < Coefficients.Count; i++)
            {
                result += Coefficients[i];
                if (i == 1) result += "x";
                else if (i > 1) result += $"x^{i}";

                if ((i + 1) < Coefficients.Count)
                {
                    result += " + ";
                }
            }
            return result;
        }
    }
}
