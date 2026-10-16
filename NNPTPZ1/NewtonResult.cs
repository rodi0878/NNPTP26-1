using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    internal sealed class NewtonResult
    {
        public Cplx Point { get; }
        public float IterationCount { get; }

        public NewtonResult(Cplx point, float iterationCount)
        {
            Point = point;
            IterationCount = iterationCount;
        }
    }
}