using System.Collections.Generic;

namespace NNPTPZ1.Mathematics
{
    /// <summary>
    /// Represents a polynomial with complex coefficients.
    /// </summary>
    public class Poly
    {
        /// <summary>
        /// Gets or sets polynomial coefficients ordered by power from lowest to highest.
        /// </summary>
        public List<Cplx> Coe { get; set; }

        /// <summary>
        /// Initializes a new polynomial with empty coefficient list.
        /// </summary>
        public Poly()
        {
            // Keep coefficients mutable to preserve the original public API.
            Coe = new List<Cplx>();
        }

        /// <summary>
        /// Adds coefficient to the polynomial.
        /// </summary>
        /// <param name="coe">Coefficient to add.</param>
        public void Add(Cplx coe)
        {
            Coe.Add(coe);
        }

        /// <summary>
        /// Derives this polynomial and creates a new one.
        /// </summary>
        /// <returns>Derived polynomial.</returns>
        public Poly Derive()
        {
            Poly p = new Poly();

            // The derivative skips the constant term and multiplies each coefficient by its power.
            for (int q = 1; q < Coe.Count; q++)
            {
                p.Coe.Add(Coe[q].Multiply(new Cplx() { Re = q }));
            }

            return p;
        }

        /// <summary>
        /// Evaluates polynomial at a real point.
        /// </summary>
        /// <param name="x">Real point of evaluation.</param>
        /// <returns>Polynomial value at the selected point.</returns>
        public Cplx Eval(double x)
        {
            // Delegate real number evaluation to complex evaluation with zero imaginary part.
            var y = Eval(new Cplx() { Re = x, Imaginari = 0 });

            return y;
        }

        /// <summary>
        /// Evaluates polynomial at a complex point.
        /// </summary>
        /// <param name="x">Complex point of evaluation.</param>
        /// <returns>Polynomial value at the selected point.</returns>
        public Cplx Eval(Cplx x)
        {
            Cplx s = Cplx.Zero;

            // Sum every coefficient multiplied by the input raised to matching power.
            for (int i = 0; i < Coe.Count; i++)
            {
                Cplx coef = Coe[i];
                Cplx bx = x;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                    {
                        bx = bx.Multiply(x);
                    }

                    coef = coef.Multiply(bx);
                }

                s = s.Add(coef);
            }

            return s;
        }

        /// <summary>
        /// Converts polynomial to the original readable text representation.
        /// </summary>
        /// <returns>Text representation of polynomial coefficients and powers.</returns>
        public override string ToString()
        {
            string s = "";
            int i = 0;

            // Append every coefficient and add one x character for each power level.
            for (; i < Coe.Count; i++)
            {
                s += Coe[i];

                if (i > 0)
                {
                    int j = 0;

                    for (; j < i; j++)
                    {
                        s += "x";
                    }
                }

                if (i + 1 < Coe.Count)
                {
                    s += " + ";
                }
            }

            return s;
        }
    }
}
