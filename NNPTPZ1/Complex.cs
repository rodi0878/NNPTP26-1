using System;

namespace NNPTPZ1.Mathematics
{
    public class Complex
    {
        public double Real { get; }
        public double Imaginary { get; }

        public Complex(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public static readonly Complex Zero = new Complex(0, 0);

        public override bool Equals(object obj)
        {
            if (obj is Complex complexNumber)
            {
                return complexNumber.Real == Real && complexNumber.Imaginary == Imaginary;
            }
            return false;
        }

        public Complex Add(Complex addValue)
        {
            return new Complex(Real + addValue.Real, Imaginary + addValue.Imaginary);
        }

        public Complex Subtract(Complex minusValue)
        {
            return new Complex(Real - minusValue.Real, Imaginary - minusValue.Imaginary);
        }

        public Complex Multiply(Complex multiplier)
        {
            return new Complex(
                Real * multiplier.Real - Imaginary * multiplier.Imaginary,
                Real * multiplier.Imaginary + Imaginary * multiplier.Real
            );
        }

        public Complex Divide(Complex divisor)
        {
            var numerator = this.Multiply(new Complex(divisor.Real, -divisor.Imaginary));
            var denominator = divisor.Real * divisor.Real + divisor.Imaginary * divisor.Imaginary;

            return new Complex(
                numerator.Real / denominator,
                numerator.Imaginary / denominator
            );
        }

        public double GetAbs() => Math.Sqrt(Real * Real + Imaginary * Imaginary);

        public double GetAngleInDegrees() => Math.Atan2(Imaginary, Real) * (180.0 / Math.PI);

        public override string ToString() => $"({Real} + {Imaginary}i)";
    }
}
