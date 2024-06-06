using Microsoft.Extensions.Caching.Memory;
using SSE.VectorData.Application.Caching;
using SSE.VectorData.Application.Services.Interfaces;
using SSE.VectorData.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.VectorData.Application.Services.Implementations
{
    public class VectorService : IVectorService
    {
        private readonly IVectorRepository _repository;
        public VectorService(IVectorRepository vectorRepository)
        {
            _repository = vectorRepository;
        }
        public double[]? GetVector(string word)
        {
            return _repository.GetVector(word);
        }

        public List<double[]> GetVectors(List<string> words)
        {
            return _repository.GetVectors(words).ToList();
        }

        public void InsertBaseVectors(string filePath)
        {
            _repository.InsertBaseVectors(filePath);
        }
    }
}
