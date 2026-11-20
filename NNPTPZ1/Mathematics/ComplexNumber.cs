using System;

namespace NNPTPZ1.Mathematics
{
    public sealed class ComplexNumber : IEquatable<ComplexNumber>
    {
        public static readonly ComplexNumber Zero = new ComplexNumber(0, 0);

        public double Real { get; }

        public float Imaginary { get; }

        public ComplexNumber(double real, float imaginary)
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
            return new ComplexNumber(
                Real * other.Real - Imaginary * other.Imaginary,
                (float)(Real * other.Imaginary + Imaginary * other.Real));
        }

        public ComplexNumber Divide(ComplexNumber other)
        {
            ComplexNumber numerator = Multiply(other.Conjugate());
            double denominator = other.Real * other.Real + other.Imaginary * other.Imaginary;

            return new ComplexNumber(
                numerator.Real / denominator,
                (float)(numerator.Imaginary / denominator));
        }

        public ComplexNumber Conjugate()
        {
            return new ComplexNumber(Real, -Imaginary);
        }

        public double SquaredMagnitude()
        {
            return Real * Real + (double)Imaginary * Imaginary;
        }

        public double Magnitude()
        {
            return Math.Sqrt(SquaredMagnitude());
        }

        public double SquaredDistanceTo(ComplexNumber other)
        {
            return Subtract(other).SquaredMagnitude();
        }

        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b) => a.Add(b);
        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b) => a.Subtract(b);
        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b) => a.Multiply(b);
        public static ComplexNumber operator /(ComplexNumber a, ComplexNumber b) => a.Divide(b);

        public bool Equals(ComplexNumber other)
        {
            return other != null && Real == other.Real && Imaginary == other.Imaginary;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ComplexNumber);
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
            return $"({Real} + {Imaginary}i)";
        }
    }
}
