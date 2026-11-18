namespace NNPTPZ1.Mathematics
{
    /// <summary>
    /// Holds result of Newton root finding.
    /// </summary>
    public class NewtonRootResult
    {
        /// <summary>
        /// Gets the calculated root.
        /// </summary>
        public Cplx Root { get; private set; }

        /// <summary>
        /// Gets the number of iterations used during root finding.
        /// </summary>
        public int Iterations { get; private set; }

        /// <summary>
        /// Creates result for Newton root finding.
        /// </summary>
        /// <param name="root">Calculated root.</param>
        /// <param name="iterations">Number of used iterations.</param>
        public NewtonRootResult(Cplx root, int iterations)
        {
            // Store both values together because renderer needs the root and color intensity needs iteration count.
            Root = root;
            Iterations = iterations;
        }
    }
}
