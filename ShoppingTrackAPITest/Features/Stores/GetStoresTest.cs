using System.Threading.Tasks;
using System.Threading;

using Xunit;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using ShoppingTrackAPITest.Setup;
using ShoppingTrackAPI.Features.Stores;

namespace ShoppingTrackAPITest.Features.Stores
{
    [Collection("api")]
    public class GetStoresTest
    {
        private readonly TestContext _testContext;
        private readonly CancellationToken _cancellationToken = CancellationToken.None;

        public GetStoresTest(TestContext context)
        {
            _testContext = context;
        }

        [Fact]
        public async Task GetStores_ReturnsAllStores()
        {
            // Arrange
            var query = new GetStores.Query();

            // Act
            var response = await _testContext.Mediator.Send(query, _cancellationToken);
            
            // Assert
            Assert.NotNull(response);
            Assert.InRange(response.Count, 17, int.MaxValue);
        }
    }
}