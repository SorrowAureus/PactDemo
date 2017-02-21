using System;
using Newtonsoft.Json;
using PactNet;
using PactNet.Mocks.MockHttpService;

namespace UnitTestProject.Model {
    public abstract class PactConsumer : IDisposable {
        private IPactBuilder PactBuilder { get; }
        public IMockProviderService MockProviderService { get; }
        private static readonly Random _random = new Random();

        public string MockProviderServiceBaseUri { get; }


        public PactConsumer(string consumerName, string producerName) {
            var mockServerPort = _random.Next() % 65535;
            MockProviderServiceBaseUri = $"http://localhost:{mockServerPort}";
            var machine = Environment.MachineName;
            var config = new PactConfig {
                //PactDir = $@"\\sopfile01.int.soderbergpartners.se\TFS\Drops\Liv\Pact\{machine}\pacts",
                //LogDir = @"..\\..\\logs\\"
            };
            PactBuilder = new PactBuilder(config); //Configures the PactDir and/or LogDir.

            PactBuilder.ServiceConsumer(consumerName).HasPactWith(producerName);
            MockProviderService = PactBuilder.MockService(mockServerPort, new JsonSerializerSettings()); //Configure the http mock server
        }

        public void Dispose() {
            PactBuilder.Build(); //NOTE: Will save the pact file once finished
        }
    }
}