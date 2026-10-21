using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    internal sealed class NewtonResult
    {
        public ComplexNumber Point { get; }
        public float IterationCount { get; }

        public NewtonResult(ComplexNumber point, float iterationCount)
        {
            Point = point;
            IterationCount = iterationCount;
        }
    }
}