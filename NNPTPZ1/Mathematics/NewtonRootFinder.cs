using System;

namespace NNPTPZ1.Mathematics
{
    /// <summary>
    /// Finds polynomial roots with Newton iteration.
    /// </summary>
    public class NewtonRootFinder
    {
        private const int MaxIterations = 30;
        private const int MaxAttempts = MaxIterations * 10;
        private const double ZeroOffset = 0.0001;
        private const double LargeStepLimit = 0.5;
        private readonly Poly polynomial;
        private readonly Poly derivative;

        /// <summary>
        /// Creates root finder for selected polynomial.
        /// </summary>
        /// <param name="polynomial">Polynomial used for Newton iteration.</param>
        public NewtonRootFinder(Poly polynomial)
        {
            // Keep derivative precomputed because every pixel uses it repeatedly.
            this.polynomial = polynomial;
            derivative = polynomial.Derive();
        }

        /// <summary>
        /// Finds a root starting from selected complex point.
        /// </summary>
        /// <param name="start">Starting point for Newton iteration.</param>
        /// <returns>Root finding result with root and iteration count.</returns>
        public NewtonRootResult FindRoot(Cplx start)
        {
            Cplx current = AvoidZeroStart(start);
            int iterations = 0;
            int attempts = 0;

            // Keep retry behavior for large Newton jumps, but prevent endless loops on unstable points.
            while (iterations < MaxIterations && attempts < MaxAttempts)
            {
                attempts++;
                Cplx derivativeValue = derivative.Eval(current);

                // Stop on zero derivative because division would produce invalid values.
                if (IsZero(derivativeValue))
                {
                    break;
                }

                Cplx diff = polynomial.Eval(current).Divide(derivativeValue);
                current = current.Subtract(diff);

                // Large jumps do not count as accepted iterations because they are still unstable.
                if (GetSquaredDistance(diff, Cplx.Zero) < LargeStepLimit)
                {
                    iterations++;
                }
            }

            return new NewtonRootResult(current, iterations);
        }

        /// <summary>
        /// Calculates squared distance between two complex numbers.
        /// </summary>
        /// <param name="a">First complex number.</param>
        /// <param name="b">Second complex number.</param>
        /// <returns>Squared distance in the complex plane.</returns>
        public static double GetSquaredDistance(Cplx a, Cplx b)
        {
            // Squared distance is enough for comparisons and avoids unnecessary square root.
            return Math.Pow(a.Re - b.Re, 2) + Math.Pow(a.Imaginari - b.Imaginari, 2);
        }

        /// <summary>
        /// Moves exact zero parts slightly away from zero.
        /// </summary>
        /// <param name="start">Original starting point.</param>
        /// <returns>Starting point safe for Newton iteration.</returns>
        private static Cplx AvoidZeroStart(Cplx start)
        {
            Cplx result = new Cplx()
            {
                Re = start.Re,
                Imaginari = start.Imaginari
            };

            // Preserve the original workaround for exact zero coordinates.
            if (result.Re == 0)
            {
                result.Re = ZeroOffset;
            }

            if (result.Imaginari == 0)
            {
                result.Imaginari = (float)ZeroOffset;
            }

            return result;
        }

        /// <summary>
        /// Checks whether complex number is exactly zero.
        /// </summary>
        /// <param name="value">Complex number to check.</param>
        /// <returns>True when real and imaginary parts are zero.</returns>
        private static bool IsZero(Cplx value)
        {
            // Exact comparison is sufficient because this check protects only against direct zero division.
            return value.Re == 0 && value.Imaginari == 0;
        }
    }
}
