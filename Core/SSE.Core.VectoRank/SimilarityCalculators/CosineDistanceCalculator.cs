using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.Core.VectoRank.SimilarityCalculators
{
    public class CosineDistanceCalculator
    {
        // Method to calculate dot product of two vectors
        static double DotProduct(double[] vectorA, double[] vectorB)
        {
            double product = 0;
            for (int i = 0; i < vectorA.Length; i++)
            {
                product += vectorA[i] * vectorB[i];
            }
            return product;
        }

        // Method to calculate magnitude of a vector
        static double Magnitude(double[] vector)
        {
            double sumOfSquares = 0;
            foreach (var component in vector)
            {
                sumOfSquares += component * component;
            }
            return Math.Sqrt(sumOfSquares);
        }

        // Method to calculate cosine distance between two vectors
        public static double CosineDistance(double[] vectorA, double[] vectorB)
        {
            if (vectorA.Length != vectorB.Length)
            {
                throw new ArgumentException("Vectors must have the same length.");
            }

            double dotProduct = DotProduct(vectorA, vectorB);
            double magnitudeA = Magnitude(vectorA);
            double magnitudeB = Magnitude(vectorB);

            // Handle division by zero case
            if (magnitudeA == 0 || magnitudeB == 0)
            {
                return 9999999;
                throw new DivideByZeroException("Magnitude of a vector cannot be zero.");
            }

            return 1 - (dotProduct / (magnitudeA * magnitudeB));
        }

    }
}
