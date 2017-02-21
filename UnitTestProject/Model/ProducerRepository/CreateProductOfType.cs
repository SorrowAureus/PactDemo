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
    public class CreateProductOfType {
        private Pact1Consumer _pact;

        public CreateProductOfType(Pact1Consumer data) {
            _pact = data;
            data.MockProviderService.ClearInteractions(); //NOTE: Clears any previously registered interactions before the test is run
        }


        [Fact]
        public async Task ShouldReturnObjectIfCreateSucceeds() {
            var type = "TypTvå";
            var clientMock = new ClientMock(_pact) {
                ConditionDescription = $"Creating new product",
                Response = new ProviderServiceResponse {
                    Status = 201,
                    Headers = new Dictionary<string, string> {
                        {"Content-Type", "application/json; charset=utf-8"}
                    },
                    Body = new ProductDto {
                        Id = 10,
                        Time = new DateTime(1984, 01, 01),
                        Type = type
                    }
                }
            };
            var repo = new PactConsumer1.ProducerRepository(clientMock);
            var product = await repo.CreateProductOfType(type);
            Assert.NotNull(product);
            Assert.Equal(type, product.Type);
        }
    }
}
