using System;
using System.Collections.Generic;
using System.Text;

namespace NNPTPZ1.Mathematics
{
    public class Polynomial
    {
        public List<ComplexNumber> Coefficients { get; set; }

        public Polynomial()
        {
            Coefficients = new List<ComplexNumber>();
        }

        public Polynomial(params ComplexNumber[] coefficients)
        {
            if (coefficients == null)
            {
                throw new ArgumentNullException(nameof(coefficients));
            }

            Coefficients = new List<ComplexNumber>(coefficients);
        }

        public void Add(ComplexNumber coefficient)
        {
            if (coefficient == null)
            {
                throw new ArgumentNullException(nameof(coefficient));
            }

            Coefficients.Add(coefficient);
        }

        public Polynomial Derive()
        {
            Polynomial derivative = new Polynomial();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                ComplexNumber factor = new ComplexNumber(i, 0);
                derivative.Add(Coefficients[i].Multiply(factor));
            }

            return derivative;
        }

        public ComplexNumber Evaluate(double realValue)
        {
            return Evaluate(new ComplexNumber(realValue, 0));
        }

        public ComplexNumber Evaluate(ComplexNumber point)
        {
            if (point == null)
            {
                throw new ArgumentNullException(nameof(point));
            }

            ComplexNumber sum = ComplexNumber.Zero;
            ComplexNumber currentPower = ComplexNumber.Zero;

            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber term = Coefficients[i];
                if (i == 1)
                {
                    currentPower = point;
                    term = term.Multiply(currentPower);
                }
                else if (i > 1)
                {
                    currentPower = currentPower.Multiply(point);
                    term = term.Multiply(currentPower);
                }

                sum = sum.Add(term);
            }

            return sum;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < Coefficients.Count; i++)
            {
                if (i > 0)
                {
                    builder.Append(" + ");
                }

                builder.Append(Coefficients[i]);
                builder.Append('x', i);
            }

            return builder.ToString();
        }
    }
}
