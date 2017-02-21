using System;
using Microsoft.Owin.Testing;
using PactNet;
using PactNet.Reporters.Outputters;
using PactProducer;
using ProducerTests;
using Xunit;
using Xunit.Abstractions;

namespace UnitTestProject.ProducerTests {
    public class PactProducerTests {
        private readonly ITestOutputHelper _output;

        public PactProducerTests(ITestOutputHelper output) {
            _output = output;
        }
        [Fact]
        public void EnsureConsumerHonoursPactWithpPact1Consumer() {
            //Arrange
            var config = new PactVerifierConfig();
            config.ReportOutputters.Clear();
            config.ReportOutputters.Add(new ConsoleOutputter(_output));

            var pactVerifier = new PactVerifier(() => { }, () => { }, config);
            pactVerifier
                .ProviderState("No subject with id '8' exists")
                .ProviderState("Subject with id '1' exists")
                .ProviderState("Creating new product")
                ;
            //Act / Assert
            using (var testServer = TestServer.Create<TestStartup>()) {
                testServer.BaseAddress = new Uri("http://localhost");
                pactVerifier
                    .ServiceProvider("Something API", testServer.HttpClient)
                    .HonoursPactWith("customerservice")
                    .PactUri($@"..\..\..\UnitTestProject\pacts\pact1consumer-pactproducer.json")
                    .Verify(); //NOTE: Optionally you can control what interactions are verified by specifying a providerDescription and/or providerState
            }
        }

        public class ConsoleOutputter : IReportOutputter {
            private readonly ITestOutputHelper _output;

            public ConsoleOutputter(ITestOutputHelper output) {
                _output = output;
            }

            public void Write(string report) {
                _output.WriteLine(report);
            }
        }
    }
}
