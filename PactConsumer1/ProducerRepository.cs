using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace PactConsumer1 {
    public class ProducerRepository {
        private readonly IRestClient _restClient;

        public ProducerRepository(IRestClient restClient) {
            _restClient = restClient;
        }

        public async Task<ProductDto> GetProductById(int id) {
            var request = new RestRequest($"api/Values/Product/{id}", Method.GET) {
                RequestFormat = DataFormat.Json
            };
            var result = await _restClient.ExecuteTaskAsync(request);
            if (result.StatusCode == HttpStatusCode.NotFound) {
                return null;
            }
            if (result.StatusCode == HttpStatusCode.OK) {
                return JsonConvert.DeserializeObject<ProductDto>(result.Content);
            }
            throw new ExternalException(result.ErrorMessage);
        }

        public async Task<ProductDto> CreateProductOfType(string type) {
            var request = new RestRequest($"api/Values/CreateProduct/", Method.POST) {
                RequestFormat = DataFormat.Json
            };
            request.AddHeader("Content-Type", "application/json");
            request.AddBody(new { Type = type });
            var result = await _restClient.ExecuteTaskAsync(request);
            if (result.StatusCode == HttpStatusCode.Created) {
                return JsonConvert.DeserializeObject<ProductDto>(result.Content);
            }
            throw new ExternalException(result.ErrorMessage);
        }
    }
}
