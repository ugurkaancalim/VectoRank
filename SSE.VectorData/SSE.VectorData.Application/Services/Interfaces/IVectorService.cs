using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.VectorData.Application.Services.Interfaces
{
    public interface IVectorService
    {
        List<double[]> GetVectors(List<string> words);
        double[]? GetVector(string word);
        void InsertBaseVectors(string filePath);
    }
}
