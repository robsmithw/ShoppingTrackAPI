using System.Threading.Tasks;
using System.Threading;

using Xunit;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using ShoppingTrackAPITest.Setup;
using ShoppingTrackAPI.Features.Stores;
using static ShoppingTrackAPI.Features.Stores.AddStore;

namespace ShoppingTrackAPITest.Features.Stores
{
    [Collection("api")]
    public class AddStoreTest
    {
        private readonly TestContext _testContext;
        private readonly Handler _handler;

        public AddStoreTest(TestContext context)
        {
            _testContext = context;
            _handler = new AddStore.Handler(_testContext.DbContext, new LoggerFactory().CreateLogger<AddStore>());
        }

        [Fact]
        public async Task AddStoreWithoutId_ReturnsCreatedStore()
        {
            // Arrange
            const string storeName = "testStore";
            var storeToAdd = GetDefaultStore(storeName);
            var command = new AddStore.Command(storeToAdd);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);
            
            // Assert
            Assert.NotNull(response);
            var storeCreated = await _testContext.DbContext.Stores
                .FirstOrDefaultAsync(x => x.Name == storeToAdd.Name, CancellationToken.None);
            Assert.NotNull(storeCreated);
            Assert.Equal(storeName, storeCreated.Name);
            Assert.True(storeCreated.Id != default);
        }

        private ShoppingTrackAPI.Models.Store GetDefaultStore(string name) =>
            new ShoppingTrackAPI.Models.Store()
            {
                Name = name
            };
    }
}