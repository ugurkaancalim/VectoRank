using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.Engine.Application.Services.Interfaces
{
    public interface IVectorDataService
    {
        Task<double[]> GetVector(string word);
        Task<List<double[]>?> GetVectors(List<string> words);
    }
}
