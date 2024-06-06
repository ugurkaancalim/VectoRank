using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.Core.VectoRank.Sentence2Vector
{
    internal class AveragingMethod
    {
        public static double[]? Execute(List<double[]> vectors)
        {
            if (vectors.Count == 0) return null;
            // Aggregate word vectors using averaging
            double[] sentenceVector = new double[vectors[0].Length];
            int wordCount = 0;
            foreach (double[] vector in vectors)
            {
                for (int i = 0; i < vector.Length; i++)
                    sentenceVector[i] += vector[i];
                wordCount++;
            }

            // Calculate average
            if (wordCount > 0)
            {
                for (int i = 0; i < sentenceVector.Length; i++)
                {
                    sentenceVector[i] /= wordCount;
                }
            }

            return sentenceVector;
        }
    }
}
