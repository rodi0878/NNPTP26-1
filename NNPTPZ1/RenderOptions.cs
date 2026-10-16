namespace NNPTPZ1
{
    internal sealed class RenderOptions
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public double XMin { get; private set; }
        public double XMax { get; private set; }
        public double YMin { get; private set; }
        public double YMax { get; private set; }
        public string OutputPath { get; private set; }

        public static RenderOptions Parse(string[] args)
        {
            return new RenderOptions
            {
                Width = int.Parse(args[0]),
                Height = int.Parse(args[1]),
                XMin = double.Parse(args[2]),
                XMax = double.Parse(args[3]),
                YMin = double.Parse(args[4]),
                YMax = double.Parse(args[5]),
                OutputPath = args[6]
            };
        }
    }
}