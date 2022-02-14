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

        public GetStoresTest(TestContext context)
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
            var response = await _handler.Handle(query, CancellationToken.None);
            
            //Assert
            Assert.NotNull(response);
            Assert.InRange(response.Count, 17, int.MaxValue);
        }
    }
}