using System;
using System.Collections.Generic;
using System.Text;

namespace NNPTPZ1.Mathematics
{
    public class Polynomial
    {
        public List<ComplexNumber> Coefficients { get; set; } = new List<ComplexNumber>();

        // backward compatibility with old property name
        public List<ComplexNumber> Coe => Coefficients;

        public Polynomial()
        {
        }

        public Polynomial(IEnumerable<ComplexNumber> coefficients)
        {
            Coefficients.AddRange(coefficients);
        }

        public void Add(ComplexNumber coefficient)
        {
            Coefficients.Add(coefficient);
        }

        // calculate derivative of poly
        public Polynomial Derive()
        {
            Polynomial derived = new Polynomial();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                derived.Add(Coefficients[i].Multiply(new ComplexNumber(i, 0)));
            }

            return derived;
        }

        // eval poly at point using horner method
        public ComplexNumber Evaluate(ComplexNumber x)
        {
            if (Coefficients.Count == 0)
            {
                return ComplexNumber.Zero;
            }

            ComplexNumber result = Coefficients[Coefficients.Count - 1];
            for (int i = Coefficients.Count - 2; i >= 0; i--)
            {
                result = result.Multiply(x).Add(Coefficients[i]);
            }

            return result;
        }

        public ComplexNumber Evaluate(double x)
        {
            return Evaluate(new ComplexNumber(x, 0));
        }

        // old eval method name
        public ComplexNumber Eval(ComplexNumber x)
        {
            return Evaluate(x);
        }

        public ComplexNumber Eval(double x)
        {
            return Evaluate(x);
        }

        public override string ToString()
        {
            if (Coefficients.Count == 0)
            {
                return "0";
            }

            List<string> parts = new List<string>();
            for (int i = 0; i < Coefficients.Count; i++)
            {
                string part = Coefficients[i].ToString();
                if (i == 1)
                {
                    part += "x";
                }
                else if (i > 1)
                {
                    part += "x^" + i;
                }
                parts.Add(part);
            }

            return string.Join(" + ", parts);
        }
    }
}
