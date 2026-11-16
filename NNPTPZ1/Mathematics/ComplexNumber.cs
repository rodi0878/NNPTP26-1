using System;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber
    {
        public static readonly ComplexNumber Zero = new ComplexNumber(0, 0);

        public double Real { get; set; }
        public double Imaginary { get; set; }

        public ComplexNumber() : this(0, 0)
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
            return new ComplexNumber(
                Real * other.Real - Imaginary * other.Imaginary,
                Real * other.Imaginary + Imaginary * other.Real
            );
        }

        public ComplexNumber Divide(ComplexNumber other)
        {
            double denominator = other.Real * other.Real + other.Imaginary * other.Imaginary;
            // avoid div by zero
            if (denominator == 0)
            {
                throw new DivideByZeroException("can not divide by zero complex number");
            }

            return new ComplexNumber(
                (Real * other.Real + Imaginary * other.Imaginary) / denominator,
                (Imaginary * other.Real - Real * other.Imaginary) / denominator
            );
        }

        public double GetAbsoluteValue()
        {
            return Math.Sqrt(Real * Real + Imaginary * Imaginary);
        }

        // keep old method for compatibility
        public double GetAbS()
        {
            return GetAbsoluteValue();
        }

        public double GetAngleInRadians()
        {
            return Math.Atan2(Imaginary, Real);
        }

        // return angle in degree
        public double GetAngleInDegrees()
        {
            return GetAngleInRadians() * 180.0 / Math.PI;
        }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber other)
            {
                return Math.Abs(Real - other.Real) < 1e-7 && Math.Abs(Imaginary - other.Imaginary) < 1e-7;
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
