using System;

namespace NNPTPZ1.Mathematics
{
    /// <summary>
    /// Immutable-style complex number with basic arithmetic operations.
    /// </summary>
    public class ComplexNumber
    {
        public static readonly ComplexNumber Zero = new ComplexNumber(0, 0);

        public double Real { get; set; }
        public double Imaginary { get; set; }

        public ComplexNumber()
        {
        }

        public ComplexNumber(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public ComplexNumber Add(ComplexNumber other)
        {
            return new ComplexNumber(Real + other.Real, Imaginary + other.Imaginary);
        }

        public ComplexNumber Subtract(ComplexNumber other)
        {
            return new ComplexNumber(Real - other.Real, Imaginary - other.Imaginary);
        }

        public ComplexNumber Multiply(ComplexNumber other)
        {
            // (a + bi)(c + di) = (ac - bd) + (ad + bc)i
            return new ComplexNumber(
                Real * other.Real - Imaginary * other.Imaginary,
                Real * other.Imaginary + Imaginary * other.Real);
        }

        public ComplexNumber Divide(ComplexNumber other)
        {
            // (a + bi) / (c + di) = ((a + bi)(c - di)) / (c^2 + d^2)
            ComplexNumber numerator = Multiply(other.Conjugate());
            double denominator = other.Real * other.Real + other.Imaginary * other.Imaginary;

            return new ComplexNumber(
                numerator.Real / denominator,
                numerator.Imaginary / denominator);
        }

        public ComplexNumber Conjugate()
        {
            return new ComplexNumber(Real, -Imaginary);
        }

        public double GetAbsoluteValue()
        {
            return Math.Sqrt(Real * Real + Imaginary * Imaginary);
        }

        public double GetAngleInDegrees()
        {
            return Math.Atan(Imaginary / Real);
        }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber other)
            {
                return other.Real == Real && other.Imaginary == Imaginary;
            }
            return base.Equals(obj);
        }

        public override string ToString()
        {
            return $"({Real} + {Imaginary}i)";
        }
    }
}
