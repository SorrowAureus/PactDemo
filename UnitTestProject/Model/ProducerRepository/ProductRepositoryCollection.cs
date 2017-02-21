using UnitTestProject.Consumer1Tests;
using Xunit;

namespace UnitTestProject.ProducerRepository {
    [CollectionDefinition("ProductRepositoryCollection")]
    public class ProductRepositoryCollection : ICollectionFixture<Pact1Consumer> {
    }
}
