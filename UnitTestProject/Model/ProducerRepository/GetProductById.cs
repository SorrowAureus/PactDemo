using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PactConsumer1;
using PactNet.Mocks.MockHttpService.Models;
using UnitTestProject.Consumer1Tests;
using UnitTestProject.Model;
using Xunit;

namespace UnitTestProject.ProducerRepository {
    [Collection("ProductRepositoryCollection")]
    public class GetProductById {
        private Pact1Consumer _pact;

        public GetProductById(Pact1Consumer data) {
            _pact = data;
            data.MockProviderService.ClearInteractions(); //NOTE: Clears any previously registered interactions before the test is run
        }
        //[Fact]
        //public async Task TestMethod1() {
        //    var id = 8;
        //    var clientMock = new Mock<IRestClient>();
        //    clientMock
        //        .Setup(m => m.ExecuteTaskAsync(It.IsAny<IRestRequest>()))
        //        .ReturnsAsync(new RestResponse { StatusCode = HttpStatusCode.NotFound });

        //    var repo = new PactConsumer1.ProducerRepository(clientMock.Object);
        //    var product = await repo.GetProductById(id);
        //    Assert.Null(product);
        //}

        [Fact]
        public async Task ShouldReturnNullIfMissing() {
            var id = 8;
            var clientMock = new ClientMock(_pact) {
                ConditionDescription = $"No subject with id '{id}' exists",
                Response = new ProviderServiceResponse {
                    Status = 404
                }
            };
            var repo = new PactConsumer1.ProducerRepository(clientMock);
            var product = await repo.GetProductById(id);
            Assert.Null(product);
        }

        [Fact]
        public async Task ShouldReturnProductIfExists() {
            var id = 1;
            var clientMock = new ClientMock(_pact) {
                ConditionDescription = $"Subject with id '{id}' exists",
                Response = new ProviderServiceResponse {
                    Status = 200,
                    Headers = new Dictionary<string, string> {
                        {"Content-Type", "application/json; charset=utf-8"}
                    },
                    Body = new ProductDto {
                        Id = id,
                        Time = new DateTime(1984, 01, 01),
                        Type = "TypEtt"
                    }
                }
            };
            var repo = new PactConsumer1.ProducerRepository(clientMock);
            var product = await repo.GetProductById(id);
            Assert.NotNull(product);
            Assert.Equal(id, product.Id);
        }

    }
}

