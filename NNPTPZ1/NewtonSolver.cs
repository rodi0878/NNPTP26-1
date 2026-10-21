using System;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    internal sealed class NewtonSolver
    {
        private const int RequiredSmallStepCount = 30;
        private const double LargeStepSquaredThreshold = 0.5;

        private readonly Polynomial polynomial;
        private readonly Polynomial derivative;

        public NewtonSolver(Polynomial polynomial, Polynomial derivative)
        {
            this.polynomial = polynomial;
            this.derivative = derivative;
        }

        public NewtonResult Solve(ComplexNumber point)
        {
            float iterationCount = 0;

            for (int smallStepCount = 0;
                 smallStepCount < RequiredSmallStepCount;
                 smallStepCount++)
            {
                ComplexNumber step = polynomial.Eval(point)
                    .Divide(derivative.Eval(point));

                point = point.Subtract(step);

                if (IsLargeStep(step))
                {
                    smallStepCount--;
                }

                iterationCount++;
            }

            return new NewtonResult(point, iterationCount);
        }

        private static bool IsLargeStep(ComplexNumber step)
        {
            return Math.Pow(step.Real, 2) + Math.Pow(step.Imaginary, 2)
                >= LargeStepSquaredThreshold;
        }
    }
}