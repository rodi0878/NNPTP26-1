using System;
using System.Globalization;

namespace NNPTPZ1.Mathematics
{
    /// <summary>An immutable complex number with double-precision components.</summary>
    public struct ComplexNumber : IEquatable<ComplexNumber>
    {
        public static readonly ComplexNumber Zero = new ComplexNumber(0, 0);

        public double Real { get; private set; }
        public double Imaginary { get; private set; }

        public ComplexNumber(double real, double imaginary)
            : this()
        {
            Real = real;
            Imaginary = imaginary;
        }

        public double SquaredMagnitude
        {
            get { return Real * Real + Imaginary * Imaginary; }
        }

        public bool IsFinite
        {
            get
            {
                return !double.IsNaN(Real) && !double.IsInfinity(Real) &&
                       !double.IsNaN(Imaginary) && !double.IsInfinity(Imaginary);
            }
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
            return new ComplexNumber(
                Real * other.Real - Imaginary * other.Imaginary,
                Real * other.Imaginary + Imaginary * other.Real);
        }

        public ComplexNumber Divide(ComplexNumber other)
        {
            double denominator = other.SquaredMagnitude;
            if (denominator == 0)
                throw new DivideByZeroException("Cannot divide by zero in the complex plane.");

            return new ComplexNumber(
                (Real * other.Real + Imaginary * other.Imaginary) / denominator,
                (Imaginary * other.Real - Real * other.Imaginary) / denominator);
        }

        public bool Equals(ComplexNumber other)
        {
            return Real == other.Real && Imaginary == other.Imaginary;
        }

        public override bool Equals(object obj)
        {
            return obj is ComplexNumber && Equals((ComplexNumber)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Real.GetHashCode() * 397) ^ Imaginary.GetHashCode();
            }
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "({0} {1} {2}i)",
                Real, Imaginary < 0 ? "-" : "+", Math.Abs(Imaginary));
        }
    }
}
