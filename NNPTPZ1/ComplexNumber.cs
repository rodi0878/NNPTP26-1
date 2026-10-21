using System;

namespace NNPTPZ1
{

    namespace Mathematics
    {
        public class ComplexNumber
        {
            public static readonly ComplexNumber Zero = new ComplexNumber()
            {
                Real = 0,
                Imaginary = 0
            };

            public double Real { get; set; }
            public float Imaginary { get; set; }

            public override bool Equals(object obj)
            {
                ComplexNumber other = obj as ComplexNumber;

                return other != null
                    && Real.Equals(other.Real)
                    && Imaginary.Equals(other.Imaginary);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (Real.GetHashCode() * 397)
                        ^ Imaginary.GetHashCode();
                }
            }

            public ComplexNumber Multiply(ComplexNumber other)
            {
                // aRe*bRe + aRe*bIm*i + aIm*bRe*i + aIm*bIm*i*i
                return new ComplexNumber()
                {
                    Real = Real * other.Real - Imaginary * other.Imaginary,
                    Imaginary = (float)(Real * other.Imaginary + Imaginary * other.Real)
                };
            }

            public double GetAbs()
            {
                return Math.Sqrt(Real * Real + Imaginary * Imaginary);
            }

            public ComplexNumber Add(ComplexNumber other)
            {
                return new ComplexNumber()
                {
                    Real = Real + other.Real,
                    Imaginary = Imaginary + other.Imaginary
                };
            }

            public double GetAngleInRadians()
            {
                return Math.Atan2(Imaginary, Real);
            }

            public ComplexNumber Subtract(ComplexNumber other)
            {
                return new ComplexNumber()
                {
                    Real = Real - other.Real,
                    Imaginary = Imaginary - other.Imaginary
                };
            }

            public override string ToString()
            {
                return $"({Real} + {Imaginary}i)";
            }

            internal ComplexNumber Divide(ComplexNumber other)
            {
                // (aRe + aIm*i) / (bRe + bIm*i)
                // ((aRe + aIm*i) * (bRe - bIm*i)) / ((bRe + bIm*i) * (bRe - bIm*i))
                //  bRe*bRe - bIm*bIm*i*i
                var numerator = this.Multiply(new ComplexNumber() { Real = other.Real, Imaginary = -other.Imaginary });
                var denominator = other.Real * other.Real + other.Imaginary * other.Imaginary;

                return new ComplexNumber()
                {
                    Real = numerator.Real / denominator,
                    Imaginary = (float)(numerator.Imaginary / denominator)
                };
            }
        }
    }
}
