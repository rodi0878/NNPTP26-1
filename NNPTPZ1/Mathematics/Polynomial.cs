using System.Collections.Generic;
using System.Text;

namespace NNPTPZ1.Mathematics
{
    public class Polynomial
    {
        public List<ComplexNumber> Coefficients { get; }

        public Polynomial()
        {
            Coefficients = new List<ComplexNumber>();
        }

        public Polynomial(IEnumerable<ComplexNumber> coefficients)
        {
            Coefficients = new List<ComplexNumber>(coefficients);
        }

        public void Add(ComplexNumber coefficient)
        {
            Coefficients.Add(coefficient);
        }

        public Polynomial Derive()
        {
            var derivative = new Polynomial();
            for (int power = 1; power < Coefficients.Count; power++)
            {
                derivative.Add(Coefficients[power].Multiply(new ComplexNumber(power, 0)));
            }

            return derivative;
        }

        public ComplexNumber Evaluate(double x)
        {
            return Evaluate(new ComplexNumber(x, 0));
        }

        public ComplexNumber Evaluate(ComplexNumber x)
        {
            ComplexNumber sum = ComplexNumber.Zero;
            for (int power = 0; power < Coefficients.Count; power++)
            {
                ComplexNumber term = Coefficients[power];
                if (power > 0)
                {
                    term = term.Multiply(Power(x, power));
                }

                sum = sum.Add(term);
            }

            return sum;
        }

        private static ComplexNumber Power(ComplexNumber x, int exponent)
        {
            ComplexNumber result = x;
            for (int i = 1; i < exponent; i++)
            {
                result = result.Multiply(x);
            }

            return result;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            for (int power = 0; power < Coefficients.Count; power++)
            {
                if (power > 0)
                {
                    builder.Append(" + ");
                }

                builder.Append(Coefficients[power]);
                builder.Append('x', power);
            }

            return builder.ToString();
        }
    }
}
