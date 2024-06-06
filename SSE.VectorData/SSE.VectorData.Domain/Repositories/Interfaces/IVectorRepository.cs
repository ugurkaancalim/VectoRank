using SSE.VectorData.Domain.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.VectorData.Domain.Repositories.Interfaces
{
    public interface IVectorRepository
    {
        IEnumerable<double[]> GetVectors(List<string> words);
        double[]? GetVector(string word);
        void InsertBaseVectors(string filePath);
    }
}
