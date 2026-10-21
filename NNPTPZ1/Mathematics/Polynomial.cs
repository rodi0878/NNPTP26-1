using System;
using System.Collections.Generic;
using System.Text;

namespace NNPTPZ1.Mathematics
{
    /// <summary>Coefficients are ordered from constant term to highest power.</summary>
    public sealed class Polynomial
    {
        private readonly ComplexNumber[] coefficients;

        public IReadOnlyList<ComplexNumber> Coefficients
        {
            get { return Array.AsReadOnly(coefficients); }
        }

        public Polynomial(params ComplexNumber[] coefficients)
        {
            if (coefficients == null || coefficients.Length == 0)
                throw new ArgumentException("At least one coefficient is required.", "coefficients");

            this.coefficients = (ComplexNumber[])coefficients.Clone();
        }

        public Polynomial Derivative()
        {
            if (coefficients.Length == 1)
                return new Polynomial(ComplexNumber.Zero);

            var derived = new ComplexNumber[coefficients.Length - 1];
            for (int power = 1; power < coefficients.Length; power++)
                derived[power - 1] = coefficients[power].Multiply(new ComplexNumber(power, 0));

            return new Polynomial(derived);
        }

        public ComplexNumber Evaluate(ComplexNumber argument)
        {
            // Horner's method avoids recomputing x^n for every coefficient.
            ComplexNumber value = coefficients[coefficients.Length - 1];
            for (int index = coefficients.Length - 2; index >= 0; index--)
                value = value.Multiply(argument).Add(coefficients[index]);

            return value;
        }

        public ComplexNumber Evaluate(double argument)
        {
            return Evaluate(new ComplexNumber(argument, 0));
        }

        public override string ToString()
        {
            var terms = new StringBuilder();
            for (int power = 0; power < coefficients.Length; power++)
            {
                if (power > 0)
                    terms.Append(" + ");
                terms.Append(coefficients[power]);
                if (power > 0)
                    terms.Append("x^").Append(power);
            }
            return terms.ToString();
        }
    }
}
