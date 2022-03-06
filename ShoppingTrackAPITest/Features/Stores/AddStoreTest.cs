using System.Threading.Tasks;
using System.Threading;

using Xunit;

using Microsoft.EntityFrameworkCore;

using ShoppingTrackAPITest.Setup;
using ShoppingTrackAPI.Features.Stores;

namespace ShoppingTrackAPITest.Features.Stores
{
    [Collection("api")]
    public class AddStoreTest
    {
        private readonly TestContext _testContext;
        private readonly CancellationToken _cancellationToken = CancellationToken.None;

        public AddStoreTest(TestContext context)
        {
            _testContext = context;
        }

        [Fact]
        public async Task AddStoreWithoutId_ReturnsCreatedStore()
        {
            // Arrange
            const string storeName = "testStore";
            var storeToAdd = GetDefaultStore(storeName);
            var command = new AddStore.Command(storeToAdd);

            // Act
            var response = await _testContext.Mediator.Send(command, _cancellationToken);
            
            // Assert
            #pragma warning disable xUnit2002
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