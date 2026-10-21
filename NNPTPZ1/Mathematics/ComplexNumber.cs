using System;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber
    {
        public static ComplexNumber Zero => new ComplexNumber(0, 0);

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
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return new ComplexNumber
            {
                Real = Real + other.Real,
                Imaginary = Imaginary + other.Imaginary
            };
        }

        public ComplexNumber Subtract(ComplexNumber other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return new ComplexNumber
            {
                Real = Real - other.Real,
                Imaginary = Imaginary - other.Imaginary
            };
        }

        public ComplexNumber Multiply(ComplexNumber other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return new ComplexNumber
            {
                Real = Real * other.Real - Imaginary * other.Imaginary,
                Imaginary = Real * other.Imaginary + Imaginary * other.Real
            };
        }

        public ComplexNumber Divide(ComplexNumber other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            ComplexNumber conjugate = new ComplexNumber(other.Real, -other.Imaginary);
            ComplexNumber numerator = Multiply(conjugate);
            double denominator = other.Real * other.Real + other.Imaginary * other.Imaginary;

            return new ComplexNumber
            {
                Real = numerator.Real / denominator,
                Imaginary = numerator.Imaginary / denominator
            };
        }

        public double GetAbsoluteValue()
        {
            return Math.Sqrt(Real * Real + Imaginary * Imaginary);
        }

        public double GetAngleInRadians()
        {
            return Math.Atan2(Imaginary, Real);
        }

        public double GetAngleInDegrees()
        {
            return GetAngleInRadians() * (180.0 / Math.PI);
        }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber other)
            {
                return other.Real == Real && other.Imaginary == Imaginary;
            }

            return false;
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
