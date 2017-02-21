using System;
using System.Dynamic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using RestSharp;

namespace UnitTestProject.Model {
    public static class MockProviderServiceExtensions {
        public static IMockProviderService With(this IMockProviderService service, IRestRequest request) {
            return service.With(new ProviderServiceRequest {
                Method = request.Method.ToHttpVerb(),
                Path = "/" + request.Resource,
                Headers = request.Parameters.Where(p => p.Type == ParameterType.HttpHeader).ToDictionary(p => p.Name, p => p.Value.ToString()),
                Body = request.GetRequestBody(),
            });
        }

        private static dynamic GetRequestBody(this IRestRequest request) {
            var requestBody = request.Parameters.SingleOrDefault(p => p.Type == ParameterType.RequestBody);
            if (requestBody == null) return null;
            return JsonConvert.DeserializeObject<ExpandoObject>(requestBody.Value.ToString(), new ExpandoObjectConverter());
        }

        private static HttpVerb ToHttpVerb(this Method method) {
            switch (method) {
                case Method.GET:
                    return HttpVerb.Get;
                case Method.POST:
                    return HttpVerb.Post;
                case Method.PUT:
                    return HttpVerb.Put;
                case Method.DELETE:
                    return HttpVerb.Delete;
                case Method.HEAD:
                    return HttpVerb.Head;
                case Method.PATCH:
                    return HttpVerb.Patch;
                case Method.OPTIONS:
                case Method.MERGE:
                default:
                    throw new ArgumentOutOfRangeException(nameof(method), method, null);
            }
        }
    }
}
