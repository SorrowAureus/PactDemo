using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using RestSharp;
using UnitTestProject.Consumer1Tests;

namespace UnitTestProject.Model {
    public class ClientMock : RestClient {
        private readonly IMockProviderService _mockProviderService;
        private string RequestDescription { get; set; }
        public ClientMock(PactConsumer data, [CallerMemberName] string key = "") : base(data.MockProviderServiceBaseUri) {
            RequestDescription = key;
            _mockProviderService = data.MockProviderService;
        }

        public override Task<IRestResponse> ExecuteTaskAsync(IRestRequest request) {
            _mockProviderService
                .Given(ConditionDescription)
                .UponReceiving(RequestDescription)
                .With(request)
                .WillRespondWith(Response);
            return base.ExecuteTaskAsync(request);
        }

        public ProviderServiceResponse Response { get; set; }

        public string ConditionDescription { get; set; }
    }
}
