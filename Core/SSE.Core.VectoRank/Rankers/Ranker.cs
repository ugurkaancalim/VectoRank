using SSE.Core.VectoRank.Constants;
using SSE.Core.VectoRank.SimilarityCalculators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.Core.VectoRank.Rankers
{
    public class Ranker
    {

        public static double Rank(List<double[]> vectors)
        {
            var avgVector = Sentence2Vector.AveragingMethod.Execute(vectors);
            //generate pivot
            return CosineDistanceCalculator.CosineDistance(avgVector, PivotVector.VALUE);
        }
    }
}
