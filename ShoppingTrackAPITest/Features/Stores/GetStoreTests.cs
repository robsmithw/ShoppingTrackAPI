using System.Threading.Tasks;
using System.Threading;

using Xunit;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using ShoppingTrackAPITest.Setup;
using ShoppingTrackAPI.Controllers;
using static ShoppingTrackAPI.Controllers.GetStores;

namespace ShoppingTrackAPITest.Features.Stores
{
    [Collection("api")]
    public class GetStoresTest
    {
        private readonly TestContext _testContext;
        private readonly Handler _handler;

        public GetStoresTest(TestContext context, ILogger<GetStores> logger)
        {
            _testContext = context;
            _handler = new GetStores.Handler(_testContext.DbContext, new LoggerFactory().CreateLogger<GetStores>());
        }

        [Fact]
        public async Task GetStores_ReturnsAllStores()
        {
            // Arrange
            var query = new GetStores.Query();

            //Act
            var response = _handler.Handle(query, CancellationToken.None);
            
            //Assert
            var stores = await _testContext.DbContext.Stores.ToListAsync(CancellationToken.None);
            Assert.NotNull(stores);
            Assert.InRange(stores.Count, 17, int.MaxValue);
        }
    }
}