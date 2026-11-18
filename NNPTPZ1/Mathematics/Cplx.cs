using System;

namespace NNPTPZ1.Mathematics
{
    /// <summary>
    /// Represents a complex number used by polynomial and Newton iteration logic.
    /// </summary>
    public class Cplx
    {
        /// <summary>
        /// Gets or sets the real part of the complex number.
        /// </summary>
        public double Re { get; set; }

        /// <summary>
        /// Gets or sets the imaginary part of the complex number.
        /// </summary>
        public float Imaginari { get; set; }

        /// <summary>
        /// Represents the zero complex number.
        /// </summary>
        public static readonly Cplx Zero = new Cplx()
        {
            Re = 0,
            Imaginari = 0
        };

        /// <summary>
        /// Checks whether another object has the same real and imaginary parts.
        /// </summary>
        /// <param name="obj">Compared object.</param>
        /// <returns>True when both complex numbers have the same values; otherwise false.</returns>
        public override bool Equals(object obj)
        {
            // Equality is intentionally exact because current tests use exact arithmetic inputs.
            if (obj is Cplx x)
            {
                return x.Re == Re && x.Imaginari == Imaginari;
            }

            return base.Equals(obj);
        }

        /// <summary>
        /// Builds a hash code from the same values that are used by <see cref="Equals(object)"/>.
        /// </summary>
        /// <returns>Hash code for the current complex number.</returns>
        public override int GetHashCode()
        {
            // Keep hash code consistent with exact equality above.
            return Re.GetHashCode() ^ Imaginari.GetHashCode();
        }

        /// <summary>
        /// Multiplies current complex number by another complex number.
        /// </summary>
        /// <param name="b">Complex number used as the multiplier.</param>
        /// <returns>Result of complex multiplication.</returns>
        public Cplx Multiply(Cplx b)
        {
            Cplx a = this;

            // Expand (a.Re + a.Imaginari*i) * (b.Re + b.Imaginari*i).
            return new Cplx()
            {
                Re = a.Re * b.Re - a.Imaginari * b.Imaginari,
                Imaginari = (float)(a.Re * b.Imaginari + a.Imaginari * b.Re)
            };
        }

        /// <summary>
        /// Calculates the absolute value of the current complex number.
        /// </summary>
        /// <returns>Absolute value of the complex number.</returns>
        public double GetAbS()
        {
            // Use Pythagoras theorem for the distance from zero in the complex plane.
            return Math.Sqrt(Re * Re + Imaginari * Imaginari);
        }

        /// <summary>
        /// Adds another complex number to the current complex number.
        /// </summary>
        /// <param name="b">Complex number to add.</param>
        /// <returns>Sum of both complex numbers.</returns>
        public Cplx Add(Cplx b)
        {
            Cplx a = this;

            // Add real and imaginary parts independently.
            return new Cplx()
            {
                Re = a.Re + b.Re,
                Imaginari = a.Imaginari + b.Imaginari
            };
        }

        /// <summary>
        /// Calculates the angle of the current complex number.
        /// </summary>
        /// <returns>Angle value based on the original implementation.</returns>
        public double GetAngleInDegrees()
        {
            // Preserve the previous formula even though it returns radians from Math.Atan.
            return Math.Atan(Imaginari / Re);
        }

        /// <summary>
        /// Subtracts another complex number from the current complex number.
        /// </summary>
        /// <param name="b">Complex number to subtract.</param>
        /// <returns>Difference of both complex numbers.</returns>
        public Cplx Subtract(Cplx b)
        {
            Cplx a = this;

            // Subtract real and imaginary parts independently.
            return new Cplx()
            {
                Re = a.Re - b.Re,
                Imaginari = a.Imaginari - b.Imaginari
            };
        }

        /// <summary>
        /// Converts the current complex number to a readable text representation.
        /// </summary>
        /// <returns>Text representation of the current complex number.</returns>
        public override string ToString()
        {
            return $"({Re} + {Imaginari}i)";
        }

        /// <summary>
        /// Divides current complex number by another complex number.
        /// </summary>
        /// <param name="b">Complex number used as the divisor.</param>
        /// <returns>Result of complex division.</returns>
        internal Cplx Divide(Cplx b)
        {
            // Multiply by the conjugated divisor and divide by the squared magnitude.
            var tmp = Multiply(new Cplx() { Re = b.Re, Imaginari = -b.Imaginari });
            var tmp2 = b.Re * b.Re + b.Imaginari * b.Imaginari;

            return new Cplx()
            {
                Re = tmp.Re / tmp2,
                Imaginari = (float)(tmp.Imaginari / tmp2)
            };
        }
    }
}
