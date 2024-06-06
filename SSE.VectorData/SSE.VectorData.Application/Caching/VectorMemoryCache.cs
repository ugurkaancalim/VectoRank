using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.VectorData.Application.Caching
{
    public class VectorMemoryCache
    {
        private readonly IMemoryCache cache = new MemoryCache(new MemoryCacheOptions()
        {
            SizeLimit = 104857600//100MB
        });
        public IMemoryCache Cache
        {
            get
            {
                return cache;
            }
        }
    }
}
