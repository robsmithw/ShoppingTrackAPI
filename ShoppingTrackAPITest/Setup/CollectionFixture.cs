using Xunit;

namespace ShoppingTrackAPITest.Setup
{
    [CollectionDefinition("api")]
    public class CollectionFixture : ICollectionFixture<TestContext> { }
}
