using Newtonsoft.Json;
using SSE.Engine.Application.Constants;
using SSE.Engine.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SSE.Engine.Application.Services.Implementations
{
    public class VectorDataService : IVectorDataService
    {
        private readonly IHttpClientFactory _clientFactory;
        public VectorDataService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<double[]> GetVector(string word)
        {
            var client = _clientFactory.CreateClient(APINames.VECTOR_DATA);
            var response = await client.GetFromJsonAsync<double[]>("vector/get?Word=" + word);
            return response;
        }

        public async Task<List<double[]>?> GetVectors(List<string> words)
        {
            string jsonData = JsonConvert.SerializeObject(words);

            var client = _clientFactory.CreateClient(APINames.VECTOR_DATA);

         
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("vector/getlist", content);
            return JsonConvert.DeserializeObject<List<double[]>>(await response.Content.ReadAsStringAsync());
        }
    }
}
